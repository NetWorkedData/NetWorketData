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
	[Serializable]
	public class NWDDistrictConnexion : NWDConnexion <NWDDistrict> {}
	//-------------------------------------------------------------------------------------------------------------
	[NWDClassServerSynchronizeAttribute (true)]
	[NWDClassTrigrammeAttribute ("DIS")]
    [NWDClassDescriptionAttribute ("District descriptions Class")]
    [NWDClassMenuNameAttribute ("District")]
	//-------------------------------------------------------------------------------------------------------------
//	[NWDTypeClassInPackageAttribute]
	//-------------------------------------------------------------------------------------------------------------
	public partial class NWDDistrict : NWDBasis<NWDDistrict>
	{
		//-------------------------------------------------------------------------------------------------------------
		//public bool DiscoverItYourSelf { get; set; }
		[NWDGroupStartAttribute ("Informations", true, true, true)]
		public NWDLocalizableStringType Name { get; set; }
		public NWDLocalizableStringType SubName { get; set; }
		public NWDLocalizableStringType Description { get; set; }
		[NWDGroupEndAttribute]

		[NWDSeparatorAttribute]

		[NWDGroupStartAttribute ("Desription", true, true, true)]
		public NWDReferenceType<NWDItem> ItemToDescribe { get; set;}
		[NWDGroupEndAttribute]

		[NWDSeparatorAttribute]

		[NWDGroupStartAttribute("Classification",true,true,true)]
		//public string Kind { get; set; }
        public NWDReferencesListType<NWDWorld> World { get; set; }
        public NWDReferencesListType<NWDDistrict> District { get; set; }
		public NWDReferencesListType<NWDCategory> Categories { get; set; }
		public NWDReferencesListType<NWDFamily> Families { get; set; }
		public NWDReferencesListType<NWDKeyword> Keywords { get; set; }
		[NWDGroupEndAttribute]

		[NWDSeparatorAttribute]

		[NWDGroupStartAttribute("Assets",true,true,true)]
		public NWDColorType PrimaryColor { get; set; }

		public NWDColorType SecondaryColor { get; set; }
	
		public NWDColorType TertiaryColor { get; set; }
		//[NWDGroupEndAttribute]
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
		//PROPERTIES
		//-------------------------------------------------------------------------------------------------------------
		#endregion
		//-------------------------------------------------------------------------------------------------------------
		#region Constructors
		//-------------------------------------------------------------------------------------------------------------
		public NWDDistrict()
		{
			//Init your instance here
			//DiscoverItYourSelf = true;
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
		public override float AddonEditor (Rect sInRect)
		{
			// Draw the interface addon for editor
			float tYadd = 0.0f;
			return tYadd;
		}
		//-------------------------------------------------------------------------------------------------------------
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
}
//=====================================================================================================================