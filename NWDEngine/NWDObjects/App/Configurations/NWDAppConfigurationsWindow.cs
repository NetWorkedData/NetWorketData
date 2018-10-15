﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// NWDAppWindow.
    /// </summary>
    [NWDTypeWindowParamAttribute("App Config",
        "Project Edition, You can add, change, remove the item of your game here. " +
        "Everythings can be item : money, gold, dress. The item can be win, buy in the pack, etc.",
        "NWDAppConfigurationsWindow",
        new Type[] {
            typeof(NWDVersion),
            typeof(NWDParameter),
            typeof(NWDPreferenceKey),
            typeof(NWDAssetBundle),
			/* Add NWDBasis here*/
		}
    )]
    public class NWDAppWindow : NWDBasisWindow<NWDAppWindow>
    {
        //-------------------------------------------------------------------------------------------------------------
        [MenuItem(NWDConstants.K_MENU_BASE + "App/Configurations", false, 200)]
        //-------------------------------------------------------------------------------------------------------------
        public static void MenuMethod()
        {
            EditorWindow tWindow = EditorWindow.GetWindow(typeof(NWDAppWindow));
            tWindow.Show();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
}
//=====================================================================================================================
#endif
