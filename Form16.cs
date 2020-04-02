using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;

namespace WindowsFormsApp1
{
    public partial class Form16 : Form
    {
        PublicCommon publicCommon;
        public double i_rowHeight = 0.8;
        public int i_rowNum = 0;
        public double i_columnWidth = 2;
        public int i_columnNum = 0;
        public Form16()
        {
            publicCommon = new PublicCommon();
            InitializeComponent();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
            DrawStringCenterX(e, "广东省医疗保险异地就医医疗费用结算单", objFontTitle,objBrush, 0, 21, 1);
            DrawStringLeftX(e, "医疗机构名称：湛江市赤坎区中医医院", objFontContent, objBrush, 1, 2.2);
            DrawStringLeftX(e, "医院等级：二级", objFontContent, objBrush, 10, 2.2);
            DrawStringLeftX(e, "医疗机构编码：4400001159975", objFontContent, objBrush, 1, 2.8);
            DrawStringLeftX(e, "就诊序列号：A4406001904190105302", objFontContent, objBrush, 10, 2.8);
            DrawStringRightX(e, "金额单位：元", objFontContent, objBrush, 20, 2.8);

            #region 画边框
            DrawLine(e, objPen, 0.8, 20.2, 3.6, 3.6);//上横
            DrawLine(e, objPen, 0.8, 0.8, 3.6, 20);//左竖
            DrawLine(e, objPen, 20.2, 20.2, 3.6, 20);//右竖
            DrawLine(e, objPen, 0.8, 20.2, 20, 20);//下横
            #endregion
            #region 画内横线

            DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//二横
            DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//三横
            DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//四横
            DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//五横
            DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//六横
            DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//七横
            DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//八横
            DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//九横
            DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//十横
            DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//十一横
            DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//十二横
            i_rowNum++;
            i_rowNum++;
            DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum++ * i_rowHeight);//十三横
            i_rowNum++;
            i_rowNum++;
            DrawLine(e, objPen, 0.8, 20.2, 3.6 + i_rowNum * i_rowHeight, 3.6 + i_rowNum * i_rowHeight);//十四横

            #endregion
            #region 画内竖线
            DrawLine(e, objPen, 3.6, 3.6, 3.6, 3.6 + 11 * i_rowHeight);//二竖
            DrawLine(e, objPen, 8.2, 8.2, 3.6, 3.6 + 7 * i_rowHeight);//二竖
            DrawLine(e, objPen, 10.6, 10.6, 3.6, 3.6 + 11 * i_rowHeight);//二竖
            DrawLine(e, objPen, 14.4, 14.4, 3.6, 3.6 + 3 * i_rowHeight);//二竖
            DrawLine(e, objPen, 17, 17, 3.6, 3.6 + 3 * i_rowHeight);//二竖
            DrawLine(e, objPen, 5.8, 5.8, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);//二竖
            DrawLine(e, objPen, 5.8, 5.8, 3.6 + 6 * i_rowHeight, 3.6 + 7 * i_rowHeight);//二竖
            DrawLine(e, objPen, 12.2, 12.2, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);//二竖
            DrawLine(e, objPen, 12.2, 12.2, 3.6 + 6 * i_rowHeight, 3.6 + 7 * i_rowHeight);//二竖
            DrawLine(e, objPen, 14.3, 14.3, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);//二竖
            DrawLine(e, objPen, 7.3, 7.3, 3.6 + 7 * i_rowHeight, 3.6 + 11 * i_rowHeight);//二竖
            DrawLine(e, objPen, 13.4, 13.4, 3.6 + 7 * i_rowHeight, 3.6 + 11 * i_rowHeight);//二竖
            DrawLine(e, objPen, 17, 17, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);//二竖
            DrawLine(e, objPen, 17, 17, 3.6 + 7 * i_rowHeight, 3.6 + 11 * i_rowHeight);//二竖
            DrawLine(e, objPen, 14.8, 14.8, 3.6 + 9 * i_rowHeight, 3.6 + 11 * i_rowHeight);//二竖

            #endregion
            #region 填充表格名称
            DrawStringCenterXY(e, "姓名", objFontElse, objBrush, 0.8, 3.6, 3.6, 3.6 + 1 * i_rowHeight);
            DrawStringCenterXY(e, "性别", objFontElse, objBrush, 8.2, 10.6, 3.6, 3.6 + 1 * i_rowHeight);
            DrawStringCenterXY(e, "性别", objFontElse, objBrush, 14.4, 17, 3.6, 3.6 + 1 * i_rowHeight);
            DrawStringCenterXY(e, "异地就医申请号", objFontElse, objBrush, 0.8, 3.6, 3.6 + 1 * i_rowHeight, 3.6 + 2 * i_rowHeight);
            DrawStringCenterXY(e, "社会保障号码", objFontElse, objBrush, 8.2, 10.6, 3.6 + 1 * i_rowHeight, 3.6 + 2 * i_rowHeight);
            DrawStringCenterXY(e, "居民身份证号码", objFontElse, objBrush, 14.4,17, 3.6 + 1 * i_rowHeight, 3.6 + 2 * i_rowHeight);
            DrawStringCenterXY(e, "参保地", objFontElse, objBrush, 0.8, 3.6, 3.6 + 2 * i_rowHeight, 3.6 + 3 * i_rowHeight);
            DrawStringCenterXY(e, "医疗保险类型", objFontElse, objBrush, 8.2,10.6, 3.6 + 2 * i_rowHeight, 3.6 + 3 * i_rowHeight);
            DrawStringCenterXY(e, "人员类别", objFontElse, objBrush, 14.4, 17, 3.6 + 2 * i_rowHeight, 3.6 + 3 * i_rowHeight);
            DrawStringCenterXY(e, "就医地", objFontElse, objBrush, 0.8, 3.6, 3.6 + 3 * i_rowHeight, 3.6 + 4 * i_rowHeight);
            DrawStringCenterXY(e, "单位名称", objFontElse, objBrush, 8.2, 10.6, 3.6 + 3 * i_rowHeight, 3.6 + 4 * i_rowHeight);
            DrawStringCenterXY(e, "住院号", objFontElse, objBrush, 0.8, 3.6, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);
            DrawStringCenterXY(e, "科别", objFontElse, objBrush, 5.8, 8.2, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);
            DrawStringCenterXY(e, "入院时间", objFontElse, objBrush, 10.6, 12.2, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);
            DrawStringCenterXY(e, "出院时间", objFontElse, objBrush, 14.4, 17, 3.6 + 4 * i_rowHeight, 3.6 + 5 * i_rowHeight);
            DrawStringCenterXY(e, "入院第一诊断", objFontElse, objBrush, 0.8, 3.6, 3.6 + 5 * i_rowHeight, 3.6 + 6 * i_rowHeight);
            DrawStringCenterXY(e, "出院第一诊断", objFontElse, objBrush, 8.2, 10.6, 3.6 + 5 * i_rowHeight, 3.6 + 6 * i_rowHeight);
            DrawStringCenterXY(e, "就诊类别", objFontElse, objBrush, 0.8, 3.6, 3.6 + 6 * i_rowHeight, 3.6 + 7 * i_rowHeight);
            DrawStringCenterXY(e, "结算时间", objFontElse, objBrush, 5.8, 8.2, 3.6 + 6 * i_rowHeight, 3.6 + 7 * i_rowHeight);
            DrawStringCenterXY(e, "住院天数", objFontElse, objBrush, 10.6, 12.2, 3.6 + 6 * i_rowHeight, 3.6 + 7 * i_rowHeight);
            DrawStringCenterXY(e, "医疗费用总金额", objFontElse, objBrush, 0.8, 3.6, 3.6 + 7 * i_rowHeight, 3.6 + 8 * i_rowHeight);
            DrawStringCenterXY(e, "自费金额", objFontElse, objBrush, 3.6, 7.3, 3.6 + 7 * i_rowHeight, 3.6 + 8 * i_rowHeight);
            DrawStringCenterXY(e, "部分项目自付金额", objFontElse, objBrush, 7.3, 10.6, 3.6 + 7 * i_rowHeight, 3.6 + 8 * i_rowHeight);
            DrawStringCenterXY(e, "超限额以上费用", objFontElse, objBrush, 10.6, 13.4, 3.6 + 7 * i_rowHeight, 3.6 + 8 * i_rowHeight);
            DrawStringCenterXY(e, "进入结算费用金额", objFontElse, objBrush, 13.4, 17, 3.6 + 7 * i_rowHeight, 3.6 + 8 * i_rowHeight);
            DrawStringCenterXY(e, "本次起付标准", objFontElse, objBrush, 17, 20.2, 3.6 + 7 * i_rowHeight, 3.6 + 8 * i_rowHeight);
            DrawStringLeftX(e, "基本统筹基金支\n付费用", objFontElse, objBrush, 0.9, 3.7 + 9 * i_rowHeight);
            DrawStringLeftX(e, "重大疾病/大病保险支\n付费用", objFontElse, objBrush, 3.7, 3.7 + 9 * i_rowHeight);
            DrawStringLeftX(e, "补充/补助保险等支\n付费用", objFontElse, objBrush, 7.4, 3.7 + 9 * i_rowHeight);
            DrawStringLeftX(e, "公务员补助支付\n费用", objFontElse, objBrush, 10.6, 3.7 + 9 * i_rowHeight);
            DrawStringCenterXY(e, "其他", objFontElse, objBrush, 13.4, 14.8, 3.6 + 9 * i_rowHeight, 3.6 + 10 * i_rowHeight);
            DrawStringCenterXY(e, "记账费用", objFontElse, objBrush, 14.8, 17, 3.6 + 9 * i_rowHeight, 3.6 + 10 * i_rowHeight);
            DrawStringCenterXY(e, "个人自负费用", objFontElse, objBrush, 17, 20.2, 3.6 + 9 * i_rowHeight, 3.6 + 10 * i_rowHeight);
            DrawStringLeftX(e, "本次住院：", objFontElse, objBrush, 0.9, 3.7 + 11 * i_rowHeight);
            DrawStringLeftX(e, "本社保年度累计支付：", objFontElse, objBrush, 0.9, 3.7 + 14 * i_rowHeight);
            DrawStringLeftX(e, "备注：", objFontElse, objBrush, 0.9, 3.7 + 17 * i_rowHeight);
            DrawStringLeftX(e, "经办人员：", objFontElse, objBrush, 1.6, 20.2);
            DrawStringLeftX(e, "审核人员：", objFontElse, objBrush, 5.6, 20.2);
            DrawStringLeftX(e, "患者（家属）签字：", objFontElse, objBrush, 10, 20.2);
            DrawStringLeftX(e, "联系电话：", objFontElse, objBrush, 15.5, 20.2);
            #endregion
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PaperSize pkCustomSize = new PaperSize("First Custom Size", publicCommon.getyc(21), publicCommon.getyc(29.5));
            printDocument1.DefaultPageSettings.PaperSize = pkCustomSize;
            ((System.Windows.Forms.Form)Pvd).StartPosition = FormStartPosition.CenterScreen;
            ((System.Windows.Forms.Form)Pvd).Width = publicCommon.getyc(21);
            ((System.Windows.Forms.Form)Pvd).Height = publicCommon.getyc(29.5);
            ((System.Windows.Forms.Form)Pvd).Icon = this.Icon;
            Pvd.Document = printDocument1;
            if (PublicCommon.str_print != "1")
            {
                Pvd.ShowDialog();
            }
            else
            {
                printDocument1.Print();
            }
        }

        private void Pvd_Load(object sender, EventArgs e)
        {

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
        public void DrawLine(PrintPageEventArgs e,Pen objPen,double x_begin, double x_end, double y_begin, double y_end)
        {
            e.Graphics.DrawLine(objPen,publicCommon.getyc(x_begin), publicCommon.getyc(y_begin), publicCommon.getyc(x_end), publicCommon.getyc(y_end));

        }

        //字符水平垂直居中对齐
        public void DrawStringCenterXY(PrintPageEventArgs e, string content, Font font, Brush brush, double x_begin, double x_end, double y_begin,double y_end)
        {
            e.Graphics.DrawString(content, font, brush,
                (int)(publicCommon.getyc(x_begin) + publicCommon.getyc(x_end) - e.Graphics.MeasureString(content, font).Width) / 2, (int)(publicCommon.getyc(y_begin) + publicCommon.getyc(y_end) - e.Graphics.MeasureString(content, font).Height) / 2);

        }
        private void Form16_Load(object sender, EventArgs e)
        {

        }
    }
}
