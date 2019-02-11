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
using UnityEngine;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [NWDClassServerSynchronizeAttribute(true)]
    [NWDClassTrigrammeAttribute("DLG")]
    [NWDClassDescriptionAttribute("Dialog descriptions Class")]
    [NWDClassMenuNameAttribute("Dialog")]
    public partial class NWDDialog : NWDBasis<NWDDialog>
    {
        //-------------------------------------------------------------------------------------------------------------
        [NWDGroupStartAttribute("Availability schedule ", true, true, true)]
        [NWDTooltips("Availability schedule of this Dialog")]
        public NWDDateTimeScheduleType AvailabilitySchedule
        {
            get; set;
        }
        [NWDGroupEndAttribute]
        [NWDGroupSeparator]
        [NWDGroupStartAttribute("Reply for previous Dialog (optional)", true, true, true)]
        [NWDTooltipsAttribute("The list and quantity of ItemGroup required to show this answer and this dialog")]
        public NWDReferencesConditionalType<NWDItemGroup> RequiredItemGroups
        {
            get; set;
        }
        [NWDTooltipsAttribute("The list and quantity of Item required to show this answer and this dialog")]
        public NWDReferencesConditionalType<NWDItem> RequiredItems
        {
            get; set;
        }
        [NWDTooltipsAttribute("The type of button to use for this answer : " +
                              "\n •None, " +
                              "\n •Default, " +
                              "\n •Cancel, " +
                              "\n •Validate, " +
                              "\n •Destructive, " +
                              "\n •etc." +
                              "")]
        public NWDDialogAnswerType AnswerType
        {
            get; set;
        }
        [NWDTooltipsAttribute("The type of answer " +
                              "\n •Normal (choose answer),  " +
                              "\n •Sequent (just continue next, perhaps not show button)," +
                              "\n •Step ( the next time restart quest here), " +
                              "\n •Reset delete the last refertence answer used" +
                              "")]
        public NWDDialogState AnswerState
        {
            get; set;
        }
        [NWDTooltipsAttribute("The random limit in range to [0-1] ")]
        //[NWDIf("AnswerState", (int)NWDDialogState.Random)]
        [NWDFloatSlider(0.0F, 1.0F)]
        public float RandomFrequency
        {
            get; set;
        }
        [NWDTooltipsAttribute("This answer change the quest state to ... " +
                              "\n •None (do nothing),  " +
                              "\n •Start, " +
                              "\n •StartAlternate, " +
                              "\n •Accept, " +
                              "\n •Refuse, " +
                              "\n •Success, " +
                              "\n •Cancel, " +
                              "\n •Failed" +
                              "")]
        public NWDQuestState QuestStep
        {
            get; set;
        }
        [NWDTooltipsAttribute("Select language and write your answer in this language")]
        public NWDLocalizableStringType Answer
        {
            get; set;
        }
        [NWDTooltips("Add an action to this answer")]
        public NWDReferencesListType<NWDAction> ActionOnAnswer
        {
            get; set;
        }
        [NWDGroupEnd]
        [NWDGroupSeparator]
        [NWDGroupStartAttribute("Dialog", true, true, true)]
        [NWDTooltipsAttribute("Select the character who says the dialog")]
        public NWDReferenceType<NWDCharacter> CharacterReference
        {
            get; set;
        }
        [NWDTooltipsAttribute("Select the character emotion to illustrate this Dialog")]
        public NWDCharacterEmotion CharacterEmotion
        {
            get; set;
        }
        [NWDTooltipsAttribute("Select the character position in screen")]
        public NWDCharacterPositionType CharacterPosition
        {
            get; set;
        }
        [NWDTooltipsAttribute("Select the Bubble style to content this Dialog" +
                              "\n •Speech," +
                              "\n •Whisper," +
                              "\n •Thought," +
                              "\n •Scream, " +
                              "")]
        public NWDBubbleStyleType BubbleStyle
        {
            get; set;
        }
        [NWDTooltips("Add an action to this dialog")]
        public NWDReferencesListType<NWDAction> ActionOnDialog
        {
            get; set;
        }
        [NWDTooltipsAttribute("Select language and write your dialog in this language " +
                              "\nreplace by user : @nickname@ (old system)" +
                              "\nreplace by user : @nicknameid@ (old system)" +
                              "\nreplace by user : #Nickname#" +
                              "\nreplace by user : #Nicknameid#" +
                              "")]
        public NWDLocalizableLongTextType Dialog
        {
            get; set;
        }
        [NWDTooltipsAttribute("Select characters to use in dialog by these tags" +
                              "\n •for Fistname : #F1# #F2# …" +
                              "\n •for Lastname : #L1# #L2# …" +
                              "\n •for Nickname : #N1# #N2# …" +
                              "")]
        public NWDReferencesListType<NWDCharacter> ReplaceCharacters
        {
            get; set;
        }

        [NWDTooltipsAttribute("Select items to use in message by these tags" +
                              "\n •for item name singular #I1# #I2# …" +
                              "\n •for item name plural #I1s# #I2s# …" +
                              "\n •for quantity and item name #xI1# #xI2# …" +
                              "")]
        public NWDReferencesQuantityType<NWDItem> ReplaceItems
        {
            get; set;
        }
        [NWDTooltipsAttribute("Select itemgroups to use item to describe the group in message by these tags" +
                              "\n •for item to describe name singular #G1# #G2# …" +
                              "\n •for item to describe name plural #G1s# #G2s# …" +
                              "\n •for quantity and item to describe name #xG1# #xG2# …" +
                              "")]
        public NWDReferencesQuantityType<NWDItemGroup> ReplaceItemGroups
        {
            get; set;
        }
        [NWDTooltipsAttribute("Select Pack to use item to describe the pack in message by these tags" +
                              "\n •for item to describe name singular #P1# #P2# …" +
                              "\n •for item to describe name plural #P1s# #P2s# …" +
                              "\n •for quantity and item to describe name #xP1# #xP2# …" +
                              "")]
        public NWDReferencesQuantityType<NWDPack> ReplacePacks
        {
            get; set;
        }
        public NWDReferenceType<NWDYoghurtLyric> YoghurtLyric {
            get; set;
        }
        [NWDGroupEndAttribute]
        [NWDGroupSeparator]
        [NWDGroupStartAttribute("List of next dialogs (and replies)", true, true, true)]
        public NWDReferencesListType<NWDDialog> NextDialogs
        {
            get; set;
        }
        [NWDGroupEndAttribute]
        [NWDGroupSeparator]
        [NWDGroupStartAttribute("Option Quest", true, true, true)]
        [NWDTooltipsAttribute("The quest launched after this dialog")]
        public NWDReferenceType<NWDQuest> NextQuest
        {
            get; set;
        }
        [NWDGroupEndAttribute]
        [NWDGroupSeparator]
        [NWDGroupStartAttribute("For the quest'sBook", true, true, true)]
        [NWDTooltipsAttribute("The resume to write in the quest book (optional)")]
        public NWDLocalizableTextType Resume
        {
            get; set;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================