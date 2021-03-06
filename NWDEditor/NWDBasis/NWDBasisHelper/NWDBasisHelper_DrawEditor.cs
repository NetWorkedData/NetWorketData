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
#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using UnityEngine;
//=====================================================================================================================
using UnityEditor;
using NetWorkedData.NWDEditor;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDBasisHelper
    {
        //-------------------------------------------------------------------------------------------------------------
        public void DrawTypeInformations()
        {
            GUILayout.BeginHorizontal();
            Texture2D tTextureOfClass = TextureOfClass();
            if (tTextureOfClass != null)
            {
                GUILayout.Label(tTextureOfClass, NWDGUI.KTableSearchClassIcon, GUILayout.Width(48.0F), GUILayout.Height(48.0F));
            }
            GUILayout.BeginVertical();
            GUILayout.Label(ClassMenuName, EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(ClassDescription, MessageType.None);
            GUILayout.Label("Webservice last version generated for this Class  is " + LastWebBuild.ToString() + " ( App use Webservice " + NWDAppConfiguration.SharedInstance().WebBuild.ToString() + ")");
            NWDGUILayout.Separator();
            foreach (KeyValuePair<int, string> tModels in WebModelSQLOrder)
            {
                GUILayout.Label("Model has definition for Webservice " + tModels.Key.ToString());
            }
            if (SaltValid == false)
            {
                if (NWDGUILayout.AlertBoxButton(NWDConstants.K_ALERT_SALT_SHORT_ERROR, NWDConstants.K_APP_CLASS_SALT_REGENERATE))
                {
                    NWDEditorWindow.GenerateCSharpFile();
                    GUIUtility.ExitGUI();
                }
            }
            if (string.IsNullOrEmpty(TablePrefix) == false)
            {
                GUILayout.Label(new GUIContent("Prefixe Table in WS : " + TablePrefix));
            }
            if (TablePrefix != TablePrefixOld)
            {
                if (NWDGUILayout.WarningBoxButton(NWDConstants.K_APP_BASIS_WARNING_PREFIXE + " TablePrefix ='" + TablePrefix + "' but TablePrefixOld ='" + TablePrefixOld + "'", NWDConstants.K_APP_WS_PHP_TOOLS.Replace("XXXX", NWDAppConfiguration.SharedInstance().WebBuild.ToString("0000"))))
                {
                    ForceOrders(NWDAppConfiguration.SharedInstance().WebBuild);
                    NWDAppConfiguration.SharedInstance().DevEnvironment.CreatePHP(new List<Type> { ClassType }, false, false);
                    NWDAppConfiguration.SharedInstance().PreprodEnvironment.CreatePHP(new List<Type> { ClassType }, false, false);
                    NWDAppConfiguration.SharedInstance().ProdEnvironment.CreatePHP(new List<Type> { ClassType }, false, false);
                    NWDEditorWindow.GenerateCSharpFile();
                    GUIUtility.ExitGUI();
                }
            }
            if (WebModelChanged == true)
            {
                // draw reintegrate the model
                if (NWDGUILayout.WarningBoxButton(NWDConstants.K_APP_BASIS_WARNING_MODEL + "\n" + ModelChangedGetChange(), NWDConstants.K_APP_WS_PHP_TOOLS.Replace("XXXX", NWDAppConfiguration.SharedInstance().WebBuild.ToString("0000"))))
                {
                    ForceOrders(NWDAppConfiguration.SharedInstance().WebBuild);
                    NWDAppConfiguration.SharedInstance().DevEnvironment.CreatePHP(new List<Type> { ClassType }, false, false);
                    NWDAppConfiguration.SharedInstance().PreprodEnvironment.CreatePHP(new List<Type> { ClassType }, false, false);
                    NWDAppConfiguration.SharedInstance().ProdEnvironment.CreatePHP(new List<Type> { ClassType }, false, false);
                    NWDEditorWindow.GenerateCSharpFile();
                    GUIUtility.ExitGUI();
                }
            }
            if (WebModelDegraded == true)
            {
                if (NWDGUILayout.WarningBoxButton(NWDConstants.K_APP_BASIS_WARNING_MODEL_DEGRADED + "\n" + ModelChangedGetChange(), NWDConstants.K_APP_WS_DELETE_OLD_MODEL_TOOLS))
                {
                    DeleteOldsModels();
                    NWDEditorWindow.GenerateCSharpFile();
                    GUIUtility.ExitGUI();
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void DrawTypeInInspector()
        {
            //NWDBenchmark.Start();
            DrawTypeInformations();
            if (SaltValid == false)
            {
                EditorGUILayout.HelpBox(NWDConstants.K_ALERT_SALT_SHORT_ERROR, MessageType.Error);
            }
            EditorGUILayout.LabelField(NWDConstants.K_APP_BASIS_CLASS_DESCRIPTION, EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(ClassDescription, MessageType.Info);
            if (NWDAppConfiguration.SharedInstance().DevEnvironment.Selected == true)
            {
                EditorGUILayout.LabelField(NWDConstants.K_APP_BASIS_CLASS_DEV, EditorStyles.boldLabel);
            }
            if (NWDAppConfiguration.SharedInstance().PreprodEnvironment.Selected == true)
            {
                EditorGUILayout.LabelField(NWDConstants.K_APP_BASIS_CLASS_PREPROD, EditorStyles.boldLabel);
            }
            if (NWDAppConfiguration.SharedInstance().ProdEnvironment.Selected == true)
            {
                EditorGUILayout.LabelField(NWDConstants.K_APP_BASIS_CLASS_PROD, EditorStyles.boldLabel);
            }
            GUIStyle tStyle = EditorStyles.foldout;
            FontStyle tPreviousStyle = tStyle.fontStyle;
            tStyle.fontStyle = FontStyle.Bold;
            mSettingsShowing = EditorGUILayout.Foldout(mSettingsShowing, NWDConstants.K_APP_BASIS_CLASS_WARNING_ZONE, tStyle);
            tStyle.fontStyle = tPreviousStyle;
            if (mSettingsShowing == true)
            {
                EditorGUILayout.HelpBox(NWDConstants.K_APP_BASIS_CLASS_WARNING_HELPBOX, MessageType.Warning);
                if (GUILayout.Button(NWDConstants.K_APP_BASIS_CLASS_RESET_TABLE, EditorStyles.miniButton))
                {
                    ResetTable();
                }
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(NWDConstants.K_APP_BASIS_CLASS_FIRST_SALT, SaltStart);
                if (GUILayout.Button(NWDConstants.K_APP_BASIS_CLASS_REGENERATE, EditorStyles.miniButton))
                {
                    GUI.FocusControl(null);
                    SaltStart = NWDToolbox.RandomString(UnityEngine.Random.Range(12, 24));
                    RecalculateAllIntegrities();
                    NWDEditorWindow.GenerateCSharpFile();
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(NWDConstants.K_APP_BASIS_CLASS_SECOND_SALT, SaltEnd);
                if (GUILayout.Button(NWDConstants.K_APP_BASIS_CLASS_REGENERATE, EditorStyles.miniButton))
                {
                    GUI.FocusControl(null);
                    SaltEnd = NWDToolbox.RandomString(UnityEngine.Random.Range(12, 24));
                    RecalculateAllIntegrities();
                    NWDEditorWindow.GenerateCSharpFile();
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                TablePrefix = EditorGUILayout.TextField(NWDConstants.K_APP_BASIS_CLASS_PREFIXE, TablePrefix);
                if (GUILayout.Button(NWDConstants.K_APP_BASIS_CLASS_RECCORD, EditorStyles.miniButton))
                {
                    GUI.FocusControl(null);
                    NWDEditorWindow.GenerateCSharpFile();
                }
                GUILayout.EndHorizontal();
                if (GUILayout.Button(NWDConstants.K_APP_BASIS_CLASS_INTEGRITY_REEVALUE, EditorStyles.miniButton))
                {
                    GUI.FocusControl(null);
                    RecalculateAllIntegrities();
                }
                if (GUILayout.Button("Reset Icon", EditorStyles.miniButton))
                {
                    ResetIconByDefaultIcon();
                }
                if (GUILayout.Button(NWDConstants.K_APP_WS_MODEL_TOOLS, NWDGUI.KTableSearchButton))
                {
                    ForceOrders(NWDAppConfiguration.SharedInstance().WebBuild);
                    NWDEditorWindow.GenerateCSharpFile();
                }
                // draw delete old model
                if (GUILayout.Button(NWDConstants.K_APP_WS_DELETE_OLD_MODEL_TOOLS, NWDGUI.KTableSearchButton))
                {
                    DeleteOldsModels();
                    ForceOrders(NWDAppConfiguration.SharedInstance().WebBuild);
                    NWDEditorWindow.GenerateCSharpFile();
                }
                if (GUILayout.Button(NWDConstants.K_APP_WS_PHP_TOOLS.Replace("XXXX", NWDAppConfiguration.SharedInstance().WebBuild.ToString("0000")), NWDGUI.KTableSearchButton))
                {
                    ForceOrders(NWDAppConfiguration.SharedInstance().WebBuild);
                    NWDAppConfiguration.SharedInstance().DevEnvironment.CreatePHP(new List<Type> { ClassType }, false, false);
                    NWDAppConfiguration.SharedInstance().PreprodEnvironment.CreatePHP(new List<Type> { ClassType }, false, false);
                    NWDAppConfiguration.SharedInstance().ProdEnvironment.CreatePHP(new List<Type> { ClassType }, false, false);
                    NWDEditorWindow.GenerateCSharpFile();
                }
                if (GUILayout.Button("Generate File Empty Template", NWDGUI.KTableSearchButton))
                {
                    GenerateFileEmptyTemplate();
                }
                if (GUILayout.Button("Generate File UnitTest", NWDGUI.KTableSearchButton))
                {
                    GenerateFileUnitTest();
                }
                if (GUILayout.Button("Generate File Connection", NWDGUI.KTableSearchButton))
                {
                    GenerateFileConnection();
                }
                if (GUILayout.Button("Generate File Workflow", NWDGUI.KTableSearchButton))
                {
                    GenerateFileWorkflow();
                }
                if (GUILayout.Button("Generate File Override", NWDGUI.KTableSearchButton))
                {
                    GenerateFileOverride();
                }
                if (GUILayout.Button("Generate File Extension", NWDGUI.KTableSearchButton))
                {
                    GenerateFileExtension();
                }
                if (GUILayout.Button("Generate File Helper", NWDGUI.KTableSearchButton))
                {
                    GenerateFileHelper();
                }
                if (GUILayout.Button("Generate File Editor", NWDGUI.KTableSearchButton))
                {
                    GenerateFileEditor();
                }
                if (GUILayout.Button("Generate File Index", NWDGUI.KTableSearchButton))
                {
                    GenerateFileIndex();
                }
                if (GUILayout.Button("Generate File PHP", NWDGUI.KTableSearchButton))
                {
                    GenerateFilePHP();
                }
                if (GUILayout.Button("Generate File Icon", NWDGUI.KTableSearchButton))
                {
                    GenerateFileIcon();
                }
            }
            //NWDBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void SelectedFirstObjectInTable(EditorWindow sEditorWindow)
        {
            //NWDBenchmark.Start();
            if (EditorTableDatas.Count > 0)
            {
                NWDTypeClass sObject = EditorTableDatas.ElementAt(0);
                SetObjectInEdition(sObject);
                sEditorWindow.Focus();
            }
            //NWDBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif
