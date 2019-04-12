// =====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:50:28
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicToolBox;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDPreferenceKey : NWDBasis<NWDPreferenceKey>
    {
        //-------------------------------------------------------------------------------------------------------------
        public const string K_PREFERENCE_CHANGED_KEY = "K_PREFERENCE_CHANGED_KEY_8zQr95er"; // OK Needed by test & verify
        //-------------------------------------------------------------------------------------------------------------
        public NWDPreferenceKey()
        {   
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDPreferenceKey(bool sInsertInNetWorkedData) : base(sInsertInNetWorkedData)
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void Initialization() // INIT YOUR INSTANCE WITH THIS METHOD
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public void AddEnter(NWDMultiType sValue)
        {
            switch (Domain)
            {
                case NWDPreferencesDomain.AccountPreference:
                    {
                        NWDAccountPreference.FindDataByPreferenceKey(this).AddEnter(sValue);
                    }
                    break;
                case NWDPreferencesDomain.UserPreference:
                    {
                        NWDUserPreference.FindDataByPreferenceKey(this).AddEnter(sValue);
                    }
                    break;
                case NWDPreferencesDomain.LocalPreference:
                    {
                        PlayerPrefs.SetString(Reference, sValue.Value);
                    }
                    break;
            }
            if (NotifyChange == true)
            {
                BTBNotificationManager.SharedInstance().PostNotification(this, K_PREFERENCE_CHANGED_KEY);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDMultiType GetEnter()
        {
            NWDMultiType rReturn = new NWDMultiType();
            rReturn.Value = Default.Value;
            switch (Domain)
            {
                case NWDPreferencesDomain.AccountPreference:
                    {
                        rReturn = NWDAccountPreference.FindDataByPreferenceKey(this).GetEnter();
                    }
                    break;
                case NWDPreferencesDomain.UserPreference:
                    {
                        rReturn =  NWDUserPreference.FindDataByPreferenceKey(this).GetEnter();
                    }
                    break;
                case NWDPreferencesDomain.LocalPreference:
                    {
                        rReturn.Value = PlayerPrefs.GetString(Reference, Default.Value);
                    }
                    break;
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================