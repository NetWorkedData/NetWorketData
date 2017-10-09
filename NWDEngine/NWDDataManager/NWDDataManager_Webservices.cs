﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using SQLite4Unity3d;

using BasicToolBox;

//=====================================================================================================================
namespace NetWorkedData
{
	//	#if UNITY_EDITOR
	//	public class BTBOperationControllerMenuTest
	//	{
	//		static string KEmail = "";
	//		static string KPassword = "";
	//		//-------------------------------------------------------------------------------------------------------------
	//		[MenuItem ("NWDWEB/test", false, 00)]
	//		public static void Test ()
	//		{
	//			NWDOperationWebUnity.AddOperation ("test");
	////			NWDDataManager.SharedInstance.AddWebRequestAllSynchronization ();
	//		}
	//		//-------------------------------------------------------------------------------------------------------------
	//		[MenuItem ("NWDWEB/reset", false, 20)]
	//		public static void AccountReset ()
	//		{
	//			NWDAppConfiguration.SharedInstance.SelectedEnvironment ().ResetSession ();
	//		}
	//		//-------------------------------------------------------------------------------------------------------------
	//		[MenuItem ("NWDWEB/session test", false, 20)]
	//		public static void SessionReset ()
	//		{
	//			NWDDataManager.SharedInstance.AddWebRequestSessionWithBlock(delegate (BTBOperation bOperation, float bProgress, BTBOperationResult bInfos) {
	//				BTBDebug.Log("####### Progress");
	//			}, delegate (BTBOperation bOperation, float bProgress, BTBOperationResult bInfos) {
	//				BTBDebug.Log("####### Finish");
	//			}, delegate (BTBOperation bOperation, float bProgress, BTBOperationResult bInfos) {
	//				BTBDebug.Log("####### Error");
	//			}, delegate (BTBOperation bOperation, float bProgress, BTBOperationResult bInfos) {
	//				BTBDebug.Log("####### Cancel");
	//			},false,NWDAppConfiguration.SharedInstance.SelectedEnvironment ()
	//			);
	//		}
	//		//-------------------------------------------------------------------------------------------------------------
	//		[MenuItem ("NWDWEB/sign-up", false, 22)]
	//		public static void AccountSignUp ()
	//		{
	//			KEmail = NWDToolbox.RandomStringUnix (8) + "@idemobi.com";
	//			KPassword = NWDToolbox.RandomString (18);
	//			KPassword = "1234";
	//			BTBDebug.Log ("Refrence : " + NWDAppConfiguration.SharedInstance.SelectedEnvironment ().PlayerAccountReference + " Email : " + KEmail + " Password : " + KPassword);
	//
	//			NWDDataManager.SharedInstance.AddWebRequestSignUp (KEmail, KPassword, KPassword);
	//		}
	//		//-------------------------------------------------------------------------------------------------------------
	//		[MenuItem ("NWDWEB/sign-out", false, 23)]
	//		public static void AccountSignOut ()
	//		{
	//			NWDDataManager.SharedInstance.AddWebRequestSignOut ();
	//		}
	//		//-------------------------------------------------------------------------------------------------------------
	//		[MenuItem ("NWDWEB/sign-in", false, 24)]
	//		public static void AccountSignIn ()
	//		{
	//			NWDDataManager.SharedInstance.AddWebRequestSignIn (KEmail, KPassword);
	//		}
	//		//-------------------------------------------------------------------------------------------------------------
	//		[MenuItem ("NWDWEB/modifiy", false, 25)]
	//		public static void AccountModify ()
	//		{
	//			string tOldPassword = KPassword + "";
	//			KEmail = NWDToolbox.RandomStringUnix (8) + "@idemobi.com";
	//			KPassword = NWDToolbox.RandomString (18);
	//			NWDDataManager.SharedInstance.AddWebRequestSignModify (KEmail, tOldPassword, KPassword, KPassword);
	//		}
	//		//-------------------------------------------------------------------------------------------------------------
	//		[MenuItem ("NWDWEB/delete", false, 26)]
	//		public static void AccountDelete ()
	//		{
	//			NWDDataManager.SharedInstance.AddWebRequestSignDelete (KPassword, KPassword);
	//		}
	//
	//
	//		//-------------------------------------------------------------------------------------------------------------
	//		[MenuItem ("NWDWEB/FLUSH QUEUE", false, 999)]
	//		public static void FlushQueue ()
	//		{
	//			NWDDataManager.SharedInstance.WebRequestFlush ();
	//		}
	//		//-------------------------------------------------------------------------------------------------------------
	//		[MenuItem ("NWDWEB/INFOS QUEUE", false, 999)]
	//		public static void InfosQueue ()
	//		{
	//			NWDDataManager.SharedInstance.WebRequestInfos ();
	//		}
	//
	//
	//
	//		//-------------------------------------------------------------------------------------------------------------
	//		[MenuItem ("NWDWEB/AccountSequence A", false, 100)]
	//		public static void AccountSequence ()
	//		{
	//			NWDAppConfiguration.SharedInstance.SelectedEnvironment ().ResetSession ();
	//
	//			KEmail = NWDToolbox.RandomStringUnix (8) + "@idemobi.com";
	//			KPassword = NWDToolbox.RandomString (18);
	//			NWDDataManager.SharedInstance.AddWebRequestSignUp (KEmail, KPassword, KPassword);
	//
	//			NWDDataManager.SharedInstance.AddWebRequestSignOut ();
	//
	//			NWDDataManager.SharedInstance.AddWebRequestSignIn (KEmail, KPassword);
	//
	//			KEmail = NWDToolbox.RandomStringUnix (8) + "@idemobi.com";
	//			string tOldPassword = KPassword + "";
	//			KPassword = NWDToolbox.RandomString (18);
	//			NWDDataManager.SharedInstance.AddWebRequestSignModify (KEmail, tOldPassword, KPassword, KPassword);
	//
	//			NWDDataManager.SharedInstance.AddWebRequestSignIn (KEmail, KPassword);
	//
	//			NWDDataManager.SharedInstance.AddWebRequestSignOut ();
	//
	//			NWDDataManager.SharedInstance.AddWebRequestSignIn (KEmail, KPassword);
	//
	//			NWDDataManager.SharedInstance.AddWebRequestSignDelete (KPassword, KPassword);
	//
	//			NWDDataManager.SharedInstance.AddWebRequestSignIn (KEmail, KPassword);
	//		}
	//	}
	//
	//	#endif

	public partial class NWDDataManager
	{
		//-------------------------------------------------------------------------------------------------------------
		public BTBOperationController WebOperationQueue = new BTBOperationController ();
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestAllSynchronizationClean (bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestAllSynchronization");
			return AddWebRequestAllSynchronizationWithBlock (null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestAllSynchronization (bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestAllSynchronization");
			return AddWebRequestAllSynchronizationCleanWithBlock (null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestAllSynchronizationForce (bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestAllSynchronizationForce");
			return AddWebRequestAllSynchronizationForceWithBlock (null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestSynchronizationClean (List<Type> sTypeList, bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSynchronization");
			return AddWebRequestSynchronizationCleanWithBlock (sTypeList, null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestSynchronization (List<Type> sTypeList, bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSynchronization");
			return AddWebRequestSynchronizationWithBlock (sTypeList, null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestSynchronizationForce (List<Type> sTypeList, bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSynchronizationForce");
			return AddWebRequestSynchronizationForceWithBlock (sTypeList, null, null, null, null, sPriority, sEnvironment);
		}
		//		public List<Type> mTypeAccountDependantList = new List<Type> ();
		//		public List<Type> mTypeNotAccountDependantList = new List<Type> ();
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestNotAccountDependantSynchronization (bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSynchronization");
			return AddWebRequestSynchronizationWithBlock (mTypeNotAccountDependantList, null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestNotAccountDependantSynchronizationForce (bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSynchronizationForce");
			return AddWebRequestSynchronizationForceWithBlock (mTypeNotAccountDependantList, null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestAccountDependantSynchronization (bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSynchronization");
			return AddWebRequestSynchronizationWithBlock (mTypeAccountDependantList, null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestAccountDependantSynchronizationForce (bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSynchronizationForce");
			return AddWebRequestSynchronizationForceWithBlock (mTypeAccountDependantList, null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestSession (bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSession");
			return AddWebRequestSessionWithBlock (null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestSignUp (string sEmail, string sPassword, string sConfirmPassword, bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSignUp");
			return AddWebRequestSignUpWithBlock (sEmail, sPassword, sConfirmPassword, null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestSignIn (string sEmail, string sPassword, bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("NWDOperationWebAccount");
			return AddWebRequestSignInWithBlock (sEmail, sPassword, null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestSignOut (bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSignOut");
			return AddWebRequestSignOutWithBlock (null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestAnonymousRestaure (bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestAnonymousRestaure");
			return AddWebRequestAnonymousRestaureWithBlock (null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestSignModify (string sEmail, string sOldPassword, string sNewPassword, string sConfirmPassword, bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSignModify");
			return AddWebRequestSignModifyWithBlock (sEmail, sOldPassword, sNewPassword, sConfirmPassword, null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestRescue (string sEmail, string sAppName, string sAppMail, bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSignModify");
			return AddWebRequestRescueWithBlock (sEmail, null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestSignDelete (string sPassword, string sConfirmPassword, bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSignDelete");
			return AddWebRequestSignDeleteWithBlock (sPassword, sConfirmPassword, null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestLogFacebook (string sSocialToken, bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestLogFacebook");
			return AddWebRequestLogFacebookWithBlock (sSocialToken, null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestLogGoogle (string sSocialToken, bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestLogGoogle");
			return AddWebRequestLogGoogleWithBlock (sSocialToken, null, null, null, null, sPriority, sEnvironment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestAllSynchronizationCleanWithBlock (
			BTBOperationBlock sSuccessBlock = null, 
			BTBOperationBlock sErrorBlock = null, 
			BTBOperationBlock sCancelBlock = null,
			BTBOperationBlock sProgressBlock = null, 
			bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestAllSynchronizationWithBlock");
			/*BTBOperationSynchronisation sOperation = */
			return NWDOperationWebSynchronisation.AddOperation ("Synchronization clean", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment, mTypeSynchronizedList, false, sPriority, true);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestAllSynchronizationWithBlock (
			BTBOperationBlock sSuccessBlock = null, 
			BTBOperationBlock sErrorBlock = null, 
			BTBOperationBlock sCancelBlock = null,
			BTBOperationBlock sProgressBlock = null, 
			bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestAllSynchronizationWithBlock");
			/*BTBOperationSynchronisation sOperation = */
			return NWDOperationWebSynchronisation.AddOperation ("Synchronization", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment, mTypeSynchronizedList, false, sPriority);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestAllSynchronizationForceWithBlock (
			BTBOperationBlock sSuccessBlock = null, 
			BTBOperationBlock sErrorBlock = null, 
			BTBOperationBlock sCancelBlock = null,
			BTBOperationBlock sProgressBlock = null, 
			bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestAllSynchronizationForceWithBlock");
			/*BTBOperationSynchronisation sOperation = */
			return NWDOperationWebSynchronisation.AddOperation ("Synchronization force", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment, mTypeSynchronizedList, true, sPriority);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestSynchronizationCleanWithBlock (List<Type> sTypeList,
			BTBOperationBlock sSuccessBlock = null, 
			BTBOperationBlock sErrorBlock = null, 
			BTBOperationBlock sCancelBlock = null,
			BTBOperationBlock sProgressBlock = null, 
			bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSynchronizationWithBlock");
			/*BTBOperationSynchronisation sOperation = */
			return NWDOperationWebSynchronisation.AddOperation ("Synchronization", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment, sTypeList, false, sPriority, true);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestSynchronizationWithBlock (List<Type> sTypeList,
		                                                                             BTBOperationBlock sSuccessBlock = null, 
		                                                                             BTBOperationBlock sErrorBlock = null, 
		                                                                             BTBOperationBlock sCancelBlock = null,
		                                                                             BTBOperationBlock sProgressBlock = null, 
		                                                                             bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSynchronizationWithBlock");
			/*BTBOperationSynchronisation sOperation = */
			return NWDOperationWebSynchronisation.AddOperation ("Synchronization", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment, sTypeList, false, sPriority);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebSynchronisation AddWebRequestSynchronizationForceWithBlock (List<Type> sTypeList,
		                                                                                  BTBOperationBlock sSuccessBlock = null, 
		                                                                                  BTBOperationBlock sErrorBlock = null, 
		                                                                                  BTBOperationBlock sCancelBlock = null,
		                                                                                  BTBOperationBlock sProgressBlock = null, 
		                                                                                  bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSynchronizationForceWithBlock");
			/*BTBOperationSynchronisation sOperation = */
			return NWDOperationWebSynchronisation.AddOperation ("Synchronization", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment, sTypeList, true, sPriority);
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestSessionWithBlock (BTBOperationBlock sSuccessBlock = null, 
                                                        			 BTBOperationBlock sErrorBlock = null, 
                                                        			 BTBOperationBlock sCancelBlock = null,
                                                        			 BTBOperationBlock sProgressBlock = null,
                                                        			 bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSessionWithBlock");
			NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Account Session with Block", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment);
			sOperation.Action = "session";
			NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, sPriority);
			return sOperation;
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestSignUpWithBlock (string sEmail, string sPassword, string sConfirmPassword,
		                                                            BTBOperationBlock sSuccessBlock = null, 
		                                                            BTBOperationBlock sErrorBlock = null, 
		                                                            BTBOperationBlock sCancelBlock = null,
		                                                            BTBOperationBlock sProgressBlock = null, 
		                                                            bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSignUpWithBlock");
			NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Account Sign-Up with Block", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment);
			sOperation.Action = "signup";
			sOperation.Email = sEmail;
			sOperation.Password = sPassword;
			sOperation.ConfirmPassword = sConfirmPassword;
			NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, sPriority);
			return sOperation;
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestSignInWithBlock (string sEmail, string sPassword,
		                                                            BTBOperationBlock sSuccessBlock = null, 
		                                                            BTBOperationBlock sErrorBlock = null, 
		                                                            BTBOperationBlock sCancelBlock = null, 
		                                                            BTBOperationBlock sProgressBlock = null, 
		                                                            bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSignInWithBlock");
			NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Account Sign-in with Block", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment);
			sOperation.Action = "signin";
			sOperation.Email = sEmail;
			sOperation.Password = sPassword;
			NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, sPriority);
			return sOperation;
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestSignOutWithBlock (BTBOperationBlock sSuccessBlock = null, 
                                                        			 BTBOperationBlock sErrorBlock = null, 
                                                        			 BTBOperationBlock sCancelBlock = null, 
                                                        			 BTBOperationBlock sProgressBlock = null, 
                                                        			 bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSignOutWithBlock");
			NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Account Sign-out with Block", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment);
			sOperation.Action = "signout";
			NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, sPriority);
			return sOperation;
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestAnonymousRestaureWithBlock (BTBOperationBlock sSuccessBlock = null, 
                                                                			   BTBOperationBlock sErrorBlock = null, 
                                                                			   BTBOperationBlock sCancelBlock = null, 
                                                                			   BTBOperationBlock sProgressBlock = null, 
                                                                			   bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestAnonymousRestaureWithBlock");
			NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Account Sign-out with Block", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment);
			sOperation.Action = "signanonymous";
			if (sEnvironment != null) {
				sOperation.AnonymousPlayerAccountReference = sEnvironment.AnonymousPlayerAccountReference;
				sOperation.AnonymousResetPassword = sEnvironment.AnonymousResetPassword;
			}
			NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, sPriority);
			return sOperation;
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestSignModifyWithBlock (string sEmail, string sOldPassword, string sNewPassword, string sConfirmPassword,
		                                                                BTBOperationBlock sSuccessBlock = null, 
		                                                                BTBOperationBlock sErrorBlock = null, 
		                                                                BTBOperationBlock sCancelBlock = null,
		                                                                BTBOperationBlock sProgressBlock = null,
		                                                                bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSignModifyWithBlock");
			NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Account Modifiy with Block", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment);
			sOperation.Action = "modify";
			sOperation.Email = sEmail;
			sOperation.OldPassword = sOldPassword;
			sOperation.NewPassword = sNewPassword;
			sOperation.ConfirmPassword = sConfirmPassword;
			NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, sPriority);
			return sOperation;
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestRescueWithBlock (string sEmail,
		                                                            BTBOperationBlock sSuccessBlock = null, 
		                                                            BTBOperationBlock sErrorBlock = null, 
		                                                            BTBOperationBlock sCancelBlock = null,
		                                                            BTBOperationBlock sProgressBlock = null,
		                                                            bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestRescueWithBlock");
			NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Account Modifiy with Block", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment);
			sOperation.Action = "modify";
			sOperation.EmailRescue = sEmail;
			NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, sPriority);
			return sOperation;
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestSignDeleteWithBlock (string sPassword, string sConfirmPassword,
		                                                                BTBOperationBlock sSuccessBlock = null, 
		                                                                BTBOperationBlock sErrorBlock = null, 
		                                                                BTBOperationBlock sCancelBlock = null,
		                                                                BTBOperationBlock sProgressBlock = null, 
		                                                                bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestSignDeleteWithBlock");
			NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Account Delete with Block", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment);
			sOperation.Action = "delete";
			sOperation.Password = sPassword;
			sOperation.ConfirmPassword = sConfirmPassword;
			NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, sPriority);
			return sOperation;
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestLogFacebookWithBlock (string sSocialToken,
		                                                                 BTBOperationBlock sSuccessBlock = null, 
		                                                                 BTBOperationBlock sErrorBlock = null, 
		                                                                 BTBOperationBlock sCancelBlock = null,
		                                                                 BTBOperationBlock sProgressBlock = null, 
		                                                                 bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestLogFacebookWithBlock");
			NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Account Facebook with Block", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment);
			sOperation.Action = "facebook";
			sOperation.SocialToken = sSocialToken;
			NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, sPriority);
			return sOperation;
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDOperationWebAccount AddWebRequestLogGoogleWithBlock (string sSocialToken,
		                                                               BTBOperationBlock sSuccessBlock = null, 
		                                                               BTBOperationBlock sErrorBlock = null, 
		                                                               BTBOperationBlock sCancelBlock = null,
		                                                               BTBOperationBlock sProgressBlock = null, 
		                                                               bool sPriority = false, NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("AddWebRequestLogGoogleWithBlock");
			NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Account Google with Block", sSuccessBlock, sErrorBlock, sCancelBlock, sProgressBlock, sEnvironment);
			sOperation.Action = "google";
			sOperation.SocialToken = sSocialToken;
			NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, sPriority);
			return sOperation;
		}
		//-------------------------------------------------------------------------------------------------------------
		public void WebRequestFlush (NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("WebRequestFlush");
			if (sEnvironment == null) {
				sEnvironment = NWDAppConfiguration.SharedInstance.SelectedEnvironment ();
			}
			WebOperationQueue.Flush (sEnvironment.Environment);
		}
		//-------------------------------------------------------------------------------------------------------------
		public void WebRequestInfos (NWDAppEnvironment sEnvironment = null)
		{
			BTBDebug.Log ("WebRequestInfos");
			if (sEnvironment == null) {
				sEnvironment = NWDAppConfiguration.SharedInstance.SelectedEnvironment ();
			}
			WebOperationQueue.Infos (sEnvironment.Environment);
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// The synchronize in progress.
		/// </summary>
		//		public static bool SynchronizeInProgress = false;
		//		/// <summary>
		//		/// The synchronize is in progress and another task want connect.
		//		/// Of course because token flow integirty, it's not possible... So I could memorize the task an run after. 
		//		/// But it's too long and difficult : I prefer to ask new Synchronise all 
		//		/// Memorize to repeat as soon as possible without lost the token flow integrity
		//		/// </summary>
		//		public static bool SynchronizeRepeat = false;
		//		/// <summary>
		//		/// The synchronize is in progress and another task ask repeat but in force (update all begin timestamp 0.
		//		/// So Memorize force required
		//		/// </summary>
		//		public static bool SynchronizeRepeatInForce = false;
		//-------------------------------------------------------------------------------------------------------------
		public void ChangeAllDatasForUserToAnotherUser (NWDAppEnvironment sEnvironment, string sNewAccountReference)
		{
			// change account refrence 
			// generate new Reference for this objetc (based on account reference)
			foreach (Type tType in mTypeList) {
				var tMethodInfo = tType.GetMethod ("TryToChangeUserForAllObjects", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
				if (tMethodInfo != null) {
					tMethodInfo.Invoke (null, new object[]{ sEnvironment.PlayerAccountReference, sNewAccountReference });
				}
			}
			sEnvironment.PlayerAccountReference = sNewAccountReference;
			#if UNITY_EDITOR
			// add object in Account table 
//			NWDBasis<NWDAccount>.NewInstanceWithReference (sEnvironment.PlayerAccountReference);
			NWDBasis<NWDAccount>.NewObjectWithReference (sEnvironment.PlayerAccountReference);
			#endif
			SavePreferences (NWDAppConfiguration.SharedInstance.SelectedEnvironment ());
		}
		//-------------------------------------------------------------------------------------------------------------
		public void SynchronizationPullClassesDatas (NWDAppEnvironment sEnvironment, Dictionary<string, object> sData, List<Type> sTypeList)
		{
			bool sUpdateData = false;
			if (sTypeList != null) {
				foreach (Type tType in sTypeList) {
					var tMethodInfo = tType.GetMethod ("SynchronizationPullData", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
					if (tMethodInfo != null) {
						string tResult = tMethodInfo.Invoke (null, new object[]{ sEnvironment, sData }) as string;
						if (tResult == "YES") {
							sUpdateData = true;
						}
					}
				}
			}
			if (sUpdateData == true) {
				NWDDataManager.SharedInstance.NotificationCenter.PostNotification (new BTBNotification (NWDNotificationConstants.K_DATAS_UPDATED, null));
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		public Dictionary<string, object> SynchronizationPushClassesDatas (NWDAppEnvironment sEnvironment, bool sForceAll, List<Type> sTypeList, bool sClean = false)
		{
			Dictionary<string, object> rSend = new Dictionary<string, object> ();
			if (sTypeList != null) {
				foreach (Type tType in sTypeList) {
					var tMethodInfo = tType.GetMethod ("SynchronizationPushData", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
					if (tMethodInfo != null) {
						Dictionary<string, object> rSendPartial = tMethodInfo.Invoke (null, new object[]{ sEnvironment, sForceAll, sClean }) as Dictionary<string, object>;
						foreach (string tKey in rSendPartial.Keys) {
							rSend.Add (tKey, rSendPartial [tKey]);
						}
					}
				}
			}
			return rSend;
		}
		//-------------------------------------------------------------------------------------------------------------
		public void TokenError (NWDAppEnvironment sEnvironment)
		{
			DeleteUser (sEnvironment);
			//TODO : Restaure anonymous account

		}
		//-------------------------------------------------------------------------------------------------------------
		public void DeleteUser (NWDAppEnvironment sEnvironment)
		{
			foreach (Type tType in mTypeList) {
				var tMethodInfo = tType.GetMethod ("DeleteUser", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
				if (tMethodInfo != null) {
					tMethodInfo.Invoke (null, new object[]{ sEnvironment });
				}
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		//		public bool SynchronizationClassesDatas (NWDAppEnvironment sEnvironment, bool sForceAll, List<Type> sTypeList, bool sBackground = true, bool sLoader = false)
		//		{
		//
		//			BTBDebug.Log ("######### SynchronizationClassesDatas start " + Time.time.ToString ());
		//			bool rReturn = false;
		//			if (NWDDataManager.SynchronizeInProgress == false) {
		//				NWDDataManager.SynchronizeInProgress = true;
		//				rReturn = true;
		//				// I write all data
		//				UpdateQueueExecute ();
		//
		//				#if UNITY_EDITOR
		//				// Deselect all object
		//				Selection.activeObject = null;
		//				#endif
		//
		//				BTBUnityWebServiceDataRequest request = NWDWebRequestor.ShareInstance ().RequestDataRequestorWebService (sEnvironment, SynchronizationPushClassesDatas (sEnvironment, sForceAll, sTypeList));
		//				request.successBlockDelegate = delegate(Dictionary<string, object> data) {
		//					// test if error
		//					NWDWebRequestorResult tResult = NWDWebRequestor.RespondAnalyzeIsValid (sEnvironment, data);
		//					if (tResult == NWDWebRequestorResult.Success) {
		//						SynchronizationPullClassesDatas (sEnvironment, data, sTypeList);
		//					} else if (tResult == NWDWebRequestorResult.NewUser) {
		//						// I must resend my request
		//						SynchronizationClassesDatas (sEnvironment, sForceAll, sTypeList, sBackground, sLoader);
		//					} else if (tResult == NWDWebRequestorResult.Error) {
		//
		//					}
		//					NWDDataManager.SynchronizeInProgress = false;
		//					if (NWDDataManager.SynchronizeRepeat == true) {
		//						bool tForceAll = NWDDataManager.SynchronizeRepeatInForce;
		//						NWDDataManager.SynchronizeRepeat = false;
		//						NWDDataManager.SynchronizeRepeatInForce = false;
		//						SynchronizeAllData (sEnvironment, tForceAll);
		//					}
		//
		//					BTBDebug.Log ("######### SynchronizationClassesDatas finish success " + Time.time.ToString ());
		//				};
		//				request.errorBlockDelegate = delegate(Error error) {
		//					BTBDebug.Log ("Error: " + error.code + " // " + error.localizedDescription);
		//					NWDDataManager.SynchronizeInProgress = false;
		//					if (NWDDataManager.SynchronizeRepeat == true) {
		//						bool tForceAll = NWDDataManager.SynchronizeRepeatInForce;
		//						NWDDataManager.SynchronizeRepeat = false;
		//						NWDDataManager.SynchronizeRepeatInForce = false;
		//						SynchronizeAllData (sEnvironment, tForceAll);
		//					}
		//
		//					BTBDebug.Log ("######### SynchronizationClassesDatas finish error " + Time.time.ToString ());
		//				};
		//
		//				BTBDebug.Log ("Synchronization send");
		//				request.send ();
		//			} else {
		//				NWDDataManager.SynchronizeRepeat = true;
		//				if (sForceAll == true) {
		//					NWDDataManager.SynchronizeRepeatInForce = sForceAll;
		//				}
		//
		//				BTBDebug.Log ("Synchronization all ready in progress ... prepare to repeat but in force if one ask it ?");
		//			}
		//			return rReturn;
		//		}

		//		public bool SynchronizationAllClassDatas (NWDAppEnvironment sEnvironment, bool sForceAll, bool sBackground = true, bool sLoader = false)
		//		{
		//			return SynchronizationClassesDatas (sEnvironment, sForceAll, mTypeSynchronizedList, sBackground, sLoader);
		//		}
		//
		//		public bool SynchronizeAllData (NWDAppEnvironment sEnvironment, bool sForceAll)
		//		{
		//			//BTBDebug.Log ("SynchronizeAllData");
		//			return SynchronizationAllClassDatas (sEnvironment, sForceAll);
		//		}

		//		public bool FirstSynchronisation (NWDAppEnvironment sEnvironment)
		//		{
		//			bool rReturn = false;
		//			if (NWDDataManager.SynchronizeInProgress == false) {
		//				rReturn = true;
		//				NWDDataManager.SynchronizeInProgress = true;
		//				Dictionary<string,object> tDico = new Dictionary<string,object> ();
		//				tDico.Add ("test", "test");
		//				BTBUnityWebServiceDataRequest request = NWDWebRequestor.ShareInstance ().RequestDataRequestorWebService (sEnvironment, tDico);
		//				request.successBlockDelegate = delegate(Dictionary<string, object> data) {
		//					NWDDataManager.SynchronizeInProgress = false;
		//					NWDWebRequestorResult tResult = NWDWebRequestor.RespondAnalyzeIsValid (sEnvironment, data);
		//					if (tResult == NWDWebRequestorResult.Success) {
		//					} else if (tResult == NWDWebRequestorResult.NewUser) {
		//					} else if (tResult == NWDWebRequestorResult.Error) {
		//					}
		//				};
		//				request.errorBlockDelegate = delegate(Error error) {
		//					NWDDataManager.SynchronizeInProgress = false;
		//					BTBDebug.Log ("Error: " + error.code + " // " + error.localizedDescription);
		//				};
		//
		//				BTBDebug.Log ("webservice send");
		//				request.send ();
		//			} else {
		//				//NWDDataManager.SynchronizeRepeat = true;
		//				// Not necessairy to repeat, the first connexion is in pogress by another task :-)
		//
		//				BTBDebug.Log ("webservice all ready in progress");
		//			}
		//			return rReturn;
		//		}
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================