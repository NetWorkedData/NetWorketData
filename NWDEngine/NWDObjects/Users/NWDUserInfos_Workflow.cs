﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using UnityEngine;

using BasicToolBox;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_IOS
using NotificationServices = UnityEngine.iOS.NotificationServices;
using NotificationType = UnityEngine.iOS.NotificationType;
#endif

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDUserInfos : NWDBasis<NWDUserInfos>
    {
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserInfos()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserInfos(bool sInsertInNetWorkedData) : base(sInsertInNetWorkedData)
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void Initialization()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        private static NWDUserInfos kCurrent = null;
        //-------------------------------------------------------------------------------------------------------------
        public static NWDUserInfos GetUserInfosOrCreate()
        {
            if (kCurrent != null)
            {
                if (kCurrent.Account.GetReference() != NWDAccount.GetCurrentAccountReference())
                {
                    kCurrent = null;
                }
            }
            
            if (kCurrent == null)
            {
                NWDUserInfos tUserInfos = GetFirstData(NWDAccount.GetCurrentAccountReference());
                if (tUserInfos == null)
                {
                    tUserInfos = NewData();
                    #if UNITY_EDITOR
                    tUserInfos.InternalKey = NWDAccount.GetCurrentAccountReference();
                    #endif
                    tUserInfos.Account.SetReference(NWDAccount.GetCurrentAccountReference());
                    tUserInfos.Tag = NWDBasisTag.TagUserCreated;
                    tUserInfos.SaveData();
                }
                kCurrent = tUserInfos;
            }
            
            return kCurrent;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void SynchronizeDatas()
        {
            SynchronizationFromWebService();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void StartOnDevice()
        {
#if UNITY_ANDROID
            OSLastSignIn = NWDOperatingSystem.Android;
            // TODO register notification token

#elif UNITY_IOS
            OSLastSignIn = NWDOperatingSystem.IOS;
            // TODO register notification token

            NotificationServices.RegisterForNotifications( NotificationType.Alert | NotificationType.Badge | NotificationType.Sound);

            byte[] tToken = NotificationServices.deviceToken;
            if (tToken != null)
            {
                AppleNotificationToken = "%" + System.BitConverter.ToString(tToken).Replace('-', '%');
            }
                
#elif UNITY_STANDALONE_OSX  
            OSLastSignIn = NWDOperatingSystem.OSX;
            // TODO register notification token

#elif UNITY_STANDALONE_WIN
            OSLastSignIn = NWDOperatingSystem.WIN;

#elif UNITY_WP8
            OSLastSignIn = NWDOperatingSystem.WIN;

#elif UNITY_WINRT
            OSLastSignIn = NWDOperatingSystem.WINRT;

#endif

            if (UpdateDataIfModified())
            {
                // TODO send to server immediatly
                NWDDataManager.SharedInstance().AddWebRequestSynchronization(new List<Type>(){typeof(NWDUserInfos)}, true);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================