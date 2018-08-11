﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
//=====================================================================================================================
namespace NetWorkedData
{
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class NWDEditorConstants
    {
        //-------------------------------------------------------------------------------------------------------------
        static private GUISkin Skin;
        static public  GUIStyle ToolbarStyle;
        //-------------------------------------------------------------------------------------------------------------
        static bool StyleLoaded = false;
        //-------------------------------------------------------------------------------------------------------------
        public static void LoadStyles()
        {
            //if (StyleLoaded == false)
            {
                StyleLoaded = true;
                Skin = (GUISkin)Resources.Load("NWDCustomSkin");
                ToolbarStyle = Skin.GetStyle("NWDToolbar");
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif