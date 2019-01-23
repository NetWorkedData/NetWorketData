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
    //TODO: FINISH THIS CLASS NWDVector3
    [SerializeField]
    //-------------------------------------------------------------------------------------------------------------
    public class NWDVector3 : BTBDataType
    {
        //-------------------------------------------------------------------------------------------------------------
        public NWDVector3()
        {
            Value = 0.0F+ NWDConstants.kFieldSeparatorA + 0.0F+NWDConstants.kFieldSeparatorA + 0.0F;
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDVector3(string sValue = BTBConstants.K_EMPTY_STRING)
        {
            if (sValue == null)
            {
                Value = string.Empty;
            }
            else
            {
                Value = sValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void Default()
        {
            Value = string.Empty;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void SetVector(Vector3 sVector)
        {
            Value = sVector.x.ToString(NWDConstants.FloatFormat, NWDConstants.FormatCountry) + NWDConstants.kFieldSeparatorA +
                    sVector.y.ToString(NWDConstants.FloatFormat, NWDConstants.FormatCountry) + NWDConstants.kFieldSeparatorA +
                    sVector.z.ToString(NWDConstants.FloatFormat, NWDConstants.FormatCountry);
        }
        //-------------------------------------------------------------------------------------------------------------
        public Vector3 GetVector()
        {
            string[] tFloats = Value.Split(new string[] { NWDConstants.kFieldSeparatorA }, StringSplitOptions.RemoveEmptyEntries);
            float tX = 0.0F;
            float tY = 0.0F;
            float tZ = 0.0F;
            if (tFloats.Count() == 3)
            {
                float.TryParse(tFloats[0], System.Globalization.NumberStyles.Float, NWDConstants.FormatCountry, out tX);
                float.TryParse(tFloats[1], System.Globalization.NumberStyles.Float, NWDConstants.FormatCountry, out tY);
                float.TryParse(tFloats[2], System.Globalization.NumberStyles.Float, NWDConstants.FormatCountry, out tZ);
            }
            Vector3 rReturn = new Vector3(tX, tY, tZ);
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
#if UNITY_EDITOR
        //-------------------------------------------------------------------------------------------------------------
        public override float ControlFieldHeight()
        {
            GUIStyle tStyle = new GUIStyle(EditorStyles.textField);
            float tHeight = tStyle.CalcHeight(new GUIContent(BTBConstants.K_A), 100.0f);
            return tHeight*2;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override object ControlField(Rect sPos, string sEntitled, string sTooltips = BTBConstants.K_EMPTY_STRING)
        {
            NWDVector3 tTemporary = new NWDVector3();
            GUIContent tContent = new GUIContent(sEntitled, sTooltips);

            Vector3 tVector = GetVector();
            Vector3 tNexVector = EditorGUI.Vector3Field(new Rect(sPos.x, sPos.y, sPos.width, NWDConstants.kLabelStyle.fixedHeight),
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