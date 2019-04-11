﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

#if UNITY_EDITOR
using SQLite.Attribute;
using UnityEngine;
using BasicToolBox;
using System.Collections.Generic;
using System;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
public partial class NWDUserTradePropositionHelper : NWDHelper<NWDUserTradeProposition>
    {
        //-------------------------------------------------------------------------------------------------------------
        public override string New_AddonPhpPreCalculate(NWDAppEnvironment AppEnvironment)
        {
            string tTradeHash = NWDToolbox.PropertyName(() => NWDUserTradeRequest.FictiveData().TradeHash);
            string tTradeStatus = NWDToolbox.PropertyName(() => NWDUserTradeRequest.FictiveData().TradeStatus);
            string tLimitDayTime = NWDToolbox.PropertyName(() => NWDUserTradeRequest.FictiveData().LimitDayTime);
            string tTradePlace = NWDToolbox.PropertyName(() => NWDUserTradeRequest.FictiveData().TradePlace);
            //string tTradeRequest = NWDToolbox.PropertyName(() => NWDUserTradeRequest.FictiveData().TradeRequest);
            string tWinnerProposition = NWDToolbox.PropertyName(() => NWDUserTradeRequest.FictiveData().WinnerProposition);

            string t_THIS_TradeRequestHash = NWDToolbox.PropertyName(() => FictiveData().TradeRequestHash);
            string t_THIS_TradePlace = NWDToolbox.PropertyName(() => FictiveData().TradePlace);
            string t_THIS_TradeRequest = NWDToolbox.PropertyName(() => FictiveData().TradeRequest);
            string t_THIS_TradeStatus = NWDToolbox.PropertyName(() => FictiveData().TradeStatus);
            string t_THIS_ItemsProposed = NWDToolbox.PropertyName(() => FictiveData().ItemsProposed);
            string t_THIS_ItemsAsked = NWDToolbox.PropertyName(() => FictiveData().ItemsAsked);


            //string tTradeHash = NWDUserTradeRequest.FindAliasName("TradeHash");
            //string tTradeStatus = NWDUserTradeRequest.FindAliasName("TradeStatus");
            //string tLimitDayTime = NWDUserTradeRequest.FindAliasName("LimitDayTime");
            //string tTradePlace = NWDUserTradeRequest.FindAliasName("TradePlace");
            //string tTradeRequest = NWDUserTradeRequest.FindAliasName("TradeRequest");
            //string tWinnerProposition = NWDUserTradeRequest.FindAliasName("WinnerProposition");

            //string t_THIS_TradeRequestHash = FindAliasName("TradeRequestHash");
            //string t_THIS_TradePlace = FindAliasName("TradePlace");
            //string t_THIS_TradeRequest = FindAliasName("TradeRequest");
            //string t_THIS_TradeStatus = FindAliasName("TradeStatus");
            int t_THIS_Index_tTradeRequestHash = CSV_IndexOf(t_THIS_TradeRequestHash);
            int t_THIS_Index_TradePlace = CSV_IndexOf(t_THIS_TradePlace);
            int t_THIS_Index_TradeRequest = CSV_IndexOf(t_THIS_TradeRequest);
            int t_THIS_Index_TradeStatus = CSV_IndexOf(t_THIS_TradeStatus);
            //string t_THIS_ItemsProposed = FindAliasName("ItemsProposed");
            int t_THIS_Index_ItemsProposed = CSV_IndexOf(t_THIS_ItemsProposed);
            //string t_THIS_ItemsAsked = FindAliasName("ItemsAsked");
            int t_THIS_Index_ItemsAsked = CSV_IndexOf(t_THIS_ItemsAsked);
            string sScript = "" +
                "// start Addon \n" +
                "include_once ( $PATH_BASE.'/'.$ENV.'/" + NWD.K_DB + "/" + NWDUserTradeRequest.BasisHelper().ClassNamePHP + "/" + NWD.K_WS_SYNCHRONISATION + "');\n" +
                // get the actual state
                "$tServerStatut = " + ((int)NWDTradeStatus.None).ToString() + ";\n" +
                "$tServerRequestHash = '';\n" +
                "$tQueryStatus = 'SELECT `" + t_THIS_TradeStatus + "`, `" + t_THIS_TradeRequestHash + "` FROM `'.$ENV.'_" + ClassNamePHP + "` " +
                "WHERE " +
                "`Reference` = \\''.$SQL_CON->real_escape_string($tReference).'\\';';\n" +
                "$tResultStatus = $SQL_CON->query($tQueryStatus);\n" +
                "if (!$tResultStatus)\n" +
                "{\n" +
                "myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tResultStatus.'', __FILE__, __FUNCTION__, __LINE__);\n" +
                "error('SERVER');\n" +
                "}\n" +
                "else" +
                "{\n" +
                "if ($tResultStatus->num_rows == 1)\n" +
                "{\n" +
                "$tRowStatus = $tResultStatus->fetch_assoc();\n" +
                "$tServerStatut = $tRowStatus['" + t_THIS_TradeStatus + "'];\n" +
                "$tServerRequestHash = $tRowStatus['" + t_THIS_TradeRequestHash + "'];\n" +
                "}\n" +
                "}\n" +
                // change the statut from CSV TO WAITING, ACCEPTED, CANCEL, EXPIRED
                "if ($sCsvList[" + t_THIS_Index_TradeStatus + "] == " + ((int)NWDTradeStatus.Waiting).ToString() +
                " || $sCsvList[" + t_THIS_Index_TradeStatus + "] == " + ((int)NWDTradeStatus.Accepted).ToString() +
                " || $sCsvList[" + t_THIS_Index_TradeStatus + "] == " + ((int)NWDTradeStatus.Cancel).ToString() +
                " || $sCsvList[" + t_THIS_Index_TradeStatus + "] == " + ((int)NWDTradeStatus.Deal).ToString() +
                " || $sCsvList[" + t_THIS_Index_TradeStatus + "] == " + ((int)NWDTradeStatus.NoDeal).ToString() +
                " || $sCsvList[" + t_THIS_Index_TradeStatus + "] == " + ((int)NWDTradeStatus.Refresh).ToString() +
                " || $sCsvList[" + t_THIS_Index_TradeStatus + "] == " + ((int)NWDTradeStatus.Cancelled).ToString() +
                " || $sCsvList[" + t_THIS_Index_TradeStatus + "] == " + ((int)NWDTradeStatus.Expired).ToString() + ")\n" +
                "{\n" +
                //"Integrity" + Datas().ClassNamePHP + "Reevalue ($tReference);\n" +
                "GetDatas" + ClassNamePHP + "ByReference ($tReference);\n" +
                "return;\n" +
                "}\n" +
                // change the statut from CSV TO NONE 
                "else if ($sCsvList[" + t_THIS_Index_TradeStatus + "] == " + ((int)NWDTradeStatus.None).ToString() + " && " +
                "($tServerStatut == " + ((int)NWDTradeStatus.Accepted).ToString() +
                //" || $tServerStatut == " + ((int)NWDTradeStatus.Cancelled).ToString() + 
                " || $tServerStatut == " + ((int)NWDTradeStatus.Expired).ToString() +
                " || ($tServerStatut == " + ((int)NWDTradeStatus.Force).ToString() + " && $sAdmin == true)" +
                "))\n" +
                "{\n" +
                "$sReplaces[" + t_THIS_Index_ItemsProposed + "]='';\n" +
                "$sReplaces[" + t_THIS_Index_ItemsAsked + "]='';\n" +
                "$sReplaces[" + t_THIS_Index_tTradeRequestHash + "]='';\n" +
                "$sReplaces[" + t_THIS_Index_TradeRequest + "]='';\n" +
                "$sCsvList = Integrity" + ClassNamePHP + "Replaces ($sCsvList, $sReplaces);\n" +
                "}\n" +
                // change the statut from CSV TO ACTIVE 
                "else if ($sCsvList[" + t_THIS_Index_TradeStatus + "] == " + ((int)NWDTradeStatus.Submit).ToString() + " && " +
                "$tServerStatut == " + ((int)NWDTradeStatus.None).ToString() + ")\n" +
                "{\n" +
                "$tQueryTrade = 'UPDATE `'.$ENV.'_" + NWDUserTradeRequest.BasisHelper().ClassNamePHP + "` SET " +
                " `DM` = \\''.$TIME_SYNC.'\\'," +
                " `DS` = \\''.$TIME_SYNC.'\\'," +
                " `'.$ENV.'Sync` = \\''.$TIME_SYNC.'\\'," +
                " `" + tWinnerProposition + "` = \\''.$sCsvList[0].'\\'," +
                " `" + tTradeStatus + "` = \\'" + ((int)NWDTradeStatus.Accepted).ToString() + "\\'" +
                " WHERE `AC`= \\'1\\' " +
                " AND `" + tTradeStatus + "` = \\'" + ((int)NWDTradeStatus.Waiting).ToString() + "\\' " +
                " AND `" + tTradePlace + "` = \\''.$sCsvList[" + t_THIS_Index_TradePlace + "].'\\' " +
                " AND `Reference` = \\''.$sCsvList[" + t_THIS_Index_TradeRequest + "].'\\' " +
                " AND `" + tTradeHash + "` = \\''.$sCsvList[" + t_THIS_Index_tTradeRequestHash + "].'\\' " +
                " AND `" + tLimitDayTime + "` > '.$TIME_SYNC.' " +
                "';\n" +
                "myLog('tQueryTrade : '. $tQueryTrade, __FILE__, __FUNCTION__, __LINE__);\n" +
                "$tResultTrade = $SQL_CON->query($tQueryTrade);\n" +
                "$tReferences = \'\';\n" +
                "$tReferencesList = \'\';\n" +
                "if (!$tResultTrade)\n" +
                "{\n" +
                "myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tQueryTrade.'', __FILE__, __FUNCTION__, __LINE__);\n" +
                "error('SERVER');\n" +
                "}\n" +
                "else\n" +
                "{\n" +
                "$tNumberOfRow = 0;\n" +
                "$tNumberOfRow = $SQL_CON->affected_rows;\n" +
                "if ($tNumberOfRow == 1)\n" +
                "{\n" +
                "// I need update the proposition too !\n" +
                "$sCsvList = Integrity" + ClassNamePHP + "Replace ($sCsvList, " + t_THIS_Index_TradeStatus + ", \'" + ((int)NWDTradeStatus.Accepted).ToString() + "\');\n" +
                "myLog('I need update the proposition accept', __FILE__, __FUNCTION__, __LINE__);\n" +
                "Integrity" + NWDUserTradeRequest.BasisHelper().ClassNamePHP + "Reevalue ($sCsvList[" + t_THIS_Index_TradeRequest + "]);\n" +
                "}\n" +
                "else\n" +
                "{\n" +
                "$sCsvList = Integrity" + ClassNamePHP + "Replace ($sCsvList, " + t_THIS_Index_TradeStatus + ", \'" + ((int)NWDTradeStatus.Expired).ToString() + "\');\n" +
                "\tmyLog('I need update the proposition refused ... too late!', __FILE__, __FUNCTION__, __LINE__);\n" +
                "}\n" +
                "GetDatas" + NWDUserTradeRequest.BasisHelper().ClassNamePHP + "ByReference ($sCsvList[" + t_THIS_Index_TradeRequest + "]);\n" +
                "}\n" +
                "}\n" +

                // change the statut from CSV TO FORCE // ADMIN ONLY 
                "else if ($sCsvList[" + t_THIS_Index_TradeStatus + "] == " + ((int)NWDTradeStatus.Force).ToString() + " && $sAdmin == true)\n" +
                    "{\n" +
                    "//EXECEPTION FOR ADMIN\n" +
                    "}\n" +

                // OTHER
                "else\n" +
                "{\n" +
                // not possible return preview value
                //"Integrity" + Datas().ClassNamePHP + "Reevalue ($tReference);\n" +
                "GetDatas" + ClassNamePHP + "ByReference ($tReference);\n" +
                "return;\n" +
                "}\n" +
                "// finish Addon \n";

            return sScript;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override string New_AddonPhpPostCalculate(NWDAppEnvironment AppEnvironment)
        {
            string t_THIS_TradeRequest = NWDToolbox.PropertyName(() => FictiveData().TradeRequest);
            //string t_THIS_TradeRequest = FindAliasName("TradeRequest");
            int t_THIS_Index_TradeRequest = CSV_IndexOf(t_THIS_TradeRequest);
            return "// write your php script here to update after sync on server\n " +
                "GetDatas" + NWDUserTradeRequest.BasisHelper().ClassNamePHP + "ByReference ($sCsvList[" + t_THIS_Index_TradeRequest + "]);\n";
        }
        //-------------------------------------------------------------------------------------------------------------
        public override string New_AddonPhpSpecialCalculate(NWDAppEnvironment AppEnvironment)
        {
            return "// write your php script here to special operation, example : \n$REP['" + ClassName + " Special'] ='success!!!';\n";
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif