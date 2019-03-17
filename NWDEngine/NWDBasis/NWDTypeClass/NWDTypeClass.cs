﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using System;
using UnityEngine;

//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDTypeClass
    {
        //-------------------------------------------------------------------------------------------------------------
        public virtual string InternalKeyValue()
        {
            return string.Empty;
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual string InternalDescriptionValue()
        {
            return string.Empty;
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual string ReferenceValue()
        {
            return string.Empty;
        }
        //-------------------------------------------------------------------------------------------------------------
        //public virtual string ClassNameUsedValue()
        //{
        //    return string.Empty;
        //}
        //-------------------------------------------------------------------------------------------------------------
        public virtual bool DataIntegrityState()
        {
            return true;
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual bool TrashState()
        {
            return false;
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void TrashAction()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void UpdateIntegrityAction()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual bool EnableState()
        {
            return true;
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual bool ReachableState()
        {
            return true;
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual bool InGameSaveState()
        {
            return true;
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void SetCurrentGameSave()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public string DatasMenu()
        {
            string rReturn = InternalKeyValue() + " <" + ReferenceValue() + ">";
            rReturn = rReturn.Replace("/", " ");
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void InsertDataProceedWithTransaction()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void InsertDataProceed()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void InsertDataFinish()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void UpdateDataProceedWithTransaction()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void UpdateDataProceed()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void UpdateDataFinish()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void DeleteDataProceedWithTransaction()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void DeleteDataProceed()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void DeleteDataFinish()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void AnalyzeData()
        {

        }
#if UNITY_EDITOR
        //-------------------------------------------------------------------------------------------------------------
        public virtual void NodeCardAnalyze(NWDNodeCard sCard)
        {

        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual float AddOnNodeDrawWidth(float sDocumentWidth)
        {
            return 250.0f;
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual float AddOnNodeDrawHeight(float sCardWidth)
        {
            return 130.0f;
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void AddOnNodeDraw(Rect sRect, bool sPropertysGroup)
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void AddOnNodePropertyDraw(string sPpropertyName, Rect sRect)
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual Color AddOnNodeColor()
        {
            return Color.white;
        }
#endif
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class NWDClassTrigrammeAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Trigramme;
        //-------------------------------------------------------------------------------------------------------------
        public NWDClassTrigrammeAttribute(string sTrigramme)
        {
            this.Trigramme = sTrigramme;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class NWDClassServerSynchronizeAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public bool ServerSynchronize;
        //-------------------------------------------------------------------------------------------------------------
        public NWDClassServerSynchronizeAttribute(bool sServerSynchronize)
        {
            this.ServerSynchronize = sServerSynchronize;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class NWDClassDescriptionAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Description;
        //-------------------------------------------------------------------------------------------------------------
        public NWDClassDescriptionAttribute(string sDescription)
        {
            this.Description = sDescription;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class NWDClassMenuNameAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string MenuName;
        //-------------------------------------------------------------------------------------------------------------
        public NWDClassMenuNameAttribute(string sMenuName)
        {
            this.MenuName = sMenuName;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [AttributeUsage(AttributeTargets.Property)]
    public class NWDNeedAccountAvatarAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public static string PHPstring(string sPropertyName)
        {
            return "if ($uuid!= $tRow['" + sPropertyName + "']) {$ACC_NEEDED['NWDAccountAvatar'][$tRow['" + sPropertyName + "']]= true;}\n";
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [AttributeUsage(AttributeTargets.Property)]
    public class NWDNeedUserAvatarAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public static string PHPstring(string sPropertyName)
        {
            return "if ($uuid!= $tRow['" + sPropertyName + "']) {$ACC_NEEDED['NWDUserAvatar'][$tRow['" + sPropertyName + "']]= true;}\n";
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [AttributeUsage(AttributeTargets.Property)]
    public class NWDNeedAccountNicknameAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public static string PHPstring(string sPropertyName)
        {
            return "if ($uuid!= $tRow['" + sPropertyName + "']) {$ACC_NEEDED['NWDAccountNickname'][$tRow['"+sPropertyName+"']]= true;}\n";
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [AttributeUsage(AttributeTargets.Property)]
    public class NWDNeedUserNicknameAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public static string PHPstring(string sPropertyName)
        {
            return "if ($uuid!= $tRow['" + sPropertyName + "']) {$ACC_NEEDED['NWDUserNickname'][$tRow['" + sPropertyName + "']]= true;}\n";
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [AttributeUsage(AttributeTargets.Property)]
    public class NWDNeedReferenceAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string ClassName;
        public Type ClassType;
        //-------------------------------------------------------------------------------------------------------------
        public NWDNeedReferenceAttribute(Type sClassType)
        {
            this.ClassName = sClassType.Name;
            this.ClassType = sClassType;
        }
        //-------------------------------------------------------------------------------------------------------------
        public string PHPstring(string sPropertyName)
        {
            return "$REF_NEEDED['"+this.ClassName+"'][$tRow['" + sPropertyName + "']]= true;\n";
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================