﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using UnityEngine;
using SQLite.Attribute;
using System;
using System.Collections.Generic;
using BasicToolBox;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[NWDClassServerSynchronize(true)]
    [NWDClassTrigramme("UBF")]
    [NWDClassDescription("User Barter Finder descriptions Class")]
    [NWDClassMenuName("User Barter Finder")]
    public partial class NWDUserBarterFinder : NWDBasis<NWDUserBarterFinder>
    {
        //-------------------------------------------------------------------------------------------------------------
        [NWDInspectorGroupStart("Barter Detail", true, true, true)]
        [Indexed("AccountIndex", 0)]
        public NWDReferenceType<NWDAccount> Account
        {
            get; set;
        }
        public NWDReferenceType<NWDGameSave> GameSave
        {
            get; set;
        }
        public NWDReferenceType<NWDBarterPlace> BarterPlace
        {
            get; set;
        }
        [NWDAlias("ForRelationshipOnly")]
        public bool ForRelationshipOnly
        {
            get; set;
        }
        [NWDInspectorGroupEnd]

       

        [NWDInspectorGroupStart("Filters", true, true, true)]
        public NWDReferencesListType<NWDItem> FilterItems
        {
            get; set;
        }
        public NWDReferencesListType<NWDWorld> FilterWorlds
        {
            get; set;
        }
        public NWDReferencesListType<NWDCategory> FilterCategories
        {
            get; set;
        }
        public NWDReferencesListType<NWDFamily> FilterFamilies
        {
            get; set;
        }
        public NWDReferencesListType<NWDKeyword> FilterKeywords
        {
            get; set;
        }
        [NWDInspectorGroupEnd]

       

        [NWDInspectorGroupStart("Results", true, true, true)]
        [NWDAlias("BarterRequestsList")]
        public NWDReferencesListType<NWDUserBarterRequest> BarterRequestsList
        {
            get; set;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================