﻿//=====================================================================================================================
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
    [NWDClassServerSynchronizeAttribute(true)]
    [NWDClassTrigrammeAttribute("UNR")]
    [NWDClassDescriptionAttribute("User News Read")]
    [NWDClassMenuNameAttribute("User News Read")]
    //[NWDInternalKeyNotEditableAttribute]
    // TODO : rename NWDUserNews
    public partial class NWDUserNewsRead : NWDBasis<NWDUserNewsRead>
    {
        //-------------------------------------------------------------------------------------------------------------
        [NWDInspectorGroupStart("User informations")]
        public NWDReferenceType<NWDAccount> Account
        {
            get; set;
        }
        public NWDReferenceType<NWDGameSave> GameSave
        {
            get; set;
        }
        [NWDInspectorGroupEnd]

        [NWDInspectorGroupStart("News connection")]
        public NWDReferenceType<NWDNews> News
        {
            get; set;
        }
        [NWDInspectorGroupEnd]

        [NWDInspectorGroupStart("News statut")]
        public bool IsInstalled
        {
            get; set;
        }
        public bool NotifyMe
        {
            get; set;
        }
        public bool IsRead
        {
            get; set;
        }
        public bool IsSendByPush
        {
            get; set;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================