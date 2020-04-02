using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> ls = new Dictionary<string, string>();

            string commandString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=C:\\Users\\Administrator\\Desktop\\新建文件夹(1)\\201902更新三目\\广东1.17.09中成药全库.xls;" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
            string commandString1 = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=C:\\Users\\Administrator\\Desktop\\新建文件夹(1)\\201902更新三目\\广东1.17.10西药全库.xls;" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";

            DataSet dataSet = new DataSet();
            //  创建连接到数据源的对象
            OleDbConnection command = new OleDbConnection(commandString);
            OleDbConnection command1 = new OleDbConnection(commandString1);
            //  打开连接
            command.Open();
            //  Sql的查询命令，有关于数据库自行百度或者Google
            string sqlex = "select * from [中成药注册信息库$]";
            string sqlex1 = "select * from [西药注册信息库$]";
            OleDbDataAdapter adaper = new OleDbDataAdapter(sqlex, command);
            OleDbDataAdapter adaper1 = new OleDbDataAdapter(sqlex1, command1);
            //  用来存放数据
            dataSet = new DataSet();
            adaper.Fill(dataSet);
            //  填充DataTable数据到DataSet中
            DataTable dt = dataSet.Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                ls.Add(r["药监局药品编码"].ToString(), r["中成药药品代码"].ToString());
            }

            dataSet = new DataSet();
            adaper1.Fill(dataSet);
            dt = dataSet.Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                ls.Add(r["药监局药品编码"].ToString(), r["西药药品代码"].ToString());
            }

            hisDBConn hdb = new hisDBConn();
            //foreach (var l in ls)
            //{
            //    string sql = "update yk_typk2 set YBDM = '" + l.Value + "' where sbdm = '" + l.Key + "'";
            //    hdb.GetSqlCmd(sql);
            //}

            Dictionary<string, string> nls = new Dictionary<string, string>();
            string sqln = "select * from [YK_TYPK2] where ybdm is null";
            DataTable dtn = hdb.GetDataSet(sqln).Tables[0];
            foreach (DataRow n in dtn.Rows)
            {
                string ypxh = n["ypxh"].ToString();
                string sbdm = n["sbdm"].ToString();

                string sql = "update yk_typk2 set YBDM = '" + sbdm + "' where ypxh = '" + ypxh + "'";
                hdb.GetSqlCmd(sql);
            }

            //  释放连接的资源
            command.Close();
        }
    }
}
