﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

//=====================================================================================================================
namespace NetWorkedData
{
	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public partial class NWDAppEnvironment
	{
		//-------------------------------------------------------------------------------------------------------------
		#if UNITY_EDITOR
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Name for the menu.
		/// </summary>
		/// <returns>The name.</returns>
		public static string MenuName ()
		{
			return NWDConstants.K_APP_ENVIRONMENT_MENU_NAME;
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Draw the interface in editor.
		/// </summary>
		public void DrawInEditor (EditorWindow sEditorWindow, bool sAutoSelect=false)
		{
			// TODO use NWDConstants for these strings
			// TODO use GUI without layout

			EditorGUILayout.HelpBox ("Project configuration " + Environment + " for connection with server", MessageType.None);

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical(GUILayout.MinWidth(280.0F));

			EditorGUILayout.TextField ("AppName for server action "+ Environment, EditorStyles.boldLabel);
			AppName = EditorGUILayout.TextField ("AppName", AppName);
			EditorGUILayout.TextField ("Security of Datas"+ Environment, EditorStyles.boldLabel);
			DataSHAPassword = EditorGUILayout.TextField ("SHA Password", DataSHAPassword);
			DataSHAVector = EditorGUILayout.TextField ("SHA Vector", DataSHAVector);
			EditorGUILayout.TextField ("Hash of Datas"+ Environment, EditorStyles.boldLabel);
			SaltStart = EditorGUILayout.TextField ("Salt start", SaltStart);
            SaltEnd = EditorGUILayout.TextField ("Salt end", SaltEnd);
            SaltServer = EditorGUILayout.TextField("Salt server", SaltServer);
			SaltFrequency = EditorGUILayout.IntField ("Salt Frequency", SaltFrequency);
			EditorGUILayout.TextField ("Server Params for "+ Environment, EditorStyles.boldLabel);
            ServerHTTPS = EditorGUILayout.TextField ("Server (https://…)", ServerHTTPS);
            ServerHost = EditorGUILayout.TextField("MySQL Host", ServerHost);
            ServerUser = EditorGUILayout.TextField("MySQL user", ServerUser);
            ServerPassword = EditorGUILayout.TextField("MySQL password", ServerPassword);
            ServerBase = EditorGUILayout.TextField("MySQL base", ServerBase);


            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUILayout.MinWidth(280.0F));

			EditorGUILayout.TextField ("Social Params for "+ Environment, EditorStyles.boldLabel);
			FacebookAppID = EditorGUILayout.TextField ("FacebookAppID", FacebookAppID);
			FacebookAppSecret = EditorGUILayout.TextField ("FacebookAppSecret", FacebookAppSecret);
			GoogleAppKey = EditorGUILayout.TextField ("GoogleAppKey", GoogleAppKey);
			UnityAppKey = EditorGUILayout.TextField ("UnityAppKey", UnityAppKey);
			TwitterAppKey = EditorGUILayout.TextField ("TwitterAppKey", TwitterAppKey);
			EditorGUILayout.TextField ("Email to send forgotten code "+ Environment, EditorStyles.boldLabel);
			//RescueEmail = EditorGUILayout.TextField ("RescueEmail", RescueEmail);
			EditorGUILayout.TextField ("Admin Key for "+ Environment, EditorStyles.boldLabel);
			AdminKey = EditorGUILayout.TextField ("AdminKey", AdminKey);
			EditorGUILayout.TextField ("Token Historic limit for "+ Environment, EditorStyles.boldLabel);
            TokenHistoric = EditorGUILayout.IntSlider ("Token number", TokenHistoric, 1, 10);
            EditorGUILayout.TextField("Options for game in " + Environment, EditorStyles.boldLabel);
            SpeedOfGameTime = EditorGUILayout.FloatField("Speed Of GameTime", SpeedOfGameTime);
			EditorGUILayout.TextField ("Version for "+ Environment, EditorStyles.boldLabel);
			EditorGUILayout.LabelField ("version", NWDVersion.GetVersionForEnvironemt (this), EditorStyles.boldLabel);

			EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();




            EditorGUILayout.HelpBox("Webservice app config (all environements)", MessageType.None);

            EditorGUILayout.LabelField("Webservice app config (all environements)", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(GUILayout.MinWidth(280.0F));

            NWDAppConfiguration.SharedInstance().WebFolder = EditorGUILayout.TextField("WebService Folder", NWDAppConfiguration.SharedInstance().WebFolder);
            NWDAppConfiguration.SharedInstance().RowDataIntegrity = EditorGUILayout.Toggle("Active Row Integrity", NWDAppConfiguration.SharedInstance().RowDataIntegrity);

            EditorGUILayout.LabelField("WebService active", NWDAppConfiguration.SharedInstance().WebBuild.ToString());

            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical(GUILayout.MinWidth(280.0F));
            Dictionary<int, bool> tWSList = new Dictionary<int, bool>();
            tWSList.Add(0, true);

            foreach (KeyValuePair<int, bool> tWS in NWDAppConfiguration.SharedInstance().WSList)
            {
                if (tWSList.ContainsKey(tWS.Key) == false)
                {
                    tWSList.Add(tWS.Key, tWS.Value);
                }
            }
            foreach (KeyValuePair<int, bool> tWS in tWSList)
            {
                EditorGUI.BeginDisabledGroup(tWS.Key == 0);
                bool tV = EditorGUILayout.Toggle("WebService " + tWS.Key.ToString() + " in config", tWS.Value);
                NWDAppConfiguration.SharedInstance().WSList[tWS.Key] = tV;
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();





            EditorGUILayout.LabelField("Tag managment (all environements)", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(GUILayout.MinWidth(280.0F));

            NWDAppConfiguration.SharedInstance().TagList[-1] = "No Tag";

            Dictionary<int, string> tTagList = new Dictionary<int, string>(NWDAppConfiguration.SharedInstance().TagList);
            for (int tI = -1; tI <= NWDAppConfiguration.SharedInstance().TagNumber; tI++)
            {
                if (NWDAppConfiguration.SharedInstance().TagList.ContainsKey(tI) == false )
                {
                    NWDAppConfiguration.SharedInstance().TagList.Add(tI, "tag " + tI.ToString());
                }
                EditorGUI.BeginDisabledGroup(tI < 0);
                string tV = EditorGUILayout.TextField("tag " + tI.ToString(), NWDAppConfiguration.SharedInstance().TagList[tI]);
                tTagList[tI] = tV.Replace("\"", "`");
                EditorGUI.EndDisabledGroup();
            }
            NWDAppConfiguration.SharedInstance().TagList = tTagList;
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

			FormatVerification ();
		}
		//-------------------------------------------------------------------------------------------------------------
		#endif
		//-------------------------------------------------------------------------------------------------------------
	}
	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================