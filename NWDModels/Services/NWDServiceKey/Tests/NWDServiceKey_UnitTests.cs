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
#if NWD_SERVICES
#if UNITY_INCLUDE_TESTS
using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using NetWorkedData;

//=====================================================================================================================
namespace NWDEditorTests
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDServiceKey_UnitTests : NWDClassTest
    {
        // herited NWDClassTest
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class NWDServiceKey_UnitTests : NWDClassTest
    {
        //-------------------------------------------------------------------------------------------------------------
        [Test]
        public void Test_NewData()
        {
            NWDUnitTests.CleanUnitTests(); // clean environment before
            NWDServiceKey tItemA = NWDUnitTests.NewLocalData<NWDServiceKey>();
            NWDServiceKey tItemB = NWDUnitTests.NewLocalData<NWDServiceKey>();
            Assert.AreNotEqual(tItemA.Reference, tItemB.Reference);
            NWDUnitTests.CleanUnitTests(); // clean environment after
        }
        //-------------------------------------------------------------------------------------------------------------
        [Test]
        public void Test_Duplicate()
        {
            NWDUnitTests.CleanUnitTests(); // clean environment before
            NWDServiceKey tItemA = NWDUnitTests.NewLocalData<NWDServiceKey>();
            tItemA.UpdateData();
            NWDServiceKey tItemB = NWDBasisHelper.DuplicateData(tItemA);
            Assert.AreNotEqual(tItemA.Reference, tItemB.Reference);
            NWDUnitTests.CleanUnitTests(); // clean environment after
        }
        //-------------------------------------------------------------------------------------------------------------
        [Test]
        public void Test_Integrity()
        {
            NWDUnitTests.CleanUnitTests(); // clean environment before
            NWDServiceKey tItemA = NWDUnitTests.NewLocalData<NWDServiceKey>();
            tItemA.Integrity = tItemA.Integrity + "a";
            Assert.IsFalse(tItemA.IntegrityIsValid());
            tItemA.UpdateIntegrity();
            Assert.IsTrue(tItemA.IntegrityIsValid());
            NWDUnitTests.CleanUnitTests(); // clean environment after
        }
        //-------------------------------------------------------------------------------------------------------------
        [Test]
        public void Test_GetRawDatas()
        {
            NWDUnitTests.CleanUnitTests(); // clean environment before
            int tCountInitial = NWDData.GetRawDatas<NWDServiceKey>().Length;
            Debug.Log(" -> initial count = " + tCountInitial.ToString());
            NWDServiceKey tItemA = NWDUnitTests.NewLocalData<NWDServiceKey>();
            NWDServiceKey[] tResult = NWDData.GetRawDatas<NWDServiceKey>();
            Assert.AreEqual(tResult.Length, tCountInitial + 1);
            if (tResult.Length == 1)
            {
                Assert.AreEqual(tResult[0], tItemA);
            }
            NWDServiceKey tItemB = NWDUnitTests.NewLocalData<NWDServiceKey>();
            NWDServiceKey tItemC = NWDUnitTests.NewLocalData<NWDServiceKey>();
            NWDServiceKey tItemD = NWDUnitTests.NewLocalData<NWDServiceKey>();
            NWDServiceKey tItemE = NWDUnitTests.NewLocalData<NWDServiceKey>();
            string tInternalKey = tItemE.InternalKey;
            NWDServiceKey tItemF = NWDUnitTests.NewLocalData<NWDServiceKey>();
            string tReference = tItemF.Reference;

            tResult = NWDData.GetRawDatas<NWDServiceKey>();
            Assert.AreEqual(tResult.Length, tCountInitial + 6);
            if (tResult.Length == 6)
            {
                Assert.IsTrue(Array.IndexOf(tResult, tItemA) >= 0);
                Assert.IsTrue(Array.IndexOf(tResult, tItemB) >= 0);
                Assert.IsTrue(Array.IndexOf(tResult, tItemC) >= 0);
                Assert.IsTrue(Array.IndexOf(tResult, tItemD) >= 0);
            }
            // test disable
            tItemB.DisableData();
            tResult = NWDData.GetRawDatas<NWDServiceKey>();
            Assert.AreEqual(tResult.Length, tCountInitial + 5);
            if (tResult.Length == 3)
            {
                Assert.IsTrue(Array.IndexOf(tResult, tItemB) < 0);
            }
            // test trash
            tItemC.TrashData();
            tResult = NWDData.GetRawDatas<NWDServiceKey>();
            Assert.AreEqual(tResult.Length, tCountInitial + 4);
            if (tResult.Length == 2)
            {
                Assert.IsTrue(Array.IndexOf(tResult, tItemC) < 0);
            }
            // test integrity
            tItemD.Integrity = tItemD.Integrity + "a";
            Assert.IsFalse(tItemD.IntegrityIsValid());
            tResult = NWDData.GetRawDatas<NWDServiceKey>();
            Assert.AreEqual(tResult.Length, tCountInitial + 3);
            if (tResult.Length == 1)
            {
                Assert.IsTrue(Array.IndexOf(tResult, tItemD) < 0);
            }
            // test internal key
            NWDServiceKey[] tItemE_R = NWDData.GetRawDatasByInternalKey<NWDServiceKey>(tInternalKey);
            Assert.AreEqual(tItemE_R.Length, 1);
            if (tItemE_R.Length == 1)
            {
                Assert.AreEqual(tItemE_R[0], tItemE);
            }
            tItemE.TrashData();
            tItemE_R = NWDData.GetRawDatasByInternalKey<NWDServiceKey>(tInternalKey);
            Assert.AreEqual(tItemE_R.Length, 0);

            // test reference
            NWDServiceKey tItemF_R = NWDData.GetRawDataByReference<NWDServiceKey>(tReference);
            Assert.AreEqual(tItemF_R, tItemF);
            tItemF.TrashData();
            tItemF_R = NWDData.GetRawDataByReference<NWDServiceKey>(tReference);
            Assert.AreEqual(tItemF_R, null);

            // Test change  account
            tItemF.UnTrashData();
            tItemF.EnableData();

            // Test change gamesave and account 
            NWDBasisHelper tHelper = NWDBasisHelper.FindTypeInfos(typeof(NWDServiceKey));
            NWDTemplateHelper tTemplateHelper = tHelper.TemplateHelper;

            string tOldAccount = NWDAccount.CurrentReference();
            NWDUnitTests.ShowFakeDevice();
            string tNewAccount = NWDUnitTests.SetNewAccount();
            tNewAccount = NWDAccount.CurrentReference();
            NWDUnitTests.ShowFakeDevice();
            Debug.Log("Account change from " + tOldAccount + " to " + tNewAccount + "");
            if (tTemplateHelper.GetAccountDependent() == NWDTemplateAccountDependent.NoAccountDependent)
            {
                Debug.Log("Account need to change without insidence");
                tItemF_R = NWDData.GetRawDataByReference<NWDServiceKey>(tReference);
                Assert.AreEqual(tItemF_R, tItemF);
            }
            else
            {
                Debug.Log("Account need to change without insidence");
                tItemF_R = NWDData.GetRawDataByReference<NWDServiceKey>(tReference);
                Assert.AreEqual(tItemF_R, null);
            }

            NWDUnitTests.CleanUnitTests(); // clean environment after
            int tCountFinal = NWDData.GetRawDatas<NWDServiceKey>().Length;
            Debug.Log(" -> final count = " + tCountFinal.ToString());
        }
        //-------------------------------------------------------------------------------------------------------------
        [Test]
        public void Test_GetReachableDatas()
        {
            NWDUnitTests.CleanUnitTests(); // clean environment before
            int tCountInitial = NWDData.GetReachableDatas<NWDServiceKey>().Length;
            Debug.Log(" -> initial count = " + tCountInitial.ToString());
            NWDServiceKey tItemA = NWDUnitTests.NewLocalData<NWDServiceKey>();
            NWDServiceKey[] tResult = NWDData.GetReachableDatas<NWDServiceKey>();
            Assert.AreEqual(tResult.Length, tCountInitial + 1);
            if (tResult.Length == 1)
            {
                Assert.AreEqual(tResult[0], tItemA);
            }
            NWDServiceKey tItemB = NWDUnitTests.NewLocalData<NWDServiceKey>();
            NWDServiceKey tItemC = NWDUnitTests.NewLocalData<NWDServiceKey>();
            NWDServiceKey tItemD = NWDUnitTests.NewLocalData<NWDServiceKey>();
            NWDServiceKey tItemE = NWDUnitTests.NewLocalData<NWDServiceKey>();
            string tInternalKey = tItemE.InternalKey;
            NWDServiceKey tItemF = NWDUnitTests.NewLocalData<NWDServiceKey>();
            string tReference = tItemF.Reference;

            tResult = NWDData.GetReachableDatas<NWDServiceKey>();
            Assert.AreEqual(tResult.Length, tCountInitial + 6);
            if (tResult.Length == 4)
            {
                Assert.IsTrue(Array.IndexOf(tResult, tItemA) >= 0);
                Assert.IsTrue(Array.IndexOf(tResult, tItemB) >= 0);
                Assert.IsTrue(Array.IndexOf(tResult, tItemC) >= 0);
                Assert.IsTrue(Array.IndexOf(tResult, tItemD) >= 0);
            }
            // test disable
            tItemB.DisableData();
            tResult = NWDData.GetReachableDatas<NWDServiceKey>();
            Assert.AreEqual(tResult.Length, tCountInitial + 5);
            if (tResult.Length == 3)
            {
                Assert.IsTrue(Array.IndexOf(tResult, tItemB) < 0);
            }
            // test trash
            tItemC.TrashData();
            tResult = NWDData.GetReachableDatas<NWDServiceKey>();
            Assert.AreEqual(tResult.Length, tCountInitial + 4);
            if (tResult.Length == 2)
            {
                Assert.IsTrue(Array.IndexOf(tResult, tItemC) < 0);
            }
            // test integrity
            tItemD.Integrity = tItemD.Integrity + "a";
            Assert.IsFalse(tItemD.IntegrityIsValid());
            tResult = NWDData.GetReachableDatas<NWDServiceKey>();
            Assert.AreEqual(tResult.Length, tCountInitial + 3);
            if (tResult.Length == 1)
            {
                Assert.IsTrue(Array.IndexOf(tResult, tItemD) < 0);
            }

            // test internal key
            NWDServiceKey[] tItemE_R = NWDData.GetReachableDatasByInternalKey<NWDServiceKey>(tInternalKey);
            Assert.AreEqual(tItemE_R.Length, 1);
            if (tItemE_R.Length == 1)
            {
                Assert.AreEqual(tItemE_R[0], tItemE);
            }
            tItemE.TrashData();
            tItemE_R = NWDData.GetReachableDatasByInternalKey<NWDServiceKey>(tInternalKey);
            Assert.AreEqual(tItemE_R.Length, 0);

            // test reference
            NWDServiceKey tItemF_R = NWDData.GetReachableDataByReference<NWDServiceKey>(tReference);
            Assert.AreEqual(tItemF_R, tItemF);
            tItemF.TrashData();
            tItemF_R = NWDData.GetReachableDataByReference<NWDServiceKey>(tReference);
            Assert.AreEqual(tItemF_R, null);

            // Test change  account
            tItemF.UnTrashData();
            tItemF.EnableData();

            // Test change gamesave and account 
            NWDBasisHelper tHelper = NWDBasisHelper.FindTypeInfos(typeof(NWDServiceKey));
            NWDTemplateHelper tTemplateHelper = tHelper.TemplateHelper;

            NWDGameSave tOldGameSave = NWDGameSave.CurrentData();
            NWDGameSave tNewGameSave = NWDGameSave.NewCurrent();
            if (tTemplateHelper.GetGamesaveDependent() == NWDTemplateGameSaveDependent.NoGameSaveDependent)
            {
                tItemF_R = NWDData.GetReachableDataByReference<NWDServiceKey>(tReference);
                Assert.AreEqual(tItemF_R, tItemF);
            }
            else
            {
                tItemF_R = NWDData.GetReachableDataByReference<NWDServiceKey>(tReference);
                Assert.AreEqual(tItemF_R, null);
            }
            string tOldAccount = NWDAccount.CurrentReference();
            NWDUnitTests.ShowFakeDevice();
            string tNewAccount = NWDUnitTests.SetNewAccount();
            tNewAccount = NWDAccount.CurrentReference();
            NWDUnitTests.ShowFakeDevice();
            Debug.Log("Account change from " + tOldAccount + " to " + tNewAccount + "");
            if (tTemplateHelper.GetAccountDependent() == NWDTemplateAccountDependent.NoAccountDependent)
            {
                Debug.Log("Account need to change without insidence");
                tItemF_R = NWDData.GetReachableDataByReference<NWDServiceKey>(tReference);
                Assert.AreEqual(tItemF_R, tItemF);
            }
            else
            {
                Debug.Log("Account and gamesave need to change with insidence");
                tItemF_R = NWDData.GetReachableDataByReference<NWDServiceKey>(tReference);
                Assert.AreEqual(tItemF_R, null);
            }

            NWDUnitTests.CleanUnitTests(); // clean environment after
            int tCountFinal = NWDData.GetReachableDatas<NWDServiceKey>().Length;
            Debug.Log(" -> final count = " + tCountFinal.ToString());
        }
        //-------------------------------------------------------------------------------------------------------------
        [Test]
        public void Test_GetCorporateDatas()
        {
            NWDUnitTests.CleanUnitTests(); // clean environment before
            string tCurrentAccount = NWDAccount.CurrentReference();
            int tCountInitial = NWDData.GetCorporateDatas<NWDServiceKey>(null, null).Length;
            Debug.Log(" -> initial count = " + tCountInitial.ToString());
            NWDServiceKey tItemA = NWDUnitTests.NewLocalData<NWDServiceKey>();
            NWDServiceKey[] tResult = NWDData.GetCorporateDatas<NWDServiceKey>(null, null);
            Assert.AreEqual(tResult.Length, tCountInitial + 1);
            if (tResult.Length == 1)
            {
                Assert.AreEqual(tResult[0], tItemA);
            }
            NWDServiceKey tItemB = NWDUnitTests.NewLocalData<NWDServiceKey>();
            NWDServiceKey tItemC = NWDUnitTests.NewLocalData<NWDServiceKey>();
            NWDServiceKey tItemD = NWDUnitTests.NewLocalData<NWDServiceKey>();
            NWDServiceKey tItemE = NWDUnitTests.NewLocalData<NWDServiceKey>();
            string tInternalKey = tItemE.InternalKey;
            NWDServiceKey tItemF = NWDUnitTests.NewLocalData<NWDServiceKey>();
            string tReference = tItemF.Reference;

            tResult = NWDData.GetCorporateDatas<NWDServiceKey>(null, null);
            Assert.AreEqual(tResult.Length, tCountInitial + 6);
            if (tResult.Length == 4)
            {
                Assert.IsTrue(Array.IndexOf(tResult, tItemA) >= 0);
                Assert.IsTrue(Array.IndexOf(tResult, tItemB) >= 0);
                Assert.IsTrue(Array.IndexOf(tResult, tItemC) >= 0);
                Assert.IsTrue(Array.IndexOf(tResult, tItemD) >= 0);
            }
            // test disable
            tItemB.DisableData();
            tResult = NWDData.GetCorporateDatas<NWDServiceKey>(null, null);
            Assert.AreEqual(tResult.Length, tCountInitial + 5);
            if (tResult.Length == 3)
            {
                Assert.IsTrue(Array.IndexOf(tResult, tItemB) < 0);
            }
            // test trash
            tItemC.TrashData();
            tResult = NWDData.GetCorporateDatas<NWDServiceKey>(null, null);
            Assert.AreEqual(tResult.Length, tCountInitial + 4);
            if (tResult.Length == 2)
            {
                Assert.IsTrue(Array.IndexOf(tResult, tItemC) < 0);
            }
            // test integrity
            tItemD.Integrity = tItemD.Integrity + "a";
            Assert.IsFalse(tItemD.IntegrityIsValid());
            tResult = NWDData.GetCorporateDatas<NWDServiceKey>(null, null);
            Assert.AreEqual(tResult.Length, tCountInitial + 3);
            if (tResult.Length == 1)
            {
                Assert.IsTrue(Array.IndexOf(tResult, tItemD) < 0);
            }

            // test internal key
            NWDServiceKey[] tItemE_R = NWDData.GetCorporateDatasByInternalKey<NWDServiceKey>(tInternalKey, null, null);
            Assert.AreEqual(tItemE_R.Length, 1);
            if (tItemE_R.Length == 1)
            {
                Assert.AreEqual(tItemE_R[0], tItemE);
            }
            tItemE.TrashData();
            tItemE_R = NWDData.GetCorporateDatasByInternalKey<NWDServiceKey>(tInternalKey, null, null);
            Assert.AreEqual(tItemE_R.Length, 0);

            // test reference
            NWDServiceKey tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, null, null);
            Assert.AreEqual(tItemF_R, tItemF);
            tItemF.TrashData();
            tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, null, null);
            Assert.AreEqual(tItemF_R, null);

            // Test change  account
            tItemF.UnTrashData();
            tItemF.EnableData();

            // Test change gamesave and account 
            NWDBasisHelper tHelper = NWDBasisHelper.FindTypeInfos(typeof(NWDServiceKey));
            NWDTemplateHelper tTemplateHelper = tHelper.TemplateHelper;

            tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, null, null);
            Assert.AreEqual(tItemF_R, tItemF);

            string tOldAccount = NWDAccount.CurrentReference();
            NWDGameSave tOldGamesave = NWDGameSave.CurrentData();
            NWDUnitTests.ShowFakeDevice();
            string tNewAccount = NWDUnitTests.SetNewAccount();
            NWDGameSave tNewGamesave = NWDGameSave.CurrentData();
            tNewAccount = NWDAccount.CurrentReference();
            NWDUnitTests.ShowFakeDevice();
            Debug.Log("Account change from " + tOldAccount + " to " + tNewAccount + "");
            if (tTemplateHelper.GetAccountDependent() == NWDTemplateAccountDependent.NoAccountDependent)
            {

                Debug.Log("Account need to change without insidence");
                tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, tOldAccount, null);
                Assert.AreEqual(tItemF_R, tItemF);
                tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, tNewAccount, null);
                Assert.AreEqual(tItemF_R, tItemF);
                tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, null, null);
                Assert.AreEqual(tItemF_R, tItemF);
            }
            else
            {
                if (tTemplateHelper.GetGamesaveDependent() == NWDTemplateGameSaveDependent.NoGameSaveDependent)
                {

                    Debug.Log("Account need to change with insidence but not the gamesave");
                    tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, tOldAccount, null);
                    Assert.AreEqual(tItemF_R, tItemF);
                    tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, tNewAccount, null);
                    Assert.AreEqual(tItemF_R, null);
                    tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, null, null);
                    Assert.AreEqual(tItemF_R, null);
                }
                else
                {
                    Debug.Log("Account and gamesave need to change with insidence");
                    tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, tOldAccount, tOldGamesave);
                    Assert.AreEqual(tItemF_R, tItemF);
                    tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, tOldAccount, tNewGamesave);
                    Assert.AreEqual(tItemF_R, null);
                    tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, tOldAccount, null);
                    Assert.AreEqual(tItemF_R, tItemF);
                    tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, tNewAccount, tNewGamesave);
                    Assert.AreEqual(tItemF_R, null);
                    tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, tNewAccount, tOldGamesave);
                    Assert.AreEqual(tItemF_R, null);
                    tItemF_R = NWDData.GetCorporateDataByReference<NWDServiceKey>(tReference, null, null);
                    Assert.AreEqual(tItemF_R, null);
                }
            }

            NWDUnitTests.CleanUnitTests(); // clean environment after
            int tCountFinal = NWDData.GetCorporateDatas<NWDServiceKey>(null, null).Length;
            Debug.Log(" -> final count = " + tCountFinal.ToString());
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif
#endif //NWD_SERVICES
