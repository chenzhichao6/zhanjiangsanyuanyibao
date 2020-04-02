using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WindowsFormsApp1.po;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        Dictionary<string, string> zd = null;
        Sfyz s = null;
        string aac044 = "";
        Form3 frm3 = null;
        string str_id = "";


        public Form5()
        {
            InitializeComponent();
        }

        public Form5(string aac044, Form3 frm, string id)
        {
            InitializeComponent();
            this.aac044 = aac044;
            str_id = id;
            sfyz();
            frm3 = frm;

        }

        public void sfyz()
        {
            string sql = "select * from sfsb where aac044 = '" + aac044 + "' and id=" + str_id;
            DBConn db = new DBConn();
            DataSet ds = db.GetDataSet(sql);
            DataTable dt = ds.Tables[0];

            s = new Sfyz();
            var fss = s.GetType().GetProperties();
            foreach (var f in fss)
            {
                f.SetValue(s, dt.Rows[0][f.Name].ToString(), null);
            }
            Addkj(s);
        }


        //动态添加控件
        public void Addkj(Sfyz s)
        {
            var props = s.GetType().GetProperties();

            int r = 1;
            int a = 1;

            int gd = 0;
            int sign = 0;
            for (int i = 0; i < props.Length; i++)
            {
                Label l1 = new Label();
                l1.AutoSize = true;
                l1.Name = props[i].Name;

                foreach (var z in qj.zd)
                {
                    if (z.Key == props[i].Name)
                    {
                        string aa = qj.pipei(props[i].Name, props[i].GetValue(s, null).ToString());
                        if (aa != null)
                        {
                            if (aa.Length > 100) { aa = aa.Insert(100, "\r\n"); }
                            l1.Text = z.Value + "：" + aa;
                        }
                        else
                        {
                            l1.Text = z.Value + "：" + props[i].GetValue(s, null).ToString();
                        }
                    }
                }
                if (i == 0) { sign = l1.Height + 2; }
                gd += l1.Height+2;
                l1.Size = new Size(41, 12);
                l1.Location = new Point(50 * r * a - 10, gd);
                if (i == 20) { r += 1; a = 5; gd = sign; }
                this.groupBox1.Controls.Add(l1);
            }





        }


        private void Form5_Load(object sender, EventArgs e)
        {

            //DBConn db = new DBConn();
            //string sql1 = "select * from cydj where aac044 = '" + s.aac044 + "'";
            //DataTable dt1 = db.GetDataSet(sql1).Tables[0];
            //if (dt1.Rows.Count > 0) { button1.Text = "出院登记回退"; return; }
            //string fymxsql = "select * from fymxjl where aac044 = '" + s.aac044 + "'";
            //DataTable dt2 = db.GetDataSet(fymxsql).Tables[0];
            //if (dt2.Rows.Count > 0) { button3.Text = "费用明细回退"; return; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmTab ft = new FrmTab(s);
            this.Close();
            ft.ShowDialog();
            //Form6 frm = new Form6(s);
            //this.Close();
            //frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Close();
            //Form14 frm = new Form14(s);
            //frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Form6 frm = new Form6(s);
            //this.Close();
            //frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DBConn db = new DBConn();
            string sql = "select * from gxjzdj where aac044 = '" + s.aac044 + "' and id = " + str_id;
            DataTable dt = db.GetDataSet(sql).Tables[0];
            if (dt.Rows.Count < 1) { MessageBox.Show("该病人尚未就诊登记"); return; }
            gxjzdj gx = new gxjzdj();
            var pros = gx.GetType().GetProperties();
            foreach (var p in pros)
            {
                p.SetValue(gx, dt.Rows[0][p.Name].ToString(), null);
            }

            DialogResult dr = MessageBox.Show("确认对该病人进行就诊登记回退吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                string otran = "select transid from fhjzdj where aac044 = '" + s.aac044 + "' and id=" + str_id;
                string otranid = db.GetDataSet(otran).Tables[0].Rows[0]["transid"].ToString();
                WebReference.STYDJY bbb = new WebReference.STYDJY();
                string resultxml = bbb.STYDJKService("0214", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                    + "<input>"
                    + "<otransid>" + otranid + "</otransid>"
                    + "<aab299>" + PublicCommon.aab299 + "</aab299>"
                    + "<yab600>" + PublicCommon.yab600 + "</yab600>"
                    + "<akb026>" + PublicCommon.akb026 + "</akb026>"
                    + "<akb021>" + PublicCommon.akb021 + "</akb021>"
                    + "<ykc700>" + gx.ykc700 + "</ykc700>"
                    + "<aab301>" + gx.aab301 + "</aab301>"
                    + "<yab060>" + gx.yab060 + "</yab060>"
                    + "<aac002>" + gx.aac002 + "</aac002>"
                    + "<aac043>" + gx.aac043 + "</aac043>"
                    + "<aac044>" + gx.aac044 + "</aac044>"
                    + "<aae011>" + gx.aae011 + "</aae011>"
                    + "<aae036>" + gx.aae036 + "</aae036>"
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
                    MessageBox.Show("身份证为：" + gx.aac044 + " 的病人出院登记回退成功");
                    string sql1 = "delete fhjzdj where aac044 = '" + gx.aac044 + "' and id = " + str_id;
                    string sql2 = "delete gxjzdj where aac044 = '" + gx.aac044 + "' and id = " + str_id;
                    db.GetSqlCmd(sql1);
                    db.GetSqlCmd(sql2);
                    frm3.sfsbcx();
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //this.Close();
            //if (button3.Text == "费用结算业务")
            //{
            //    DialogResult dr = MessageBox.Show("该病号尚未做费用结算业务，是否进行出院业务", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //    if (dr != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}
            //if (button1.Text == "出院登记回退")
            //{
            //    button7_Click(sender, e);
            //    return;
            //}

            //DBConn con = new DBConn();
            //string sql = "select * from fhjzdj where aac044 = '" + s.aac044 + "' ";
            //DataTable dt = con.GetDataSet(sql).Tables[0];
            //if (dt.Rows.Count < 1) { return; }
            //string ykc700 = dt.Rows[0]["ykc700"].ToString();
            //Form7 frm7 = new Form7(s, ykc700);
            //this.Hide();
            //frm7.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form9 frm = new Form9(s.aac044, "WindowsFormsApp1.po.fhjzdj", str_id);
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            DBConn db = new DBConn();
            string sql = "select * from cydj where aac044 = '" + s.aac044 + "' and id = " + str_id;
            DataTable dt = db.GetDataSet(sql).Tables[0];
            string ykc = "select ykc700 from fhjzdj where aac044 = '" + s.aac044 + "' and id = " + str_id;
            DataTable dt1 = db.GetDataSet(ykc).Tables[0];
            string ykc700 = dt1.Rows[0]["ykc700"].ToString();
            string otransid = dt.Rows[0]["transid"].ToString();
            string aae011 = dt.Rows[0]["aae011"].ToString();

            DialogResult dr = MessageBox.Show("该病号已做出院登记，是否回退？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                WebReference.STYDJY bbb = new WebReference.STYDJY();
                string resultxml = bbb.STYDJKService("0216", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
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
                    string sql1 = "delete cydj where aac044 = '" + s.aac044 + "' and id = " + str_id;
                    db.GetSqlCmd(sql1);
                    MessageBox.Show("身份证为：" + s.aac044 + " 的病人出院登记回退成功");
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //this.Close();
            //if (button3.Text == "费用明细回退")
            //{
            //    DBConn db = new DBConn();
            //    string sql = "select * from fymxjl where aac044 = '" + s.aac044 + "'";
            //    DataTable dt = db.GetDataSet(sql).Tables[0];
            //    string ykc = "select ykc700 from fhjzdj where aac044 = '" + s.aac044 + "'";
            //    DataTable dt1 = db.GetDataSet(ykc).Tables[0];
            //    string ykc700 = dt1.Rows[0]["ykc700"].ToString();
            //    string otransid = dt.Rows[0]["transid"].ToString();
            //    string aae011 = dt.Rows[0]["aae011"].ToString();
            //    DialogResult dr = MessageBox.Show("是否回退费用明细？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //    if (dr == DialogResult.OK)
            //    {
            //        WebReference.STYDJY bbb = new WebReference.STYDJY();
            //        string resultxml = bbb.STYDJKService("0302", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
            //            + "<input>"
            //            + "<otransid>" + otransid + "</otransid>"
            //            + "<aab299>440800</aab299>"
            //            + "<yab600>440825</yab600>"
            //            + "<akb026>4400001159975</akb026>"
            //            + "<akb021>湛江市赤坎区中医医院</akb021>"
            //            + "<ykc700>" + ykc700 + "</ykc700>"
            //            + "<aab301>" + formatString(s.aab301 + "</aab301>"
            //            + "<yab060>" + formatString(s.yab060 + "</yab060>"
            //            + "<aac002>" + formatString(s.aac002 + "</aac002>"
            //            + "<aac043>" + formatString(s.aac043 + "</aac043>"
            //            + "<aac044>" + formatString(s.aac044 + "</aac044>"
            //            + "<ykc610></ykc610>"
            //            + "<aae011>" + aae011 + "</aae011>"
            //            + "<aae036>" + DateTime.Now.ToString("yyyyMMdd") + "</aae036>"
            //            + "</input>");

            //        XmlDocument xd = new XmlDocument();
            //        xd.LoadXml(resultxml);
            //        XmlNode res = xd.SelectSingleNode("//result");
            //        int error = int.Parse(res.SelectSingleNode("errorcode").InnerText);
            //        if (error < 0)
            //        {
            //            MessageBox.Show(res.SelectSingleNode("errormsg").InnerText);
            //        }
            //        else
            //        {
            //            string sql1 = "delete fymxjl where aac044 = '" + s.aac044 + "'";
            //            db.GetSqlCmd(sql1);
            //            MessageBox.Show("身份证为：" + s.aac044 + " 的病人是费用明细回退成功");
            //        }
            //    }
            //}
            //else
            //{
            //    DBConn db = new DBConn();
            //    string sql = "select * from gxjzdj where aac044 = '" + s.aac044 + "'";
            //    DataTable dt = db.GetDataSet(sql).Tables[0];

            //    if (dt.Rows.Count > 0)
            //    {
            //        gxjzdj gx = new gxjzdj();
            //        var pros = gx.GetType().GetProperties();
            //        foreach (var p in pros)
            //        {
            //            p.SetValue(gx, dt.Rows[0][p.Name].ToString(), null);
            //        }
            //        Form10 frm = new Form10(gx);
            //        frm.ShowDialog();
            //    }
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {


            this.Close();
            Form12 frm = new Form12(this.s, str_id);
            frm.ShowDialog();
        }
    }
}
