﻿// =====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:49:32
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
// =====================================================================================================================

//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using System;

using UnityEngine;

using SQLite4Unity3d;

#if UNITY_EDITOR
using UnityEditor;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [NWDTypeWindowParamAttribute("Shop",
        "Shop window description",
        "NWDShopWindow",
        new Type[] {
            typeof(NWDShop),
            typeof(NWDRack),
            typeof(NWDPack),
            typeof(NWDItemPack),
            typeof(NWDItem),
            //typeof(NWDInAppPack),
            typeof(NWDUserTransaction),
			/* Add NWDBasis here*/
		}
                                 )]
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class NWDShopWindow : NWDBasisWindow<NWDShopWindow>
    {
        //-------------------------------------------------------------------------------------------------------------
        [MenuItem(NWDConstants.K_MENU_BASE + "Marketplaces/Shop" + NWDConstants.K_MENU_BASIS_WINDOWS_MANAGEMENT, false, 532)]
        //-------------------------------------------------------------------------------------------------------------
        public static void MenuMethod()
        {
            EditorWindow tWindow = EditorWindow.GetWindow(typeof(NWDShopWindow));
            tWindow.Show();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif