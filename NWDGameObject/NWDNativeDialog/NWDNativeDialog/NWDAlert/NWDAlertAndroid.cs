﻿using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class NWDAlertAndroid : MonoBehaviour
    {
        //-------------------------------------------------------------------------------------------------------------
        const string K_OK = "OK";
        //-------------------------------------------------------------------------------------------------------------
        public string Title;
        public string Message;
        public string Ok;
        public NWDAlertOnCompleteBlock CompleteBlock;
        //-------------------------------------------------------------------------------------------------------------
        public static NWDAlertAndroid Create(string sTitle, string sMessage)
        {
            return Create(sTitle, sMessage, K_OK, null);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static NWDAlertAndroid Create(string sTitle, string sMessage, string sOK, NWDAlertOnCompleteBlock sCompleteBlock)
        {
            NWDAlertAndroid tDialog = new GameObject("BTBAlertAndroid_GameObject").AddComponent<NWDAlertAndroid>();
            tDialog.Title = sTitle;
            tDialog.Message = sMessage;
            tDialog.Ok = sOK;
            tDialog.CompleteBlock = sCompleteBlock;
            tDialog.Initialization();
            return tDialog;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void Initialization()
        {
            BTBNativeDialogAndroid.ShowAlert(Title, Message, Ok);
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnAlertCallback(string sButtonIndex) // call from MobileNativePopup.jar! Don't change the name
        {
            if (CompleteBlock != null)
            {
                CompleteBlock(NWDMessageState.OK);
            }
            Destroy(gameObject);
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
