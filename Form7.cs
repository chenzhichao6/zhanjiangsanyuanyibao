using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WindowsFormsApp1.po;

namespace WindowsFormsApp1
{
    public partial class Form7 : Form
    {
        public Sfyz s = null;
        public string ykc700 = "";
        string str_id;
        public Form7()
        {
            InitializeComponent();
        }

        public Form7(Sfyz s, string ykc700, string id)
        {
            InitializeComponent();
            this.s = s;
            str_id = id;
            this.ykc700 = ykc700;
            init();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void init()
        {
            DBConn db = new DBConn();
            string sql1 = "select * from cydj where aac044 = '" + s.aac044 + "' and id = " + str_id;
            DataTable dt1 = db.GetDataSet(sql1).Tables[0];


            foreach (var y in qj.ykc195)
            {
                comykc195.Items.Add(y.Value);
            }


            var bm = new AutoCompleteStringCollection();
            bm.AddRange(qj.icd.Keys.ToArray<string>());
            txtakc196.AutoCompleteCustomSource = bm;
            txtakc196.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtakc196.AutoCompleteSource = AutoCompleteSource.CustomSource;

            txtakc189.AutoCompleteCustomSource = bm;
            txtakc189.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtakc189.AutoCompleteSource = AutoCompleteSource.CustomSource;

            txtakc188.AutoCompleteCustomSource = bm;
            txtakc188.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtakc188.AutoCompleteSource = AutoCompleteSource.CustomSource;

            var jb = new AutoCompleteStringCollection();
            jb.AddRange(qj.icd.Values.ToArray<string>());
            txtakc185.AutoCompleteCustomSource = jb;
            txtakc185.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtakc185.AutoCompleteSource = AutoCompleteSource.CustomSource;

            var ks = new AutoCompleteStringCollection();
            ks.AddRange(qj.akf002.Values.ToArray<string>());
            txtakf002.AutoCompleteCustomSource = ks;
            txtakf002.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtakf002.AutoCompleteSource = AutoCompleteSource.CustomSource;

            if (dt1.Rows.Count > 0) { button1.Text = "出院登记回退"; return; }
            txtaac002.Text = s.aac002;
            txtaac043.Text = (from a in qj.aac043 where a.Key == s.aac043 select a.Value).ToList<string>()[0];
            txtaac044.Text = s.aac044;

            comykc195.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "出院登记回退")
            {
                cydjht();
            }
            else
            {
                cydj();
            }
        }

        public void cydj()
        {
            string cyks = "";
            var lryks = (from r in qj.akf002 where r.Value == txtakf002.Text.Trim() select r.Key).ToList<string>();
            if (lryks.Count > 0) { cyks = lryks[0]; }
            string cyyy = (from r in qj.ykc195 where r.Value == comykc195.SelectedItem.ToString() select r.Key).ToList<string>()[0];

            string xml = "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                + "<input>"
                + "<aab299>" + PublicCommon.aab299 + "</aab299>"
                + "<yab600>" + PublicCommon.yab600 + "</yab600>"
                + "<akb026>" + PublicCommon.akb026 + "</akb026>"
                + "<akb021>" + PublicCommon.akb021 + "</akb021>"
                + "<ykc700>" + ykc700 + "</ykc700>"
                + "<aab301>" + s.aab301 + "</aab301>"
                + "<yab060>" + s.yab060 + "</yab060>"
                + "<aac002>" + s.aac002 + "</aac002>"
                + "<aac043>" + s.aac043 + "</aac043>"
                + "<aac044>" + s.aac044 + "</aac044>"
                + "<akf002>" + cyks + "</akf002>"
                + "<yzz088>" + txtyzz088.Text.Trim() + "</yzz088>"
                + "<yzz089>" + txtyzz089.Text.Trim() + "</yzz089>"
                + "<ykc016>" + txtykc016.Text.Trim() + "</ykc016>"
                + "<akc185>" + txtakc185.Text.Trim() + "</akc185>"
                + "<akc196>" + txtakc196.Text.Trim() + "</akc196>"
                + "<akc188>" + txtakc188.Text.Trim() + "</akc188>"
                + "<akc189>" + txtakc189.Text.Trim() + "</akc189>"
                + "<akc056>" + txtakc056.Text.Trim() + "</akc056>"
                + "<ake021>" + txtake021.Text.Trim() + "</ake021>"
                + "<ykc195>" + cyyy + "</ykc195>"
                + "<ykc683>" + txtykc683.Text.Trim() + "</ykc683>"
                + "<ykc702>" + dtpykc702.Text.Trim() + "</ykc702>"
                + "<akb063>" + txtakb063.Text.Trim() + "</akb063>"
                + "<yzz020>" + txtyzz020.Text.Trim() + "</yzz020>"
                + "<aae011>" + txtaae011.Text.Trim() + "</aae011>"
                + "<ykc018>" + dtpykc018.Text.Trim() + "</ykc018>"
                + "</input>";
            string resultxml = qj.cscf("0215", xml);

            if (resultxml == "") { return; }
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(resultxml);
            XmlNode res = xd.SelectSingleNode("//result");
            int error = int.Parse(res.SelectSingleNode("errorcode").InnerText);
            if (error < 0)
            {
                MessageBox.Show(res.SelectSingleNode("errormsg").InnerText);
            }
            else
            {
                string errormsg = res.SelectSingleNode("errormsg").InnerText;
                string sign = res.SelectSingleNode("sign").InnerText;
                string transid = res.SelectSingleNode("transid").InnerText;

                cydj cy = new cydj();
                var pros = cy.GetType().GetProperties();

                XmlDocument ixd = new XmlDocument();
                ixd.LoadXml(xml);
                XmlNode ipt = ixd.SelectSingleNode("//input");

                for (int i = 0; i < pros.Length; i++)
                {
                    if (pros[i].Name == "sign" || pros[i].Name == "transid") continue;
                    pros[i].SetValue(cy, ipt.SelectSingleNode(pros[i].Name).InnerText, null);
                }


                cy.transid = transid;
                cy.sign = sign;

                string sql = qj.getSql(cy, "cydj", str_id, null);

                DBConn db = new DBConn();
                db.GetSqlCmd(sql);
                MessageBox.Show("出院登记操作成功");
                qj.gxStatus(3, s.aac044, str_id);
            }
        }

        //出院登记回退
        public void cydjht()
        {
            DBConn db = new DBConn();
            string sql = "select * from cydj where aac044 = '" + s.aac044 + "' and id =" + str_id;
            DataTable dt = db.GetDataSet(sql).Tables[0];
            string ykc = "select ykc700 from fhjzdj where aac044 = '" + s.aac044 + "' and id=" + str_id;
            DataTable dt1 = db.GetDataSet(ykc).Tables[0];
            string ykc700 = dt1.Rows[0]["ykc700"].ToString();
            string otransid = dt.Rows[0]["transid"].ToString();
            //string otransid = "4408001905061000101490";
            string aae011 = dt.Rows[0]["aae011"].ToString();
            //string aae011 = "l";

            DialogResult dr = MessageBox.Show("该病号已做出院登记，是否回退？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                string resultxml = qj.cscf("0216", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                    + "<input>"
                    + "<otransid>" + otransid + "</otransid>"
                    + "<aab299>" + PublicCommon.aab299 + "</aab299>"
                    + "<yab600>" + PublicCommon.yab600 + "</yab600>"
                    + "<akb026>" + PublicCommon.akb026 + "</akb026>"
                    + "<akb021>" + PublicCommon.akb021 + "</akb021>"
                    + "<ykc700>" + ykc700 + "</ykc700>"
                    + "<aab301>" + s.aab301 + "</aab301>"
                    + "<yab060>" + s.yab060 + "</yab060>"
                    + "<aac002>" + s.aac002 + "</aac002>"
                    + "<aac043>" + s.aac043 + "</aac043>"
                    + "<aac044>" + s.aac044 + "</aac044>"
                    + "<aae011>" + aae011 + "</aae011>"
                    + "<aae036>" + DateTime.Now.ToString("yyyyMMdd") + "</aae036>"
                    + "</input>");

                if (resultxml == "") { return; }
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(resultxml);
                XmlNode res = xd.SelectSingleNode("//result");
                int error = int.Parse(res.SelectSingleNode("errorcode").InnerText);
                if (error < 0)
                {
                    MessageBox.Show(res.SelectSingleNode("errormsg").InnerText);
                }
                else
                {
                    qj.dHuitui("cydj", s.aac044, str_id);
                    MessageBox.Show("身份证为：" + s.aac044 + " 的病人出院登记回退成功");
                    qj.gxStatus(2, s.aac044, str_id);
                }
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            string cysql = "select * from cydj where aac044 = '" + s.aac044 + "' and id = " + str_id;
            DBConn db = new DBConn();
            DataTable dt = db.GetDataSet(cysql).Tables[0];
            if (dt.Rows.Count < 1) { return; }
            cydj c = new cydj();
            var pros = c.GetType().GetProperties();

            foreach (var p in pros)
            {
                p.SetValue(c, dt.Rows[0][p.Name].ToString(), null);
            }

            groupBox1 = (GroupBox)qj.kjdq(groupBox1, c);
        }

        private void txtaac002_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
