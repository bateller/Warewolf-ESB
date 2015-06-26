
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2015 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/


//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using Dev2.Data.SystemTemplates.Models;
//using Newtonsoft.Json;

//namespace Dev2.Data.ModelGenerator
//{
//    internal class JSONModelCreator
//    {
//        private const string _modelDir = @"C:\Development\DEV2 SCRUM Project\Trunk\Dev2.Data\SystemTemplates\JSON";
//        private const string _enumDir = @"C:\Development\DEV2 SCRUM Project\Trunk\Dev2.Data\SystemTemplates";
//        private const string _enumFile = "enSystemModels.cs";

//        private const string _modelLocatorTemplateDir = @"C:\Development\DEV2 SCRUM Project\Trunk\Dev2.Data\SystemTemplates\Models\ModelLocatorTemplate.cs";
//        private const string _modelLocatorDir = @"C:\Development\DEV2 SCRUM Project\Trunk\Dev2.Data\SystemTemplates\Models\ModelLocator.cs";
//        private const string _modelInjectStr = "//DEV2.ModelMap//";

//        static void Main(string[] args)
//        {

//            IList<string> modelNames = new List<string>(10);

//            Type theType = typeof (IDev2DataModel);

//            List<Type> types = theType.Assembly.GetTypes()
//                    .Where(t => (theType.IsAssignableFrom(t))).ToList();

//            foreach (Type t in types)
//            {
//                if (!t.IsAbstract && !t.IsInterface)
//                {
//                    IDev2DataModel item = (IDev2DataModel)Activator.CreateInstance(t, true);

//                    string theModel = JsonConvert.SerializeObject(item);

//                    // Write model to Templates directory ;)

//                    string modelName = item.ModelName + ".json";
//                    if (!string.IsNullOrEmpty(item.ModelName) && !modelNames.Contains(item.ModelName))
//                    {
//                        string loc = Path.Combine(_modelDir, modelName);
//                        File.WriteAllText(loc, theModel);
//                        modelNames.Add(item.ModelName);
//                        Console.WriteLine("Generated model for [ " + t.FullName + " ] to [ " + loc + " ] as model  [ " + modelName + " ]");
//                    }
//                    else
//                    {
//                        if (!string.IsNullOrEmpty(modelName))
//                        {
//                            Console.WriteLine("Failed to generate model for [ " + t.FullName + " ] since there is already another model registerd with the name [ " + modelName + " ]");
//                        }
//                        else
//                        {
//                            Console.WriteLine("Failed to generate model for [ " + t.FullName + " ]");    
//                        }
                            
//                    }
//                }
//            }

//            // Push the contract upate ;)
//            string enLoc = Path.Combine(_enumDir, _enumFile);
//            File.WriteAllText(enLoc, GenerateTemplateContract(modelNames));

//            // Generate template locator ;)
//            GenerateModelLocator(modelNames);

//            Console.WriteLine("Done");
//            Console.ReadKey();
//        }


//        private static string GenerateTemplateContract(IEnumerable<string> modelNames )
//        {
//            StringBuilder result = new StringBuilder(10000);

//            result.Append("/* Autogenerated Code - Changes will be lost ;) */");
//            result.Append("namespace Dev2.Data.SystemTemplates{");
//            result.Append(Environment.NewLine);
//            result.Append("\tpublic enum enSystemModels{");
//            result.Append(Environment.NewLine);
//            // Inject the model enums ;)
//            foreach (string mn in modelNames)
//            {
//                result.Append("\t\t");
//                result.Append(mn);
//                result.Append(",");
//                result.Append(Environment.NewLine);
//            }
//            result.Append("\t}");
//            result.Append(Environment.NewLine);
//            result.Append("}");

//            return result.ToString();
//        }

//        private static string GenerateModelLocator(IList<string> modelNames )
//        {
//            // _modelLocatorDir, _modelInjectStr

//            StringBuilder result = new StringBuilder("/* Autogenerated Code - Changes will be lost ;) */");

//            int cnt = 0;
//            int total = modelNames.Count;
//            foreach (string mn in modelNames)
//            {
//                // {enSystemModels.Dev2Decision, typeof(Dev2Decision)}

//                result.Append("{enSystemModels.");
//                result.Append(mn);
//                result.Append(",");
//                result.Append("typeof(");
//                result.Append(mn);
//                result.Append(")}");
//                result.Append(Environment.NewLine);
//                result.Append("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
//                if ((cnt + 1) < total)
//                {
//                    result.Append(",");
//                }

//                cnt++;
//            }



//            string data = File.ReadAllText(_modelLocatorTemplateDir);
//            if (!string.IsNullOrEmpty(data))
//            {
//                data = data.Replace("/**", "");
//                data = data.Replace("**/", "");
//                data = data.Replace(_modelInjectStr, result.ToString());
//                File.WriteAllText(_modelLocatorDir, data);
//            }

            
//            return result.ToString();
//        }
//    }
//}
