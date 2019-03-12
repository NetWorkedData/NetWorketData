﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using UnityEngine;
using SQLite.Attribute;
using System;
using System.Collections.Generic;
using BasicToolBox;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[NWDClassServerSynchronize(true)]
    [NWDClassTrigramme("UTF")]
    [NWDClassDescription("User Trade Finder descriptions Class")]
    [NWDClassMenuName("User Trade Finder")]
    public partial class NWDUserTradeFinder : NWDBasis<NWDUserTradeFinder>
    {
        //-------------------------------------------------------------------------------------------------------------
        [NWDGroupStart("Trade Detail", true, true, true)]
        [Indexed("AccountIndex", 0)]
        public NWDReferenceType<NWDAccount> Account
        {
            get; set;
        }
        public NWDReferenceType<NWDGameSave> GameSave
        {
            get; set;
        }
        public NWDReferenceType<NWDTradePlace> TradePlace
        {
            get; set;
        }
        [NWDAlias("ForRelationshipOnly")]
        public bool ForRelationshipOnly
        {
            get; set;
        }
        [NWDGroupEnd]

        [NWDGroupStart("Filters", true, true, true)]
        public NWDReferencesListType<NWDItem> FilterItems
        {
            get; set;
        }
        public NWDReferencesListType<NWDWorld> FilterWorlds
        {
            get; set;
        }
        public NWDReferencesListType<NWDCategory> FilterCategories
        {
            get; set;
        }
        public NWDReferencesListType<NWDFamily> FilterFamilies
        {
            get; set;
        }
        public NWDReferencesListType<NWDKeyword> FilterKeywords
        {
            get; set;
        }
        [NWDGroupEnd]

        [NWDGroupStart("Results", true, true, true)]
        [NWDAlias("TradeRequestsList")]
        public NWDReferencesListType<NWDUserTradeRequest> TradeRequestsList
        {
            get; set;
        }
        //[NWDGroupEnd]
        //-------------------------------------------------------------------------------------------------------------
        public delegate void tradeFinderBlock(bool error, NWDOperationResult result);
        public tradeFinderBlock tradeFinderBlockDelegate;
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserTradeFinder()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserTradeFinder(bool sInsertInNetWorkedData) : base(sInsertInNetWorkedData)
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void Initialization()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        [NWDAliasMethod(NWDConstants.M_OverrideClasseInThisSync)]
        public static List<Type> OverrideClasseInThisSync()
        {
            return new List<Type> { typeof(NWDUserOwnership), typeof(NWDTradePlace), typeof(NWDUserTradeRequest), typeof(NWDUserTradeProposition), typeof(NWDUserTradeFinder) };
        }
        //-------------------------------------------------------------------------------------------------------------
        public static NWDUserTradeRequest[] FindPropositionsWith(NWDTradePlace sTradePlace)
        {
            NWDUserTradeFinder[] tUserTradesFinder = FindDatas();
            foreach (NWDUserTradeFinder k in tUserTradesFinder)
            {
                if (k.TradePlace.GetReference().Equals(sTradePlace.Reference))
                {
                    return k.TradeRequestsList.GetObjectsAbsolute();
                }
            }

            CreateTradeFinderWith(sTradePlace);

            return new NWDUserTradeRequest[0];
        }
        //-------------------------------------------------------------------------------------------------------------
        public static NWDUserTradeFinder GetTradeFinderWith(NWDTradePlace sTradePlace)
        {
            NWDUserTradeFinder[] tUserTradesFinder = FindDatas();
            foreach (NWDUserTradeFinder k in tUserTradesFinder)
            {
                if (k.TradePlace.GetReference().Equals(sTradePlace.Reference))
                {
                    return k;
                }
            }

            return CreateTradeFinderWith(sTradePlace);
        }
        //-------------------------------------------------------------------------------------------------------------
        private static NWDUserTradeFinder CreateTradeFinderWith(NWDTradePlace sTradePlace)
        {
            // No NWD Finder Object found, we create one
            NWDUserTradeFinder rFinder = NewData();
#if UNITY_EDITOR
            rFinder.InternalKey = NWDUserNickname.GetNickname() + " - " + sTradePlace.InternalKey;
#endif
            rFinder.Tag = NWDBasisTag.TagUserCreated;
            rFinder.TradePlace.SetObject(sTradePlace);
            rFinder.SaveData();

            return rFinder;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void SyncTradeFinder()
        {
            // Clean the Trade Place Finder Result
            CleanResult();

            BTBOperationBlock tSuccess = delegate (BTBOperation bOperation, float bProgress, BTBOperationResult bResult)
            {
                if (tradeFinderBlockDelegate != null)
                {
                    NWDOperationResult tResult = bResult as NWDOperationResult;
                    tradeFinderBlockDelegate(false, tResult);
                }
            };
            BTBOperationBlock tFailed = delegate (BTBOperation bOperation, float bProgress, BTBOperationResult bResult)
            {
                if (tradeFinderBlockDelegate != null)
                {
                    NWDOperationResult tResult = bResult as NWDOperationResult;
                    tradeFinderBlockDelegate(true, tResult);
                }
            };

            SynchronizationFromWebService(tSuccess, tFailed);
        }
        //-------------------------------------------------------------------------------------------------------------
        void CleanResult()
        {
            TradeRequestsList.Flush();
            SaveData();

            // Remove stranger data request
            NWDUserTradeRequest.PurgeTable();
        }
        //-------------------------------------------------------------------------------------------------------------
#if UNITY_EDITOR
        //-------------------------------------------------------------------------------------------------------------
        [NWDAliasMethod(NWDConstants.M_AddonPhpPreCalculate)]
        public static string AddonPhpPreCalculate(NWDAppEnvironment AppEnvironment)
        {

            string tTradeStatus = NWDUserTradeRequest.FindAliasName("TradeStatus");
            string tLimitDayTime = NWDUserTradeRequest.FindAliasName("LimitDayTime");
            string tTradePlaceRequest = NWDUserTradeRequest.FindAliasName("TradePlace");
            string tForRelationshipOnly = NWDUserTradeRequest.FindAliasName("ForRelationshipOnly");
            string tRelationshipAccountReferences = NWDUserTradeRequest.FindAliasName("RelationshipAccountReferences");
            int tIndex_tTradeStatus = NWDUserTradeRequest.CSV_IndexOf(tTradeStatus);

            string t_THIS_TradeRequestsList = FindAliasName("TradeRequestsList");
            string t_THIS_TradePlace = FindAliasName("TradePlace");
            string t_THIS_ForRelationshipOnly = FindAliasName("ForRelationshipOnly");

            int tIndex_TradeRequestsList = CSV_IndexOf(t_THIS_TradeRequestsList);
            int tIndex_TradePlace = CSV_IndexOf(t_THIS_TradePlace);
            int tIndex_THIS_ForRelationshipOnly = CSV_IndexOf(t_THIS_ForRelationshipOnly);

            int tDelayOfRefresh = 5; // minutes before stop to get the datas!
            string sScript = "" +
                "// start Addon \n" +
                "include_once($PATH_BASE.'/'.$ENV.'/" + NWD.K_DB + "/" + NWDUserTradeRequest.BasisHelper().ClassNamePHP + "/" + NWD.K_WS_SYNCHRONISATION + "');\n" +
                "$tQueryExpired = 'SELECT " + NWDUserTradeRequest.SLQSelect() + " FROM `'.$ENV.'_" + NWDUserTradeRequest.BasisHelper().ClassNamePHP + "` " +
                "WHERE `AC`= \\'1\\' " +
                "AND `" + tTradeStatus + "` = \\'" + ((int)NWDTradeStatus.Waiting).ToString() + "\\' " +
                "AND `" + tLimitDayTime + "` < '.$TIME_SYNC.' " +
                "AND `WebModel` <= '.$WSBUILD.' " +
                "LIMIT 0, 100;';\n" +
                "myLog('tQueryExpired : '. $tQueryExpired, __FILE__, __FUNCTION__, __LINE__);\n" +
                "$tResultExpired = $SQL_CON->query($tQueryExpired);\n" +
                "if (!$tResultExpired)\n" +
                "{\n" +
                "myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tQueryExpired.'', __FILE__, __FUNCTION__, __LINE__);\n" +
                "error('UTRFx31');\n" +
                "}\n" +
                "else\n" +
                "{\n" +
                "while($tRowExpired = $tResultExpired->fetch_row())\n" +
                "{\n" +
                "myLog('tReferences need be cancelled : '. $tRowExpired[0], __FILE__, __FUNCTION__, __LINE__);\n" +
                "$tRowExpired = Integrity" + NWDUserTradeRequest.BasisHelper().ClassNamePHP + "Replace ($tRowExpired," + tIndex_tTradeStatus + ", " + ((int)NWDTradeStatus.Cancel).ToString() + ");\n" +
                "$tRowExpired = implode('" + NWDConstants.kStandardSeparator + "',$tRowExpired);\n" +
                "UpdateData" + NWDUserTradeRequest.BasisHelper().ClassNamePHP + " ($tRowExpired, $TIME_SYNC, $uuid, false);\n" +
                "}\n" +
                //"mysqli_free_result($tResultExpired);\n" +
                "}\n" +

                "$tQueryTrade = 'SELECT `Reference` FROM `'.$ENV.'_" + NWDUserTradeRequest.BasisHelper().ClassNamePHP + "` " +
                // WHERE REQUEST
                "WHERE `AC`= \\'1\\' " +
                "AND `Account` != \\''.$SQL_CON->real_escape_string($uuid).'\\' " +
                "AND `" + tTradeStatus + "` = \\'" + ((int)NWDTradeStatus.Waiting).ToString() + "\\' " +
                "AND `" + tForRelationshipOnly + "` = \\''.$sCsvList[" + tIndex_THIS_ForRelationshipOnly + "].'\\' ';\n" +
                "if ($sCsvList[" + tIndex_THIS_ForRelationshipOnly + "] == '1')\n" +
                "{\n" +
                "$tQueryTrade.= 'AND `" + tRelationshipAccountReferences + "` = \\''.$uuid.'\\' ';\n" +
                "}\n" +
                "$tQueryTrade.= '" +
                "AND `" + tTradePlaceRequest + "` = \\''.$sCsvList[" + tIndex_TradePlace + "].'\\' " +
                "AND `" + tLimitDayTime + "` > '.($TIME_SYNC+" + (tDelayOfRefresh * 60).ToString() + ").' " +
                // ORDER BY 
                //"ORDER BY `" + tLimitDayTime + "` " +
                // END WHERE REQUEST LIMIT START
                "LIMIT 0, 100;';\n" +
                "myLog('tQueryTrade : '. $tQueryTrade, __FILE__, __FUNCTION__, __LINE__);\n" +
                "$tResultTrade = $SQL_CON->query($tQueryTrade);\n" +
                "$tReferences = \'\';\n" +
                "$tReferencesList = \'\';\n" +
                "if (!$tResultTrade)\n" +
                "{\n" +
                "myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tQueryTrade.'', __FILE__, __FUNCTION__, __LINE__);\n" +
                "error('UTRFx31');\n" +
                "}\n" +
                "else\n" +
                "{\n" +
                "while($tRowTrade = $tResultTrade->fetch_assoc())\n" +
                "{\n" +
                "myLog('tReferences found : '. $tRowTrade['Reference'], __FILE__, __FUNCTION__, __LINE__);\n" +
                "$tReferences[]=$tRowTrade['Reference'];\n" +
                "}\n" +
                //"mysqli_free_result($tRowTrade);\n" +
                "if (is_array($tReferences))\n" +
                "{\n" +
                "$tReferencesList = implode('" + NWDConstants.kFieldSeparatorA + "',$tReferences);\n" +
                "GetDatas" + NWDUserTradeRequest.BasisHelper().ClassNamePHP + "ByReferences ($tReferences);\n" +
                "}\n" +
                "}\n" +
                "myLog('tReferencesList : '. $tReferencesList, __FILE__, __FUNCTION__, __LINE__);\n" +
                "$sCsvList = Integrity" + BasisHelper().ClassNamePHP + "Replace ($sCsvList, " + tIndex_TradeRequestsList.ToString() + ", $tReferencesList);\n" +
                "// finish Addon \n";

            return sScript;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static string AddonPhpPostCalculate(NWDAppEnvironment AppEnvironment)
        {
            return "\n" +
                "\n";
        }
        //-------------------------------------------------------------------------------------------------------------
        public static string AddonPhpSpecialCalculate(NWDAppEnvironment AppEnvironment)
        {
            return "// write your php script here to special operation, example : \n$REP['" + BasisHelper().ClassName + " Special'] ='success!!!';\n";
        }
        //-------------------------------------------------------------------------------------------------------------
#endif
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================