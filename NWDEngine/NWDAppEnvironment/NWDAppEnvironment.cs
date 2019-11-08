﻿//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:24:47
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================

using System;
using UnityEngine;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDAppEnvironment
    {
        #region properties
        //-------------------------------------------------------------------------------------------------------------
        public bool Selected = false;
        public string Environment = NWDConstants.K_PRODUCTION_NAME;
        //-------------------------------------------------------------------------------------------------------------
        //public NWDAppEnvironmentPlayerStatut PlayerStatut = NWDAppEnvironmentPlayerStatut.Temporary;
        public string PlayerAccountReference = string.Empty;
        public string RequesToken = string.Empty;
        // for debug anti-crack
#if UNITY_EDITOR
        public string PreviewRequesToken = string.Empty;
        public string LastPreviewRequesToken = string.Empty;
#endif
        private string WithSpecialSDKI;
        //-------------------------------------------------------------------------------------------------------------
        //public string AnonymousPlayerAccountReference = string.Empty;
        // reccord the first anonymous value to restaure old original account
        //public string AnonymousResetPassword = string.Empty;
        // reccord the secretKey to reset token
        //-------------------------------------------------------------------------------------------------------------
        public string DataSHAPassword = string.Empty;
        public string DataSHAVector = string.Empty;
        public string SaltStart = string.Empty;
        public string SaltEnd = string.Empty;
        public int IPBanTimer = 3600;
        public int IPBanMaxTentative = 3;
        public bool IPBanActive = true;
        public int SFTPBalanceLoad = 50; // TODO : Rename LoadBalancingLimit
#if UNITY_EDITOR
        public NWDServerLanguage ServerLanguage = NWDServerLanguage.PHP;
        public string SaltServer = string.Empty;
        public bool MailBySMTP = false;
        public string MailHost = string.Empty;
        public int MailPort = 465;
        public string MailUserName = string.Empty;
        public string MailPassword = string.Empty;
        public string MailDomain = string.Empty;
        public string MailAuthentication = "plain";
        public string MailEnableStarttlsAuto = "true";
        public string MailOpenSSLVerifyMode = "peer";
        public string MailFrom = string.Empty;
        public string MailReplyTo = string.Empty;
        public string RescueEmail = "no-reply@my-web-site.com";
        public int RescueDelay = 3600;
        public int RescueLoginLength = 12;
        public int RescuePasswordLength = 24;
        public string ServerHost = "localhost";
        public string ServerUser = "user";
        public string ServerPassword = string.Empty;
        public string ServerBase = "myDatabase";
#endif
        public bool LogMode = true;
        public string AdminKey = string.Empty;
        public string AdminKeyHash = string.Empty;
        public bool AdminInPlayer = false;

        public int SaltFrequency = 300;
        public string AddressPing = "8.8.8.8";
        public string ServerHTTPS = "https://www.my-web-site.com/";
        public bool AllwaysSecureData = false;
        //public string FacebookAppID = string.Empty;
        //public string FacebookAppSecret = string.Empty;
        //public string GoogleAppKey = string.Empty;
        //public string UnityAppKey = string.Empty;
        //public string TwitterAppKey = string.Empty;
        public int BuildTimestamp = 0;
        public int TokenHistoric = 6;
        public string AppName = "MyGameApp";
        public string PreProdTimeFormat = "_yyyy_MM_dd_HHmmss"; // TODO: rename
        public string AppProtocol = "MyGameApp://";
        public int EditorWebTimeOut = 600;
        public int WebTimeOut = 10;

        public float SpeedOfGameTime = 1.0F;
        //public string Version = "0.00.00";

        //public string AccountsForTests = string.Empty;

        public string BuildDate = string.Empty;

        public Color CartridgeColor = new Color(1.0F, 1.0F, 1.0F);

        // TODO : check if working
        public bool ThreadPoolForce = true;
        public NWDWritingMode WritingModeLocal = NWDWritingMode.QueuedMainThread;
        public NWDWritingMode WritingModeWebService = NWDWritingMode.QueuedMainThread;
        public NWDWritingMode WritingModeEditor = NWDWritingMode.QueuedMainThread;
        //-------------------------------------------------------------------------------------------------------------
        public string GetServerHTTPS()
        {
            string rReturn = ServerHTTPS;
            NWDAccountInfos tAccountInfos = NWDAccountInfos.CurrentData();
            if (tAccountInfos.Server != null)
            {
                NWDServerDomain tServer = tAccountInfos.Server.GetReachableData();
                if (tServer != null)
                {
                    rReturn = tServer.ServerDNS;
                }
            }
            rReturn = NWDToolbox.CleanDNS(rReturn);
            return "https://" + rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public string GetConfigurationServerHTTPS()
        {
            string rReturn = NWDToolbox.CleanDNS(ServerHTTPS);
            return "https://" + rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion
        #region constructor
        //-------------------------------------------------------------------------------------------------------------
        public NWDAppEnvironment()
        {
            PlayerAccountReference = NWDToolbox.GenerateUniqueID();
            FormatVerification();
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDAppEnvironment(string sEnvironement, bool sSelected)
        {
            this.Environment = sEnvironement;
            this.Selected = sSelected;
            FormatVerification();
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion
        #region Class methods
        //-------------------------------------------------------------------------------------------------------------
        public static NWDAppEnvironment SelectedEnvironment()
        {
            return NWDAppConfiguration.SharedInstance().SelectedEnvironment();
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void SetEnvironment(NWDAppEnvironment sAppEnvironment)
        {
            if (NWDAppEnvironment.SelectedEnvironment() != sAppEnvironment)
            {
                foreach (NWDAppEnvironment tEnv in NWDAppConfiguration.SharedInstance().AllEnvironements())
                {
                    tEnv.Selected = false;
                }
                sAppEnvironment.Selected = true;
#if UNITY_EDITOR
                NWDAppEnvironmentChooser.Refresh();
#endif
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion
        #region instance methods
        //-------------------------------------------------------------------------------------------------------------
        public void CleanSecretKeyDevice()
        {
            WithSpecialSDKI = null;
        }
        //-------------------------------------------------------------------------------------------------------------
        public string SecretKeyDevice()
        {
            string rReturn;
            if (Application.isPlaying == true)
            {
                rReturn = SecretKeyDevicePlayer();
            }
            else if (Application.isEditor == true)
            {
                rReturn = SecretKeyDeviceEditor();
            }
            else
            {
                rReturn = "Hacker?";
            }
            if (string.IsNullOrEmpty(WithSpecialSDKI) == false)
            {
                rReturn = WithSpecialSDKI;
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public string SecretKeyDeviceEditor()
        {
            string rReturn;
            rReturn = NWESecurityTools.GenerateSha(SystemInfo.deviceUniqueIdentifier + SaltStart);
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        const string kSecretKeyDevicePlayerKey = "kSecretKeyDevicePlayerKey_dad42928";
        const int kSecretKeyDevicePlayerLength = 36;
        //-------------------------------------------------------------------------------------------------------------
        public string SecretKeyDevicePlayer()
        {
            string rReturn;
            if (NWDAppConfiguration.SharedInstance().AnonymousDeviceConnected == true)
            {
                rReturn = NWESecurityTools.GenerateSha(SystemInfo.deviceUniqueIdentifier + SaltEnd);
            }
            else
            {
                rReturn = NWEPrefsManager.ShareInstance().getString(kSecretKeyDevicePlayerKey, NWDToolbox.RandomStringUnix(kSecretKeyDevicePlayerLength));
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void SecretKeyDevicePlayerReset()
        {
            NWEPrefsManager.ShareInstance().set(kSecretKeyDevicePlayerKey, NWDToolbox.RandomStringUnix(kSecretKeyDevicePlayerLength));
        }
        //-------------------------------------------------------------------------------------------------------------
        public string AdminKeyHashGenerate()
        {
            return NWESecurityTools.GenerateSha("455" + AdminKey + "gytf");
        }
        //-------------------------------------------------------------------------------------------------------------
        public void FormatVerification()
        {
            // Debug.Log ("VerifySecurity");
            // clean the salts
            DataSHAPassword = NWDToolbox.SaltCleaner(DataSHAPassword);
            DataSHAVector = NWDToolbox.SaltCleaner(DataSHAVector);
            SaltStart = NWDToolbox.SaltCleaner(SaltStart);
            SaltEnd = NWDToolbox.SaltCleaner(SaltEnd);

#if UNITY_EDITOR
            SaltServer = NWDToolbox.SaltCleaner(SaltServer);
            // ServerPassword = NWDToolbox.SaltCleaner (ServerPassword);
            AdminKey = NWDToolbox.SaltCleaner(AdminKey);
            //check salts are not mull
            if (DataSHAPassword == string.Empty)
            {
                DataSHAPassword = NWDToolbox.RandomString(16);
            }
            if (DataSHAVector == string.Empty)
            {
                DataSHAVector = NWDToolbox.RandomString(16);
            }
            if (SaltStart == string.Empty)
            {
                SaltStart = NWDToolbox.RandomString(16);
            }
            if (SaltEnd == string.Empty)
            {
                SaltEnd = NWDToolbox.RandomString(16);
            }
            if (SaltServer == string.Empty)
            {
                SaltServer = NWDToolbox.RandomString(16);
            }
            if (SaltFrequency <= 400)
            {
                SaltFrequency = UnityEngine.Random.Range(400, 800);
            }
            if (ServerPassword == string.Empty)
            {
                ServerPassword = NWDToolbox.RandomString(16);
            }
            if (AdminKey == string.Empty)
            {
                AdminKey = NWDToolbox.RandomString(16);
            }
            if (TokenHistoric < 1 || TokenHistoric > 10)
            {
                TokenHistoric = 3;
            }
#endif
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Absolutes the date time in game time. With additional year.
        /// </summary>
        /// <returns>The date time in game time.</returns>
        public DateTime AbsoluteDateTimeInGameTime()
        {
            float tSpeedOfGameTime = SpeedOfGameTime;
            DateTime tNow = DateTime.Now;
            DateTime tNowTwo = new DateTime(tNow.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            float tTimestamp = (float)tNow.Subtract(tNowTwo).TotalSeconds;
            if (tSpeedOfGameTime > 0 && tSpeedOfGameTime < 1000)
            {
                tTimestamp = tTimestamp * tSpeedOfGameTime;
            }
            DateTime tDateInGame = new DateTime(tNow.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            tDateInGame = tDateInGame.AddSeconds(tTimestamp);
            return tDateInGame;
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Date the time in game time. Without additional year (loop in the same year… paradoxal seasons)
        /// </summary>
        /// <returns>The time in game time.</returns>
        public DateTime DateTimeInGameTime()
        {
            float tSpeedOfGameTime = SpeedOfGameTime;
            DateTime tNow = DateTime.Now;
            DateTime tNowTwo = new DateTime(tNow.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            float tTimestamp = (float)tNow.Subtract(tNowTwo).TotalSeconds;
            if (tSpeedOfGameTime > 0 && tSpeedOfGameTime < 1000)
            {
                tTimestamp = tTimestamp * tSpeedOfGameTime;
            }
            DateTime tDateInGame = new DateTime(tNow.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            tDateInGame = tDateInGame.AddSeconds(tTimestamp);
            DateTime tDateInGameResult = new DateTime(tNow.Year, tDateInGame.Month, tDateInGame.Day, tDateInGame.Hour, tDateInGame.Minute, tDateInGame.Second, DateTimeKind.Utc);
            return tDateInGameResult;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static float kSunRotationPerSeconds = 0.0014666667F; // ((360 /24)/60)/60 : 360° div by 24 hours, div by 60 minutes, div by 60 seconds : angle for one second
        //-------------------------------------------------------------------------------------------------------------
        public float RotationOfSunInGameTime()
        {
            //TODO Test this solution 
            DateTime tNow = DateTime.Now;
            int tSeconds = tNow.Hour * 3600 + tNow.Minute * 60 + tNow.Second;
            return kSunRotationPerSeconds * tSeconds * SpeedOfGameTime;
        }
        //-------------------------------------------------------------------------------------------------------------
        #endregion
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================