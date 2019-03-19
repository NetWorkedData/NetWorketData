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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using BasicToolBox;
using UnityEditor;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDNews : NWDBasis<NWDNews>
    {
        //-------------------------------------------------------------------------------------------------------------
        [NWDAliasMethod(NWDConstants.M_AddonPhpPreCalculate)]
        public static string AddonPhpPreCalculate(NWDAppEnvironment AppEnvironment)
        {
            // "function UpdateData" + tClassName + " ($sCsv, $sTimeStamp, $sAccountReference, $sAdmin)\n" 
            //"\t ..."
            //"\t\t\t\t$sCsvList = Prepare" + tClassName + "Data($sCsv);\n"
            //"\t ..."
            return "// write your php script string here to update $tReference before sync on server\n";
            //"\t ..."
            //"\t Datas Updated"
            //"\t ..."
            //"\t}\n"
        }
        //-------------------------------------------------------------------------------------------------------------
        [NWDAliasMethod(NWDConstants.M_AddonPhpPostCalculate)]
        public static string AddonPhpPostCalculate(NWDAppEnvironment AppEnvironment)
        {
            // "function UpdateData" + tClassName + " ($sCsv, $sTimeStamp, $sAccountReference, $sAdmin)\n" 
            //"\t{\n" 
            //"\t ..."
            //"\t\t\t\t$sCsvList = Prepare" + tClassName + "Data($sCsv);\n"
            //"\t ..."
            //"\t Datas Updated"
            //"\t ..."
            return "// write your php script string here to update afetr sync on server\n";
            //"\t ..."
            //"\t}\n"
        }
        //-------------------------------------------------------------------------------------------------------------
        [NWDAliasMethod(NWDConstants.M_AddonPhpGetCalculate)]
        public static string AddonPhpGetCalculate(NWDAppEnvironment AppEnvironment)
        {
            //"while($tRow = $tResult->fetch_row()")
            //"{"
            return "// write your php script string here to special operation, example : \n$REP['" + BasisHelper().ClassName + " After Get'] ='success!!!';\n";
            //"\t}\n"
        }
        //-------------------------------------------------------------------------------------------------------------
        [NWDAliasMethod(NWDConstants.M_AddonPhpSpecialCalculate)]
        public static string AddonPhpSpecialCalculate(NWDAppEnvironment AppEnvironment)
        {
            // TODO : create and send push notification loop!
            //"function Special" + tClassName + " ($sTimeStamp, $sAccountReferences)\n" 
            //"\t{\n" 
            return "// write your php script string here to special operation, example : \n$REP['" + BasisHelper().ClassName + " Special'] ='success!!!';\n";
            //"\t}\n"
        }
        //-------------------------------------------------------------------------------------------------------------
        [NWDAliasMethod(NWDConstants.M_AddonPhpFunctions)]
        public static string AddonPhpFunctions(NWDAppEnvironment AppEnvironment)
        {
            return "// write your php script string here to add function in php file;\n";
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif