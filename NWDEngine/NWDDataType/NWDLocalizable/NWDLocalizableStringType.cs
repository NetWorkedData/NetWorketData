﻿//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:27:55
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;

using UnityEngine;

using SQLite4Unity3d;
using BasicToolBox;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

//=====================================================================================================================
namespace NetWorkedData
{
	[SerializeField]
	//-------------------------------------------------------------------------------------------------------------
	public class NWDLocalizableStringType : NWDLocalizableType
	{
		//-------------------------------------------------------------------------------------------------------------
		public NWDLocalizableStringType ()
		{
            //Debug.Log("NWDLocalizableStringType()");
            Value = string.Empty;
            AddBaseString(string.Empty);
            //Debug.Log("NWDLocalizableStringType() Value = " + Value);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDLocalizableStringType (string sValue = BTBConstants.K_EMPTY_STRING)
        {
           // Debug.Log("NWDLocalizableStringType(string sValue)");
            if (string.IsNullOrEmpty(sValue))
            {
                Value = string.Empty;
                AddBaseString(string.Empty);
			} else {
				Value = sValue;
			}
			kSplitDico = new Dictionary<string,string> ();
			DicoPopulate ();
		}
		//-------------------------------------------------------------------------------------------------------------
		//[NonSerializedAttribute]
		//private Dictionary<string,string> kSplitDico;
		////-------------------------------------------------------------------------------------------------------------
		//private void DicoPopulate()
		//{
		//	if (Value != null && Value != "") {
		//		string[] tValueArray = Value.Split (new string[]{ NWDConstants.kFieldSeparatorA }, StringSplitOptions.RemoveEmptyEntries);
		//		foreach (string tValueArrayLine in tValueArray) {
		//			string[] tLineValue = tValueArrayLine.Split (new string[]{ NWDConstants.kFieldSeparatorB }, StringSplitOptions.RemoveEmptyEntries);
		//			if (tLineValue.Length == 2) {
		//				string tLangague = tLineValue [0];
		//				string tText = tLineValue [1];
		//				if (kSplitDico.ContainsKey (tLangague) == false) {
		//					kSplitDico.Add (tLangague, tText);
		//				}
		//			} else if (tLineValue.Length == 1) {
		//				string tLangague = tLineValue [0];
		//				string tText = "";
		//				if (kSplitDico.ContainsKey (tLangague) == false) {
		//					kSplitDico.Add (tLangague, tText);
		//				}
		//			}
		//		}
		//	}
		//}
		////-------------------------------------------------------------------------------------------------------------
		//private string SplitDico (string sKey)
		//{
		//	string rReturn = "";
		//	if (kSplitDico == null) { 
		//		kSplitDico = new Dictionary<string,string> ();
		//		DicoPopulate ();
		//	}
		//	if (kSplitDico.ContainsKey (sKey)) {
		//		rReturn = kSplitDico [sKey];
		//	} else if (kSplitDico.ContainsKey (NWDDataLocalizationManager.kBaseDev)) {
		//		rReturn = kSplitDico [NWDDataLocalizationManager.kBaseDev];
		//	} else {
		//		rReturn = "no key value";
		//	}
		//	return rReturn;
		//}
		////-------------------------------------------------------------------------------------------------------------
		//public void AddBaseString (string sValue)
		//{
		//	AddValue (NWDDataLocalizationManager.kBaseDev, sValue);
		//}
		////-------------------------------------------------------------------------------------------------------------
		//public void AddLocalString (string sValue)
		//{
		//	AddValue (NWDDataManager.SharedInstance().PlayerLanguage, sValue);
		//}
		////-------------------------------------------------------------------------------------------------------------
		//public string GetLocalString ()
		//{
		//	return NWDToolbox.TextUnprotect( SplitDico (NWDDataManager.SharedInstance().PlayerLanguage));
		//}
		////-------------------------------------------------------------------------------------------------------------
		//public string GetBaseString ()
		//{
			//return NWDToolbox.TextUnprotect(SplitDico (NWDDataLocalizationManager.kBaseDev));
        //}
        ////-------------------------------------------------------------------------------------------------------------
        //public string GetLanguageString(string sLanguage)
        //{
        //    return NWDToolbox.TextUnprotect(SplitDico(sLanguage));
        //}
        //-------------------------------------------------------------------------------------------------------------
#if UNITY_EDITOR
        //-------------------------------------------------------------------------------------------------------------
		public override float ControlFieldHeight ()
		{
			int tRow = 0;
			if (Value != null && Value != string.Empty) {
				string[] tValueArray = Value.Split (new string[]{ NWDConstants.kFieldSeparatorA }, StringSplitOptions.RemoveEmptyEntries);
				tRow += tValueArray.Count ();
			}
            float rReturn = (NWDGUI.kTextFieldStyle.fixedHeight + NWDGUI.kFieldMarge) * tRow +NWDGUI.kPopupStyle.fixedHeight;
			return rReturn;
		}
		//-------------------------------------------------------------------------------------------------------------
        public override object ControlField (Rect sPosition, string sEntitled, bool sDisabled, string sTooltips = BTBConstants.K_EMPTY_STRING, object sAdditionnal = null)
        {
            NWDLocalizableStringType tTemporary = new NWDLocalizableStringType ();
            GUIContent tContent = new GUIContent(sEntitled, sTooltips);

			float tWidth = sPosition.width;
			float tHeight = sPosition.height;
			float tX = sPosition.position.x;
			float tY = sPosition.position.y;

            float tLangWidth = EditorGUIUtility.labelWidth + NWDGUI.kLangWidth;

			List<string> tLocalizationList = new List<string> ();
			tLocalizationList.Add (BTBConstants.K_MINUS);

			string tLanguage = NWDAppConfiguration.SharedInstance().DataLocalizationManager.LanguagesString;
			string[] tLanguageArray = tLanguage.Split (new string[]{ ";" }, StringSplitOptions.RemoveEmptyEntries);

			tLocalizationList.AddRange (tLanguageArray);
			tLocalizationList.Sort ();

			List<string> tValueList = new List<string> ();
			List<string> tValueNextList = new List<string> ();

			Dictionary<string,string> tResult = new Dictionary<string,string> ();

			if (Value != null && Value != string.Empty) {
				string[] tValueArray = Value.Split (new string[]{ NWDConstants.kFieldSeparatorA }, StringSplitOptions.RemoveEmptyEntries);
				tValueList = new List<string> (tValueArray);
			}

			for (int i = 0; i < tValueList.Count; i++) {
				string tLine = tValueList.ElementAt (i);
				string[] tLineValue = tLine.Split (new string[]{ NWDConstants.kFieldSeparatorB }, StringSplitOptions.RemoveEmptyEntries);
				if (tLineValue.Length > 0) {
					//Debug.Log ("i remove " + tLineValue [0]);
					tLocalizationList.Remove (tLineValue [0]);
				}
			}
			string[] tLangageArray = tLocalizationList.ToArray ();
			//Debug.Log (" tLangageArray =  " + string.Join(".",tLangageArray));
			tValueList.Add (string.Empty);
			for (int i = 0; i < tValueList.Count; i++) {
				//string tFieldName = sEntitled;
				if (i > 0) {
                    //tFieldName = "   ";
                    tContent = new GUIContent("   ");
				}
				string tLangague = string.Empty;
				string tText = string.Empty;
				string tLine = tValueList.ElementAt (i);
				string[] tLineValue = tLine.Split (new string[]{ NWDConstants.kFieldSeparatorB }, StringSplitOptions.RemoveEmptyEntries);
				if (tLineValue.Length == 2) {
					tLangague = tLineValue [0];
					tText = tLineValue [1];
				}
				if (tLineValue.Length == 1) {
					tLangague = tLineValue [0];
				}
				List<string> tValueFuturList = new List<string> ();
				tValueFuturList.AddRange (tLangageArray);
				tValueFuturList.Add (tLangague);
				tValueFuturList.Sort ();
                string[] tLangageFuturArray = tValueFuturList.ToArray ();
                List<GUIContent> tContentFuturList = new List<GUIContent>();
                foreach (string tS in tLangageFuturArray)
                {
                    tContentFuturList.Add(new GUIContent(tS));
                }
				//Debug.Log (" tLangageFuturArray =  " + string.Join(":",tLangageFuturArray));

				int tIndex = tValueFuturList.IndexOf (tLangague);
                //tIndex = EditorGUI.Popup (new Rect (tX, tY, tLangWidth, tPopupdStyle.fixedHeight), tFieldName, tIndex, tLangageFuturArray, tPopupdStyle);
                tIndex = EditorGUI.Popup(new Rect(tX, tY, tLangWidth, NWDGUI.kPopupStyle.fixedHeight), tContent, tIndex, tContentFuturList.ToArray(), NWDGUI.kPopupStyle);
				if (tIndex < 0 || tIndex >= tValueFuturList.Count) {
					tIndex = 0;
				}
				tLangague = tValueFuturList [tIndex];
//				Debug.LogVerbose ("tIndex = " +tIndex.ToString ());
				if (tLangague != string.Empty) 
                {
                    //remove EditorGUI.indentLevel to draw next controller without indent 
                    int tIndentLevel = EditorGUI.indentLevel;
                    EditorGUI.indentLevel = 0;
                    tText = EditorGUI.TextField (new Rect (tX + tLangWidth + NWDGUI.kFieldMarge, tY, tWidth - tLangWidth - NWDGUI.kFieldMarge, NWDGUI.kTextFieldStyle.fixedHeight), NWDToolbox.TextUnprotect (tText));
                    EditorGUI.indentLevel = tIndentLevel;
                }
				tText = NWDToolbox.TextProtect (tText);
                tY += NWDGUI.kTextFieldStyle.fixedHeight + NWDGUI.kFieldMarge;
				if (tResult.ContainsKey (tLangague)) {
					tResult [tLangague] = tText;
				} else {
					tResult.Add (tLangague, tText);
				}
			}
			tResult.Remove (BTBConstants.K_MINUS); // remove default value
			tResult.Remove (string.Empty); // remove empty value
            if (tResult.ContainsKey(NWDDataLocalizationManager.kBaseDev)==false)
            {
                tResult.Add(NWDDataLocalizationManager.kBaseDev, string.Empty);
            }
			foreach (KeyValuePair<string,string> tKeyValue in tResult) {
				tValueNextList.Add (tKeyValue.Key + NWDConstants.kFieldSeparatorB + tKeyValue.Value);
			}

			string[] tNextValueArray = tValueNextList.Distinct ().ToArray ();
			string tNextValue = string.Join (NWDConstants.kFieldSeparatorA, tNextValueArray);
			tNextValue = tNextValue.Trim (NWDConstants.kFieldSeparatorA.ToCharArray () [0]);
			if (tNextValue == NWDConstants.kFieldSeparatorB) {
				tNextValue = string.Empty;
			}
			tTemporary.Value = tNextValue;
			return tTemporary;
		}
		//-------------------------------------------------------------------------------------------------------------
		#endif
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================