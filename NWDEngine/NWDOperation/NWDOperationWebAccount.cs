﻿//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:42:38
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================

using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SQLite4Unity3d;
//using BasicToolBox;

#if UNITY_EDITOR
using UnityEditor;
#endif

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public enum NWDOperationWebAccountAction
    {
        signin = 0,
        signout = 1,
        signup = 2,
        rescue = 9,
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class NWDOperationWebAccount : NWDOperationWebUnity
    {
        //-------------------------------------------------------------------------------------------------------------
        public NWDOperationWebAccountAction Action;
        public string PasswordToken;
        public string SignHash;
        public string RescueHash;
        public NWDAccountSignType SignType;
        //-------------------------------------------------------------------------------------------------------------
        static public NWDOperationWebAccount AddOperation(string sName,
                                                           NWEOperationBlock sSuccessBlock = null,
                                                           NWEOperationBlock sFailBlock = null,
                                                           NWEOperationBlock sCancelBlock = null,
                                                           NWEOperationBlock sProgressBlock = null,
                                                           NWDAppEnvironment sEnvironment = null, bool sPriority = false)
        {
            NWDOperationWebAccount rReturn = NWDOperationWebAccount.Create(sName, sSuccessBlock, sFailBlock, sCancelBlock, sProgressBlock, sEnvironment);
            if (rReturn != null)
            {
                NWDDataManager.SharedInstance().WebOperationQueue.AddOperation(rReturn, sPriority);
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        static public NWDOperationWebAccount Create(string sName,
                                                     NWEOperationBlock sSuccessBlock = null,
                                                     NWEOperationBlock sFailBlock = null,
                                                     NWEOperationBlock sCancelBlock = null,
                                                     NWEOperationBlock sProgressBlock = null,
                                                     NWDAppEnvironment sEnvironment = null)
        {
            NWDOperationWebAccount rReturn = null;
            if (NWDDataManager.SharedInstance().DataLoaded() == true)
            {
                if (sName == null)
                {
                    sName = "Web Operation Account";
                }
                if (sEnvironment == null)
                {
                    sEnvironment = NWDAppConfiguration.SharedInstance().SelectedEnvironment();
                }
                GameObject tGameObjectToSpawn = new GameObject(sName);
#if UNITY_EDITOR
                tGameObjectToSpawn.hideFlags = HideFlags.HideAndDontSave;
#else
            tGameObjectToSpawn.transform.SetParent(NWDGameDataManager.UnitySingleton().transform);
#endif
                rReturn = tGameObjectToSpawn.AddComponent<NWDOperationWebAccount>();
                rReturn.GameObjectToSpawn = tGameObjectToSpawn;
                rReturn.Environment = sEnvironment;
                rReturn.QueueName = sEnvironment.Environment;
                rReturn.SecureData = true;
                rReturn.InitBlock(sSuccessBlock, sFailBlock, sCancelBlock, sProgressBlock);
            }
            else
            {
                sFailBlock(null, 1.0F, null);
            }

            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override string ServerFile()
        {
            return NWD.K_AUTHENTIFICATION_PHP;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override bool CanRestart()
        {
            return false;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void DataUploadPrepare()
        {
            //go in secure
            SecureData = true;

            Dictionary<string, object> tDataNotAccount = NWDDataManager.SharedInstance().SynchronizationGetClassesDatas(ResultInfos, Environment, false, NWDDataManager.SharedInstance().mTypeNotAccountDependantList);
            Data = Data.Concat(tDataNotAccount).ToDictionary(x => x.Key, x => x.Value);
            // insert action
            if (Data.ContainsKey(NWD.K_WEB_ACTION_KEY))
            {
                Data[NWD.K_WEB_ACTION_KEY] = Action.ToString();
            }
            else
            {
                Data.Add(NWD.K_WEB_ACTION_KEY, Action.ToString());
            }
            // wich sign will be inserted?
            if (Action == NWDOperationWebAccountAction.signout)
            {
                // insert device key in data and go in secure
                DataAddSecetDevicekey();
            }
            else if (Action == NWDOperationWebAccountAction.signup)
            {
                if (Data.ContainsKey(NWD.K_WEB_SIGN_UP_TYPE_Key))
                {
                    Data[NWD.K_WEB_SIGN_UP_TYPE_Key] = SignType.ToLong();
                }
                else
                {
                    Data.Add(NWD.K_WEB_SIGN_UP_TYPE_Key, SignType.ToLong());
                }

                if (Data.ContainsKey(NWD.K_WEB_SIGN_UP_VALUE_Key))
                {
                    Data[NWD.K_WEB_SIGN_UP_VALUE_Key] = SignHash;
                }
                else
                {
                    Data.Add(NWD.K_WEB_SIGN_UP_RESCUE_Key, SignHash);
                }

                if (Data.ContainsKey(NWD.K_WEB_SIGN_UP_RESCUE_Key))
                {
                    Data[NWD.K_WEB_SIGN_UP_RESCUE_Key] = RescueHash;
                }
                else
                {
                    Data.Add(NWD.K_WEB_SIGN_UP_RESCUE_Key, RescueHash);
                }
            }
            else if (Action == NWDOperationWebAccountAction.signin)
            {
                // insert sign
                Dictionary<string, object> tDataAccount = NWDDataManager.SharedInstance().SynchronizationGetClassesDatas(ResultInfos, Environment, true, NWDDataManager.SharedInstance().mTypeAccountDependantList);
                Data = Data.Concat(tDataAccount).ToDictionary(x => x.Key, x => x.Value);
                if (Data.ContainsKey(NWD.K_WEB_SIGN_Key))
                {
                    Data[NWD.K_WEB_SIGN_Key] = PasswordToken;
                }
                else
                {
                    Data.Add(NWD.K_WEB_SIGN_Key, PasswordToken);
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void DataDownloadedCompute(NWDOperationResult sData)
        {
            //Debug.Log ("NWDOperationWebAccount DataDownloadedCompute start");
            NWDDataManager.SharedInstance().SynchronizationPullClassesDatas(ResultInfos, Environment, sData, NWDDataManager.SharedInstance().mTypeAccountDependantList, NWDOperationSpecial.None);
            //Debug.Log ("NWDOperationWebAccount DataDownloadedCompute finish");
#if UNITY_EDITOR
            NWDAppEnvironmentChooser.Refresh();
#endif
        }
        //-------------------------------------------------------------------------------------------------------------
    }
}
//=====================================================================================================================