using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp1.po;

namespace WindowsFormsApp1
{
    public partial class FrmTab : Form
    {
        Sfyz sf = null;
        Form3 frm = null;
        string aac044;
        string str_id;
        public FrmTab()
        {
            InitializeComponent();
        }

        public FrmTab(Sfyz sf)
        {
            this.sf = sf;
            InitializeComponent();
        }
        public FrmTab(string aac044, Form3 frm,string id)
        {
            str_id = id;
            this.frm = frm;
            this.aac044 = aac044;
            InitializeComponent();

            string sql = "select * from sfsb where id='"+str_id+"'and aac044 = '" + aac044 + "'";
            DBConn db = new DBConn();
            DataSet ds = db.GetDataSet(sql);
            DataTable dt = ds.Tables[0];

            sf = new Sfyz();
            var fss = sf.GetType().GetProperties();
            foreach (var f in fss)
            {
                f.SetValue(sf, dt.Rows[0][f.Name].ToString(), null);
            }



        }

        public int[] s = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };         //用来记录from是否打开过  


        private void FrmTab_Load(object sender, EventArgs e)
        {
            if (s[tabControl1.SelectedIndex] == 0)    //只生成一次  
            {
                btnX_Click(tabControl1, e);
            }
        }

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (s[tabControl1.SelectedIndex] == 0)    //只生成一次 AAAA 
            {
                btnX_Click(sender, e);
            }
        }

        /// <summary>  
        /// 通用按钮点击选项卡 在选项卡上显示对应的窗体  
        /// </summary>  
        private void btnX_Click(object sender, EventArgs e)
        {
            string formClass = ((TabControl)sender).SelectedTab.Tag.ToString();

            //string form = tabControl1.SelectedTab.Tag.ToString();  

            Form fm = null;
            DBConn db = null;
            DataTable dt = null;
            string sql = "";

            switch (formClass)
            {
                case "Form5": //身份验证信息
                    fm = new Form5(aac044, frm,str_id);
                    break;
                case "Form6"://就诊登记
                    fm = new Form6(sf,str_id);
                    break;
                case "Form10"://费用明细录入
                    db = new DBConn();
                    sql = "select * from gxjzdj where aac044 = '" + sf.aac044 + "'and id ="+str_id;
                    dt = db.GetDataSet(sql).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        gxjzdj gx = new gxjzdj();
                        var pros = gx.GetType().GetProperties();
                        foreach (var p in pros)
                        {
                            p.SetValue(gx, dt.Rows[0][p.Name].ToString(), null);
                        }
                        fm = new Form10(gx,str_id);
                    }
                    break;
                case "Form7"://出院登记
                    DBConn con = new DBConn();
                    sql = "select * from fymxjl where aac044 = '" + sf.aac044 + "' and id = "+str_id;
                    dt = con.GetDataSet(sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        sql = "select * from fhjzdj where aac044 = '" + sf.aac044 + "' and id = " + str_id;
                        dt = con.GetDataSet(sql).Tables[0];
                        string ykc700 = dt.Rows[0]["ykc700"].ToString();
                        fm = new Form7(sf, ykc700, str_id);
                    }
                    break;
                case "Form12"://模拟结算
                     db = new DBConn();
                    sql = "select * from cydj where aac044 = '" + sf.aac044 + "' and id = " + str_id;
                    dt = db.GetDataSet(sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        fm = new Form12(this.sf, str_id);
                    }
                    break;
                case "Form14": //费用结算
                    db = new DBConn();
                    sql = "select * from cydj where aac044 = '" + sf.aac044 + "' and id = " + str_id;
                    dt = db.GetDataSet(sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        fm = new Form14(sf, str_id); ; ;
                    }
                    break;
                case "Form15":
                    db = new DBConn();
                    sql = "select * from gxjzdj where aac044 = '" + sf.aac044 + "' and id = "+str_id;
                    dt = db.GetDataSet(sql).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        gxjzdj gx = new gxjzdj();
                        var pros = gx.GetType().GetProperties();
                        foreach (var p in pros)
                        {
                            p.SetValue(gx, dt.Rows[0][p.Name].ToString(), null);
                        }
                        fm = new Form15(gx,str_id);
                    }
                    break;
                
                default:

                    break;
            }
            if(fm == null)
            {
                int zt =int.Parse( qj.getGxStatus(aac044, str_id));
                foreach (var q in qj.status)
                {
                    if (int.Parse(q.Key )== zt+1)
                    {
                        ((TabControl)sender).SelectedTab.Controls.Clear();
                        MessageBox.Show("请先完成："+q.Value);
                        return;
                    }
                }
            }
            ((TabControl)sender).SelectedTab.Controls.Clear();
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.TopLevel = false;
            fm.Parent = ((TabControl)sender).SelectedTab;
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;
            fm.Anchor = AnchorStyles.None;
            fm.Show();

            //s[((TabControl)sender).SelectedIndex] = 1;
        }


        //在选项卡中生成窗体  
        public void GenerateForm(string form, object sender)
        {

            // 反射生成窗体  
            Form fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(form);
            //设置窗体没有边框 加入到选项卡中  
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.TopLevel = false;
            fm.Parent = ((TabControl)sender).SelectedTab;
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;
            fm.Show();



        }


    }

}