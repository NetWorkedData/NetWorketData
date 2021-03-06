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
#if NWD_RGPD
#if UNITY_EDITOR
using System;
//=====================================================================================================================
using UnityEditor;
using NetWorkedData.NWDEditor;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [NWDTypeWindowParamAttribute(
            "RGPD",
            "Consent Managment",
        new Type[] {
            typeof(NWDConsent),
            typeof(NWDAccountConsent),
		}
    )]
    public class NWDConsentWindow : NWDBasisWindow<NWDConsentWindow>
    {
        //-------------------------------------------------------------------------------------------------------------
        const string K_APP_MENU = "Consents" + NWDConstants.K_MENU_BASIS_WINDOWS_MANAGEMENT;
        //-------------------------------------------------------------------------------------------------------------
        [MenuItem(NWDEditorMenu.K_NETWORKEDDATA + K_APP_MENU + "/Consents key", false, NWDEditorMenu.K_ENGINE_MANAGEMENT_INDEX + 1)]
        public static void MenuMethodVersion()
        {
            ShowWindow(typeof(NWDConsent));
        }
        //-------------------------------------------------------------------------------------------------------------
        [MenuItem(NWDEditorMenu.K_NETWORKEDDATA + K_APP_MENU + "/Account's consents", false, NWDEditorMenu.K_ENGINE_MANAGEMENT_INDEX + 1)]
        public static void MenuMethodPreferenceKey()
        {
            ShowWindow(typeof(NWDAccountConsent));
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif
#endif
