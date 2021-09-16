using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using MySql.Data.MySqlClient;
using System.Reflection;

namespace WpfApp1.Controllers
{
    public class MysqlDB<T> : IDB<T> where T : DBObject
    {
        string server, db, user, password;
        MySqlConnection sqlConnection;
        private string sqltable;
        Dictionary<string, PropertyInfo> properties = new Dictionary<string, PropertyInfo>();
        public MysqlDB(string server, string db, string user, string password)
        {
            this.server = server;
            this.db = db;
            this.user = user;
            this.password = password;
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.UserID = user;
            sb.Password = password;
            sb.Server = server;
            sb.Database = db;
            sqlConnection = new MySqlConnection(sb.ToString());
            var tableName =
                typeof(T).GetCustomAttributes((typeof(TableNameAttribute)), false).FirstOrDefault();
            if (tableName != null)
            {
                sqltable = ((TableNameAttribute)tableName).Name;
            }

            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                var collatt = prop.GetCustomAttributes
                    (typeof(TableColumnAttribute), false).FirstOrDefault();
                if (collatt == null)
                    continue;
                var colName = ((TableColumnAttribute)collatt).Column;
                properties.Add(colName, prop);
            }
        }
        public void Add(T obj)
        {
            List<string> colls = new List<string>(), vals = new List<string>();
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            int colIndex = 0;
            foreach (var p in properties)
            {
                var colValue = p.Value.GetValue(obj);
                colls.Add($"`{p.Key}`");
                vals.Add($"@p{colIndex}");
                parameters.Add(new MySqlParameter($"@p{colIndex++}", colValue));
            }

            string query = $"insert into {db}.{sqltable} ({string.Join(",", colls)})" +
                    $" values ({string.Join(",", vals)});";
            ExecuteNonQuery(parameters.ToArray(), query);
        }

        private void ExecuteNonQuery(Array parameters, string query)
        {
            if (OpenConnection())
            {
                
                using (MySqlCommand mc = new MySqlCommand(query, sqlConnection))
                {
                    if (parameters != null)
                        mc.Parameters.AddRange(parameters);
                    mc.ExecuteNonQuery();
                }
                CloseConnection();
            }
        }

        private bool OpenConnection()
        {
            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
        private void CloseConnection()
        {
            sqlConnection.Close();
        }

        public void Delete(T obj)
        {
            // delete from db.table where id = obj.id
            string query = $"delete from {db}.{sqltable} where id = {obj.ID}";
            ExecuteNonQuery(null, query);
        }


        public IEnumerable<T> GetAll()
        {
            List<T> result = new List<T>();
            string query = $"select * from {db}.{sqltable}";
            if (OpenConnection())
            {
                using(MySqlCommand mc = new MySqlCommand(query, sqlConnection))
                   

                using(MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        T obj = (T)Activator.CreateInstance(typeof(T));
                        for(int i = 0; i < dr.FieldCount; i++)
                        {
                            string col = dr.GetName(i);
                            if(col == "id")
                                obj.ID = dr.GetInt32("id");
                            else
                            {
                                if(!dr.IsDBNull(i))
                                properties[col].SetValue(obj, dr.GetValue(i));

                            }
                        }
                        result.Add(obj);
                    }
                }

                CloseConnection();
            }
            return result;
        }

        public void Update(T obj)
        {
            // table.db.table set
            // col1 = @p0, col2 = @p1
            // where id = obj.id
            List<string> colls = new List<string>();
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            int colIndex = 0;
            foreach (var p in properties)
            {
                var colValue = p.Value.GetValue(obj);
                colls.Add($"`{p.Key}` = @p{colIndex}");
                parameters.Add(new MySqlParameter($"@p{colIndex++}", colValue));
            }

            string query = $"update {db}.{sqltable} set " +
                $"{string.Join(",", colls)} " + $" where id = {obj.ID}";

            ExecuteNonQuery(parameters.ToArray(), query);
            
        }
    }
}
