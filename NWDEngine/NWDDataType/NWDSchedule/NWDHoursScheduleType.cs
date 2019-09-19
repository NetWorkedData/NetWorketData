﻿//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:28:38
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
	/// <summary>
	/// NWD day schedule type. Can determine wich days of week must return a true result or false result
	/// </summary>
	[SerializeField]
	//-------------------------------------------------------------------------------------------------------------
	public class NWDHoursScheduleType : NWDScheduleType
	{
		//-------------------------------------------------------------------------------------------------------------
		public NWDHoursScheduleType ()
		{
			Value = string.Empty;
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDHoursScheduleType (string sValue = BTBConstants.K_EMPTY_STRING)
		{
			if (sValue == null) {
				Value = string.Empty;
			} else {
				Value = sValue;
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		public override bool ResultForDate (DateTime sDateTime)
		{
			bool rReturn = true;
			int tHour = sDateTime.Hour;
			rReturn = !Value.Contains (kHoursSchedulePrefix + tHour.ToString("00"));
			return rReturn;
		}
		//-------------------------------------------------------------------------------------------------------------
		#if UNITY_EDITOR
		//-------------------------------------------------------------------------------------------------------------
		public override float ControlFieldHeight ()
		{
			GUIStyle tToggleStyle = new GUIStyle (EditorStyles.toggle);
			float tHeight = tToggleStyle.CalcHeight (new GUIContent ("A"), 100.0f);
			GUIStyle tLabelStyle = new GUIStyle (EditorStyles.boldLabel);
			float tHeightTitle = tLabelStyle.CalcHeight (new GUIContent ("A"), 100.0f);
			return tHeight * 8 + tHeightTitle;
		}
		//-------------------------------------------------------------------------------------------------------------
        public override object ControlField (Rect sPos, string sEntitled, bool sDisabled, string sTooltips = BTBConstants.K_EMPTY_STRING, object sAdditionnal = null)
		{
            NWDHoursScheduleType tTemporary = new NWDHoursScheduleType ();
            GUIContent tContent = new GUIContent(sEntitled, sTooltips);

            EditorGUI.LabelField (new Rect (sPos.x, sPos.y, sPos.width, sPos.height), tContent);
			GUIStyle tToggleStyle = new GUIStyle (EditorStyles.toggle);
			float tHeight = tToggleStyle.CalcHeight (new GUIContent ("A"), 100.0f);
			GUIStyle tLabelStyle = new GUIStyle (EditorStyles.boldLabel);
			float tHeightTitle = tLabelStyle.CalcHeight (new GUIContent ("A"), 100.0f);

			float tTiersWidth = Mathf.Ceil( (sPos.width - EditorGUIUtility.labelWidth) / 3.0F);



			float tHeightAdd = 0;

			GUI.Label (new Rect (sPos.x+EditorGUIUtility.labelWidth, sPos.y, sPos.width, sPos.height), "Hours selection", tLabelStyle);
			tHeightAdd += tHeightTitle;


			for (int i=0; i<NWDDateTimeType.kHours.Length;i++)
			{
				int c = i % 8;
				int l = (i - c) / 8;
				bool tValueTest = GUI.Toggle (new Rect (sPos.x+EditorGUIUtility.labelWidth + l*tTiersWidth, sPos.y + tHeightAdd+ tHeight * c, tTiersWidth, sPos.height),
					!Value.Contains (kHoursSchedulePrefix+i.ToString("00")),
					NWDDateTimeType.kHours[i]+"H");
				if (tValueTest==false)
				{
					tTemporary.Value += kHoursSchedulePrefix+i.ToString("00");
				}
			}


            if (base.AvailableNow() == false)
            {
                EditorGUI.LabelField(new Rect(sPos.x + 15, sPos.y + tHeight, sPos.width, sPos.height), kNowFailed);
            }
            else
            {
                EditorGUI.LabelField(new Rect(sPos.x + 15, sPos.y + tHeight, sPos.width, sPos.height), kNowSuccess);
            }

            DateTime tDateTimeInGame = NWDAppEnvironment.SelectedEnvironment().DateTimeInGameTime();
            EditorGUI.LabelField(new Rect(sPos.x + 15, sPos.y + tHeight * 3, sPos.width, sPos.height), kNowGameTime + " (" + NWDAppEnvironment.SelectedEnvironment().SpeedOfGameTime + "x)");
            EditorGUI.LabelField(new Rect(sPos.x + 15, sPos.y + tHeight * 4, sPos.width, sPos.height), tDateTimeInGame.ToString("yyyy-MMM-dd"));
            EditorGUI.LabelField(new Rect(sPos.x + 15, sPos.y + tHeight * 5, sPos.width, sPos.height), tDateTimeInGame.ToString("ddd HH:mm:ss"));
            if (base.AvailableNowInGameTime() == false)
            {
                EditorGUI.LabelField(new Rect(sPos.x + 15, sPos.y + tHeight * 6, sPos.width, sPos.height), kNowGameFailed);
            }
            else
            {
                EditorGUI.LabelField(new Rect(sPos.x + 15, sPos.y + tHeight * 6, sPos.width, sPos.height), kNowGameSuccess);
            }

            //EditorGUI.TextField(new Rect(sPos.x, sPos.y + tHeight * 9, sPos.width, sPos.height), tTemporary.Value);
            //EditorGUI.TextField(new Rect(sPos.x, sPos.y + tHeight * 10, sPos.width, sPos.height), StringResultOfDate(tDateTimeInGame));
			return tTemporary;
		}
		//-------------------------------------------------------------------------------------------------------------
		#endif
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================