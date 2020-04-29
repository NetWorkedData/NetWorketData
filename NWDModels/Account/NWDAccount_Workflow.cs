﻿//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:29:15
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================

//=====================================================================================================================
using System;
using UnityEngine;

namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public enum NWDAccountEnvironment : int
    {
        InGame = 0, // player state (Prod)
        Dev = 1,    // dev state
        Preprod = 2, // preprod state
        Prod = 3, //NEVER COPY ACCOUNT IN PROD !!!!
        None = 9,
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDAccount : NWDBasis
    {
        //-------------------------------------------------------------------------------------------------------------
        public NWDAccount() { }
        //-------------------------------------------------------------------------------------------------------------
        public NWDAccount(bool sInsertInNetWorkedData) : base(sInsertInNetWorkedData) { }
        //-------------------------------------------------------------------------------------------------------------
        private void Check()
        {
            if (Reference.Contains(NWDAccount.K_ACCOUNT_FROM_EDITOR))
            {
                if (DevSync >= 0)
                {
                    InternalDescription = "Create in editor environment " + NWDAppConfiguration.SharedInstance().DevEnvironment.Environment;
                }
                if (PreprodSync >= 0)
                {
                    InternalDescription = "Create in editor environment " + NWDAppConfiguration.SharedInstance().PreprodEnvironment.Environment;
                }
                if (ProdSync >= 0)
                {
                    InternalDescription = "WHAT! Create in editor environment " + NWDAppConfiguration.SharedInstance().ProdEnvironment.Environment;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonInsertedMe()
        {
            base.AddonInsertedMe();
            Check();
            UpdateDataIfModified();
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonUpdateMe()
        {
            base.AddonUpdateMe();
            Check();
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// New reference. If account dependent this UUID of Player Account is integrate in Reference generation
        /// </summary>
        /// <returns>The reference.</returns>
        public override string NewReference()
        {
            string rReturn = string.Empty;
            NWDServerDatabaseAuthentication[] tServerDatas = NWDServerDatas.GetAllConfigurationServerDatabase(NWDAppEnvironment.SelectedEnvironment());
            if (tServerDatas.Length > 1)
            {
                tServerDatas.ShuffleList();
            }
            string tRangeServer = "0000";
            if (tServerDatas.Length > 1)
            {
                NWDServerDatabaseAuthentication tServerData = tServerDatas[0];
                tRangeServer = UnityEngine.Random.Range(tServerData.RangeMin, tServerData.RangeMax).ToString();
            }
            bool tValid = false;
            while (tValid == false)
            {
                string tReferenceMiddle = NWDToolbox.AplhaNumericToNumeric(NWESecurityTools.GenerateSha("5r5" + SystemInfo.deviceUniqueIdentifier + "7z4").ToUpper().Substring(0, 9));

                rReturn = K_ACCOUNT_PREFIX_TRIGRAM + NWEConstants.K_MINUS + tRangeServer + NWEConstants.K_MINUS + tReferenceMiddle + NWEConstants.K_MINUS + UnityEngine.Random.Range(100000, 999999).ToString() + K_ACCOUNT_FROM_EDITOR;
                tValid = TestReference(rReturn);
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void Initialization()
        {
            UseInEnvironment = NWDAccountEnvironment.InGame;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static bool AccountCanSignOut()
        {
            bool rReturn = true;
            foreach (NWDAccountSign tSign in NWDAccountSign.GetReachableDatasAssociated())
            {
                if (tSign.SignHash == NWDAppEnvironment.SelectedEnvironment().SecretKeyDevice())
                {
                    rReturn = false;
                }
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonUnTrashMe()
        {
            AccountUnTrash(Reference);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void AccountUnTrash(string sAccountReference)
        {
            NWEBenchmark.Start();
            foreach (Type tType in NWDDataManager.SharedInstance().mTypeAccountDependantList)
            {
                if (tType != typeof(NWDAccount))
                {
                    foreach (NWDBasis tObject in NWDBasisHelper.FindTypeInfos(tType).Datas)
                    {
                        if (tObject.IsReacheableByAccount(sAccountReference))
                        {
                            if (tObject.UnTrashData())
                            {
                                Debug.Log("Data ref " + tObject.Reference + " put remove from trash");
                            }
                        }
                    }
                }
            }
            NWEBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonTrashMe()
        {
            AccountTrash(Reference);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void AccountTrash(string sAccountReference)
        {
            NWEBenchmark.Start();
            foreach (Type tType in NWDDataManager.SharedInstance().mTypeAccountDependantList)
            {
                if (tType != typeof(NWDAccount))
                {
                    foreach (NWDBasis tObject in NWDBasisHelper.FindTypeInfos(tType).Datas)
                    {
                        if (tObject.IsReacheableByAccount(sAccountReference))
                        {
                            if (tObject.TrashData())
                            {
                                Debug.Log("Data ref " + tObject.Reference + " put in trash, ready to be sync and delete!");
                            }
                        }
                    }
                }
            }
            NWEBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
