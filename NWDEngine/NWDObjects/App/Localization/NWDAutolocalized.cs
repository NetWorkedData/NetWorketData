﻿//=====================================================================================================================
//
// ideMobi copyright 2018
// All rights reserved by ideMobi
//
//=====================================================================================================================

using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [SerializeField]
    public enum NWDAutolocalizedTag : int
    {
        None,
        BaseString,
        MarkedBaseString,
        KeyInternal,
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// Autolocalized text with an NWDLocalizationConnection reference selected by popmenu in editor.
    /// </summary>
    public class NWDAutolocalized : MonoBehaviour
    {
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The localization reference.
        /// </summary>
        [Header("Connect the localizable Text")]
        public NWDLocalizationConnection LocalizationReference;
        public NWDAutolocalizedTag AutoTag = NWDAutolocalizedTag.MarkedBaseString;
        /// <summary>
        /// The text target (optional).
        /// </summary>
        [Header("Optional binding")]
        public bool TextBinding = true;
        public Text TextTarget;
        public bool TextMeshBinding = true;
        public TextMesh TextMeshTarget;
        //-------------------------------------------------------------------------------------------------------------
#if UNITY_EDITOR
        //-------------------------------------------------------------------------------------------------------------
        private void OnDrawGizmos()
        {
            LocalizeEditor();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void LocalizeEditor()
        {
            if (EditorApplication.isPlaying)
            {
            }
            else
            {
                if (AutoTag != NWDAutolocalizedTag.None)
                {

                    // text
                    if (TextBinding == true)
                    {
                        // if not pluging
                        if (TextTarget == null)
                        {
                            // get root text
                            TextTarget = GetComponent<Text>();
                        }
                        // if not find in root object
                        if (TextTarget == null)
                        {
                            // get first children text
                            TextTarget = GetComponentInChildren<Text>();
                        }
                        if (TextTarget != null)
                        {
                            // set the localizable text
                            if (LocalizationReference != null)
                            {
                                NWDLocalization tLocalization = LocalizationReference.GetObject();
                                if (tLocalization != null)
                                {
                                    string tTextString = "";
                                    switch (AutoTag)
                                    {
                                        case NWDAutolocalizedTag.BaseString:
                                            {
                                                tTextString = tLocalization.TextValue.GetBaseString();
                                            }
                                            break;
                                        case NWDAutolocalizedTag.MarkedBaseString:
                                            {
                                                tTextString = "#" + tLocalization.TextValue.GetBaseString() + "#";
                                            }
                                            break;
                                        case NWDAutolocalizedTag.KeyInternal:
                                            {
                                                tTextString = tLocalization.InternalKeyValue();
                                            }
                                            break;
                                    }
                                    if (TextTarget.text != tTextString)
                                    {
                                        TextTarget.text = tTextString;
                                        EditorUtility.SetDirty(TextTarget);
                                    }
                                }
                            }
                        }
                    }

                    //TextMesh
                    if (TextMeshBinding == true)
                    {
                        // if not pluging
                        if (TextMeshTarget == null)
                        {
                            // get root text
                            TextMeshTarget = GetComponent<TextMesh>();
                        }
                        // if not find in root object
                        if (TextMeshTarget == null)
                        {
                            // get first children text
                            TextMeshTarget = GetComponentInChildren<TextMesh>();
                        }
                        if (TextMeshTarget != null)
                        {
                            // set the localizable text
                            if (LocalizationReference != null)
                            {
                                NWDLocalization tLocalization = LocalizationReference.GetObject();
                                if (tLocalization != null)
                                {
                                    string tTextString = "";
                                    switch (AutoTag)
                                    {
                                        case NWDAutolocalizedTag.BaseString:
                                            {
                                                tTextString = tLocalization.TextValue.GetBaseString();
                                            }
                                            break;
                                        case NWDAutolocalizedTag.MarkedBaseString:
                                            {
                                                tTextString = "#" + tLocalization.TextValue.GetBaseString() + "#";
                                            }
                                            break;
                                        case NWDAutolocalizedTag.KeyInternal:
                                            {
                                                tTextString = tLocalization.InternalKeyValue();
                                            }
                                            break;
                                    }
                                    if (TextMeshTarget.text != tTextString)
                                    {
                                        TextMeshTarget.text = tTextString;
                                        EditorUtility.SetDirty(TextMeshTarget);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
#endif
        //-------------------------------------------------------------------------------------------------------------
        public void Localize(bool sUseBaseString = false)
        {
            //Text
            if (TextBinding == true)
            {
                // if not pluging
                if (TextTarget == null)
                {
                    // get root text
                    TextTarget = GetComponent<Text>();
                }

                // if not find in root object
                if (TextTarget == null)
                {
                    // get first children text
                    TextTarget = GetComponentInChildren<Text>();
                }

                // if Text found
                if (TextTarget != null)
                {
                    // set the localizable text
                    if (LocalizationReference != null)
                    {
                        NWDLocalization tLocalization = LocalizationReference.GetObject();
                        if (tLocalization != null)
                        {
                            string tTextString = "";
                            if (sUseBaseString == true)
                            {
                                tTextString = tLocalization.TextValue.GetBaseString();
                            }
                            else
                            {
                                tTextString = tLocalization.GetLocalString();
                            }

                            // Enrich text
                            TextTarget.text = Enrichment(tTextString);
                        }
                    }
                }
            }
            //TextMesh 
            if (TextMeshBinding == true)
            {
                // if not pluging
                if (TextMeshTarget == null)
                {
                    // get root text
                    TextMeshTarget = GetComponent<TextMesh>();
                }

                // if not find in root object
                if (TextMeshTarget == null)
                {
                    // get first children text
                    TextMeshTarget = GetComponentInChildren<TextMesh>();
                }

                // TextMesh found
                if (TextMeshTarget != null)
                {
                    // set the localizable text
                    if (LocalizationReference != null)
                    {
                        NWDLocalization tLocalization = LocalizationReference.GetObject();
                        if (tLocalization != null)
                        {
                            string tTextString = "";
                            if (sUseBaseString == true)
                            {
                                tTextString = tLocalization.TextValue.GetBaseString();
                            }
                            else
                            {
                                tTextString = tLocalization.GetLocalString();
                            }

                            // Enrich text
                            TextMeshTarget.text = Enrichment(tTextString);
                        }
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        void Awake()
        {
            Localize(false);
        }
        //-------------------------------------------------------------------------------------------------------------
        string Enrichment(string sText)
        {
            return NWDAccountNickname.Enrichment(NWDLocalization.Enrichment(sText));
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================