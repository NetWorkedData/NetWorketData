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

#if UNITY_EDITOR
using UnityEditor;
#endif

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// NWDRelationshipInformationsConnection can be use in MonBehaviour script to connect GameObject with NWDBasis<Data> in editor.
    /// Use like :
    /// public class MyScriptInGame : MonoBehaviour
    /// { 
    /// [NWDConnectionAttribut (true, true, true, true)] // optional
    /// public NWDRelationshipInformationsConnection MyNetWorkedData;
    /// }
    /// </summary>
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [Serializable]
    public class NWDRelationshipInformationsConnection : NWDConnection<NWDRelationshipInformations>
    {
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [Serializable]
    public enum BabaooSexType : int
    {
        Male,
        Female,
        Unknown,
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [NWDClassServerSynchronizeAttribute(true)]
    [NWDClassTrigrammeAttribute("RLI")]
    [NWDClassDescriptionAttribute("Relationship Informations")]
    [NWDClassMenuNameAttribute("Relation Infos")]
    [NWDClassPhpPostCalculateAttribute(" // write your php script here to update $tReference")]
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    //[NWDInternalKeyNotEditableAttribute]
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// NWD example class. This class is use for (complete description here)
    /// </summary>
    public partial class NWDRelationshipInformations : NWDBasis<NWDRelationshipInformations>
    {
        //-------------------------------------------------------------------------------------------------------------
        #region Class Properties
        //-------------------------------------------------------------------------------------------------------------
        #endregion
        //-------------------------------------------------------------------------------------------------------------
        #region Instance Properties
        //-------------------------------------------------------------------------------------------------------------
        //PROPERTIES
        [NWDGroupStart("Account")]
        public NWDReferenceType<NWDAccount> Account
        {
            get; set;
        }
        [NWDGroupEnd()]
        [NWDGroupSeparator()]
        [NWDGroupStart("Relation ship connexion")]
        public NWDReferenceType<NWDRelationship> Relationship
        {
            get; set;
        }
        [NWDGroupEnd()]
        [NWDGroupSeparator()]
        [NWDGroupStart("Relation ship informations")]
        public string Firstname
        {
            get; set;
        }
        public string Lastname
        {
            get; set;
        }
        public NWDDateType Anniversary
        {
            get; set;
        }
        public BabaooSexType Gender
        {
            get; set;
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion
        //-------------------------------------------------------------------------------------------------------------
        #region Constructors
        //-------------------------------------------------------------------------------------------------------------
        public NWDRelationshipInformations()
        {
            //Debug.Log("NWDRelationshipInformations Constructor");
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDRelationshipInformations(bool sInsertInNetWorkedData) : base(sInsertInNetWorkedData)
        {
            //Debug.Log("NWDRelationshipInformations Constructor with sInsertInNetWorkedData : " + sInsertInNetWorkedData.ToString() + "");
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void Initialization() // INIT YOUR INSTANCE WITH THIS METHOD
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion
        //-------------------------------------------------------------------------------------------------------------
        #region Class methods
        //-------------------------------------------------------------------------------------------------------------
        public static NWDRelationshipInformations InformationsForRelationship(NWDRelationship sRelationship, bool sCreateIfNull = true)
        {
            NWDRelationshipInformations rRelationshipInformations = null;
            if (sRelationship != null)
            {
                foreach (NWDRelationshipInformations tRelationInfo in GetAllObjects())
                {
                    if (tRelationInfo.Relationship.GetObject() == sRelationship)
                    {
                        rRelationshipInformations = tRelationInfo;
                        break;
                    }
                }
                if (sCreateIfNull==true && rRelationshipInformations == null)
                {
                    rRelationshipInformations = NewObject();
                    rRelationshipInformations.Tag = 20; // Data generated by User-created
                    rRelationshipInformations.Relationship.SetReference(sRelationship.Reference);
                }
            }
            return rRelationshipInformations;
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Exampel of implement for class method.
        /// </summary>
        public static void MyClassMethod()
        {
            // do something with this class
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion
        //-------------------------------------------------------------------------------------------------------------
        #region Instance methods
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Exampel of implement for instance method.
        /// </summary>
        public void MyInstanceMethod()
        {
            // do something with this object
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion
        //-------------------------------------------------------------------------------------------------------------
        #region NetWorkedData addons methods
        //-------------------------------------------------------------------------------------------------------------
        public static List<Type> OverrideClasseInThisSync()
        {
            return new List<Type> { typeof(NWDRelationshipInformations)/*, typeof(NWDUserNickname), etc*/ };
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addon method just after loaded from database.
        /// </summary>
        public override void AddonLoadedMe()
        {
            // do something when object was loaded
            // TODO verif if method is call in good place in good timing
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addon method just before unload from memory.
        /// </summary>
        public override void AddonUnloadMe()
        {
            // do something when object will be unload
            // TODO verif if method is call in good place in good timing
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addon method just before insert.
        /// </summary>
        public override void AddonInsertMe()
        {
            // do something when object will be inserted
            // TODO verif if method is call in good place in good timing
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addon method just before update.
        /// </summary>
        public override void AddonUpdateMe()
        {
            // do something when object will be updated
            // TODO verif if method is call in good place in good timing
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addon method when updated.
        /// </summary>
        public override void AddonUpdatedMe()
        {
            // do something when object finish to be updated
            // TODO verif if method is call in good place in good timing
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addon method when updated me from Web.
        /// </summary>
        public override void AddonUpdatedMeFromWeb()
        {
            // do something when object finish to be updated from CSV from WebService response
            // TODO verif if method is call in good place in good timing
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addon method just before dupplicate.
        /// </summary>
        public override void AddonDuplicateMe()
        {
            // do something when object will be dupplicate
            // TODO verif if method is call in good place in good timing
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addon method just before enable.
        /// </summary>
        public override void AddonEnableMe()
        {
            // do something when object will be enabled
            // TODO verif if method is call in good place in good timing
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addon method just before disable.
        /// </summary>
        public override void AddonDisableMe()
        {
            // do something when object will be disabled
            // TODO verif if method is call in good place in good timing
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addon method just before put in trash.
        /// </summary>
        public override void AddonTrashMe()
        {
            // do something when object will be put in trash
            // TODO verif if method is call in good place in good timing
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addon method just before remove from trash.
        /// </summary>
        public override void AddonUnTrashMe()
        {
            // do something when object will be remove from trash
            // TODO verif if method is call in good place in good timing
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addons the delete me.
        /// </summary>
        public override void AddonDeleteMe()
        {
            // do something when object will be delete from local base
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonWebversionUpgradeMe(int sOldWebversion, int sNewWebVersion)
        {
            // do something when object will be web service upgrade
            // TODO verif if method is call in good place in good timing
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion
        //-------------------------------------------------------------------------------------------------------------
        #region Editor
#if UNITY_EDITOR
        //-------------------------------------------------------------------------------------------------------------
        //Addons for Edition
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addons in edition state of object.
        /// </summary>
        /// <returns><c>true</c>, if object need to be update, <c>false</c> or not not to be update.</returns>
        /// <param name="sNeedBeUpdate">If set to <c>true</c> need be update in enter.</param>
        public override bool AddonEdited(bool sNeedBeUpdate)
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
        public override float AddonEditor(Rect sInRect)
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
        public override float AddonEditorHeight()
        {
            // Height calculate for the interface addon for editor
            float tYadd = 0.0f;
            return tYadd;
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Adds the width of node draw.
        /// </summary>
        /// <returns>The on node draw width.</returns>
        /// <param name="sDocumentWidth">S document width.</param>
        public override float AddOnNodeDrawWidth(float sDocumentWidth)
        {
            return 250.0f;
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Adds the height of node draw.
        /// </summary>
        /// <returns>The on node draw height.</returns>
        public override float AddOnNodeDrawHeight(float sCardWidth)
        {
            return 130.0f;
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Adds node draw.
        /// </summary>
        /// <param name="sRect">S rect.</param>
        public override void AddOnNodeDraw(Rect sRect, bool sPropertysGroup)
        {

        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Adds color on node.
        /// </summary>
        /// <returns>The on node color.</returns>
        public override Color AddOnNodeColor()
        {
            return Color.gray;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override bool AddonErrorFound()
        {
            bool rReturnErrorFound = false;
            // check if you found error in Data values.
            // normal way is return false!
            return rReturnErrorFound;
        }
        //-------------------------------------------------------------------------------------------------------------
#endif
        #endregion
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================