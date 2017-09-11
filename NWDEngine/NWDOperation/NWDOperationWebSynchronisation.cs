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

using BTBMiniJSON;

#if UNITY_EDITOR
using UnityEditor;
#endif

using SQLite4Unity3d;

using BasicToolBox;

//=====================================================================================================================
namespace NetWorkedData
{
	public class NWDOperationWebSynchronisation : NWDOperationWebUnity
	{
		//-------------------------------------------------------------------------------------------------------------
		public List<Type> TypeList;
		public bool ForceSync = false;
		public bool FlushTrash = false;
		//-------------------------------------------------------------------------------------------------------------
		static public NWDOperationWebSynchronisation AddOperation (string sName,
		                                                           BTBOperationBlock sSuccessBlock = null, 
		                                                           BTBOperationBlock sFailBlock = null, 
		                                                           BTBOperationBlock sCancelBlock = null,
		                                                           BTBOperationBlock sProgressBlock = null, 
		                                                           NWDAppEnvironment sEnvironment = null,
		                                                           List<Type> sTypeList = null, bool sForceSync = false, bool sPriority = false)
		{
			NWDOperationWebSynchronisation rReturn = NWDOperationWebSynchronisation.Create (sName, sSuccessBlock, sFailBlock, sCancelBlock, sProgressBlock, sEnvironment, sTypeList, sForceSync);
			NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (rReturn, sPriority);
			return rReturn;
		}
		//-------------------------------------------------------------------------------------------------------------
		static public NWDOperationWebSynchronisation Create (string sName,
		                                                     BTBOperationBlock sSuccessBlock = null, 
		                                                     BTBOperationBlock sFailBlock = null,
		                                                     BTBOperationBlock sCancelBlock = null,
		                                                     BTBOperationBlock sProgressBlock = null,
		                                                     NWDAppEnvironment sEnvironment = null, List<Type> sTypeList = null, bool sForceSync = false)
		{
			NWDOperationWebSynchronisation rReturn = null;
			if (sName == null) {
				sName = "UnNamed Web Operation Synchronisation";
			}
			if (sEnvironment == null) {
				sEnvironment = NWDAppConfiguration.SharedInstance.SelectedEnvironment ();
			}

			// IF BTBOperationUnity
			GameObject tGameObjectToSpawn = new GameObject (sName);
			rReturn = tGameObjectToSpawn.AddComponent<NWDOperationWebSynchronisation> ();
			rReturn.GameObjectToSpawn = tGameObjectToSpawn;

			rReturn.Environment = sEnvironment;
			rReturn.QueueName = sEnvironment.Environment;
			rReturn.TypeList = sTypeList;
			rReturn.ForceSync = sForceSync;

			rReturn.InitBlock (sSuccessBlock, sFailBlock, sCancelBlock, sProgressBlock);

			#if UNITY_EDITOR
			#else
			DontDestroyOnLoad (tGameObjectToSpawn);
			#endif
			// ELSE IF BTBOperationWWW
//			rReturn = new BTBOperationSynchronisation();
			// END
			return rReturn;
		}
		//-------------------------------------------------------------------------------------------------------------
		public override string ServerFile ()
		{
			return "webservices.php";
		}
		//-------------------------------------------------------------------------------------------------------------
		public override void DataUploadPrepare ()
		{
			Dictionary<string, object> tData = NWDDataManager.SharedInstance.SynchronizationPushClassesDatas (Environment, ForceSync, TypeList);
			tData.Add ("action", "sync");
			#if UNITY_EDITOR
			tData.Add ("flushtrash", FlushTrash);
			#endif
			Data = tData;
		}
		//-------------------------------------------------------------------------------------------------------------
		public override void DataDownloadedCompute (Dictionary<string, object> sData)
		{
			NWDDataManager.SharedInstance.SynchronizationPullClassesDatas (Environment, sData, TypeList);
		}
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================