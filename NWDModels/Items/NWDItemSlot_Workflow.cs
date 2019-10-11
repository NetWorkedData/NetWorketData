//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:48:40
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
    public partial class NWDItemSlot : NWDBasis
    {
        //-------------------------------------------------------------------------------------------------------------
        public NWDItemSlot()
        {
            //Debug.Log("NWDItemSlot Constructor");
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDItemSlot(bool sInsertInNetWorkedData) : base(sInsertInNetWorkedData)
        {
            //Debug.Log("NWDItemSlot Constructor with sInsertInNetWorkedData : " + sInsertInNetWorkedData.ToString() + "");
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void Initialization() // INIT YOUR INSTANCE WITH THIS METHOD
        {
            FromOwnership = true;
        }
        //-------------------------------------------------------------------------------------------------------------
        public List<NWDItem> ItemPossibilities()
        {
            List<NWDItem> rItemsPossibilities = ItemGroup.GetRawData().OwnershipIntersection();
            NWDItem tItemNone = ItemNone.GetRawData();
            if (tItemNone != null)
            {
                rItemsPossibilities.Insert(0, tItemNone);
            }
            return rItemsPossibilities;
        }
        //-------------------------------------------------------------------------------------------------------------
        public bool UserAddItem(NWDItem sItem, int sIndex)
        {
            NWDUserItemSlot tUserSlot = NWDUserItemSlot.FindFirstByIndex(Reference);
            return tUserSlot.AddItem(sItem, sIndex);
        }
        //-------------------------------------------------------------------------------------------------------------
        public bool UserRemoveItem(int sIndex)
        {
            NWDUserItemSlot tUserSlot = NWDUserItemSlot.FindFirstByIndex(Reference);
            return tUserSlot.RemoveItem(sIndex);
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDItem UserGetItem(int sIndex)
        {
            NWDUserItemSlot tUserSlot = NWDUserItemSlot.FindFirstByIndex(Reference);
            return tUserSlot.GetItem(sIndex);
        }
        //-------------------------------------------------------------------------------------------------------------
        const int K_NUMBER_MIN = 1;
        const int K_NUMBER_MAX = 32;
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonUpdateMe()
        {
            Debug.Log("AddonUpdateMe");
            if (Number < K_NUMBER_MIN)
            {
                Number = K_NUMBER_MIN;
            }
            else if (Number > K_NUMBER_MAX)
            {
                Number = K_NUMBER_MAX;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================