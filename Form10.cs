using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WindowsFormsApp1.po;

namespace WindowsFormsApp1
{
    public partial class Form10 : Form
    {
        fymxlr fl = null;
        string zyhm = "";
        string zyh = "";
        string aae011 = "";
        DataTable mxdt = null;
        gxjzdj gx = null;
        string str_id;
        public Form10()
        {
            InitializeComponent();
        }
        public Form10(gxjzdj gx,string id)
        {
            this.str_id = id;
            this.zyhm = gx.akc190;
            this.aae011 = gx.aae011;
            this.gx = gx;
            InitializeComponent();
            dataGridView1.RowHeadersVisible = false;
        }

        public void init()
        {

            textBox1.Text = zyhm;
            fl = new fymxlr();
            hisDBConn hdb = new hisDBConn();
            string zyhsql = "select zyh from ZY_BRRY where ZYHM = '" + zyhm.Trim() + "'";
            DataTable dt = hdb.GetDataSet(zyhsql).Tables[0];
            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("查询不到该病号的费用明细");
                return;
            }

            zyh = dt.Rows[0]["zyh"].ToString();

            string fymxsql = "  SELECT   f.yzxh as 'akc220',f.rowid as 'ykc610',  s.SFXM as 'yka111',s.SFMC as 'yka112',"
                   + "( case when fyxm = 2 or fyxm = 3 or fyxm = 4 or fyxm = 21 or fyxm = 22 or fyxm = 23  or fyxm = 24  then case when f.yplx = 1  or f.yplx = 3 or f.yplx = 0 or f.yplx = 2"
                   + "then(select distinct ISNULL( ybdm,sbdm) from yk_typk where yk_typk.ypxh = f.fyxh)  end ELSE(select distinct ISNULL( ybdm,sbdm)  from gy_ylsf where gy_ylsf.fyxh"
                   + "= f.fyxh )    END) as 'ake001',    "
                   + "( case when fyxm = 2 or fyxm = 3 or fyxm = 4 or fyxm = 21 or fyxm = 22 or fyxm = 23  or fyxm = 24 then case when f.yplx = 1  or f.yplx = 3 or f.yplx = 0 or f.yplx = 2"
                   + "then(select distinct fymc from yk_typk where yk_typk.ypxh = f.fyxh)  end ELSE(select distinct fymc from  gy_ylsf where gy_ylsf.fyxh"
                   + "= f.fyxh)    END) as 'ake002',"
                   + "( case when fyxm = 2 or fyxm = 3 or fyxm = 4 or fyxm = 21 or fyxm = 22 or fyxm = 23  or fyxm = 24 then case when f.yplx = 1  or f.yplx = 3 or f.yplx = 0 or f.yplx = 2"
                   + " then(select MESS from yk_typk where yk_typk.ypxh = f.fyxh)  end ELSE('')    END) as 'mess' ,"
                   + "'' as 'ake114','0' as 'aka185','' as 'yke230','' as 'yke231',"
                   + "( case when fyxm = 2 or fyxm = 3 or fyxm = 4 or fyxm = 21 or fyxm = 22 or fyxm = 23  or fyxm = 24  then case when f.yplx = 1  or f.yplx = 3 or f.yplx = 0 or f.yplx = 2"
                   + "then(select distinct sbdm from yk_typk where yk_typk.ypxh = f.fyxh)  end ELSE(select distinct sbdm from  gy_ylsf where gy_ylsf.fyxh"
                   + "= f.fyxh)    END) as 'ake005',f.FYMC as 'ake006' , f.FYSL as 'akc226',f.FYDJ as 'akc225', f.ZJJE as 'akc264',yc.CDMC as 'ykc611', '' as 'ykc615','' as 'aka074',"
                   + " '' as 'aka067' , '' as 'aka070','' as 'akc056','' as 'akc273' ,k.KSMC as 'aae386' ,CONVERT(varchar(12) , f.fyrq, 112 ) as 'akc221','" + aae011 + "' as 'aae011','' as 'aae036'"
                   + " FROM ZY_FYMX f join GY_SFXM s on s.SFXM = f.FYXM left join YK_CDDZ yc on yc.YPCD = f.YPCD  join GY_KSDM k on k.KSDM = f.FYKS  WHERE 1 = 1 AND(f.ZYH = '"+zyh+"')  ";

            mxdt = hdb.GetDataSet(fymxsql).Tables[0];

            Form2 frm = new Form2(this);
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "//dlwh.xml";
            frm.ReadXml(path);
            List<string> mesls = new List<string>();
            foreach (DataRow r in mxdt.Rows)
            {
                if (r["mess"].ToString() != "" && r["mess"] != null)
                {
                    mesls.Add(r["ykc610"].ToString());
                }
                foreach (var v in qj.dlwh)
                {
                    if (v.odlmc == r["yka112"].ToString())
                    {
                        if (v.dlmc != "-")
                        {
                            r["yka111"] = (from y in qj.yka111 where y.Value == v.dlmc select y.Key).ToList<string>()[0];
                            r["yka112"] = v.dlmc;
                        }
                    }
                }
                fymx fy = new fymx();
                var pros = fy.GetType().GetProperties();
                foreach (var p in pros)
                {
                    if (p.Name == "yka111")
                    {
                        if (r[p.Name].ToString().Length < 2)
                        {
                            p.SetValue(fy, "0" + r[p.Name].ToString().Trim(), null);
                            continue;
                        }
                    }
                    if (p.Name == "aae036")
                    {
                        p.SetValue(fy, DateTime.Now.ToString("yyyyMMdd"), null);
                        continue;
                    }
                    p.SetValue(fy, r[p.Name].ToString().Trim(), null);
                }
                fl.fyls.Add(fy);
            }
            dataGridView1.DataSource = mxdt;
            DBConn db = new DBConn();
            string fyczsql = "select * from fymxjl where aac044 = '" + gx.aac044 + "'and id = "+str_id;
            DataTable dt2 = db.GetDataSet(fyczsql).Tables[0];
            if (dt2.Rows.Count!= 0) { button1.Text = "费用明细回退"; return; }
            else
            {
                List<DataRow> lsd = new List<DataRow>();
                foreach (var l in mesls)
                {
                    DataRow d = mxdt.Select("ykc610 = " + l + "")[0];
                    DialogResult dr = MessageBox.Show(d["ake006"].ToString() + ",该药品为（" + d["mess"].ToString() + "）使用药品！是[可报销]，否[自费]?", "提示：医保限制用药", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        d["aka185"] = "1";
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (mxdt == null) { return; }
            DataTable lsdt = mxdt.Clone();
            DataRow[] drs = mxdt.Select("ake006 like '%" + textBox3.Text.Trim() + "%'");
            foreach (var d in drs)
            {
                lsdt.Rows.Add(d.ItemArray);
            }
            dataGridView1.DataSource = lsdt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (button1.Text == "费用明细回退")
                {
                    DBConn db = new DBConn();
                    string sql = "select * from fymxjl where aac044 = '" + gx.aac044 + "' and id='"+str_id+"'";
                    DataTable dt = db.GetDataSet(sql).Tables[0];
                    string otransid = dt.Rows[0]["transid"].ToString();
                    DialogResult dr = MessageBox.Show("是否回退费用明细？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        string resultxml = qj.cscf("0302", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                            + "<input>"
                            + "<otransid>0</otransid>"
                            + "<aab299>"+PublicCommon.aab299+"</aab299>"
                            + "<yab600>"+PublicCommon.yab600+"</yab600>"
                            + "<akb026>"+PublicCommon.akb026+"</akb026>"
                            + "<akb021>"+PublicCommon.akb021+"</akb021>"
                            + "<ykc700>" + gx.ykc700 + "</ykc700>"
                            + "<aab301>" + gx.aab301 + "</aab301>"
                            + "<yab060>" + gx.yab060 + "</yab060>"
                            + "<aac002>" + gx.aac002 + "</aac002>"
                            + "<aac043>" + gx.aac043 + "</aac043>"
                            + "<aac044>" + gx.aac044 + "</aac044>"
                            + "<ykc610></ykc610>"
                            + "<aae011>" + gx.aae011 + "</aae011>"
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
                            string sql1 = "delete fymxjl where aac044 = '" + gx.aac044 + "' and id = "+str_id;
                            db.GetSqlCmd(sql1);
                            MessageBox.Show("身份证为：" + gx.aac044 + " 的病人是费用明细回退成功");
                            qj.gxStatus(1, gx.aac044, str_id);
                            button1.Text = "费用明细上传";
                        }
                    }
                    return;
                }

                List<fymx> fx = fl.fyls;
                if (fx.Count > 500)
                {
                    int j = 500;
                    for (int i = 0; i < fx.Count / 500 + 1; i++)
                    {
                        List<fymx> cList = new List<fymx>();
                        cList = fx.Take(j).Skip(i * 500).ToList();
                        j += 500;
                        SCXml(cList);
                    }
                }
                else
                {
                    SCXml(fx);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "操作超时")
                {
                    DialogResult dr1 = MessageBox.Show(ex.Message + "。是否超时重发？", "提示", MessageBoxButtons.YesNo);
                    if (dr1 == DialogResult.Yes)
                    {
                        button1_Click(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void SCXml(List<fymx> fx)
        {
                string xml = "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                    + "<input>"
                    + "<aab299>"+PublicCommon.aab299+"</aab299>"
                    + "<yab600>"+PublicCommon.yab600 + "</yab600>"
                    + "<akb026>"+PublicCommon.akb026+"</akb026>"
                    + "<akb021>"+PublicCommon.akb021+"</akb021>"
                    + "<ykc700>" + gx.ykc700 + "</ykc700>"
                    + "<aab301>" + gx.aab301 + "</aab301>"
                    + "<yab060>" + gx.yab060 + "</yab060>"
                    + "<aac002>" + gx.aac002 + "</aac002>"
                    + "<aac043>" + gx.aac043 + "</aac043>"
                    + "<aac044>" + gx.aac044 + "</aac044>"
                    + "<detail>";
                foreach (var f in fx)
                {
                    xml += "<row>";
                    var pros = f.GetType().GetProperties();
                    foreach (var p in pros)
                    {
                        xml += "<" + p.Name + ">" + p.GetValue(f, null) + "</" + p.Name + ">";
                    }
                    xml += "</row>";
                }
                xml += "</detail></input>";

                string resultxml = qj.cscf("0301", xml);

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
                    string insql = "INSERT INTO [ydjs_zyy].[dbo].[fymxjl] ([sign],[transid],[errorcode],[errormsg],[aac044],[aae011],[id])" +
                                 "VALUES ('" + sign + "','" + transid + "','" + error + "','" + errormsg + "','" + gx.aac044 + "','" + aae011 + "'," + str_id + ")";
                    DBConn db = new DBConn();
                   int i = db.GetSqlCmd(insql);
                if (i > 0)
                {
                    MessageBox.Show("费用明细录入成功");
                    qj.gxStatus(2, gx.aac044, str_id);
                    button1.Text = "费用明细回退";
                }
                }
            
            
        }

        private void 大类维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2(this);
            frm.ShowDialog(this);
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            init();
        }
    }
}
