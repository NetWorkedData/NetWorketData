//=====================================================================================================================
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
#else
#undef NWD_LOG
#undef NWD_BENCHMARK
#endif
//=====================================================================================================================
#if NWD_USER_IDENTITY
using System;
using System.Collections.Generic;
using UnityEngine;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDUserNickname : NWDBasisGameSaveDependent
    {
        //-------------------------------------------------------------------------------------------------------------
        public delegate void SyncNicknameBlock(bool error, NWDOperationResult result = null);
        public SyncNicknameBlock SyncNicknameBlockDelegate;
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserNickname()
        {
            //Debug.Log("NWDUserNickname Constructor");
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserNickname(bool sInsertInNetWorkedData) : base(sInsertInNetWorkedData)
        {
            //Debug.Log("NWDUserNickname Constructor with sInsertInNetWorkedData : " + sInsertInNetWorkedData.ToString()+"");
        }
        //-------------------------------------------------------------------------------------------------------------
        public static string Enrichment(string sText, bool sBold = true)
        {
            // Get First nickname found and return a new string
            return Enrichment(sText, NWDBasisHelper.GetReachableFirstData<NWDUserNickname>(), sBold);
        }
        //-------------------------------------------------------------------------------------------------------------
        //TODO : clean
        public static string Enrichment(string sText, NWDUserNickname sNickname, bool sBold = true)
        {
            string rText = sText;
            string tBstart = "<b>";
            string tBend = "</b>";
            if (sBold == false)
            {
                tBstart = string.Empty;
                tBend = string.Empty;
            }
            /*if (sLanguage == null)
            {
                sLanguage = NWDDataManager.SharedInstance().PlayerLanguage;
            }*/
            // Replace the nickname
            string tNickname = string.Empty;
            string tNicknameID = string.Empty;
            if (sNickname != null)
            {
                tNickname = sNickname.Nickname;
                tNicknameID = sNickname.UniqueNickname;
            }
            // Replace the old tag nickname
            //rText = rText.Replace("@nickname@", tBstart + tNickname + tBend);
            //rText = rText.Replace("@nicknameid@", tBstart + tNicknameID + tBend);
            // Replace the new tag nickname
            rText = rText.Replace("#AccountNickname#", tBstart + tNickname + tBend);
            rText = rText.Replace("#UniqueAccountNickname#", tBstart + tNicknameID + tBend);
            //// Replace the standard tag nickname
            //rText = rText.Replace("{Nickname}", tBstart + tNickname + tBend);
            //rText = rText.Replace("{UniqueNickname}", tBstart + tNicknameID + tBend);
            return rText;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static NWDUserNickname NewNickname(string name)
        {
            NWDUserNickname rNickname = NWDBasisHelper.NewData<NWDUserNickname>();
            rNickname.InternalKey = name;
            rNickname.InternalDescription = string.Empty;
            rNickname.Nickname = name;
            rNickname.UpdateData();
            return rNickname;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static string GetNickname()
        {
            string rNickname = string.Empty;
            NWDUserNickname[] tNickname = NWDBasisHelper.GetReachableDatas<NWDUserNickname>();
            if (tNickname.Length > 0)
            {
                rNickname = tNickname[0].Nickname;
            }
            return rNickname;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static string GetUniqueNickname()
        {
            string rUniqueNickname = string.Empty;
            NWDUserNickname[] tNickname = NWDBasisHelper.GetReachableDatas<NWDUserNickname>();
            if (tNickname.Length > 0)
            {
                rUniqueNickname = tNickname[0].UniqueNickname;
            }
            return rUniqueNickname;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void SyncNickname()
        {
            // Sync with the server
            List<Type> tList = new List<Type>
            {
                typeof(NWDUserNickname)
            };

            NWEOperationBlock tSuccess = delegate (NWEOperation bOperation, float bProgress, NWEOperationResult bInfos)
            {
                if (SyncNicknameBlockDelegate != null)
                {
                    SyncNicknameBlockDelegate(false);
                }
            };
            NWEOperationBlock tFailed = delegate (NWEOperation bOperation, float bProgress, NWEOperationResult bInfos)
            {
                NWDOperationResult tInfos = bInfos as NWDOperationResult;
                if (SyncNicknameBlockDelegate != null)
                {
                    SyncNicknameBlockDelegate(true, tInfos);
                }
            };
            NWDDataManager.SharedInstance().AddWebRequestSynchronizationWithBlock(tList, tSuccess, tFailed);
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif
