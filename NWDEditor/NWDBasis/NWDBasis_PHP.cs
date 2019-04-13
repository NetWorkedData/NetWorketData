﻿// =====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:22:7
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
// =====================================================================================================================
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SQLite4Unity3d;
using BasicToolBox;
using UnityEditor;
using System.Text;
using UnityEngine;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDBasis<K> : NWDTypeClass where K : NWDBasis<K>, new()
    {
        //-------------------------------------------------------------------------------------------------------------
        static K sFictive;
        //-------------------------------------------------------------------------------------------------------------
        public static K FictiveData()
        {
            //BTBBenchmark.Start();
            if (sFictive == null)
            {
                sFictive = NewDataWithReference("FICTIVE");
                sFictive.DeleteData();
                //Debug.Log("Test Fictive Data : " + NWDToolbox.ExposeProperty(() => sFictive.Reference));
            }
            //BTBBenchmark.Finish();
            return sFictive;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif