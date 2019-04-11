﻿//=====================================================================================================================
//
// ideMobi copyright 2018 
// All rights reserved by ideMobi
//
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.IO;
using UnityEngine;
using System.Threading;
using System.Text.RegularExpressions;
using SQLite4Unity3d;
using BasicToolBox;
#if UNITY_EDITOR
using UnityEditor;
#endif
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDDataManager
    {
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The List of Data to Insert by queue in main thread
        /// </summary>
        public List<NWDTypeClass> kInsertDataQueueMain = new List<NWDTypeClass>();
        /// <summary>
        /// The List of Data to Update by queue in main thread
        /// </summary>
        public List<NWDTypeClass> kUpdateDataQueueMain = new List<NWDTypeClass>();
        /// <summary>
        /// The List of Data to Delete by queue in main thread
        /// </summary>
        public List<NWDTypeClass> kDeleteDataQueueMain = new List<NWDTypeClass>();
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The List of Data to Insert by queue in pool thread
        /// </summary>
        public List<NWDTypeClass> kInsertDataQueuePool = new List<NWDTypeClass>();
        /// <summary>
        /// The List of Data to Update by queue in main thread
        /// </summary>
        public List<NWDTypeClass> kUpdateDataQueuePool = new List<NWDTypeClass>();
        /// <summary>
        /// The List of Data to Delete by queue in main thread
        /// </summary>
        public List<NWDTypeClass> kDeleteDataQueuePool = new List<NWDTypeClass>();
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The List of Data in pending to write
        /// </summary>
        public List<NWDTypeClass> kDataInWriting = new List<NWDTypeClass>();
        //-------------------------------------------------------------------------------------------------------------
        #region Queue Data
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Datas in writing process.
        /// </summary>
        /// <returns><c>true</c>, if in writing process was dataed, <c>false</c> otherwise.</returns>
        public bool DataInWritingProcess()
        {
            //BTBBenchmark.Start();
            bool rReturn = false;
            if (kDataInWriting.Count > 0)
            {
                rReturn = true;
            }
            //BTBBenchmark.Finish();
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Execute Queues
        /// </summary>
        public int DataQueueCounter()
        {
            //BTBBenchmark.Start();
            int rResult = kDataInWriting.Count;
            //BTBBenchmark.Finish();
            return rResult;
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Execute Queues
        /// </summary>
        public void DataQueueExecute()
        {
            //BTBBenchmark.Start();
            if (DataInWritingProcess())
            {
                DataQueueMainExecute();
                DataQueuePoolExecute();
            }
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Execute the Queue on main thread
        /// </summary>
        public void DataQueueMainExecute()
        {
            //BTBBenchmark.Start();
            if (DataInWritingProcess())
            {
                List<NWDTypeClass> tInsertDataQueueMain = new List<NWDTypeClass>(kInsertDataQueueMain);
                List<NWDTypeClass> tUpdateDataQueueMain = new List<NWDTypeClass>(kUpdateDataQueueMain);
                List<NWDTypeClass> tDeleteDataQueueMain = new List<NWDTypeClass>(kDeleteDataQueueMain);
                kInsertDataQueueMain = new List<NWDTypeClass>();
                kUpdateDataQueueMain = new List<NWDTypeClass>();
                kDeleteDataQueueMain = new List<NWDTypeClass>();
                InsertDataQueueExecute(tInsertDataQueueMain);
                UpdateDataQueueExecute(tUpdateDataQueueMain);
                DeleteDataQueueExecute(tDeleteDataQueueMain);
            }
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Execute the Queue on pool thread
        /// </summary>
        public void DataQueuePoolExecute()
        {
            //BTBBenchmark.Start();
            if (DataInWritingProcess())
            {
                List<NWDTypeClass> tInsertDataQueuePool = new List<NWDTypeClass>(kInsertDataQueuePool);
                List<NWDTypeClass> tUpdateDataQueuePool = new List<NWDTypeClass>(kUpdateDataQueuePool);
                List<NWDTypeClass> tDeleteDataQueuePool = new List<NWDTypeClass>(kDeleteDataQueuePool);
                kInsertDataQueuePool = new List<NWDTypeClass>();
                kUpdateDataQueuePool = new List<NWDTypeClass>();
                kDeleteDataQueuePool = new List<NWDTypeClass>();
                ThreadPool.QueueUserWorkItem(DataQueuePoolThread, new object[] {
                tInsertDataQueuePool,
                tUpdateDataQueuePool,
                tDeleteDataQueuePool
            });
            }
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Execute the Queue on pool thread by ThreadPool.QueueUserWorkItem()
        /// </summary>
        private void DataQueuePoolThread(object sState)
        {
            //BTBBenchmark.Start();
            object[] tParam = sState as object[];
            List<NWDTypeClass> tInsertDataQueue = tParam[0] as List<NWDTypeClass>;
            List<NWDTypeClass> tUpdateDataQueue = tParam[1] as List<NWDTypeClass>;
            List<NWDTypeClass> tDeleteDataQueue = tParam[2] as List<NWDTypeClass>;
            InsertDataQueueExecute(tInsertDataQueue);
            UpdateDataQueueExecute(tUpdateDataQueue);
            DeleteDataQueueExecute(tDeleteDataQueue);
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion Queue Data
        #region Insert Data
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Insert data in database
        /// </summary>
        /// <param name="sObject">S object.</param>
        /// <param name="sWritingMode">S writing mode.</param>
        public void InsertData(NWDTypeClass sObject, NWDWritingMode sWritingMode = NWDWritingMode.ByDefaultLocal)
        {
            //            Debug.Log("NWDDataManager InsertData()");
            // Determine the default mode
            sWritingMode = NWDAppConfiguration.WritingMode(sWritingMode);
            //BTBBenchmark.Start();
            switch (sWritingMode)
            {
                case NWDWritingMode.MainThread:
                    {
                        //Debug.Log("NWDDataManager InsertData() MainThread");
                        InsertDataExecute(sObject);
                    }
                    break;
                case NWDWritingMode.PoolThread:
                    {
                        //Debug.Log("NWDDataManager InsertData() PoolThread");
#if UNITY_EDITOR
                        RepaintWindowsInManager(sObject.GetType());
#endif
                        ThreadPool.QueueUserWorkItem(InsertDataExecute, sObject);
                    }
                    break;
                case NWDWritingMode.QueuedMainThread:
                    {
                        //Debug.Log("NWDDataManager InsertData() QueuedMainThread");
                        if (kInsertDataQueueMain.Contains(sObject) == false)
                        {
#if UNITY_EDITOR
                            RepaintWindowsInManager(sObject.GetType());
#endif
                            kInsertDataQueueMain.Add(sObject);
                        }
                    }
                    break;
                case NWDWritingMode.QueuedPoolThread:
                    {
                        //Debug.Log("NWDDataManager InsertData() QueuedPoolThread");
                        if (kInsertDataQueuePool.Contains(sObject) == false)
                        {
#if UNITY_EDITOR
                            RepaintWindowsInManager(sObject.GetType());
#endif
                            kInsertDataQueuePool.Add(sObject);
                        }
                    }
                    break;
            }
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Insert data in database on pool thread.
        /// </summary>
        /// <param name="sObject">S object.</param>
        private void InsertDataExecute(object sObject)
        {
            //BTBBenchmark.Start();
            //Type tType = sObject.GetType();
            NWDTypeClass tObject = sObject as NWDTypeClass;

            tObject.InsertDataProceedWithTransaction();
            tObject.InsertDataFinish();
            //var tMethodInfo = tType.GetMethod("InsertDataProceedWithTransaction", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            //if (tMethodInfo != null)
            //{
            //    tMethodInfo.Invoke(sObject, null);
            //}
            //var tMethodFinish = tType.GetMethod("InsertDataFinish", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            //if (tMethodFinish != null)
            //{
            //    tMethodFinish.Invoke(sObject, null);
            //}
            InsertDataCompleted(tObject);

            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Insert data queue pool execute.
        /// </summary>
        /// <param name="sInsertDataQueuePool">S insert data queue pool.</param>
        private void InsertDataQueueExecute(List<NWDTypeClass> sInsertDataQueuePool)
        {
            if (NWDDataManager.SharedInstance().SQLiteConnectionAccountIsValid() && NWDDataManager.SharedInstance().SQLiteConnectionEditorIsValid())
            {
                //BTBBenchmark.Start();
                //Debug.Log("InsertDataQueueExecute with " + sInsertDataQueuePool.Count + " Object(s)");
                List<Type> tTypeList = new List<Type>();
                if (sInsertDataQueuePool.Count > 0)
                {
                    //if (NWDDataManager.SharedInstance().DataAccountConnected == true)
                    //{
                        NWDDataManager.SharedInstance().SQLiteConnectionAccount.BeginTransaction();
                    //}
                    //if (NWDDataManager.SharedInstance().DataEditorConnected == true)
                    //{
                        NWDDataManager.SharedInstance().SQLiteConnectionEditor.BeginTransaction();
                    //}
                    foreach (NWDTypeClass tObject in sInsertDataQueuePool)
                    {
                        tObject.InsertDataProceed();
                        //Type tType = tObject.GetType();
                        //var tMethodInfo = tType.GetMethod("InsertDataProceed", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                        //if (tMethodInfo != null)
                        //{
                        //    tMethodInfo.Invoke(tObject, null);
                        //}
                    }
                    //if (NWDDataManager.SharedInstance().DataAccountConnected == true)
                    //{
                        NWDDataManager.SharedInstance().SQLiteConnectionAccount.Commit();
                    //}
                    //if (NWDDataManager.SharedInstance().DataEditorConnected == true)
                    //{
                        NWDDataManager.SharedInstance().SQLiteConnectionEditor.Commit();
                    //}
                    foreach (NWDTypeClass tObject in sInsertDataQueuePool)
                    {
                        Type tType = tObject.GetType();
                        if (tTypeList.Contains(tType) == false)
                        {
                            tTypeList.Add(tType);
                        }
                        tObject.InsertDataFinish();
                        //var tMethodFinish = tType.GetMethod("InsertDataFinish", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                        //if (tMethodFinish != null)
                        //{
                        //    tMethodFinish.Invoke(tObject, null);
                        //}
                    }
                    InsertDataQueueCompleted(tTypeList);
                }
                sInsertDataQueuePool = null;
            }
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        private void InsertDataCompleted(NWDTypeClass sObject)
        {
            //BTBBenchmark.Start();
            NWDDataManagerMainThread.AddInsertType(sObject.GetType());
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        private void InsertDataQueueCompleted(List<Type> sTypeList)
        {
            //BTBBenchmark.Start();
            NWDDataManagerMainThread.AddInsertTypeList(sTypeList);
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion Insert Data
        #region Update Data
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Update data in database.
        /// </summary>
        /// <param name="sObject">S object.</param>
        /// <param name="sWritingMode">S writing mode.</param>
        public void UpdateData(NWDTypeClass sObject, NWDWritingMode sWritingMode = NWDWritingMode.ByDefaultLocal)
        {
            // Determine the default mode
            sWritingMode = NWDAppConfiguration.WritingMode(sWritingMode);
            //BTBBenchmark.Start();
            switch (sWritingMode)
            {
                case NWDWritingMode.MainThread:
                    {
                        //Debug.Log("NWDDataManager UpdateData() MainThread");
                        UpdateDataExecute(sObject);
                        BTBNotificationManager.SharedInstance().PostNotification(sObject, NWDNotificationConstants.K_DATA_LOCAL_UPDATE);
                    }
                    break;
                case NWDWritingMode.PoolThread:
                    {
                        //Debug.Log("NWDDataManager UpdateData() PoolThread");
#if UNITY_EDITOR
                        RepaintWindowsInManager(sObject.GetType());
#endif
                        ThreadPool.QueueUserWorkItem(UpdateDataExecute, sObject);

                    }
                    break;
                case NWDWritingMode.QueuedMainThread:
                    {
                        //Debug.Log("NWDDataManager UpdateData() QueuedMainThread");
                        if (kUpdateDataQueueMain.Contains(sObject) == false)
                        {
#if UNITY_EDITOR
                            RepaintWindowsInManager(sObject.GetType());
#endif
                            kUpdateDataQueueMain.Add(sObject);
                        }
                    }
                    break;
                case NWDWritingMode.QueuedPoolThread:
                    {
                        //Debug.Log("NWDDataManager UpdateData() QueuedPoolThread");
                        if (kUpdateDataQueuePool.Contains(sObject) == false)
                        {
#if UNITY_EDITOR
                            RepaintWindowsInManager(sObject.GetType());
#endif
                            kUpdateDataQueuePool.Add(sObject);
                        }
                    }
                    break;
            }

#if UNITY_EDITOR
            // no auto update data
            if (sObject != null)
            {
                //MethodBase tMethodInfo = NWDAliasMethod.GetMethodPublicInstance(sObject.GetType(), NWDConstants.M_CheckError);
                //if (tMethodInfo != null)
                //{
                //    tMethodInfo.Invoke(sObject, null);
                //}
                sObject.ErrorCheck();
            }
#endif
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Update data in database on pool thread.
        /// </summary>
        /// <param name="sObject">S object.</param>
        private void UpdateDataExecute(object sObject)
        {
            //BTBBenchmark.Start();
            //Type tType = sObject.GetType();
            NWDTypeClass tObject = sObject as NWDTypeClass;
            tObject.UpdateDataProceedWithTransaction();
            //var tMethodInfo = tType.GetMethod("UpdateDataProceedWithTransaction", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            //if (tMethodInfo != null)
            //{
            //    tMethodInfo.Invoke(sObject, null);
            //}
            tObject.UpdateDataFinish();
            //var tMethodFinish = tType.GetMethod("UpdateDataFinish", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            //if (tMethodFinish != null)
            //{
            //    tMethodFinish.Invoke(sObject, null);
            //}
            UpdateDataCompleted(tObject);
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Update data queue pool execute.
        /// </summary>
        /// <param name="sUpdateDataQueuePool">S update data queue pool.</param>
        private void UpdateDataQueueExecute(List<NWDTypeClass> sUpdateDataQueuePool)
        {
            //BTBBenchmark.Start();
            //Debug.Log("UpdateDataQueueExecute with " + sUpdateDataQueuePool.Count + " Object(s)");
            if (NWDDataManager.SharedInstance().SQLiteConnectionAccountIsValid() && NWDDataManager.SharedInstance().SQLiteConnectionEditorIsValid())
            {
                List<Type> tTypeList = new List<Type>();
                if (sUpdateDataQueuePool.Count > 0)
                {
                    //if (NWDDataManager.SharedInstance().DataAccountConnected == true)
                    //{
                        NWDDataManager.SharedInstance().SQLiteConnectionAccount.BeginTransaction();
                    //}
                    //if (NWDDataManager.SharedInstance().DataEditorConnected == true)
                    //{
                        NWDDataManager.SharedInstance().SQLiteConnectionEditor.BeginTransaction();
                    //}
                    foreach (NWDTypeClass tObject in sUpdateDataQueuePool)
                    {
                        tObject.UpdateDataProceed();
                        //Type tType = tObject.GetType();
                        //var tMethodInfo = tType.GetMethod("UpdateDataProceed", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                        //if (tMethodInfo != null)
                        //{
                        //    tMethodInfo.Invoke(tObject, null);
                        //}
                    }
                    //if (NWDDataManager.SharedInstance().DataAccountConnected == true)
                    //{
                        NWDDataManager.SharedInstance().SQLiteConnectionAccount.Commit();
                    //}
                    //if (NWDDataManager.SharedInstance().DataEditorConnected == true)
                    //{
                        NWDDataManager.SharedInstance().SQLiteConnectionEditor.Commit();
                    //}
                    foreach (NWDTypeClass tObject in sUpdateDataQueuePool)
                    {
                        Type tType = tObject.GetType();
                        if (tTypeList.Contains(tType) == false)
                        {
                            tTypeList.Add(tType);
                        }
                        tObject.UpdateDataFinish();
                        //var tMethodFinish = tType.GetMethod("UpdateDataFinish", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                        //if (tMethodFinish != null)
                        //{
                        //    tMethodFinish.Invoke(tObject, null);
                        //}
                    }
                    UpdateDataQueueCompleted(tTypeList);
                }
                sUpdateDataQueuePool = null;
            }
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        private void UpdateDataCompleted(NWDTypeClass sObject)
        {
            //BTBBenchmark.Start();
            NWDDataManagerMainThread.AddUpdateType(sObject.GetType());
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        private void UpdateDataQueueCompleted(List<Type> sTypeList)
        {
            //BTBBenchmark.Start();
            NWDDataManagerMainThread.AddUpdateTypeList(sTypeList);
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion Update Data
        #region Delete Data
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Delete data in database.
        /// </summary>
        /// <param name="sObject">S object.</param>
        /// <param name="sWritingMode">S writing mode.</param>
        public void DeleteData(NWDTypeClass sObject, NWDWritingMode sWritingMode = NWDWritingMode.MainThread)
        {
            // Determine the default mode
            sWritingMode = NWDAppConfiguration.WritingMode(sWritingMode);
            //BTBBenchmark.Start();
            switch (sWritingMode)
            {
                case NWDWritingMode.MainThread:
                    {
                        //Debug.Log("NWDDataManager DeleteData() MainThread");
                        DeleteDataExecute(sObject);
                        BTBNotificationManager.SharedInstance().PostNotification(sObject, NWDNotificationConstants.K_DATA_LOCAL_DELETE);
                    }
                    break;
                case NWDWritingMode.PoolThread:
                    {
                        //Debug.Log("NWDDataManager DeleteData() PoolThread");
#if UNITY_EDITOR
                        RepaintWindowsInManager(sObject.GetType());
#endif
                        ThreadPool.QueueUserWorkItem(DeleteDataExecute, sObject);
                    }
                    break;
                case NWDWritingMode.QueuedMainThread:
                    {
                        //Debug.Log("NWDDataManager DeleteData() QueuedMainThread");
                        if (kDeleteDataQueueMain.Contains(sObject) == false)
                        {
#if UNITY_EDITOR
                            RepaintWindowsInManager(sObject.GetType());
#endif
                            kDeleteDataQueueMain.Add(sObject);
                        }
                    }
                    break;
                case NWDWritingMode.QueuedPoolThread:
                    {
                        //Debug.Log("NWDDataManager DeleteData() QueuedPoolThread");
                        if (kDeleteDataQueuePool.Contains(sObject) == false)
                        {
#if UNITY_EDITOR
                            RepaintWindowsInManager(sObject.GetType());
#endif
                            kDeleteDataQueuePool.Add(sObject);
                        }
                    }
                    break;
            }
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Delete data pool.
        /// </summary>
        /// <param name="sObject">S object.</param>
        private void DeleteDataExecute(object sObject)
        {
            //BTBBenchmark.Start();
            //Type tType = sObject.GetType();
            NWDTypeClass tObject = sObject as NWDTypeClass;
            tObject.DeleteDataProceed();
            //var tMethodInfo = tType.GetMethod("DeleteDataProceed", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            //if (tMethodInfo != null)
            //{
            //    tMethodInfo.Invoke(sObject, null);
            //}
            tObject.DeleteDataFinish();
            //var tMethodFinish = tType.GetMethod("DeleteDataFinish", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            //if (tMethodFinish != null)
            //{
            //    tMethodFinish.Invoke(sObject, null);
            //}
            DeleteDataCompleted(tObject);
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Delete data queue pool execute.
        /// </summary>
        /// <param name="sDeleteDataQueuePool">S delete data queue pool.</param>
        private void DeleteDataQueueExecute(List<NWDTypeClass> sDeleteDataQueuePool)
        {
            if (NWDDataManager.SharedInstance().SQLiteConnectionAccountIsValid() && NWDDataManager.SharedInstance().SQLiteConnectionEditorIsValid())
            {
                //BTBBenchmark.Start();
                //Debug.Log("DeleteDataQueueExecute with " + sDeleteDataQueuePool.Count + " Object(s)");
                List<Type> tTypeList = new List<Type>();
                if (sDeleteDataQueuePool.Count > 0)
                {
                    //if (NWDDataManager.SharedInstance().DataAccountConnected == true)
                    //{
                        NWDDataManager.SharedInstance().SQLiteConnectionAccount.BeginTransaction();
                    //}
                    //if (NWDDataManager.SharedInstance().DataEditorConnected == true)
                    //{
                        NWDDataManager.SharedInstance().SQLiteConnectionEditor.BeginTransaction();
                    //}
                    foreach (NWDTypeClass tObject in sDeleteDataQueuePool)
                    {
                        tObject.DeleteDataProceed();
                        //Type tType = tObject.GetType();
                        //var tMethodInfo = tType.GetMethod("DeleteDataProceed", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                        //if (tMethodInfo != null)
                        //{
                        //    tMethodInfo.Invoke(tObject, null);
                        //}
                    }
                    //if (NWDDataManager.SharedInstance().DataAccountConnected == true)
                    //{
                        NWDDataManager.SharedInstance().SQLiteConnectionAccount.Commit();
                    //}
                    //if (NWDDataManager.SharedInstance().DataEditorConnected == true)
                    //{
                        NWDDataManager.SharedInstance().SQLiteConnectionEditor.Commit();
                    //}
                    foreach (NWDTypeClass tObject in kDeleteDataQueueMain)
                    {
                        Type tType = tObject.GetType();
                        if (tTypeList.Contains(tType) == false)
                        {
                            tTypeList.Add(tType);
                        }
                        tObject.DeleteDataFinish();
                        //var tMethodFinish = tType.GetMethod("DeleteDataFinish", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                        //if (tMethodFinish != null)
                        //{
                        //    tMethodFinish.Invoke(tObject, null);
                        //}
                    }
                    DeleteDataQueueCompleted(tTypeList);
                }
                sDeleteDataQueuePool = null;
            }
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        private void DeleteDataCompleted(NWDTypeClass sObject)
        {
            //BTBBenchmark.Start();
            NWDDataManagerMainThread.AddDeleteType(sObject.GetType());
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        private void DeleteDataQueueCompleted(List<Type> sTypeList)
        {
            //BTBBenchmark.Start();
            NWDDataManagerMainThread.AddDeleteTypeList(sTypeList);
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion Delete Data
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================