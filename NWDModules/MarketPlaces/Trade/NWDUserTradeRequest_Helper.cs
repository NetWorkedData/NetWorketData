﻿//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:50:0
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;

//=====================================================================================================================
namespace NetWorkedData
{
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDUserTradeRequestHelper : NWDHelper<NWDUserTradeRequest>
    {
        //-------------------------------------------------------------------------------------------------------------
        public override List<Type>  OverrideClasseInThisSync()
        {
            return new List<Type> { typeof(NWDUserOwnership), typeof(NWDTradePlace), typeof(NWDUserTradeRequest), typeof(NWDUserTradeProposition), typeof(NWDUserTradeFinder) };
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================