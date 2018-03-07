﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
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

//using BTBMiniJSON;

#if UNITY_EDITOR
using UnityEditor;
#endif

using SQLite4Unity3d;

using BasicToolBox;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class NWDOperationWebBlank : NWDOperationWebUnity
	{
		//-------------------------------------------------------------------------------------------------------------
        public List<Type> TypeList = new List<Type>();
        public string Action;
		public bool ForceSync = false;
		//-------------------------------------------------------------------------------------------------------------
        static public NWDOperationWebBlank AddOperation (string sName,
		                                                           BTBOperationBlock sSuccessBlock = null, 
		                                                           BTBOperationBlock sFailBlock = null, 
		                                                           BTBOperationBlock sCancelBlock = null,
                                                                BTBOperationBlock sProgressBlock = null,
                                                                NWDAppEnvironment sEnvironment = null,
                                                                bool sForceSync = false, 
                                                                bool sPriority = false)
        {
            Debug.Log("NWDOperationWebBlank AddOperation()");
            NWDOperationWebBlank rReturn = NWDOperationWebBlank.Create (sName, sSuccessBlock, sFailBlock, sCancelBlock, sProgressBlock, sEnvironment, sForceSync);
			NWDDataManager.SharedInstance().WebOperationQueue.AddOperation (rReturn, sPriority);
			return rReturn;
		}
		//-------------------------------------------------------------------------------------------------------------
        static public NWDOperationWebBlank Create (string sName,
		                                                     BTBOperationBlock sSuccessBlock = null, 
		                                                     BTBOperationBlock sFailBlock = null,
		                                                     BTBOperationBlock sCancelBlock = null,
                                                          BTBOperationBlock sProgressBlock = null,
			NWDAppEnvironment sEnvironment = null,bool sForceSync = false)
        {
            //Debug.Log("NWDOperationWebBlank Create()");
            NWDOperationWebBlank rReturn = null;
			if (sName == null) {
				sName = "UnNamed Web Operation Synchronisation";
			}
			if (sEnvironment == null) {
				sEnvironment = NWDAppConfiguration.SharedInstance().SelectedEnvironment ();
			}

			// IF BTBOperationUnity
			GameObject tGameObjectToSpawn = new GameObject (sName);
            rReturn = tGameObjectToSpawn.AddComponent<NWDOperationWebBlank> ();
			rReturn.GameObjectToSpawn = tGameObjectToSpawn;

			rReturn.Environment = sEnvironment;
			rReturn.QueueName = sEnvironment.Environment;
			rReturn.ForceSync = sForceSync;
			rReturn.InitBlock (sSuccessBlock, sFailBlock, sCancelBlock, sProgressBlock);

			#if UNITY_EDITOR
			#else
			DontDestroyOnLoad (tGameObjectToSpawn);
			#endif

			return rReturn;
		}
		//-------------------------------------------------------------------------------------------------------------
		public override string ServerFile ()
        {
            //Debug.Log("NWDOperationWebBlank ServerFile()");
			return "blank.php";
		}
		//-------------------------------------------------------------------------------------------------------------
		public override void DataUploadPrepare ()
        {
            //Debug.Log("NWDOperationWebBlank DataUploadPrepare()");
            Dictionary<string, object> tData = NWDDataManager.SharedInstance().SynchronizationPushClassesDatas (Environment, ForceSync, TypeList, false);
            tData.Add ("action", Action);
			Data = tData;
		}
		//-------------------------------------------------------------------------------------------------------------
        public override void DataDownloadedCompute (NWDOperationResult sData)
		{
            //Debug.Log("NWDOperationWebBlank DataDownloadedCompute()");
            // I put null for pull typeList to analyze all NWDClass
            NWDDataManager.SharedInstance().SynchronizationPullClassesDatas (Environment, sData, null);
		}
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================