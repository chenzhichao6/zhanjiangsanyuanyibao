using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBConn conn = new DBConn();
            string ssql = "select * from zidian where csdm = '" + textBox1.Text.Trim() + "'";
            string sql = "INSERT INTO [ydjs_zyy].[dbo].[zidian]([csdm],[csmc]) VALUES('" + textBox1.Text.Trim() + "','" + textBox2.Text.Trim() + "')";
            int i = conn.GetDataSet(ssql).Tables[0].Rows.Count;
            if (i < 1) { conn.GetSqlCmd(sql); }
            label3.Text = textBox1.Text;
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
