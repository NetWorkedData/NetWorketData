﻿//=====================================================================================================================
//
// ideMobi copyright 2019
// All rights reserved by ideMobi
//
// Read License-en or Licence-fr
//
//=====================================================================================================================
#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class NWDBasisClassInspector : ScriptableObject
    {
        //-------------------------------------------------------------------------------------------------------------
        public Type mTypeInEdition;
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [CustomEditor(typeof(NWDBasisClassInspector))]
    public class NWDBasisClassEditor : Editor
    {
        //-------------------------------------------------------------------------------------------------------------
        public override void OnInspectorGUI()
        {
            NWDBasisClassInspector tTarget = (NWDBasisClassInspector)target;
            if (tTarget.mTypeInEdition != null)
            {
                NWDBasisHelper.FindTypeInfos(tTarget.mTypeInEdition).New_DrawTypeInInspector();
                //NWDAliasMethod.InvokeClassMethod(tTarget.mTypeInEdition, NWDConstants.M_DrawTypeInInspector);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif