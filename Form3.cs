using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WindowsFormsApp1.po;
using System.Linq;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        Dictionary<string, string> zjlx = null; //证件类型
        DataTable dt = null;
        public Form3()
        {
            InitializeComponent();
            PublicCommon a = new PublicCommon();

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

            foreach (var i in qj.tcbm.Values)
            {
                comboBox2.Items.Add(i);
            }
            comboBox2.SelectedIndex = 0;

            if (tabControl1.SelectedIndex == 0)
            {
                sfsbcx();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            zjlx = qj.aac043;
            string date = dateTimePicker1.Text;
            string zjlxval = "";
            foreach (var i in zjlx)
            {
                if (i.Value == comboBox1.SelectedItem.ToString().Trim())
                {
                    zjlxval = i.Key;
                }
            }
            string cbd = (from r in qj.tcbm where comboBox2.SelectedItem.ToString() == r.Value select r.Key).ToList<string>()[0];
            
            string resultxml = qj.cscf("0211", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                + "<input>"
                + "<aab299>" + PublicCommon.aab299 + "</aab299>"
                + "<yab600>" + PublicCommon.yab600 + "</yab600>"
                + "<akb026>" + PublicCommon.akb026 + "</akb026>"
                + "<aab301>" + cbd + "</aab301>"
                + "<yab060>" + textBox5.Text.Trim() + "</yab060>"
                + "<aac002>" + textBox2.Text.Trim() + "</aac002>"
                + "<aac043>" + zjlxval + "</aac043>"
                + "<aac044>" + textBox1.Text.Trim() + "</aac044>"
                + "<ake007>" + date + "</ake007></input>");


            if (resultxml == "") { return; }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(resultxml);
            XmlNode list = doc.SelectSingleNode("//result");
            int error = int.Parse(list.SelectSingleNode("errorcode").InnerText);
            if (error < 0)
            {
                string errormsg = list.SelectSingleNode("errormsg").InnerText;
                MessageBox.Show("错误信息为：" + errormsg);
            }
            else
            {
                string transid = list.SelectSingleNode("transid").InnerText;
                xzsfsb(resultxml);
                sfsbcx();
                FrmTab frm = new FrmTab(textBox1.Text.Trim(), this, str_id);
                frm.ShowDialog();
            }
        }
        public string str_id;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Trim() == "社会保障卡")
            {
                textBox2.Enabled = false;
            }
            else
            {
                textBox2.Enabled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Trim() == "社会保障卡")
            {
                textBox2.Text = textBox1.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            comboBox1.SelectedIndex = 0;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                sfsbcx();
            }
        }

        //新增身份识别
        public void xzsfsb(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode list = doc.SelectSingleNode("//result");
            XmlNode ot = list.SelectSingleNode("output");
            Sfyz s = new Sfyz();
            var propers = s.GetType().GetProperties();
            foreach (var p in propers)
            {
                if (p.Name != "sign" && p.Name != "transid" && p.Name != "errorcode" && p.Name != "errormsg")
                {
                    string n = ot.SelectSingleNode(p.Name).InnerText;
                    if (n.Trim() == "") n = null;
                    p.SetValue(s, n, null);
                }
                else
                {
                    p.SetValue(s, list.SelectSingleNode(p.Name).InnerText, null);
                }
            }

            //status 0验证 1就诊登记 2费用明细上传 3出院登记 4费用结算 5病案上传
            DBConn db = new DBConn();
            string sql = "INSERT INTO [ydjs_zyy].[dbo].[sfsb]([sign],[transid],[errorcode],[errormsg],[aab301],[yab060],[aac002],"
                        + "[aac043],[aac044],[aac003],[aac004],[aac005],[aac006],[ykc021],[ykc300],[akc026],[akc023],[aae379],[akc252]"
                        + ",[aab001],[aab003],[aab004],[yka116],[yka119],[yka121],[yka123],[ake092],[yka437],[akc200],[ykc667],[yzz014],[ake132]"
                        + ",[ykc669],[ykc678],[ykc670],[aka130],[ykc682],[ake014],[ykc672],[ykc673],[ykc674],[status]) "
                        + "VALUES('" + s.sign + "','" + s.transid + "','" + s.errorcode + "','" + s.errormsg + "','" + s.aab301 + "','" + s.yab060 + "','" + s.aac002 + "',"
                        + "'" + s.aac043 + "','" + s.aac044 + "','" + s.aac003 + "','" + s.aac004 + "','" + s.aac005 + "','" + s.aac006 + "','" + s.ykc021 + "','" + s.ykc300 + "','" + s.akc026 + "',"
                        + "'" + s.akc023 + "','" + s.aae379 + "','" + s.akc252 + "','" + s.aab001 + "','" + s.aab003 + "','" + s.aab004 + "','" + s.yka116 + "','" + s.yka119 + "','" + s.yka121 + "','"
                        + s.yka123 + "','" + s.ake092 + "','" + s.yka437 + "','" + s.akc200 + "','" + s.ykc667 + "','" + s.yzz014 + "','" + s.ake132 + "','" + s.ykc669 + "','" + s.ykc678 + "','"
                        + s.ykc670 + "','" + s.aka130 + "','" + s.ykc682 + "','" + s.ake014 + "'," + "'" + s.ykc672 + "','" + s.ykc673 + "','" + s.ykc674 + "','0')select @@identity ";

            string issql = "select * from sfsb where aac044 = '" + s.aac044 + "' and status!='5'";
            DataTable dt = db.GetDataSet(issql).Tables[0];
            if (dt.Rows.Count < 1)
            {
                str_id = db.GetDataScalar(sql).ToString();
                if (int.Parse(str_id) > 0)//转换测试数据
                {
                    MessageBox.Show("身份验证信息已添加");
                }
            }
            else
            {
                MessageBox.Show("身份验证已存在");
            }
        }

        //身份识别列表查询
        public void sfsbcx()
        {
            string sql = "select s.id as id,s.transid as '交易流水号', s.aac003 as '姓名',s.aac044 as '证件号码',s.aac002 as '社会保障卡' ,s.aac043 as '证件类型',s.status as '状态' from sfsb s";
            DBConn conn = new DBConn();
            DataSet ds = conn.GetDataSet(sql);
            dt = ds.Tables[0];


            DataTable ztdt = dt.Clone();
            ztdt.Columns["状态"].DataType = typeof(string);
            foreach (DataRow d in dt.Rows)
            {
                DataRow rowNew = ztdt.NewRow();
                rowNew["id"] = d["id"];
                rowNew["交易流水号"] = d["交易流水号"];
                //修改记录值
                rowNew["姓名"] = d["姓名"];
                rowNew["证件号码"] = d["证件号码"];
                rowNew["社会保障卡"] = d["社会保障卡"];
                rowNew["证件类型"] = d["证件类型"];
                rowNew["状态"] = d["状态"];
                ztdt.Rows.Add(rowNew);
            }
            foreach (DataRow d in ztdt.Rows)
            {
                foreach (var q in qj.status)
                {
                    if (q.Key == d["状态"].ToString())
                    {
                        d["状态"] = q.Value;
                    }
                }
            }

            dataGridView1.DataSource = ztdt;
        }

        //选项卡切换
        public void tabqiehuan(int page)
        {
            tabControl1.SelectedIndex = page;
        }


        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            FrmTab frm = new FrmTab(dataGridView1.SelectedRows[0].Cells["证件号码"].Value.ToString(), this, dataGridView1.SelectedRows[0].Cells["id"].Value.ToString());
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataRow[] dr = dt.Select("姓名 like '%" + textBox3.Text.Trim().ToString() + "%'");
            DataTable dt1 = dt.Clone();
            foreach (var d in dr)
            {
                dt1.Rows.Add(d.ItemArray);
            }
            dataGridView1.DataSource = dt1;
        }

        private void 编码维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSetup3 fs3 = new FrmSetup3();
            fs3.ShowDialog();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }
            else
            {
                FrmTab frm = new FrmTab(dataGridView1.SelectedRows[0].Cells["证件号码"].Value.ToString(), this, dataGridView1.SelectedRows[0].Cells["id"].Value.ToString());
                frm.ShowDialog();
            }
        }

        private void 月度申报ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 职工ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 居民ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 职工ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            Form17 fm = new Form17("310");
            fm.ShowDialog();
        }

        private void 居民ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            Form17 fm = new Form17("390");
            fm.ShowDialog();
        }

        private void 提交审核支付明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
