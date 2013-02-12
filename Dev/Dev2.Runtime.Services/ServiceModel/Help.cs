﻿using System;
using Dev2.Runtime.Diagnostics;
using Dev2.Runtime.ServiceModel.Data;

namespace Dev2.Runtime.ServiceModel
{
    public class Help : ExceptionManager
    {
        #region GetDictionary

        // POST: Service/Help/GetDictionary
        public HelpDictionary GetDictionary(string dictionaryID, Guid workspaceID, Guid dataListID)
        {
            var result = new HelpDictionary();

            try
            {
                // TODO: Read from file based on dictionaryID
                switch(dictionaryID)
                {
                    case "Server":
                        result.Add("default", "<h4>New Server Details</h4><p>This creates a connection to an existing server.</p>");
                        result.Add("address", "Enter the <b>server url</b> e.g. http://192.168.0.1:77/dsf.");
                        result.Add("userName", "Enter your <b>user name</b>.");
                        result.Add("password", "Enter the <b>password</b> for the server.");
                        break;
                }
            }
            catch(Exception ex)
            {
                RaiseError(ex);
            }
            return result;
        }

        #endregion

    }
}
