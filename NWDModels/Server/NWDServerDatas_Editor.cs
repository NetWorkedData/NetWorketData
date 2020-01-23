﻿//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:29:11
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================

#if UNITY_EDITOR
using System;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Net;
using System.Linq;
using System.Net.Sockets;

//=====================================================================================================================
namespace NetWorkedData
{
    // doc to read to finish script : https://www.cyberciti.biz/tips/how-do-i-enable-remote-access-to-mysql-database-server.html

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDServerDatas : NWDBasis
    {
        //-------------------------------------------------------------------------------------------------------------
        public override float AddonEditorHeight(float sWidth)
        {
            float tYadd = NWDGUI.AreaHeight(NWDGUI.kMiniButtonStyle.fixedHeight, 100);
            return tYadd;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonEditor(Rect sRect)
        {
            Rect[,] tMatrix = NWDGUI.DiviseArea(sRect, 2, 100);
            int tI = 0;
            GUIStyle tSyleTextArea = new GUIStyle(GUI.skin.textArea);
            NWDGUI.Separator(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]));
            tI++;

            if (GUI.Button(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]), "Find IP from Server (NWDServerDomain)"))
            {
                string tLocalIP = "0.0.0.0";
                NWDServerDomain tDomain = Server.GetRawData();
                if (tDomain != null)
                {
                    foreach (IPAddress tIP in Dns.GetHostAddresses(tDomain.ServerDNS))
                    {
                        if (tIP.AddressFamily == AddressFamily.InterNetwork)
                        {
                            tLocalIP = tIP.ToString();
                        }
                    }
                    IP.SetValue(tLocalIP);
                }
                else
                {
                    IP.SetValue("0.0.0.0");
                }

            }
            tI++;

            if (GUI.Button(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]), "Open Terminal"))
            {
                // /Applications/Utilities/Terminal.app/Contents/MacOS/Terminal
                FileInfo tFileInfo = new FileInfo("/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal");
                System.Diagnostics.Process.Start(tFileInfo.FullName);
            }
            tI++;

            NWDGUI.Separator(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]));
            tI++;

            GUI.Label(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]), "Install Server SSH");
            tI++;
            string tSSH = NWDServerInstall.CommandInstallServerSSH(Distribution, IP.GetValue(), Port, Root_User, Root_Password.GetValue(), Admin_User, Admin_Password.GetValue());
            int tLineSSH = Mathf.CeilToInt(tSyleTextArea.CalcHeight(new GUIContent(tSSH), sRect.width) / tSyleTextArea.fixedHeight);
            //GUI.TextArea(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI + tLineSSH]), tSSH);
            //tI += tLineSSH;

            GUI.TextArea(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI + 10]), tSSH);
            tI += 11;

            GUI.Label(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]), "Install Server MySQL command");
            tI++;
            GUI.TextArea(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI + 10]), NWDServerInstall.CommandInstallServerMySQL(Distribution, IP.GetValue(), Port, Admin_User, Admin_Password.GetValue(), Root_Password.GetValue(), Root_MysqlPassword.GetValue(), External, PhpMyAdmin));
            tI += 11;

            if (PhpMyAdmin == true)
            {

                if (GUI.Button(tMatrix[0, tI], "http://" + IP + "/phpmyadmin/"))
                {
                    Application.OpenURL("http://" + IP + "/phpmyadmin/");
                }
                if (GUI.Button(tMatrix[1, tI], "https://" + IP + "/phpmyadmin/"))
                {
                    Application.OpenURL("https://" + IP + "/phpmyadmin/");
                }
                tI++;
            }

            NWDGUI.Separator(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]));
            tI++;
            
            GUI.Label(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]), "Install Database command");
            tI++;
            GUI.TextArea(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI + 10]), NWDServerInstall.CommandInstallDatabase(Distribution, IP.GetValue(), Port, Admin_User, Admin_Password.GetValue(), Root_Password.GetValue(), Root_MysqlPassword.GetValue(), MySQLUser, MySQLPassword.GetValue(), MySQLBase));
            tI += 11;
            if (GUI.Button(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]), "Push force dev editor data"))
            {
                //TODO : push data ...
            }
            tI++;
            if (GUI.Button(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]), "Push force preprod editor data"))
            {
                //TODO : push data ...
            }
            tI++;
            if (GUI.Button(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]), "Push force prod editor data"))
            {
                //TODO : push data ...
            }
            tI++;

            NWDGUI.Separator(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]));
            tI++;

            /*
            if (GUI.Button(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]), "copy for dev"))
            {
                NWDAppEnvironment tDev = NWDAppConfiguration.SharedInstance().DevEnvironment;
                tDev.ServerHost = "localhost";
                tDev.ServerUser = MySQLUser;
                tDev.ServerPassword = MySQLPassword.GetValue();
                tDev.ServerBase = MySQLBase;
                NWDAppEnvironmentConfigurationManager.Refresh();
            }
            tI++;
            if (GUI.Button(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]), "copy for preprod"))
            {
                NWDAppEnvironment tPreprod= NWDAppConfiguration.SharedInstance().PreprodEnvironment;
                tPreprod.ServerHost = "localhost";
                tPreprod.ServerUser = MySQLUser;
                tPreprod.ServerPassword = MySQLPassword.GetValue();
                tPreprod.ServerBase = MySQLBase;
                NWDAppEnvironmentConfigurationManager.Refresh();
            }
            tI++;
            if (GUI.Button(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]), "copy for prod"))
            {
                NWDAppEnvironment tProd = NWDAppConfiguration.SharedInstance().ProdEnvironment;
                tProd.ServerHost = "localhost";
                tProd.ServerUser = MySQLUser;
                tProd.ServerPassword = MySQLPassword.GetValue();
                tProd.ServerBase = MySQLBase;
                NWDAppEnvironmentConfigurationManager.Refresh();
            }
            tI++;

            NWDGUI.Separator(NWDGUI.AssemblyArea(tMatrix[0, tI], tMatrix[1, tI]));
            tI++;
            */
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif