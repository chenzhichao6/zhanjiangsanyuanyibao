using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1.po;
using System.Drawing.Printing;
using System.Collections;
using System.Text.RegularExpressions;
namespace WindowsFormsApp1
{
    public partial class Form17 : Form
    {
        DBConn db;
        String strSql = "";
        SqlDataAdapter sqlDataAdapter;
        SqlDataReader sqlDataReader;
        DataSet dataSet;
        PublicCommon publicCommon;
        ArrayList arrayList_content;
        ArrayList arrayList_Width;
        string aae140 = "";
        public Form17(string aae140)
        {
            this.aae140 = aae140;
            db = new DBConn();
            publicCommon = new PublicCommon();
            InitializeComponent();

        }

        private void Form17_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime datetime = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);

            strSql = "select count(*)  from ydsb where yzz060='" + dateTimePicker1.Value.ToString("yyyy") + "' and yzz061='" + dateTimePicker1.Value.ToString("MM") + "' and flag=1 and aae140 = '" + aae140 + "'";
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
                strSql = "select transid,yzz060,yzz061,yzz062,aae140 from ydsb where flag=1 and aae140 = '" + aae140 + "' and yzz060='" + dateTimePicker1.Value.ToString("yyyy") + "' and yzz061='" + dateTimePicker1.Value.ToString("MM") + "'";
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
        private void button2_Click(object sender, EventArgs e)
        {
            string yzz062 = "";
            String Str_xml = "";
            strSql = "select yzz062 from ydsb where  yzz060='" + dateTimePicker1.Value.ToString("yyyy") + "' and yzz061='" + dateTimePicker1.Value.ToString("MM") + "' and aae140 = '" + aae140 + "'";
            //   sqlDataReader = new SqlDataReader();
            sqlDataReader = db.GetDataReader(strSql);

            if (sqlDataReader.Read())
            {
                yzz062 = getStringFormat("P440800", dateTimePicker1.Value.ToString("yy") + dateTimePicker1.Value.ToString("MM"), sqlDataReader.GetValue(0).ToString());

            }
            else
            {
                yzz062 = "P440800" + dateTimePicker1.Value.ToString("yy") + dateTimePicker1.Value.ToString("MM") + "0000001";


            }
            if (button2.Text == "提交")
            {
                Str_xml = "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                   + "<input>"
                   + "<aab299>" + PublicCommon.aab299 + "</aab299>"

                   + "<akb026>" + PublicCommon.akb026 + "</akb026>"
                   + "<akb021>" + PublicCommon.akb021 + "</akb021>"
                   + "<yzz060>"+ dateTimePicker1.Value.ToString("yyyy") + "</yzz060>"
                   + "<yzz061>"+ dateTimePicker1.Value.ToString("MM") + "</yzz061>"
                   + "<yzz062>" + yzz062 + "</yzz062>"
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
                        + yzz060 + "','" + yzz061 + "','" + yzz062 + "','" + flag + "','" + aae140 + "')";
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


        private void button3_Click(object sender, EventArgs e)
        {
            PaperSize pkCustomSize = new PaperSize("First Custom Size", publicCommon.getyc(42), publicCommon.getyc(29.7));
            PDoc.DefaultPageSettings.PaperSize = pkCustomSize;
            ((System.Windows.Forms.Form)Pvd).StartPosition = FormStartPosition.CenterScreen;
            ((System.Windows.Forms.Form)Pvd).Width = publicCommon.getyc(42);
            ((System.Windows.Forms.Form)Pvd).Height = publicCommon.getyc(29.7);
            ((System.Windows.Forms.Form)Pvd).Icon = this.Icon;
            Pvd.Document = PDoc;
            if (PublicCommon.str_print != "1")
            {
                Pvd.ShowDialog();
            }
            else
            {
                PDoc.Print();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PaperSize pkCustomSize = new PaperSize("First Custom Size", publicCommon.getyc(42), publicCommon.getyc(29.7));
            PDoc2.DefaultPageSettings.PaperSize = pkCustomSize;
            ((System.Windows.Forms.Form)Pvd).StartPosition = FormStartPosition.CenterScreen;
            ((System.Windows.Forms.Form)Pvd).Width = publicCommon.getyc(42);
            ((System.Windows.Forms.Form)Pvd).Height = publicCommon.getyc(29.7);
            ((System.Windows.Forms.Form)Pvd).Icon = this.Icon;
            Pvd.Document = PDoc2;
            if (PublicCommon.str_print != "1")
            {
                Pvd.ShowDialog();
            }
            else
            {
                PDoc2.Print();
            }
        }

        private ArrayList loadList_Content(DataGridView dv, int n_hj)
        {
            arrayList_content = new ArrayList();
            for (int i = 0; i < dv.Columns.Count; i++)
            {
                arrayList_content.Add(dv.Columns[i].HeaderText);
            }
            if (dv.RowCount != 0)
            {


                for (int i = 0; i < dv.RowCount; i++)
                {
                    for (int j = 0; j < dv.Rows[i].Cells.Count; j++)
                    {

                        string zhi = dv.Rows[i].Cells[j].Value.ToString();
                        if (zhi.Length > 10)
                        {
                            if (dv.Columns[j].Name == "入院诊断" || dv.Columns[j].Name == "出院诊断")
                            {
                                zhi = zhi.Insert(10, "\r\n");
                            }
                        }
                        zhi = qj.pipei(qj.zdvaltokey(dv.Columns[j].Name.ToString()), zhi);

                        arrayList_content.Add(zhi);
                    }
                }
            }

            arrayList_content.Add("合  计");
            for (int j = n_hj; j < dv.ColumnCount; j++)
            {
                arrayList_content.Add(getSum(dv, j));
            }
            return arrayList_content;
        }

        private ArrayList loadLis_Width_MX()
        {
            arrayList_Width = new ArrayList();
            arrayList_Width.Add(1.3);
            arrayList_Width.Add(2.7);
            arrayList_Width.Add(3);
            arrayList_Width.Add(4);
            arrayList_Width.Add(2);
            arrayList_Width.Add(2.5);
            arrayList_Width.Add(2.5);
            arrayList_Width.Add(2.5);
            arrayList_Width.Add(3);
            arrayList_Width.Add(3);
            arrayList_Width.Add(2.5);
            arrayList_Width.Add(2.5);
            arrayList_Width.Add(2.5);
            arrayList_Width.Add(2.5);
            arrayList_Width.Add(2.5);
            arrayList_Width.Add(1);

            return arrayList_Width;
        }
        private ArrayList loadLis_Width_HZ()
        {
            arrayList_Width = new ArrayList();
            arrayList_Width.Add(3);
            arrayList_Width.Add(5);
            arrayList_Width.Add(5);
            arrayList_Width.Add(5);
            arrayList_Width.Add(5);
            arrayList_Width.Add(5);
            arrayList_Width.Add(5);
            arrayList_Width.Add(5);
            arrayList_Width.Add(2);
            return arrayList_Width;
        }
        private void drawLine(System.Drawing.Printing.PrintPageEventArgs e, Pen objPen, int i_row, ArrayList al_width, int n_hj)
        {
            float row_hight = 0.8F;
            float left = 1.0F;
            float top = 3.6F;
            DrawLine(e, objPen, left, 42 - left, top, top);//横线
            DrawLine(e, objPen, left, left, top, top + (i_row + 2) * row_hight);//竖线
            DrawLine(e, objPen, 42 - left, 42 - left, top, top + (i_row + 2) * row_hight);//竖线
            for (int i = 1; i < i_row + 1; i++)
            {
                DrawLine(e, objPen, left, 42 - left, top + i * row_hight, top + i * row_hight);//横线

            }
            DrawLine(e, objPen, left, 42 - left, top + (i_row + 1) * row_hight, top + (i_row + 1) * row_hight);//横线
            ArrayList al1 = al_width;
            float x = left;
            for (int i = 0; i < al1.Count; i++)
            {
                x = x + float.Parse(al1[i].ToString());
                DrawLine(e, objPen, x, x, top, top + (i_row + 1) * row_hight);//竖线


            }
            x = left;
            for (int i = 0; i < al1.Count; i++)
            {

                x = x + float.Parse(al1[i].ToString());
                if (i >= n_hj - 1)
                {
                    DrawLine(e, objPen, x, x, top, top + (i_row + 2) * row_hight);//竖线
                }
            }
            DrawLine(e, objPen, left, 42 - left, top + (i_row + 2) * row_hight, top + (i_row + 2) * row_hight);//横线

        }
        private void drawContent(System.Drawing.Printing.PrintPageEventArgs e, Font objFontContent, Brush objBrush, ArrayList al_content, ArrayList al_width, int n_hj)
        {
            float row_hight = 0.8F;
            float left = 1.0F;
            float top = 3.6F;
            float x1 = left;
            float x2 = 0.0F;
            int i_row = 0;
            int j = 0;
            for (int i = 0; i < al_content.Count - al_width.Count + n_hj - 1; i++)
            {

                x2 = x1 + float.Parse(al_width[j++].ToString());
                DrawStringCenterXY(e, al_content[i].ToString(), objFontContent, objBrush, x1, x2, top + i_row * row_hight, top + (i_row + 1) * row_hight);
                x1 = x2;
                if (j == al_width.Count)
                {
                    j = 0;

                    x1 = left;
                    i_row++;

                }

            }
            x1 = left;
            x2 = x1;
            for (int i = i_row * al_width.Count; i < al_content.Count - 1;)
            {

                if (j >= n_hj)
                {
                    x2 = x1 + float.Parse(al_width[j++].ToString());


                    DrawStringCenterXY(e, al_content[i].ToString(), objFontContent, objBrush, x1, x2, top + i_row * row_hight, top + (i_row + 1) * row_hight);
                    i++;
                    x1 = x2;
                    if (j == al_width.Count)
                    {
                        j = 0;
                        x1 = left;
                        i_row++;

                    }
                }
                else
                {
                    x2 = x2 + float.Parse(al_width[j++].ToString());
                    if (j == n_hj)
                    {
                        DrawStringCenterXY(e, al_content[i].ToString(), objFontContent, objBrush, x1, x2, top + i_row * row_hight, top + (i_row + 1) * row_hight);
                        i++;
                        x1 = x2;
                    }

                }

            }

            DrawStringLeftX(e, "负责人：                    复核人：                    填表人：                    填表日期：" + DateTime.Now.ToString("yyyy-MM-dd") + "              联系电话：", objFontContent, objBrush, left, top + (i_row + 2) * row_hight);



        }
        private void PDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font objFontTitle = new Font("方正小标宋简体", 16, FontStyle.Regular);
            Font objFontContent = new Font("宋体", 8, FontStyle.Regular);
            Font objFontElse = new Font("宋体", 8, FontStyle.Regular);
            Brush objBrush = Brushes.Black;
            Pen objPen = new Pen(objBrush);
            objPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            Pen objPen1 = new Pen(objBrush);
            objPen1.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            DrawStringCenterX(e, "广东省医疗保险异地就医医疗费用结算申报明细表(城镇职工基本医疗保险（主险种））", objFontTitle, objBrush, 0, 42, 0.6);
            DrawStringCenterX(e, "（医疗机构用表）", objFontTitle, objBrush, 0, 42, 1.5);
            DrawStringLeftX(e, "申报结算日期：" + dateTimePicker1.Value.ToString("yyyy年MM月") + "01日   至   " + DateTime.Parse(dateTimePicker1.Value.AddMonths(1).ToString("yyyy年MM月") + "01日").AddDays(-1).ToString("yyyy年MM月dd日"), objFontContent, objBrush, 1, 2.3);
            DrawStringLeftX(e, "填报单位：" + PublicCommon.akb021 + "      所属地市：湛江市   业务交接号：" + dataGridView1.Rows[0].Cells[0].Value.ToString(), objFontContent, objBrush, 1, 3);
            DrawStringRightX(e, "金额单位：元", objFontContent, objBrush, 41, 3);

            ArrayList al2 = loadLis_Width_MX();
            ArrayList al1 = loadList_Content(dataGridView2, 10);
            drawLine(e, objPen, dataGridView2.RowCount, al2, 10);
            drawContent(e, objFontContent, objBrush, al1, al2, 10);

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
        private String getSum(DataGridView dv, int col_index)
        {
            Double d_sum = 0;
            if (!IsNumeric(dv.Rows[0].Cells[col_index].Value.ToString()))
            {
                return "";
            }
            else
            {
                for (int i = 0; i < dv.RowCount; i++)
                {
                    if (dv.Columns[col_index].Name == "医疗类别") return "";
                    d_sum = d_sum + Double.Parse(dv.Rows[i].Cells[col_index].Value.ToString());
                }
                return d_sum.ToString();
            }
        }
        //字符水平居中对齐
        public void DrawStringCenterX(PrintPageEventArgs e, string content, Font font, Brush brush, double x_begin, double x_end, double y_begin)
        {
            e.Graphics.DrawString(content, font, brush,
                (int)(publicCommon.getyc(x_begin) + publicCommon.getyc(x_end) - e.Graphics.MeasureString(content, font).Width) / 2, publicCommon.getyc(y_begin));

        }
        //字符水平左对齐
        public void DrawStringLeftX(PrintPageEventArgs e, string content, Font font, Brush brush, double x_begin, double y_begin)
        {
            e.Graphics.DrawString(content, font, brush, publicCommon.getyc(x_begin), publicCommon.getyc(y_begin));

        }
        //字符水平右对齐
        public void DrawStringRightX(PrintPageEventArgs e, string content, Font font, Brush brush, double x_end, double y_begin)
        {
            e.Graphics.DrawString(content, font, brush, publicCommon.getyc(x_end) - e.Graphics.MeasureString(content, font).Width, publicCommon.getyc(y_begin));

        }
        //划线
        public void DrawLine(PrintPageEventArgs e, Pen objPen, double x_begin, double x_end, double y_begin, double y_end)
        {
            e.Graphics.DrawLine(objPen, publicCommon.getyc(x_begin), publicCommon.getyc(y_begin), publicCommon.getyc(x_end), publicCommon.getyc(y_end));

        }

        //字符水平垂直居中对齐
        public void DrawStringCenterXY(PrintPageEventArgs e, string content, Font font, Brush brush, double x_begin, double x_end, double y_begin, double y_end)
        {
            e.Graphics.DrawString(content, font, brush,
                (int)(publicCommon.getyc(x_begin) + publicCommon.getyc(x_end) - e.Graphics.MeasureString(content, font).Width) / 2, (int)(publicCommon.getyc(y_begin) + publicCommon.getyc(y_end) - e.Graphics.MeasureString(content, font).Height) / 2);

        }

        private void PDoc2_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font objFontTitle = new Font("方正小标宋简体", 16, FontStyle.Regular);
            Font objFontContent = new Font("宋体", 11, FontStyle.Regular);
            Font objFontElse = new Font("宋体", 9, FontStyle.Regular);
            Brush objBrush = Brushes.Black;
            Pen objPen = new Pen(objBrush);
            objPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            Pen objPen1 = new Pen(objBrush);
            objPen1.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            DrawStringCenterX(e, "广东省医疗保险异地就医医疗费用结算申报汇总表(城镇职工基本医疗保险（主险种））", objFontTitle, objBrush, 0, 42, 0.6);
            DrawStringCenterX(e, "（医疗机构用表）", objFontTitle, objBrush, 0, 42, 1.5);
            DrawStringLeftX(e, "申报结算日期：" + dateTimePicker1.Value.ToString("yyyy年MM月") + "01日   至   " + DateTime.Parse(dateTimePicker1.Value.AddMonths(1).ToString("yyyy年MM月") + "01日").AddDays(-1).ToString("yyyy年MM月dd日"), objFontContent, objBrush, 1, 2.3);
            DrawStringLeftX(e, "填报单位：" + PublicCommon.akb021 + "      所属地市：湛江市   业务交接号：" + dataGridView1.Rows[0].Cells[0].Value.ToString(), objFontContent, objBrush, 1, 3);
            DrawStringRightX(e, "金额单位：元", objFontContent, objBrush, 41, 3);

            ArrayList al2 = loadLis_Width_HZ();
            ArrayList al1 = loadList_Content(dataGridView3, 2);
            drawLine(e, objPen, dataGridView3.RowCount, al2, 2);
            drawContent(e, objFontContent, objBrush, al1, al2, 2);

        }
        public static bool IsNumeric(string value)
        {
            if (value == "")
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }
    }
}
