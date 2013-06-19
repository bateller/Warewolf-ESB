﻿using Dev2;
using Dev2.DataList.Contract.Binary_Objects;
using Dev2.Diagnostics;
using Dev2.Tests.Activities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Activities.Statements;
using System.Collections.Generic;
using Unlimited.Applications.BusinessDesignStudio.Activities;

namespace ActivityUnitTests.ActivityTest
{
    /// <summary>
    /// Summary description for CountRecordsTest
    /// </summary>
    [TestClass]
    public class DeleteRecordsActivityTest : BaseActivityUnitTest
    {
        public DeleteRecordsActivityTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        [ClassInitialize()]
        public static void BaseActivityUnitTestInitialize(TestContext testContext)
        {
//            var pathToRedis = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "redis-server.exe");
//            _redisProcess = Process.Start(pathToRedis);
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void BaseActivityUnitTestCleanup()
        {
            //_redisProcess.Kill();
        }        
        #endregion

        #region Delete Using Index

        [TestMethod]
        public void DeleteRecord_Using_Index_When_Record_Exists_Expected_RecordToBeRemovedFromRecordset_Success()
        {
            SetupArguments(ActivityStrings.DeleteRecordsDataListWithData, ActivityStrings.DeleteRecordsDataListShape, "[[recset1(2)]]", "[[res]]");

            IDSFDataObject result = ExecuteProcess();
            string expected = @"Success";
            string actual = string.Empty;
            List<string> recsetData = new List<string>();
            string error = string.Empty;
            GetScalarValueFromDataList(result.DataListID, "res", out actual, out error);
            recsetData = RetrieveAllRecordSetFieldValues(result.DataListID, "recset1", "field1", out error);
            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual(expected, actual);
                Assert.AreEqual(5, recsetData.Count);
                Assert.AreEqual("f1r3", recsetData[1]);
            }
            else
            {
                Assert.Fail(string.Format("The following errors occured while retrieving datalist items\r\nerrors:{0}", error));
            }
        }

        #endregion Delete Using Index

        #region Delete Using Star

        [TestMethod]
        public void DeleteRecord_Using_Star_When_Record_Exists_Expected_WholeRecordsetToBeRemoved_Success()
        {
            SetupArguments(ActivityStrings.DeleteRecordsDataListWithData, ActivityStrings.DeleteRecordsDataListShape, "[[recset1(*)]]", "[[res]]");

            IDSFDataObject result = ExecuteProcess();
            string expected = @"Success";
            string actual = string.Empty;
            List<string> recsetData = new List<string>();
            string error = string.Empty;
            GetScalarValueFromDataList(result.DataListID, "res", out actual, out error);
            recsetData = RetrieveAllRecordSetFieldValues(result.DataListID, "recset1", "field1", out error);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(0,recsetData.Count);
            //Assert.AreEqual("Index [ 1 ] is out of bounds", error);
        }

        #endregion Delete Using Star

        #region Delete Using Blank Index

        [TestMethod]
        public void DeleteRecord_Using_Blank_Index_When_Record_Exists_Expected_LastRecordToBeRemoved_Success()
        {
            SetupArguments(ActivityStrings.DeleteRecordsDataListWithData, ActivityStrings.DeleteRecordsDataListShape, "[[recset1()]]", "[[res]]");

            IDSFDataObject result = ExecuteProcess();
            string expected = @"Success";
            string actual = string.Empty;
            List<string> recsetData = new List<string>();
            string error = string.Empty;
            GetScalarValueFromDataList(result.DataListID, "res", out actual, out error);
            recsetData = RetrieveAllRecordSetFieldValues(result.DataListID, "recset1", "field1", out error);
            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual(expected, actual);
                Assert.AreEqual(5, recsetData.Count);
                Assert.AreEqual("f1r5", recsetData[4]);
            }
            else
            {
                Assert.Fail(string.Format("The following errors occured while retrieving datalist items\r\nerrors:{0}", error));
            }
        }

        #endregion Delete Using Blank Index

        #region Error Test Cases

        [TestMethod]
        public void DeleteRecord_Blank_RecordSet_Name_When_Record_Exists_Expected_No_Change_Failure()
        {
            SetupArguments(ActivityStrings.DeleteRecordsDataListWithData, ActivityStrings.DeleteRecordsDataListShape, "", "[[res]]");

            IDSFDataObject result = ExecuteProcess();
            string expected = @"Failure";
            string actual = string.Empty;
            List<string> recsetData = new List<string>();
            string error = string.Empty;
            GetScalarValueFromDataList(result.DataListID, "res", out actual, out error);
            recsetData = RetrieveAllRecordSetFieldValues(result.DataListID, "recset1", "field1", out error);
            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual(expected, actual);
                Assert.AreEqual(6, recsetData.Count);
            }
            else
            {
                Assert.Fail(string.Format("The following errors occured while retrieving datalist items\r\nerrors:{0}", error));
            }
        }

        [TestMethod]
        public void DeleteRecord_Blank_Result_Field_When_Record_Exists_Expected_RecordRemove()
        {
            SetupArguments(ActivityStrings.DeleteRecordsDataListWithData, ActivityStrings.DeleteRecordsDataListShape, "[[recset1(1)]]", "");

            IDSFDataObject result = ExecuteProcess();
            string expected = string.Empty;
            string actual = string.Empty;
            List<string> recsetData = new List<string>();
            string error = string.Empty;
            GetScalarValueFromDataList(result.DataListID, "res", out actual, out error);
            recsetData = RetrieveAllRecordSetFieldValues(result.DataListID, "recset1", "field1", out error);
            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual(expected, actual);
                Assert.AreEqual(5, recsetData.Count);
                Assert.AreEqual("f1r2", recsetData[0]);
            }
            else
            {
                Assert.Fail(string.Format("The following errors occured while retrieving datalist items\r\nerrors:{0}", error));
            }
        }

        [TestMethod]
        public void DeleteRecord_Scalar_When_Record_Exists_Expected_NoChange()
        {
            SetupArguments(ActivityStrings.DeleteRecordsDataListWithData, ActivityStrings.DeleteRecordsDataListShape, "[[res]]", "[[res]]");

            IDSFDataObject result = ExecuteProcess();
            string expected = @"Failure";
            string actual = string.Empty;
            List<string> recsetData = new List<string>();
            string error = string.Empty;
            GetScalarValueFromDataList(result.DataListID, "res", out actual, out error);
            recsetData = RetrieveAllRecordSetFieldValues(result.DataListID, "recset1", "field1", out error);
            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual(expected, actual);
                Assert.AreEqual(6, recsetData.Count);
            }
            else
            {
                Assert.Fail(string.Format("The following errors occured while retrieving datalist items\r\nerrors:{0}", error));
            }
        }

        [TestMethod]
        public void DeleteRecord_When_Index_Doesnt_Exist_Expected_NoChange()
        {
            SetupArguments(ActivityStrings.DeleteRecordsDataListWithData, ActivityStrings.DeleteRecordsDataListShape, "[[recset1(8)]]", "[[res]]");

            IDSFDataObject result = ExecuteProcess();
            string expected = @"Failure";
            string actual = string.Empty;
            List<string> recsetData = new List<string>();
            string error = string.Empty;
            GetScalarValueFromDataList(result.DataListID, "res", out actual, out error);
            recsetData = RetrieveAllRecordSetFieldValues(result.DataListID, "recset1", "field1", out error);
            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual(expected, actual);
                Assert.AreEqual(6, recsetData.Count);
            }
            else
            {
                Assert.Fail(string.Format("The following errors occured while retrieving datalist items\r\nerrors:{0}", error));
            }
        }

        [TestMethod]
        public void DeleteRecord_When_Field_Is_Included_Expected_RecordAtIndexToStillBeRemoved_Success()
        {
            SetupArguments(ActivityStrings.DeleteRecordsDataListWithData, ActivityStrings.DeleteRecordsDataListShape, "[[recset1(3).field1]]", "[[res]]");

            IDSFDataObject result = ExecuteProcess();
            string expected = @"Success";
            string actual = string.Empty;
            List<string> recsetData = new List<string>();
            string error = string.Empty;
            GetScalarValueFromDataList(result.DataListID, "res", out actual, out error);
            recsetData = RetrieveAllRecordSetFieldValues(result.DataListID, "recset1", "field1", out error);
            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual(expected, actual);
                Assert.AreEqual(5, recsetData.Count);
                Assert.AreEqual("f1r4", recsetData[2]);
            }
            else
            {
                Assert.Fail(string.Format("The following errors occured while retrieving datalist items\r\nerrors:{0}", error));
            }
        }

        [TestMethod]
        public void DeleteRecord_When_Index_Is_Negative_Expected_No_Change_Failure()
        {
            SetupArguments(ActivityStrings.DeleteRecordsDataListWithData, ActivityStrings.DeleteRecordsDataListShape, "[[recset1(-1)]]", "[[res]]");

            IDSFDataObject result = ExecuteProcess();
            string actual = string.Empty;
            string expected = @"Failure";
            List<string> recsetData = new List<string>();
            string error = string.Empty;
            GetScalarValueFromDataList(result.DataListID, "res", out actual, out error);
            recsetData = RetrieveAllRecordSetFieldValues(result.DataListID, "recset1", "field1", out error);
            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual(expected, actual);
                Assert.AreEqual(6, recsetData.Count);
            }
            else
            {
                Assert.Fail(string.Format("The following errors occured while retrieving datalist items\r\nerrors:{0}", error));
            }
        }

        [TestMethod]
        public void DeleteRecord_When_Index_Is_Zero_Expected_No_Change_Failure()
        {
            SetupArguments(ActivityStrings.DeleteRecordsDataListWithData, ActivityStrings.DeleteRecordsDataListShape, "[[recset1(0)]]", "[[res]]");

            IDSFDataObject result = ExecuteProcess();
            string actual = string.Empty;
            string expected = @"Failure";
            List<string> recsetData = new List<string>();
            string error = string.Empty;
            GetScalarValueFromDataList(result.DataListID, "res", out actual, out error);
            recsetData = RetrieveAllRecordSetFieldValues(result.DataListID, "recset1", "field1", out error);
            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual(expected, actual);
                Assert.AreEqual(6, recsetData.Count);
            }
            else
            {
                Assert.Fail(string.Format("The following errors occured while retrieving datalist items\r\nerrors:{0}", error));
            }
        }

        #endregion Error Test Cases

        #region Get Debug Input/Output Tests

        /// <summary>
        /// Author : Massimo Guerrera Bug 8104 
        /// </summary>
        [TestMethod]
        public void DeleteRecord_Get_Debug_Input_Output_With_Recordset_Expected_Pass()
        {
            DsfDeleteRecordActivity act = new DsfDeleteRecordActivity { RecordsetName = "[[Numeric()]]", Result = "[[res]]" };

            List<DebugItem> inRes;
            List<DebugItem> outRes;

            CheckActivityDebugInputOutput(act, ActivityStrings.DebugDataListShape,
                                                                ActivityStrings.DebugDataListWithData, out inRes, out outRes);
            Assert.AreEqual(1, inRes.Count);
            Assert.AreEqual(31, inRes[0].FetchResultsList().Count);

            Assert.AreEqual(1, outRes.Count);
            Assert.AreEqual(3, outRes[0].FetchResultsList().Count);
        }

        /// <summary>
        /// Author : Massimo Guerrera Bug 9473 
        /// </summary>
        [TestMethod]
        public void DeleteRecordGetDebugInputOutputWithRecordsetWithAndIndexExpectedPass()
        {
            DsfDeleteRecordActivity act = new DsfDeleteRecordActivity { RecordsetName = "[[Numeric(1)]]", Result = "[[res]]" };

            List<DebugItem> inRes;
            List<DebugItem> outRes;

            CheckActivityDebugInputOutput(act, ActivityStrings.DeleteDebugDataListShape,
                                                                ActivityStrings.DeleteDebugDataListWithData, out inRes, out outRes);
            Assert.AreEqual(1, inRes.Count);
            Assert.AreEqual(32, inRes[0].FetchResultsList().Count);

            Assert.AreEqual("[[Numeric(1).num1]]",inRes[0].ResultsList[1].Value);
            Assert.AreEqual("=", inRes[0].ResultsList[2].Value);
            Assert.AreEqual("1", inRes[0].ResultsList[3].Value);

            Assert.AreEqual("[[Numeric(1).num2]]", inRes[0].ResultsList[4].Value);
            Assert.AreEqual("=", inRes[0].ResultsList[5].Value);
            Assert.AreEqual("2", inRes[0].ResultsList[6].Value);

            Assert.AreEqual("[[Numeric(1).num3]]", inRes[0].ResultsList[7].Value);
            Assert.AreEqual("=", inRes[0].ResultsList[8].Value);
            Assert.AreEqual("3", inRes[0].ResultsList[9].Value);

            Assert.AreEqual("[[Numeric(1).num4]]", inRes[0].ResultsList[10].Value);
            Assert.AreEqual("=", inRes[0].ResultsList[11].Value);
            Assert.AreEqual("4", inRes[0].ResultsList[12].Value);

            Assert.AreEqual("[[Numeric(1).num5]]", inRes[0].ResultsList[13].Value);
            Assert.AreEqual("=", inRes[0].ResultsList[14].Value);
            Assert.AreEqual("5", inRes[0].ResultsList[15].Value);

            Assert.AreEqual("[[Numeric(1).num6]]", inRes[0].ResultsList[16].Value);
            Assert.AreEqual("=", inRes[0].ResultsList[17].Value);
            Assert.AreEqual("6", inRes[0].ResultsList[18].Value);

            Assert.AreEqual("[[Numeric(1).num7]]", inRes[0].ResultsList[19].Value);
            Assert.AreEqual("=", inRes[0].ResultsList[20].Value);
            Assert.AreEqual("7", inRes[0].ResultsList[21].Value);

            Assert.AreEqual("[[Numeric(1).num8]]", inRes[0].ResultsList[22].Value);
            Assert.AreEqual("=", inRes[0].ResultsList[23].Value);
            Assert.AreEqual("8", inRes[0].ResultsList[24].Value);

            Assert.AreEqual("[[Numeric(1).num9]]", inRes[0].ResultsList[25].Value);
            Assert.AreEqual("=", inRes[0].ResultsList[26].Value);
            Assert.AreEqual("9", inRes[0].ResultsList[27].Value);

            Assert.AreEqual("[[Numeric(1).num10]]", inRes[0].ResultsList[28].Value);
            Assert.AreEqual("=", inRes[0].ResultsList[29].Value);
            Assert.AreEqual("10", inRes[0].ResultsList[30].Value);
            
            Assert.AreEqual(1, outRes.Count);
            Assert.AreEqual(3, outRes[0].FetchResultsList().Count);

            Assert.AreEqual("[[res]]", outRes[0].ResultsList[0].Value);
            Assert.AreEqual("=", outRes[0].ResultsList[1].Value);
            Assert.AreEqual("Success", outRes[0].ResultsList[2].Value);
        }

        #endregion

        #region Get Input/Output Tests

        [TestMethod]
        public void DeleteRecordActivity_GetInputs_Expected_One_Input()
        {
            DsfCountRecordsetActivity testAct = new DsfCountRecordsetActivity();

            IBinaryDataList inputs = testAct.GetInputs();

            Assert.IsTrue(inputs.FetchAllEntries().Count == 1);
        }

        [TestMethod]
        public void DeleteRecordActivity_GetOutputs_Expected_One_Output()
        {
            DsfCountRecordsetActivity testAct = new DsfCountRecordsetActivity();

            IBinaryDataList outputs = testAct.GetOutputs();

            Assert.IsTrue(outputs.FetchAllEntries().Count == 1);
        }

        #endregion Get Input/Output Tests

        #region Private Test Methods

        private void SetupArguments(string currentDL, string testData, string recordSetName, string resultVar)
        {
            TestStartNode = new FlowStep
            {
                Action = new DsfDeleteRecordActivity { RecordsetName = recordSetName, Result = resultVar }
            };

            CurrentDl = testData;
            TestData = currentDL;
        }

        #endregion Private Test Methods

        #region Output Result to Multiple Regions

        [TestMethod]
        public void DeleteRecordUsingIndexWhereRecordExistsAndOutputToMultipleReigonsExpectedSuccessUpsertedToAllRegions()
        {
            SetupArguments(ActivityStrings.DeleteRecordsDataListWithDataWithExtraScalar, ActivityStrings.DeleteRecordsDataListShapeWithExtraScalar, "[[recset1(2)]]", "[[res]], [[res2]]");

            IDSFDataObject result = ExecuteProcess();
            string expected = @"Success";
            string firstActual = string.Empty;
            string secondActual = string.Empty;
            List<string> recsetData = new List<string>();
            string error = string.Empty;
            GetScalarValueFromDataList(result.DataListID, "res", out firstActual, out error);
            GetScalarValueFromDataList(result.DataListID, "res2", out secondActual, out error);
            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual(expected, firstActual);
                Assert.AreEqual(expected, secondActual);
            }
            else
            {
                Assert.Fail(string.Format("The following errors occured while retrieving datalist items\r\nerrors:{0}", error));
            }
        }

        #endregion
    }
}
