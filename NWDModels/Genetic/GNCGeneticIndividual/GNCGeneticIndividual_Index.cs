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
#endif

#if GNC_GENETIC
//=====================================================================================================================

using System;
using System.Collections.Generic;
using UnityEngine;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Example with fictive class NWDSomething
    // Connect by property Something
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /*
    public partial class NWDSomething : NWDBasisGameSaveDependent
    {
        //-------------------------------------------------------------------------------------------------------------
        public NWDSomething()
        {
            //Debug.Log("NWDSomething Constructor");
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDSomething(bool sInsertInNetWorkedData) : base(sInsertInNetWorkedData)
        {
            //Debug.Log("NWDSomething Constructor with sInsertInNetWorkedData : " + sInsertInNetWorkedData.ToString() + "");
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class GNCGeneticIndividual : NWDBasisGameSaveDependent
    {
        NWDReferenceType<NWDSomething> SomethingReference { set; get; }
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class GNCGeneticIndividual : NWDBasisGameSaveDependent
    {
        //-------------------------------------------------------------------------------------------------------------
        static protected NWDIndex<NWDLevel, GNCGeneticIndividual> kLevelIndex = new NWDIndex<NWDLevel, GNCGeneticIndividual>();
        //-------------------------------------------------------------------------------------------------------------
        [NWDIndexInMemory]
        public void InsertInLevelIndex()
        {
            // Re-add to the actual indexation ?
            if (IsUsable())
            {
                // Re-add !
                kLevelIndex.UpdateData(this, SomethingReference.GetReference());
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        [NWDDeindexInMemory]
        public void RemoveFromLevelIndex()
        {
            // Remove from the actual indexation
            kLevelIndex.RemoveData(this);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static GNCGeneticIndividual FindFirstDataBySomething(NWDLevel sKey, bool sOrCreate = false)
        {
            GNCGeneticIndividual rReturn = kLevelIndex.FirstRawDataByKey(sKey.Reference);
            if (rReturn == null && sOrCreate == true)
            {
                rReturn = new GNCGeneticIndividual();
                rReturn.SaveData();
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    */
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif //GNC_GENETIC
