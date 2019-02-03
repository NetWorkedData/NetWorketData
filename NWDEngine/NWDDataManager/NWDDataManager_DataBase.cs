﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
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

#if UNITY_EDITOR
using UnityEditor;
#endif

using SQLite4Unity3d;

using BasicToolBox;
//using ColoredAdvancedDebug;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDDataManager
    {
        //-------------------------------------------------------------------------------------------------------------
        public static List<object> kObjectToUpdateQueue = new List<object>();
        //-------------------------------------------------------------------------------------------------------------
        public void ConnectToDatabase()
        {
             //BTBBenchmark.Start();
            if (kConnectedToDatabase == false && kConnectedToDatabaseIsProgress == false)
            {
                kConnectedToDatabaseIsProgress = true;
#if UNITY_EDITOR
                // create the good folder
                string tAccessPath = Application.dataPath;
                if (Directory.Exists(tAccessPath + "/" + DatabasePathEditor) == false)
                {
                    //Debug.Log("NWDDataManager ConnectToDatabase () path : " + tAccessPath + "/" + DatabasePathEditor);
                    AssetDatabase.CreateFolder("Assets", DatabasePathEditor);
                    AssetDatabase.ImportAsset("Assets/" + DatabasePathEditor);
                    AssetDatabase.Refresh();
                }
                // path for base editor
                string tDatabasePathEditor = "Assets/" + DatabasePathEditor + "/" + DatabaseNameEditor;
                string tDatabasePathAccount = "Assets/" +
                NWDAppConfiguration.SharedInstance().DatabasePrefix + DatabaseNameAccount;
#else
                // Get saved App version from pref
                // check if file exists in Application.persistentDataPath
                string tPathEditor = string.Format ("{0}/{1}", Application.persistentDataPath, DatabaseNameEditor);
                string tPathAccount = string.Format ("{0}/{1}", Application.persistentDataPath, NWDAppConfiguration.SharedInstance().DatabasePrefix + DatabaseNameAccount);
                // if must be update by build version : delete old editor data!
                if (UpdateBuildTimestamp() == true) // must update the editor base
                {
                    Debug.Log("#DATABASE# Application must be updated with the database from bundle! Copy the New database");
                    Debug.Log("#DATABASE# Application will delete database : " + tPathEditor);
                    File.Delete(tPathEditor);
                    Debug.Log("#DATABASE# Application has delete database : " + tPathEditor);
                }
                // Write editor database
                if (!File.Exists (tPathEditor))
                {
                    // if it doesn't ->
                    // open StreamingAssets directory and load the db ->
                    Debug.Log("#DATABASE# Application will copy database : " + tPathEditor);
#if UNITY_ANDROID
                    var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseNameEditor);  // this is the path to your StreamingAssets in android
                    while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
                    // then save to Application.persistentDataPath
                    File.WriteAllBytes(tPathEditor, loadDb.bytes);
#elif UNITY_IOS
                    var loadDb = Application.dataPath + "/Raw/" + DatabaseNameEditor;  // this is the path to your StreamingAssets in iOS
                    // then save to Application.persistentDataPath
                    File.Copy (loadDb, tPathEditor);
#elif UNITY_TVOS
                    var loadDb = Application.dataPath + "/Raw/" + DatabaseNameEditor;  // this is the path to your StreamingAssets in iOS
                    // then save to Application.persistentDataPath
                    File.Copy (loadDb, tPathEditor);
#elif UNITY_STANDALONE_OSX
                    var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseNameEditor;
                    // then save to Application.persistentDataPath
                    File.Copy(loadDb, tPathEditor);
#elif UNITY_WP8
                    var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseNameEditor;
                    // then save to Application.persistentDataPath
                    File.Copy(loadDb, tPathEditor);
#elif UNITY_WINRT
                    var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseNameEditor;
                    // then save to Application.persistentDataPath
                    File.Copy(loadDb, tPathEditor);
#elif UNITY_WSA_10_0
                    var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseNameEditor;
                    // then save to Application.persistentDataPath
                    File.Copy(loadDb, tPathEditor);
#elif UNITY_STANDALONE_WIN
                    var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseNameEditor;
                    // then save to Application.persistentDataPath
                    File.Copy(loadDb, tPathEditor);
#elif UNITY_STANDALONE_LINUX
                    var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseNameEditor;
                    // then save to Application.persistentDataPath
                    File.Copy(loadDb, tPathEditor);
#else
                    var loadDb = Application.dataPath + "/Resources/StreamingAssets/" + DatabaseNameEditor;
                    // then save to Application.persistentDataPath
                    File.Copy(loadDb, tPathEditor);
#endif
                }

                string tDatabasePathEditor = tPathEditor;
                string tDatabasePathAccount = tPathAccount;
#endif
                string tAccountPass = NWDAppConfiguration.SharedInstance().GetAccountPass();
                string tEditorPass = NWDAppConfiguration.SharedInstance().GetEditorPass();
                if (NWDAppEnvironment.SelectedEnvironment() == NWDAppConfiguration.SharedInstance().DevEnvironment
                || NWDAppEnvironment.SelectedEnvironment() == NWDAppConfiguration.SharedInstance().PreprodEnvironment)
                {
                    Debug.Log("ConnectToDatabase () tDatabasePathEditor : " + tDatabasePathEditor);
                    Debug.Log("ConnectToDatabase () tEditorPass : " + tEditorPass);
                    Debug.Log("ConnectToDatabase () tDatabasePathAccount : " + tDatabasePathAccount);
                    Debug.Log("ConnectToDatabase () tAccountPass : " + tAccountPass);
                }
                SQLiteConnectionEditor = new SQLiteConnection(tDatabasePathEditor, tEditorPass, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
                // RESET TOKEN SYNC OF USER 'S DATAS TO ZERO!
                if (File.Exists(tDatabasePathAccount) == false)
                {
                    // restart with new user!
                    NWDAppEnvironment.SelectedEnvironment().ResetSession();
                    foreach (Type tType in NWDDataManager.SharedInstance().mTypeAccountDependantList)
                    {
                        NWDAliasMethod.InvokeClassMethod(tType, NWDConstants.M_SynchronizationSetToZeroTimestamp);
                    }
                }
                SQLiteConnectionAccount = new SQLiteConnection(tDatabasePathAccount, tAccountPass, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
                // waiting the tables and file will be open...
                // TODO: REAL DISPO! MARCHE PAS?!
                double tSeconds = SQLiteConnectionAccount.BusyTimeout.TotalSeconds +
                SQLiteConnectionEditor.BusyTimeout.TotalSeconds;
                DateTime t = DateTime.Now;
                DateTime tf = DateTime.Now.AddSeconds(tSeconds);
                while (t < tf)
                {
                    t = DateTime.Now;
                }
                // TEST WHILE / MARCHE PAS
                while (SQLiteConnectionEditor.IsOpen() == false)
                {
                    Debug.LogWarning("SQLiteConnectionEditor is not opened!");
                    // waiting
                    // TODO : timeout and Mesaage d'erreur : desinstaller app et reinstaller
                    // TODO : Detruire fichier et reinstaller ? 
                }
                
                while (SQLiteConnectionAccount.IsOpen() == false)
                {
                    Debug.LogWarning("SQLiteConnectionAccount is not opened!");
                    // waiting
                    // TODO : timeout and Mesaage d'erreur : desinstaller app et reinstaller
                    // TODO : Detruire fichier et reinstaller ? 
                }
                // finish
                kConnectedToDatabase = true;
                kConnectedToDatabaseIsProgress = false;
            }
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void DeleteDatabase()
        {
            //Debug.Log("DeleteDatabase ()");
            kConnectedToDatabase = false;
            //Close SLQite
            if (SQLiteConnectionEditor != null)
            {
                SQLiteConnectionEditor.Close();
            }
            SQLiteConnectionEditor = null;
            if (SQLiteConnectionAccount != null)
            {
                SQLiteConnectionAccount.Close();
            }
            SQLiteConnectionAccount = null;
            // reload empty object
            NWDDataManager.SharedInstance().ReloadAllObjects();
            // database is not connected
            kConnectedToDatabase = false;
#if UNITY_EDITOR
            if (AssetDatabase.IsValidFolder("Assets/" + DatabasePathEditor) == false)
            {
                AssetDatabase.CreateFolder("Assets", DatabasePathEditor);
            }
            string tDatabasePathEditor = "Assets/" + DatabasePathEditor + "/" + DatabaseNameEditor;
            string tDatabasePathAccount = "Assets/" +
             NWDAppConfiguration.SharedInstance().DatabasePrefix + DatabaseNameAccount;

            File.Delete(tDatabasePathEditor);
            File.Delete(tDatabasePathAccount);
#else
				string tPathEditor = string.Format ("{0}/{1}", Application.persistentDataPath, DatabaseNameEditor);
				string tPathAccount = string.Format ("{0}/{1}", Application.persistentDataPath, DatabaseNameAccount);
                File.Delete(tPathEditor);
                File.Delete(tPathAccount);
#endif
        }
#if UNITY_EDITOR
        //-------------------------------------------------------------------------------------------------------------
        public void RecreateDatabase(bool sRegeneratePassword = false, bool sRegenerateDeviceSalt = false)
        {
            // delete DataBase
            DeleteDatabase();
            bool tCSharpRegenerate = false;
            if (sRegeneratePassword == true)
            {
                NWDAppConfiguration.SharedInstance().EditorPass = NWDToolbox.RandomStringCypher(UnityEngine.Random.Range(24, 36));
                NWDAppConfiguration.SharedInstance().EditorPassA = NWDToolbox.RandomStringCypher(UnityEngine.Random.Range(12, 18));
                NWDAppConfiguration.SharedInstance().EditorPassB = NWDToolbox.RandomStringCypher(UnityEngine.Random.Range(12, 18));
                // force data base to be copy on next install!
                int tTimeStamp = NWDToolbox.Timestamp();
                NWDAppConfiguration.SharedInstance().DevEnvironment.BuildTimestamp = tTimeStamp;
                NWDAppConfiguration.SharedInstance().PreprodEnvironment.BuildTimestamp = tTimeStamp;
                NWDAppConfiguration.SharedInstance().ProdEnvironment.BuildTimestamp = tTimeStamp;
                tCSharpRegenerate = true;
            }
            if (sRegenerateDeviceSalt == true)
            {
                NWDAppConfiguration.SharedInstance().DatabasePrefix = "NWD" + BTBDateHelper.ConvertToTimestamp(DateTime.Now).ToString("F0");
                NWDAppConfiguration.SharedInstance().AccountHashSalt = NWDToolbox.RandomStringCypher(UnityEngine.Random.Range(24, 36));
                NWDAppConfiguration.SharedInstance().AccountHashSaltA = NWDToolbox.RandomStringCypher(UnityEngine.Random.Range(12, 18));
                NWDAppConfiguration.SharedInstance().AccountHashSaltB = NWDToolbox.RandomStringCypher(UnityEngine.Random.Range(12, 18));
                tCSharpRegenerate = true;
            }
            if (tCSharpRegenerate == true)
            {
                NWDAppConfiguration.SharedInstance().GenerateCSharpFile(NWDAppConfiguration.SharedInstance().SelectedEnvironment());
            }
        }
#endif
        //-------------------------------------------------------------------------------------------------------------
        public bool UpdateBuildTimestamp()
        {
            bool rReturn = false;
            // Get saved App version from pref
            int tBuildTimeStamp = NWDAppConfiguration.SharedInstance().SelectedEnvironment().BuildTimestamp;
            int tBuildTimeStampActual = BTBPrefsManager.ShareInstance().getInt("APP_VERSION");
            // test version
            if (tBuildTimeStamp > tBuildTimeStampActual)
            {
                rReturn = true;
                // delete all sync of data 
                foreach (Type tType in NWDDataManager.SharedInstance().mTypeNotAccountDependantList)
                {
                    NWDAliasMethod.InvokeClassMethod(tType, NWDConstants.M_SynchronizationUpadteTimestamp);
                }
                BTBPrefsManager.ShareInstance().set("APP_VERSION", tBuildTimeStamp);
            }
            // Save App version in pref for futur used
            if (rReturn == true)
            {
                //Debug.Log("#DATABASE# Database must upadte by bundle");
            }
            else
            {
                //Debug.Log("#DATABASE# Database is ok (no update needed)");
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void CreateAllTablesLocal()
        {
            if (kConnectedToDatabase == true && kConnectedToDatabaseIsProgress == false)
            {
                foreach (Type tType in mTypeList)
                {
                    NWDAliasMethod.InvokeClassMethod(tType, NWDConstants.M_CreateTable);
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void CleanAllTablesLocal()
        {
            if (kConnectedToDatabase == true && kConnectedToDatabaseIsProgress == false)
            {
                foreach (Type tType in mTypeList)
                {
                    NWDAliasMethod.InvokeClassMethod(tType, NWDConstants.M_CleanTable);
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void PurgeAllTablesLocal()
        {
            if (kConnectedToDatabase == true && kConnectedToDatabaseIsProgress == false)
            {
                foreach (Type tType in mTypeList)
                {
                    NWDAliasMethod.InvokeClassMethod(tType, NWDConstants.M_PurgeTable);
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void UpdateAllTablesLocal()
        {
            if (kConnectedToDatabase == true && kConnectedToDatabaseIsProgress == false)
            {
                foreach (Type tType in mTypeList)
                {
                    NWDAliasMethod.InvokeClassMethod(tType, NWDConstants.M_UpdateDataTable);
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void ResetAllTablesLocal()
        {
            if (kConnectedToDatabase == true && kConnectedToDatabaseIsProgress == false)
            {
                foreach (Type tType in mTypeList)
                {
                    NWDAliasMethod.InvokeClassMethod(tType, NWDConstants.M_ResetTable);
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void CreateTable(Type sType, bool sAccountConnected)
        {
            if (kConnectedToDatabase == true && kConnectedToDatabaseIsProgress == false)
            {
                if (sAccountConnected)
                {
                    if (SQLiteConnectionAccount != null)
                    {
                        SQLiteConnectionAccount.CreateTableByType(sType);
                    }
                }
                else
                {
                    if (SQLiteConnectionEditor != null)
                    {
                        SQLiteConnectionEditor.CreateTableByType(sType);
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void MigrateTable(Type sType, bool sAccountConnected)
        {
            if (kConnectedToDatabase == true && kConnectedToDatabaseIsProgress == false)
            {
                if (sAccountConnected)
                {
                    if (SQLiteConnectionAccount != null)
                    {
                        SQLiteConnectionAccount.MigrateTableByType(sType);
                    }
                }
                else
                {
                    if (SQLiteConnectionEditor != null)
                    {
                        SQLiteConnectionEditor.MigrateTableByType(sType);
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void EmptyTable(Type sType, bool sAccountConnected)
        {
            if (kConnectedToDatabase == true && kConnectedToDatabaseIsProgress == false)
            {
                if (sAccountConnected)
                {
                    if (SQLiteConnectionAccount != null)
                    {
                        SQLiteConnectionAccount.TruncateTableByType(sType);
                    }
                }
                else
                {
                    if (SQLiteConnectionEditor != null)
                    {
                        SQLiteConnectionEditor.TruncateTableByType(sType);
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void DropTable(Type sType, bool sAccountConnected)
        {
            if (kConnectedToDatabase == true && kConnectedToDatabaseIsProgress == false)
            {
                if (sAccountConnected)
                {
                    if (SQLiteConnectionAccount != null)
                    {
                        SQLiteConnectionAccount.DropTableByType(sType);
                    }
                }
                else
                {
                    if (SQLiteConnectionEditor != null)
                    {
                        SQLiteConnectionEditor.DropTableByType(sType);
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void ReInitializeTable(Type sType, bool sAccountConnected)
        {
                EmptyTable(sType, sAccountConnected);
        }
        //-------------------------------------------------------------------------------------------------------------
        public void ResetTable(Type sType, bool sAccountConnected)
        {
            DropTable(sType, sAccountConnected);
            CreateTable(sType, sAccountConnected);
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================