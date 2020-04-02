using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Drawing;
using System.Drawing.Printing;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
   public class PublicCommon
    {
        string FilePath = @".\Common.Xml";
        private XmlDocument XmlDoc = new XmlDocument();
        StringFormat sf ;
        public static string str_print = "0";
        public static string str_ShowCursor = "";
        public static string akb021 = "";
        public static string akb026 = "";
        public static string aab299 = "";
        public static string yab600 = "";
        public PublicCommon()
        {
            readXML();
        }
       
        //厘米转英寸
        public  int getyc(double d)
        {
            double b = Convert.ToDouble(d / 2.54);
            int a = (int)Math.Round(b * 100, 0);
            return a;
        }
        public string getHospitalName()
        {

            string StrNode = "";
            string StrHospitalName = "";
            XmlNodeReader reader = null;
            XmlDoc.Load(FilePath);
            // 设定XmlNodeReader对象来打开XML文件
            reader = new XmlNodeReader(XmlDoc);
            // 读取XML文件中的数据，并显示出来
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        StrNode = reader.Name;
                        break;
                    case XmlNodeType.Text:
                        if (StrNode.Equals("hospitalName"))
                        {
                            StrHospitalName = reader.Value;
                        }

                        break;
                }
            }
            return Decodebase64(StrHospitalName);

        }
        public void readXML()
        {

            string StrNode = "";

            XmlNodeReader reader = null;
            XmlDoc.Load(FilePath);
            // 设定XmlNodeReader对象来打开XML文件
            reader = new XmlNodeReader(XmlDoc);
            // 读取XML文件中的数据，并显示出来
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        StrNode = reader.Name;
                        break;
                    case XmlNodeType.Text:
                        if (StrNode.Equals("akb021"))
                        {
                            akb021 = reader.Value;
                        }
                        else if (StrNode.Equals("akb026"))
                        {

                            akb026 = reader.Value;
                        }
                        else if (StrNode.Equals("aab299"))
                        {

                            aab299 = reader.Value;
                        }
                        else if (StrNode.Equals("yab600"))
                        {

                            yab600 = reader.Value;
                        }
                        else if (StrNode.Equals("print"))
                        {

                            str_print = reader.Value;
                        }
                        else if (StrNode.Equals("ShowCursor"))
                        {

                            str_ShowCursor = reader.Value;
                        }

                        break;
                }
            }


        }
        private string Encodebase64(string code)
        {

            string encode = "";
            byte[] bytes = Encoding.Default.GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
        private string Decodebase64(string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.Default.GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
        public void DrawString(string str,Font objFont, Rectangle rectangle, PrintPageEventArgs e)
        {
            sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            sf.LineAlignment = StringAlignment.Center;

          //  e.Graphics.DrawString(str, objFont, Brushes.Black, new Rectangle(0, 20, 827, 40), sf);
            e.Graphics.DrawString(str, objFont, Brushes.Black, rectangle, sf);


        }
        //字符水平居中对齐
        public void DrawStringCenterX(PrintPageEventArgs e, string content, Font font, Brush brush, double x_begin, double x_end, double y_begin)
        {
            e.Graphics.DrawString(content, font, brush,
                (int)(this.getyc(x_begin) + this.getyc(x_end) - e.Graphics.MeasureString(content, font).Width) / 2, this.getyc(y_begin));

        }
        //字符水平左对齐
        public void DrawStringLeftX(PrintPageEventArgs e, string content, Font font, Brush brush, double x_begin, double y_begin)
        {
            e.Graphics.DrawString(content, font, brush, this.getyc(x_begin), this.getyc(y_begin));

        }
        //字符水平右对齐
        public void DrawStringRightX(PrintPageEventArgs e, string content, Font font, Brush brush, double x_end, double y_begin)
        {
            e.Graphics.DrawString(content, font, brush, this.getyc(x_end) - e.Graphics.MeasureString(content, font).Width, this.getyc(y_begin));

        }
        //划线
        public void DrawLine(PrintPageEventArgs e, Pen objPen, double x_begin, double x_end, double y_begin, double y_end)
        {
            e.Graphics.DrawLine(objPen, this.getyc(x_begin), this.getyc(y_begin), this.getyc(x_end), this.getyc(y_end));

        }

        //字符水平垂直居中对齐
        public void DrawStringCenterXY(PrintPageEventArgs e, string content, Font font, Brush brush, double x_begin, double x_end, double y_begin, double y_end)
        {
            e.Graphics.DrawString(content, font, brush,
                (int)(this.getyc(x_begin) + this.getyc(x_end) - e.Graphics.MeasureString(content, font).Width) / 2, (int)(this.getyc(y_begin) + this.getyc(y_end) - e.Graphics.MeasureString(content, font).Height) / 2);

        }
        //转换字符格式type=number 返回默认字符0.00， type=shordate 返回yyMMdd,type=longdate 返回yyyyMMdd,空字符返回'-'
        public string formatString(string str_value, string type)
        {

            if (type == "number")
            {
                if (IsNumeric(str_value))
                {
                    return Double.Parse(str_value).ToString("0.00");
                }
                else
                {
                    return "0.00";
                }
            }
            else if (type == "shortdate")
            {
                return DateTime.Parse(str_value).ToString("yyMMdd");
            }
            else if (type == "longdate")
            {
                return DateTime.Parse(str_value).ToString("yyyyMMdd");

            }
            else if (str_value == "")
            {
                return "-";
            }
            else
            {
                return str_value;
            }

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

    }
}
