﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using System.Collections;
using UnityEngine;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    ///
    /// </summary>
    public class NWDUserNetWorkingScript : MonoBehaviour
    {
        //-------------------------------------------------------------------------------------------------------------
        public bool IsActive = true;
        //-------------------------------------------------------------------------------------------------------------
        public void CheckNow() // TODO TEST
        {
            //NWDUserNetWorking.NetworkingUpdate();
            // but need to chnage the waiting seconds? 
            StopCoroutine(UserNetworkinUpdate());
            StartCoroutine(UserNetworkinUpdate());
        }
        //-------------------------------------------------------------------------------------------------------------
        public void CheckStart() // TODO TEST
        {
            StartCoroutine(UserNetworkinUpdate());
        }
        //-------------------------------------------------------------------------------------------------------------
        public void CheckStop() // TODO TEST
        {
            StopCoroutine(UserNetworkinUpdate());
        }
        //-------------------------------------------------------------------------------------------------------------
        void Start()
        {
            //Debug.Log("NWDUserNetWorkingScript Start()");
            NWDUserNetWorking.PrepareUpdate(0, null);
            //CheckStart();
        }
        //-------------------------------------------------------------------------------------------------------------
        IEnumerator UserNetworkinUpdate()
        {
            while (true)
            {
                if (IsActive == true)
                {
                    //Debug.Log("NWDUserNetWorkingScript UserNetworkinUpdate()");
                    NWDUserNetWorking.NetworkingUpdate();
                }
                yield return new WaitForSeconds(NWDUserNetWorking.DelayInSeconds());
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================