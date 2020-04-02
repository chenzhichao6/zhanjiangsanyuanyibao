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
    public partial class Form12 : Form
    {
        Sfyz s = null;
        gxjzdj gx = null;
        string str_id = "";
        public Form12(Sfyz s,string id)
        {
            this.s = s;
            this.str_id = id;
            init();
            InitializeComponent();
            textBox1.Text = gx.akc190;
        }

        public void init()
        {
            DBConn db = new DBConn();
            string sql = "select * from gxjzdj where aac044 = '" + s.aac044 + "' and id="+str_id;
            DataTable dt = db.GetDataSet(sql).Tables[0];
            gx = new gxjzdj();
            var pros = gx.GetType().GetProperties();
            foreach (var p in pros)
            {
                p.SetValue(gx, dt.Rows[0][p.Name].ToString(), null);
            }


        }
        private void button1_Click(object sender, EventArgs e)
        {

            hisDBConn hdb = new hisDBConn();
            string zyhsql = "select zyh from ZY_BRRY where ZYHM = '" + gx.akc190 + "'";
            DataTable dt1 = hdb.GetDataSet(zyhsql).Tables[0];
            string zyh = dt1.Rows[0]["zyh"].ToString();

            string ykc675 = (from r in qj.ykc675 where comboBox1.SelectedItem.ToString() == r.Value select r.Key).ToList<string>()[0];
            string asql = "select SUM(zjje) as 'akc264' from ZY_JSMX where ZYH = '" + zyh + "'";
            string akc264 = hdb.GetDataSet(asql).Tables[0].Rows[0][0].ToString();
            string resultxml = qj.cscf("0303", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                + "<input>"
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
                + "<ykc675>" + ykc675 + "</ykc675>"
                + "<akc264>" + akc264 + "</akc264>"
                + "</input>");

            if (resultxml == "") { return; }
            mnjs mj = new mnjs();
            mj = (mnjs)qj.rxml(mj, resultxml, null, null, null);
            if (mj != null)
            {
                Addkj(mj);
            }
        }
        //动态添加控件
        public void Addkj(dynamic s)
        {
            var props = s.GetType().GetProperties();

            int r = 1;
            int a = 1;
            int b = 0;
            int c = 0;
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
                            l1.Text = z.Value + "：" + aa;
                        }
                        else
                        {
                            l1.Text = z.Value + "：" + props[i].GetValue(s, null).ToString();
                        }
                    }
                }
                l1.Size = new Size(41, 12);
                l1.Location = new Point(30 * r * a - 10, 20 * (i + 1 - b));
                if (i == 16) { r += 1; a = 6; b = 16; }
                this.groupBox2.Controls.Add(l1);

            }
        }
        private void Form12_Load(object sender, EventArgs e)
        {
            foreach (var q in qj.ykc675)
            {
                comboBox1.Items.Add(q.Value);
            }
            comboBox1.SelectedIndex = 0;
        }
    }
}
