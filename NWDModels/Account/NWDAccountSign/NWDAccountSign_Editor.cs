//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:42:12
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================

#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
//using BasicToolBox;
using UnityEditor;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDAccountSign : NWDBasis
    {
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
        string Email;
        string Password;
        string Social;
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addons editor interface.
        /// </summary>
        /// <returns>The editor height addon.</returns>
        /// <param name="sInRect">S in rect.</param>
        public override void AddonEditor(Rect sRect)
        {
            // Draw the interface addon for editor
            Rect[,] tMatrix = NWDGUI.DiviseArea(sRect, 1, 20);

            bool tActive = false;
            List<string> tEnvironment = new List<string>();

            if (DevSync >= 0)
            {
                tEnvironment.Add(NWDConstants.K_DEVELOPMENT_NAME);
                if (NWDAppEnvironment.SelectedEnvironment() == NWDAppConfiguration.SharedInstance().DevEnvironment)
                {
                    tActive = true;
                }
            }
            if (PreprodSync >= 0)
            {
                tEnvironment.Add(NWDConstants.K_PREPRODUCTION_NAME);
                if (NWDAppEnvironment.SelectedEnvironment() == NWDAppConfiguration.SharedInstance().PreprodEnvironment)
                {
                    tActive = true;
                }
            }
            if (ProdSync >= 0)
            {
                tEnvironment.Add(NWDConstants.K_PRODUCTION_NAME);
                if (NWDAppEnvironment.SelectedEnvironment() == NWDAppConfiguration.SharedInstance().ProdEnvironment)
                {
                    tActive = true;
                }
            }
            int tI = 0;
            NWDGUI.Separator(tMatrix[0, tI++]);

            if (tActive == false)
            {
                GUI.Label(tMatrix[0, tI++], "Not active in this environment");
            }
            else
            {


                if (GUI.Button(tMatrix[0, tI++], "Associate Editor Secret Key", NWDGUI.kMiniButtonStyle))
                {
                    RegisterDeviceEditor();
                }
                if (GUI.Button(tMatrix[0, tI++], "Associate Player Secret Key", NWDGUI.kMiniButtonStyle))
                {
                    RegisterDevicePlayer();
                }
                NWDGUI.Separator(tMatrix[0, tI++]);
                Email = EditorGUI.TextField(tMatrix[0, tI++], "Email", Email);
                Password = EditorGUI.TextField(tMatrix[0, tI++], "Password", Password);
                EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password));
                if (GUI.Button(tMatrix[0, tI++], "Associate login Password", NWDGUI.kMiniButtonStyle))
                {
                    RegisterEmailPassword(Email, Password);
                }
                EditorGUI.EndDisabledGroup();
                NWDGUI.Separator(tMatrix[0, tI++]);
                Social = EditorGUI.TextField(tMatrix[0, tI++], "Social", Social);
                EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(Social));
                if (GUI.Button(tMatrix[0, tI++], "Associate FacebookID", NWDGUI.kMiniButtonStyle))
                {
                    RegisterSocialNetwork(Social, NWDAccountSignType.Facebook);
                }
                if (GUI.Button(tMatrix[0, tI++], "Associate GoogleID", NWDGUI.kMiniButtonStyle))
                {
                    RegisterSocialNetwork(Social, NWDAccountSignType.Google);
                }
                EditorGUI.EndDisabledGroup();
                NWDGUI.Separator(tMatrix[0, tI++]);
                if (GUI.Button(tMatrix[0, tI++], "Associate Delete", NWDGUI.kMiniButtonStyle))
                {
                    RegisterDelete();
                }
                NWDGUI.Separator(tMatrix[0, tI++]);
                EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(SignHash));
                if (GUI.Button(tMatrix[0, tI++], "Test Sign in", NWDGUI.kMiniButtonStyle))
                {
                    NWDDataManager.SharedInstance().AddWebRequestSignIn(SignHash);
                }
                if (GUI.Button(tMatrix[0, tI++], "Test Sign in with error", NWDGUI.kMiniButtonStyle))
                {
                    NWDDataManager.SharedInstance().AddWebRequestSignIn(SignHash + "a");
                }
                if (GUI.Button(tMatrix[0, tI++], "Sign OUT", NWDGUI.kMiniButtonStyle))
                {
                    NWDDataManager.SharedInstance().AddWebRequestSignOut();
                }
                if (GUI.Button(tMatrix[0, tI++], "ResetSession", NWDGUI.kMiniButtonStyle))
                {
                    NWDAppEnvironment.SelectedEnvironment().ResetSession();
                }
                //if (GUI.Button(tMatrix[0, tI++], "Rescue", NWDGUI.kMiniButtonStyle))
                //{
                //    NWDDataManager.SharedInstance().AddWebRequestSignOut();
                //}
                EditorGUI.EndDisabledGroup();
                NWDGUI.Separator(tMatrix[0, tI++]);
                if (GUI.Button(tMatrix[0, tI++], "Crack estimation", NWDGUI.kMiniButtonStyle))
                {
                    NWEPassAnalyseWindow.SharedInstance().AnalyzePassword(SignHash);
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Addons editor intreface expected height.
        /// </summary>
        /// <returns>The editor expected height.</returns>
        public override float AddonEditorHeight(float sWidth)
        {
            // Height calculate for the interface addon for editor
            float tYadd = NWDGUI.AreaHeight(NWDGUI.kMiniButtonStyle.fixedHeight, 20);
            return tYadd;
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
        public override void AddOnNodeDraw(Rect sRect)
        {

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
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif