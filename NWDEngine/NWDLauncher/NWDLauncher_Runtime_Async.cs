﻿//=====================================================================================================================
//
//  ideMobi 2020©
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================

using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Linq;

using UnityEngine;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDGameDataManager : NWDCallBackDataLoadOnly
    {
        //-------------------------------------------------------------------------------------------------------------
        private void LaunchRuntimeAsync()
        {
            if (NWDLauncher.GetPreload() == false)
            {
                if (NWDLauncher.GetState() != NWDStatut.NetWorkedDataReady)
                {
                    // Load async the engine!
                    //Debug.Log("########## <color=blue>Load async the engine</color>!");
                    StartCoroutine(NWDLauncher.LaunchRuntimeAsync());
                }
                else
                {
                    //Debug.Log("########## <color=blue>Load async the engine ALL READY READY!</color>!");
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public static partial class NWDLauncher
    {
        //-------------------------------------------------------------------------------------------------------------
        public static IEnumerator LaunchRuntimeAsync()
        {
            //if (ActiveBenchmark)
            {
                NWEBenchmark.Start("Launch_Runtime_Async");
            }
            NWDBundle tBasisBundle = NWDBundle.None;
            if (NWDAppConfiguration.SharedInstance().BundleDatas == false)
            {
                tBasisBundle = NWDBundle.ALL;
            }
            IEnumerator tWaitTime = null;
            StepSum = 12 +
                NWDAppConfiguration.SharedInstance().LauncherClassEditorStep + // load editor class
                NWDAppConfiguration.SharedInstance().LauncherClassAccountStep + // load account class
                NWDAppConfiguration.SharedInstance().LauncherClassEditorStep + // load editor class
                NWDAppConfiguration.SharedInstance().LauncherClassAccountStep + // load account class
                0;
            StepIndex = 0;
            NWENotificationManager.SharedInstance().PostNotification(null, NWDNotificationConstants.K_LAUNCHER_STEP); // to init the gauge to 0
            // lauch engine
            tWaitTime = EngineRuntimeAsync();
            NotifyStep(true);
            yield return tWaitTime;
            // declare models
            tWaitTime = DeclareRuntimeAsync();
            NotifyStep(true);
            yield return tWaitTime;
            // restaure models' param
            tWaitTime = null;
            RestaureStandard();
            NotifyStep(true);
            yield return tWaitTime;

            StepSum = 13 +
                NWDDataManager.SharedInstance().ClassInEditorDatabaseRumberExpected + // load editor class
                NWDDataManager.SharedInstance().ClassInDeviceDatabaseNumberExpected + // load account class
                NWDDataManager.SharedInstance().ClassInEditorDatabaseRumberExpected + // index editor class
                NWDDataManager.SharedInstance().ClassInDeviceDatabaseNumberExpected + // index account class
                0;

            NotifyEngineReady();

            // connect editor
            tWaitTime = null;
            AddIndexMethod();
            NotifyStep(true);
            yield return tWaitTime;
            ConnectEditorStandard();
            NotifyStep(true);
            yield return tWaitTime;
            // create table editor
            tWaitTime = null;
            CreateTableEditorStandard();
            NotifyStep(true);
            yield return tWaitTime;
            // load editor data
            tWaitTime = NWDDataManager.SharedInstance().AsyncReloadAllObjectsEditor(tBasisBundle);
            NotifyStep(true);
            yield return tWaitTime;
            // index all data editor
            //tWaitTime = NWDDataManager.SharedInstance().AsyncIndexAllObjects();
            //yield return tWaitTime;

            NotifyDataEditorReady();

            // need account pincode
            tWaitTime = null;
            ConnectAccountStandard();
            NotifyStep(true);
            yield return tWaitTime;
            // create table account
            tWaitTime = null;
            CreateTableAccountStandard();
            NotifyStep(true);
            yield return tWaitTime;

            NotifyDataAccountReady();
            NotifyStep(true);

            // load account data account
            tWaitTime = NWDDataManager.SharedInstance().AsyncReloadAllObjectsAccount(tBasisBundle);
            yield return tWaitTime;
            // index all data
            tWaitTime = NWDDataManager.SharedInstance().AsyncIndexAllObjects();
            yield return tWaitTime;
            // Special NWDAppConfiguration loaded()
            tWaitTime = null;
            NWDAppConfiguration.SharedInstance().Loaded();
            NotifyStep(true);
            yield return tWaitTime;
            // Ready!
            tWaitTime = null;
            Ready();
            NotifyStep(true);
            yield return tWaitTime;


            //NWEBenchmark.Log(" NWDDataManager.SharedInstance().ClassEditorExpected = " + NWDDataManager.SharedInstance().ClassEditorExpected);
            //NWEBenchmark.Log(" NWDDataManager.SharedInstance().ClassAccountExpected = " + NWDDataManager.SharedInstance().ClassAccountExpected);
            //NWEBenchmark.Log(" StepSum = " + StepSum + " and StepIndex =" + StepIndex);

            if (ActiveBenchmark)
            {
                //TimeFinish = NWEBenchmark.SinceStartup();
                TimeFinish = Time.realtimeSinceStartup;
                TimeNWDFinish = NWEBenchmark.Finish("Launch_Runtime_Async");
                LauncherBenchmarkToMarkdown();
                NWBBenchmarkResult.CurrentData().BenchmarkNow();
            }

            NotifyNetWorkedDataReady();
        }
        //-------------------------------------------------------------------------------------------------------------
        private static IEnumerator EngineRuntimeAsync()
        {
            if (ActiveBenchmark)
            {
                NWEBenchmark.Start();
            }
            State = NWDStatut.EngineStart;
            Thread.CurrentThread.CurrentCulture = NWDConstants.FormatCountry;
            AllNetWorkedDataTypes.Clear();
            BasisToHelperList.Clear();
            NWEBenchmark.Start("get_refelexion");
            List<Type> tTypeList = new List<Type>();
            Type[] tAllTypes = Assembly.GetExecutingAssembly().GetTypes();
            Type[] tAllNWDTypes = (from Type type in tAllTypes where type.IsSubclassOf(typeof(NWDTypeClass)) select type).ToArray();
            Type[] tAllHelperDTypes = (from Type type in tAllTypes where type.IsSubclassOf(typeof(NWDBasisHelper)) select type).ToArray();
            NWEBenchmark.Finish("get_refelexion");
            foreach (Type tType in tAllNWDTypes)
            {
                if (tType != typeof(NWDBasis) &&
                    tType != typeof(NWDBasisBundled) &&
                    tType != typeof(NWDBasisUnsynchronize) &&
                    tType != typeof(NWDBasisAccountUnsynchronize) &&
                    tType != typeof(NWDBasisAccountDependent) &&
                    tType != typeof(NWDBasisAccountRestricted) &&
                    tType != typeof(NWDBasisGameSaveDependent) &&
                    tType.IsGenericType == false)
                {
                    bool tEditorOnly = false;
                    if (tType.IsSubclassOf(typeof(NWDBasisAccountRestricted)))
                    {
                        tEditorOnly = true;
                        NWEBenchmark.LogWarning("exclude " + tType.Name);
                    }
                    //if (tType != typeof(NWDAccount))
                    //{
                    //    if (tType.GetCustomAttributes(typeof(NWDClassUnityEditorOnlyAttribute), true).Length > 0)
                    //    {
                    //        tEditorOnly = true;
                    //        NWEBenchmark.LogWarning("exclude " + tType.Name);
                    //    }
                    //}
                    if (tEditorOnly == false)
                    {
                        if (AllNetWorkedDataTypes.Contains(tType) == false)
                        {
                            AllNetWorkedDataTypes.Add(tType);
                            foreach (Type tPossibleHelper in tAllHelperDTypes)
                            {
                                if (tPossibleHelper.ContainsGenericParameters == false)
                                {
                                    if (tPossibleHelper.BaseType.GenericTypeArguments.Contains(tType))
                                    {
                                        if (BasisToHelperList.ContainsKey(tType) == false)
                                        {
                                            BasisToHelperList.Add(tType, tPossibleHelper);
                                        }
                                        break;
                                    }
                                }
                            }
                            if (BasisToHelperList.ContainsKey(tType) == false)
                            {
                                BasisToHelperList.Add(tType, typeof(NWDBasisHelper));
                            }
                        }
                    }
                }
                if (YieldValid())
                {
                    yield return null;
                }
            }
            StepSum = StepSum + AllNetWorkedDataTypes.Count * 3;
            State = NWDStatut.EngineFinish;
            if (ActiveBenchmark)
            {
                NWEBenchmark.Finish();
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        private static IEnumerator DeclareRuntimeAsync()
        {
            if (ActiveBenchmark)
            {
                NWEBenchmark.Start();
            }
            State = NWDStatut.ClassDeclareStart;
            foreach (Type tType in AllNetWorkedDataTypes)
            {
                if (tType != typeof(NWDBasis))
                {
                    NWDBasisHelper tHelper = NWDBasisHelper.Declare(tType, BasisToHelperList[tType]);
                    State = NWDStatut.ClassDeclareStep;
                }
                if (YieldValid())
                {
                    yield return null;
                }
            }
            State = NWDStatut.ClassDeclareFinish;
            NotifyStep();
            if (ActiveBenchmark)
            {
                NWEBenchmark.Finish();
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================