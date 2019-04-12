// =====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:42:24
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

using System;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public enum NWDUserNetWorkingState : int
    {
        Unknow = -1,
        OffLine = 0,
        OnLine = 1,

        NotDisturbe = 2,
        Masked = 9,
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [NWDClassServerSynchronizeAttribute(true)]
    [NWDClassTrigrammeAttribute("UNW")]
    [NWDClassDescriptionAttribute("User statut on Network")]
    [NWDClassMenuNameAttribute("User Net Working")]
    public partial class NWDUserNetWorking : NWDBasis<NWDUserNetWorking>
    {
        //-------------------------------------------------------------------------------------------------------------
        public NWDReferenceType<NWDAccount> Account
        {
            get; set;
        }
        public NWDReferenceType<NWDGameSave> GameSave
        {
            get; set;
        }
        public NWDDateTimeType NextUpdate
        {
            get; set;
        }
        public bool NotDisturbe
        {
            get; set;
        }
        public bool Masked
        {
            get; set;
        }
        // perhaps add some stats 
        public int TotalPlay
        {
            get; set;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================