//=====================================================================================================================
//
//  ideMobi 2020©
//
//=====================================================================================================================
// Define the use of Log and Benchmark only for this file!
// Add NWD_VERBOSE in scripting define symbols (Edit->Project Settings…->Player->[Choose Plateform]->Other Settings->Scripting Define Symbols)
#if NWD_VERBOSE
#if UNITY_EDITOR
#define NWD_LOG
#define NWD_BENCHMARK
#elif DEBUG
//#define NWD_LOG
//#define NWD_BENCHMARK
#endif
#else
#undef NWD_LOG
#undef NWD_BENCHMARK
#endif
//=====================================================================================================================
#if NWD_INTERMESSAGE
using System;
using System.Collections.Generic;
using UnityEngine;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [Serializable]
    public class NWDUserInterMessageConnection : NWDConnection<NWDUserInterMessage>
    {
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDMessage : NWDBasis
    {
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserInterMessage PostCustomNotification(
                                          NWDReferencesListType<NWDCharacter> sReplaceCharacters = null,
                                          NWDReferencesQuantityType<NWDItem> sReplaceItems = null,
                                          NWDReferencesQuantityType<NWDItemPack> sReplaceItemPack = null,
                                          NWDReferencesQuantityType<NWDPack> sReplacePacks = null,
                                          NWDUserNotificationDelegate sValidationbBlock = null,
                                          NWDUserNotificationDelegate sCancelBlock = null)
        {
            NWDUserInterMessage rUserIntermessage = NWDUserInterMessage.CreateNewMessageWith(this,
                    string.Empty,
                    0,
                    sReplaceCharacters,
                    sReplaceItems,
                    sReplaceItemPack,
                    sReplacePacks);
            NWDUserNotification tNotification = new NWDUserNotification(rUserIntermessage, sValidationbBlock, sCancelBlock);
            tNotification.Post();
            return rUserIntermessage;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDUserNotification
    {
        //-------------------------------------------------------------------------------------------------------------
        private NWDUserInterMessage _UserMessage;
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserNotification(NWDUserInterMessage sUserInterMessage, NWDUserNotificationDelegate sValidationbBlock = null, NWDUserNotificationDelegate sCancelBlock = null)
        {
            _Origin = NWDUserNotificationOrigin.InterMessage;
            _UserMessage = sUserInterMessage;
            _ValidationDelegate = sValidationbBlock;
            _CancelDelegate = sCancelBlock;
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserNotification PostNotification(NWDUserInterMessage sUserInterMessage, NWDUserNotificationDelegate sValidationbBlock = null, NWDUserNotificationDelegate sCancelBlock = null)
        {
            NWDUserNotification rReturn = new NWDUserNotification(sUserInterMessage, sValidationbBlock, sCancelBlock);
            rReturn.Post();
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDUserInterMessage : NWDBasis
    {
        //-------------------------------------------------------------------------------------------------------------
        public delegate void messageBlock(bool error, NWDOperationResult result);
        public messageBlock messageBlockDelegate;
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserInterMessage()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserInterMessage(bool sInsertInNetWorkedData) : base(sInsertInNetWorkedData)
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void Initialization()
        {
            if (PublicationDate==null)
            {
                PublicationDate = new NWDDateTimeType();
            }
            PublicationDate.SetDateTime(DateTime.Now);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static NWDUserInterMessage CreateNewMessageWith(NWDMessage sMessage,
                                          string sReceiver,
                                          int sPushDelayInSeconds = 0,
                                          NWDReferencesListType<NWDCharacter> sReplaceCharacters = null,
                                          NWDReferencesQuantityType<NWDItem> sReplaceItems = null,
                                          NWDReferencesQuantityType<NWDItemPack> sReplaceItemPack = null,
                                          NWDReferencesQuantityType<NWDPack> sReplacePacks = null
                                         )
        {
            NWDUserInterMessage rInterMessage = NWDBasisHelper.NewData<NWDUserInterMessage>();

            // Set Sender
            string tPublisher = NWDAppEnvironment.SelectedEnvironment().GetAccountReference();
            rInterMessage.Sender.SetReference(tPublisher);
            rInterMessage.PublicationDate.SetDateTime(DateTime.Now.AddSeconds(sPushDelayInSeconds));

            // Set Receiver
            rInterMessage.Receiver.SetReference(sReceiver);

            // Add Replaceable object if any set in Message
            /*if (sMessage.AttachmentItemPack != null)
            {
                Dictionary<NWDItemPack, int> tItemPacks = sMessage.AttachmentItemPack.GetObjectAndQuantity();
                foreach (KeyValuePair<NWDItemPack, int> pair in tItemPacks)
                {
                    NWDItemPack tItemPack = pair.Key;
                    int tItemPackQte = pair.Value;
                    if (sReplaceItemPack == null)
                    {
                        sReplaceItemPack = new NWDReferencesQuantityType<NWDItemPack>();
                    }
                    sReplaceItemPack.AddObjectQuantity(tItemPack, tItemPackQte);
                }
            }*/

            // Set Message and insert the Replaceable object
            rInterMessage.Message.SetData(sMessage);
            rInterMessage.Style = sMessage.Style;
            rInterMessage.Type = sMessage.Type;
#if NWD_USER_IDENTITY
            rInterMessage.ReplaceSenderNickname = NWDUserNickname.GetNickname();
#endif
            rInterMessage.ReplaceCharacters = sReplaceCharacters;
            rInterMessage.ReplaceItems = sReplaceItems;
            rInterMessage.ReplaceItemPacks = sReplaceItemPack;
            rInterMessage.ReplacePacks = sReplacePacks;
#if UNITY_EDITOR
#if NWD_USER_IDENTITY
            rInterMessage.InternalKey = NWDUserNickname.GetNickname() + " - " + sMessage.Title.GetBaseString();
#endif
#endif
            rInterMessage.Tag = NWDBasisTag.TagUserCreated;
            rInterMessage.SaveData();

            return rInterMessage;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static NWDUserInterMessage[] FindSenderDatas()
        {
            List<NWDUserInterMessage> rList = new List<NWDUserInterMessage>();
            NWDUserInterMessage[] tMessages = NWDBasisHelper.GetReachableDatas<NWDUserInterMessage>();
            foreach (NWDUserInterMessage tMessage in tMessages)
            {
                if (tMessage.Sender.GetReference() == NWDAccount.CurrentReference())
                {
                    rList.Add(tMessage);
                }
            }

            return rList.ToArray();
        }
        //-------------------------------------------------------------------------------------------------------------
        public static NWDUserInterMessage[] FindReceiverDatas()
        {
            List<NWDUserInterMessage> rList = new List<NWDUserInterMessage>();
            NWDUserInterMessage[] tMessages = NWDBasisHelper.GetReachableDatas<NWDUserInterMessage>();
            foreach (NWDUserInterMessage tMessage in tMessages)
            {
                if (tMessage.Receiver.GetReference() == NWDAccount.CurrentReference())
                {
                    rList.Add(tMessage);
                }
            }

            return rList.ToArray();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void SendMessage()
        {
            // Push System
            //TODO : set a push system here, not implemented yet

            //Ask server to generate a new Code Pin
            NWEOperationBlock tSuccess = delegate (NWEOperation bOperation, float bProgress, NWEOperationResult bResult)
            {
                if (messageBlockDelegate != null)
                {
                    NWDOperationResult tResult = bResult as NWDOperationResult;
                    messageBlockDelegate(false, tResult);
                }
            };
            NWEOperationBlock tFailed = delegate (NWEOperation bOperation, float bProgress, NWEOperationResult bResult)
            {
                if (messageBlockDelegate != null)
                {
                    NWDOperationResult tResult = bResult as NWDOperationResult;
                    messageBlockDelegate(true, tResult);
                }
            };

            // Sync NWDUserInterMessage
             NWDBasisHelper.SynchronizationFromWebService<NWDUserInterMessage>(tSuccess, tFailed);
        }
        //-------------------------------------------------------------------------------------------------------------
        public string MessageRichText(bool sBold = true)
        {
            string rReturn = string.Empty;
            NWDMessage tMessage = Message.GetRawData();
            if (tMessage != null)
            {
                rReturn = tMessage.Description.GetLocalString();
                rReturn = Enrichment(rReturn, null, sBold);
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public string MessageRichTextForLanguage(string sLanguage, bool sBold = true)
        {
            string rReturn = string.Empty;
            NWDMessage tMessage = Message.GetRawData();
            if (tMessage != null)
            {
                rReturn = tMessage.Description.GetLanguageString(sLanguage);
                rReturn = Enrichment(rReturn, NWDDataManager.SharedInstance().PlayerLanguage, sBold);
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public string TitleRichText(bool sBold = true)
        {
            string rReturn = string.Empty;
            NWDMessage tMessage = Message.GetRawData();
            if (tMessage != null)
            {
                rReturn = tMessage.Title.GetLocalString();
                rReturn = Enrichment(rReturn, null, sBold);
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public string TitleRichTextForLanguage(string sLanguage, bool sBold = true)
        {
            string rReturn = string.Empty;
            NWDMessage tMessage = Message.GetRawData();
            if (tMessage != null)
            {
                rReturn = tMessage.Title.GetLanguageString(sLanguage);
                rReturn = Enrichment(rReturn, NWDDataManager.SharedInstance().PlayerLanguage, sBold);
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public string ValidationRichText(bool sBold = true)
        {
            string rReturn = string.Empty;
            NWDMessage tMessage = Message.GetRawData();
            if (tMessage != null)
            {
                rReturn = tMessage.Validation.GetLocalString();
                rReturn = Enrichment(rReturn, null, sBold);
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public string ValidationRichTextForLanguage(string sLanguage,bool sBold = true)
        {
            string rReturn = string.Empty;
            NWDMessage tMessage = Message.GetRawData();
            if (tMessage != null)
            {
                rReturn = tMessage.Validation.GetLanguageString(sLanguage);
                rReturn = Enrichment(rReturn, null, sBold);
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public string CancelRichText(bool sBold = true)
        {
            string rReturn = string.Empty;
            NWDMessage tMessage = Message.GetRawData();
            if (tMessage != null)
            {
                rReturn = tMessage.Cancel.GetLocalString();
                rReturn = Enrichment(rReturn, null, sBold);
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public string CancelRichTextForLanguage(string sLanguage,bool sBold = true)
        {
            string rReturn = string.Empty;
            NWDMessage tMessage = Message.GetRawData();
            if (tMessage != null)
            {
                rReturn = tMessage.Cancel.GetLanguageString(sLanguage);
                rReturn = Enrichment(rReturn, null, sBold);
            }
            return rReturn;
        }
#if NWD_USER_IDENTITY
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserNickname PublisherNickname()
        {
            return NWDBasisHelper.GetCorporateFirstData<NWDUserNickname>(Sender.GetReference(), null);
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserNickname ReceiverNickname()
        {
            return NWDBasisHelper.GetCorporateFirstData<NWDUserNickname>(Receiver.GetReference(), null);
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserAvatar PublisherAvatar()
        {
            return NWDBasisHelper.GetCorporateFirstData<NWDUserAvatar>(Sender.GetReference(),NWDGameSave.SelectCurrentDataForAccount(Sender.GetReference()));
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDUserAvatar ReceiverAvatar()
        {
            return NWDBasisHelper.GetCorporateFirstData<NWDUserAvatar>(Receiver.GetReference(), NWDGameSave.SelectCurrentDataForAccount(Receiver.GetReference()));
        }
#endif
        //-------------------------------------------------------------------------------------------------------------
        public string Enrichment(string sText, string sLanguage = null, bool sBold = true)
        {
            string tBstart = "<b>";
            string tBend = "</b>";
            if (sBold == false)
            {
                tBstart = string.Empty;
                tBend = string.Empty;
            }

            // Replace Tag by user Nickname
            string rText = NWDLocalization.Enrichment(sText, sLanguage, sBold);
#if NWD_USER_IDENTITY
            rText = NWDUserNickname.Enrichment(rText, sBold);
#endif
#if NWD_ACCOUNT_IDENTITY
            rText = NWDAccountNickname.Enrichment(rText, sLanguage, sBold);
#endif

            // Replace Tag by sender Nickname
            rText = rText.Replace("#SenderNickname#", tBstart + ReplaceSenderNickname + tBend);
            //rText = rText.Replace("#SenderUniqueNickname#", tBstart + tPublisherID + tBend);

            // Replace Tag by Characters
            int tCpt = 0;
#if NWD_QUEST
            foreach(NWDCharacter k in ReplaceCharacters.GetReachableDatas())
            {
                rText = k.Enrichment(rText, tCpt, sLanguage, sBold);
                tCpt++;
            }
#endif

            // Replace Tag by Items
            tCpt = 0;
            foreach (NWDItem k in ReplaceItems.GetReachableDatas())
            {
                rText = k.Enrichment(rText, tCpt, sLanguage, sBold);
                tCpt++;
            }

            // Replace Tag by Items Groups
            tCpt = 0;
            foreach (NWDItemPack k in ReplaceItemPacks.GetReachableDatas())
            {
                rText = k.Enrichment(rText, tCpt, sLanguage, sBold);
                tCpt++;
            }

            // Replace Tag by Pack
            tCpt = 0;
            foreach (NWDPack k in ReplacePacks.GetReachableDatas())
            {
                rText = k.Enrichment(rText, tCpt, sLanguage, sBold);
                tCpt++;
            }

            return rText;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif
