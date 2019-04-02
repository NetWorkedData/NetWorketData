﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NetWorkedData;
using BasicToolBox;
using System;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class NWDStatsScript : MonoBehaviour
    {
        //-------------------------------------------------------------------------------------------------------------
        public NWDStatisticKeyConnection StatKeyConnection;
        private NWDStatisticKey StatKey;
        public bool AutoCountDuration = false;
        public bool AutoExecuteQueue = true;
        public float IncrementValue = 1.0F;
        private DateTime StartDateTime;
        //-------------------------------------------------------------------------------------------------------------
        public void Increment()
        {
            if (StatKey != null && AutoCountDuration == false)
            {
                StatKey.AddEnter(IncrementValue);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void StartChrono()
        {
            //Debug.Log("NWDStatsScript StartChrono()");
            StartDateTime = DateTime.Now;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void StopChrono()
        {
            //Debug.Log("NWDStatsScript StopChrono()");
            double tStart = BTBDateHelper.ConvertToTimestamp(StartDateTime);
            double tFinish = BTBDateHelper.ConvertToTimestamp(DateTime.Now);
            float rDelta = (float)(tFinish - tStart);
            if (StatKey != null)
            {
                StatKey.AddEnter(rDelta);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        void Awake()
        {
            StatKey = StatKeyConnection.GetObject();
        }
        //-------------------------------------------------------------------------------------------------------------
        void Update()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        void OnEnable()
        {
            //BTBBenchmark.Start();
            if (AutoCountDuration == true)
            {
                StartChrono();
            }
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        void OnDisable()
        {
            //BTBBenchmark.Start();
            if (AutoCountDuration == true)
            {
                StopChrono();
            }
            if (AutoExecuteQueue == true)
            {
                NWDDataManager.SharedInstance().DataQueueExecute();
            }
            //BTBBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================