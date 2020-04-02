using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp1.po;

namespace WindowsFormsApp1
{
    public partial class Form9 : Form
    {
        string aac044 = "";

        public Form9()
        {
            InitializeComponent();
        }

        // 参数1 身份证  ，，，参数2 类的名称
        public Form9(string aac044, string leiming,string str_id)
        {
            InitializeComponent();
            this.aac044 = aac044;
            Type type = Type.GetType(leiming);
            dynamic obj = type.Assembly.CreateInstance(leiming);
            var pros = type.GetProperties();
            string sql = "select * from " + type.Name + " where aac044 = '" + aac044 + "' and id ="+str_id;
            DBConn con = new DBConn();
            DataTable dt = con.GetDataSet(sql).Tables[0];

            for (int i = 0; i < pros.Length; i++)
            {
                pros[i].SetValue(obj, dt.Rows[0][pros[i].Name].ToString(), null);
            }
            Addkj(obj);
        }





        //动态添加控件
        public void Addkj(dynamic s)
        {
            var props = s.GetType().GetProperties();

            int r = 1;
            int a = 1;
            int b = 0;
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
                if (i == 25) { r += 1; a = 6; b = 20; }
                this.groupBox1.Controls.Add(l1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form9_Load(object sender, EventArgs e)
        {

        }
    }
}
