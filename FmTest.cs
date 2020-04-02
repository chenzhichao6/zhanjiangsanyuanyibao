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
    public partial class FmTest : Form
    {
        public FmTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PublicCommon pc = new PublicCommon();
            //查询费用明细
            string resultxml = qj.cscf("0707", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
               + "<input>"
               + "<aab299>" + PublicCommon.aab299 + "</aab299>"
               + "<yab600>" + PublicCommon.yab600 + "</yab600>"
               + "<akb026>" + PublicCommon.akb026 + "</akb026>"
               + "<aab301>441400</aab301>"
               + "<aac002>441421195901051710</aac002>"
               + "<aac043>90</aac043>"
               + "<aac044>441421195901051710</aac044>"
               + "<ykc700>S4414001908300245520</ykc700>"
               + "<ykc618></ykc618>"
               + "<aae310>20180505</aae310>"
               + "<aae311>20190830</aae311>"
               + "<startrow>1</startrow>"
               + "</input>");
            // string resultxml = qj.cscf("0701", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
            //  + "<input>"
            //  + "<startrow>0</startrow>"
            //  + "<aab299>" + PublicCommon.aab299 + "</aab299>"
            //  + "<yab600>" + PublicCommon.yab600 + "</yab600>"
            //  + "<akb026>" + PublicCommon.akb026 + "</akb026>"
            //  + "<aab301>440900</aab301>"
            //  + "<aac002>440902195411024039</aac002>"
            //  + "<aac043>90</aac043>"
            //  + "<aac044>440902195411024039</aac044>"
            //  + "<ykc700>S4409001908135882306</ykc700>"
            //  + "</input>");
            //if (resultxml == "") { return; }
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(resultxml);
            //XmlNode list = doc.SelectSingleNode("//result");
            //int error = int.Parse(list.SelectSingleNode("errorcode").InnerText);
            //if (error < 0)
            //{
            //    string errormsg = list.SelectSingleNode("errormsg").InnerText;
            //    MessageBox.Show("错误信息为：" + errormsg);
            //}
            //else
            //{
            //    XmlNodeList rows = doc.SelectSingleNode("//output").SelectSingleNode("detail").SelectNodes("row");
            //    DataTable dt = new DataTable();

            //    foreach (XmlNode ra in rows[0].ChildNodes)
            //    {
            //        dt.Columns.Add(ra.Name);
            //    }
            //    foreach (XmlNode xml in rows)
            //    {
            //        DataRow dr = dt.NewRow();
            //        foreach (XmlNode x in xml.ChildNodes)
            //        {
            //            dr[x.Name] = x.InnerText;

            //        }
            //        dt.Rows.Add(dr);
            //    }

            //    dataGridView1.DataSource = dt;
            //}

            //string resultxml = qj.cscf("0302", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
            //        + "<input>"
            //        + "<otransid>0</otransid>"
            //        + "<aab299>" + PublicCommon.aab299 + "</aab299>"
            //        + "<yab600>" + PublicCommon.yab600 + "</yab600>"
            //        + "<akb026>" + PublicCommon.akb026 + "</akb026>"
            //        + "<akb021>" + PublicCommon.akb021 + "</akb021>"
            //        + "<ykc700>S4414001908300245520</ykc700>"
            //        + "<aab301>441400</aab301>"
            //        + "<yab060></yab060>"
            //        + "<aac002>441421195901051710</aac002>"
            //        + "<aac043>90</aac043>"
            //        + "<aac044>441421195901051710</aac044>"
            //        + "<ykc610></ykc610>"
            //        + "<aae011>111</aae011>"
            //        + "<aae036>20190826</aae036>"
            //        + "</input>");
            //if (resultxml == "") { return; }
            //XmlDocument xd = new XmlDocument();
            //xd.LoadXml(resultxml);
            //XmlNode res = xd.SelectSingleNode("//result");
            //int error = int.Parse(res.SelectSingleNode("errorcode").InnerText);
            //if (error < 0)
            //{
            //    MessageBox.Show(res.SelectSingleNode("errormsg").InnerText);
            //}
            //else
            //{
            //    MessageBox.Show("身份证为： 的病人出院登记回退成功");

            //}
        }
    }
}
