﻿//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:34:7
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================



using System;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [NWDClassServerSynchronizeAttribute(true)]
    [NWDClassTrigrammeAttribute("ACT")]
    [NWDClassDescriptionAttribute("Action by notification")]
    [NWDClassMenuNameAttribute("Action")]
    public partial class NWDAction : NWDBasis
    {
        //-------------------------------------------------------------------------------------------------------------
        [NWDInspectorGroupStart("Optional", true, true, false)]
        [NWDTooltips("An additional message, it's optional and not used in standard process.")]
        public string Message
        {
            get; set;
        }
        [NWDTooltips("An additional param as string, it's optional and not used in standard process.")]
        public string ParamOne
        {
            get; set;
        }
        [NWDTooltips("An additional param as string, it's optional and not used in standard process.")]
        public string ParamTwo
        {
            get; set;
        }
        [NWDTooltips("An additional param as string, it's optional and not used in standard process.")]
        public string ParamThree
        {
            get; set;
        }
        [NWDInspectorGroupEnd]
       
        [NWDInspectorGroupStart("Optional Scene", true, true, false)]
        [NWDTooltips("An additional scene to use, it's optional and not used in standard process.")]
        public NWDSceneType UseScene
        {
            get; set;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================