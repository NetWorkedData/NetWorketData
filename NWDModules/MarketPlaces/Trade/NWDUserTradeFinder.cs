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
    /// <summary>
    /// 
    /// </summary>
	[NWDClassServerSynchronize(true)]
    [NWDClassTrigramme("UTRF")]
    [NWDClassDescription("User Trade Finder descriptions Class")]
    [NWDClassMenuName("User Trade Finder")]
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDUserTradeFinder : NWDBasis<NWDUserTradeFinder>
    {
        //-------------------------------------------------------------------------------------------------------------
        #region Properties
        //-------------------------------------------------------------------------------------------------------------
        [NWDGroupStart("Trade Detail", true, true, true)]
        [Indexed("AccountIndex", 0)]
        public NWDReferenceType<NWDAccount> Account { get; set; }
        public NWDReferenceType<NWDGameSave> GameSave { get; set; }
        public NWDReferenceType<NWDTradePlace> TradePlace { get; set; }
        [NWDGroupEnd]

        [NWDGroupSeparator]

        [NWDGroupStart("Filters", true, true, true)]
        public NWDReferencesListType<NWDItem> FilterItems { get; set; }
        public NWDReferencesListType<NWDWorld> FilterWorlds { get; set; }
        public NWDReferencesListType<NWDCategory> FilterCategories { get; set; }
        public NWDReferencesListType<NWDFamily> FilterFamilies { get; set; }
        public NWDReferencesListType<NWDKeyword> FilterKeywords { get; set; }
        [NWDGroupEnd]

        [NWDGroupSeparator]

        [NWDGroupStart("Results", true, true, true)]
        [NWDAlias("TradeRequestsList")]
        public NWDReferencesListType<NWDUserTradeRequest> TradeRequestsList { get; set; }
        //[NWDGroupEnd]
        //-------------------------------------------------------------------------------------------------------------
        public delegate void tradeFinderBlock(bool result, NWDOperationResult infos);
        public tradeFinderBlock tradeFinderBlockDelegate;
        //-------------------------------------------------------------------------------------------------------------
        #endregion
        //-------------------------------------------------------------------------------------------------------------
        #region Constructors
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserTradeFinder()
        {

        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserTradeFinder(bool sInsertInNetWorkedData) : base(sInsertInNetWorkedData)
        {

        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion
        //-------------------------------------------------------------------------------------------------------------
        #region Class methods
        //-------------------------------------------------------------------------------------------------------------
        public override void Initialization()
        {

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
            NWDUserTradeFinder tFinder = NewData();
            #if UNITY_EDITOR
            tFinder.InternalKey = NWDAccountNickname.GetNickname();
            #endif
            tFinder.Tag = NWDBasisTag.TagUserCreated;
            tFinder.TradePlace.SetObject(sTradePlace);
            tFinder.SaveData();

            return tFinder;
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------
        #region Instance methods
        //-------------------------------------------------------------------------------------------------------------
        public void SyncTradeFinder()
        {
            // Clean the Trade Place Finder Result
            CleanResult();

            List<Type> tLists = new List<Type>() {
                typeof(NWDUserTradeProposition),
                typeof(NWDUserTradeRequest),
                typeof(NWDUserTradeFinder),
            };

            BTBOperationBlock tSuccess = delegate (BTBOperation bOperation, float bProgress, BTBOperationResult bInfos)
            {
                if (tradeFinderBlockDelegate != null)
                {
                    tradeFinderBlockDelegate(true, null);
                }
            };
            BTBOperationBlock tFailed = delegate (BTBOperation bOperation, float bProgress, BTBOperationResult bInfos)
            {
                if (tradeFinderBlockDelegate != null)
                {
                    NWDOperationResult tInfos = bInfos as NWDOperationResult;
                    tradeFinderBlockDelegate(false, tInfos);
                }
            };
            NWDDataManager.SharedInstance().AddWebRequestSynchronizationWithBlock(tLists, tSuccess, tFailed);
        }
        //-------------------------------------------------------------------------------------------------------------
        private void CleanResult()
        {
            TradeRequestsList = null;
            SaveData();

            // Remove stranger data request
            NWDUserTradeRequest.PurgeTable();
        }
        //-------------------------------------------------------------------------------------------------------------
        #region NetWorkedData addons methods
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonInsertMe()
        {
            // do something when object will be inserted
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonUpdateMe()
        {
            // do something when object will be updated
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonUpdatedMe()
        {
            // do something when object finish to be updated
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonDuplicateMe()
        {
            // do something when object will be dupplicate
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonEnableMe()
        {
            // do something when object will be enabled
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonDisableMe()
        {
            // do something when object will be disabled
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonTrashMe()
        {
            // do something when object will be put in trash
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonUnTrashMe()
        {
            // do something when object will be remove from trash
        }
        //-------------------------------------------------------------------------------------------------------------
#if UNITY_EDITOR
        //-------------------------------------------------------------------------------------------------------------
        //Addons for Edition
        //-------------------------------------------------------------------------------------------------------------
        public override bool AddonEdited(bool sNeedBeUpdate)
        {
            if (sNeedBeUpdate == true)
            {
                // do something
            }
            return sNeedBeUpdate;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override float AddonEditor(Rect sInRect)
        {
            // Draw the interface addon for editor
            float tYadd = 0.0f;
            return tYadd;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override float AddonEditorHeight()
        {
            // Height calculate for the interface addon for editor
            float tYadd = 0.0f;
            return tYadd;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static string AddonPhpPreCalculate()
        {

            string tTradeStatus = NWDUserTradeRequest.FindAliasName("TradeStatus");
            string tLimitDayTime = NWDUserTradeRequest.FindAliasName("LimitDayTime");
            string tTradePlaceRequest = NWDUserTradeRequest.FindAliasName("TradePlace");

            string tTradeRequestsList = NWDUserTradeFinder.FindAliasName("TradeRequestsList");
            string tTradePlace = NWDUserTradeFinder.FindAliasName("TradePlace");
            int tIndex_TradeRequestsList = CSVAssemblyIndexOf(tTradeRequestsList);
            int tIndex_TradePlace = CSVAssemblyIndexOf(tTradePlace);

            int tDelayOfRefresh = 5; // minutes before stop to get the datas!
            string sScript = "" +
                "// debut find \n" +
                "$tQueryTrade = 'SELECT `Reference` FROM `'.$ENV.'_" + NWDUserTradeRequest.Datas().ClassNamePHP + "` " +
                // WHERE REQUEST
                "WHERE `AC`= \\'1\\' " +
                "AND `Account` != \\''.$SQL_CON->real_escape_string($uuid).'\\' " +
                "AND `" + tTradeStatus + "` = \\'" + ((int)NWDTradeStatus.Active).ToString() + "\\' " +
                "AND `" + tTradePlaceRequest + "` = \\''.$sCsvList[" + tIndex_TradePlace + "].'\\' " +
                "AND `" + tLimitDayTime + "` > '.($TIME_SYNC+"+ (tDelayOfRefresh * 60).ToString() + ").' " +
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
                "if (is_array($tReferences))\n" +
                "{\n" +
                "$tReferencesList = implode('" + NWDConstants.kFieldSeparatorA + "',$tReferences);\n" +
                "global $PATH_BASE;\n" +
                "include_once ( $PATH_BASE.'/Environment/'.$ENV.'/Engine/Database/" + NWDUserTradeRequest.Datas().ClassNamePHP + "/synchronization.php');\n" +
                "GetDatas" + NWDUserTradeRequest.Datas().ClassNamePHP + "ByReferences ($tReferences);\n" +
                "}\n" +
                "}\n" +
                "myLog('tReferencesList : '. $tReferencesList, __FILE__, __FUNCTION__, __LINE__);\n" +
                "$sCsvList = Integrity" + NWDUserTradeFinder.Datas().ClassNamePHP + "Replace ($sCsvList, " + tIndex_TradeRequestsList.ToString() + ", $tReferencesList);\n" +
                "// fin find \n";

            return sScript;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static string AddonPhpPostCalculate()
        {
            return "\n" +
            	"\n";
        }
        //-------------------------------------------------------------------------------------------------------------
        public static string AddonPhpSpecialCalculate()
        {
            return "// write your php script here to special operation, example : \n$REP['" + Datas().ClassName + " Special'] ='success!!!';\n";
        }
        //-------------------------------------------------------------------------------------------------------------
#endif
        //-------------------------------------------------------------------------------------------------------------
        #endregion
        //-------------------------------------------------------------------------------------------------------------
        #endregion
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================