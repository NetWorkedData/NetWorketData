﻿//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:48:53
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
public partial class NWDUserBarterFinderHelper : NWDHelper<NWDUserBarterFinder>
    {
        //-------------------------------------------------------------------------------------------------------------
public override List<Type>  OverrideClasseInThisSync()
        {
            return new List<Type> { typeof(NWDUserBarterFinder), typeof(NWDUserOwnership), typeof(NWDBarterPlace), typeof(NWDUserBarterRequest), typeof(NWDUserBarterProposition) };
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================