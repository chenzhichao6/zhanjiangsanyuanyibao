using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp1.po;
using System.Linq;
using System.Xml;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {
        public Sfyz s = null;
        public gxjzdj gj = null;
        public int isgx = 0;
        public string str_id = "";

        public Form6()
        {
            InitializeComponent();
            init();
        }
        public Form6(Sfyz s,string id)
        {
            this.s = s;
            this.str_id = id;
            InitializeComponent();
            gxinit();
        }

        //更新就诊登记初始化
        public void gxinit()
        {

            DBConn db = new DBConn();
            string sql = "select * from gxjzdj where aac044 = '" + s.aac044 + "' and id = "+str_id;
            DataTable dt = db.GetDataSet(sql).Tables[0];
            init();
            if (dt.Rows.Count == 0)
            {
                button5.Visible = false;
                button6.Visible = false;

                label37.Visible = false;
                label38.Visible = false;
                txttransid.Visible = false;
                txtykc700.Visible = false;
                button1.Text = "提交就诊登记";
                return;
            }
            button5.Visible = true;
            button6.Visible = true;

            label37.Visible = true;
            label38.Visible = true;
            txttransid.Visible = true;
            txtykc700.Visible = true;

            button1.Text = "更新就诊登记";

            gj = new gxjzdj();
            var pros = gj.GetType().GetProperties();
            foreach (var p in pros)
            {
                p.SetValue(gj, dt.Rows[0][p.Name].ToString(), null);
            }

            panel1 =(Panel) qj.kjdq(this.panel1, gj);
        }

        public void init()
        {
            DBConn conn = new DBConn();


            foreach (var c in qj.ykc679)
            {
                comykc679.Items.Add(c.Value);
            }

            foreach (var c in qj.ykc680)
            {
                comykc680.Items.Add(c.Value);
            }

            comykc679.SelectedIndex = 0;
            comykc680.SelectedIndex = 0;

            var bm = new AutoCompleteStringCollection();
            bm.AddRange(qj.icd.Keys.ToArray<string>());
            txtakc193.AutoCompleteCustomSource = bm;
            txtakc193.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtakc193.AutoCompleteSource = AutoCompleteSource.CustomSource;

            txtykc601.AutoCompleteCustomSource = bm;
            txtykc601.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtykc601.AutoCompleteSource = AutoCompleteSource.CustomSource;

            txtykc602.AutoCompleteCustomSource = bm;
            txtykc602.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtykc602.AutoCompleteSource = AutoCompleteSource.CustomSource;

            var jb = new AutoCompleteStringCollection();
            jb.AddRange(qj.icd.Values.ToArray<string>());
            txtakc050.AutoCompleteCustomSource = jb;
            txtakc050.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtakc050.AutoCompleteSource = AutoCompleteSource.CustomSource;


            var ks = new AutoCompleteStringCollection();
            ks.AddRange(qj.akf001.Values.ToArray<string>());
            txtakf001.AutoCompleteCustomSource = ks;
            txtakf001.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtakf001.AutoCompleteSource = AutoCompleteSource.CustomSource;



            txtaac002.Text = s.aac002;
            txtaac044.Text = s.aac044;
            foreach (var z in qj.aac043)
            {
                if (s.aac043 == z.Key)
                {
                    comaac043.SelectedItem = z.Value;
                }
            }

            foreach (var y in qj.aka130)
            {
                if (s.aka130 == y.Key)
                {
                    comaka130.SelectedItem = y.Value;
                }
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Trim() == "更新就诊登记")
            {
                gxjzdj();
            }
            else
            {
                djjzdj();
            }
        }

        //更新就诊登记
        public void gxjzdj()
        {
            string ryks = "";
            var lryks = (from r in qj.akf001 where r.Value == txtakf001.Text.Trim() select r.Key).ToList<string>();
            if (lryks.Count > 0) { ryks = lryks[0]; }

            string ryyy = (from r in qj.ykc679 where r.Value == comykc679.SelectedItem.ToString() select r.Key).ToList<string>()[0];
            string bzlx = (from r in qj.ykc680 where r.Value == comykc680.SelectedItem.ToString() select r.Key).ToList<string>()[0];

            string resultxml = qj.cscf("0213", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                + "<input>"
                + "<aab299>"+PublicCommon.aab299+"</aab299>"
                + "<yab600>"+PublicCommon.yab600+"</yab600>"
                + "<akb026>"+PublicCommon.akb026+"</akb026>"
                + "<akb021>"+PublicCommon.akb021+"</akb021>"
                + "<aab301>" + s.aab301 + "</aab301>"
                + "<ykc700>" + txtykc700.Text.Trim() + "</ykc700>"
                + "<yab060>" + s.yab060 + "</yab060>"
                + "<aac002>" + s.aac002 + "</aac002>"
                + "<aac043>" + s.aac043 + "</aac043>"
                + "<aac044>" + s.aac044 + "</aac044>"
                + "<aka130>" + s.aka130 + "</aka130>"
                + "<akc190>" + txtakc190.Text.Trim() + "</akc190>"
                + "<akf001>" + ryks + "</akf001>"
                + "<yzz018>" + txtyzz018.Text.Trim() + "</yzz018>"
                + "<yzz019>" + txtyzz019.Text.Trim() + "</yzz019>"
                + "<ykc012>" + txtykc012.Text.Trim() + "</ykc012>"
                + "<akc050>" + txtakc050.Text.Trim() + "</akc050>"
                + "<ykc679>" + ryyy + "</ykc679>"
                + "<ykc680>" + bzlx + "</ykc680>"
                + "<akc193>" + txtakc193.Text.Trim() + "</akc193>"
                + "<ykc601>" + txtykc601.Text.Trim() + "</ykc601>"
                + "<ykc602>" + txtykc602.Text.Trim() + "</ykc602>"
                + "<akc056>" + txtakc056.Text.Trim() + "</akc056>"
                + "<ake022>" + txtake022.Text.Trim() + "</ake022>"
                + "<ykc701>" + dtpykc701.Text.Trim() + "</ykc701>"
                + "<aae011>" + txtaae011.Text.Trim() + "</aae011>"
                + "<aae036>" + txtaae036.Text.Trim() + "</aae036>"
                + "<aae004>" + txtaae004.Text.Trim() + "</aae004>"
                + "<aae005>" + txtaae005.Text.Trim() + "</aae005>"
                + "<aae013>" + txtaae013.Text.Trim() + "</aae013>"
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
                string transid = res.SelectSingleNode("transid").InnerText;
                DBConn db = new DBConn();
                string gxsql = "INSERT INTO [ydjs_zyy].[dbo].[gxjzdj]([sign],[transid],[aab299],[yab600],[akb026],[akb021],[aab301],[ykc700],[yab060],[aac002]," +
                             "[aac043],[aac044],[aka130],[ykc009],[akc190],[akf001],[yzz018],[yzz019],[ykc012],[akc050],[ykc679],[ykc680]," +
                             "[akc193],[ykc601],[ykc602],[akc056],[ake022],[ykc701],[aae011],[aae036],[aae004],[aae005],[aae013],[id])" +
                             "VALUES('" + s.sign + "','" + transid + "','440800','440825','4400001159975','湛江市赤坎区中医医院','" + s.aab301 + "','" + txtykc700.Text.Trim() + "'," +
                             "'" + s.yab060 + "','" + s.aac002 + "','" + s.aac043 + "','" + s.aac044 + "','" + s.aka130 + "','" + txtykc009.Text.Trim() + "'," +
                             "'" + txtakc190.Text.Trim() + "','" + ryks + "','" + txtyzz018.Text.Trim() + "','" + txtyzz019.Text.Trim() + "'," +
                             "'" + txtykc012.Text.Trim() + "','" + txtakc050.Text.Trim() + "','" + ryyy + "','" + bzlx + "'," +
                             "'" + txtakc193.Text.Trim() + "','" + txtykc601.Text.Trim() + "','" + txtykc602.Text.Trim() + "','" + txtakc056.Text.Trim() + "'," +
                             "'" + txtake022.Text.Trim() + "','" + dtpykc701.Text.Trim() + "','" + txtaae011.Text.Trim() + "','" + txtaae036.Text.Trim() + "'," +
                             "'" + txtaae004.Text.Trim() + "','" + txtaae005.Text.Trim() + "','" + txtaae013.Text.Trim() + "',"+str_id+")";
                string sql = "DELETE FROM [gxjzdj] WHERE aac044 = '" + s.aac044 + "' AND id="+str_id;
                db.GetSqlCmd(sql);
                int f = db.GetSqlCmd(gxsql);
                if (f > 0)
                {
                    MessageBox.Show("更新就诊登记成功");
                }
            }
        }

        //就诊登记
        public void djjzdj()
        {

            string ryks = "";
            var lryks = (from r in qj.akf001 where r.Value == txtakf001.Text.Trim() select r.Key).ToList<string>();
            if (lryks.Count > 0) { ryks = lryks[0]; }

            string ryyy = (from r in qj.ykc679 where r.Value == comykc679.SelectedItem.ToString() select r.Key).ToList<string>()[0];
            string bzlx = (from r in qj.ykc680 where r.Value == comykc680.SelectedItem.ToString() select r.Key).ToList<string>()[0];

            string resultxml = qj.cscf("0212", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                + "<input>"
                + "<aab299>" + PublicCommon.aab299+"</aab299>"
                + "<yab600>" + PublicCommon.yab600 + "</yab600>"
                + "<akb026>" + PublicCommon.akb026 + "</akb026>"
                + "<akb021>" + PublicCommon.akb021 + "</akb021>"
                + "<aab301>" + s.aab301 + "</aab301>"
                + "<yab060>" + s.yab060 + "</yab060>"
                + "<aac002>" + s.aac002 + "</aac002>"
                + "<aac043>" + s.aac043 + "</aac043>"
                + "<aac044>" + s.aac044 + "</aac044>"
                + "<aka130>" + s.aka130 + "</aka130>"
                + "<ykc009>" + txtykc009.Text.Trim() + "</ykc009>"
                + "<akc190>" + txtakc190.Text.Trim() + "</akc190>"
                + "<akf001>" + ryks + "</akf001>"
                + "<yzz018>" + txtyzz018.Text.Trim() + "</yzz018>"
                + "<yzz019>" + txtyzz019.Text.Trim() + "</yzz019>"
                + "<ykc012>" + txtykc012.Text.Trim() + "</ykc012>"
                + "<akc050>" + txtakc050.Text.Trim() + "</akc050>"
                + "<ykc679>" + ryyy + "</ykc679>"
                + "<ykc680>" + bzlx + "</ykc680>"
                + "<akc193>" + txtakc193.Text.Trim() + "</akc193>"
                + "<ykc601>" + txtykc601.Text.Trim() + "</ykc601>"
                + "<ykc602>" + txtykc602.Text.Trim() + "</ykc602>"
                + "<akc056>" + txtakc056.Text.Trim() + "</akc056>"
                + "<ake022>" + txtake022.Text.Trim() + "</ake022>"
                + "<ykc701>" + dtpykc701.Text.Trim() + "</ykc701>"
                + "<aae011>" + txtaae011.Text.Trim() + "</aae011>"
                + "<aae036>" + txtaae036.Text.Trim() + "</aae036>"
                + "<aae004>" + txtaae004.Text.Trim() + "</aae004>"
                + "<aae005>" + txtaae005.Text.Trim() + "</aae005>"
                + "<aae013>" + txtaae013.Text.Trim() + "</aae013>"
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
                string ykc700 = res.SelectSingleNode("output").SelectSingleNode("ykc700").InnerText;
                string transid = res.SelectSingleNode("transid").InnerText;
                //FileStream fs = new FileStream("D://ak.txt", FileMode.Create);
                //StreamWriter sw = new StreamWriter(fs);
                ////开始写入
                //sw.Write(ykc700);
                ////清空缓冲区
                //sw.Flush();
                ////关闭流
                //sw.Close();
                //fs.Close();

                string gxsql = "INSERT INTO [ydjs_zyy].[dbo].[gxjzdj]([sign],[transid],[aab299],[yab600],[akb026],[akb021],[aab301],[ykc700],[yab060],[aac002]," +
                             "[aac043],[aac044],[aka130],[ykc009],[akc190],[akf001],[yzz018],[yzz019],[ykc012],[akc050],[ykc679],[ykc680]," +
                             "[akc193],[ykc601],[ykc602],[akc056],[ake022],[ykc701],[aae011],[aae036],[aae004],[aae005],[aae013],[id])" +
                             "VALUES('" + s.sign + "','" + transid + "','"+ PublicCommon .aab299+ "','"+ PublicCommon.yab600+ "','"+ PublicCommon.akb026+ "','"+ PublicCommon.akb021+ "','" + s.aab301 + "','" + ykc700 + "'," +
                             "'" + s.yab060 + "','" + s.aac002 + "','" + s.aac043 + "','" + s.aac044 + "','" + s.aka130 + "','" + txtykc009.Text.Trim() + "'," +
                             "'" + txtakc190.Text.Trim() + "','" + ryks + "','" + txtyzz018.Text.Trim() + "','" + txtyzz019.Text.Trim() + "'," +
                             "'" + txtykc012.Text.Trim() + "','" + txtakc050.Text.Trim() + "','" + ryyy + "','" + bzlx + "'," +
                             "'" + txtakc193.Text.Trim() + "','" + txtykc601.Text.Trim() + "','" + txtykc602.Text.Trim() + "','" + txtakc056.Text.Trim() + "'," +
                             "'" + txtake022.Text.Trim() + "','" + dtpykc701.Text.Trim() + "','" + txtaae011.Text.Trim() + "','" + txtaae036.Text.Trim() + "'," +
                             "'" + txtaae004.Text.Trim() + "','" + txtaae005.Text.Trim() + "','" + txtaae013.Text.Trim() + "',"+str_id+")";
                DBConn db = new DBConn();
                int re = db.GetSqlCmd(gxsql);
                string result = "";
                if (re > 0)
                {
                    result += "就诊登记已经记录";
                }

                fhjzdj fj = new fhjzdj();
                var pors = fj.GetType().GetProperties();
                XmlNode op = res.SelectSingleNode("output");
                for (int i = 0; i < pors.Length; i++)
                {
                    if (pors[i].Name == "sign" || pors[i].Name == "transid" || pors[i].Name == "errorcode" || pors[i].Name == "errormsg")
                    {
                        pors[i].SetValue(fj, res.SelectSingleNode(pors[i].Name).InnerText, null);
                    }
                    else
                    {
                        pors[i].SetValue(fj, op.SelectSingleNode(pors[i].Name).InnerText, null);
                    }
                }
                string sql = qj.getSql(fj, fj.GetType().Name,str_id,null);
                int f = db.GetSqlCmd(sql);
                if (f > 0)
                {
                    result = "操作成功！";
                    qj.gxStatus(1, s.aac044, str_id);
                }
                MessageBox.Show(result);
                gxinit();
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DBConn db = new DBConn();
            DialogResult dr = MessageBox.Show("确认对该病人进行就诊登记回退吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                string otran = "select transid from fhjzdj where aac044 = '" + s.aac044 + "' and id ="+str_id;
                string otranid = db.GetDataSet(otran).Tables[0].Rows[0]["transid"].ToString();
                string resultxml = qj.cscf("0214", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                    + "<input>"
                    + "<otransid>" + otranid + "</otransid>"
                    + "<aab299>"+PublicCommon.aab299+"</aab299>"
                    + "<yab600>"+PublicCommon.yab600+"</yab600>"
                    + "<akb026>"+PublicCommon.akb026+"</akb026>"
                    + "<akb021>"+PublicCommon.akb021+"</akb021>"
                    + "<ykc700>" + gj.ykc700 + "</ykc700>"
                    + "<aab301>" + gj.aab301 + "</aab301>"
                    + "<yab060>" + gj.yab060 + "</yab060>"
                    + "<aac002>" + gj.aac002 + "</aac002>"
                    + "<aac043>" + gj.aac043 + "</aac043>"
                    + "<aac044>" + gj.aac044 + "</aac044>"
                    + "<aae011>" + gj.aae011 + "</aae011>"
                    + "<aae036>" + gj.aae036 + "</aae036>"
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
                    MessageBox.Show("身份证为：" + gj.aac044 + " 的病人出院登记回退成功");
                    qj.gxStatus(0, s.aac044, str_id);
                    string sql1 = "delete fhjzdj where aac044 = '" + gj.aac044 + "' and id ="+str_id;
                    string sql2 = "delete gxjzdj where aac044 = '" + gj.aac044 + "' and id ="+str_id;
                    db.GetSqlCmd(sql1);
                    db.GetSqlCmd(sql2);
                    qj.dHuitui("fymxjl", gj.aac044, str_id);
                    gxinit();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form9 frm = new Form9(s.aac044, "WindowsFormsApp1.po.fhjzdj",str_id);
            frm.ShowDialog();
        }

       
    }
}