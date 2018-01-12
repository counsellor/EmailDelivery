using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace UnileverEmailDelivery.Models
{
    public class User
    {
        private string _login;

        public string Login
        {
            get { return _login; }
            set { _login = value.Replace("TE-EX\\", "").ToUpper(); }
        }

        public User(string login)
        {
            Login = login;
        }

        public bool isAdmin()
        {
            var admins = new List<string>();
            var dt = new DataTable();

            using (var conn = new OracleConnection(Connections.KLGPROJ))
            {
                var query = String.Format("SELECT LOGIN FROM {0}", "PROJECTS.UNILEVER_EMAIL_DELIVERY_ADMIN");

                using (var adapter = new OracleDataAdapter(query, conn))
                {
                    try
                    {
                        conn.Open();
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        var mes = ex.Message;
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                admins.Add(dt.Rows[i]["LOGIN"].ToString().ToUpper());
            }

            if (admins.Contains(Login))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}