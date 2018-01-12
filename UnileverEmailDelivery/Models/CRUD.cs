using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace UnileverEmailDelivery.Models
{
    public class CRUD
    {
        private string _tableName;

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public CRUD()
        {
            TableName = "PROJECTS.UNILEVER_EMAIL_DELIVERY";
        }

        public CRUD(string _tableName)
        {
            TableName = _tableName;
        }

        public void Create(Delivery delivery)
        {
            using (var conn = new OracleConnection(Connections.KLGPROJ))
            {
                using (var cmd = new OracleCommand())
                {
                    cmd.Parameters.Add("BRAND", delivery.Brand);
                    cmd.Parameters.Add("CAUSE", delivery.Cause);
                    cmd.Parameters.Add("EMAIL", delivery.Email);
                    cmd.Parameters.Add("CONCRETIZATION", delivery.Concretization);

                    var query = String.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                        TableName,
                        String.Join(", ", delivery.ColModel.Where(x => x != "ID").ToArray()),
                        String.Join(", ", delivery.ColModel.Where(x => x != "ID").Select(x => ":" + x).ToArray()));

                    QueryExecute(conn, cmd, query);
                }
            }       
        }

        internal List<Delivery> ReadAll()
        {
            var dt = new DataTable();

            using (var conn = new OracleConnection(Connections.KLGPROJ))
            {
                var query = String.Format(@"SELECT * FROM {0}", TableName);

                using (var adapter = new OracleDataAdapter(query, conn))
                {
                    try
                    {
                        conn.Open();
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        var message = ex.Message;
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            var result = new List<Delivery>();

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var temp = new Delivery
                {
                    Id = Convert.ToInt32(dt.Rows[i]["ID"]),
                    Brand = dt.Rows[i]["BRAND"].ToString(),
                    Cause = dt.Rows[i]["CAUSE"].ToString(),
                    Email = dt.Rows[i]["EMAIL"].ToString()
                };

                var parseResult = 0;
                if (Int32.TryParse(dt.Rows[i]["CONCRETIZATION"].ToString(), out parseResult))
                {
                    temp.Concretization = parseResult;
                }
                else
                {
                    temp.Concretization = null;
                }

                result.Add(temp);
            }

            return result;
        }

        internal void Update(Delivery delivery)
        {
            using (var conn = new OracleConnection(Connections.KLGPROJ))
            {
                using (var cmd = new OracleCommand())
                {
                    cmd.Parameters.Add("BRAND", delivery.Brand);
                    cmd.Parameters.Add("CAUSE", delivery.Cause);
                    cmd.Parameters.Add("EMAIL", delivery.Email);
                    cmd.Parameters.Add("CONCRETIZATION", delivery.Concretization);

                    var sqlQuery = String.Format("UPDATE {0} SET {1} WHERE ID = {2}",
                        TableName,
                        String.Join(", ", delivery.ColModel.Where(x => x != "ID").Select(x => x + " = :" + x)),
                        delivery.Id);

                    QueryExecute(conn, cmd, sqlQuery);
                }
            }
        }

        internal void Delete(int id)
        {
            using (var conn = new OracleConnection(Connections.KLGPROJ))
            {
                using (var cmd = new OracleCommand())
                {
                    cmd.Parameters.Add("ID", id);
                    var query = String.Format(@"DELETE FROM {0} WHERE ID = :ID", TableName);
                    
                    QueryExecute(conn, cmd, query);
                }
            }
        }

        private void QueryExecute(OracleConnection conn, OracleCommand cmd, string query)
        {
            cmd.Connection = conn;
            cmd.CommandTimeout = 1800;
            cmd.BindByName = true;

            try
            {
                conn.Open();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}