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
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

#if UNITY_EDITOR
using Renci.SshNet;
using UnityEditor;
#endif

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class NWDServerDatabaseAuthentication
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Title;
        public string NameID;
        public string Range;
        public string MaxUser;
        public string Host;
        public int Port;
        public string Database;
        public string User;
        public string Password;
        public int RangeMax;
        public int RangeMin;
        //-------------------------------------------------------------------------------------------------------------
#if UNITY_EDITOR
        //-------------------------------------------------------------------------------------------------------------
        public NWDServerDatabaseAuthentication(string sTitle, string sNameID, string sRange, int sRangeMin, int sRangeMax, string sMaxUser, string sHost, int sPort, string sDatabase, string sUser, string sPassword)
        {
            //----- for debug notion
            Title = sTitle;
            //----- for cluster notion
            NameID = sNameID;
            Range = sRange;
            RangeMin = sRangeMin;
            RangeMax = sRangeMax;
            MaxUser = sMaxUser;
            //-----
            Host = NWDToolbox.CleanDNS(sHost);
            Port = sPort;
            Database = sDatabase;
            User = sUser;
            Password = sPassword;
        }
        //-------------------------------------------------------------------------------------------------------------
#endif
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
