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

//=====================================================================================================================
//namespace MacroDefineEditor
//{
//    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//    // You can create custom enum of macro
//    // Just follow this example class
//    public class GNC_GENETIC_DefineBool : MDEDataTypeBoolGeneric<GNC_GENETIC_DefineBool>
//    {
//        //-------------------------------------------------------------------------------------------------------------
//        // the title of enum controller
//        public static string Title = SetTitle("GNC_GENETIC");
//        //-------------------------------------------------------------------------------------------------------------
//        // declare one value
//        public static GNC_GENETIC_DefineBool Macro = SetValue("GNC_GENETIC");
//        //-------------------------------------------------------------------------------------------------------------
//    }
//    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//}
//=====================================================================================================================

#if GNC_GENETIC
//=====================================================================================================================

using System;
using UnityEngine;
using System.Collections.Generic;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class GNCGeneticIndividualHelper : NWDHelper<GNCGeneticIndividual>
    {
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// GNCGeneticIndividual class. This class is use for (complete description here).
    /// </summary>
    [NWDClassMacroAttribute("GNC_GENETIC")]
    [NWDClassTrigrammeAttribute("GNCI")]
    [NWDClassDescriptionAttribute("")]
    [NWDClassMenuNameAttribute("Genetic Individual")]
    public partial class GNCGeneticIndividual : NWDBasisGameSaveDependent
    {
        #warning YOU MUST FOLLOW THIS INSTRUCTIONS
        //-------------------------------------------------------------------------------------------------------------
        // YOU MUST GENERATE PHP FOR THIS CLASS AFTER FIELD THIS CLASS WITH YOUR PROPERTIES
        // YOU MUST GENERATE WEBSITE AND UPLOAD THE FOLDER ON YOUR SERVER
        // YOU MUST UPDATE TABLE ON THE SERVER WITH THE MENU FOR DEV, FOR PREPROD AND FOR PROD
        //-------------------------------------------------------------------------------------------------------------
        #region Properties
        //-------------------------------------------------------------------------------------------------------------
        // Your properties
        //-------------------------------------------------------------------------------------------------------------

        //PROPERTIES
		//[NWDInspectorGroupReset]
		public NWDReferenceType<GNCSpecie> Specie {get; set;}
		public NWDReferencesListType<GNCGene> PaternalHeritage {get; set;}
		public NWDReferencesListType<GNCGene> MaternalHeritage {get; set;}
		public float CytoplasmicMaleSterility {get; set;}


        //-------------------------------------------------------------------------------------------------------------
        #endregion
        //-------------------------------------------------------------------------------------------------------------
        #region Constructors
        //-------------------------------------------------------------------------------------------------------------
        // never change the constructors! they are used by the NetWorkedData Writing System
        //-------------------------------------------------------------------------------------------------------------
        public GNCGeneticIndividual()
        {
            //Debug.Log("GNCGeneticIndividual Constructor");
        }
        //-------------------------------------------------------------------------------------------------------------
        public GNCGeneticIndividual(bool sInsertInNetWorkedData) : base(sInsertInNetWorkedData)
        {
            //Debug.Log("GNCGeneticIndividual Constructor with sInsertInNetWorkedData : " + sInsertInNetWorkedData.ToString() + "");
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif //GNC_GENETIC
