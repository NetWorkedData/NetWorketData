﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;

using BasicToolBox;

#if UNITY_EDITOR
using UnityEditor;
#endif

using SQLite4Unity3d;
using System.IO;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDBasis<K> where K : NWDBasis<K>, new()
    {
        //-------------------------------------------------------------------------------------------------------------
#if UNITY_EDITOR
        //-------------------------------------------------------------------------------------------------------------
        public void NodeCardAnalyze(NWDNodeCard sCard)
        {
            Debug.Log("NWDBasis<K> NodeCardAnalyze() Ananlyze type " + ClassNamePHP());
            // insert informations
            sCard.ReferenceString = Reference;
            sCard.TypeString = ClassNamePHP();
            sCard.InternalKeyString = InternalKey;
            sCard.InformationsHeight = AddOnNodeDrawHeight();
            sCard.InformationsHeight = AddOnNodeDrawHeight();
            sCard.ParentDocument.SetInformationsHeight(sCard.InformationsHeight);

            sCard.Width = AddOnNodeDrawWidth(sCard.ParentDocument.GetWidth());
            sCard.ParentDocument.SetWidth(AddOnNodeDrawWidth(sCard.ParentDocument.GetWidth()));
            // data must be analyzed
            // data is in a preview card?
            bool tDataAllReadyShow = false;
            foreach (NWDNodeCard tCard in sCard.ParentDocument.AllCards)
            {
                if (tCard.Data == sCard.Data)
                {
                    tDataAllReadyShow = true;
                    break;
                }
            }
            // data is not in a preview card
            if (tDataAllReadyShow == false)
            {
                // I add this card
                sCard.ParentDocument.AllCards.Add(sCard);
                // I analyze this card and its properties (only the reference properties)
                Type tType = ClassType();
                foreach (PropertyInfo tProp in tType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    Type tTypeOfThis = tProp.PropertyType;
                    if (tTypeOfThis != null)
                    {
                        if (tTypeOfThis.IsGenericType)
                        {
                            if (tTypeOfThis.GetGenericTypeDefinition() == typeof(NWDReferenceType<>)
                                || tTypeOfThis.GetGenericTypeDefinition() == typeof(NWDReferencesListType<>)
                                || tTypeOfThis.GetGenericTypeDefinition() == typeof(NWDReferencesQuantityType<>)
                                || tTypeOfThis.GetGenericTypeDefinition() == typeof(NWDReferencesArrayType<>))
                            {
                                var tMethodInfo = tTypeOfThis.GetMethod("EditorGetObjects", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                                if (tMethodInfo != null)
                                {
                                    var tVar = tProp.GetValue(this, null);
                                    if (tVar != null)
                                    {
                                        object[] tObjects = tMethodInfo.Invoke(tVar, null) as object[];
                                        List<NWDNodeCard> tNewCards = sCard.AddPropertyResult(tProp, tObjects);
                                        foreach (NWDNodeCard tNewCard in tNewCards)
                                        {
                                            tNewCard.Analyze(sCard.ParentDocument);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------
        public virtual float AddOnNodeDrawWidth(float sDocumentWidth)
        {
            return 250.0f;
            //return sDocumentWidth;
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual float AddOnNodeDrawHeight()
        {
            return 130.0f;
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void AddOnNodeDraw(Rect sRect)
        {
            //GUI.Button(sRect, "knkjkjhg");
            GUI.Label(sRect, InternalDescription);
        }
        //-------------------------------------------------------------------------------------------------------------
#endif
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================