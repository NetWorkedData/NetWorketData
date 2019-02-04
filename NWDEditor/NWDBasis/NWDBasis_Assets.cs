﻿//=====================================================================================================================
//
// ideMobi copyright 2018 
// All rights reserved by ideMobi
//
//=====================================================================================================================
#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using SQLite4Unity3d;
using BasicToolBox;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDBasis<K> : NWDTypeClass where K : NWDBasis<K>, new()
    {
        //-------------------------------------------------------------------------------------------------------------
        [NWDAliasMethod(NWDConstants.M_ChangeAssetPath)]
        public static void ChangeAssetPath(string sOldPath, string sNewPath)
        {
            //Debug.Log (ClassName () +" ChangeAssetPath " + sOldPath + " to " + sNewPath);
            if (AssetDependent() == true)
            {
                foreach (NWDBasis<K> tObject in NWDBasis<K>.BasisHelper().Datas)
                {
                    tObject.ChangeAssetPathMe(sOldPath, sNewPath);
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void ChangeAssetPathMe(string sOldPath, string sNewPath)
        {
            //Debug.Log (ClassName () +" ChangeAssetPathMe " + sOldPath + " to " + sNewPath);
            if (TestIntegrity() == true)
            {
                bool tUpdate = false;
                if (Preview != null)
                {
                    if (Preview.Contains(sOldPath))
                    {
                        Preview = Preview.Replace(sOldPath, sNewPath);
                        tUpdate = true;
                    }
                }
                foreach (var tProp in PropertiesAssetDependent())
                {
                    Type tTypeOfThis = tProp.PropertyType;
                    NWDAssetType tValueStruct = (NWDAssetType)tProp.GetValue(this, null);
                    if (tValueStruct != null)
                    {
                        if (tValueStruct.ChangeAssetPath(sOldPath, sNewPath))
                        {
                            tUpdate = true;
                        }
                    }
                }
                if (tUpdate == true)
                {
                    UpdateData(true, NWDWritingMode.ByDefaultLocal);
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif