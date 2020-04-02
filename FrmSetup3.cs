using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace WindowsFormsApp1
{
    public partial class FrmSetup3 : Form
    {
        String Strsql = "";
        hisDBConn dbConn1 = null;
        DataSet dataSet = null;
        SqlDataReader sqlDataReader = null;
        public static String Str_ypxh = "";
        public static String Str_ypcd = "";
        ArrayList arrayList = null;
        static int rowIndex = 0;
        public DataTable dataTable;
        public DataTable ypdt;
        public DataTable sfdt;

        public FrmSetup3()
        {
            InitializeComponent();

            dbConn1 = new hisDBConn();
        }

        private void FrmSetup3_Load(object sender, EventArgs e)
        {
            Strsql = "select ypxh,ypmc,sbdm,ybdm from [YK_TYPK] ";//药品
            loadGv(Gv1, Strsql);
            ypdt = dataTable.Copy();

            Strsql = "select fyxh,fymc,sbdm,ybdm from [gy_ylsf] ";//收費
            loadGv(Gv2, Strsql);
            sfdt = dataTable.Copy();
        }

        private void loadGv(DataGridView gv, string sql)
        {

            dataSet = new DataSet();
            dataSet = dbConn1.GetDataSet(Strsql);
            dataTable = dataSet.Tables[0];
            gv.DataSource = dataTable;



        }



        private void Gv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable lsdt = (DataTable)Gv1.DataSource;
            DataRow dr = lsdt.Rows[e.RowIndex];
            textBox1.Text = dr["ypxh"].ToString();
            textBox5.Text = dr["ypmc"].ToString();
            textBox3.Text = dr["sbdm"].ToString();
            textBox4.Text = dr["ybdm"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "update yk_typk set YBDM = '" + textBox4.Text.Trim() + "' where ypxh = '" + textBox1.Text.Trim() + "'";
            int i = dbConn1.GetSqlCmd(sql);
            if (i > 0)
            {
                MessageBox.Show("修改成功");
            }
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            DataRow[] drs = ypdt.Select("ypmc like '%" + textBox2.Text.Trim() + "%'");
            DataTable dt1 = ypdt.Clone();
            foreach (DataRow d in drs)
            {
                dt1.Rows.Add(d.ItemArray);
            }
            Gv1.DataSource = dt1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Strsql = "select ypxh,ypmc,sbdm,ybdm from [YK_TYPK] ";//药品
            loadGv(Gv1, Strsql);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataRow[] drs = sfdt.Select("fymc like '%" + textBox6.Text.Trim() + "%'");
            DataTable dt1 = sfdt.Clone();
            foreach (DataRow d in drs)
            {
                dt1.Rows.Add(d.ItemArray);
            }
            Gv2.DataSource = dt1;
        }

        private void Gv2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable lsdt = (DataTable)Gv2.DataSource;
            DataRow dr = lsdt.Rows[e.RowIndex];
            textBox7.Text = dr["fyxh"].ToString();
            textBox8.Text = dr["fymc"].ToString();
            textBox9.Text = dr["sbdm"].ToString();
            textBox10.Text = dr["ybdm"].ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Strsql = "select fyxh,fymc,sbdm,ybdm from [gy_ylsf] ";//收費
            loadGv(Gv2, Strsql);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sql = "update gy_ylsf set YBDM = '" + textBox10.Text.Trim() + "' where fyxh = '" + textBox7.Text.Trim() + "'";
            int i = dbConn1.GetSqlCmd(sql);
            if (i > 0)
            {
                MessageBox.Show("修改成功");
            }
        }

        private void Gv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }
    }
}
