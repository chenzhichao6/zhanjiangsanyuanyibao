using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WindowsFormsApp1.po;


namespace WindowsFormsApp1
{
    public partial class Form14 : Form
    {
        PublicCommon publicCommon = null;
        Sfyz s = null;
        string str_id;
        gxjzdj gx = null;
        public double i_rowHeight = 0.8;
        public int i_rowNum = 0;
        public double i_columnWidth = 2;
        public int i_columnNum = 0;
        string strSql = "";
        SqlDataReader sqlDataReader;
        DBConn db;
        string Str_xml;
        public Form14(Sfyz s,string id)
        {
            db = new DBConn();
            this.s = s;
            str_id = id;
            init();
            InitializeComponent();
            textBox1.Text = gx.akc190;
        }

        public void init()
        {
            DBConn db = new DBConn();
            string sql = "select * from gxjzdj where aac044 = '" + s.aac044 + "' and id='"+str_id+"'";
            DataTable dt = db.GetDataSet(sql).Tables[0];
            gx = new gxjzdj();
            var pros = gx.GetType().GetProperties();
            foreach (var p in pros)
            {
                p.SetValue(gx, dt.Rows[0][p.Name].ToString(), null);
            }


            publicCommon = new PublicCommon();

        }

        fyjs fj = null;
        private void Form14_Load(object sender, EventArgs e)
        {
            button1.Text = "";
            foreach (var q in qj.ykc675)
            {
                comboBox1.Items.Add(q.Value);
            }
            comboBox1.SelectedIndex = 0;

            DBConn db = new DBConn();

            string sql = "select * from fyjs where aac044 = '" + gx.aac044 + "' and id="+str_id;

            DataTable dt = db.GetDataSet(sql).Tables[0];
            if (dt.Rows.Count < 1) { button1.Text = "提交"; return; }
            fj = new fyjs();
            var pros = fj.GetType().GetProperties();
            foreach (var p in pros)
            {
                p.SetValue(fj, dt.Rows[0][p.Name].ToString(), null);
            }
            Addkj(fj);
            button1.Text = "打印结算单";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "打印结算单")
            {
                PaperSize pkCustomSize = new PaperSize("First Custom Size", publicCommon.getyc(21), publicCommon.getyc(29.5));
                PDoc.DefaultPageSettings.PaperSize = pkCustomSize;
                ((System.Windows.Forms.Form)Pvd).StartPosition = FormStartPosition.CenterScreen;
                ((System.Windows.Forms.Form)Pvd).Width = publicCommon.getyc(21);
                ((System.Windows.Forms.Form)Pvd).Height = publicCommon.getyc(29.5);
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

                return;
            }
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("请输入医院发票号");
                return;
            }
            hisDBConn hdb = new hisDBConn();
            string zyhsql = "select zyh from ZY_BRRY where ZYHM = '" + gx.akc190 + "'";
            DataTable dt1 = hdb.GetDataSet(zyhsql).Tables[0];
            string zyh = dt1.Rows[0]["zyh"].ToString();

            string ykc675 = (from r in qj.ykc675 where comboBox1.SelectedItem.ToString() == r.Value select r.Key).ToList<string>()[0];
            string asql = "select SUM(zjje) as 'akc264' from ZY_JSMX where ZYH = '" + zyh + "'";
            string akc264 = hdb.GetDataSet(asql).Tables[0].Rows[0][0].ToString();
            string resultxml = qj.cscf("0304", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
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
                + "<aae011>" + gx.aae011 + "</aae011>"
                + "<aae036>" + DateTime.Now.ToString("yyyyMMdd") + "</aae036>"
                + "<akc194>" + dateTimePicker1.Text.Trim() + "</akc194>"
                + "<yzz021>" + textBox2.Text.Trim() + "</yzz021>"
                + "<aae013>" + textBox3.Text.Trim() + "</aae013>"
                + "</input>");

            if (resultxml == "") { return; }

            fyjs fj = new fyjs();
            fj = (fyjs)qj.rxml(fj, resultxml, null, null, null);
            if (fj == null) { return; }
            fj.aac044 = gx.aac044;
            fj.aae036 = DateTime.Now.ToString("yyyyMMdd");
            fj.yzz021 = textBox2.Text.Trim();
            string sql = qj.getSql(fj, fj.GetType().Name,str_id,null);
            DBConn db = new DBConn();
            int i = db.GetSqlCmd(sql);
            sql = "update fyjs set akc194='"+dateTimePicker1.Value.ToString("yyyy-MM-dd")+"' where id="+str_id;
            db.GetSqlCmd(sql);
            if (fj != null)
            {
                qj.gxStatus(4, gx.aac044, str_id);
                Addkj(fj);
                PaperSize pkCustomSize = new PaperSize("First Custom Size", publicCommon.getyc(21), publicCommon.getyc(29.5));
                PDoc.DefaultPageSettings.PaperSize = pkCustomSize;
                ((System.Windows.Forms.Form)Pvd).StartPosition = FormStartPosition.CenterScreen;
                ((System.Windows.Forms.Form)Pvd).Width = publicCommon.getyc(21);
                ((System.Windows.Forms.Form)Pvd).Height = publicCommon.getyc(29.5);
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

        }
        //获取打印内容
        public DataTable getPrintValue(string id)
        {
            string strSql = "select aac003 as 姓名,"
                + "case when aac004 = 2 then '女' else '男' end as 性别,"
                + "aac006 as 出生日期,GXJZDJ.ykc700 as 申请号,SFSB.aac002 as 保障号码,"
                + "SFSB.aac002 as 身份证号码,SFSB.aab301 as 参保地,AAE140 as 保险类型,"
                + "YKC021 as 人员类别,'湛江市' as 就医地,aab004 as 单位名称, akc190 as 住院号,"
                + "akf001 as 入院科别,GXJZDJ.aae036 as 入院时间,cydj.ykc702 as 出院时间,"
                + "akc050 as 入院第一诊断,akc185 as 出院第一诊断,GXJZDJ.AKA130 as 就诊类别,"
                + "fyjs.aae036 as 结算时间,CYDJ.akb063 as 住院天数,akc264 as 总金额,"
                + "akc253 as 自费金额,akc254 as 部分项目自付金额,ykc631 as 超限额以上费用,"
                + "yka319 as 进入结算费用,aka151 as 起付线,ake039 as 基本医疗统筹,"
                + "ykc630 as 大病医疗统筹,ykc637 as 补充医疗统筹,"
                + "ake035 + ykc635 + ykc636 as 公务员统筹,"
                + "ykc639 as 其他,akb068 as 记账费用,yzz139 as 个人自负费用,"
                + "fyjs.akc200 as 年度住院次数,yka430 as 累计统筹支付,"
                + "yka433 as 大病累计,yka431 as 补充累计,yka432 as 公务员累计,yka434 as 其他累计 "
                + " from sfsb, fyjs, cydj, GXJZDJ"
                + " where SFSB.ID = FYJS.ID AND SFSB.ID = CYDJ.ID AND SFSB.ID = GXJZDJ.ID"
                + " and SFSB.ID=" + id;
            DBConn db = new DBConn();
            DataSet dataSet = db.GetDataSet(strSql);
            return dataSet.Tables[0];

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

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "select * from fyjs where aac044 = '" + gx.aac044 + "' and id = " +str_id;
            DBConn db = new DBConn();
            DataTable dt = db.GetDataSet(sql).Tables[0];

            fyjs fj = new fyjs();
            var pros = fj.GetType().GetProperties();
            foreach (var p in pros)
            {
                p.SetValue(fj, dt.Rows[0][p.Name].ToString(), null);
            }

            string resultxml = qj.cscf("0305", "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                + "<input>"
                //+ "<transid>4408001908301000106898</transid>"
                + "<otransid>" + fj.transid + "</otransid>"
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
                + "<ykc618>" + fj.ykc618 + "</ykc618>"
                + "<aae011>" + gx.aae011 + "</aae011>"
                + "<aae036>" + DateTime.Now.ToString("yyyyMMdd") + "</aae036>"
                + "<yzz021>"+fj.yzz021+"</yzz021>"
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
                string sql1 = "delete fyjs where aac044 = '" + gx.aac044 + "' and id = "+str_id;
                db.GetSqlCmd(sql1);
                qj.gxStatus(3, gx.aac044, str_id);
                MessageBox.Show("回退成功！");
            }
        }

        private void PDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            PrintBc4250(sender, e);
        }


        private void PrintBc4250(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
            i_columnNum = 1;
            i_rowNum = 1;

            Font objFontTitle = new Font("方正小标宋简体", 22, FontStyle.Regular);
            Font objFontContent = new Font("宋体", 11, FontStyle.Regular);
            Font objFontElse = new Font("宋体", 9, FontStyle.Regular);
            Brush objBrush = Brushes.Black;
            Pen objPen = new Pen(objBrush);
            objPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            Pen objPen1 = new Pen(objBrush);
            objPen1.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            publicCommon.DrawStringCenterX(e, "广东省医疗保险异地就医医疗费用结算单", objFontTitle, objBrush, 0, 21, 1);
            publicCommon.DrawStringLeftX(e, "医疗机构名称："+PublicCommon.akb021, objFontContent, objBrush, 1, 2.2);
            publicCommon.DrawStringLeftX(e, "医院等级：二级", objFontContent, objBrush, 10, 2.2);
            publicCommon.DrawStringLeftX(e, "医疗机构编码："+PublicCommon.akb026, objFontContent, objBrush, 1, 2.8);
            publicCommon.DrawStringLeftX(e, "就诊序列号："+gx.ykc700, objFontContent, objBrush, 10, 2.8);
            publicCommon.DrawStringRightX(e, "金额单位：元", objFontContent, objBrush, 20, 2.8);

            #region 画边框
            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 3.6, 3.6);//上横
            publicCommon.DrawLine(e, objPen, 0.8, 0.8, 3.6, 20);//左竖
            publicCommon.DrawLine(e, objPen, 20.2, 20.2, 3.6, 20);//右竖
            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 20, 20);//下横
            #endregion
            #region 画内横线

            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//二横
            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//三横
            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//四横
            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//五横
            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//六横
            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//七横
            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//八横
            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//九横
            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//十横
            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//十一横
            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//十二横
            i_rowNum++;
            i_rowNum++;
            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//十三横
            i_rowNum++;
            i_rowNum++;
            publicCommon.DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum * i_rowHeight);//十四横

            #endregion
            #region 画内竖线
            publicCommon.DrawLine(e, objPen, 3.6, 3.6, 3.6, 3.6 + 11 * i_rowHeight);//二竖
            publicCommon.DrawLine(e, objPen, 8.2, 8.2, 3.6, 3.6 + 7 * i_rowHeight);//二竖
            publicCommon.DrawLine(e, objPen, 10.6, 10.6, 3.6, 3.6 + 11 * i_rowHeight);//二竖
            publicCommon.DrawLine(e, objPen, 14.4, 14.4, 3.6, 3.6 + 3 * i_rowHeight);//二竖
            publicCommon.DrawLine(e, objPen, 17, 17, 3.6, 3.6 + 3 * i_rowHeight);//二竖
            publicCommon.DrawLine(e, objPen, 5.8, 5.8, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);//二竖
            publicCommon.DrawLine(e, objPen, 5.8, 5.8, 3.6 + 6 * i_rowHeight, 3.6 + 7 * i_rowHeight);//二竖
            publicCommon.DrawLine(e, objPen, 12.2, 12.2, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);//二竖
            publicCommon.DrawLine(e, objPen, 12.2, 12.2, 3.6 + 6 * i_rowHeight, 3.6 + 7 * i_rowHeight);//二竖
            publicCommon.DrawLine(e, objPen, 14.3, 14.3, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);//二竖
            publicCommon.DrawLine(e, objPen, 7.3, 7.3, 3.6 + 7 * i_rowHeight, 3.6 + 11 * i_rowHeight);//二竖
            publicCommon.DrawLine(e, objPen, 13.4, 13.4, 3.6 + 7 * i_rowHeight, 3.6 + 11 * i_rowHeight);//二竖
            publicCommon.DrawLine(e, objPen, 17, 17, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);//二竖
            publicCommon.DrawLine(e, objPen, 17, 17, 3.6 + 7 * i_rowHeight, 3.6 + 11 * i_rowHeight);//二竖
            publicCommon.DrawLine(e, objPen, 14.8, 14.8, 3.6 + 9 * i_rowHeight, 3.6 + 11 * i_rowHeight);//二竖

            #endregion
            #region 填充表格名称
            publicCommon.DrawStringCenterXY(e, "姓名", objFontElse, objBrush, 0.8, 3.6, 3.6, 3.6 + 1 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "性别", objFontElse, objBrush, 8.2, 10.6, 3.6, 3.6 + 1 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "出生日期", objFontElse, objBrush, 14.4, 17, 3.6, 3.6 + 1 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "异地就医申请号", objFontElse, objBrush, 0.8, 3.6, 3.6 + 1 * i_rowHeight, 3.6 + 2 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "社会保障号码", objFontElse, objBrush, 8.2, 10.6, 3.6 + 1 * i_rowHeight, 3.6 + 2 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "居民身份证号码", objFontElse, objBrush, 14.4, 17, 3.6 + 1 * i_rowHeight, 3.6 + 2 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "参保地", objFontElse, objBrush, 0.8, 3.6, 3.6 + 2 * i_rowHeight, 3.6 + 3 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "医疗保险类型", objFontElse, objBrush, 8.2, 10.6, 3.6 + 2 * i_rowHeight, 3.6 + 3 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "人员类别", objFontElse, objBrush, 14.4, 17, 3.6 + 2 * i_rowHeight, 3.6 + 3 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "就医地", objFontElse, objBrush, 0.8, 3.6, 3.6 + 3 * i_rowHeight, 3.6 + 4 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "单位名称", objFontElse, objBrush, 8.2, 10.6, 3.6 + 3 * i_rowHeight, 3.6 + 4 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "住院号", objFontElse, objBrush, 0.8, 3.6, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "科别", objFontElse, objBrush, 5.8, 8.2, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "入院时间", objFontElse, objBrush, 10.6, 12.2, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "出院时间", objFontElse, objBrush, 14.4, 17, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "入院第一诊断", objFontElse, objBrush, 0.8, 3.6, 3.6 + 5 * i_rowHeight, 3.6 + 6 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "出院第一诊断", objFontElse, objBrush, 8.2, 10.6, 3.6 + 5 * i_rowHeight, 3.6 + 6 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "就诊类别", objFontElse, objBrush, 0.8, 3.6, 3.6 + 6 * i_rowHeight, 3.6 + 7 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "结算时间", objFontElse, objBrush, 5.8, 8.2, 3.6 + 6 * i_rowHeight, 3.6 + 7 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "住院天数", objFontElse, objBrush, 10.6, 12.2, 3.6 + 6 * i_rowHeight, 3.6 + 7 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "医疗费用总金额", objFontElse, objBrush, 0.8, 3.6, 3.6 + 7 * i_rowHeight, 3.6 + 8 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "自费金额", objFontElse, objBrush, 3.6, 7.3, 3.6 + 7 * i_rowHeight, 3.6 + 8 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "部分项目自付金额", objFontElse, objBrush, 7.3, 10.6, 3.6 + 7 * i_rowHeight, 3.6 + 8 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "超限额以上费用", objFontElse, objBrush, 10.6, 13.4, 3.6 + 7 * i_rowHeight, 3.6 + 8 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "进入结算费用金额", objFontElse, objBrush, 13.4, 17, 3.6 + 7 * i_rowHeight, 3.6 + 8 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "本次起付标准", objFontElse, objBrush, 17, 20.2, 3.6 + 7 * i_rowHeight, 3.6 + 8 * i_rowHeight);
            publicCommon.DrawStringLeftX(e, "基本统筹基金支\n付费用", objFontElse, objBrush, 0.9, 3.7 + 9 * i_rowHeight);
            publicCommon.DrawStringLeftX(e, "重大疾病/大病保险支\n付费用", objFontElse, objBrush, 3.7, 3.7 + 9 * i_rowHeight);
            publicCommon.DrawStringLeftX(e, "补充/补助保险等支\n付费用", objFontElse, objBrush, 7.4, 3.7 + 9 * i_rowHeight);
            publicCommon.DrawStringLeftX(e, "公务员补助支付\n费用", objFontElse, objBrush, 10.6, 3.7 + 9 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "其他", objFontElse, objBrush, 13.4, 14.8, 3.6 + 9 * i_rowHeight, 3.6 + 10 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "记账费用", objFontElse, objBrush, 14.8, 17, 3.6 + 9 * i_rowHeight, 3.6 + 10 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, "个人自负费用", objFontElse, objBrush, 17, 20.2, 3.6 + 9 * i_rowHeight, 3.6 + 10 * i_rowHeight);
            publicCommon.DrawStringLeftX(e, "本次住院：", objFontElse, objBrush, 0.9, 3.7 + 11 * i_rowHeight);
            publicCommon.DrawStringLeftX(e, "本社保年度累计支付：", objFontElse, objBrush, 0.9, 3.7 + 14 * i_rowHeight);
            publicCommon.DrawStringLeftX(e, "备注：", objFontElse, objBrush, 0.9, 3.7 + 17 * i_rowHeight);
            publicCommon.DrawStringLeftX(e, "经办人员：", objFontElse, objBrush, 1.6, 20.2);
            publicCommon.DrawStringLeftX(e, "审核人员：", objFontElse, objBrush, 5.6, 20.2);
            publicCommon.DrawStringLeftX(e, "患者（家属）签字：", objFontElse, objBrush, 10, 20.2);
            publicCommon.DrawStringLeftX(e, "联系电话：", objFontElse, objBrush, 15.5, 20.2);
            #endregion
            #region 填充表格内容
            DataTable dataTable = getPrintValue(str_id);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][0].ToString(), objFontElse, objBrush, 3.6, 8.2, 3.6, 3.6 + 1 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][1].ToString(), objFontElse, objBrush, 10.6, 14.4, 3.6, 3.6 + 1 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][2].ToString(), objFontElse, objBrush, 17, 20.2, 3.6, 3.6 + 1 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][3].ToString(), objFontElse, objBrush, 3.6, 8.2, 3.6 + 1 * i_rowHeight, 3.6 + 2 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][4].ToString(), objFontElse, objBrush, 10.6, 14.4, 3.6 + 1 * i_rowHeight, 3.6 + 2 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][5].ToString(), objFontElse, objBrush, 17, 20.2, 3.6 + 1 * i_rowHeight, 3.6 + 2 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e,qj.pipei("tcbm", dataTable.Rows[0][6].ToString()), objFontElse, objBrush, 3.6, 8.2, 3.6 + 2 * i_rowHeight, 3.6 + 3 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, longStringToShort(e, qj.pipei("aae140",dataTable.Rows[0][7].ToString()), objFontElse, (float)3.8), objFontElse, objBrush, 10.6, 14.4, 3.6 + 2 * i_rowHeight, 3.6 + 3 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e,qj.pipei("ykc021", dataTable.Rows[0][8].ToString()), objFontElse, objBrush, 17, 20.2, 3.6 + 2 * i_rowHeight, 3.6 + 3 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][9].ToString(), objFontElse, objBrush, 3.6, 8.2, 3.6 + 3 * i_rowHeight, 3.6 + 4 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][10].ToString(), objFontElse, objBrush, 10.6, 20.2, 3.6 + 3 * i_rowHeight, 3.6 + 4 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][11].ToString(), objFontElse, objBrush, 3.6, 5.8, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, qj.pipei("akf001",dataTable.Rows[0][12].ToString()), objFontElse, objBrush, 8.2, 10.6, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][13].ToString(), objFontElse, objBrush, 12.2, 14.4, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][14].ToString(), objFontElse, objBrush, 17, 20.2, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][15].ToString(), objFontElse, objBrush, 3.6, 8.2, 3.6 + 5 * i_rowHeight, 3.6 + 6 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][16].ToString(), objFontElse, objBrush, 10.6, 20.2, 3.6 + 5 * i_rowHeight, 3.6 + 6 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][17].ToString(), objFontElse, objBrush, 3.6, 5.8, 3.6 + 6 * i_rowHeight, 3.6 + 7 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][18].ToString(), objFontElse, objBrush, 8.2, 10.6, 3.6 + 6 * i_rowHeight, 3.6 + 7 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][19].ToString(), objFontElse, objBrush, 12.2, 20.2, 3.6 + 6 * i_rowHeight, 3.6 + 7 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][20].ToString(), objFontElse, objBrush, 0.8, 3.6, 3.6 + 8 * i_rowHeight, 3.6 + 9* i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][21].ToString(), objFontElse, objBrush, 3.6, 7.3, 3.6 + 8 * i_rowHeight, 3.6 + 9 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][22].ToString(), objFontElse, objBrush, 7.3, 10.6, 3.6 + 8 * i_rowHeight, 3.6 + 9 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][23].ToString(), objFontElse, objBrush, 10.6, 13.4, 3.6 + 8 * i_rowHeight, 3.6 + 9 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][24].ToString(), objFontElse, objBrush, 13.4, 17, 3.6 + 8 * i_rowHeight, 3.6 + 9 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][25].ToString(), objFontElse, objBrush, 17, 20.2, 3.6 + 8 * i_rowHeight, 3.6 + 9 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][26].ToString(), objFontElse, objBrush, 0.8, 3.6, 3.6 + 10 * i_rowHeight, 3.6 + 11 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][27].ToString(), objFontElse, objBrush, 3.6, 7.3, 3.6 + 10 * i_rowHeight, 3.6 + 11 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][28].ToString(), objFontElse, objBrush, 7.3, 10.6, 3.6 + 10 * i_rowHeight, 3.6 + 11 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][29].ToString(), objFontElse, objBrush, 10.6, 13.4, 3.6 + 10 * i_rowHeight, 3.6 + 11 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][30].ToString(), objFontElse, objBrush, 13.4, 14.8, 3.6 + 10 * i_rowHeight, 3.6 + 11 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][31].ToString(), objFontElse, objBrush, 14.8, 17, 3.6 + 10 * i_rowHeight, 3.6 + 11 * i_rowHeight);
            publicCommon.DrawStringCenterXY(e, dataTable.Rows[0][32].ToString(), objFontElse, objBrush, 17, 20.2, 3.6 + 10 * i_rowHeight, 3.6 + 11 * i_rowHeight);
            publicCommon.DrawStringLeftX(e, "           总费用" + dataTable.Rows[0][20].ToString()+"元；记账费用"+ dataTable.Rows[0][31].ToString()+"元；个人自负费用"+ dataTable.Rows[0][32].ToString()+"元。", objFontElse, objBrush, 0.9, 3.0 + 14 * i_rowHeight);

            publicCommon.DrawStringLeftX(e, longStringToShort(e, "已住院" + dataTable.Rows[0][33].ToString()+"次，"
                +"累计统筹支付：" + dataTable.Rows[0][34].ToString()+"元；"
                +"重大疾病/大病保险累计支付" + dataTable.Rows[0][35].ToString()+"元；"
                +"补充/补助保险等累计支付" + dataTable.Rows[0][36].ToString()+"元；"
                +"公务员补助累计支付" + dataTable.Rows[0][37].ToString()+"元；"
                +"其他累计支付" + dataTable.Rows[0][38].ToString()+"元。",objFontElse,(float)19.4), objFontElse, objBrush, 0.9, 3.7 + 15.5 * i_rowHeight);
            //DrawStringLeftX(e, "备注：", objFontElse, objBrush, 0.9, 3.7 + 17 * i_rowHeight);
            //DrawStringLeftX(e, "经办人员：", objFontElse, objBrush, 1.6, 20.2);
            //DrawStringLeftX(e, "审核人员：", objFontElse, objBrush, 5.6, 20.2);
            //DrawStringLeftX(e, "患者（家属）签字：", objFontElse, objBrush, 10, 20.2);
            //DrawStringLeftX(e, "联系电话：", objFontElse, objBrush, 15.5, 20.2);

            #endregion
        }
        public string longStringToShort(PrintPageEventArgs e,string str,Font font,float width)
        {
            float i_width = 0;
            string str_new = "";

            for (int i=0;i<str.Length;i++)
            {
                i_width+= e.Graphics.MeasureString(str[i].ToString(),font).Width;
                if (i_width <= publicCommon.getyc(width))
                {
                    str_new += str[i].ToString();
                }
                else
                {
                    str_new += "\n";
                    str_new += str[i].ToString();
                    i_width = e.Graphics.MeasureString(str[i].ToString(), font).Width;
                }
            }

            return str_new;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string ykc700 = "S4409001905063592557";
                strSql = "select * from tpatientvisit where  FPRN='51019222'";
                sqlDataReader = db.GetDataReader(strSql);
                if (sqlDataReader.Read())
                {
                    Str_xml = "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                      + "<input>"
                      + "<aab299>" + "440800" + "</aab299>"
                      + "<yab600>" + "" + "</yab600>"
                      + "<akb026>" + "4400001159975" + "</akb026>"
                        + "<akb021>" + "湛江市赤坎区中医医院" + "</akb021>"
                       + "<ykc700>" + ykc700 + "</ykc700>"
                       + "<aab301>" + "440900" + "</aab301>"
                       + "<yab060>" + gx.yab060 + "</yab060>"
                       + "<aac002>" + "440923196912170030" + "</aac002>"
                       + "<ake022>" + "440923196912170030" + "</ake022>"
                       + "<aac043>" + "90" + "</aac043>"
                       + "<aac044>" + "440923196912170030" + "</aac044>"
                       + "<yzy001>" + sqlDataReader.GetValue(1).ToString() + "</yzy001>"
                      + "<yzy002>" + sqlDataReader.GetValue(2).ToString() + "</yzy002>"
                      + "<yzy003>" + sqlDataReader.GetValue(3).ToString() + "</yzy003>"
                      + "<yzy004>" + sqlDataReader.GetValue(0).ToString() + "</yzy004>"
                      + "<akc023>" + sqlDataReader.GetValue(5).ToString().Substring(1) + "</akc023>"
                      + "<aac003>" + sqlDataReader.GetValue(6).ToString() + "</aac003>"
                      + "<aac004>" + sqlDataReader.GetValue(7).ToString() + "</aac004>"
                      + "<yzy008>" + sqlDataReader.GetValue(8).ToString() + "</yzy008>"
                      + "<aac006>" + DateTime.Parse(sqlDataReader.GetValue(9).ToString()).ToString("yyyyMMdd") + "</aac006>"
                      + "<yzy010>" + sqlDataReader.GetValue(10).ToString() + "</yzy010>"
                      + "<yzy011>" + sqlDataReader.GetValue(11).ToString() + "</yzy011>"
                      + "<aac161>" + sqlDataReader.GetValue(12).ToString().Substring(1) + "</aac161>"
                      + "<yzy013>" + sqlDataReader.GetValue(13).ToString() + "</yzy013>"
                      + "<aac005>" + sqlDataReader.GetValue(14).ToString() + "</aac005>"
                      + "<yzy015>" + sqlDataReader.GetValue(15).ToString() + "</yzy015>"
                      + "<yzy016>" + sqlDataReader.GetValue(16).ToString() + "</yzy016>"
                      + "<aac017>" + sqlDataReader.GetValue(17).ToString().Substring(0, 1) + "</aac017>"
                      + "<yzy018>" + sqlDataReader.GetValue(18).ToString() + "</yzy018>"

                      + "<aab004>" + sqlDataReader.GetValue(19).ToString() + "</aab004>"
                      + "<yzy020>" + sqlDataReader.GetValue(20).ToString() + "</yzy020>"
                      + "<yzy021>" + sqlDataReader.GetValue(21).ToString() + "</yzy021>"
                      + "<yzy022>" + sqlDataReader.GetValue(22).ToString() + "</yzy022>"
                      + "<aac010>" + sqlDataReader.GetValue(23).ToString() + "</aac010>"
                      + "<yzy024>" + sqlDataReader.GetValue(24).ToString() + "</yzy024>"
                      + "<aae004>" + sqlDataReader.GetValue(25).ToString() + "</aae004>"
                      + "<yzy026>" + sqlDataReader.GetValue(26).ToString() + "</yzy026>"
                      + "<yzy027>" + sqlDataReader.GetValue(27).ToString() + "</yzy027>"
                      + "<yzy028>" + sqlDataReader.GetValue(28).ToString() + "</yzy028>"
                      + "<yzy029>" + sqlDataReader.GetValue(31).ToString() + "</yzy029>"
                      + "<ykc701>" + DateTime.Parse(sqlDataReader.GetValue(33).ToString()).ToString("yyyyMMdd") + "</ykc701>"
                      + "<yzy032>" + sqlDataReader.GetValue(35).ToString() + "</yzy032>"
                      + "<yzy033>" + sqlDataReader.GetValue(36).ToString() + "</yzy033>"
                      + "<yzy034>" + sqlDataReader.GetValue(36).ToString() + "</yzy034>"
                      + "<ykc702>" + DateTime.Parse(sqlDataReader.GetValue(38).ToString()).ToString("yyyyMMdd") + "</ykc702>"
                      + "<yzy037>" + sqlDataReader.GetValue(40).ToString() + "</yzy037>"
                      + "<yzy038>" + sqlDataReader.GetValue(41).ToString() + "</yzy038>"
                      + "<yzy039>" + sqlDataReader.GetValue(41).ToString() + "</yzy039>"
                      + "<akb063>" + sqlDataReader.GetValue(43).ToString() + "</akb063>"
                      + "<akc193>" + sqlDataReader.GetValue(44).ToString() + "</akc193>"
                      + "<akc050>" + sqlDataReader.GetValue(45).ToString() + "</akc050>"
                      + "<yzy043>" + sqlDataReader.GetValue(46).ToString() + "</yzy043>"
                        // + "<ake020>" + sqlDataReader.GetValue(46).ToString() + "</ake020>"
                        + "<yzy045>" + sqlDataReader.GetValue(50).ToString() + "</yzy045>"
                      + "<yzy046>" + sqlDataReader.GetValue(53).ToString() + "</yzy046>"
                      + "<yzy047>" + sqlDataReader.GetValue(71).ToString() + "</yzy047>"
                      + "<yzy048>" + sqlDataReader.GetValue(72).ToString() + "</yzy048>"
                      + "<yzy049>" + sqlDataReader.GetValue(73).ToString() + "</yzy049>"
                      + "<yzy050>" + sqlDataReader.GetValue(74).ToString() + "</yzy050>"
                      + "<yzy051>" + sqlDataReader.GetValue(75).ToString() + "</yzy051>"
                      + "<yzy052>" + sqlDataReader.GetValue(76).ToString() + "</yzy052>"
                      + "<yzy053>" + sqlDataReader.GetValue(77).ToString() + "</yzy053>"
                      + "<yzy054>" + sqlDataReader.GetValue(78).ToString() + "</yzy054>"
                      + "<yzy055>" + sqlDataReader.GetValue(79).ToString() + "</yzy055>"
                      + "<yzy056>" + sqlDataReader.GetValue(80).ToString() + "</yzy056>"
                      + "<yzy057>" + sqlDataReader.GetValue(81).ToString() + "</yzy057>"
                      + "<yzy058>" + sqlDataReader.GetValue(82).ToString() + "</yzy058>"
                      + "<yzy059>" + sqlDataReader.GetValue(85).ToString() + "</yzy059>"
                      + "<yzy060>" + sqlDataReader.GetValue(86).ToString() + "</yzy060>"
                      + "<yzy061>" + sqlDataReader.GetValue(87).ToString() + "</yzy061>"
                      + "<yzy062>" + sqlDataReader.GetValue(88).ToString() + "</yzy062>"
                      + "<yzy063>" + sqlDataReader.GetValue(91).ToString() + "</yzy063>"
                      + "<yzy064>" + sqlDataReader.GetValue(92).ToString() + "</yzy064>"
                      + "<yzy065>" + sqlDataReader.GetValue(93).ToString() + "</yzy065>"
                      + "<yzy066>" + sqlDataReader.GetValue(94).ToString() + "</yzy066>"
                      + "<yzy067>" + publicCommon.formatString(sqlDataReader.GetValue(95).ToString(), "string") + "</yzy067>"
                      + "<yzy068>" + publicCommon.formatString(sqlDataReader.GetValue(96).ToString(), "string") + "</yzy068>"
                      + "<yzy069>" + publicCommon.formatString(sqlDataReader.GetValue(97).ToString(), "longdate") + "</yzy069>"
                      + "<akc264>" + publicCommon.formatString(sqlDataReader.GetValue(100).ToString(), "number") + "</akc264>"
                      + "<ake047>" + publicCommon.formatString(sqlDataReader.GetValue(103).ToString(), "number") + "</ake047>"
                      + "<yzy072>" + publicCommon.formatString(sqlDataReader.GetValue(104).ToString(), "number") + "</yzy072>"
                      + "<ake050>" + publicCommon.formatString(sqlDataReader.GetValue(105).ToString(), "number") + "</ake050>"
                      + "<ake049>" + publicCommon.formatString(sqlDataReader.GetValue(106).ToString(), "number") + "</ake049>"
                      + "<ake044>" + publicCommon.formatString(sqlDataReader.GetValue(118).ToString(), "number") + "</ake044>"
                      + "<yzy076>" + sqlDataReader.GetValue(119).ToString() + "</yzy076>"
                      + "<yzy077>" + sqlDataReader.GetValue(120).ToString() + "</yzy077>"
                      + "<yzy078>" + sqlDataReader.GetValue(134).ToString() + "</yzy078>"
                      + "<yzy079>" + sqlDataReader.GetValue(135).ToString() + "</yzy079>"
                      + "<yzy080>" + sqlDataReader.GetValue(136).ToString() + "</yzy080>"
                      + "<yzy081>" + sqlDataReader.GetValue(137).ToString() + "</yzy081>"
                      + "<yzy082>" + sqlDataReader.GetValue(164).ToString() + "</yzy082>"
                      + "<yzy083>" + sqlDataReader.GetValue(165).ToString() + "</yzy083>"
                      + "<yzy084>" + sqlDataReader.GetValue(166).ToString() + "</yzy084>"
                      + "<yzy085>" + sqlDataReader.GetValue(167).ToString() + "</yzy085>"
                      + "<yzy086>" + sqlDataReader.GetValue(171).ToString() + "</yzy086>"
                      + "<yzy087>" + sqlDataReader.GetValue(172).ToString() + "</yzy087>"
                      + "<yzy088>" + sqlDataReader.GetValue(199).ToString() + "</yzy088>"
                      + "<yzy089>" + sqlDataReader.GetValue(200).ToString() + "</yzy089>"
                      + "<yzy090>" + sqlDataReader.GetValue(201).ToString() + "</yzy090>"
                      + "<yzy091>" + sqlDataReader.GetValue(202).ToString() + "</yzy091>"
                      + "<aca111>" + sqlDataReader.GetValue(203).ToString() + "</aca111>"
                      + "<yzy093>" + sqlDataReader.GetValue(204).ToString() + "</yzy093>"
                      + "<yzy094>" + sqlDataReader.GetValue(205).ToString() + "</yzy094>"
                      + "<yzy095>" + sqlDataReader.GetValue(206).ToString() + "</yzy095>"
                      + "<yzy096>" + sqlDataReader.GetValue(207).ToString() + "</yzy096>"
                      + "<yzy097>" + publicCommon.formatString(sqlDataReader.GetValue(208).ToString(), "string") + "</yzy097>"
                      + "<yzy098>" + publicCommon.formatString(sqlDataReader.GetValue(209).ToString(), "number") + "</yzy098>"
                      + "<yzy099>" + publicCommon.formatString(sqlDataReader.GetValue(210).ToString(), "number") + "</yzy099>"
                      + "<yzy100>" + sqlDataReader.GetValue(211).ToString() + "</yzy100>"
                      + "<yzy101>" + sqlDataReader.GetValue(212).ToString() + "</yzy101>"
                      + "<yzy102>" + sqlDataReader.GetValue(213).ToString() + "</yzy102>"
                      + "<yzy103>" + publicCommon.formatString(sqlDataReader.GetValue(214).ToString(), "string") + "</yzy103>"
                      + "<yzy104>" + publicCommon.formatString(sqlDataReader.GetValue(215).ToString(), "string") + "</yzy104>"
                      + "<yzy105>" + sqlDataReader.GetValue(216).ToString() + "</yzy105>"
                      + "<yzy106>" + sqlDataReader.GetValue(217).ToString() + "</yzy106>"
                      + "<yzy107>" + sqlDataReader.GetValue(218).ToString() + "</yzy107>"
                      + "<yzy108>" + sqlDataReader.GetValue(219).ToString() + "</yzy108>"
                      + "<yzy109>" + publicCommon.formatString(sqlDataReader.GetValue(220).ToString(), "string") + "</yzy109>"
                      + "<yzy110>" + publicCommon.formatString(sqlDataReader.GetValue(221).ToString(), "number") + "</yzy110>"
                      + "<yzy111>" + sqlDataReader.GetValue(222).ToString() + "</yzy111>"
                      + "<yzy112>" + sqlDataReader.GetValue(223).ToString() + "</yzy112>"
                      + "<yzy113>" + sqlDataReader.GetValue(224).ToString() + "</yzy113>"
                      + "<yzy114>" + sqlDataReader.GetValue(225).ToString() + "</yzy114>"
                      + "<yzy115>" + sqlDataReader.GetValue(226).ToString() + "</yzy115>"
                      + "<yzy116>" + sqlDataReader.GetValue(227).ToString() + "</yzy116>"
                      + "<yzy117>" + sqlDataReader.GetValue(228).ToString() + "</yzy117>"
                      + "<yzy118>" + sqlDataReader.GetValue(229).ToString() + "</yzy118>"
                      + "<yzy119>" + sqlDataReader.GetValue(230).ToString() + "</yzy119>"
                      + "<yzy120>" + sqlDataReader.GetValue(231).ToString() + "</yzy120>"
                      + "<yzy121>" + sqlDataReader.GetValue(232).ToString() + "</yzy121>"
                      + "<yzy122>" + publicCommon.formatString(sqlDataReader.GetValue(234).ToString(), "number") + "</yzy122>"
                      + "<yzy123>" + publicCommon.formatString(sqlDataReader.GetValue(235).ToString(), "number") + "</yzy123>"
                      + "<yzy124>" + publicCommon.formatString(sqlDataReader.GetValue(236).ToString(), "number") + "</yzy124>"
                      + "<yzy125>" + publicCommon.formatString(sqlDataReader.GetValue(237).ToString(), "number") + "</yzy125>"
                      // + "<yzy125>" + "100.5" + "</yzy125>"
                      + "<yzy126>" + publicCommon.formatString(sqlDataReader.GetValue(238).ToString(), "number") + "</yzy126>"
                      + "<yzy127>" + publicCommon.formatString(sqlDataReader.GetValue(239).ToString(), "number") + "</yzy127>"
                      + "<yzy128>" + publicCommon.formatString(sqlDataReader.GetValue(240).ToString(), "number") + "</yzy128>"
                      + "<yzy129>" + publicCommon.formatString(sqlDataReader.GetValue(241).ToString(), "number") + "</yzy129>"
                      + "<yzy130>" + publicCommon.formatString(sqlDataReader.GetValue(242).ToString(), "number") + "</yzy130>"
                      + "<yzy131>" + publicCommon.formatString(sqlDataReader.GetValue(243).ToString(), "number") + "</yzy131>"
                      + "<yzy132>" + publicCommon.formatString(sqlDataReader.GetValue(244).ToString(), "number") + "</yzy132>"
                      + "<yzy133>" + publicCommon.formatString(sqlDataReader.GetValue(245).ToString(), "number") + "</yzy133>"
                      + "<yzy134>" + publicCommon.formatString(sqlDataReader.GetValue(246).ToString(), "number") + "</yzy134>"
                      + "<yzy135>" + publicCommon.formatString(sqlDataReader.GetValue(247).ToString(), "number") + "</yzy135>"
                      + "<yzy136>" + publicCommon.formatString(sqlDataReader.GetValue(248).ToString(), "number") + "</yzy136>"
                      + "<yzy137>" + publicCommon.formatString(sqlDataReader.GetValue(249).ToString(), "number") + "</yzy137>"
                      + "<yzy138>" + publicCommon.formatString(sqlDataReader.GetValue(250).ToString(), "number") + "</yzy138>"
                      + "<yzy139>" + publicCommon.formatString(sqlDataReader.GetValue(251).ToString(), "number") + "</yzy139>"
                      + "<yzy140>" + publicCommon.formatString(sqlDataReader.GetValue(252).ToString(), "number") + "</yzy140>"
                      + "<yzy141>" + publicCommon.formatString(sqlDataReader.GetValue(253).ToString(), "number") + "</yzy141>"
                      + "<yzy142>" + publicCommon.formatString(sqlDataReader.GetValue(254).ToString(), "number") + "</yzy142>"
                      + "<yzy143>" + publicCommon.formatString(sqlDataReader.GetValue(255).ToString(), "number") + "</yzy143>"
                      + "<yzy144>" + publicCommon.formatString(sqlDataReader.GetValue(256).ToString(), "number") + "</yzy144>"
                      + "<yzy145>" + publicCommon.formatString(sqlDataReader.GetValue(257).ToString(), "number") + "</yzy145>"
                      + "<yzy146>" + publicCommon.formatString(sqlDataReader.GetValue(258).ToString(), "number") + "</yzy146>"
                      + "<yzy147>" + publicCommon.formatString(sqlDataReader.GetValue(259).ToString(), "number") + "</yzy147>"
                      + "<yzy148>" + publicCommon.formatString(sqlDataReader.GetValue(260).ToString(), "number") + "</yzy148>"
                      + "<yzy149>" + publicCommon.formatString(sqlDataReader.GetValue(261).ToString(), "number") + "</yzy149>"
                      + "<yzy150>" + publicCommon.formatString(sqlDataReader.GetValue(262).ToString(), "number") + "</yzy150>"
                      + "<yzy151>" + publicCommon.formatString(sqlDataReader.GetValue(267).ToString(), "number") + "</yzy151>"
                      + "<yzy152>" + publicCommon.formatString(sqlDataReader.GetValue(268).ToString(), "number") + "</yzy152>"
                      + "<yzy153>" + publicCommon.formatString(sqlDataReader.GetValue(269).ToString(), "number") + "</yzy153>"
                      + "<yzy154>" + publicCommon.formatString(sqlDataReader.GetValue(270).ToString(), "number") + "</yzy154>"
                      + "<yzy155>" + publicCommon.formatString(sqlDataReader.GetValue(271).ToString(), "number") + "</yzy155>"
                      + "<yzy156>" + publicCommon.formatString(sqlDataReader.GetValue(272).ToString(), "number") + "</yzy156>"
                      + "<yzy157>" + publicCommon.formatString(sqlDataReader.GetValue(273).ToString(), "number") + "</yzy157>"
                      + "<yzy158>" + publicCommon.formatString(sqlDataReader.GetValue(274).ToString(), "number") + "</yzy158>"
                      + "<yzy159>" + publicCommon.formatString(sqlDataReader.GetValue(275).ToString(), "number") + "</yzy159>"
                      + "<yzy160>" + "0.0" + "</yzy160>"
                      + "<yzy161>" + "" + "</yzy161>"
                      + "<yzy162>" + "" + "</yzy162>"
                      + "<yzy163>" + "" + "</yzy163>"
                      + "<yzy164>" + "" + "</yzy164>"
                      + "<yzy165>" + "" + "</yzy165>"
                      + "<yzy166>" + "" + "</yzy166>"
                      + "<yzy167>" + "" + "</yzy167>"
                      + "<yzy168>" + "" + "</yzy168>"
                      + "<yzy169>" + "" + "</yzy169>"
                      + "<yzy170>" + "" + "</yzy170>"
                      + "<yzy171>" + "" + "</yzy171>"
                      + "<yzy172>" + "" + "</yzy172>"
                      + "<yzy173>" + "" + "</yzy173>"
                      + "<yzy174>" + "" + "</yzy174>"
                      + "</input>";

                    string resultxml = qj.cscf("0801", Str_xml);
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

                        MessageBox.Show(ykc700 + "病案首页上传成功！");
                    }

                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "操作超时")
                {
                    DialogResult dr1 = MessageBox.Show("操作超时，是否进行超时重发？", "提示", MessageBoxButtons.YesNo);
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}