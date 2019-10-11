//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:48:39
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================

//=====================================================================================================================
//
// ideMobi copyright 2019
// All rights reserved by ideMobi
//
// Read License-en or Licence-fr
//
//=====================================================================================================================

using System;
using System.Collections.Generic;
using UnityEngine;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDItemSlotHelper : NWDHelper<NWDItemSlot>
    {
//-------------------------------------------------------------------------------------------------------------
        public override List<Type>  OverrideClasseInThisSync()
        {
            return new List<Type> { typeof(NWDItemSlot), typeof(NWDUserItemSlot), typeof(NWDItem), typeof(NWDUserOwnership) };
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================