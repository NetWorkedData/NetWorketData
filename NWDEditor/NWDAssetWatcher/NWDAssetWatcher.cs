﻿//=====================================================================================================================
//
//  ideMobi 2020©
//
//=====================================================================================================================
// Define the use of Log and Benchmark only for this file!
// Add NWD_VERBOSE in scripting define symbols (Edit->Project Settings…->Player->[Choose Plateform]->Other Settings->Scripting Define Symbols)
#if NWD_VERBOSE
#if UNITY_EDITOR
#define NWD_LOG
#define NWD_BENCHMARK
#elif DEBUG
//#define NWD_LOG
//#define NWD_BENCHMARK
#endif
#endif
//=====================================================================================================================

#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [InitializeOnLoad]
    public class NWDAssetWatcher : UnityEditor.AssetModificationProcessor
    {
        //-------------------------------------------------------------------------------------------------------------
        static List<string> kExtensionsWatchedList = new List<string>() {
            ".prefab",
            ".tga",
            ".mat",
            ".jpg",
            ".png",
            ".jpeg",
            ".mp3",
            ".mp4",
            ".aiff",
            ".aif",
            ".jpeg",
        };
        //-------------------------------------------------------------------------------------------------------------
        static NWDAssetWatcher() {}
        //-------------------------------------------------------------------------------------------------------------
        static bool ContaintsResource(string sOldPath)
        {
            bool tMove = false;
            if (AssetDatabase.IsValidFolder(sOldPath))
            {
                // ok is a folder
                if (sOldPath.Contains(NWD.K_Resources))
                {
                    tMove = true;
                }
                else
                {
                    foreach (string tFile in Directory.GetFiles(sOldPath))
                    {
                        string tExtension = Path.GetExtension(tFile);
                        if (kExtensionsWatchedList.Contains(tExtension.ToLower()))
                        {
                            tMove = true;
                            break;
                        }
                    }
                    if (tMove == false)
                    {
                        foreach (string tFolder in Directory.GetDirectories(sOldPath))
                        {
                            if (ContaintsResource(tFolder))
                            {
                                tMove = true;
                                break;
                            }
                        }
                    }
                }

            }
            else
            {
                string tExtension = Path.GetExtension(sOldPath);
                if (kExtensionsWatchedList.Contains(tExtension.ToLower()))
                {
                    tMove = true;
                }
            }
            return tMove;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static AssetMoveResult OnWillMoveAsset(string sOldPath, string sNewPath)
        {
            //NWEBenchmark.Start();
            //Debug.Log("sOldPath = " + sOldPath + " to sNewPath " + sNewPath);
            AssetMoveResult rReturn = AssetMoveResult.DidNotMove;
            if (ContaintsResource(sOldPath) == true)
            {
                NWDDataManager.SharedInstance().ChangeAssetPath(sOldPath, sNewPath);
            }
            //NWEBenchmark.Finish();
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static AssetDeleteResult OnWillDeleteAsset(string sOldPath, RemoveAssetOptions sUnused)
        {
            //NWEBenchmark.Start();
            //Debug.Log("sOldPath = " + sOldPath + " deleted");
            AssetDeleteResult rReturn = AssetDeleteResult.DidNotDelete;
            if (ContaintsResource(sOldPath) == true)
            {
                NWDDataManager.SharedInstance().ChangeAssetPath(sOldPath, string.Empty);
            }
            //NWEBenchmark.Finish();
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif