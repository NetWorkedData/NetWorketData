﻿using System;
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
	public class NWDDateScheduleType : NWDScheduleType
	{
		//-------------------------------------------------------------------------------------------------------------
		public NWDDateScheduleType ()
		{
			Value = "";
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDDateScheduleType (string sValue = "")
		{
			if (sValue == null) {
				Value = "";
			} else {
				Value = sValue;
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		public override bool ResultForDate (DateTime sDateTime)
		{
			bool rReturn = false;
			DayOfWeek tDayOfWeek = sDateTime.DayOfWeek;
			switch (tDayOfWeek) {
			case DayOfWeek.Monday: 
				rReturn = !Value.Contains (kDaysOfWeekSchedulePrefix + "0");
				break;
			case DayOfWeek.Tuesday: 
				rReturn = !Value.Contains (kDaysOfWeekSchedulePrefix + "1");
				break;
			case DayOfWeek.Wednesday: 
				rReturn = !Value.Contains (kDaysOfWeekSchedulePrefix + "2");
				break;
			case DayOfWeek.Thursday: 
				rReturn = !Value.Contains (kDaysOfWeekSchedulePrefix + "3");
				break;
			case DayOfWeek.Friday: 
				rReturn = !Value.Contains (kDaysOfWeekSchedulePrefix + "4");
				break;
			case DayOfWeek.Saturday: 
				rReturn = !Value.Contains (kDaysOfWeekSchedulePrefix + "5");
				break;
			case DayOfWeek.Sunday: 
				rReturn = !Value.Contains (kDaysOfWeekSchedulePrefix + "6");
				break;
			}
			if (rReturn == true) {
				int tMonth = sDateTime.Month-1;
				rReturn = !Value.Contains (kMonthsSchedulePrefix + tMonth.ToString("00"));
			}

			if (rReturn == true) {
				int tDays = sDateTime.Day-1;
				rReturn = !Value.Contains (kDaysSchedulePrefix + tDays.ToString("00"));
			}

			if (rReturn == true) {
				int tHour = sDateTime.Hour;
				rReturn = !Value.Contains (kHoursSchedulePrefix + tHour.ToString("00"));
			}

			if (rReturn == true) {
				int tMinute = sDateTime.Minute;
				rReturn = !Value.Contains (kMinutesSchedulePrefix + tMinute.ToString("00"));
			}

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
			int tCount = NWDDateType.kDayNames.Length;
			int tCountB = NWDDateType.kMonths.Length;
			int tCountC = NWDDateType.kHours.Length/3;
			int tCountD = 11;
			int tCountE = NWDDateType.kMinutes.Length/3;
			return tHeight * (tCount + tCountB + tCountC + tCountD+ tCountE) + tHeightTitle*5;
		}
		//-------------------------------------------------------------------------------------------------------------
		public override object ControlField (Rect sPos, string sEntitled)
		{
			NWDDateScheduleType tTemporary = new NWDDateScheduleType ();

			GUI.Label (new Rect (sPos.x, sPos.y, sPos.width, sPos.height), sEntitled);
			GUIStyle tToggleStyle = new GUIStyle (EditorStyles.toggle);
			float tHeight = tToggleStyle.CalcHeight (new GUIContent ("A"), 100.0f);
			GUIStyle tLabelStyle = new GUIStyle (EditorStyles.boldLabel);
			float tHeightTitle = tLabelStyle.CalcHeight (new GUIContent ("A"), 100.0f);

			float tTiersWidth = Mathf.Ceil( (sPos.width - EditorGUIUtility.labelWidth) / 3.0F);
			float tHeightAdd = 0;

			GUI.Label (new Rect (sPos.x+EditorGUIUtility.labelWidth, sPos.y, sPos.width, sPos.height), "Month", tLabelStyle);
			tHeightAdd += tHeightTitle;
			for (int i=0; i<NWDDateType.kMonths.Length;i++)
			{
				bool tValueTest = GUI.Toggle (new Rect (sPos.x+EditorGUIUtility.labelWidth, sPos.y + tHeightAdd + tHeight * i,sPos.width, sPos.height),
					!Value.Contains (kMonthsSchedulePrefix+i.ToString("00")),
					NWDDateType.kMonths[i]);
				if (tValueTest==false)
				{
					tTemporary.Value += kMonthsSchedulePrefix+i.ToString("00");
				}
			}
			tHeightAdd += tHeight * 12;


			GUI.Label (new Rect (sPos.x+EditorGUIUtility.labelWidth, sPos.y+tHeightAdd, sPos.width, sPos.height), "Dates of day", tLabelStyle);
			tHeightAdd += tHeightTitle;
			for (int i=0; i<NWDDateType.kDays.Length;i++)
			{
				int c = i % 11;
				int l = (i - c) / 11;
				bool tValueTest = GUI.Toggle (new Rect (sPos.x+EditorGUIUtility.labelWidth + l*tTiersWidth, sPos.y +tHeightAdd+ tHeight * c, tTiersWidth, sPos.height),
					!Value.Contains (kDaysSchedulePrefix+i.ToString("00")),
					NWDDateType.kDays[i]);
				if (tValueTest==false)
				{
					tTemporary.Value += kDaysSchedulePrefix+i.ToString("00");
				}
			}
			tHeightAdd += tHeight * 11;


			GUI.Label (new Rect (sPos.x+EditorGUIUtility.labelWidth, sPos.y+tHeightAdd, sPos.width, sPos.height), "Days of week", tLabelStyle);
			tHeightAdd += tHeightTitle;
			for (int i=0; i<NWDDateType.kDayNames.Length;i++)
			{
				bool tValueTest = GUI.Toggle (new Rect (sPos.x+EditorGUIUtility.labelWidth, sPos.y + tHeightAdd+ tHeight * i, sPos.width, sPos.height),
					!Value.Contains (kDaysOfWeekSchedulePrefix+i.ToString()),
					NWDDateType.kDayNames[i]);
				if (tValueTest==false)
				{
					tTemporary.Value += kDaysOfWeekSchedulePrefix+i.ToString();
				}
			}
			tHeightAdd += tHeight * 7;

			GUI.Label (new Rect (sPos.x+EditorGUIUtility.labelWidth, sPos.y+tHeightAdd, sPos.width, sPos.height), "Hours", tLabelStyle);
			tHeightAdd += tHeightTitle;
			for (int i=0; i<NWDDateType.kHours.Length;i++)
			{
				int c = i % 8;
				int l = (i - c) / 8;
				bool tValueTest = GUI.Toggle (new Rect (sPos.x+EditorGUIUtility.labelWidth + l*tTiersWidth, sPos.y +tHeightAdd+ tHeight * c, tTiersWidth, sPos.height),
					!Value.Contains (kHoursSchedulePrefix+i.ToString("00")),
					NWDDateType.kHours[i]+kHoursUnit);
				if (tValueTest==false)
				{
					tTemporary.Value += kHoursSchedulePrefix+i.ToString("00");
				}
			}
			tHeightAdd += tHeight * 8;

			GUI.Label (new Rect (sPos.x+EditorGUIUtility.labelWidth, sPos.y+tHeightAdd, sPos.width, sPos.height), "Minutes", tLabelStyle);
			tHeightAdd += tHeightTitle;
			for (int i=0; i<NWDDateType.kMinutes.Length;i++)
			{
				int c = i % 20;
				int l = (i - c) / 20;
				bool tValueTest = GUI.Toggle (new Rect (sPos.x+EditorGUIUtility.labelWidth + l*tTiersWidth, sPos.y +tHeightAdd+ tHeight * c, tTiersWidth, sPos.height),
					!Value.Contains (kMinutesSchedulePrefix+i.ToString("00")),
					NWDDateType.kMinutes[i]+kMinutesUnit);
				if (tValueTest==false)
				{
					tTemporary.Value += kMinutesSchedulePrefix+i.ToString("00");
				}
			}
			tHeightAdd += tHeight * 20;

			if (ResultNow () == false) {
				GUI.Label (new Rect (sPos.x, sPos.y + tHeight, sPos.width, sPos.height), kNowFailed);
			} else {
				GUI.Label (new Rect (sPos.x, sPos.y + tHeight, sPos.width, sPos.height), kNowSuccess);
			}
			//GUI.Label (new Rect (sPos.x, sPos.y + tHeight *2, sPos.width, sPos.height), Value);
			return tTemporary;
		}
		//-------------------------------------------------------------------------------------------------------------
		public override bool ChangeAssetPath (string sOldPath, string sNewPath)
		{
			return false;
		}
		//-------------------------------------------------------------------------------------------------------------
		#endif
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================