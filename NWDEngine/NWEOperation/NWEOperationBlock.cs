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

using System.Collections.Generic;

using UnityEngine;

//=====================================================================================================================
namespace NetWorkedData
{
	//-------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// NWE operation block.
	/// </summary>
	public delegate void NWEOperationBlock(NWEOperation sOperation, float sProgress = 0.0f, NWEOperationResult sInfos = null);
	//-------------------------------------------------------------------------------------------------------------
}
//=====================================================================================================================
