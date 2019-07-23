﻿//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Date		2019-4-12 18:41:35
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	NetWorkedData for Unity3D
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================

//=====================================================================================================================
//
// ideMobi copyright 2019
// All rights reserved by ideMobi
//
// Read License-en or Licence-fr
//
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BasicToolBox;
//=====================================================================================================================
namespace NetWorkedData
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDSector : NWDBasis<NWDSector>
    {
        //-------------------------------------------------------------------------------------------------------------
        public NWDSector()
        {
            //Debug.Log("NWDSector Constructor");
        }
        //-------------------------------------------------------------------------------------------------------------
        public NWDSector(bool sInsertInNetWorkedData) : base(sInsertInNetWorkedData)
        {
            //Debug.Log("NWDSector Constructor with sInsertInNetWorkedData : " + sInsertInNetWorkedData.ToString()+"");
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void Initialization()
        {
            //CardTypeLevel = CardType.Truc;
        }
        //-------------------------------------------------------------------------------------------------------------
        // prevent exec bad
        private void CascadeAnalyzePrevent()
        {
            // upgrade model
            if (ParentSectorList == null)
            {
                ParentSectorList = new NWDReferencesListType<NWDSector>();
            }
            if (ChildSectorList == null)
            {
                ChildSectorList = new NWDReferencesListType<NWDSector>();
            }
            if (CascadeSectorList == null)
            {
                CascadeSectorList = new NWDReferencesListType<NWDSector>();
            }
            // prevent basic circularity
            if (ParentSectorList.ConstaintsData(this) == true)
            {
                ParentSectorList.RemoveDatas(new NWDSector[] { this });
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        // analyze the cascade of children
        private void CascadeAnalyze(bool tUpdate = false)
        {
            CascadeAnalyzePrevent();
            bool tTest = true;
            while (tTest == true)
            {
                tTest = false;
                // restaure the good parents
                ChildSectorList.Flush();
                List<NWDSector> tChildrensDirect = GetChildrensDirect();
                ChildSectorList.AddDatas(tChildrensDirect.ToArray());

                CascadeSectorList.Flush();
                List<NWDSector> tCascade = GetCascade();
                CascadeSectorList.AddDatas(tCascade.ToArray());

                foreach (NWDSector tP in tCascade)
                {
                    if (ParentSectorList.ConstaintsData(tP))
                    {
                        ParentSectorList.RemoveDatas(new NWDSector[] { tP });
                        tTest = true;
                        break;
                    }
                }
            }
            if (tUpdate == true)
            {
                UpdateDataIfModifiedWithoutCallBack();
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        private void UpdateCascade()
        {
            CascadeAnalyzePrevent();
            //List<NWDCategory> tParentsDirect = GetParentsDirect();
            List<NWDSector> tParents = GetParents();
            foreach (NWDSector tParent in tParents)
            {
                if (tParent.ParentSectorList.ConstaintsData(this))
                {
                    ParentSectorList.RemoveDatas(new NWDSector[] { tParent });
                }
            }
            // test de cicrcularité
            CascadeAnalyze(false);
            ParentsUpdate();
#if UNITY_EDITOR
            // just for improvment
            BasisHelper().RepaintTableEditor();
            BasisHelper().RepaintInspectorEditor();
#endif
        }
        //-------------------------------------------------------------------------------------------------------------
        // update parent evrywhere
        private void ParentsUpdate()
        {
            List<NWDSector> tChildrenFound = new List<NWDSector>();

            // add know children
            foreach (NWDSector tData in ParentSectorList.GetReachableDatas())
            {
                tChildrenFound.Add(tData);
            }
            // analyze children lost by change
            foreach (NWDSector tData in BasisHelper().Datas)
            {
                if (tData != null)
                {
                    if (tData.ChildSectorList.ConstaintsData(this))
                    {
                        if (tChildrenFound.Contains(tData) == false)
                        {
                            tChildrenFound.Add(tData);
                        }
                    }
                }
            }
            foreach (NWDSector tData in tChildrenFound)
            {
                tData.CascadeAnalyze(true);
            }
            foreach (NWDSector tData in tChildrenFound)
            {
                tData.ParentsUpdate();
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        private List<NWDSector> GetParentsDirect()
        {
            List<NWDSector> rReturn = ParentSectorList.GetReachableDatasList();
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        private List<NWDSector> GetParents()
        {
            List<NWDSector> rReturn = new List<NWDSector>();
            ParentsFinder(rReturn, this);
            if (rReturn.Contains(this))
            {
                rReturn.Remove(this);
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        private static void ParentsFinder(List<NWDSector> sList, NWDSector sSector)
        {
            if (sList.Contains(sSector) == false)
            {
                sList.Add(sSector);
                // analyze children
                foreach (NWDSector tData in sSector.ParentSectorList.GetReachableDatas())
                {
                    if (tData != null)
                    {
                        if (sList.Contains(tData) == false)
                        {
                            ParentsFinder(sList, tData);
                        }
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        private List<NWDSector> GetChildrensDirect()
        {
            List<NWDSector> rReturn = new List<NWDSector>();
            // analyze children
            foreach (NWDSector tData in BasisHelper().Datas)
            {
                tData.CascadeAnalyzePrevent();
                if (tData != null)
                {
                    if (tData.ParentSectorList.ConstaintsData(this))
                    {
                        if (rReturn.Contains(tData) == false)
                        {
                            rReturn.Add(tData);
                        }
                    }
                }
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        private List<NWDSector> GetCascade()
        {
            List<NWDSector> rReturn = new List<NWDSector>();
            ChildrenFinder(rReturn, this);
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        private List<NWDSector> GetChildrens()
        {
            List<NWDSector> rReturn = new List<NWDSector>();
            ChildrenFinder(rReturn, this);
            if (rReturn.Contains(this))
            {
                rReturn.Remove(this);
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        private static void ChildrenFinder(List<NWDSector> sList, NWDSector sSector)
        {
            if (sList.Contains(sSector) == false)
            {
                sList.Add(sSector);
                // analyze children
                foreach (NWDSector tData in BasisHelper().Datas)
                {
                    tData.CascadeAnalyzePrevent();
                    if (tData != null)
                    {
                        if (tData.ParentSectorList.ConstaintsData(sSector))
                        {
                            if (sList.Contains(tData) == false)
                            {
                                ChildrenFinder(sList, tData);
                            }
                        }
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public bool Containts(NWDSector sSector)
        {
            bool rReturn = false;
            if (sSector == this)
            {
                rReturn = true;
            }
            else
            {
                rReturn = CascadeSectorList.ConstaintsData(sSector);
            }
            return rReturn;
        }

        //-------------------------------------------------------------------------------------------------------------
        public static bool CategoryIsContaintedIn(NWDSector sSector, List<NWDSector> sSectorsList)
        {
            bool rReturn = false;
            foreach (NWDSector tCategory in sSectorsList)
            {
                if (tCategory.Containts(sSector))
                {
                    rReturn = true;
                    break;
                }
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonUpdateMe()
        {
            UpdateCascade();
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void AddonDuplicateMe()
        {
            UpdateCascade();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
