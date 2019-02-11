﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================
#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using SQLite4Unity3d;
using BasicToolBox;
using SQLite.Attribute;
using UnityEditor;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDUserBarterProposition : NWDBasis<NWDUserBarterProposition>
    {
        //-------------------------------------------------------------------------------------------------------------
        [NWDAliasMethod(NWDConstants.M_AddonPhpPreCalculate)]
        public static string AddonPhpPreCalculate(NWDAppEnvironment AppEnvironment)
        {
            string tBarterStatus = NWDUserBarterRequest.FindAliasName("BarterStatus");
            string tLimitDayTime = NWDUserBarterRequest.FindAliasName("LimitDayTime");
            string tBarterPlace = NWDUserBarterRequest.FindAliasName("BarterPlace");
            string tBarterRequest = NWDUserBarterRequest.FindAliasName("BarterRequest");
            string tWinnerProposition = NWDUserBarterRequest.FindAliasName("WinnerProposition");
            string tPropositions = NWDUserBarterRequest.FindAliasName("Propositions");
            string tMaxPropositions = NWDUserBarterRequest.FindAliasName("MaxPropositions");
            string tPropositionsCounter = NWDUserBarterRequest.FindAliasName("PropositionsCounter");
            string tBarterHash = NWDUserBarterRequest.FindAliasName("BarterHash");
            string tItemsProposed = NWDUserBarterRequest.FindAliasName("ItemsProposed");

            string t_THIS_BarterRequestHash = FindAliasName("BarterRequestHash");
            string t_THIS_BarterPlace = FindAliasName("BarterPlace");
            string t_THIS_BarterRequest = FindAliasName("BarterRequest");
            string t_THIS_BarterStatus = FindAliasName("BarterStatus");
            int t_THIS_Index_BarterRequestHash = CSV_IndexOf(t_THIS_BarterRequestHash);
            int t_THIS_Index_BarterPlace = CSV_IndexOf(t_THIS_BarterPlace);
            int t_THIS_Index_BarterRequest = CSV_IndexOf(t_THIS_BarterRequest);
            int t_THIS_Index_BarterStatus = CSV_IndexOf(t_THIS_BarterStatus);

            string t_THIS_ItemsProposed = FindAliasName("ItemsProposed");
            string t_THIS_ItemsSend = FindAliasName("ItemsSend");
            int t_THIS_Index_ItemsProposed = CSV_IndexOf(t_THIS_ItemsProposed);
            int t_THIS_Index_ItemsSend = CSV_IndexOf(t_THIS_ItemsSend);

            string sScript = "" +
                "// debut find \n" +
                "include_once ( $PATH_BASE.'/'.$ENV.'/" + NWD.K_DB + "/" + NWDUserBarterRequest.BasisHelper().ClassNamePHP + "/" + NWD.K_WS_SYNCHRONISATION + "');\n" +
                // get the actual state
                "$tServerStatut = " + ((int)NWDTradeStatus.None).ToString() + ";\n" +
                "$tServerHash = '';\n" +
                "$tQueryStatus = 'SELECT `" + t_THIS_BarterStatus + "`, `" + t_THIS_BarterRequestHash + "` FROM `'.$ENV.'_" + BasisHelper().ClassNamePHP + "` " +
                "WHERE " +
                "`Reference` = \\''.$SQL_CON->real_escape_string($tReference).'\\';';" +
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
                                "$tServerStatut = $tRowStatus['" + t_THIS_BarterStatus + "'];\n" +
                                "$tServerHash = $tRowStatus['" + t_THIS_BarterRequestHash + "'];\n" +
                            "}\n" +
                    "}\n" +

                // change the statut from CSV TO WAITING, ACCEPTED, EXPIRED, DEAL, REFRESH, CANCELLED
                "if ($sCsvList[" + t_THIS_Index_BarterStatus + "] == " + ((int)NWDTradeStatus.Accepted).ToString() +
                " || $sCsvList[" + t_THIS_Index_BarterStatus + "] == " + ((int)NWDTradeStatus.Sync).ToString() +
                " || $sCsvList[" + t_THIS_Index_BarterStatus + "] == " + ((int)NWDTradeStatus.Waiting).ToString() +
                " || $sCsvList[" + t_THIS_Index_BarterStatus + "] == " + ((int)NWDTradeStatus.Deal).ToString() +
                " || $sCsvList[" + t_THIS_Index_BarterStatus + "] == " + ((int)NWDTradeStatus.Expired).ToString() +
                " || $sCsvList[" + t_THIS_Index_BarterStatus + "] == " + ((int)NWDTradeStatus.Refresh).ToString() +
                " || $sCsvList[" + t_THIS_Index_BarterStatus + "] == " + ((int)NWDTradeStatus.Cancelled).ToString() + ")\n" +
                    "{\n" +
                        //"Integrity" + Datas().ClassNamePHP + "Reevalue ($tReference);\n" +
                        "GetDatas" + BasisHelper().ClassNamePHP + "ByReference ($tReference);\n" +
                        "return;\n" +
                    "}\n" +

                // change the statut from CSV TO NONE 
                "else if ($sCsvList[" + t_THIS_Index_BarterStatus + "] == " + ((int)NWDTradeStatus.None).ToString() + " && " +
                "($tServerStatut == " + ((int)NWDTradeStatus.Accepted).ToString() +
                //" || $tServerStatut == " + ((int)NWDTradeStatus.Cancelled).ToString() +
                " || $tServerStatut == " + ((int)NWDTradeStatus.Expired).ToString() +
                " || ($tServerStatut == " + ((int)NWDTradeStatus.Force).ToString() + " && $sAdmin == true)" +
                "))\n" +
                    "{\n" +
                        "$sReplaces[" + t_THIS_Index_ItemsProposed + "]='';\n" +
                        "$sReplaces[" + t_THIS_Index_ItemsSend + "]='';\n" +
                        "$sReplaces[" + t_THIS_Index_BarterRequestHash + "]='';\n" +
                        "$sReplaces[" + t_THIS_Index_BarterRequest + "]='';\n" +
                        "$sCsvList = Integrity" + BasisHelper().ClassNamePHP + "Replaces ($sCsvList, $sReplaces);\n" +
                        "myLog('PUT TO NONE FROM EXPIRED OR ACCEPTED', __FILE__, __FUNCTION__, __LINE__);\n" +
                    "}\n" +

                // change the statut from CSV TO ACTIVE 
                "else if ($sCsvList[" + t_THIS_Index_BarterStatus + "] == " + ((int)NWDTradeStatus.Active).ToString() + " && " +
                "$tServerStatut == " + ((int)NWDTradeStatus.None).ToString() + ")\n" +
                    "{\n" +
                        "$tQueryTrade = 'UPDATE `'.$ENV.'_" + NWDUserBarterRequest.BasisHelper().ClassNamePHP + "` SET " +
                        " `DM` = \\''.$TIME_SYNC.'\\'," +
                        " `DS` = \\''.$TIME_SYNC.'\\'," +
                        " `'.$ENV.'Sync` = \\''.$TIME_SYNC.'\\'," +
                        " `" + tPropositions + "` = TRIM(\\'" + NWDConstants.kFieldSeparatorA + "\\' FROM CONCAT(CONCAT(`" + tPropositions + "`,\\'" + NWDConstants.kFieldSeparatorA + "\\'),\\''.$sCsvList[0].'\\')), " +
                        " `" + tPropositionsCounter + "` = `" + tPropositionsCounter + "`+1 " +
                        " WHERE `AC`= \\'1\\' " +
                        " AND `" + tBarterStatus + "` = \\'" + ((int)NWDTradeStatus.Waiting).ToString() + "\\' " +
                        " AND `" + tBarterPlace + "` = \\''.$sCsvList[" + t_THIS_Index_BarterPlace + "].'\\' " +
                        " AND `Reference` = \\''.$sCsvList[" + t_THIS_Index_BarterRequest + "].'\\' " +
                        " AND `" + tBarterHash + "` = \\''.$sCsvList[" + t_THIS_Index_BarterRequestHash + "].'\\' " +
                        " AND `" + tLimitDayTime + "` > '.$TIME_SYNC.' " +
                        " AND `" + tPropositionsCounter + "` < `" + tMaxPropositions + "` " +
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
                                     //   "$sCsvList = Integrity" + BasisHelper().ClassNamePHP + "Replace ($sCsvList, " + t_THIS_Index_BarterStatus + ", \'" + ((int)NWDTradeStatus.Waiting).ToString() + "\');\n" +

                                        "$tQueryBarterRequest = 'SELECT" +
                                        " `" + tItemsProposed + "`" +
                                        " FROM `'.$ENV.'_" + NWDUserBarterRequest.BasisHelper().ClassNamePHP + "`" +
                                        " WHERE" +
                                        " `Reference` = \\''.$SQL_CON->real_escape_string($sCsvList[" + t_THIS_Index_BarterRequest + "]).'\\';" +
                                        "';\n" +
                                        "myLog('tQueryBarterPlace : '.$tQueryBarterRequest.'', __FILE__, __FUNCTION__, __LINE__);\n" +
                                        "$tResultBarterRequest = $SQL_CON->query($tQueryBarterRequest);\n" +
                                        "if (!$tResultBarterRequest)\n" +
                                            "{\n" +
                                                "myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tQueryBarterRequest.'', __FILE__, __FUNCTION__, __LINE__);\n" +
                                                "error('SERVER');\n" +
                                            "}\n" +
                                        "else" +
                                            "{\n" +
                                                "if ($tResultBarterRequest->num_rows == 1)\n" +
                                                    "{\n" +
                                                        "myLog('FIND THE USER BARTER REQUEST', __FILE__, __FUNCTION__, __LINE__);\n" +
                                                        "$tRowBarterRequest = $tResultBarterRequest->fetch_assoc();\n" +
                                                        "$sReplaces[" + t_THIS_Index_ItemsProposed + "] = $tRowBarterRequest['" + tItemsProposed + "'];\n" +
                                                    "}\n" +
                                            "}\n" +
                                        "$sReplaces[" + t_THIS_Index_BarterStatus + "]=" + ((int)NWDTradeStatus.Waiting).ToString() + ";\n" +
                                        "$sCsvList = Integrity" + BasisHelper().ClassNamePHP + "Replaces ($sCsvList, $sReplaces);\n" +

                                        "myLog('I need update the proposition waiting', __FILE__, __FUNCTION__, __LINE__);\n" +
                                        "Integrity" + NWDUserBarterRequest.BasisHelper().ClassNamePHP + "Reevalue ($sCsvList[" + t_THIS_Index_BarterRequest + "]);\n" +
                                    "}\n" +
                                "else\n" +
                                    "{\n" +
                                        "$sCsvList = Integrity" + BasisHelper().ClassNamePHP + "Replace ($sCsvList, " + t_THIS_Index_BarterStatus + ", \'" + ((int)NWDTradeStatus.Expired).ToString() + "\');\n" +
                                        "myLog('I need update the proposition refused ... too late!', __FILE__, __FUNCTION__, __LINE__);\n" +
                                    "}\n" +
                            "}\n" +
                        "GetDatas" + NWDUserBarterRequest.BasisHelper().ClassNamePHP + "ByReference ($sCsvList[" + t_THIS_Index_BarterRequest + "]);\n" +
                    "}\n" +

                // change the statut from CSV TO CANCEL 
                "else if ($sCsvList[" + t_THIS_Index_BarterStatus + "] == " + ((int)NWDTradeStatus.Cancel).ToString() + " && " +
                "$tServerStatut == " + ((int)NWDTradeStatus.Waiting).ToString() + ")\n" +
                    "{\n" +
                        "$tQueryCancelable = 'UPDATE `'.$ENV.'_" + BasisHelper().ClassNamePHP + "` SET " +
                        "`DM` = \\''.$TIME_SYNC.'\\', " +
                        "`DS` = \\''.$TIME_SYNC.'\\', " +
                        "`'.$ENV.'Sync` = \\''.$TIME_SYNC.'\\', " +
                        "`" + t_THIS_BarterStatus + "` = \\'" + ((int)NWDTradeStatus.Cancelled).ToString() + "\\' " +
                        "WHERE " +
                        "`Reference` = \\''.$SQL_CON->real_escape_string($tReference).'\\' " +
                        "AND `" + t_THIS_BarterStatus + "` = \\'" + ((int)NWDTradeStatus.Waiting).ToString() + "\\' " +
                        "';" +
                        "$tResultCancelable = $SQL_CON->query($tQueryCancelable);\n" +
                        "if (!$tResultCancelable)\n" +
                            "{\n" +
                                "myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tResultCancelable.'', __FILE__, __FUNCTION__, __LINE__);\n" +
                                "error('SERVER');\n" +
                            "}\n" +
                        "else" +
                            "{\n" +
                                "$tNumberOfRow = 0;\n" +
                                "$tNumberOfRow = $SQL_CON->affected_rows;\n" +
                                "if ($tNumberOfRow == 1)\n" +
                                    "{\n" +
                                        "Integrity" + BasisHelper().ClassNamePHP + "Reevalue ($tReference);\n" +
                                    "}\n" +
                            "}\n" +
                        "GetDatas" + BasisHelper().ClassNamePHP + "ByReference ($tReference);\n" +
                        "myLog('Break!', __FILE__, __FUNCTION__, __LINE__);\n" +
                        "return;\n" +
                    "}\n" +


                // change the statut from CSV TO FORCE // ADMIN ONLY 
                "else if ($sCsvList[" + t_THIS_Index_BarterStatus + "] == " + ((int)NWDTradeStatus.Force).ToString() + " && $sAdmin == true)\n" +
                    "{\n" +
                        "//EXECEPTION FOR ADMIN\n" +
                    "}\n" +

                // change the statut from CSV TO FORCE NONE  // ADMIN ONLY 
                "else if ($sCsvList[" + t_THIS_Index_BarterStatus + "] == " + ((int)NWDTradeStatus.ForceNone).ToString() + " && $sAdmin == true)\n" +
                    "{\n" +
                        "$sReplaces[" + t_THIS_Index_BarterStatus + "]=" + ((int)NWDTradeStatus.None).ToString() + ";\n" +
                        "$sReplaces[" + t_THIS_Index_ItemsProposed + "]='';\n" +
                        "$sReplaces[" + t_THIS_Index_ItemsSend + "]='';\n" +
                        "$sReplaces[" + t_THIS_Index_BarterRequestHash + "]='';\n" +
                        "$sReplaces[" + t_THIS_Index_BarterRequest + "]='';\n" +
                        "$sCsvList = Integrity" + BasisHelper().ClassNamePHP + "Replaces ($sCsvList, $sReplaces);\n" +
                    "}\n" +

                // OTHER
                "else\n" +
                    "{\n" +
                        //"Integrity" + Datas().ClassNamePHP + "Reevalue ($tReference);\n" +
                        "GetDatas" + BasisHelper().ClassNamePHP + "ByReference ($tReference);\n" +
                        "return;\n" +
                    "}\n" +
                "myLog('FINSIH ADD ON ... UPDATE FROM CSV', __FILE__, __FUNCTION__, __LINE__);\n" +
                "// finish Addon \n";

            return sScript;
        }
        //------------------------------------------------------------------------------------------------------------- 
        [NWDAliasMethod(NWDConstants.M_AddonPhpPostCalculate)]
        public static string AddonPhpPostCalculate(NWDAppEnvironment AppEnvironment)
        {
            string t_THIS_BarterRequest = FindAliasName("BarterRequest");
            int t_THIS_Index_BarterRequest = CSV_IndexOf(t_THIS_BarterRequest);

            return "// write your php script here to update after sync on server\n " +
                "GetDatas" + NWDUserBarterRequest.BasisHelper().ClassNamePHP + "ByReference ($sCsvList[" + t_THIS_Index_BarterRequest + "]);\n";
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif