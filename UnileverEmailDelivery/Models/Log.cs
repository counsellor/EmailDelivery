using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnileverEmailDelivery.Models
{
    public class Log
    {
        private List<string> _colModel;

        public List<string> ColModel
        {
            get { return _colModel; }
            set { _colModel = value; }
        }
        private string _tableName;

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        private string _historyTableName;

        public string HistoryTableName
        {
            get { return _historyTableName; }
            set { _historyTableName = value; }
        }

        public Log()
        {
            TableName = "PROJECTS.UNILEVER_EMAIL_DELIVERY";
            HistoryTableName = "PROJECTS.UNILEVER_EMAIL_DELIVERY_STORY";

            ColModel = new List<string>
            {
                "ID",
                "BRAND",
                "CAUSE",
                "EMAIL",
                "CONCRETIZATION",

                "OPERATION",
                "EDIT_DATE",
                "LOGIN"
            };
        }

        //delivery id = -1 (create new record flag)
        internal void Insert(string operType, string login, Delivery delivery)
        {
            using(var conn = new OracleConnection(Connections.KLGPROJ))
            {
                using (var cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 1800;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("BRAND", delivery.Brand);
                    cmd.Parameters.Add("CAUSE", delivery.Cause);
                    cmd.Parameters.Add("EMAIL", delivery.Email);
                    cmd.Parameters.Add("CONCRETIZATION", delivery.Concretization);
                    cmd.Parameters.Add("OPERATION", operType);
                    cmd.Parameters.Add("EDIT_DATE", DateTime.Now);
                    cmd.Parameters.Add("LOGIN", login);

                    var sqlQuery = String.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                        HistoryTableName,
                        String.Join(", ", ColModel.Where(x => x != "ID").ToArray()),
                        String.Join(", ", ColModel.Where(x => x != "ID").Select(x => ":" + x).ToArray())
                        );

                    try
                    {
                        conn.Open();
                        cmd.CommandText = sqlQuery;
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

        //for edit or delete operations
        internal void Insert(string operType, string login, int id)
        {
            using (var conn = new OracleConnection(Connections.KLGPROJ))
            {
                using (var cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 1800;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("ID", id);
                    cmd.Parameters.Add("OPERATION", operType);
                    cmd.Parameters.Add("EDIT_DATE", DateTime.Now);
                    cmd.Parameters.Add("LOGIN", login);

                    var sqlQuery = String.Format("INSERT INTO {0} ({1}) SELECT {2} FROM {3} WHERE ID = :ID",
                        HistoryTableName,
                        String.Join(", ", ColModel.Where(x => x != "ID").ToArray()),
                        String.Join(", ", ColModel.Take(5).Where(x => x != "ID").ToArray()) + ", :OPERATION, :EDIT_DATE, :LOGIN",
                        TableName
                        );

                    try
                    {
                        conn.Open();
                        cmd.CommandText = sqlQuery;
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
    }
}