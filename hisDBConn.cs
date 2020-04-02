using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;


namespace WindowsFormsApp1
{
    class hisDBConn
    {
        private string StrConn;
        public SqlConnection SqlDrConn = null;
        public SqlConnection SqlDaConn = null;
        public SqlConnection SqlCmdConn = null;
        public static string ServerName = "";
        public static string SerUser = "";
        public static string SerPassword = "";
        public static string Database = "";
        public static string Print = "";
        public static bool SerLogin = true;  //登录模式
        private XmlDocument XmlDoc = new XmlDocument();
        private string FilePath = @".\hisDataBase.Xml";
        public hisDBConn()
        {

            ReadXml();
        }

        public SqlDataReader GetDataReader(string StrSql)
        {
            try
            {
                SqlDrConn = new SqlConnection(StrConn);
                SqlDrConn.Open();
                SqlCommand SqlCmd = new SqlCommand(StrSql, SqlDrConn);
                SqlDataReader SqlDr = SqlCmd.ExecuteReader();
                return SqlDr;
            }
            catch (Exception ex)
            {

                SqlDrConn.Close();
                return null;
            }
            finally
            {

            }
        }

        public DataSet GetDataSet(string StrSql)
        {
            if (SqlDaConn != null && SqlDaConn.State == ConnectionState.Open)
            {
                SqlDaConn.Close();
            }
            else
            {
                SqlDaConn = new SqlConnection(StrConn);
            }
            try
            {
                SqlDaConn.Open();
                SqlDataAdapter SqlDa = new SqlDataAdapter(StrSql, SqlDaConn);
                DataSet ds = new DataSet();
                SqlDa.Fill(ds, "table1");
                return ds;
            }
            catch (Exception Msg)
            {
                SqlDaConn.Close();
                return null;
            }
            finally
            {
                SqlDaConn.Close();
            }
        }

        public bool GetTransaction(System.Collections.ArrayList StrSqlList)
        {
            SqlConnection SqlTrConn = new SqlConnection(StrConn);
            SqlTransaction SqlTr = null;
            //----------------------------------------------------------
            SqlTrConn.Open();
            SqlTr = SqlTrConn.BeginTransaction();
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = SqlTrConn;
            SqlCmd.Transaction = SqlTr;
            try
            {
                for (int i = 0; i < StrSqlList.Count; i++)
                {
                    SqlCmd.CommandText = StrSqlList[i].ToString();
                    SqlCmd.ExecuteNonQuery();
                }
                SqlTr.Commit();
            }
            catch (Exception ex)
            {
                SqlTr.Rollback();
                SqlTrConn.Close();
                return false;
            }
            SqlTrConn.Close();
            return true;
        }

        public int GetSqlCmd(string StrSql)
        {
            int k = 0;
            try
            {

                if (SqlCmdConn != null && SqlCmdConn.State == ConnectionState.Open)
                {
                    SqlCmdConn.Close();
                }
                else
                {
                    SqlCmdConn = new SqlConnection(StrConn);
                }
                SqlCmdConn = new SqlConnection(StrConn);
                SqlCommand SqlCmd = new SqlCommand(StrSql, SqlCmdConn);
                SqlCmdConn.Open();
                k = SqlCmd.ExecuteNonQuery();
                SqlCmdConn.Close();
                return k;
            }
            catch (Exception Msg)
            {
                System.Windows.Forms.MessageBox.Show(Msg.ToString());
                return k;
            }
            finally
            {

            }
        }

        private void ReadXml()
        {
            //读取Xml文档
            string StrNode = "";
            XmlNodeReader reader = null;
            try
            {
                // 装入指定的XML文档
                XmlDoc.Load(FilePath);
                // 设定XmlNodeReader对象来打开XML文件
                reader = new XmlNodeReader(XmlDoc);
                // 读取XML文件中的数据，并显示出来
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            StrNode = reader.Name;
                            break;
                        case XmlNodeType.Text:
                            if (StrNode.Equals("ServerName"))
                            {
                                ServerName = reader.Value;
                            }
                            else if (StrNode.Equals("SerUser"))
                            {
                                SerUser = reader.Value;
                            }
                            else if (StrNode.Equals("SerPassword"))
                            {
                                SerPassword = reader.Value;

                            }
                            else if (StrNode.Equals("SerLogin"))
                            {
                                SerLogin = Convert.ToBoolean(reader.Value);
                            }
                            else if (StrNode.Equals("Database"))
                            { Database = reader.Value; }

                            break;
                    }
                }
                if (!ServerName.Equals("") && !SerLogin.Equals(""))
                {
                    StrConn = ToStrConn(ServerName, SerLogin, SerUser, SerPassword, Database);
                }
            }
            catch (Exception ex)
            {
                // System.Windows.Forms.Application.Exit();
            }
            finally
            {
                //清除打开的数据流
                if (reader != null)
                    reader.Close();
            }
        }

        public static string ToStrConn(string ServerName, bool SerLogin, string SerUser, string SerPassword, string dataBase)
        {
            if (ServerName.Equals(".") || ServerName.Equals(""))
            {
                ServerName = "(local)";
            }
            if (SerLogin == true)
            {
                return "server=" + ServerName + ";integrated security=sspi;database=" + dataBase;
            }
            else
            {
                return "server=" + ServerName + ";database=" + dataBase + ";uid=" + SerUser + ";pwd=" + SerPassword;
            }
        }
        private string Encodebase64(string code)
        {

            string encode = "";
            byte[] bytes = Encoding.Default.GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }

        public void close()
        {
            SqlDaConn.Close();
        }

        private string Decodebase64(string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.Default.GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }

    }
}
