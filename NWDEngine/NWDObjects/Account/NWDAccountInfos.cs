﻿//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:29:20
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================

using System;
using System.Collections.Generic;

#if UNITY_IOS
using NotificationServices = UnityEngine.iOS.NotificationServices;
using NotificationType = UnityEngine.iOS.NotificationType;
#endif

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [NWDClassServerSynchronizeAttribute(true)]
    [NWDClassTrigrammeAttribute("AIF")]
    [NWDClassDescriptionAttribute("General Account Informations")]
    [NWDClassMenuNameAttribute("Account Infos")]
    public partial class NWDAccountInfos : NWDBasis
    {
        //-------------------------------------------------------------------------------------------------------------

        [NWDInspectorGroupStart("Player Informations")]
        public NWDReferenceType<NWDAccount> Account
        {
            get; set;
        }
        //public NWDAppEnvironmentPlayerStatut AccountType
        //{
        //    get; set;
        //}
        public NWDAppEnvironmentPlayerStatut AccountType()
        {
            NWDAppEnvironmentPlayerStatut rReturn = NWDAppEnvironmentPlayerStatut.Temporary;
            if (Account.GetReference().Contains("T"))
            {
                rReturn = NWDAppEnvironmentPlayerStatut.Temporary;
            }
            else
            {
                rReturn = NWDAppEnvironmentPlayerStatut.Certified;
                NWDAccountSign[] tSigns = NWDBasisHelper.GetCorporateDatas<NWDAccountSign>(Account.GetReference());
                foreach(NWDAccountSign tSign in tSigns)
                {
                    if (tSign.SignType != NWDAccountSignType.None && tSign.SignType != NWDAccountSignType.DeviceID)
                    {
                        if (tSign.SignStatus == NWDAccountSignAction.Associated)
                        {
                            rReturn = NWDAppEnvironmentPlayerStatut.Signed;
                            break;
                        }
                    }
                }
            }
            return rReturn;
        }
        public NWDReferenceType<NWDAccountAvatar> Avatar
        {
            get; set;
        }
        public NWDReferenceType<NWDAccountNickname> Nickname
        {
            get; set;
        }
        public NWDReferenceFreeType<NWDGameSave> CurrentGameSave
        {
            get; set;
        }
        //public string FirstName
        //{
        //    get; set;
        //}
        //public string LastName
        //{
        //    get; set;
        //}
        [NWDInspectorGroupEnd]

        [NWDInspectorGroupStart("Stat")]
        public NWDDateTimeType LastSignIn
        {
            get; set;
        }
        public NWDDateTimeType LastAppOpen
        {
            get; set;
        }
        [NWDInspectorGroupEnd]

        [NWDInspectorGroupStart("Push notification Options")]
        public NWDOperatingSystem OSLastSignIn
        {
            get; set;
        }
        public string AppleNotificationToken
        {
            get; set;
        }
        public string GoogleNotificationToken
        {
            get; set;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================