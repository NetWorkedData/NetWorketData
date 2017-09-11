﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using UnityEngine;

using SQLite4Unity3d;

using BasicToolBox;

#if UNITY_EDITOR
using UnityEditor;
#endif

//=====================================================================================================================
namespace NetWorkedData
{
	//-------------------------------------------------------------------------------------------------------------
	[NWDClassServerSynchronizeAttribute (false)]
	[NWDClassTrigrammeAttribute ("ACC")]
	[NWDClassDescriptionAttribute ("Account descriptions Class")]
	[NWDClassMenuNameAttribute ("Account")]
	//-------------------------------------------------------------------------------------------------------------
//	[NWDTypeClassInPackageAttribute]
	//-------------------------------------------------------------------------------------------------------------
	public partial class NWDAccount : NWDBasis <NWDAccount>
	{
		//-------------------------------------------------------------------------------------------------------------
		//#warning YOU MUST FOLLOW THIS INSTRUCTIONS
		//-------------------------------------------------------------------------------------------------------------
		// YOU MUST GENERATE PHP FOR THIS CLASS AFTER FIELD THIS CLASS WITH YOUR PROPERTIES
		// YOU MUST GENERATE WEBSITE AND UPLOAD THE FOLDER ON YOUR SERVER
		// YOU MUST UPDATE TABLE ON THE SERVER WITH THE MENU FOR DEV, FOR PREPROD AND FOR PROD
		//-------------------------------------------------------------------------------------------------------------
		#region Properties
		//-------------------------------------------------------------------------------------------------------------
		// Your properties
		/// <summary>
		/// Gets or sets the NickName.
		/// </summary>
		/// <value>The nickname.</value>
		public string NickName { get; set; }

		/// <summary>
		/// Gets or sets the Avatar Data PNG.
		/// </summary>
		/// <value>The Avatar Data PNG (limit on server).</value>
		public string Avatar { get; set; }

		/// <summary>
		/// Gets or sets the SecretKey to restaure anonymous account.
		/// </summary>
		/// <value>The login.</value>
		public string SecretKey { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>The login is an email.</value>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the apple notification token for message.
		/// </summary>
		/// <value>The apple notification token.</value>
		public string AppleNotificationToken { get; set; }

		/// <summary>
		/// Gets or sets the google notification token for message.
		/// </summary>
		/// <value>The google notification token.</value>
		public string GoogleNotificationToken { get; set; }

		/// <summary>
		/// Gets or sets the Facebook Identifiant.
		/// </summary>
		/// <value>The facebook I.</value>
		public string FacebookID { get; set; }

		/// <summary>
		/// Gets or sets the Google Identifiant.
		/// </summary>
		/// <value>The google I.</value>
		public string GoogleID { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this account <see cref="NWDEditor.NWDAccount"/> is banned.
		/// </summary>
		/// <value><c>true</c> if ban; otherwise, <c>false</c>.</value>
		public int Ban { get; set; }
		//-------------------------------------------------------------------------------------------------------------
		#endregion
		//-------------------------------------------------------------------------------------------------------------
		#region Constructors
		//-------------------------------------------------------------------------------------------------------------
		public NWDAccount()
		{
			//Init your instance here
		}
		//-------------------------------------------------------------------------------------------------------------
		#endregion
		//-------------------------------------------------------------------------------------------------------------
		#region Class methods
		//-------------------------------------------------------------------------------------------------------------
		public static void MyClassMethod ()
		{
			// do something with this class
		}
		//-------------------------------------------------------------------------------------------------------------
		#endregion
		//-------------------------------------------------------------------------------------------------------------
		#region Instance methods
		//-------------------------------------------------------------------------------------------------------------
		public void MyInstanceMethod ()
		{
			// do something with this object
		}
		//-------------------------------------------------------------------------------------------------------------
		#region override of NetWorkedData addons methods
		//-------------------------------------------------------------------------------------------------------------
		public override void AddonInsertMe ()
		{
			// do something when object will be inserted
		}
		//-------------------------------------------------------------------------------------------------------------
		public override void AddonUpdateMe ()
		{
			// do something when object will be updated
		}
		//-------------------------------------------------------------------------------------------------------------
		public override void AddonUpdatedMe ()
		{
			// do something when object finish to be updated
		}
		//-------------------------------------------------------------------------------------------------------------
		public override void AddonDuplicateMe ()
		{
			// do something when object will be dupplicate
		}
		//-------------------------------------------------------------------------------------------------------------
		public override void AddonEnableMe ()
		{
			// do something when object will be enabled
		}
		//-------------------------------------------------------------------------------------------------------------
		public override void AddonDisableMe ()
		{
			// do something when object will be disabled
		}
		//-------------------------------------------------------------------------------------------------------------
		public override void AddonTrashMe ()
		{
			// do something when object will be put in trash
		}
		//-------------------------------------------------------------------------------------------------------------
		public override void AddonUnTrashMe ()
		{
			// do something when object will be remove from trash
		}
		//-------------------------------------------------------------------------------------------------------------
		#if UNITY_EDITOR
		//-------------------------------------------------------------------------------------------------------------
		//Addons for Edition
		//-------------------------------------------------------------------------------------------------------------
		public override bool AddonEdited( bool sNeedBeUpdate)
		{
			if (sNeedBeUpdate == true) 
			{
				// do something
			}
			return sNeedBeUpdate;
		}
		//-------------------------------------------------------------------------------------------------------------
		private string kInternalLogin; //TODO : change prefiuxe by p
		private string kInternalPassword; //TODO : change prefiuxe by p
		//-------------------------------------------------------------------------------------------------------------
		public override float AddonEditor (Rect sInRect)
		{
			//Debug.Log ("AddonEditor");
			float tWidth = sInRect.width;
			float tX = sInRect.x;
			float tY = sInRect.y;

			GUIStyle tTextFieldStyle = new GUIStyle (EditorStyles.textField);
			tTextFieldStyle.fixedHeight = tTextFieldStyle.CalcHeight (new GUIContent ("A"), tWidth);

			GUIStyle tMiniButtonStyle = new GUIStyle (EditorStyles.miniButton);
			tMiniButtonStyle.fixedHeight = tMiniButtonStyle.CalcHeight (new GUIContent ("A"), tWidth);

			GUIStyle tLabelStyle = new GUIStyle (EditorStyles.boldLabel);
			tLabelStyle.fixedHeight = tLabelStyle.CalcHeight (new GUIContent ("A"), tWidth);


			EditorGUI.DrawRect (new Rect (tX, tY + NWDConstants.kFieldMarge, tWidth, 1), kRowColorLine);
			tY += NWDConstants.kFieldMarge * 2;

			EditorGUI.LabelField (new Rect (tX, tY, tWidth, tTextFieldStyle.fixedHeight), "Tools box", tLabelStyle);
			tY += tLabelStyle.fixedHeight + NWDConstants.kFieldMarge;

			kInternalLogin = EditorGUI.TextField (new Rect (tX, tY, tWidth, tTextFieldStyle.fixedHeight), "Email to hash", kInternalLogin, tTextFieldStyle);
			tY += tTextFieldStyle.fixedHeight + NWDConstants.kFieldMarge;
			kInternalPassword = EditorGUI.TextField (new Rect (tX, tY, tWidth, tTextFieldStyle.fixedHeight), "Password to hash", kInternalPassword, tTextFieldStyle);
			tY += tTextFieldStyle.fixedHeight + NWDConstants.kFieldMarge;

			float tWidthTiers = (tWidth - NWDConstants.kFieldMarge * 1) / 2.0f;

			EditorGUI.BeginDisabledGroup (kInternalLogin == "" || kInternalPassword == "" || kInternalLogin == null || kInternalPassword == null);

			if (GUI.Button (new Rect (tX, tY, tWidthTiers, tMiniButtonStyle.fixedHeight), "SignUp dev", tMiniButtonStyle)) {
				NWDAppEnvironment tEnvironmentDev = NWDAppConfiguration.SharedInstance.DevEnvironment;
				this.Email = BTBSecurityTools.GenerateSha (kInternalLogin + tEnvironmentDev.SaltStart, BTBSecurityShaTypeEnum.Sha1);
				this.Password = BTBSecurityTools.GenerateSha(kInternalPassword + tEnvironmentDev.SaltEnd, BTBSecurityShaTypeEnum.Sha1);
				this.InternalDescription = "Account for Dev test (" + kInternalLogin + " / " + kInternalPassword + ")";
				kInternalLogin = "";
				kInternalPassword = "";
				this.UpdateMeIfModified ();
			}

			if (GUI.Button (new Rect (tX + tWidthTiers + NWDConstants.kFieldMarge, tY, tWidthTiers, tMiniButtonStyle.fixedHeight), "SignUp preprod", tMiniButtonStyle)) {
				NWDAppEnvironment tEnvironmentPreprod = NWDAppConfiguration.SharedInstance.PreprodEnvironment;
				this.Email = BTBSecurityTools.GenerateSha(kInternalLogin + tEnvironmentPreprod.SaltStart, BTBSecurityShaTypeEnum.Sha1);
				this.Password = BTBSecurityTools.GenerateSha(kInternalPassword + tEnvironmentPreprod.SaltEnd, BTBSecurityShaTypeEnum.Sha1);
				this.InternalDescription = "Account for Preprod test (" + kInternalLogin + " / " + kInternalPassword + ")";
				kInternalLogin = "";
				kInternalPassword = "";
				this.UpdateMeIfModified ();
			}

			//			if (GUI.Button (new Rect (tX+(tWidthTiers+NWDConstants.kFieldMarge)*2, tY, tWidthTiers, tMiniButtonStyle.fixedHeight), "SignUp prod", tMiniButtonStyle)) {
			//				NWDAppEnvironment tEnvironmentProd = NWDAppConfiguration.SharedInstance.ProdEnvironment;
			//				this.Email = BTBSecurityTools.generateSha (kInternalLogin + tEnvironmentProd.SaltStart, BTBSecurityShaTypeEnum.Sha1);
			//				this.Password = BTBSecurityTools.generateSha (kInternalPassword + tEnvironmentProd.SaltEnd, BTBSecurityShaTypeEnum.Sha1);
			//				this.InternalDescription = "Account for Prod test (" + kInternalLogin + " / " + kInternalPassword + ")";
			//				kInternalLogin = "";
			//				kInternalPassword = "";
			//				this.UpdateMeIfModified();
			//			}

			tY += tMiniButtonStyle.fixedHeight + NWDConstants.kFieldMarge;
			EditorGUI.EndDisabledGroup ();


			EditorGUI.BeginDisabledGroup (kInternalLogin == "" || kInternalLogin == null);

			if (GUI.Button (new Rect (tX, tY, tWidthTiers, tMiniButtonStyle.fixedHeight), "Rescue dev", tMiniButtonStyle)) {
				NWDAppEnvironment tEnvironmentDev = NWDAppConfiguration.SharedInstance.DevEnvironment;
				NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Editor Account Rescue", null, null, null, null, tEnvironmentDev);
				sOperation.Action = "rescue";
				sOperation.EmailRescue = kInternalLogin;
				NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, true);
			}

			if (GUI.Button (new Rect (tX + tWidthTiers + NWDConstants.kFieldMarge, tY, tWidthTiers, tMiniButtonStyle.fixedHeight), "SignUp preprod", tMiniButtonStyle)) {
				NWDAppEnvironment tEnvironmentPreprod = NWDAppConfiguration.SharedInstance.PreprodEnvironment;
				NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Editor Account Rescue", null, null, null, null, tEnvironmentPreprod);
				sOperation.Action = "rescue";
				sOperation.EmailRescue = kInternalLogin;
				NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, true);
			}

			//			if (GUI.Button (new Rect (tX + (tWidthTiers + NWDConstants.kFieldMarge) * 2, tY, tWidthTiers, tMiniButtonStyle.fixedHeight), "SignUp prod", tMiniButtonStyle)) {
			//				NWDAppEnvironment tEnvironmentProd = NWDAppConfiguration.SharedInstance.ProdEnvironment;
			//				NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Editor Account Rescue", null, null, null, null, tEnvironmentProd);
			//				sOperation.Action = "rescue";
			//				sOperation.Email = Email;
			//				NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, true);
			//			}

			tY += tMiniButtonStyle.fixedHeight + NWDConstants.kFieldMarge;
			EditorGUI.EndDisabledGroup ();

			EditorGUI.BeginDisabledGroup (Email == "" || Password == "");
			if (GUI.Button (new Rect (tX, tY, tWidthTiers, tMiniButtonStyle.fixedHeight), "SignIn dev", tMiniButtonStyle)) {
				NWDAppEnvironment tEnvironmentDev = NWDAppConfiguration.SharedInstance.DevEnvironment;

				NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Editor Account Sign-in", null, null, null, null, tEnvironmentDev);
				sOperation.Action = "signin";
				sOperation.EmailHash = Email;
				sOperation.PasswordHash = Password;
				NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, true);
			}

			if (GUI.Button (new Rect (tX + tWidthTiers + NWDConstants.kFieldMarge, tY, tWidthTiers, tMiniButtonStyle.fixedHeight), "SignIn preprod", tMiniButtonStyle)) {
				NWDAppEnvironment tEnvironmentPreprod = NWDAppConfiguration.SharedInstance.PreprodEnvironment;

				NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Editor Account Sign-in", null, null, null, null, tEnvironmentPreprod);
				sOperation.Action = "signin";
				sOperation.EmailHash = Email;
				sOperation.PasswordHash = Password;
				NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, true);
			}

			//			if (GUI.Button (new Rect (tX+(tWidthTiers+NWDConstants.kFieldMarge)*2, tY, tWidthTiers, tMiniButtonStyle.fixedHeight), "SignIn prod", tMiniButtonStyle)) {
			//				NWDAppEnvironment tEnvironmentProd = NWDAppConfiguration.SharedInstance.ProdEnvironment;
			//
			//				NWDOperationWebAccount sOperation = NWDOperationWebAccount.Create ("Editor Account Sign-in", null, null, null, null, tEnvironmentProd);
			//				sOperation.Action = "signin";
			//				sOperation.EmailHash = Email;
			//				sOperation.PasswordHash = Password;
			//				NWDDataManager.SharedInstance.WebOperationQueue.AddOperation (sOperation, true);
			//			}
			tY += tMiniButtonStyle.fixedHeight + NWDConstants.kFieldMarge;
			EditorGUI.EndDisabledGroup ();
			tY += NWDConstants.kFieldMarge;
			return tY;
		}
		//-------------------------------------------------------------------------------------------------------------
		public override float AddonEditorHeight ()
		{
			//Debug.Log ("AddonEditorHeight");
			GUIStyle tTextFieldStyle = new GUIStyle (EditorStyles.textField);
			tTextFieldStyle.fixedHeight = tTextFieldStyle.CalcHeight (new GUIContent ("A"), 100);

			GUIStyle tMiniButtonStyle = new GUIStyle (EditorStyles.miniButton);
			tMiniButtonStyle.fixedHeight = tMiniButtonStyle.CalcHeight (new GUIContent ("A"), 100);

			GUIStyle tLabelStyle = new GUIStyle (EditorStyles.label);
			tLabelStyle.fixedHeight = tLabelStyle.CalcHeight (new GUIContent ("A"), 100);

			float tY = NWDConstants.kFieldMarge;

			tY += NWDConstants.kFieldMarge * 2;
			tY += tLabelStyle.fixedHeight + NWDConstants.kFieldMarge;
			tY += tTextFieldStyle.fixedHeight + NWDConstants.kFieldMarge;
			tY += tTextFieldStyle.fixedHeight + NWDConstants.kFieldMarge;
			tY += tMiniButtonStyle.fixedHeight + NWDConstants.kFieldMarge;
			tY += tMiniButtonStyle.fixedHeight + NWDConstants.kFieldMarge;
			tY += tMiniButtonStyle.fixedHeight + NWDConstants.kFieldMarge;
			tY += NWDConstants.kFieldMarge;

			return tY;
		}
		//-------------------------------------------------------------------------------------------------------------
		#endif
		//-------------------------------------------------------------------------------------------------------------
		#endregion
		//-------------------------------------------------------------------------------------------------------------
		#endregion
		//-------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------
	#region Connexion NWDAccount with Unity MonoBehavior
	//-------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// NWDAccount connexion.
	/// In your MonoBehaviour Script connect object with :
	/// <code>
	///	[NWDConnexionAttribut(true,true, true, true)]
	/// public NWDAccountConnexion MyNWDAccountObject;
	/// </code>
	/// </summary>
	//-------------------------------------------------------------------------------------------------------------
	// CONNEXION STRUCTURE METHODS
	//-------------------------------------------------------------------------------------------------------------
	[Serializable]
	public class NWDAccountConnexion
	{
		//-------------------------------------------------------------------------------------------------------------
		[SerializeField]
		public string Reference;
		//-------------------------------------------------------------------------------------------------------------
		public NWDAccount GetObject ()
		{
			return NWDAccount.GetObjectByReference (Reference);
		}
		//-------------------------------------------------------------------------------------------------------------
		public void SetObject (NWDAccount sObject)
		{
			if (sObject != null) {
				Reference = sObject.Reference;
			} else {
				Reference = "";
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		public NWDAccount NewObject ()
		{
			NWDAccount tObject = NWDAccount.NewObject ();
			Reference = tObject.Reference;
			return tObject;
		}
		//-------------------------------------------------------------------------------------------------------------
	}
	//-------------------------------------------------------------------------------------------------------------
	// CUSTOM PROPERTY DRAWER METHODS
	//-------------------------------------------------------------------------------------------------------------
	#if UNITY_EDITOR
	//-------------------------------------------------------------------------------------------------------------
	[CustomPropertyDrawer (typeof(NWDAccountConnexion))]
	public class NWDAccountConnexionDrawer : PropertyDrawer
	{
		//-------------------------------------------------------------------------------------------------------------
		public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
		{
			Debug.Log ("GetPropertyHeight");
			NWDConnexionAttribut tReferenceConnexion = new NWDConnexionAttribut ();
			if (fieldInfo.GetCustomAttributes (typeof(NWDConnexionAttribut), true).Length > 0)
			{
				tReferenceConnexion = (NWDConnexionAttribut)fieldInfo.GetCustomAttributes (typeof(NWDConnexionAttribut), true)[0];
			}
			return NWDAccount.ReferenceConnexionHeightSerialized(property, tReferenceConnexion.ShowInspector);
		}
		//-------------------------------------------------------------------------------------------------------------
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			Debug.Log ("OnGUI");
			NWDConnexionAttribut tReferenceConnexion = new NWDConnexionAttribut ();
			if (fieldInfo.GetCustomAttributes (typeof(NWDConnexionAttribut), true).Length > 0)
			{
				tReferenceConnexion = (NWDConnexionAttribut)fieldInfo.GetCustomAttributes (typeof(NWDConnexionAttribut), true)[0];
			}
			NWDAccount.ReferenceConnexionFieldSerialized (position, property.displayName, property, "", tReferenceConnexion.ShowInspector, tReferenceConnexion.Editable, tReferenceConnexion.EditButton, tReferenceConnexion.NewButton);
		}
		//-------------------------------------------------------------------------------------------------------------
	}
	//-------------------------------------------------------------------------------------------------------------
	#endif
	//-------------------------------------------------------------------------------------------------------------
	#endregion
	//-------------------------------------------------------------------------------------------------------------
}
//=====================================================================================================================