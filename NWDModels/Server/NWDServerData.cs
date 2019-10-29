﻿//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:29:11
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
    [NWDInternalKeyNotEditable]
    [NWDClassSpecialAccountOnlyAttribute]
    [NWDClassServerSynchronizeAttribute(false)]
    [NWDClassTrigrammeAttribute("SSD")]
    [NWDClassDescriptionAttribute("Server Datas descriptions Class")]
    [NWDClassMenuNameAttribute("Server Datas")]
    public partial class NWDServerDatas : NWDBasis
    {
        //-------------------------------------------------------------------------------------------------------------
        [NWDInspectorGroupStart("Authentification MySQL")]
        [NWDEntitled("MySQL Port")]
        public int MySQLPort { get; set; }
        [NWDEntitled("MySQL User")]
        public string MySQLUser { get; set; }
        [NWDEntitled("MySQL Password")]
        public NWDPasswordType MySQLPassword { get; set; }
        [NWDEntitled("MySQL Base")]
        public string MySQLBase { get; set; }
        [NWDInspectorGroupEnd]
        [NWDInspectorGroupStart("Authentification SSH")]
        [NWDEntitled("SSH IP")]
        public NWDIPType IP { get; set; }
        [NWDEntitled("SSH Port")]
        public int Port { get; set; }
        [NWDEntitled("SSH User")]
        public string User { get; set; }
        [NWDEntitled("SSH Password")]
        public NWDPasswordType Password { get; set; }
        [NWDInspectorGroupEnd]
        [NWDInspectorGroupStart("Install Server Options")]
        public NWDServerDistribution Distribution { get; set; }
        public string ServerName { get; set; }
        public NWDPasswordType RootPassword { get; set; }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
