﻿//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:42:15
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================

#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDExampleHelper : NWDHelper<NWDExample>
    {
        public override Dictionary<string, string> CreatePHPAddonFiles(NWDAppEnvironment sEnvironment, bool sWriteOnDisk = true)
        {
            Dictionary<string, string> rReturn = new Dictionary<string, string>(new StringIndexKeyComparer());
            StringBuilder tFile = new StringBuilder(string.Empty);
            tFile.AppendLine("<?php");
            tFile.AppendLine(sEnvironment.Headlines());
            tFile.AppendLine(NWD.K_CommentSeparator);
            tFile.AppendLine("// EXAMPLE ENGINE FILE");
            tFile.AppendLine(NWD.K_CommentSeparator);
            tFile.AppendLine("?>");
            string tFileFormatted = NWDToolbox.CSharpFormat(tFile.ToString());
            rReturn.Add(sEnvironment.EnvFolder(sWriteOnDisk) + "/NWDExample_engine.php", tFileFormatted);
            //NWD.K_DB
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override string AddonPhpPreCalculate(NWDAppEnvironment sEnvironment)
        {
            StringBuilder rReturn = new StringBuilder();
            rReturn.Append(base.AddonPhpPreCalculate(sEnvironment));
            // "function UpdateData" + tClassName + " ($sCsv, $sTimeStamp, $sAccountReference, $sAdmin)\n" 
            //"\t ..."
            //"\t\t\t\t$sCsvList = Prepare" + tClassName + "Data($sCsv);\n"
            //"\t ..."
            rReturn.AppendLine("// write your php script string here to update $tReference before sync on server");
            rReturn.AppendLine("// use public override string AddonPhpPreCalculate(NWDAppEnvironment sEnvironment)");
            //"\t ..."
            //"\t Datas Updated"
            //"\t ..."
            //"\t}\n"
            return rReturn.ToString();
        }
        //-------------------------------------------------------------------------------------------------------------
        public override string AddonPhpPostCalculate(NWDAppEnvironment sEnvironment)
        {
            StringBuilder rReturn = new StringBuilder();
            rReturn.Append(base.AddonPhpPostCalculate(sEnvironment));
            // "function UpdateData" + tClassName + " ($sCsv, $sTimeStamp, $sAccountReference, $sAdmin)\n" 
            //"\t{\n" 
            //"\t ..."
            //"\t\t\t\t$sCsvList = Prepare" + tClassName + "Data($sCsv);\n"
            //"\t ..."
            //"\t Datas Updated"
            //"\t ..."
            rReturn.AppendLine("// write your php script string here to update after sync on server");
            rReturn.AppendLine("// use public override string AddonPhpPostCalculate(NWDAppEnvironment sEnvironment)");
            //"\t ..."
            //"\t}\n"
            return rReturn.ToString();
        }
        //-------------------------------------------------------------------------------------------------------------
        public override string  AddonPhpGetCalculate(NWDAppEnvironment sEnvironment)
        {
            StringBuilder rReturn = new StringBuilder();
            rReturn.Append(base.AddonPhpGetCalculate(sEnvironment));
            rReturn.AppendLine("// use $tRow");
            rReturn.AppendLine("// write your php script string here to special operation, example : $REP['" + ClassNamePHP + " After Get'] ='success!!!';");
            rReturn.AppendLine("// use public override string  AddonPhpGetCalculate(NWDAppEnvironment sEnvironment)");
            return rReturn.ToString();
        }
        //-------------------------------------------------------------------------------------------------------------
        public override string  AddonPhpSpecialCalculate(NWDAppEnvironment sEnvironment)
        {
            StringBuilder rReturn = new StringBuilder();
            rReturn.Append(base.AddonPhpSpecialCalculate(sEnvironment));
            rReturn.AppendLine("// in function " + PHP_FUNCTION_SPECIAL() + " ($sTimeStamp, $sAccountReferences)");
            rReturn.AppendLine("// write your php script string here to special operation, example : $REP['" + ClassNamePHP + " Special'] ='success!!!';");
            rReturn.AppendLine("// use public override string  AddonPhpSpecialCalculate(NWDAppEnvironment sEnvironment)");
            return rReturn.ToString();
        }
        //-------------------------------------------------------------------------------------------------------------
        public override string  AddonPhpFunctions(NWDAppEnvironment sEnvironment)
        {
            StringBuilder rReturn = new StringBuilder();
            rReturn.Append(base.AddonPhpFunctions(sEnvironment));
            rReturn.AppendLine("function AddOnOne" + ClassNamePHP + " ($sTimeStamp, $sAccountReferences)");
            rReturn.AppendLine("{");
            rReturn.AppendLine("// write your php script string here to special operation, example : $REP['" + ClassNamePHP + " Special'] ='success!!!';");
            rReturn.AppendLine("// use public override string  AddonPhpFunctions(NWDAppEnvironment sEnvironment)");
            rReturn.AppendLine("}");
            return rReturn.ToString();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif