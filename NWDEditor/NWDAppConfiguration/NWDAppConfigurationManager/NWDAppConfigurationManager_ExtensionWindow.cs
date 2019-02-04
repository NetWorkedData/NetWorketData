﻿//=====================================================================================================================
//
// ideMobi copyright 2018 
// All rights reserved by ideMobi
//
//=====================================================================================================================
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//=====================================================================================================================
namespace NetWorkedData
{
	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++	
	/// <summary>
	/// NWD app environment manager.
	/// </summary>
	public partial class NWDAppConfigurationManager
    {
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// The scroll position.
		/// </summary>
		static Vector2 ScrollPosition;
		/// <summary>
		/// The tab selected.
		/// </summary>
		static int TabSelected = 0;
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Menu name.
		/// </summary>
		/// <returns>The name.</returns>
		public static string MenuName ()
		{
			return NWDConstants.K_APP_CONFIGURATION_MENU_NAME;
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Draws in editor.
		/// </summary>
        [NWDAliasMethod(NWDConstants.M_DrawInEditor)]
        public static void DrawInEditor (EditorWindow sEditorWindow, bool sAutoSelect=false)
        {
            float tMinWidht = 270.0F;
            float tScrollMarge = 20.0f;
            int tColum = 1;
            // Draw warning if salt for class is false
            if (NWDDataManager.SharedInstance().TestSaltMemorizationForAllClass () == false) {
				EditorGUILayout.HelpBox (NWDConstants.kAlertSaltShortError, MessageType.Error);
                if (GUILayout.Button(NWDConstants.K_APP_CLASS_SALT_REGENERATE))
                {
                    NWDAppConfiguration.SharedInstance().GenerateCSharpFile(NWDAppConfiguration.SharedInstance().SelectedEnvironment());
                }
			}
			// Draw helpbox
			//EditorGUILayout.HelpBox (NWDConstants.K_APP_CONFIGURATION_HELPBOX, MessageType.None);
			// List environment
			//update the veriosn of Bundle
			//NWDVersion.UpdateVersionBundle ();
			// Draw interface for environment selected inn scrollview
			ScrollPosition = GUILayout.BeginScrollView (ScrollPosition, EditorStyles.inspectorFullWidthMargins, GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true));


            // WEBSERVICES PARAMS
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("WebServices", MessageType.None);
            EditorGUILayout.LabelField("Webservices config for all environements", EditorStyles.boldLabel);
            if (tColum > 1)
            {
                EditorGUILayout.BeginHorizontal();
            }
            EditorGUILayout.BeginVertical(GUILayout.MinWidth(tMinWidht));

            Dictionary<int, bool> tWSList = new Dictionary<int, bool>();
            tWSList.Add(0, true);
            foreach (KeyValuePair<int, bool> tWS in NWDAppConfiguration.SharedInstance().WSList)
            {
                if (tWSList.ContainsKey(tWS.Key) == false)
                {
                    tWSList.Add(tWS.Key, tWS.Value);
                }
            }
            List<int> tWSListUsable = new List<int>();
            List<string> tWSListUsableString = new List<string>();
            foreach (KeyValuePair<int, bool> tWS in tWSList)
            {
                if (tWS.Value == true)
                {
                    tWSListUsable.Add(tWS.Key);
                    tWSListUsableString.Add(NWDAppConfiguration.SharedInstance().WebFolder + "_" + tWS.Key.ToString("0000"));
                }
            }
            NWDAppConfiguration.SharedInstance().WebFolder = EditorGUILayout.TextField("WebService Folder", NWDAppConfiguration.SharedInstance().WebFolder);
            int tIndexWS = tWSListUsable.IndexOf(NWDAppConfiguration.SharedInstance().WebBuild);
            tIndexWS = EditorGUILayout.Popup("WebService active", tIndexWS, tWSListUsableString.ToArray());
            if (tIndexWS >= 0)
            {
                NWDAppConfiguration.SharedInstance().WebBuild = tWSListUsable[tIndexWS];
            }
            else
            {
                NWDAppConfiguration.SharedInstance().WebBuild = 0;
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical(GUILayout.MinWidth(tMinWidht));
            foreach (KeyValuePair<int, bool> tWS in tWSList)
            {
                EditorGUI.BeginDisabledGroup(tWS.Key == 0);
                NWDBasisHelper tDatasToTest = NWDBasisHelper.FindTypeInfos(typeof(NWDParameter));
                if (tDatasToTest.WebModelSQLOrder.ContainsKey(tWS.Key) == false)
                {
                    bool tV = EditorGUILayout.Toggle("(" + NWDAppConfiguration.SharedInstance().WebFolder + "_" + tWS.Key.ToString("0000") + " unused)", tWS.Value);
                    NWDAppConfiguration.SharedInstance().WSList[tWS.Key] = tV;
                }
                else
                {
                    bool tV = EditorGUILayout.Toggle(NWDAppConfiguration.SharedInstance().WebFolder + "_" + tWS.Key.ToString("0000") + " in config", tWS.Value);
                    NWDAppConfiguration.SharedInstance().WSList[tWS.Key] = tV;
                }
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.EndVertical();
            if (tColum > 1)
            {
                EditorGUILayout.EndHorizontal();
            }




            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("Databases", MessageType.None);
            NWDAppConfiguration.SharedInstance().RowDataIntegrity = EditorGUILayout.Toggle("Active Row Integrity", NWDAppConfiguration.SharedInstance().RowDataIntegrity);
            NWDAppConfiguration.SharedInstance().PreloadDatas = EditorGUILayout.Toggle("Preload Datas", NWDAppConfiguration.SharedInstance().PreloadDatas);

            string tDatabasePathEditor = NWDDataManager.SharedInstance().DatabasePathEditor + "/" + NWDDataManager.SharedInstance().DatabaseNameEditor;

            //string tDatabasePathAccount = NWDDataManager.SharedInstance().DatabasePathAccount + "/" + NWDDataManager.SharedInstance().DatabaseNameAccount;
            string tDatabasePathAccount = "/" + NWDDataManager.SharedInstance().DatabaseNameAccount;

            EditorGUILayout.LabelField("Databases Editor config for all environements", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Editor path ", tDatabasePathEditor);
            if (GUILayout.Button("Editor Database File"))
            {
                EditorUtility.RevealInFinder(tDatabasePathEditor);
            }
            EditorGUILayout.LabelField("EditorPass", NWDAppConfiguration.SharedInstance().EditorPass);
            EditorGUILayout.LabelField("EditorPassA", NWDAppConfiguration.SharedInstance().EditorPassA);
            EditorGUILayout.LabelField("EditorPassAB", NWDAppConfiguration.SharedInstance().EditorPassB);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("Editor Pass Result", NWDAppConfiguration.SharedInstance().GetEditorPass());
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.LabelField("Databases Accountconfig for all environements (by device)", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Account path ", tDatabasePathAccount);
            if (GUILayout.Button("Account Database File"))
            {
                EditorUtility.RevealInFinder(tDatabasePathAccount);
            }
            EditorGUILayout.LabelField("AccountHashSalt", NWDAppConfiguration.SharedInstance().AccountHashSalt);
            EditorGUILayout.LabelField("AccountHashSaltA", NWDAppConfiguration.SharedInstance().AccountHashSaltA);
            EditorGUILayout.LabelField("AccountHashSaltB", NWDAppConfiguration.SharedInstance().AccountHashSaltB);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("Account Pass Result", NWDAppConfiguration.SharedInstance().GetAccountPass());
            EditorGUI.EndDisabledGroup();

            GUILayout.EndScrollView ();
			GUILayout.Space (8.0f);
			if (GUILayout.Button (NWDConstants.K_APP_CONFIGURATION_SAVE_BUTTON)) 
            {
				NWDAppConfiguration.SharedInstance().GenerateCSharpFile (NWDAppConfiguration.SharedInstance().SelectedEnvironment ());
			}
			GUILayout.Space (8.0f);
		}
		//-------------------------------------------------------------------------------------------------------------
	}
	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif