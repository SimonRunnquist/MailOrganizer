using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace KevinsMailOrganizer.Connection
{
    class SP_Connection
    {
        protected string sP_username;
        protected string sP_pass;
        

        public void Login() {
            using (ClientContext context = new ClientContext("")) {
                
            }
        }
    }
}
