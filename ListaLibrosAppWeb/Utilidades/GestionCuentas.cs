using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Web;
using Microsoft.SharePoint.Client;

namespace ListaLibrosAppWeb.Utilidades
{
    public class GestionCuentas
    {
        private static SecureString pwd;
        public static SharePointOnlineCredentials GetCredentials()
        {
            if (pwd == null)
            {
                pwd = GetSecureString(ConfigurationManager.AppSettings["SPPassword"]);
            }

            return new SharePointOnlineCredentials(ConfigurationManager.AppSettings["SpUser"], pwd);
        }

        private static SecureString GetSecureString(String tx)
        {
            var st = new SecureString();
            foreach (var c in tx.ToCharArray())
            {
                st.AppendChar(c);
            }

            return st;
        }
    }
}