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
    //TODO: FINISH THIS CLASS NWDVector2
    [SerializeField]
    //-------------------------------------------------------------------------------------------------------------
    public class NWDVector2 : BTBDataType
    {
        //-------------------------------------------------------------------------------------------------------------
        public NWDVector2()
        {
            Value = 0.0F + NWDConstants.kFieldSeparatorA + 0.0F;
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDVector2(string sValue = "")
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
        public void SetVector(Vector2 sVector)
        {
            Value = sVector.x + NWDConstants.kFieldSeparatorA +
                    sVector.y;
        }
        //-------------------------------------------------------------------------------------------------------------
        public Vector2 GetVector()
        {
            string[] tFloats = Value.Split(new string[] { NWDConstants.kFieldSeparatorA }, StringSplitOptions.RemoveEmptyEntries);
            float tX = 0.0F;
            float tY = 0.0F;
            if (tFloats.Count() == 2)
            {
                float.TryParse(tFloats[0], out tX);
                float.TryParse(tFloats[1], out tY);
            }
            Vector2 rReturn = new Vector2(tX, tY);
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
#if UNITY_EDITOR
        //-------------------------------------------------------------------------------------------------------------
        public override float ControlFieldHeight()
        {
            GUIStyle tStyle = new GUIStyle(EditorStyles.textField);
            float tHeight = tStyle.CalcHeight(new GUIContent("A"), 100.0f);
            return tHeight*2;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override object ControlField(Rect sPos, string sEntitled, string sTooltips = "")
        {
            NWDVector2 tTemporary = new NWDVector2();
            GUIContent tContent = new GUIContent(sEntitled, sTooltips);

            Vector2 tVector = GetVector();
            Vector2 tNexVector = EditorGUI.Vector2Field(new Rect(sPos.x, sPos.y, sPos.width, NWDConstants.kLabelStyle.fixedHeight),
                                   tContent,tVector);

            int tIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            EditorGUI.indentLevel = tIndentLevel;

            tTemporary.SetVector(tNexVector);
            return tTemporary;
        }
        //-------------------------------------------------------------------------------------------------------------
#endif
        //-------------------------------------------------------------------------------------------------------------
    }
}
//=====================================================================================================================