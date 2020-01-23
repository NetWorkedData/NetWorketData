﻿//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:33:56
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================

using System;

//=====================================================================================================================
//
// ideMobi copyright 2019
// All rights reserved by ideMobi
//
// Read License-en or Licence-fr
//
//=====================================================================================================================
using UnityEngine;
//using BasicToolBox;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDMessage : NWDBasis
    {
        //-------------------------------------------------------------------------------------------------------------
        public NWDMessage()
        {
            //Debug.Log("NWDMessage Constructor");
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDMessage(bool sInsertInNetWorkedData) : base(sInsertInNetWorkedData)
        {
            //Debug.Log("NWDMessage Constructor with sInsertInNetWorkedData : " + sInsertInNetWorkedData.ToString()+"");
        }
        //-------------------------------------------------------------------------------------------------------------
        public void PostNotification(NWDUserNotificationDelegate sValidationbBlock = null, NWDUserNotificationDelegate sCancelBlock = null)
        {
            NWDUserNotification tNotification = new NWDUserNotification(this, sValidationbBlock, sCancelBlock);
            tNotification.Post();
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserInterMessage PostCustomNotification(
                                          NWDReferencesListType<NWDCharacter> sReplaceCharacters = null,
                                          NWDReferencesQuantityType<NWDItem> sReplaceItems = null,
                                          NWDReferencesQuantityType<NWDItemPack> sReplaceItemPack = null,
                                          NWDReferencesQuantityType<NWDPack> sReplacePacks = null,
                                          NWDUserNotificationDelegate sValidationbBlock = null,
                                          NWDUserNotificationDelegate sCancelBlock = null)
        {
            NWDUserInterMessage rUserIntermessage = NWDUserInterMessage.CreateNewMessageWith(this,
                    string.Empty,
                    0,
                    sReplaceCharacters,
                    sReplaceItems,
                    sReplaceItemPack,
                    sReplacePacks);
            NWDUserNotification tNotification = new NWDUserNotification(rUserIntermessage, sValidationbBlock, sCancelBlock);
            tNotification.Post();
            return rUserIntermessage;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================