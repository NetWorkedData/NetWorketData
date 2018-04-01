﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
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
    //TODO: FINISH THIS CLASS NWDRectInt
    [SerializeField]
    //-------------------------------------------------------------------------------------------------------------
    public class NWDRectInt : BTBDataType
    {
        //-------------------------------------------------------------------------------------------------------------
        public NWDRectInt()
        {
            Value = 0 + NWDConstants.kFieldSeparatorA + 0+ NWDConstants.kFieldSeparatorA + 0+ NWDConstants.kFieldSeparatorA + 0;
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDRectInt(string sValue = "")
        {
            if (sValue == null)
            {
                Value = "";
            }
            else
            {
                Value = sValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void SetRectInt(RectInt sRectInt)
        {
            Value = sRectInt.x + NWDConstants.kFieldSeparatorA +
                    sRectInt.y + NWDConstants.kFieldSeparatorA +
                         sRectInt.height + NWDConstants.kFieldSeparatorA +
                         sRectInt.width;
        }
        //-------------------------------------------------------------------------------------------------------------
        public RectInt GetRectInt()
        {
            string[] tFloats = Value.Split(new string[] { NWDConstants.kFieldSeparatorA }, StringSplitOptions.RemoveEmptyEntries);
            int tX = 0;
            int tY = 0;
            int tHeight = 0;
            int tWidth = 0;
            if (tFloats.Count() == 4)
            {
                int.TryParse(tFloats[0], out tX);
                int.TryParse(tFloats[1], out tY);
                int.TryParse(tFloats[2], out tHeight);
                int.TryParse(tFloats[3], out tWidth);
            }
            RectInt rReturn = new RectInt(tX, tY, tHeight, tWidth);
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
#if UNITY_EDITOR
        //-------------------------------------------------------------------------------------------------------------
        public override float ControlFieldHeight()
        {
            GUIStyle tStyle = new GUIStyle(EditorStyles.textField);
            float tHeight = tStyle.CalcHeight(new GUIContent("A"), 100.0f);
            return tHeight*3;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override object ControlField(Rect sPos, string sEntitled, string sTooltips = "")
        {
            NWDRectInt tTemporary = new NWDRectInt();
            GUIContent tContent = new GUIContent(sEntitled, sTooltips);

            RectInt tRectInt = GetRectInt();
            RectInt tNexrect = EditorGUI.RectIntField(new Rect(sPos.x, sPos.y, sPos.width, NWDConstants.kLabelStyle.fixedHeight),
                                                      tContent,tRectInt);

            int tIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            EditorGUI.indentLevel = tIndentLevel;

            tTemporary.SetRectInt(tNexrect);
            return tTemporary;
        }
        //-------------------------------------------------------------------------------------------------------------
#endif
        //-------------------------------------------------------------------------------------------------------------
    }
}
//=====================================================================================================================