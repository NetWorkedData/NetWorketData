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
	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[Serializable]
    public class NWDRecipientGroupConnection : NWDConnection <NWDRecipientGroup> {}
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[NWDClassServerSynchronizeAttribute (true)]
	[NWDClassTrigrammeAttribute ("RCP")]
	[NWDClassDescriptionAttribute ("Recipient descriptions Class")]
    [NWDClassMenuNameAttribute ("Recipient")]
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public partial class NWDRecipientGroup :NWDBasis <NWDRecipientGroup>
	{
		//-------------------------------------------------------------------------------------------------------------
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
		[NWDGroupStartAttribute("Description",true, true, true)] // ok
        public NWDReferenceType<NWDItem> DescriptionItem { get; set; }
		[NWDGroupEndAttribute]

		[NWDGroupSeparatorAttribute]

		[NWDGroupStartAttribute("Usage",true, true, true)] // ok
		public bool CraftOnlyMax {get; set;}
		public bool CraftUnUsedElements {get; set;}
		[NWDGroupEndAttribute]

		[NWDGroupSeparatorAttribute]

		[NWDGroupStartAttribute("FX (Special Effects)", true, true, true)]
        [NWDHeaderAttribute("Active Recipient")]
		public NWDPrefabType ActiveParticles { get; set; }
		public NWDPrefabType ActiveSound { get; set; }

		[NWDHeaderAttribute("Add Item")]
		public NWDPrefabType AddParticles { get; set; }
		public NWDPrefabType AddSound { get; set; }

		[NWDHeaderAttribute("Disactive Recipient")]
		public NWDPrefabType DisactiveParticles { get; set; }
		public NWDPrefabType DisactiveSound { get; set; }

		[NWDGroupEndAttribute]

		[NWDGroupSeparatorAttribute]

		[NWDGroupStartAttribute("Item(s) use as recipient",true, true, true)] // ok
		public NWDReferencesListType<NWDItem> ItemList { get; set; }
		//[NWDGroupEndAttribute]
		//-------------------------------------------------------------------------------------------------------------
		#endregion
		//-------------------------------------------------------------------------------------------------------------
        #region Constructors
		//-------------------------------------------------------------------------------------------------------------
		public NWDRecipientGroup()
        {
            //Debug.Log("NWDRecipientGroup Constructor");
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDRecipientGroup(bool sInsertInNetWorkedData) : base(sInsertInNetWorkedData)
        {
            //Debug.Log("NWDRecipientGroup Constructor with sInsertInNetWorkedData : " + sInsertInNetWorkedData.ToString()+"");
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void Initialization()
        {
            CraftOnlyMax = true;
            CraftUnUsedElements = true;
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
        #endregion
		//-------------------------------------------------------------------------------------------------------------
		#region NetWorkedData addons methods
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
            CheckMyItems();
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
        public override void AddonDeleteMe()
        {
            // do something when object will be delete from local base
            //Debug.Log("AddonDeleteMe()");
            ItemList = new NWDReferencesListType<NWDItem>();
            CheckMyItems();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void CheckMyItems()
        {
            List<NWDItem> tActualItems = ItemList.GetObjectsList();
            foreach (NWDItem tItem in NWDItem.GetAllObjects())
            {
                if (tActualItems.Contains(tItem))
                {
                    if (tItem.RecipientGroupList.GetObjectsList().Contains(this) == true)
                    {
                        // ok It's contains me
                    }
                    else
                    {
                        // oh item group not contains me! WHYYYYYYYY
                        tItem.RecipientGroupList.AddObject(this);
                        tItem.UpdateMe();
                    }
                }
                else
                {
                    if (tItem.RecipientGroupList.GetObjectsList().Contains(this))
                    {
                        // Oh This ItemGroup contains me but I not refere it ... remove me from it
                        tItem.RecipientGroupList.RemoveObjects(new NWDRecipientGroup[] { this });
                        tItem.UpdateMe();
                    }
                    else
                    {
                        // ok i'ts not contains me!
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion
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

    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================