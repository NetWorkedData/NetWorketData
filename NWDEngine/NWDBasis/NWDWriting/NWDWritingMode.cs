﻿//=====================================================================================================================
//
// ideMobi copyright 2018 
// All rights reserved by ideMobi
//
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.IO;
using UnityEngine;
using System.Threading;
using System.Text.RegularExpressions;
using SQLite4Unity3d;
using BasicToolBox;
#if UNITY_EDITOR
using UnityEditor;
#endif
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// NWDWritingMode in Database.
    /// </summary>
    public enum NWDWritingMode : int
    {
        /// <summary>
        /// The data is writing in main thread now.
        /// </summary>
        MainThread, // Main Thread
        /// <summary>
        /// The data is writing in background thread now. 
        /// Can be concurrence by main thread an webservice!
        /// </summary>
        PoolThread, // Pool Thread
        /// <summary>
        /// The data is pull in queue in main thread. 
        /// The data will be writing directly on QueueExecution.
        /// </summary>
        QueuedMainThread, // Main Thread In Queue
        /// <summary>
        /// The data is pull in queue in background thread. 
        /// The data will be writing in background on QueueExecution. 
        /// Can be concurrence by main thread an webservice!
        /// </summary>
        QueuedPoolThread, // Pool Thread In Queue
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================