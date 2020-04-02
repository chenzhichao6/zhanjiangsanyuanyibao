using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp1.po
{
    public static class qj
    {
        public static Dictionary<string, string> icd { get; set; } = null; //icd编码
        public static List<string> ls { get; set; } = null; //qj属性集合
        public static Dictionary<string, string> zd { get; set; } = null; //字典
        public static Dictionary<string, string> aab019 { get; set; } = null; //单位类型
        public static Dictionary<string, string> aab020 { get; set; } = null; //经济类型
        public static Dictionary<string, string> aab021 { get; set; } = null; //隶属关系
        public static Dictionary<string, string> aab088 { get; set; } = null; //缴费周期类型
        public static Dictionary<string, string> aac004 { get; set; } = null; //性别
        public static Dictionary<string, string> aac031 { get; set; } = null; //个人缴费状态
        public static Dictionary<string, string> aac043 { get; set; } = null; //证件类型
        public static Dictionary<string, string> aae140 { get; set; } = null; //险种类型
        public static Dictionary<string, string> aka065 { get; set; } = null; //收费项目等级
        public static Dictionary<string, string> aka070 { get; set; } = null; //药品剂型
        public static Dictionary<string, string> aka105 { get; set; } = null; //主要费用结算方式
        public static Dictionary<string, string> yka111 { get; set; } = null; //大类代码（结算项目分类）
        public static Dictionary<string, string> aka130 { get; set; } = null; //医疗类别
        public static Dictionary<string, string> ykc021 { get; set; } = null; //人员类别
        public static Dictionary<string, string> ykc195 { get; set; } = null; //出院原因
        public static Dictionary<string, string> ykc300 { get; set; } = null; //人群类别
        public static Dictionary<string, string> akf001 { get; set; } = null; //入院、出院科室
        public static Dictionary<string, string> ykc023 { get; set; } = null; //住院状态
        public static Dictionary<string, string> ykc615 { get; set; } = null; //特项标志
        public static Dictionary<string, string> ykc667 { get; set; } = null; //二次反院审批标志
        public static Dictionary<string, string> ykc673 { get; set; } = null; //转诊转院审批结果
        public static Dictionary<string, string> ykc675 { get; set; } = null; //出院结算类型
        public static Dictionary<string, string> ykc679 { get; set; } = null; //住院原因
        public static Dictionary<string, string> ykc680 { get; set; } = null; //补助类型
        public static Dictionary<string, string> ykc682 { get; set; } = null; //转院类别
        public static Dictionary<string, string> yzz059 { get; set; } = null; //公务员级别
        public static Dictionary<string, string> akb023 { get; set; } = null; //医疗机构分类代码
        public static Dictionary<string, string> tcbm { get; set; } = null; //统筹区代码
        public static Dictionary<string, string> ake132 { get; set; } = null; //转院标志
        public static Dictionary<string, string> aka075 { get; set; } = null; //扣款类型
        public static Dictionary<string, string> akc269 { get; set; } = null; //医疗费用结算扣款原因
        public static Dictionary<string, string> aka101 { get; set; } = null; //医院等级
        public static Dictionary<string, string> aac005 { get; set; } = null;// 民族字典
        public static Dictionary<string, string> akf002 { get; set; } = null;// 出院科室
        public static List<dlwh> dlwh { get; set; } = null;// 大类维护

        public static Dictionary<string, string> status = null;

        public static void init()
        {

            status = new Dictionary<string, string>();
            status.Add("0", "身份验证");
            status.Add("1", "就诊登记");
            status.Add("2", "费用明细");
            status.Add("3", "出院登记");
            status.Add("4", "费用结算");
            status.Add("5", "病案上传");

            string path = @".\dmdy.Xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode ziduan = doc.SelectSingleNode("//ziduan");

            ls = new List<string>();
            foreach (XmlNode z in ziduan)
            {
                ls.Add(z.Name);
            }

            foreach (var l in ls)
            {
                XmlNode zds = ziduan.SelectSingleNode(l);
                Dictionary<string, string> s = (Dictionary<string, string>)typeof(qj).GetProperty(l).GetValue(null, null);
                s = new Dictionary<string, string>();
                foreach (XmlNode a in zds)
                {
                    s.Add(a.Attributes["dm"].InnerText, a.InnerText);
                }
                typeof(qj).GetProperty(l).SetValue(s, s, null);
            }

            //代码字典
            zd = new Dictionary<string, string>();
            DBConn db = new DBConn();
            string sql = "select * from zidian";
            DataTable dt = db.GetDataSet(sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int a = 0;
                foreach (var z in zd)
                {
                    if (dt.Rows[i][0].ToString() == z.Key)
                    {
                        a = 1;
                        continue;
                    }
                }
                if (a == 1) { continue; }
                zd.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
            }

            //icd编码
            string sql1 = "select FICDM,FJBNAME from TICD where FICDVERSION = 10";
            DataTable dt1 = db.GetDataSet(sql1).Tables[0];

            icd = new Dictionary<string, string>();
            foreach (DataRow d in dt1.Rows)
            {
                icd.Add(d["FICDM"].ToString(), d["FJBNAME"].ToString());
            }

            akf002 = akf001;
            string sql2 = "";
        }

        //控件数据绑定
        public static Control kjdq(Control gb, object o)
        {
            var pros = o.GetType().GetProperties();

            foreach (var p in pros)
            {
                foreach (Control c in gb.Controls)
                {
                    if (c.Name.Substring(3) == p.Name)
                    {
                        if (c.Name.Substring(0, 3) == "com")
                        {
                            Dictionary<string, string> dd = (Dictionary<string, string>)typeof(qj).GetProperty(p.Name).GetValue(null, null);
                            var a = from r in dd where r.Key == p.GetValue(o, null).ToString() select r.Value;
                            if (a.ToList<string>().Count > 0)
                            {
                                string res = a.ToList<string>()[0];
                                ((ComboBox)c).SelectedItem = res;
                            }
                        }
                        else if (c.Name.Substring(0, 3) == "dtp")
                        {
                            string time = p.GetValue(o, null).ToString();
                            if (time.Trim() != "")
                            {
                                time = time.Insert(4, "-");
                                time = time.Insert(7, "-");
                                c.Text = time;
                            }

                        }
                        else
                        {
                            var d = typeof(qj).GetProperty(p.Name);
                            if (d != null)
                            {
                                Dictionary<string, string> dd = (Dictionary<string, string>)d.GetValue(null, null);
                                var a = from r in dd where r.Key == p.GetValue(o, null).ToString() select r.Value;
                                if (a.ToList<string>().Count > 0)
                                {
                                    string res = a.ToList<string>()[0];
                                    c.Text = res;
                                }
                            }
                            if (d == null)
                            {
                                c.Text = p.GetValue(o, null).ToString();
                            }
                        }
                    }
                }

            }
            return gb;
        }

        //遍历xml 返回对象
        //对象  XML  类名  方法名  参数
        public static object rxml(object o, string xml, string leiming, string ffming, List<object> cs)
        {
            var pors = o.GetType().GetProperties();

            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            XmlNode res = xd.SelectSingleNode("result");
            int error = int.Parse(res.SelectSingleNode("errorcode").InnerText);
            if (error < 0)
            {
                MessageBox.Show(res.SelectSingleNode("errormsg").InnerText);
                return null;
            }
            else
            {
                if (leiming != null)
                {
                    if (ffming != null)
                    {
                        // 1.Load(命名空间名称)，GetType(命名空间.类名)
                        Type type = Assembly.Load("WindowsFormsApp1").GetType("WindowsFormsApp1." + leiming);
                        //2.GetMethod(需要调用的方法名称)
                        MethodInfo method = type.GetMethod(ffming, new Type[] { typeof(string) });
                        // 3.调用的实例化方法（非静态方法）需要创建类型的一个实例
                        object obj = Activator.CreateInstance(type);
                        //4.方法需要传入的参数
                        object[] parameters = null;
                        if (cs.Count > 0)
                        {
                            parameters = new object[] { cs.ToArray() };
                        }
                        else
                        {
                            parameters = new object[] { };
                        }
                        // 5.调用方法，如果调用的是一个静态方法，就不需要第3步（创建类型的实例）
                        // 相应地调用静态方法时，Invoke的第一个参数为null
                        method.Invoke(obj, parameters);

                    }
                }

                XmlNode op = res.SelectSingleNode("output");
                for (int i = 0; i < pors.Length; i++)
                {
                    if (pors[i].Name == "sign" || pors[i].Name == "transid" || pors[i].Name == "errorcode" || pors[i].Name == "errormsg")
                    {
                        pors[i].SetValue(o, res.SelectSingleNode(pors[i].Name).InnerText, null);
                    }
                    else
                    {
                        if (op.SelectSingleNode(pors[i].Name) == null) { continue; }
                        pors[i].SetValue(o, op.SelectSingleNode(pors[i].Name).InnerText, null);
                    }
                }
            }
            return o;
        }

        //更换dt中的所有代码
        public static DataTable ppdt(DataTable dt)
        {
            var ps = typeof(qj).GetProperties();
            foreach (DataColumn dc in dt.Columns)
            {
                for (int i = 0; i < ps.Length; i++)
                {
                    if (dc.ColumnName == ps[i].Name)
                    {
                        foreach (DataRow d in dt.Rows)
                        {
                            d[dc] = pipei(ps[i].Name, d[dc].ToString());
                        }
                    }
                }
            }
            return dt;
        }

        //字典val 转key
        public static string zdvaltokey(string val)
        {
            foreach (var z in zd)
            {
                if(z.Value == val)
                {
                    return z.Key;
                }
            }
            return val;
        }

        //字段匹配   根据key  返回值
        public static string pipei(string ziduan, string key)
        {
            if (ziduan == "aac005")
            {

            }

            foreach (var l in ls)
            {
                if (ziduan == l)
                {
                    Dictionary<string, string> s = (Dictionary<string, string>)typeof(qj).GetProperty(ziduan).GetValue(null, null);
                    foreach (var q in s)
                    {
                        if (q.Key == key)
                        {
                            return q.Value;
                        }
                    }
                }
            }

            return key;
        }
        //字段匹配   根据val  返回key
        public static string ppVal(string ziduan, string value)
        {
            if (ziduan == "aac005")
            {

            }

            foreach (var l in ls)
            {
                if (ziduan == l)
                {
                    Dictionary<string, string> s = (Dictionary<string, string>)typeof(qj).GetProperty(ziduan).GetValue(null, null);
                    foreach (var q in s)
                    {
                        if (q.Value == value)
                        {
                            return q.Key;
                        }
                    }
                }
            }

            return value;
        }

        //sql工厂模式
        public static string getSql(object o, string taname, string id, Dictionary<string, string> sx)
        {
            var pros = o.GetType().GetProperties();
            string col = "INSERT INTO [ydjs_zyy].[dbo].[" + taname + "](";
            string val = " VALUES (";
            for (int i = 0; i < pros.Length; i++)
            {
                col += "[" + pros[i].Name + "]";
                val += "'" + pros[i].GetValue(o, null) + "'";
                if (i < pros.Length - 1) { col += ","; } else if (i == pros.Length - 1) { col = col + ",Id"; }
                if (i < pros.Length - 1) { val += ","; } else if (i == pros.Length - 1) { val = val + "," + id; }
            }
            if (sx != null)
            {
                foreach (var s in sx)
                {
                    col += "," + s.Key;
                    val += ",'" + s.Value + "'";
                }
            }
            col += ")";
            val += ")";
            string sql = col + val;
            return sql;
        }

        //回退數據
        public static int dHuitui(string tabname, string aac044, string id)
        {
            DBConn db = new DBConn();
            string sql1 = "delete FROM  " + tabname + " where aac044 = '" + aac044 + "' and id =" + id;
            return db.GetSqlCmd(sql1);
        }

        //更新状态
        public static void gxStatus(int status, string aac044, string id)
        {
            DBConn db = new DBConn();
            string sql = "update sfsb set status = '" + status + "' where aac044 = '" + aac044 + "' and id =" + id;
            db.GetSqlCmd(sql);
        }

        //获取状态
        public static string getGxStatus(string aac044, string id)
        {
            DBConn db = new DBConn();
            string sql = "select status from sfsb where aac044 = '" + aac044 + "' and id = " + id;
            return db.GetDataScalar(sql).ToString();
        }

        //超时重发机制
        public static string cscf(string no, string xml)
        {
            try
            {
                WebReference.STYDJY bbb = new WebReference.STYDJY();
                return bbb.STYDJKService(no, xml);
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "System.Net.WebException" && ex.Message.ToString().Contains("超时"))
                {
                    DialogResult result = MessageBox.Show("操作超时，是否进行重发？", "友情提示", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        return cscf(no, xml);
                    }
                }
                return "";
            }
        }
    }
}
