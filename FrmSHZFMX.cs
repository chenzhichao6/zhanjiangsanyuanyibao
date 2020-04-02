using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WindowsFormsApp1.po;

namespace WindowsFormsApp1
{
    public partial class FrmSHZFMX : Form
    {
        string aae140 = "";
        DBConn db;
        String strSql = "";
        SqlDataAdapter sqlDataAdapter;
        SqlDataReader sqlDataReader;
        DataSet dataSet;
        PublicCommon publicCommon;
        ArrayList arrayList_content;
        ArrayList arrayList_Width;
        public FrmSHZFMX(string aae140)
        {
            InitializeComponent();
            this.aae140 = aae140;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmSHZFMX_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime datetime = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);

            strSql = "select count(*)  from shzf where yzz060='" + dateTimePicker1.Value.ToString("yyyy") + "' and yzz061='" + dateTimePicker1.Value.ToString("MM") + "' and aae140 = '" + aae140 + "'";
            sqlDataReader = db.GetDataReader(strSql);
            sqlDataReader.Read();
            if (sqlDataReader.GetValue(0).ToString() == "1")
            {
                button2.Text = "回退";
            }
            else
            {
                button2.Text = "提交";
            }
            if (button2.Text == "提交")
            {
                strSql = "select sfsb.aac002,sfsb.aac043,sfsb.aac044,sfsb.aac003,"
                    + "sfsb.aab301,gxjzdj.ykc700,fyjs.ykc618,gxjzdj.ykc701,cydj.ykc702,"
                    + "cydj.akb063,fyjs.akc194,gxjzdj.akc050,cydj.akc185,fyjs.akc264,"
                    + "fyjs.akb068,fyjs.ykc630,gxjzdj.aka130,aae140,gxjzdj.akc190,fyjs.yzz139,'无' as aae013"
                    + " from  gxjzdj,sfsb,fyjs,cydj "
                    + " where  gxjzdj.id = sfsb.id and sfsb.id = fyjs.id and sfsb.id = cydj.id "
                    + " and fyjs.akc194 between '" + datetime.ToString("yyyyMMdd") + "' and '" + datetime.AddMonths(1).AddDays(-1).ToString("yyyyMMdd") + "'"
                    + " and aae140 = '" + aae140 + "'";
                sqlDataAdapter = db.GetDataAdapter(strSql);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "caigou");
                dataGridView1.DataSource = dataSet.Tables["caigou"].DefaultView;
                strSql = "select SUM(cast(akc264 as numeric(18,2))),COUNT(*) from fyjs where aae140 = '" + aae140 + "' and left(CONVERT(varchar(100), akc194, 112),6)='" + dateTimePicker1.Value.ToString("yyyyMM") + "'";
                sqlDataReader = db.GetDataReader(strSql);
                sqlDataReader.Read();
                label3.Text = sqlDataReader.GetValue(1).ToString();
                label6.Text = sqlDataReader.GetValue(0).ToString();
            }
            else
            {
                strSql = "select [transid],[aab301],[aab299],[yzz060],[yzz061],[yzz065],[yzz066],[yzz067]" +
                    ",[yzz041],[yzz042],[yzz131],[aae140],[yzz134],[yzz135],[yzz136],[yzz137],[yzz138] from shzf" +
                    " where and aae140 = '" + aae140 + "' and yzz060='" + dateTimePicker1.Value.ToString("yyyy") + "' and yzz061='" + dateTimePicker1.Value.ToString("MM") + "'";
                sqlDataAdapter = db.GetDataAdapter(strSql);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "caigou");
                DataTable dt = dataSet.Tables[0];
                dt = qj.ppdt(dt);
                dataGridView1.DataSource = dt;
                strSql = "select ROW_NUMBER() OVER (ORDER BY fyjs.akc194 ASC) AS 序号,aac003 as "
                        + " 姓名,sfsb.aab301 as 参保地,sfsb.aac002 as 身份证号码,akc190 as 住院号 "
                        + " ,ykc701 as 入院日期,ykc702 as 出院日期,CONVERT(varchar(100), fyjs.akc194, 112) as 结算日期,akc050 "
                        + " as 入院诊断,cydj.akc185 as 出院诊断,fyjs.akc264 as 医疗费总额,ykc624 "
                        + " as 个人自负,akb068 as 统筹支付,ykc630 as 大病统筹,gxjzdj.aka130 as "
                        + " 医疗类别,'' as 备注 from sfsb, gxjzdj, cydj, fyjs where "
                        + " sfsb.id = gxjzdj.id and sfsb.id = cydj.id and sfsb.id = fyjs.id "
                        + " and aae140 = '" + aae140 + "' and fyjs.akc194 between '" + datetime.ToString("yyyyMMdd") + "' and '" + datetime.AddMonths(1).AddDays(-1).ToString("yyyyMMdd") + "'";
                sqlDataAdapter = db.GetDataAdapter(strSql);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "caigou");
                DataTable dt1 = dataSet.Tables[0];
                dt1 = qj.ppdt(dt1);
                dataGridView2.DataSource = dt1;
                strSql = "select ROW_NUMBER() OVER (ORDER BY fyjs.akc194 ASC) AS 序号,"
                        + " cydj.aab301 as 参保地,COUNT(*) as 就医人数,COUNT(*) as 就医人次,"
                        + " SUM(cast(akc264 as numeric(18, 2))) as 医疗费总额,"
                        + " SUM(cast(ykc624 as numeric(18, 2))) as 个人自负金额,"
                        + " SUM(cast(akb068 as numeric(18, 2))) as 记账金额,"
                        + " SUM(cast(ykc630 as numeric(18, 2))) as 大病金额,'' as 备注"
                        + " from fyjs, cydj where cydj.id = fyjs.id"
                        + " and aae140 = '" + aae140 + "' and fyjs.akc194 between '" + datetime.ToString("yyyyMMdd") + "' and '" + datetime.AddMonths(1).AddDays(-1).ToString("yyyyMMdd") + "'"
                        + " group by fyjs.akc194,cydj.aab301";
                sqlDataAdapter = db.GetDataAdapter(strSql);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "caigou");
                DataTable dt2 = dataSet.Tables[0];
                dt2 = qj.ppdt(dt2);
                dataGridView3.DataSource = dt2;

            }
        }

        private string getStringFormat(string head, string Content, string num)
        {
            string Str_txt3 = "";
            int i_num = int.Parse(num.Substring(head.Length + Content.Length));
            i_num = i_num + 1;
            if (i_num.ToString().Length < 8)
            {
                for (int i = 0; i < (8 - i_num.ToString().Length); i++)
                {
                    Str_txt3 = "0" + Str_txt3;
                }
            }
            return head + Content + Str_txt3;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string yzz065 = "";
            String Str_xml = "";
            strSql = "select yzz065 from ydsb where  yzz060='" + dateTimePicker1.Value.ToString("yyyy") + "' and yzz061='" + dateTimePicker1.Value.ToString("MM") + "' and aae140 = '" + aae140 + "'";
            //   sqlDataReader = new SqlDataReader();
            sqlDataReader = db.GetDataReader(strSql);

            if (sqlDataReader.Read())
            {
                yzz065 = getStringFormat("B440800", dateTimePicker1.Value.ToString("yy") + dateTimePicker1.Value.ToString("MM"), sqlDataReader.GetValue(0).ToString());

            }
            else
            {
                yzz065 = "B440800" + dateTimePicker1.Value.ToString("yy") + dateTimePicker1.Value.ToString("MM") + "0000001";


            }
            if (button2.Text == "提交")
            {
                Str_xml = "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                   + "<input>"
                   + "<aab299>" + PublicCommon.aab299 + "</aab299>"

                   + "<akb026>" + PublicCommon.akb026 + "</akb026>"
                   + "<akb021>" + PublicCommon.akb021 + "</akb021>"
                   + "<yzz060>2020</yzz060>"
                   + "<yzz061>02</yzz061>"
                   + "<yzz062>" + yzz065 + "</yzz062>"
                   /* + "<aae011>" + "LH" + "</aae011>"
                    + "<aae036>" + DateTime.Now.ToString("yyyy-MM-dd") + "</aae036>"
                    */
                   + "<yzz063>" + label3.Text + "</yzz063>"
                   + "<yzz064>" + label6.Text + "</yzz064>"
                   + "<yzz134>" + textBox1.Text.Trim() + "</yzz134>"
                   + "<yzz135>" + textBox1.Text.Trim() + "</yzz135>"
                   + "<yzz136>" + textBox1.Text.Trim() + "</yzz136>"
                   + "<yzz137>" + DateTime.Now.ToString("yyyyMMdd") + "</yzz137>"
                   + "<yzz138>" + textBox2.Text.Trim() + "</yzz138>"
                   + "<yzz041>" + dateTimePicker1.Value.AddDays(1 - dateTimePicker1.Value.Day).ToString("yyyyMMdd") + "</yzz041>"
                   + "<yzz042>" + dateTimePicker1.Value.AddDays(1 - dateTimePicker1.Value.Day).Date.AddMonths(1).AddSeconds(-1).ToString("yyyyMMdd") + "</yzz042>"
               + "<aae140>" + aae140 + "</aae140><detail>";


                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    Str_xml = Str_xml + " <row>"
                        + "<aac002>" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "</aac002>"
                        + "<aac043>" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "</aac043>"
                        + "<aac044>" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "</aac044>"
                        + "<aac003>" + dataGridView1.Rows[i].Cells[3].Value.ToString() + "</aac003>"
                        + "<aab301>" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "</aab301>"
                        + "<ykc700>" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "</ykc700>"
                        + "<ykc618>" + dataGridView1.Rows[i].Cells[6].Value.ToString() + "</ykc618>"
                        + "<ykc701>" + dataGridView1.Rows[i].Cells[7].Value.ToString() + "</ykc701>"
                        + "<ykc702>" + dataGridView1.Rows[i].Cells[8].Value.ToString() + "</ykc702>"
                        + "<akb063>" + dataGridView1.Rows[i].Cells[9].Value.ToString() + "</akb063>"
                        + "<akc194>" + DateTime.Parse(dataGridView1.Rows[i].Cells[10].Value.ToString()).ToString("yyyyMMdd") + "</akc194>"
                        + "<akc050>" + dataGridView1.Rows[i].Cells[11].Value.ToString() + "</akc050>"
                        + "<akc185>" + dataGridView1.Rows[i].Cells[12].Value.ToString() + "</akc185>"
                        + "<akc264>" + dataGridView1.Rows[i].Cells[13].Value.ToString() + "</akc264>"
                        + "<akb068>" + dataGridView1.Rows[i].Cells[14].Value.ToString() + "</akb068>"
                        + "<ykc630>" + dataGridView1.Rows[i].Cells[15].Value.ToString() + "</ykc630>"
                        + "<aka130>" + dataGridView1.Rows[i].Cells[16].Value.ToString() + "</aka130>"
                        + "<aae140>" + dataGridView1.Rows[i].Cells[17].Value.ToString() + "</aae140>"
                        + "<akc190>" + dataGridView1.Rows[i].Cells[18].Value.ToString() + "</akc190>"

                        + "<yzz139>" + dataGridView1.Rows[i].Cells[19].Value.ToString() + "</yzz139>"
                        + "<aae013>" + dataGridView1.Rows[i].Cells[20].Value.ToString() + "</aae013>"

                       + "</row>";
                }

                Str_xml = Str_xml + "</detail></input>";

                string resultxml = qj.cscf("0521", Str_xml);
                // MessageBox.Show(resultxml);
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
                    string sign = list.SelectSingleNode("sign").InnerText;
                    string transid = list.SelectSingleNode("transid").InnerText;
                    string errorcode = list.SelectSingleNode("errorcode").InnerText;
                    string errormsg = list.SelectSingleNode("errormsg").InnerText;
                    string yzz060 = dateTimePicker1.Value.ToString("yyyy");
                    string yzz061 = dateTimePicker1.Value.ToString("MM");
                    // string yzz062 = dateTimePicker1.Value.Year.ToString();
                    string flag = "1";
                    string strSql = "insert into ydsb values('" + sign + "'"
                        + ",'" + transid + "','" + errorcode + "','" + errormsg + "','"
                        + yzz060 + "','" + yzz061 + "','" + yzz065 + "','" + flag + "','" + aae140 + "')";
                    ;
                    if (db.GetSqlCmd(strSql) != 0)
                    {
                        MessageBox.Show("申报成功！");
                    }

                }
            }
            else if (button2.Text == "回退")
            {
                Str_xml = "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                   + "<input>"
                   + "<otransid>" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "</otransid>"
                   + "<aab299>" + PublicCommon.aab299 + "</aab299>"
                   + "<akb026>" + PublicCommon.akb026 + "</akb026>"
                   + "<akb021>" + PublicCommon.akb021 + "</akb021>"
                   + "<yzz060>" + dataGridView1.Rows[0].Cells[1].Value.ToString() + "</yzz060>"
                   + "<yzz061>" + dataGridView1.Rows[0].Cells[2].Value.ToString() + "</yzz061>"
                   + "<yzz062>" + dataGridView1.Rows[0].Cells[3].Value.ToString() + "</yzz062>"
                   + "<aae140>" + dataGridView1.Rows[0].Cells[4].Value.ToString() + "</aae140>"
                   + "</input>";
                string resultxml = qj.cscf("0522", Str_xml);
                if (resultxml == "") { return; }
                // MessageBox.Show(resultxml);
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
                    strSql = "update ydsb set flag=0 where transid='" + dataGridView1.Rows[0].Cells[0].Value.ToString() + "'";
                    if (db.GetSqlCmd(strSql) != 0)
                    {

                        MessageBox.Show("回退成功！");
                    }
                }

            }
        }
    }
}
