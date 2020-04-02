using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp1.po;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;
using System.Xml;

namespace WindowsFormsApp1
{


    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {


            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            qj.init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form3());


        }

        static string getStringFormat(string head, string Content, string num)
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

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show(string.Format("捕获到未处理异常：{0}\r\n异常信息：{1}\r\n异常堆栈：{2}\r\nCLR即将退出：{3}", ex.GetType(), ex.Message, ex.StackTrace, e.IsTerminating));
        }
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            MessageBox.Show(string.Format("捕获到未处理异常：{0}\r\n异常信息：{1}\r\n异常堆栈：{2}", ex.GetType(), ex.Message, ex.StackTrace));
        }

    }
}
