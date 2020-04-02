using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WindowsFormsApp1.po;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private List<dlwh> list = null;
        string path;
        Form10 frm = null;


        public Form2(Form10 frm)
        {
            this.frm = frm;
            InitializeComponent();
            dataGridView1.RowHeadersVisible = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string sql = "SELECT distinct sfmc from GY_SFXM ";

            hisDBConn conn = new hisDBConn();
            DataSet ds = conn.GetDataSet(sql);
            DataTable dt = ds.Tables[0];


            path = System.AppDomain.CurrentDomain.BaseDirectory + "//dlwh.xml";
            if (File.Exists(@path))
            {
                ReadXml(path);
            }
            else
            {
                CreateXmlFile(dt, path);
                ReadXml(path);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i][0].ToString());
            }
            comboBox2.Items.Add("-");
            foreach (var y in qj.yka111.Values)
            {
                comboBox2.Items.Add(y);
            }
            if (comboBox1.Items.Count > 0) { comboBox1.SelectedIndex = 0; }
            if (comboBox2.Items.Count > 0) { comboBox2.SelectedIndex = 0; }

            DataTable xml = new DataTable();
            xml.Columns.Add("本地大类名称");
            xml.Columns.Add("异地结算大类名称");
            foreach (var t in list)
            {
                DataRow dr = xml.NewRow();
                dr[0] = t.odlmc;
                dr[1] = t.dlmc;
                xml.Rows.Add(dr);
            }
            dataGridView1.DataSource = xml;
        }

        public void CreateXmlFile(DataTable dt, string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点    
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);

            //创建根节点    
            XmlNode root = xmlDoc.CreateElement("TWorkRoom");
            xmlDoc.AppendChild(root);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlNode node1 = xmlDoc.CreateNode(XmlNodeType.Element, "Dept", null);
                CreateNode(xmlDoc, node1, "odlmc", dt.Rows[i][0].ToString());
                if (dt.Columns.Count > 1) { CreateNode(xmlDoc, node1, "dlmc", dt.Rows[i][1].ToString()); } else { CreateNode(xmlDoc, node1, "dlmc", "-"); }

                root.AppendChild(node1);
            }


            try
            {
                int i = 0;
                if (File.Exists(@path))
                {
                    i = 1;
                }
                xmlDoc.Save(path);
                if (i == 1)
                {
                    MessageBox.Show("保存成功");
                    ReadXml(path);
                }
            }
            catch (Exception e)
            {
                //显示错误信息    
                MessageBox.Show("操作有误");
            }
            //Console.ReadLine();    

        }

        /// <summary>      
        /// 创建节点      
        /// </summary>      
        /// <param name="xmldoc"></param>  xml文档    
        /// <param name="parentnode"></param>父节点      
        /// <param name="name"></param>  节点名    
        /// <param name="value"></param>  节点值    
        ///     
        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }

        public void ReadXml(string path)
        {
            list = new List<dlwh>();

            XmlDocument XmlDoc = new XmlDocument();
            //读取Xml文档
            string StrNode = "";
            XmlNodeReader reader = null;
            try
            {
                // 装入指定的XML文档
                XmlDoc.Load(path);
                // 设定XmlNodeReader对象来打开XML文件
                reader = new XmlNodeReader(XmlDoc);
                // 读取XML文件中的数据，并显示出来
                dlwh tw = null;
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            StrNode = reader.Name;
                            if (StrNode.Equals("Dept"))
                            {
                                tw = new dlwh();
                            }
                            break;
                        case XmlNodeType.Text:
                            if (StrNode.Equals("odlmc"))
                            {
                                tw.odlmc = reader.Value;

                            }
                            else if (StrNode.Equals("dlmc"))
                            {
                                tw.dlmc = reader.Value;
                                list.Add(tw);
                            }
                            break;
                    }
                }
                qj.dlwh = list;
            }
            catch (Exception ex)
            {
                // System.Windows.Forms.Application.Exit();
            }
            finally
            {
                //清除打开的数据流
                if (reader != null)
                    reader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var ls in list)
            {
                if (ls.odlmc == comboBox1.SelectedItem.ToString())
                {
                    ls.dlmc = comboBox2.SelectedItem.ToString();
                    DataTable xml = new DataTable();
                    xml.Columns.Add("本地大类名称");
                    xml.Columns.Add("异地计算大类名称");
                    foreach (var t in list)
                    {
                        DataRow dr = xml.NewRow();
                        dr[0] = t.odlmc;
                        dr[1] = t.dlmc;
                        xml.Rows.Add(dr);
                    }
                    CreateXmlFile(xml, path);
                    dataGridView1.DataSource = xml;
                    break;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm.init();
        }
    }
}
