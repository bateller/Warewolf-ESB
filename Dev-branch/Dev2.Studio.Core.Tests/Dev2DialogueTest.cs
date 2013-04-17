﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dev2.Studio.Core.Interfaces;
using Dev2.Studio.Core.ViewModels.Administration;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Dev2.Core.Tests
{
    /// <summary>
    /// Summary description for Dev2DialogueTest
    /// </summary>
    [TestClass]
    public class Dev2DialogueTest
    {

        private TestContext testContextInstance;
        private string _filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\test.png";
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            
        }

        private void createFile(string fileName)
        {
            System.Drawing.Bitmap flag = new System.Drawing.Bitmap(10, 10);
            for (int x = 0; x < flag.Height; ++x)
            {
                for (int y = 0; y < flag.Width; ++y)
                {
                    flag.SetPixel(x, y, Color.White);
                }
            }
            for (int x = 0; x < flag.Height; ++x)
            {
                flag.SetPixel(x, x, Color.Red);
            }
            FileStream fs = File.Create(fileName);
            fs.Close();
            fs.Dispose();
            flag.Save(fileName);
        }

        #endregion

        #region Positive Test Cases

        [TestMethod]
        public void Dev2DialogueSetup_CorrectParameterSet_Test()
        {
            IDev2DialogueViewModel dev2Dialogue = new Dev2DialogueViewModel();
            string newFileName = _filePath.Replace(".png", "Dev2DialogueSetup_CorrectParameterSet_Test.png");
            createFile(newFileName);
            dev2Dialogue.SetupDialogue("Test Title", "Test Description", newFileName, "SomeTitleText");
            File.Delete(newFileName);
            Assert.IsTrue(dev2Dialogue.ImageSource != null);

            // cleanup - Dispose of the dev2Dialogue
            dev2Dialogue.Dispose();
        }

        #endregion Positive Test Cases

        #region Negative Test Cases

        [TestMethod]
        public void Dev2Dialogue_NullTitle_ValidDescriptionImgSourceDecsriptionTitle_Expected_DialogueSetup_TitleSetToEmptyStringNotNull() {
            IDev2DialogueViewModel dev2Dialogue = new Dev2DialogueViewModel();
            string newFileName = _filePath.Replace(".png", "Dev2Dialogue_NullTitle_ValidDescriptionImgSourceDecsriptionTitle_Expected_DialogueSetup_TitleSetToEmptyStringNotNull.png");
            createFile(newFileName);
            dev2Dialogue.SetupDialogue(null, "Test Description", newFileName, "SomeTitleText");
            File.Delete(newFileName);
            Assert.AreEqual(string.Empty, dev2Dialogue.Title);

            // cleanup - Dispose of the dev2Dialogue
            dev2Dialogue.Dispose();

        }

        [TestMethod]
        public void Dev2Dialogue_NullDescription_ValidDescriptionImgSourceDecsriptionTitle_Expected_DialogueSetup_DescriptionSetToEmpty()
        {
            IDev2DialogueViewModel dev2Dialogue = new Dev2DialogueViewModel();
            string newFileName = _filePath.Replace(".png", "Dev2Dialogue_NullDescription_ValidDescriptionImgSourceDecsriptionTitle_Expected_DialogueSetup_DescriptionSetToEmpty.png");
            createFile(newFileName);
            dev2Dialogue.SetupDialogue("Test Title", null, newFileName, "SomeTitleText");
            File.Delete(newFileName);
            Assert.AreEqual(string.Empty, dev2Dialogue.DescriptionText);
            // cleanup - Dispose of the dev2Dialogue
            dev2Dialogue.Dispose();

        }

        [TestMethod]
        public void Dev2Dialogue_NullTitleDescription_ValidgImgSourceDecsriptionTitle_Expected_DialogueSetup_TitleandDescriptionSetToEmpty()
        {
            IDev2DialogueViewModel dev2Dialogue = new Dev2DialogueViewModel();
            string newFileName = _filePath.Replace(".png", "Dev2Dialogue_NullTitleDescription_ValidgImgSourceDecsriptionTitle_Expected_DialogueSetup_TitleandDescriptionSetToEmpty.png");
            createFile(newFileName);
            dev2Dialogue.SetupDialogue(null, null, newFileName, "SomeTitleText");
            File.Delete(newFileName);
            Assert.IsTrue(dev2Dialogue.Title == string.Empty && dev2Dialogue.DescriptionText == string.Empty);

            // cleanup - Dispose of the dev2Dialogue
            dev2Dialogue.Dispose();
        }

        [TestMethod]
        public void Dev2Dialogue_NullImageSource_ValidTitleDescriptionDescriptionTitle_Expected_NullImageReference()
        {
            IDev2DialogueViewModel dev2Dialogue = new Dev2DialogueViewModel();
            dev2Dialogue.SetupDialogue("Test Title", "Test Description", null, "SomeTitleText");
            Assert.IsTrue(dev2Dialogue.ImageSource == null);
            // cleanup - Dispose of the dev2Dialogue
            dev2Dialogue.Dispose();
        }

        [TestMethod]
        public void Dev2Dialogue_NullTitleDesciptionImageSource_ValidDescriptionTitle_Expected_CreatedWithOnlyDescriptionTitleText()
        {
            IDev2DialogueViewModel dev2Dialogue = new Dev2DialogueViewModel();
            dev2Dialogue.SetupDialogue(null, null, null, "SomeTitleText");
            Assert.IsFalse(string.IsNullOrEmpty(dev2Dialogue.DescriptionTitleText));
            // cleanup - Dispose of the dev2Dialogue
            dev2Dialogue.Dispose();
        }


        [TestMethod]
        public void Dev2Dialogue_NullDesciptionTitleText_ValidTitleDescriptionImageSource_Expected_NullImageReference()
        {
            IDev2DialogueViewModel dev2Dialogue = new Dev2DialogueViewModel();
            dev2Dialogue.SetupDialogue(null, null, null, "SomeTitleText");
            Assert.IsFalse(string.IsNullOrEmpty(dev2Dialogue.DescriptionTitleText));

            // cleanup - Dispose of the dev2Dialogue
            dev2Dialogue.Dispose();
        }

        [TestMethod]
        public void Dev2Dialogue_NullTitleDescriptionTitleText_ValidImageSourceDescription_Expected_EmptyTitleAndDescription()
        {
            IDev2DialogueViewModel dev2Dialogue = new Dev2DialogueViewModel();
            string newFileName = _filePath.Replace(".png", "Dev2Dialogue_NullTitleDescriptionTitleText_ValidImageSourceDescription_Expected_EmptyTitleAndDescription.png");
            createFile(newFileName);
            dev2Dialogue.SetupDialogue(null, "Test Description", newFileName, null);
            File.Delete(newFileName);
            Assert.IsTrue(dev2Dialogue.Title == string.Empty && dev2Dialogue.DescriptionTitleText == string.Empty);

            // cleanup - Dispose of the dev2Dialogue
            dev2Dialogue.Dispose();
        }

        [TestMethod]
        public void Dev2Dialogue_NullDescriptionDescriptionTitleText_ValidTitleImageSource_Expected_EmptyDescriptionTitleAndDescription()
        {
            IDev2DialogueViewModel dev2Dialogue = new Dev2DialogueViewModel();
            string newFileName = _filePath.Replace(".png", "Dev2Dialogue_NullDescriptionDescriptionTitleText_ValidTitleImageSource_Expected_EmptyDescriptionTitleAndDescription.png");
            createFile(newFileName);
            dev2Dialogue.SetupDialogue("Test Title", null, newFileName, null);
            File.Delete(newFileName);
            Assert.IsTrue(dev2Dialogue.DescriptionText == string.Empty && dev2Dialogue.DescriptionTitleText == string.Empty);

            // cleanup - Dispose of the dev2Dialogue
            dev2Dialogue.Dispose();
        }

        [TestMethod]
        public void Dev2Dialogue_NullTitleDescriptionDescriptionTitleText_ValidImageSource_Expected_ValidImage()
        {
            IDev2DialogueViewModel dev2Dialogue = new Dev2DialogueViewModel();
            string newFileName = _filePath.Replace(".png", "Dev2Dialogue_NullTitleDescriptionDescriptionTitleText_ValidImageSource_Expected_ValidImage.png");
            createFile(newFileName);
            dev2Dialogue.SetupDialogue("Test Title", null, newFileName, null);
            File.Delete(newFileName);
            Assert.IsNotNull(dev2Dialogue.ImageSource);

            // cleanup - Dispose of the dev2Dialogue
            dev2Dialogue.Dispose();
        }

        #endregion Negative Test Cases
    }
}
