﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.18444
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Warewolf.AcceptanceTesting.Variables
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class VariableListFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "VariableList.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "VariableList", "In order to manage my variables\r\nAs a Warewolf user\r\nI want to be told shown all " +
                    "variables in my workflow service", ProgrammingLanguage.CSharp, new string[] {
                        "VariableList"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((TechTalk.SpecFlow.FeatureContext.Current != null) 
                        && (TechTalk.SpecFlow.FeatureContext.Current.FeatureInfo.Title != "VariableList")))
            {
                Warewolf.AcceptanceTesting.Variables.VariableListFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Variables adding in variable list")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "VariableList")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("VariableList")]
        public virtual void VariablesAddingInVariableList()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Variables adding in variable list", ((string[])(null)));
#line 32
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Note",
                        "Input",
                        "Output",
                        "Not Used"});
            table1.AddRow(new string[] {
                        "[[rec().a]]",
                        "This is recordset",
                        "",
                        "",
                        ""});
            table1.AddRow(new string[] {
                        "[[rec().a]]",
                        "This is recordset",
                        "",
                        "",
                        ""});
            table1.AddRow(new string[] {
                        "[[mr().a]]",
                        "",
                        "",
                        "",
                        ""});
            table1.AddRow(new string[] {
                        "[[mr().a]]",
                        "",
                        "",
                        "",
                        ""});
            table1.AddRow(new string[] {
                        "[[Var]]",
                        "",
                        "",
                        "",
                        ""});
            table1.AddRow(new string[] {
                        "[[a]]",
                        "",
                        "",
                        "",
                        "Yes"});
            table1.AddRow(new string[] {
                        "[[lr().a]]",
                        "",
                        "",
                        "",
                        "Yes"});
#line 33
 testRunner.Given("I have variables as", ((string)(null)), table1, "Given ");
#line 42
 testRunner.Then("\"Variables\" is \"Enabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 43
 testRunner.And("variables filter box is \"Visible\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.And("\"Filter Clear\" is \"Disabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.And("\"Delete Variables\" is \"Disabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
 testRunner.And("\"Sort Variables\" is \"Enabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Delete Visible",
                        "Note Visible",
                        "Note Highlighted",
                        "Input",
                        "Output"});
            table2.AddRow(new string[] {
                        "Var",
                        "NO",
                        "Yes",
                        "No",
                        "",
                        ""});
            table2.AddRow(new string[] {
                        "",
                        "NO",
                        "NO",
                        "NO",
                        "",
                        ""});
#line 47
 testRunner.And("the Variable Names are", ((string)(null)), table2, "And ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Recordset Name",
                        "Delete Visible",
                        "Note Visible",
                        "Note Highlighted",
                        "Input",
                        "Output"});
            table3.AddRow(new string[] {
                        "rec()",
                        "NO",
                        "Yes",
                        "Yes",
                        "",
                        ""});
            table3.AddRow(new string[] {
                        "rec().a",
                        "NO",
                        "Yes",
                        "Yes",
                        "",
                        ""});
            table3.AddRow(new string[] {
                        "mr()",
                        "NO",
                        "Yes",
                        "",
                        "",
                        ""});
            table3.AddRow(new string[] {
                        "mr().a",
                        "NO",
                        "Yes",
                        "",
                        "",
                        ""});
            table3.AddRow(new string[] {
                        "",
                        "No",
                        "No",
                        "",
                        "",
                        ""});
#line 51
 testRunner.And("the Recordset Names are", ((string)(null)), table3, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Deleting Unassigned Variables on variable list")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "VariableList")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("VariableList")]
        public virtual void DeletingUnassignedVariablesOnVariableList()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Deleting Unassigned Variables on variable list", ((string[])(null)));
#line 59
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Note",
                        "Inputs",
                        "Outputs",
                        "Not Used"});
            table4.AddRow(new string[] {
                        "[[rec().a]]",
                        "This is recordset",
                        "",
                        "",
                        ""});
            table4.AddRow(new string[] {
                        "[[rec().a]]",
                        "This is recordset",
                        "",
                        "",
                        ""});
            table4.AddRow(new string[] {
                        "[[mr().a]]",
                        "",
                        "",
                        "",
                        ""});
            table4.AddRow(new string[] {
                        "[[mr().a]]",
                        "",
                        "",
                        "",
                        ""});
            table4.AddRow(new string[] {
                        "[[Var]]",
                        "",
                        "",
                        "",
                        ""});
            table4.AddRow(new string[] {
                        "[[a]]",
                        "",
                        "",
                        "",
                        "Yes"});
            table4.AddRow(new string[] {
                        "[[lr().a]]",
                        "",
                        "",
                        "",
                        "Yes"});
#line 60
 testRunner.Given("I have variables as", ((string)(null)), table4, "Given ");
#line 69
 testRunner.Then("\"Variables\" is \"Enabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 70
 testRunner.And("variables filter box is \"Visible\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 71
 testRunner.And("\"Filter Clear\" is \"Disabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
 testRunner.And("\"Delete Variables\" is \"Enabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
 testRunner.And("\"Sort Variables\" is \"Enabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Delete Visible",
                        "Note Visible",
                        "Note Highlighted",
                        "Input",
                        "Output"});
            table5.AddRow(new string[] {
                        "Var",
                        "NO",
                        "Yes",
                        "No",
                        "",
                        ""});
            table5.AddRow(new string[] {
                        "[[a]]",
                        "Yes",
                        "NO",
                        "NO",
                        "",
                        ""});
            table5.AddRow(new string[] {
                        "",
                        "NO",
                        "NO",
                        "NO",
                        "",
                        ""});
#line 74
 testRunner.And("the Variable Names are", ((string)(null)), table5, "And ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Recordset Name",
                        "Delete Visible",
                        "Note Visible",
                        "Note Highlighted",
                        "Input",
                        "Output"});
            table6.AddRow(new string[] {
                        "rec()",
                        "NO",
                        "Yes",
                        "Yes",
                        "",
                        ""});
            table6.AddRow(new string[] {
                        "rec().a",
                        "NO",
                        "Yes",
                        "Yes",
                        "",
                        ""});
            table6.AddRow(new string[] {
                        "mr()",
                        "NO",
                        "Yes",
                        "No",
                        "",
                        ""});
            table6.AddRow(new string[] {
                        "mr().a",
                        "NO",
                        "Yes",
                        "No",
                        "",
                        ""});
            table6.AddRow(new string[] {
                        "[[lr().a]]",
                        "Yes",
                        "NO",
                        "No",
                        "",
                        ""});
            table6.AddRow(new string[] {
                        "",
                        "NO",
                        "NO",
                        "NO",
                        "",
                        ""});
#line 79
 testRunner.And("the Recordset Names are", ((string)(null)), table6, "And ");
#line 87
 testRunner.When("I delete unassigned variables", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Delete Visible",
                        "Note Visible",
                        "Note Highlighted",
                        "Input",
                        "Output"});
            table7.AddRow(new string[] {
                        "Var",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table7.AddRow(new string[] {
                        "",
                        "NO",
                        "NO",
                        "NO",
                        "",
                        ""});
#line 88
 testRunner.Then("the Variable Names are", ((string)(null)), table7, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Recordset Name",
                        "Delete Visible",
                        "Note Visible",
                        "Note Highlighted",
                        "Input",
                        "Output"});
            table8.AddRow(new string[] {
                        "rec()",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table8.AddRow(new string[] {
                        "rec().a",
                        "Yes",
                        "Yes",
                        "Yes",
                        "",
                        ""});
            table8.AddRow(new string[] {
                        "mr()",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table8.AddRow(new string[] {
                        "mr().a",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table8.AddRow(new string[] {
                        "",
                        "NO",
                        "NO",
                        "NO",
                        "",
                        ""});
#line 92
 testRunner.And("the Recordset Names are", ((string)(null)), table8, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Searching Variables in Variable list")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "VariableList")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("VariableList")]
        public virtual void SearchingVariablesInVariableList()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Searching Variables in Variable list", ((string[])(null)));
#line 100
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Note",
                        "Inputs",
                        "Outputs",
                        "Not Used"});
            table9.AddRow(new string[] {
                        "[[rec().a]]",
                        "This is recordset",
                        "",
                        "",
                        ""});
            table9.AddRow(new string[] {
                        "[[rec().a]]",
                        "This is recordset",
                        "",
                        "",
                        ""});
            table9.AddRow(new string[] {
                        "[[mr().a]]",
                        "",
                        "",
                        "",
                        ""});
            table9.AddRow(new string[] {
                        "[[mr().a]]",
                        "",
                        "",
                        "",
                        ""});
            table9.AddRow(new string[] {
                        "[[Var]]",
                        "",
                        "",
                        "",
                        ""});
            table9.AddRow(new string[] {
                        "[[a]]",
                        "",
                        "",
                        "",
                        "Yes"});
            table9.AddRow(new string[] {
                        "[[lr().a]]",
                        "",
                        "",
                        "",
                        "Yes"});
#line 101
 testRunner.Given("I have variables as", ((string)(null)), table9, "Given ");
#line 110
 testRunner.Then("\"Variables\" is \"Enabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 111
 testRunner.And("variables filter box is \"Visible\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 112
 testRunner.And("\"Filter Clear\" is \"Disabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 113
 testRunner.And("\"Delete Variables\" is \"Disabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 114
 testRunner.And("\"Sort Variables\" is \"Enabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 115
 testRunner.When("I search for variable \"[[mr().a]]\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Delete Visible",
                        "Note Visible",
                        "Note Highlighted",
                        "Input",
                        "Output"});
#line 116
 testRunner.Then("the Variable Names are", ((string)(null)), table10, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Recordset Name",
                        "Delete Visible",
                        "Note Visible",
                        "Note Highlighted",
                        "Input",
                        "Output"});
            table11.AddRow(new string[] {
                        "mr()",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table11.AddRow(new string[] {
                        "mr().a",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
#line 118
 testRunner.And("the Recordset Names are", ((string)(null)), table11, "And ");
#line 122
 testRunner.When("I clear the filter", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Delete Visible",
                        "Note Visible",
                        "Note Highlighted",
                        "Input",
                        "Output"});
            table12.AddRow(new string[] {
                        "Var",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table12.AddRow(new string[] {
                        "[[a]]",
                        "NO",
                        "NO",
                        "NO",
                        "Not Visible",
                        "Not Visible"});
            table12.AddRow(new string[] {
                        "",
                        "NO",
                        "NO",
                        "NO",
                        "",
                        ""});
#line 123
 testRunner.Then("the Variable Names are", ((string)(null)), table12, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "Recordset Name",
                        "Delete Visible",
                        "Note Visible",
                        "Note Highlighted",
                        "Input",
                        "Output"});
            table13.AddRow(new string[] {
                        "rec()",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table13.AddRow(new string[] {
                        "rec().a",
                        "Yes",
                        "Yes",
                        "Yes",
                        "",
                        ""});
            table13.AddRow(new string[] {
                        "mr()",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table13.AddRow(new string[] {
                        "mr().a",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table13.AddRow(new string[] {
                        "[[lr().a]]",
                        "Yes",
                        "No",
                        "No",
                        "Not Visible",
                        "Not Visible"});
            table13.AddRow(new string[] {
                        "",
                        "NO",
                        "NO",
                        "NO",
                        "",
                        ""});
#line 128
 testRunner.And("the Recordset Names are", ((string)(null)), table13, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Sorting Variables in Variable list")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "VariableList")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("VariableList")]
        public virtual void SortingVariablesInVariableList()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Sorting Variables in Variable list", ((string[])(null)));
#line 139
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Note",
                        "Inputs",
                        "Outputs",
                        "Not Used"});
            table14.AddRow(new string[] {
                        "[[rec().a]]",
                        "This is recordset",
                        "",
                        "",
                        ""});
            table14.AddRow(new string[] {
                        "[[rec().a]]",
                        "This is recordset",
                        "",
                        "",
                        ""});
            table14.AddRow(new string[] {
                        "[[mr().a]]",
                        "",
                        "",
                        "",
                        ""});
            table14.AddRow(new string[] {
                        "[[mr().a]]",
                        "",
                        "",
                        "",
                        ""});
            table14.AddRow(new string[] {
                        "[[Var]]",
                        "",
                        "",
                        "",
                        ""});
            table14.AddRow(new string[] {
                        "[[a]]",
                        "",
                        "",
                        "",
                        "Yes"});
            table14.AddRow(new string[] {
                        "[[lr().a]]",
                        "",
                        "",
                        "",
                        "Yes"});
#line 140
 testRunner.Given("I have variables as", ((string)(null)), table14, "Given ");
#line 149
 testRunner.Then("\"Variables\" is \"Enabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 150
 testRunner.And("variables filter box is \"Visible\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 151
 testRunner.And("\"Filter Clear\" is \"Disabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 152
 testRunner.And("\"Delete Variables\" is \"Disabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 153
 testRunner.And("\"Sort Variables\" is \"Enabled\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 154
 testRunner.When("I Sort the variables", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Delete Visible",
                        "Note Visible",
                        "Note Highlighted",
                        "Input",
                        "Output"});
            table15.AddRow(new string[] {
                        "[[a]]",
                        "NO",
                        "NO",
                        "NO",
                        "Not Visible",
                        "Not Visible"});
            table15.AddRow(new string[] {
                        "Var",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table15.AddRow(new string[] {
                        "",
                        "NO",
                        "NO",
                        "NO",
                        "",
                        ""});
#line 155
 testRunner.Then("the Variable Names are", ((string)(null)), table15, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                        "Recordset Name",
                        "Delete Visible",
                        "Note Visible",
                        "Note Highlighted",
                        "Input",
                        "Output"});
            table16.AddRow(new string[] {
                        "[[lr().a]]",
                        "Yes",
                        "No",
                        "No",
                        "Not Visible",
                        "Not Visible"});
            table16.AddRow(new string[] {
                        "mr()",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table16.AddRow(new string[] {
                        "mr().a",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table16.AddRow(new string[] {
                        "rec()",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table16.AddRow(new string[] {
                        "rec().a",
                        "Yes",
                        "Yes",
                        "Yes",
                        "",
                        ""});
            table16.AddRow(new string[] {
                        "",
                        "NO",
                        "NO",
                        "NO",
                        "",
                        ""});
#line 160
 testRunner.And("the Recordset Names are", ((string)(null)), table16, "And ");
#line 168
 testRunner.When("I Sort the variables", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Delete Visible",
                        "Note Visible",
                        "Note Highlighted",
                        "Input",
                        "Output"});
            table17.AddRow(new string[] {
                        "Var",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table17.AddRow(new string[] {
                        "[[a]]",
                        "NO",
                        "NO",
                        "NO",
                        "Not Visible",
                        "Not Visible"});
            table17.AddRow(new string[] {
                        "",
                        "NO",
                        "NO",
                        "NO",
                        "",
                        ""});
#line 169
 testRunner.Then("the Variable Names are", ((string)(null)), table17, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                        "Recordset Name",
                        "Delete Visible",
                        "Note Visible",
                        "Note Highlighted",
                        "Input",
                        "Output"});
            table18.AddRow(new string[] {
                        "rec()",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table18.AddRow(new string[] {
                        "rec().a",
                        "Yes",
                        "Yes",
                        "Yes",
                        "",
                        ""});
            table18.AddRow(new string[] {
                        "mr()",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table18.AddRow(new string[] {
                        "mr().a",
                        "Yes",
                        "Yes",
                        "No",
                        "",
                        ""});
            table18.AddRow(new string[] {
                        "[[lr().a]]",
                        "Yes",
                        "No",
                        "No",
                        "Not Visible",
                        "Not Visible"});
            table18.AddRow(new string[] {
                        "",
                        "NO",
                        "NO",
                        "NO",
                        "",
                        ""});
#line 174
 testRunner.And("the Recordset Names are", ((string)(null)), table18, "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
