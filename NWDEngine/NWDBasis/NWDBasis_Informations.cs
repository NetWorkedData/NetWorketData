﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using System;

using UnityEngine;

using SQLite4Unity3d;

using BasicToolBox;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDBasis<K> : NWDTypeClass where K : NWDBasis<K>, new()
	{
		//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Class informations.
        /// </summary>
        /// <param name="sString">S string.</param>
		public static void ClassInformations (string sString)
        {
			Debug.Log ("From " + sString + " real [" + typeof(K).Name + "] = > " + NWDDatas.Informations (typeof(K)) + "' ");
		}
		//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Informations about this instance.
        /// </summary>
        /// <returns>The informations.</returns>
		public static string Informations ()
		{
            int tCount = Datas().ObjectsList.Count;
			if (tCount == 0) {
                return "" + Datas().ClassNamePHP + " " + NWDConstants.K_APP_BASIS_NO_OBJECT + " (sync at " + SynchronizationGetLastTimestamp(NWDAppEnvironment.SelectedEnvironment())+  ")\n";
			} else if (tCount == 1) {
                return "" + Datas().ClassNamePHP + " : " + tCount + " " + NWDConstants.K_APP_BASIS_ONE_OBJECT + " (sync at " + SynchronizationGetLastTimestamp(NWDAppEnvironment.SelectedEnvironment()) + ")\n";
			} else {
                return "" + Datas().ClassNamePHP + " : " + tCount + " " + NWDConstants.K_APP_BASIS_X_OBJECTS + " (sync at " + SynchronizationGetLastTimestamp(NWDAppEnvironment.SelectedEnvironment()) + ")\n";
			}
		}
		//-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================