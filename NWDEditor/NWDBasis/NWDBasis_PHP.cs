﻿//=====================================================================================================================
//
// ideMobi copyright 2019
// All rights reserved by ideMobi
//
// Read License-en or Licence-fr
//
//=====================================================================================================================
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SQLite4Unity3d;
using BasicToolBox;
using UnityEditor;
using System.Text;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDBasis<K> : NWDTypeClass where K : NWDBasis<K>, new()
    {
        //-------------------------------------------------------------------------------------------------------------
        [NWDAliasMethod(NWDConstants.M_CreateAllError)]
        public static void CreateAllError()
        {
            // Create error in local data base
            string tClassName = Datas().ClassTableName;
            string tTrigramme = Datas().ClassTrigramme;
            NWDError.CreateGenericError(tClassName, tTrigramme + "x01", "Error in " + tClassName, "error in request creation in " + tClassName + "", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x02", "Error in " + tClassName, "error in request creation add primary key in " + tClassName + "", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x03", "Error in " + tClassName, "error in request creation add autoincrement modify in " + tClassName + "", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x05", "Error in " + tClassName, "error in sql index creation in " + tClassName + "", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x07", "Error in " + tClassName, "error in sql defragment in " + tClassName + "", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x08", "Error in " + tClassName, "error in sql drop in " + tClassName + "", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x09", "Error in " + tClassName, "error in sql Flush in " + tClassName + "", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x11", "Error in " + tClassName, "error in sql add columns in " + tClassName + "", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x12", "Error in " + tClassName, "error in sql alter columns in " + tClassName + "", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x31", "Error in " + tClassName, "error in request insert new datas before update in " + tClassName + " (update table?)", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x32", "Error in " + tClassName, "error in request select datas to update in " + tClassName + " (update table?)", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x33", "Error in " + tClassName, "error in request select updatable datas in " + tClassName + " (update table?)", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x38", "Error in " + tClassName, "error in request update datas in " + tClassName + " (update table?)", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x39", "Error in " + tClassName, "error more than one row for this reference in  " + tClassName + "", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x40", "Error in " + tClassName, "error in flush trashed in  " + tClassName + "", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x91", "Error in " + tClassName, "error update integrity in " + tClassName + " (update table?)", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x99", "Error in " + tClassName, "error columns number in " + tClassName + " (update table?)", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x88", "Error in " + tClassName, "integrity of one datas is false, break in " + tClassName + "", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);
            NWDError.CreateGenericError(tClassName, tTrigramme + "x77", "Error in " + tClassName, "error update log in " + tClassName + " (update table?)", "OK", NWDErrorType.LogVerbose, NWDBasisTag.TagServerCreated);

        }
        //-------------------------------------------------------------------------------------------------------------
        public static Dictionary<string, string> CreatePHPConstant(NWDAppEnvironment sEnvironment)
        {
            Dictionary<string, string> rReturn = new Dictionary<string, string>();

            string tClassName = Datas().ClassNamePHP;
            string tTrigramme = Datas().ClassTrigramme;

            Dictionary<string, int> tResult = new Dictionary<string, int>();
            foreach (KeyValuePair<int, Dictionary<string, string>> tKeyValue in NWDAppConfiguration.SharedInstance().kWebBuildkSLQAssemblyOrder.OrderBy(x => x.Key))
            {
                if (NWDAppConfiguration.SharedInstance().WSList.ContainsKey(tKeyValue.Key) == true)
                {
                    if (NWDAppConfiguration.SharedInstance().WSList[tKeyValue.Key] == true)
                    {
                        foreach (KeyValuePair<string, string> tSubKeyValue in tKeyValue.Value.OrderBy(x => x.Key))
                        {
                            if (tResult.ContainsKey(tSubKeyValue.Key))
                            {
                                if (tResult[tSubKeyValue.Key] < tKeyValue.Key)
                                {
                                    tResult[tSubKeyValue.Key] = tKeyValue.Key;
                                }
                            }
                            else
                            {
                                tResult.Add(tSubKeyValue.Key, tKeyValue.Key);
                            }
                        }
                    }
                }
            }
            int tWebBuildUsed = NWDAppConfiguration.SharedInstance().WebBuild;
            //NWDAppConfiguration.SharedInstance().kLastWebBuildClass = new Dictionary<Type, int>();
            foreach (KeyValuePair<string, int> tKeyValue in tResult.OrderBy(x => x.Key))
            {
                if (tKeyValue.Key == Datas().ClassNamePHP)
                {
                    tWebBuildUsed = tKeyValue.Value;
                }
            }

            StringBuilder tFile = new StringBuilder(string.Empty);
            tFile.AppendLine("<?php");
            tFile.AppendLine(sEnvironment.Headlines());
            tFile.AppendLine(NWD.K_CommentSeparator);
            tFile.AppendLine("// CONSTANTS");
            tFile.AppendLine(NWD.K_CommentSeparator);
            tFile.AppendLine("include_once ($PATH_BASE.'/" + sEnvironment.Environment + "/" + NWD.K_ENG + "/" + NWD.K_STATIC_FUNCTIONS_PHP + "');");
            tFile.AppendLine(NWD.K_CommentSeparator);
            // to bypass the global limitation of PHP in internal include : use function :-) 
            tFile.AppendLine("function " + tClassName + "Constants ()");
            tFile.AppendLine("{");
            if (sEnvironment.LogMode == true)
            {
                tFile.AppendLine("myLog('DEBUG TRACE', __FILE__, __FUNCTION__, __LINE__);");
            }
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB, $SQL_" + tClassName + "_WebService;");
            tFile.AppendLine("$SQL_" + tClassName + "_SaltA = '" + Datas().SaltA + "';");
            tFile.AppendLine("$SQL_" + tClassName + "_SaltB = '" + Datas().SaltB + "';");
            tFile.AppendLine("$SQL_" + tClassName + "_WebService = " + tWebBuildUsed + ";");
            tFile.AppendLine("}");
            tFile.AppendLine("//Run this function to install globals of theses datas!");
            tFile.AppendLine(tClassName + "Constants();");
            tFile.AppendLine(NWD.K_CommentSeparator);
            // craete constants of erro in php
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x01', 'error in request creation in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x02', 'error in request creation add primary key in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x03', 'error in request creation add autoincrement modify in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x04', 'error in sql index remove in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x05', 'error in sql index creation in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x07', 'error in sql defragment in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x08', 'error in sql drop in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x09', 'error in sql Flush in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x11', 'error in sql add columns in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x12', 'error in sql alter columns in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x31', 'error in request insert new datas before update in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x32', 'error in request select datas to update in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x33', 'error in request select updatable datas in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x38', 'error in request update datas in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x39', 'error too much datas for this reference in  " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x88', 'integrity of one datas is false, break in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x91', 'error update integrity in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x99', 'error columns number in " + tClassName + "');");
            tFile.AppendLine("errorDeclaration('" + tTrigramme + "x77', 'error update log in " + tClassName + "');");
            tFile.AppendLine(NWD.K_CommentSeparator);
            tFile.AppendLine("?>");
            string tFileFormatted = NWDToolbox.CSharpFormat(tFile.ToString());
            rReturn.Add(tClassName + "/" + NWD.K_CONSTANTS_FILE, tFileFormatted);
            return rReturn;

        }
        //-------------------------------------------------------------------------------------------------------------
        public static Dictionary<string, string> CreatePHPManagement(NWDAppEnvironment sEnvironment)
        {
            Dictionary<string, string> rReturn = new Dictionary<string, string>();
            string tClassName = Datas().ClassNamePHP;
            string tTrigramme = Datas().ClassTrigramme;
            Type tType = ClassType();
            TableMapping tTableMapping = new TableMapping(tType);
            string tTableName = tTableMapping.TableName;
            //========= MANAGEMENT TABLE FUNCTIONS FILE
            StringBuilder tFile = new StringBuilder(string.Empty);
            tFile.AppendLine("<?php");
            tFile.AppendLine(sEnvironment.Headlines());
            tFile.AppendLine(NWD.K_CommentSeparator);
            tFile.AppendLine("// TABLE MANAGEMENT");
            tFile.AppendLine(NWD.K_CommentSeparator);
            tFile.AppendLine("include_once ( $PATH_BASE.'/" + sEnvironment.Environment + "/" + NWD.K_DB + "/" + tClassName + "/" + NWD.K_CONSTANTS_FILE + "');");
            tFile.AppendLine("include_once ( $PATH_BASE.'/" + sEnvironment.Environment + "/" + NWD.K_ENG + "/" + NWD.K_STATIC_FUNCTIONS_PHP + "');");
            tFile.AppendLine(NWD.K_CommentSeparator);
            tFile.AppendLine("function Create" + tClassName + "Table () {");
            if (sEnvironment.LogMode == true)
            {
                tFile.AppendLine("myLog('DEBUG TRACE', __FILE__, __FUNCTION__, __LINE__);");
            }
            tFile.AppendLine("global $SQL_CON, $ENV;");
            var tQuery = "CREATE TABLE IF NOT EXISTS `'.$ENV.'_" + tTableName + "` (";
            var tDeclarations = tTableMapping.Columns.Select(p => Orm.SqlDecl(p, true));
            var tDeclarationsJoined = string.Join(",", tDeclarations.ToArray());
            tDeclarationsJoined = tDeclarationsJoined.Replace('"', '`');
            tDeclarationsJoined = tDeclarationsJoined.Replace("`ID` integer", "`ID` int(11) NOT NULL");
            tDeclarationsJoined = tDeclarationsJoined.Replace("`DC` integer", "`DC` int(11) NOT NULL DEFAULT 0");
            //tDeclarationsJoined = tDeclarationsJoined.Replace ("`AC` integer", "`AC` int(11) NOT NULL DEFAULT 1");
            tDeclarationsJoined = tDeclarationsJoined.Replace("`DM` integer", "`DM` int(11) NOT NULL DEFAULT 0");
            tDeclarationsJoined = tDeclarationsJoined.Replace("`DD` integer", "`DD` int(11) NOT NULL DEFAULT 0");
            tDeclarationsJoined = tDeclarationsJoined.Replace("`DS` integer", "`DS` int(11) NOT NULL DEFAULT 0");
            tDeclarationsJoined = tDeclarationsJoined.Replace("`XX` integer", "`XX` int(11) NOT NULL DEFAULT 0");
            tDeclarationsJoined = tDeclarationsJoined.Replace("varchar", "text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL");
            tDeclarationsJoined = tDeclarationsJoined.Replace("primary key autoincrement not null", string.Empty);
            tQuery += tDeclarationsJoined;
            tQuery += ") ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;";
            tFile.AppendLine("$tQuery = '" + tQuery + "';");
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("if (!$tResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("error('" + tTrigramme + "x01');");
            tFile.AppendLine("}");
            tFile.AppendLine("$tQuery = 'ALTER TABLE `'.$ENV.'_" + tTableName + "` ADD PRIMARY KEY (`ID`), ADD UNIQUE KEY `ID` (`ID`);';");
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("$tQuery = 'ALTER TABLE `'.$ENV.'_" + tTableName + "` MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;';");
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("");
            tFile.AppendLine("// Alter all existing table with new columns or change type columns");
            foreach (TableMapping.Column tColumn in tTableMapping.Columns)
            {
                if (tColumn.Name != "ID" &&
                    tColumn.Name != "Reference" &&
                    tColumn.Name != "DM" &&
                    tColumn.Name != "DC" &&
                    //                  tColumn.Name != "AC" &&
                    tColumn.Name != "DD" &&
                    tColumn.Name != "DS" &&
                    tColumn.Name != "XX")
                {
                    tFile.Append("$tQuery ='ALTER TABLE `'.$ENV.'_" + tTableName + "` ADD " +
                        Orm.SqlDecl(tColumn, true).Replace(" varchar ", " TEXT CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ").Replace(" float ", " double ").Replace("\"", "`") +
                        ";';");
                    tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
                    tFile.AppendLine("$tQuery ='ALTER TABLE `'.$ENV.'_" + tTableName + "` MODIFY " +
                        Orm.SqlDecl(tColumn, true).Replace(" varchar ", " TEXT CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ").Replace(" float ", " double ").Replace("\"", "`") +
                        ";';");
                    tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
                }
            }
            var indexes = new Dictionary<string, SQLite4Unity3d.SQLiteConnection.IndexInfo>();
            foreach (var c in tTableMapping.Columns)
            {
                foreach (var i in c.Indices)
                {
                    var iname = i.Name ?? tTableName + "_" + c.Name;
                    SQLite4Unity3d.SQLiteConnection.IndexInfo iinfo;
                    if (!indexes.TryGetValue(iname, out iinfo))
                    {
                        iinfo = new SQLite4Unity3d.SQLiteConnection.IndexInfo
                        {
                            IndexName = iname,
                            TableName = tTableName,
                            Unique = i.Unique,
                            Columns = new List<SQLite4Unity3d.SQLiteConnection.IndexedColumn>()
                        };
                        indexes.Add(iname, iinfo);
                    }
                    if (i.Unique != iinfo.Unique)
                        throw new Exception("All the columns in an index must have the same value for their Unique property");
                    iinfo.Columns.Add(new SQLite4Unity3d.SQLiteConnection.IndexedColumn
                    {
                        Order = i.Order,
                        ColumnName = c.Name
                    });
                }
            }
            foreach (var indexName in indexes.Keys)
            {
                var index = indexes[indexName];
                string[] columnNames = new string[index.Columns.Count];
                if (index.Columns.Count == 1)
                {
                    columnNames[0] = index.Columns[0].ColumnName;
                }
                else
                {
                    index.Columns.Sort((lhs, rhs) =>
                    {
                        return lhs.Order - rhs.Order;
                    });
                    for (int i = 0, end = index.Columns.Count; i < end; ++i)
                    {
                        columnNames[i] = index.Columns[i].ColumnName;
                    }
                }
                List<string> columnNamesFinalList = new List<string>();
                foreach (string tName in columnNames)
                {
                    PropertyInfo tColumnInfos = tType.GetProperty(tName);
                    Type tColumnType = tColumnInfos.PropertyType;
                    if (tColumnType.IsSubclassOf(typeof(BTBDataType)))
                    {
                        columnNamesFinalList.Add("`" + tName + "`(24)");
                    }
                    else if (tColumnType.IsSubclassOf(typeof(BTBDataTypeInt)))
                    {
                        columnNamesFinalList.Add("`" + tName + "`");
                    }
                    else if (tColumnType.IsSubclassOf(typeof(BTBDataTypeFloat)))
                    {
                        columnNamesFinalList.Add("`" + tName + "`");
                    }
                    else if (tColumnType == typeof(string))
                    {
                        columnNamesFinalList.Add("`" + tName + "`(24)");
                    }
                    else if (tColumnType == typeof(string))
                    {
                        columnNamesFinalList.Add("`" + tName + "`(32)");
                    }
                    else
                    {
                        columnNamesFinalList.Add("`" + tName + "`");
                    }
                }
                string[] columnNamesFinal = columnNamesFinalList.ToArray<string>();
                tFile.AppendLine("$tRemoveIndexQuery = 'DROP INDEX `" + indexName + "` ON `'.$ENV.'_" + index.TableName + "`;';");
                tFile.AppendLine("$tRemoveIndexResult = $SQL_CON->query($tRemoveIndexQuery);");
                const string sqlFormat = "CREATE {2}INDEX `{3}` ON `'.$ENV.'_{0}` ({1});";
                var sql = String.Format(sqlFormat, index.TableName, string.Join(", ", columnNamesFinal), index.Unique ? "UNIQUE " : "", indexName);
                tFile.AppendLine("$tQuery = '" + sql + "';");
                tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
                tFile.AppendLine("if (!$tResult)");
                tFile.AppendLine("{");
                if (sEnvironment.LogMode == true)
                {
                    tFile.AppendLine("myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tQuery.'', __FILE__, __FUNCTION__, __LINE__);");
                }
                tFile.AppendLine("error('" + tTrigramme + "x05');");
                tFile.AppendLine("}");
            }
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("function Defragment" + tClassName + "Table ()");
            tFile.AppendLine("{");
            if (sEnvironment.LogMode == true)
            {
                tFile.AppendLine("myLog('DEBUG TRACE', __FILE__, __FUNCTION__, __LINE__);");
            }
            tFile.AppendLine("global $SQL_CON, $ENV;");
            tFile.AppendLine("$tQuery = 'ALTER TABLE `'.$ENV.'_" + tTableName + "` ENGINE=InnoDB;';");
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("if (!$tResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("error('" + tTrigramme + "x07');");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("function Drop" + tClassName + "Table ()");
            tFile.AppendLine("{");
            if (sEnvironment.LogMode == true)
            {
                tFile.AppendLine("myLog('DEBUG TRACE', __FILE__, __FUNCTION__, __LINE__);");
            }
            tFile.AppendLine("global $SQL_CON, $ENV;");
            tFile.AppendLine("$tQuery = 'DROP TABLE `'.$ENV.'_" + tTableName + "`;';");
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("if (!$tResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("error('" + tTrigramme + "x08');");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("function Flush" + tClassName + "Table ()");
            tFile.AppendLine("{");
            if (sEnvironment.LogMode == true)
            {
                tFile.AppendLine("myLog('DEBUG TRACE', __FILE__, __FUNCTION__, __LINE__);");
            }
            tFile.AppendLine("global $SQL_CON, $ENV;");
            tFile.AppendLine("$tQuery = 'FLUSH TABLE `'.$ENV.'_" + tTableName + "`;';");
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("if (!$tResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("error('" + tTrigramme + "x09');");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("?>");
            string tFileFormatted = NWDToolbox.CSharpFormat(tFile.ToString());
            rReturn.Add(tClassName + "/" + NWD.K_MANAGEMENT_FILE, tFileFormatted);
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static Dictionary<string, string> CreatePHPSynchronisation(NWDAppEnvironment sEnvironment)
        {
            Dictionary<string, string> rReturn = new Dictionary<string, string>();
            string tClassName = Datas().ClassNamePHP;
            string tTableName = Datas().ClassNamePHP;
            string tTrigramme = Datas().ClassTrigramme;
            Type tType = ClassType();
            StringBuilder tFile = new StringBuilder(string.Empty);
            //========= SYNCHRONIZATION FUNCTIONS FILE
            // if need Account reference I prepare the restriction
            List<string> tAccountReference = new List<string>();
            List<string> tAccountReferences = new List<string>();
            foreach (var tProp in tType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Type tTypeOfThis = tProp.PropertyType;
                if (tTypeOfThis != null)
                {
                    if (tTypeOfThis.IsGenericType)
                    {

                        if (tTypeOfThis.GetGenericTypeDefinition() == typeof(NWDReferenceType<>))
                        {
                            Type tSubType = tTypeOfThis.GetGenericArguments()[0];
                            if (tSubType == typeof(NWDAccount))
                            {
                                tAccountReference.Add("`" + tProp.Name + "` LIKE \\''.$SQL_CON->real_escape_string($sAccountReference).'\\' ");
                                tAccountReferences.Add("`" + tProp.Name + "` IN (\\''.implode('\\', \\'', $sAccountReferences).'\\') ");
                            }
                        }
                        else if (tTypeOfThis.GetGenericTypeDefinition() == typeof(NWDReferenceHashType<>))
                        {
                            Type tSubType = tTypeOfThis.GetGenericArguments()[0];
                            if (tSubType == typeof(NWDAccount))
                            {
                                tAccountReference.Add("`" + tProp.Name + "` LIKE \\''.$SQL_CON->real_escape_string(md5($sAccountReference.$SQL_" + tClassName + "_SaltA)).'\\' ");
                                tAccountReferences.Add("`" + tProp.Name + "` IN (\\''.implode('\\', \\'', $sAccountReferences).'\\') ");
                            }
                        }
                    }
                }
            }
            bool tINeedAdminAccount = true;
            if (tAccountReference.Count == 0)
            {
            }
            else
            {
                tINeedAdminAccount = false;
            }

            string SLQIntegrityOrderToSelect = "***";
            foreach (string tPropertyName in SLQIntegrityOrder())
            {
                PropertyInfo tPropertyInfo = tType.GetProperty(tPropertyName, BindingFlags.Public | BindingFlags.Instance);
                Type tTypeOfThis = tPropertyInfo.PropertyType;
                if (tTypeOfThis == typeof(int) || tTypeOfThis == typeof(long))
                {
                    SLQIntegrityOrderToSelect += ", REPLACE(`" + tPropertyName + "`,\",\",\"\") as `" + tPropertyName + "`";
                }
                else if (tTypeOfThis == typeof(float))
                {
                    SLQIntegrityOrderToSelect += ", REPLACE(FORMAT(`" + tPropertyName + "`," + NWDConstants.FloatSQLFormat + "),\",\",\"\") as `" + tPropertyName + "`";
                }
                else if (tTypeOfThis == typeof(double))
                {
                    SLQIntegrityOrderToSelect += ", REPLACE(FORMAT(`" + tPropertyName + "`," + NWDConstants.DoubleSQLFormat + "),\",\",\"\") as `" + tPropertyName + "`";
                }
                else
                {
                    SLQIntegrityOrderToSelect += ", `" + tPropertyName + "`";
                }
            }
            SLQIntegrityOrderToSelect = SLQIntegrityOrderToSelect.Replace("***, ", "");

            tFile.AppendLine("<?php");
            tFile.AppendLine(sEnvironment.Headlines());
            tFile.AppendLine(NWD.K_CommentSeparator);
            tFile.AppendLine("// SYNCHRONIZATION FUNCTIONS");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("include_once ( $PATH_BASE.'/" + sEnvironment.Environment + "/" + NWD.K_DB + "/" + tClassName + "/" + NWD.K_CONSTANTS_FILE + "');");
            tFile.AppendLine("include_once ( $PATH_BASE.'/" + sEnvironment.Environment + "/" + NWD.K_ENG + "/" + NWD.K_STATIC_FUNCTIONS_PHP + "');");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("function Integrity" + tClassName + "Test ($sCsv)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB;");
            tFile.AppendLine("$rReturn = true;");
            tFile.AppendLine("$sCsvList = explode('" + NWDConstants.kStandardSeparator + "',$sCsv);");
            tFile.AppendLine("$tIntegrity = array_pop($sCsvList);");
            tFile.AppendLine("unset($sCsvList[2]);//remove DS");
            tFile.AppendLine("unset($sCsvList[3]);//remove DevSync");
            tFile.AppendLine("unset($sCsvList[4]);//remove PreprodSync");
            tFile.AppendLine("unset($sCsvList[5]);//remove ProdSync");
            tFile.AppendLine("$sDataString = implode('',$sCsvList);");
            tFile.AppendLine("$tCalculate = str_replace('" + NWDConstants.kStandardSeparator + "', '', md5($SQL_" + tClassName + "_SaltA.$sDataString.$SQL_" + tClassName + "_SaltB));");
            tFile.AppendLine("if ($tCalculate!=$tIntegrity)");
            tFile.AppendLine("{");
            tFile.AppendLine("$rReturn = false;");
            tFile.AppendLine("error('" + tTrigramme + "x88');");
            tFile.AppendLine("}");
            tFile.AppendLine("return $rReturn;");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("function Integrity" + tClassName + "Replace ($sCsvArray, $sIndex, $sValue)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB;");
            tFile.AppendLine("$sCsvList = $sCsvArray;");
            tFile.AppendLine("$sCsvList[$sIndex] = $sValue;");
            tFile.AppendLine("$tIntegrity = array_pop($sCsvList);");
            tFile.AppendLine("unset($sCsvList[2]);//remove DS");
            tFile.AppendLine("unset($sCsvList[3]);//remove DevSync");
            tFile.AppendLine("unset($sCsvList[4]);//remove PreprodSync");
            tFile.AppendLine("unset($sCsvList[5]);//remove ProdSync");
            tFile.AppendLine("$sDataString = implode('',$sCsvList);");
            tFile.AppendLine("$tCalculate = str_replace('|', '', md5($SQL_" + tClassName + "_SaltA.$sDataString.$SQL_" + tClassName + "_SaltB));");
            tFile.AppendLine("$sCsvArray[$sIndex] = $sValue;");
            tFile.AppendLine("array_pop($sCsvArray);");
            tFile.AppendLine("$sCsvArray[] = $tCalculate;");
            tFile.AppendLine("return $sCsvArray;");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("function Integrity" + tClassName + "Replaces ($sCsvArray, $sIndexesAndValues)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB;");
            tFile.AppendLine("$sCsvList = $sCsvArray;");
            tFile.AppendLine("foreach(array_keys($sIndexesAndValues) as $tKey)");
            tFile.AppendLine("{");
            tFile.AppendLine("$sCsvList[$tKey] = $sIndexesAndValues[$tKey];");
            tFile.AppendLine("}");
            tFile.AppendLine("$tIntegrity = array_pop($sCsvList);");
            tFile.AppendLine("unset($sCsvList[2]);//remove DS");
            tFile.AppendLine("unset($sCsvList[3]);//remove DevSync");
            tFile.AppendLine("unset($sCsvList[4]);//remove PreprodSync");
            tFile.AppendLine("unset($sCsvList[5]);//remove ProdSync");
            tFile.AppendLine("$sDataString = implode('',$sCsvList);");
            tFile.AppendLine("$tCalculate = str_replace('|', '', md5($SQL_" + tClassName + "_SaltA.$sDataString.$SQL_" + tClassName + "_SaltB));");
            tFile.AppendLine("foreach(array_keys($sIndexesAndValues) as $tKey)");
            tFile.AppendLine("{");
            tFile.AppendLine("$sCsvArray[$tKey] = $sIndexesAndValues[$tKey];");
            tFile.AppendLine("}");
            tFile.AppendLine("array_pop($sCsvArray);");
            tFile.AppendLine("$sCsvArray[] = $tCalculate;");
            tFile.AppendLine("return $sCsvArray;");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("function Prepare" + tClassName + "Data ($sCsv)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB, $TIME_SYNC;");
            tFile.AppendLine("$sCsvList = explode('" + NWDConstants.kStandardSeparator + "',$sCsv);");
            tFile.AppendLine("$sCsvList[2] = $TIME_SYNC;// change DS");
            tFile.AppendLine("if ($sCsvList[1]<$TIME_SYNC)");
            tFile.AppendLine("{");
            tFile.AppendLine("$sCsvList[2] = $sCsvList[1];");
            tFile.AppendLine("}");
            if (sEnvironment == NWDAppConfiguration.SharedInstance().DevEnvironment)
            {
                tFile.AppendLine("$sCsvList[3] = $TIME_SYNC;// change DevSync");
            }
            else if (sEnvironment == NWDAppConfiguration.SharedInstance().PreprodEnvironment)
            {
                tFile.AppendLine("$sCsvList[4] = $TIME_SYNC;// change PreprodSync");
            }
            else if (sEnvironment == NWDAppConfiguration.SharedInstance().ProdEnvironment)
            {
                tFile.AppendLine("$sCsvList[5] = $TIME_SYNC;// change ProdSync");
            }
            tFile.AppendLine("return $sCsvList;");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);


            tFile.AppendLine("function Log" + tClassName + " ($sReference, $sLog)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $SQL_CON, $ENV;");
            tFile.AppendLine("$tUpdate = 'UPDATE `'.$ENV.'_" + tTableName + "` SET `ServerLog` = CONCAT(`ServerLog`, \\' ; '.$SQL_CON->real_escape_string($sLog).'\\') WHERE `Reference` = \\''.$SQL_CON->real_escape_string($sReference).'\\';';");
            tFile.AppendLine("$tUpdateResult = $SQL_CON->query($tUpdate);");
            tFile.AppendLine("if (!$tUpdateResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tUpdate.'', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("error('" + tTrigramme + "x77');");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            // SERVER Integrity generate
            tFile.AppendLine("function IntegrityServer" + tClassName + "Generate ($sRow)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $NWD_SLT_SRV;");
            tFile.Append("$sDataServerString =''");
            foreach (string tPropertyName in SLQIntegrityServerOrder())
            {
                tFile.Append(".$sRow['" + tPropertyName + "']");
            }
            tFile.AppendLine(";");
            tFile.AppendLine("return str_replace('" + NWDConstants.kStandardSeparator + "', '', md5($NWD_SLT_SRV.$sDataServerString.$NWD_SLT_SRV));");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            // DATA Integrity generate
            tFile.AppendLine("function Integrity" + tClassName + "Generate ($sRow)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $SQL_CON, $ENV, $NWD_SLT_SRV;");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB;");
            tFile.Append("$sDataString =''");
            foreach (string tPropertyName in SLQIntegrityOrder())
            {
                tFile.Append(".$sRow['" + tPropertyName + "']");
            }
            tFile.AppendLine(";");
            tFile.AppendLine("myLog('sDataString : '.$sDataString.'', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("return str_replace('" + NWDConstants.kStandardSeparator + "', '', md5($SQL_" + tClassName + "_SaltA.$sDataString.$SQL_" + tClassName + "_SaltB));");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            // TODO refactor to be more easy to generate
            tFile.AppendLine("function Integrity" + tClassName + "Reevalue ($sReference)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $SQL_CON, $WSBUILD, $ENV, $NWD_SLT_SRV, $TIME_SYNC, $NWD_FLOAT_FORMAT;");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB, $SQL_" + tClassName + "_WebService;");
            tFile.AppendLine("$tQuery = 'SELECT " + SLQIntegrityOrderToSelect + " FROM `'.$ENV.'_" + tTableName + "` WHERE `Reference` = \\''.$SQL_CON->real_escape_string($sReference).'\\';';");
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("if (!$tResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tQuery.'', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("error('" + tTrigramme + "x31');");
            tFile.AppendLine("}");
            tFile.AppendLine("else");
            tFile.AppendLine("{");
            tFile.AppendLine("if ($tResult->num_rows == 1)");
            tFile.AppendLine("{");
            tFile.AppendLine("// I calculate the integrity and reinject the good value");
            tFile.AppendLine("$tRow = $tResult->fetch_assoc();");
            //"$tRow['WebServiceVersion'] = $WSBUILD;" );
            tFile.AppendLine("$tRow['WebServiceVersion'] = $SQL_" + tClassName + "_WebService;");
            tFile.AppendLine("$tCalculate = Integrity" + tClassName + "Generate ($tRow);");
            tFile.AppendLine("$tCalculateServer = IntegrityServer" + tClassName + "Generate ($tRow);");
            tFile.AppendLine("$tUpdate = 'UPDATE `'.$ENV.'_" + tTableName + "` SET `Integrity` = \\''.$SQL_CON->real_escape_string($tCalculate).'\\',");
            tFile.Append(" `ServerHash` = \\''.$SQL_CON->real_escape_string($tCalculateServer).'\\',");
            tFile.Append(" `'.$ENV.'Sync` = \\''.$TIME_SYNC.'\\' ,");
            //tSynchronizationFile.Append(" `WebServiceVersion` = \\''.$WSBUILD.'\\'" );
            tFile.AppendLine(" `WebServiceVersion` = \\''.$SQL_" + tClassName + "_WebService.'\\'" + " WHERE `Reference` = \\''.$SQL_CON->real_escape_string($sReference).'\\';';");
            tFile.AppendLine("$tUpdateResult = $SQL_CON->query($tUpdate);");
            tFile.AppendLine("if (!$tUpdateResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tUpdate.'', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("error('" + tTrigramme + "x91');");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            // TODO refactor to be more easy to generate
            tFile.AppendLine("function IntegrityServer" + tClassName + "Validate ($sReference)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $SQL_CON, $ENV, $NWD_SLT_SRV;");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB;");
            tFile.AppendLine("$tQuery = 'SELECT " + SLQAssemblyOrder() + " FROM `'.$ENV.'_" + tTableName + "` WHERE `Reference` = \\''.$SQL_CON->real_escape_string($sReference).'\\';';");
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("if (!$tResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tQuery.'', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("error('" + tTrigramme + "x31');");
            tFile.AppendLine("}");
            tFile.AppendLine("else");
            tFile.AppendLine("{");
            tFile.AppendLine("if ($tResult->num_rows == 1)");
            tFile.AppendLine("{");
            tFile.AppendLine("// I calculate the integrity and reinject the good value");
            tFile.AppendLine("$tRow = $tResult->fetch_assoc();");
            tFile.AppendLine("$tCalculateServer = IntegrityServer" + tClassName + "Generate ($tRow);");
            tFile.AppendLine("if ($tCalculateServer == $tRow['ServerHash'])");
            tFile.AppendLine("{");
            tFile.AppendLine("return true;");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine("return false;");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            // TODO refactor to be more easy to generate
            tFile.AppendLine("function IntegrityServer" + tClassName + "ValidateByRow ($sRow)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $NWD_SLT_SRV;");
            tFile.AppendLine("$tCalculateServer = IntegrityServer" + tClassName + "Generate ($sRow);");
            tFile.AppendLine("if ($tCalculateServer == $sRow['ServerHash'])");
            tFile.AppendLine("{");
            tFile.AppendLine("return true;");
            tFile.AppendLine("}");
            tFile.AppendLine("return false;");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            // TODO refactor to be more easy to generate
            tFile.AppendLine("function Integrity" + tClassName + "Validate ($sReference)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $SQL_CON, $ENV, $NWD_SLT_SRV;");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB;");
            tFile.AppendLine("$tQuery = 'SELECT " + SLQAssemblyOrder() + " FROM `'.$ENV.'_" + tTableName + "` WHERE `Reference` = \\''.$SQL_CON->real_escape_string($sReference).'\\';';");
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("if (!$tResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("error('" + tTrigramme + "x31');");
            tFile.AppendLine("myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tQuery.'', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("}");
            tFile.AppendLine("else");
            tFile.AppendLine("{");
            tFile.AppendLine("if ($tResult->num_rows == 1)");
            tFile.AppendLine("{");
            tFile.AppendLine("// I calculate the integrity and reinject the good value");
            tFile.AppendLine("$tRow = $tResult->fetch_assoc();");
            tFile.AppendLine("$tCalculate =Integrity" + tClassName + "Generate ($tRow);");
            tFile.AppendLine("if ($tCalculate == $tRow['Integrity'])");
            tFile.AppendLine("{");
            tFile.AppendLine("return true;");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine("return false;");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            // TODO refactor to be more easy to generate

            tFile.AppendLine("function Integrity" + tClassName + "ValidateByRow ($sRow)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $SQL_CON, $WSBUILD, $ENV, $NWD_SLT_SRV;");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB,$SQL_" + tClassName + "_WebService;");
            tFile.AppendLine("$tCalculate =Integrity" + tClassName + "Generate ($sRow);");
            tFile.AppendLine("if ($tCalculate == $sRow['Integrity'])");
            tFile.AppendLine("{");
            tFile.AppendLine("return true;");
            tFile.AppendLine("}");
            tFile.AppendLine("return false;");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("function UpdateData" + tClassName + " ($sCsv, $sTimeStamp, $sAccountReference, $sAdmin)");
            List<string> tModify = new List<string>();
            List<string> tColumnNameList = new List<string>();
            List<string> tColumnValueList = new List<string>();
            tColumnNameList.Add("`Reference`");
            tColumnValueList.Add("\\''.$SQL_CON->real_escape_string($sCsvList[0]).'\\'");
            int tIndex = 1;
            foreach (string tProperty in SLQAssemblyOrderArray())
            {
                tModify.Add("`" + tProperty + "` = \\''.$SQL_CON->real_escape_string($sCsvList[" + tIndex.ToString() + "]).'\\'");
                tColumnNameList.Add("`" + tProperty + "`");
                tColumnValueList.Add("\\''.$SQL_CON->real_escape_string($sCsvList[" + tIndex.ToString() + "]).'\\'");
                tIndex++;
            }

            MethodInfo tMethodDeclareFunctions = NWDAliasMethod.GetMethod(tType, NWDConstants.M_AddonPhpFunctions, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            if (tMethodDeclareFunctions != null)
            {
                tFile.Append((string)tMethodDeclareFunctions.Invoke(null, new object[] { sEnvironment }));
            }
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('DEBUG TRACE', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("global $SQL_CON, $WSBUILD, $ENV, $NWD_SLT_SRV, $TIME_SYNC, $NWD_FLOAT_FORMAT, $ACC_NEEDED, $PATH_BASE, $REF_NEEDED, $REP;");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB, $SQL_" + tClassName + "_WebService;");
            tFile.AppendLine("global $admin, $uuid;");
            tFile.AppendLine("if (Integrity" + tClassName + "Test ($sCsv) == true)");
            tFile.AppendLine("{");
            tFile.AppendLine("$sCsvList = Prepare" + tClassName + "Data($sCsv);");
            tFile.AppendLine("if (count ($sCsvList) != " + tColumnNameList.Count.ToString() + ")");
            tFile.AppendLine("{");
            tFile.AppendLine("error('" + tTrigramme + "x99');");
            tFile.AppendLine("}");
            tFile.AppendLine("else");
            tFile.AppendLine("{");
            tFile.AppendLine("$tReference = $sCsvList[0];");
            tFile.AppendLine("// find solution for pre calculate on server");

            MethodInfo tMethodDeclarePre = NWDAliasMethod.GetMethod(tType, NWDConstants.M_AddonPhpPreCalculate, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            if (tMethodDeclarePre != null)
            {
                tFile.Append((string)tMethodDeclarePre.Invoke(null, new object[] { sEnvironment }));
            }
            tFile.AppendLine("$tQuery = 'SELECT `Reference`, `DM` FROM `'.$ENV.'_" + tTableName + "` WHERE `Reference` = \\''.$SQL_CON->real_escape_string($tReference).'\\';';");
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("if (!$tResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tQuery.'', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("error('" + tTrigramme + "x31');");
            tFile.AppendLine("}");
            tFile.AppendLine("else");
            tFile.AppendLine("{");
            tFile.AppendLine("if ($tResult->num_rows <= 1)");
            tFile.AppendLine("{");
            tFile.AppendLine("if ($tResult->num_rows == 0)");
            tFile.AppendLine("{");
            tFile.AppendLine("$tInsert = 'INSERT INTO `'.$ENV.'_" + tTableName + "` (" + string.Join(", ", tColumnNameList.ToArray()) + ") VALUES (" + string.Join(", ", tColumnValueList.ToArray()) + ");';");
            tFile.AppendLine("$tInsertResult = $SQL_CON->query($tInsert);");
            tFile.AppendLine("if (!$tInsertResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tInsertResult.'', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("error('" + tTrigramme + "x32');");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine("else");
            tFile.AppendLine("{");
            //"while($tRow = $tResult->fetch_row())"+
            //"{" );
            tFile.Append("$tUpdate = 'UPDATE `'.$ENV.'_" + tTableName + "` SET ");
            tFile.Append(string.Join(", ", tModify.ToArray()) + " WHERE `Reference` = \\''.$SQL_CON->real_escape_string($tReference).'\\' ");
            if (sEnvironment == NWDAppConfiguration.SharedInstance().DevEnvironment)
            {
                // tSynchronizationFile += "AND (`DevSync`<= \\''.$SQL_CON->real_escape_string($sTimeStamp).'\\') "; 
                //no test the last is the winner!
            }
            else if (sEnvironment == NWDAppConfiguration.SharedInstance().PreprodEnvironment)
            {
                //tSynchronizationFile += "AND (`DevSync`<= \\''.$SQL_CON->real_escape_string($sTimeStamp).'\\' || `PreprodSync`<= \\''.$SQL_CON->real_escape_string($sTimeStamp).'\\') ";
                //tSynchronizationFile += "AND `PreprodSync`<= \\''.$SQL_CON->real_escape_string($sTimeStamp).'\\' ";
            }
            else if (sEnvironment == NWDAppConfiguration.SharedInstance().ProdEnvironment)
            {
                //tSynchronizationFile += "AND (`DevSync`<= \\''.$SQL_CON->real_escape_string($sTimeStamp).'\\' || `PreprodSync`<= \\''.$SQL_CON->real_escape_string($sTimeStamp).'\\' || `ProdSync`<= \\''.$SQL_CON->real_escape_string($sTimeStamp).'\\') ";
                //tSynchronizationFile += "AND `ProdSync`<= \\''.$SQL_CON->real_escape_string($sTimeStamp).'\\' ";
            }
            tFile.AppendLine("';");
            if (tAccountReference.Count == 0)
            {
                tFile.AppendLine("$tUpdateRestriction = '';");
            }
            else
            {
                tFile.AppendLine("$tUpdateRestriction = 'AND (" + string.Join(" OR ", tAccountReference.ToArray()) + ") ';");
            }
            tFile.AppendLine("if ($admin == false)");
            tFile.AppendLine("{");
            tFile.AppendLine("$tUpdate = $tUpdate.$tUpdateRestriction.' AND `WebServiceVersion` <= '.$WSBUILD.'';");
            tFile.AppendLine("}");
            //"else" );
            //"{" );
            //"//$tUpdate = $tUpdate.' AND `DM`<= \\''.$SQL_CON->real_escape_string($sCsvList[1]).'\\'';" );
            //"}" );
            tFile.AppendLine("$tUpdateResult = $SQL_CON->query($tUpdate);");
            tFile.AppendLine("if (!$tUpdateResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tUpdate.'', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("error('" + tTrigramme + "x38');");
            tFile.AppendLine("}");
            //"}" );
            tFile.AppendLine("}");
            tFile.AppendLine("// find solution for post calculate on server");
            MethodInfo tMethodDeclarePost = NWDAliasMethod.GetMethod(tType, NWDConstants.M_AddonPhpPostCalculate, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            if (tMethodDeclarePost != null)
            {
                tFile.Append((string)tMethodDeclarePost.Invoke(null, new object[] { sEnvironment }));
            }
            tFile.AppendLine("");
            tFile.AppendLine("$tLigneAffceted = $SQL_CON->affected_rows;");
            //"myLog('tLigneAffceted = '.$tLigneAffceted, __FILE__, __FUNCTION__, __LINE__);" );
            tFile.AppendLine("if ($tLigneAffceted == 1)");
            tFile.AppendLine("{");
            tFile.AppendLine("// je transmet la sync à tout le monde");
            tFile.AppendLine("if ($sCsvList[3] != -1)");
            tFile.AppendLine("{");
            tFile.AppendLine("$tUpdate = 'UPDATE `Dev_" + tTableName + "` SET `DS` = \\''.$TIME_SYNC.'\\',  `'.$ENV.'Sync` = \\''.$TIME_SYNC.'\\' WHERE `Reference` = \\''.$SQL_CON->real_escape_string($tReference).'\\'';");
            tFile.AppendLine("$tUpdateResult = $SQL_CON->query($tUpdate);");
            tFile.AppendLine("}");
            tFile.AppendLine("if ($sCsvList[4] != -1)");
            tFile.AppendLine("{");
            tFile.AppendLine("$tUpdate = 'UPDATE `Preprod_" + tTableName + "` SET `DS` = \\''.$TIME_SYNC.'\\',  `'.$ENV.'Sync` = \\''.$TIME_SYNC.'\\' WHERE `Reference` = \\''.$SQL_CON->real_escape_string($tReference).'\\'';");
            tFile.AppendLine("$tUpdateResult = $SQL_CON->query($tUpdate);");
            tFile.AppendLine("}");
            tFile.AppendLine("if ($sCsvList[5] != -1)");
            tFile.AppendLine("{");
            tFile.AppendLine("$tUpdate = 'UPDATE `Prod_" + tTableName + "` SET `DS` = \\''.$TIME_SYNC.'\\',  `'.$ENV.'Sync` = \\''.$TIME_SYNC.'\\' WHERE `Reference` = \\''.$SQL_CON->real_escape_string($tReference).'\\'';");
            tFile.AppendLine("$tUpdateResult = $SQL_CON->query($tUpdate);");
            tFile.AppendLine("}");
            tFile.AppendLine("}");

            tFile.AppendLine("");
            tFile.AppendLine("}");
            tFile.AppendLine("else");
            tFile.AppendLine("{");
            tFile.AppendLine("error('" + tTrigramme + "x39');");
            tFile.AppendLine("}");
            tFile.AppendLine("mysqli_free_result($tResult);");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("function FlushTrashedDatas" + tClassName + " ()");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('DEBUG TRACE', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("global $SQL_CON, $ENV;");
            tFile.AppendLine("$tQuery = 'DELETE FROM `'.$ENV.'_" + tTableName + "` WHERE XX>0';");
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("if (!$tResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("error('" + tTrigramme + "x40');");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("function GetDatas" + tClassName + "ByReference ($sReference)");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('DEBUG TRACE', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("global $SQL_CON, $WSBUILD, $ENV, $REF_NEEDED, $ACC_NEEDED, $uuid;");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB,$SQL_" + tClassName + "_WebService;");
            tFile.AppendLine("global $REP;");
            tFile.AppendLine("global $admin;");
            //"$tPage = $sPage*$sLimit;" );
            tFile.AppendLine("$tQuery = 'SELECT " + SLQAssemblyOrder() + " FROM `'.$ENV.'_" + tTableName + "` WHERE Reference = \\''.$SQL_CON->real_escape_string($sReference).'\\'';");
            tFile.AppendLine("if ($admin == false)");
            tFile.AppendLine("{");
            tFile.AppendLine("$tQuery = $tQuery.' AND `WebServiceVersion` <= '.$WSBUILD.';';");
            tFile.AppendLine("}");
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("if (!$tResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tQuery.'', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("error('" + tTrigramme + "x33');");
            tFile.AppendLine("}");
            tFile.AppendLine("else");
            tFile.AppendLine("{");
            tFile.AppendLine("while($tRow = $tResult->fetch_row())");
            tFile.AppendLine("{");
            tFile.AppendLine("$REP['" + tClassName + "'][] = implode('" + NWDConstants.kStandardSeparator + "',$tRow);");
            tFile.AppendLine("}");
            string tSpecialAdd = string.Empty;
            foreach (PropertyInfo tProp in tType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (tProp.GetCustomAttributes(typeof(NWDNeedUserAvatarAttribute), true).Length > 0)
                {
                    tSpecialAdd += NWDNeedUserAvatarAttribute.PHPstring(tProp.Name);
                }
                if (tProp.GetCustomAttributes(typeof(NWDNeedAccountNicknameAttribute), true).Length > 0)
                {
                    tSpecialAdd += NWDNeedAccountNicknameAttribute.PHPstring(tProp.Name);
                }
                if (tProp.GetCustomAttributes(typeof(NWDNeedReferenceAttribute), true).Length > 0)
                {
                    foreach (NWDNeedReferenceAttribute tReference in tProp.GetCustomAttributes(typeof(NWDNeedReferenceAttribute), true))
                    {
                        tSpecialAdd += tReference.PHPstring(tProp.Name);
                    }
                }
            }
            if (tSpecialAdd != string.Empty)
            {
                tFile.AppendLine("$tResult->data_seek(0);while($tRow = $tResult->fetch_assoc()){" + tSpecialAdd + "}");
            }
            tFile.AppendLine("mysqli_free_result($tResult);");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("function GetDatas" + tClassName + "ByReferences ($sReferences)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $SQL_CON, $WSBUILD, $ENV, $REF_NEEDED, $ACC_NEEDED, $uuid;");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB,$SQL_" + tClassName + "_WebService;");
            tFile.AppendLine("global $REP;");
            tFile.AppendLine("global $admin;");
            //"$tPage = $sPage*$sLimit;" );
            tFile.AppendLine("$tQuery = 'SELECT " + SLQAssemblyOrder() + " FROM `'.$ENV.'_" + tTableName + "` WHERE Reference IN ( \\''.implode('\\', \\'', $sReferences).'\\')';");
            tFile.AppendLine("if ($admin == false)");
            tFile.AppendLine("{");
            tFile.AppendLine("$tQuery = $tQuery.' AND `WebServiceVersion` <= '.$WSBUILD.';';");
            tFile.AppendLine("}");
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("if (!$tResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tQuery.'', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("error('" + tTrigramme + "x33');");
            tFile.AppendLine("}");
            tFile.AppendLine("else");
            tFile.AppendLine("{");
            tFile.AppendLine("while($tRow = $tResult->fetch_row())");
            tFile.AppendLine("{");
            tFile.AppendLine("$REP['" + tClassName + "'][] = implode('" + NWDConstants.kStandardSeparator + "',$tRow);");
            tFile.AppendLine("}");
            if (tSpecialAdd != string.Empty)
            {
                tFile.AppendLine("$tResult->data_seek(0);while($tRow = $tResult->fetch_assoc()){" + tSpecialAdd + "}");
            }

            tFile.AppendLine("mysqli_free_result($tResult);");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("function GetDatas" + tClassName + " ($sTimeStamp, $sAccountReference)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $SQL_CON, $WSBUILD, $ENV, $REF_NEEDED, $ACC_NEEDED, $uuid;");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB,$SQL_" + tClassName + "_WebService;");
            tFile.AppendLine("global $REP;");
            tFile.AppendLine("global $admin;");
            //"$tPage = $sPage*$sLimit;" );
            tFile.Append("$tQuery = 'SELECT " + SLQAssemblyOrder() + " FROM `'.$ENV.'_" + tTableName + "` WHERE ");
            //"(`'.$ENV.'Sync` >= \\''.$SQL_CON->real_escape_string($sTimeStamp).'\\' OR `DS` >= \\''.$SQL_CON->real_escape_string($sTimeStamp).'\\')";
            tFile.Append("(`'.$ENV.'Sync` >= \\''.$SQL_CON->real_escape_string($sTimeStamp).'\\')");
            // if need Account reference
            if (tAccountReference.Count == 0)
            {
            }
            else
            {
                tFile.Append("AND (" + string.Join("OR ", tAccountReference.ToArray()) + ") ");
            }
            tFile.AppendLine("';");
            tFile.AppendLine("if ($admin == false)");
            tFile.AppendLine("{");
            tFile.AppendLine("$tQuery = $tQuery.' AND `WebServiceVersion` <= '.$WSBUILD.';';");
            tFile.AppendLine("}");
            // I do the result operation
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("if (!$tResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tQuery.'', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("error('" + tTrigramme + "x33');");
            tFile.AppendLine("}");
            tFile.AppendLine("else");
            tFile.AppendLine("{");
            tFile.AppendLine("while($tRow = $tResult->fetch_row())");
            tFile.AppendLine("{");
            tFile.AppendLine("$REP['" + tClassName + "'][] = implode('" + NWDConstants.kStandardSeparator + "',$tRow);");
            tFile.AppendLine("}");
            if (tSpecialAdd != string.Empty)
            {
                tFile.AppendLine("$tResult->data_seek(0);while($tRow = $tResult->fetch_assoc()){" + tSpecialAdd + "}");
            }
            tFile.AppendLine("mysqli_free_result($tResult);");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("function GetDatas" + tClassName + "ByAccounts ($sTimeStamp, $sAccountReferences)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $SQL_CON, $WSBUILD, $ENV, $REF_NEEDED, $ACC_NEEDED, $uuid;");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB, $SQL_" + tClassName + "_WebService;");
            tFile.AppendLine("global $REP;");
            //"$tPage = $sPage*$sLimit;" );
            tFile.Append("$tQuery = 'SELECT " + SLQAssemblyOrder() + " FROM `'.$ENV.'_" + tTableName + "` WHERE ");
            //"(`'.$ENV.'Sync` >= \\''.$SQL_CON->real_escape_string($sTimeStamp).'\\' OR `DS` >= \\''.$SQL_CON->real_escape_string($sTimeStamp).'\\')";
            tFile.Append("(`'.$ENV.'Sync` >= \\''.$SQL_CON->real_escape_string($sTimeStamp).'\\')");
            // if need Account reference
            if (tAccountReferences.Count == 0)
            {
            }
            else
            {
                tFile.Append("AND (" + string.Join("OR ", tAccountReferences.ToArray()) + ") ");
            }
            tFile.AppendLine(" AND `WebServiceVersion` <= '.$SQL_" + tClassName + "_WebService.';';");
            // I do the result operation
            tFile.AppendLine("$tResult = $SQL_CON->query($tQuery);");
            tFile.AppendLine("if (!$tResult)");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('error in mysqli request : ('. $SQL_CON->errno.')'. $SQL_CON->error.'  in : '.$tQuery.'', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("error('" + tTrigramme + "x33');");
            tFile.AppendLine("}");
            tFile.AppendLine("else");
            tFile.AppendLine("{");
            tFile.AppendLine("while($tRow = $tResult->fetch_row())");
            tFile.AppendLine("{");
            tFile.AppendLine("$REP['" + tClassName + "'][] = implode('" + NWDConstants.kStandardSeparator + "',$tRow);");
            tFile.AppendLine("}");
            if (tSpecialAdd != string.Empty)
            {
                tFile.AppendLine("$tResult->data_seek(0);while($tRow = $tResult->fetch_assoc()){" + tSpecialAdd + "}");
            }
            tFile.AppendLine("mysqli_free_result($tResult);");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("function Special" + tClassName + " ($sTimeStamp, $sAccountReferences)");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('DEBUG TRACE', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("global $SQL_CON, $WSBUILD, $ENV, $NWD_SLT_SRV, $TIME_SYNC, $NWD_FLOAT_FORMAT, $ACC_NEEDED, $PATH_BASE, $REF_NEEDED, $REP;");
            tFile.AppendLine("global $SQL_" + tClassName + "_SaltA, $SQL_" + tClassName + "_SaltB, $SQL_" + tClassName + "_WebService;");
            tFile.AppendLine("global $admin, $uuid;");
            MethodInfo tMethodDeclareSpecial = NWDAliasMethod.GetMethod(tType, NWDConstants.M_AddonPhpSpecialCalculate, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            if (tMethodDeclareSpecial != null)
            {
                tFile.Append((string)tMethodDeclareSpecial.Invoke(null, new object[] { sEnvironment }));
            }
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);


            tFile.AppendLine("function Synchronize" + tClassName + " ($sJsonDico, $sAccountReference, $sAdmin)");
            tFile.AppendLine("{");
            tFile.AppendLine("global $token_FirstUse,$PATH_BASE;");

            if (tType.GetCustomAttributes(typeof(NWDForceSecureDataAttribute), true).Length > 0)
            {
                tFile.AppendLine("respondAdd('securePost',true);");
            }

            NWDOperationSpecial tOperation = NWDOperationSpecial.None;
            tFile.AppendLine("if ($sAdmin == true)");
            tFile.AppendLine("{");
            tFile.AppendLine("$sAccountReference = '%';");

            // Clean data?
            tOperation = NWDOperationSpecial.Clean;
            tFile.AppendLine("if (isset($sJsonDico['" + tClassName + "']['" + tOperation.ToString().ToLower() + "']))");
            tFile.AppendLine("{");
            tFile.AppendLine("if (!errorDetected())");
            tFile.AppendLine("{");
            tFile.AppendLine("FlushTrashedDatas" + tClassName + " ();");
            tFile.AppendLine("myLog('SPECIAL : CLEAN', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("}");
            tFile.AppendLine("}");

            //Special?
            tOperation = NWDOperationSpecial.Special;
            tFile.AppendLine("if (isset($sJsonDico['" + tClassName + "']['" + tOperation.ToString().ToLower() + "']))");
            tFile.AppendLine("{");
            tFile.AppendLine("if (!errorDetected())");
            tFile.AppendLine("{");
            tFile.AppendLine("Special" + tClassName + " ($sJsonDico['" + tClassName + "']['sync'], $sAccountReference);");
            tFile.AppendLine("myLog('SPECIAL : SPECIAL', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("}");
            tFile.AppendLine("}");

            //Upgrade?
            tOperation = NWDOperationSpecial.Upgrade;
            tFile.AppendLine("if (isset($sJsonDico['" + tClassName + "']['" + tOperation.ToString().ToLower() + "']))");
            tFile.AppendLine("{");
            tFile.AppendLine("if (!errorDetected())");
            tFile.AppendLine("{");
            tFile.AppendLine("include_once ($PATH_BASE.'/" + sEnvironment.Environment + "/" + NWD.K_DB + "/" + tClassName + "/" + NWD.K_MANAGEMENT_FILE + "');");
            tFile.AppendLine("Create" + tClassName + "Table ();");
            tFile.AppendLine("myLog('SPECIAL : UPGRADE OR CREATE TABLE', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("}");
            tFile.AppendLine("}");

            //Optimize?
            tOperation = NWDOperationSpecial.Optimize;
            tFile.AppendLine("if (isset($sJsonDico['" + tClassName + "']['" + tOperation.ToString().ToLower() + "']))");
            tFile.AppendLine("{");
            tFile.AppendLine("if (!errorDetected())");
            tFile.AppendLine("{");
            tFile.AppendLine("include_once ($PATH_BASE.'/" + sEnvironment.Environment + "/" + NWD.K_DB + "/" + tClassName + "/" + NWD.K_MANAGEMENT_FILE + "');");
            tFile.AppendLine("Defragment" + tClassName + "Table ();");
            tFile.AppendLine("myLog('SPECIAL : OPTIMIZE AND DEFRAGMENT TABLE', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("}");
            tFile.AppendLine("}");

            // ENDIF ADMIN OPERATION
            tFile.AppendLine("}");
            tFile.AppendLine("if ($token_FirstUse == true)");
            tFile.AppendLine("{");

            if (tINeedAdminAccount == true)
            {
                tFile.AppendLine("if ($sAdmin == true){");
            }
            tFile.AppendLine("if (isset($sJsonDico['" + tClassName + "']))");
            tFile.AppendLine("{");
            tFile.AppendLine("if (isset($sJsonDico['" + tClassName + "']['data']))");
            tFile.AppendLine("{");
            tFile.AppendLine("foreach ($sJsonDico['" + tClassName + "']['data'] as $sCsvValue)");
            tFile.AppendLine("{");
            tFile.AppendLine("if (!errorDetected())");
            tFile.AppendLine("{");
            tFile.AppendLine("UpdateData" + tClassName + " ($sCsvValue, $sJsonDico['" + tClassName + "']['sync'], $sAccountReference, $sAdmin);");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            if (tINeedAdminAccount == true)
            {
                tFile.AppendLine("}");
            }
            tFile.AppendLine("}");
            tFile.AppendLine("else");
            tFile.AppendLine("{");
            tFile.AppendLine("myLog('NOT UPDATE, SPECIAL OR CLEAN ACTION ... YOU USE OLDEST TOKEN', __FILE__, __FUNCTION__, __LINE__);");
            tFile.AppendLine("}");
            tFile.AppendLine("if (isset($sJsonDico['" + tClassName + "']))");
            tFile.AppendLine("{");
            tFile.AppendLine("if (isset($sJsonDico['" + tClassName + "']['sync']))");
            tFile.AppendLine("{");
            tFile.AppendLine("if (!errorDetected())");
            tFile.AppendLine("{");
            tFile.AppendLine("GetDatas" + tClassName + " ($sJsonDico['" + tClassName + "']['sync'], $sAccountReference);");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine("}");
            tFile.AppendLine(NWD.K_CommentSeparator);

            tFile.AppendLine("?>");

            string tFileFormatted = NWDToolbox.CSharpFormat(tFile.ToString());
            rReturn.Add(tClassName + "/" + NWD.K_WS_SYNCHRONISATION, tFileFormatted);
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        [NWDAliasMethod(NWDConstants.M_BasisCreatePHP)]
        public static Dictionary<string, string> CreatePHP(NWDAppEnvironment sEnvironment, bool sPrepareOrder = true)
        {
            Dictionary<string, string> rReturn = new Dictionary<string, string>();
            Datas().PrefLoad();
            if (sPrepareOrder == true)
            {
                PrepareOrders();
            }
            foreach (KeyValuePair<string, string> tKeyValue in CreatePHPConstant(sEnvironment))
            {
                rReturn.Add(tKeyValue.Key, tKeyValue.Value);
            }
            foreach (KeyValuePair<string, string> tKeyValue in CreatePHPManagement(sEnvironment))
            {
                rReturn.Add(tKeyValue.Key, tKeyValue.Value);
            }
            foreach (KeyValuePair<string, string> tKeyValue in CreatePHPSynchronisation(sEnvironment))
            {
                rReturn.Add(tKeyValue.Key, tKeyValue.Value);
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif