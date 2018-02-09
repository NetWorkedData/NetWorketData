//=====================================================================================================================
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

using BasicToolBox;
using SQLite4Unity3d;

#if UNITY_EDITOR
using UnityEditor;
#endif

//=====================================================================================================================
namespace NetWorkedData
{
	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	/// <summary>
	/// NWDPlayerConnexion can be use in MonBehaviour script to connect GameObject with NWDBasis<Data> in editor.
	/// Use like :
	/// public class MyScriptInGame : MonoBehaviour
	/// { 
	/// [NWDConnexionAttribut (true, true, true, true)] // optional
	/// public NWDPlayerConnexion MyNetWorkedData;
	/// }
	/// </summary>
	[Serializable]
    public class NWDPlayerConnexion : NWDConnexion <NWDPlayerInfos> {}
	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[NWDClassServerSynchronizeAttribute (true)]
	[NWDClassTrigrammeAttribute ("UFO")]
	[NWDClassDescriptionAttribute ("General User Informations")]
	[NWDClassMenuNameAttribute ("User Infos")]
	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	//[NWDInternalKeyNotEditableAttribute]
	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	/// <summary>
	/// NWD example class. This class is use for (complete description here)
	/// </summary>
    public partial class NWDPlayerInfos : NWDBasis<NWDPlayerInfos>
	{
		//#warning YOU MUST FOLLOW THIS INSTRUCTIONS
		//-------------------------------------------------------------------------------------------------------------
		// YOU MUST GENERATE PHP FOR THIS CLASS AFTER FIELD THIS CLASS WITH YOUR PROPERTIES
		// YOU MUST GENERATE WEBSITE AND UPLOAD THE FOLDER ON YOUR SERVER
		// YOU MUST UPDATE TABLE ON THE SERVER WITH THE MENU FOR DEV, FOR PREPROD AND FOR PROD
		//-------------------------------------------------------------------------------------------------------------
		#region Class Properties
		//-------------------------------------------------------------------------------------------------------------
		// Your static properties
		//-------------------------------------------------------------------------------------------------------------
		#endregion
		//-------------------------------------------------------------------------------------------------------------
		#region Instance Properties
		//-------------------------------------------------------------------------------------------------------------
		// Your properties
        [NWDHeader("Player Informations")]
		public NWDReferenceType<NWDAccount> AccountReference {get; set;}
        public string Fisrtname {get; set;}
        public string Lastname { get; set; }
        public string Nickname { get; set; } // Nickname in game, not the nickname of account
		public NWDTextureType Avatar {get; set;}


        /// <summary>
        /// Gets or sets the Nickname.
        /// </summary>
        /// <value>The login is NOT the Nickname ... Nickname is use to friendly connect and matchmaking. If you need unique Nickname you must develop the fonction yourself</value>
        [Indexed("NicknameIndex", 0)]
        public string UniqueNickname
        {
            get; set;
        }
		//-------------------------------------------------------------------------------------------------------------
		#endregion
		//-------------------------------------------------------------------------------------------------------------
		#region Constructors
		//-------------------------------------------------------------------------------------------------------------
        public NWDPlayerInfos()
		{
			//Init your instance here
			// Example : this.MyProperty = true, 1 , "bidule", etc.
		}
		//-------------------------------------------------------------------------------------------------------------
		#endregion
		//-------------------------------------------------------------------------------------------------------------
		#region Class methods
		//-------------------------------------------------------------------------------------------------------------
		//-------------------------------------------------------------------------------------------------------------
		#endregion
		//-------------------------------------------------------------------------------------------------------------
		#region Instance methods
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Exampel of implement for instance method.
		/// </summary>
		public void MyInstanceMethod ()
		{
			// do something with this object
		}
		//-------------------------------------------------------------------------------------------------------------
		#region override of NetWorkedData addons methods
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Addon method just after loaded from database.
		/// </summary>
		public override void AddonLoadedMe ()
		{
			// do something when object was loaded
			// TODO verif if method is call in good place in good timing
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Addon method just before unload from memory.
		/// </summary>
		public override void AddonUnloadMe ()
		{
			// do something when object will be unload
			// TODO verif if method is call in good place in good timing
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Addon method just before insert.
		/// </summary>
		public override void AddonInsertMe ()
		{
			// do something when object will be inserted
			// TODO verif if method is call in good place in good timing
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Addon method just before update.
		/// </summary>
		public override void AddonUpdateMe ()
		{
			// do something when object will be updated
			// TODO verif if method is call in good place in good timing
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Addon method when updated.
		/// </summary>
		public override void AddonUpdatedMe ()
		{
			// do something when object finish to be updated
			// TODO verif if method is call in good place in good timing
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Addon method when updated me from Web.
		/// </summary>
        public override void AddonUpdatedMeFromWeb ()
		{
			// do something when object finish to be updated from CSV from WebService response
			// TODO verif if method is call in good place in good timing
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Addon method just before dupplicate.
		/// </summary>
		public override void AddonDuplicateMe ()
		{
			// do something when object will be dupplicate
			// TODO verif if method is call in good place in good timing
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Addon method just before enable.
		/// </summary>
		public override void AddonEnableMe ()
		{
			// do something when object will be enabled
			// TODO verif if method is call in good place in good timing
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Addon method just before disable.
		/// </summary>
		public override void AddonDisableMe ()
		{
			// do something when object will be disabled
			// TODO verif if method is call in good place in good timing
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Addon method just before put in trash.
		/// </summary>
		public override void AddonTrashMe ()
		{
			// do something when object will be put in trash
			// TODO verif if method is call in good place in good timing
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Addon method just before remove from trash.
		/// </summary>
		public override void AddonUnTrashMe ()
		{
			// do something when object will be remove from trash
			// TODO verif if method is call in good place in good timing
		}
		//-------------------------------------------------------------------------------------------------------------
		#if UNITY_EDITOR
		//-------------------------------------------------------------------------------------------------------------
		//Addons for Edition
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Addons in edition state of object.
		/// </summary>
		/// <returns><c>true</c>, if object need to be update, <c>false</c> or not not to be update.</returns>
		/// <param name="sNeedBeUpdate">If set to <c>true</c> need be update in enter.</param>
		public override bool AddonEdited( bool sNeedBeUpdate)
		{
			if (sNeedBeUpdate == true) 
			{
				// do something
			}
			return sNeedBeUpdate;
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Addons editor interface.
		/// </summary>
		/// <returns>The editor height addon.</returns>
		/// <param name="sInRect">S in rect.</param>
		public override float AddonEditor (Rect sInRect)
		{
			// Draw the interface addon for editor
			float tYadd = 0.0f;
			return tYadd;
		}
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Addons editor intreface expected height.
		/// </summary>
		/// <returns>The editor expected height.</returns>
		public override float AddonEditorHeight ()
		{
			// Height calculate for the interface addon for editor
			float tYadd = 0.0f;
			return tYadd;
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
    #region Connexion NWDPlayerInfos with Unity MonoBehavior
	//-------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// NWDPlayer connexion.
	/// In your MonoBehaviour Script connect object with :
	/// <code>
	///	[NWDConnexionAttribut(true,true, true, true)]
	/// public NWDPlayerConnexion MyNWDPlayerObject;
	/// </code>
	/// </summary>
	//-------------------------------------------------------------------------------------------------------------
	// CONNEXION STRUCTURE METHODS
	//-------------------------------------------------------------------------------------------------------------

	//-------------------------------------------------------------------------------------------------------------

	//-------------------------------------------------------------------------------------------------------------
	//public class NWDPlayerMonoBehaviour: NWDMonoBehaviour<NWDPlayerMonoBehaviour> {}
	#endregion
	//-------------------------------------------------------------------------------------------------------------
}
//=====================================================================================================================