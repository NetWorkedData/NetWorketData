//=====================================================================================================================
//
//  ideMobi 2020©
//  All rights reserved by ideMobi
//
//=====================================================================================================================

using System;
using UnityEngine;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDAccountNickname : NWDBasisAccountDependent
    {
        //-------------------------------------------------------------------------------------------------------------
        static protected NWDIndex<NWDAccount, NWDAccountNickname> kAccountIndex = new NWDIndex<NWDAccount, NWDAccountNickname>();
        //-------------------------------------------------------------------------------------------------------------
        [NWDIndexInMemory]
        public void InsertInAccountIndex()
        {
            // Re-add to the actual indexation ?
            if (IsUsable())
            {
                // Re-add !
                string tKey = Account.GetReference();
                kAccountIndex.UpdateData(this, tKey);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        [NWDDeindexInMemory]
        public void RemoveFromAccountIndex()
        {
            // Remove from the actual indexation
            kAccountIndex.RemoveData(this);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static NWDAccountNickname FindFirstDataByAccount(string sAccountReference, bool sOrCreate = true)
        {
            NWDAccountNickname rReturn = null;
            NWDBasisHelper tHelper = NWDBasisHelper.FindTypeInfos(typeof(NWDAccountNickname));
            if (tHelper.AllDatabaseIsLoaded() && tHelper.AllDatabaseIsIndexed() == true)
            {
                rReturn = kAccountIndex.FirstRawDataByKey(sAccountReference);
                if (rReturn == null && sOrCreate == true)
                {
                    rReturn = NWDBasisHelper.NewData<NWDAccountNickname>();
                    rReturn.Account.SetReference(sAccountReference);
                    rReturn.Tag = NWDBasisTag.TagUserCreated;
                    rReturn.UpdateData();
                }
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static NWDAccountNickname CurrentData()
        {
            return FindFirstDataByAccount(NWDAccount.CurrentReference(), true);
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================