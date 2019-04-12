﻿// =====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:29:25
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
// =====================================================================================================================

//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using BasicToolBox;
using System.Text;

//=====================================================================================================================
namespace NetWorkedData
{

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDAccountNicknameHelper : NWDHelper<NWDAccountNickname>
    {
        //-------------------------------------------------------------------------------------------------------------
        public override string New_AddonPhpPostCalculate(NWDAppEnvironment AppEnvironment)
        {
            StringBuilder rReturn = new StringBuilder();
            rReturn.AppendLine("if (UniquePropertyValueFromValue($ENV.'_NWDAccountNickname', 'Nickname', 'UniqueNickname', $tReference) == true)");
            rReturn.AppendLine("{");
            rReturn.AppendLine("IntegrityNWDAccountNicknameReevalue($tReference);");
            rReturn.AppendLine("}");
            return rReturn.ToString();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif