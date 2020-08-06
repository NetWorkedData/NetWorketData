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
#else
#undef NWD_LOG
#undef NWD_BENCHMARK
#endif
//=====================================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    ////[NWDClassServerSynchronizeAttribute(false)]// ALLWAYS FALSE! Never sync this data! It's local only
    [NWDClassTrigrammeAttribute("BSP")]
    [NWDClassDescriptionAttribute("Basis Preferences Class! Never use by yourself in game!")]
    [NWDClassMenuNameAttribute("Basis Preferences")]
    //public partial class NWDBasisPreferences : NWDBasisAccountUnsynchronize
    public partial class NWDBasisPreferences : NWDBasisUnsynchronize
    {
        //-------------------------------------------------------------------------------------------------------------
        //[NWDCertified]
        //public string Environment
        //{
        //    get; set;
        //}
        [NWDCertified]
        public string StringValue
        {
            get; set;
        }
        [NWDCertified]
        public int IntValue
        {
            get; set;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
