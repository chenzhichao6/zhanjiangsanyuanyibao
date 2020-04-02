using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using WindowsFormsApp1.po;

namespace WindowsFormsApp1
{
    public partial class Form15 : Form
    {
        gxjzdj gx = null;
        string bahm = ""; // 病案号码
        string xml = "";
        string TransNo = ""; //调用的接口
        string str_id="";

        public Form15(gxjzdj gx,string id)
        {
            this.gx = gx;
            this.str_id = id;
            InitializeComponent();
        }

        private void Form15_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }


        #region 病人信息初始化
        public void brxxInit()
        {
            xml = "";
            TransNo = "0801";
            //string sql = " select BAHM from zy_brry where ZYHM = '" + gx.akc190 + "'";
            //hisDBConn hdb = new hisDBConn();
            //DataTable dt = hdb.GetDataSet(sql).Tables[0];
            //if (dt.Rows.Count > 0)
            //{
            //    bahm = dt.Rows[0][0].ToString();
            //}
            bahm = gx.akc190;
            if (bahm == "") return;


            string sql1 = "SELECT t.FPRN as 'yzy001',t.FTIMES as 'yzy002',t.FICDVERSION as 'yzy003',t.FID as 'yzy004',t.FAGE as 'akc023',t.FNAME as 'aac003',t.FSEXBH as 'aac004',t.FSEX as 'yzy008',"
            + "t.FBIRTHDAY as 'aac006' ,t.FBIRTHPLACE as 'yzy010', t.FIDCARD as 'yzy011' ,t.FCOUNTRYBH as 'aac161',t.FCOUNTRY as 'yzy013',t.FNATIONALITYBH as 'aac005',t.FNATIONALITY as 'yzy015',"
            + "t.FJOB as 'yzy016',t.FSTATUSBH as 'aac017',t.FSTATUS as 'yzy018',t.FDWNAME as 'aab004',t.fdwaddr as 'yzy020',t.FDWTELE as 'yzy021',t.FDWPOST as 'yzy022',"
            + "t.fhkaddr as 'aac010',t.FHKPOST as 'yzy024',t.FLXNAME as 'aae004',t.FRELATE as 'yzy026',t.flxaddr as 'yzy027',t.FLXTELE as 'yzy028',t.FASCARD1 as 'yzy029',"
            + "t.FRYDATE as 'ykc701',t.FRYTYKH as 'yzy032',t.FRYDEPT as 'yzy033',t.FRYBS as 'yzy034',t.FCYDATE as 'ykc702',t.FCYTYKH as 'yzy037',t.FCYDEPT as 'yzy038',"
            + "t.FCYBS as 'yzy039',t.FDAYS as 'akb063',t.FMZZDBH as 'akc193',t.FMZZD as 'akc050',t.FMZDOCTBH as 'yzy043',t.FMZDOCT as 'ake022',t.FPHZD as 'yzy045',"
            + "t.FGMYW as 'yzy046',t.FQJTIMES as 'yzy047', isnull(t.FQJSUCTIMES, '0') as 'yzy048',t.FKZRBH as 'yzy049',t.FKZR as 'yzy050',t.FZRDOCTBH as 'yzy051',"
            + "t.FZRDOCTOR as 'yzy052',t.FZZDOCTBH as 'yzy053',t.FZZDOCT as 'yzy054',t.FZYDOCTBH as 'yzy055',t.FZYDOCT as 'yzy056',t.FJXDOCTBH as 'yzy057',t.FJXDOCT as 'yzy058',"
            + "t.FSXDOCTBH as 'yzy059',t.FSXDOCT as 'yzy060',t.FBMYBH as 'yzy061',t.FBMY as 'yzy062',t.FQUALITYBH as 'yzy063',t.FQUALITY as 'yzy064',t.FZKDOCTBH as 'yzy065',"
            + "t.FZKDOCT as 'yzy066',t.FZKNURSE as 'yzy067',t.FZKNURSE as 'yzy068',t.FZKRQ as 'yzy069',t.FSUM1 as 'akc264',t.FXYF as 'ake047',t.FZYF as 'yzy072',"
            + "t.FZCHYF as 'ake050',t.FZCYF as 'ake049',t.FQTF as 'ake044',Isnull(t.FBODYBH,'2') as 'yzy076',Isnull(t.FBODY,'否') as 'yzy077',t.FBLOODBH as 'yzy078',t.FBLOOD as 'yzy079',"
            + "t.FRHBH as 'yzy080',t.FRH as 'yzy081',t.FZKTYKH as 'yzy082',t.FZKDEPT as 'yzy083',t.FZKDATE as 'yzy084',t.FZKTIME as 'yzy085',t.FJBFXBH as 'yzy086',"
            + "t.FJBFX as 'yzy087',t.FNATIVE as 'yzy088',t.fcurraddr as 'yzy089',t.FCURRTELE as 'yzy090',t.FCURRPOST as 'yzy091',t.FJOBBH as 'aca111',t.FCSTZ as 'yzy093',"
            + "t.FRYTZ as 'yzy094',t.FRYTJBH as 'yzy095',t.FRYTJ as 'yzy096',Isnull( t.FYCLJBH,'-') as 'yzy097',Isnull( t.FYCLJ,'-') as 'yzy098',Isnull( t.FPHZDBH,'-') as 'yzy099',t.FPHZDNUM as 'yzy100',"
            + "t.FIFGMYWBH as 'yzy101',t.FIFGMYW as 'yzy102',t.FNURSEBH as 'yzy103',t.FNURSE as 'yzy104',t.FLYFSBH as 'yzy105',t.FLYFS as 'yzy106',t.FYZOUTHOSTITAL as 'yzy107',"
            + "t.FSQOUTHOSTITAL as 'yzy108',t.FISAGAINRYBH as 'yzy109',t.FISAGAINRY as 'yzy110',t.FISAGAINRYMD as 'yzy111',t.FRYQHMDAYS as 'yzy112',t.FRYQHMHOURS as 'yzy113',"
            + "t.FRYQHMMINS as 'yzy114',t.FRYQHMCOUNTS as 'yzy115',t.FRYHMDAYS as 'yzy116',t.FRYHMHOURS as 'yzy117',t.FRYHMMINS as 'yzy118',t.FRYHMCOUNTS as 'yzy119',"
            + "t.FFBBHNEW as 'yzy120',t.FFBNEW as 'yzy121',t.FZFJE as 'yzy122',t.FZHFWLYLF as 'yzy123',t.FZHFWLCZF as 'yzy124',t.FZHFWLHLF as 'yzy125',t.FZHFWLQTF as 'yzy126',"
            + "t.FZDLBLF as 'yzy127',t.FZDLSSSF as 'yzy128',t.FZDLYXF as 'yzy129',t.FZDLLCF as 'yzy130',t.FZLLFFSSF as 'yzy131',t.FZLLFWLZWLF as 'yzy132',t.FZLLFSSF as 'yzy133',"
            + "t.FZLLFMZF as 'yzy134',t.FZLLFSSZLF as 'yzy135',t.FKFLKFF as 'yzy136',t.FZYLZF as 'yzy137',t.FXYLGJF as 'yzy138',t.FXYLXF as 'yzy139',t.FXYLBQBF as 'yzy140',"
            + "t.FXYLQDBF as 'yzy141',t.FXYLYXYZF as 'yzy142',t.FXYLXBYZF as 'yzy143',t.FHCLCJF as 'yzy144',t.FHCLZLF as 'yzy145',t.FHCLSSF as 'yzy146',t.FZHFWLYLF01 as 'yzy147',"
            + "t.FZHFWLYLF02 as 'yzy148',t.FZYLZDF as 'yzy149',t.FZYLZLF as 'yzy150',t.FZYLZLF01 as 'yzy151',t.FZYLZLF02 as 'yzy152',t.FZYLZLF03 as 'yzy153',t.FZYLZLF04 as 'yzy154',"
            + "t.FZYLZLF05 as 'yzy155',t.FZYLZLF06 as 'yzy156',t.FZYLQTF as 'yzy157',t.FZYLQTF01 as 'yzy158',t.FZYLQTF02 as 'yzy159',t.FZCLJGZJF as 'yzy160',c.FZLLBBH as 'yzy161',"
            + "c.FZLLB as 'yzy162',c.FMZZYZDBH as 'yzy163',c.FMZZYZD as 'yzy164',c.FSSLCLJBH as 'yzy165',c.FSSLCLJ as 'yzy166',c.FSSLCLJBH as 'yzy167',c.FSYJGZJ as 'yzy168',"
            + "c.FSYZYSBBH as 'yzy169',c.FSYZYSB as 'yzy170',c.FSYZYJSBH as 'yzy171',c.FSYZYJS as 'yzy172',c.FBZSHBH as 'yzy173',c.FBZSH as 'yzy174'"
            + "FROM [TPATIENTVISIT] t join TCHADD c on t.FPRN = c.FPRN where t.FPRN = '" + bahm + "'";

            baDBConn db = new baDBConn();
            DataTable xxdt = db.GetDataSet(sql1).Tables[0];
            if (xxdt.Rows.Count < 1) { return; }

            xml = "<?xml version=\"1.0\" encoding=\"GBK\"?>"
                 + "<input>"
                 + "<aab299>"+PublicCommon.aab299+"</aab299>"
                 + "<yab600>" + PublicCommon.yab600 + "</yab600>"
                 + "<akb026>" + PublicCommon.akb026 + "</akb026>"
                 + "<akb021>" + PublicCommon.akb021 + "</akb021>"
                 + "<ykc700>" + gx.ykc700 + "</ykc700>"
                 + "<aab301>" + gx.aab301 + "</aab301>"
                 + "<yab060>" + gx.yab060 + "</yab060>"
                 + "<aac002>" + gx.aac002 + "</aac002>"
                 + "<aac043>" + gx.aac043 + "</aac043>"
                 + "<aac044>" + gx.aac044 + "</aac044>";


            zybrxx zb = new zybrxx();
            var pros = zb.GetType().GetProperties();
            foreach (var p in pros)
            {
                
                xml += "<" + p.Name + ">" + sjcl(xxdt.Rows[0][p.Name].ToString(),p.Name) + "</" + p.Name + ">";
                p.SetValue(zb, xxdt.Rows[0][p.Name].ToString(), null);
            }



            xml += "</input>";

            Addkj(zb);
        }
        #endregion

        public string sjcl(string sj,string name)
        {
            DateTime dtTime;
            Double dob;
            if (name == "akc023")
            {
                string dw = sj.Substring(0, 1);
                string nl = sj.Substring(1);
                switch (dw)
                {
                    case "Y":
                        return nl;
                    case "M":
                        return "1";
                    case "D":
                        return "1";
                    default:
                        break;
                }
            }else if(name == "aac017")
            {
                if (sj != "")
                {
                    return sj.Substring(0, 1);
                }
            }else if (DateTime.TryParse(sj, out dtTime))
            {
                    //str转换成日期类型dtTime输出
                   
                //使用转换后的日期类型dtTime
                return dtTime.ToString("yyyyMMdd");
            }else if (sj.Contains("."))
            {
                Regex rgx = new Regex("^[0-9.]+$");
                if (rgx.IsMatch(sj)) { return Convert.ToDouble(sj).ToString("0.00"); }
            }
            return sj;
        }

       

        #region 病人诊断信息初始化
        public void zdxxInit()
        {
            xml = "";
            TransNo = "0802";

            string sql = "select d.FICDVERSION as 'yzy003',d.FPX as 'yzy201',d.FZDLX as 'yzy202',d.FJBNAME as 'akc185',d.FICDM as 'akc196',isnull(d.FRYBQBH,'1') as 'yzy205'," +
                "isnull(d.FRYBQ,'有') as 'yzy206' from [TDIAGNOSE] d where d.FPRN = '" + bahm + "'";

            DataTable dt = dtcjDVG(sql, null);

        }
        #endregion


        #region 手术信息初始化
        public void ssxxInit()
        {
            xml = "";
            TransNo = "0803";

            string sql = "select o.FPX as 'yzy201',o.FOPCODE as 'yzy207',o.FOP as 'yzy208',o.FOPDATE as 'yzy209',o.FQIEKOUBH as 'yzy210',o.FQIEKOU as 'yzy211',o.FYUHEBH as 'yzy212',o.FYUHE as 'yzy213',"
            + "o.FDOCBH as 'yzy214',o.FDOCNAME as 'yzy215',o.FMAZUIBH as 'yzy216',o.FMAZUI as 'yzy217',CAST(o.FIFFSOP AS VARCHAR) as 'yzy218',isnull(o.FOPDOCT1BH, '0') as 'yzy219',isnull(o.FOPDOCT1, '-') as 'yzy220',"
            + "isnull(o.FOPDOCT2BH, '0') as 'yzy221',isnull(o.FOPDOCT2, '-') as 'yzy222',o.FMZDOCTBH as 'yzy223',o.FMZDOCT as 'yzy224',o.FZQSSBH as 'yzy225',o.FZQSS as 'yzy226',o.FSSJBBH as 'yzy227',o.FSSJB as 'yzy228'"
            + "from TOPERATION o where o.FPRN = '51044164'";

            dtcjDVG(sql, null);
        }
        #endregion


        #region 产科分娩婴儿信息
        public void ckfmxxInit()
        {
            xml = "";
            TransNo = "0804";

            baDBConn conn = new baDBConn();
            string sql = " select b.FBABYNUM as 'yzy201',b.FBABYSEXBH as 'aac004',b.FBABYSEX as 'yzy230',b.FTZ as 'yzy231',b.FRESULTBH as 'yzy232',b.FRESULT as 'yzy233',b.FZGBH as 'yzy234',"
                   + "b.FZG as 'yzy235' , isnull(b.FBABYSUC, '0') as 'yzy236',b.FHXBH as 'yzy237',b.FHX as 'yzy238'"
                   + "from[TBABYCARD] b where b.FPRN = '" + bahm + "'";

            dtcjDVG(sql, null);
        }
        #endregion


        DataTable zldt = null;
        #region 肿瘤专科病人治疗记录信息
        public void zlzkInit()
        {
            xml = "";
            TransNo = "0805";

            string xqsql = "  select k.FPX as 'yzy201',k.FHLRQ1 as 'yzy265',k.FHLDRUG as 'yzy266',k.FHLPROC as 'yzy267',k.FHLLXBH as 'yzy268',k.FHLLX as 'yzy269' from [TKNUBHL] k where k.FPRN = '" + bahm + "'";
            string flsql = "select k.FFLFSBH as 'yzy239',k.FFLFS as 'yzy240',k.FFLCXBH as 'yzy241',k.FFLCX as 'yzy242',k.FFLZZBH as 'yzy243',k.FFLZZ as 'yzy244',k.FYJY as 'yzy245',"
                   + "k.FYCS as 'yzy246',k.FYTS as 'yzy247',k.FYRQ1 as 'yzy248',k.FYRQ2 as 'yzy249',k.FQJY as 'yzy250',k.FQCS as 'yzy251',k.FQTS as 'yzy252',"
                   + "k.FQRQ1 as 'yzy253',k.FQRQ2 as 'yzy254',k.FZNAME as 'yzy255',k.FZJY as 'yzy256',k.FZCS as 'yzy257',k.FZTS as 'yzy258',k.FZRQ1 as 'yzy259',"
                   + "k.FZRQ2 as 'yzy260',k.FHLFSBH as 'yzy261',k.FHLFS as 'yzy262',k.FHLFFBH as 'yzy263',k.FHLFF as 'yzy264'"
                   + "from[TKNUBCARD] k where k.FPRN = '" + bahm + "'";

            dtcjDVG(xqsql, zldt);

            baDBConn con = new baDBConn();
            zldt = con.GetDataSet(flsql).Tables[0];
            if (zldt.Rows.Count > 0)
            {
                Button btn1 = new Button();
                btn1.Location = new System.Drawing.Point(641, 503);
                btn1.Name = "btn1";
                btn1.Size = new System.Drawing.Size(93, 40);
                btn1.TabIndex = 4;
                btn1.Text = "详情资料";
                btn1.UseVisualStyleBackColor = true;
                btn1.Click += new EventHandler(btn1_Click);
            }
        }
        #endregion


        #region 动态创建DVG
        public DataTable dtcjDVG(string sql, DataTable lsdt)
        {
            DataGridView dg = new DataGridView();
            dg.AllowUserToAddRows = false;
            dg.AllowUserToDeleteRows = false;
            dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dg.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dg.BackgroundColor = System.Drawing.Color.White;
            dg.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dg.GridColor = System.Drawing.Color.White;
            dg.Location = new System.Drawing.Point(12, 118);
            dg.MultiSelect = false;
            dg.Name = "dataGridView1";
            dg.RowTemplate.Height = 23;
            dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dg.Dock = System.Windows.Forms.DockStyle.Fill;
            dg.RowHeadersVisible = false;
            dg.AllowUserToResizeRows = false;
            dg.ReadOnly = true;



            baDBConn bdb = new baDBConn();
            DataTable dt = bdb.GetDataSet(sql).Tables[0];


            DataColumnCollection dcc = dt.Columns;
            Dictionary<string, string> cns = new Dictionary<string, string>();
            foreach (DataColumn d in dcc)
            {
                foreach (var q in qj.zd)
                {
                    if (q.Key == d.ColumnName)
                    {
                        cns.Add(q.Key, q.Value);
                    }
                }
            }

            //病案xml 批量生成
            xml = "<?xml version=\"1.0\" encoding=\"GBK\"?>"
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
                + "<aac044>" + gx.aac044 + "</aac044>";

            if (lsdt != null)
            {
                foreach (DataColumn d in lsdt.Columns)
                {
                    xml += "<" + d.ColumnName + ">" + lsdt.Rows[0][d.ColumnName].ToString() + "</" + d.ColumnName + ">";
                }
            }

            foreach (DataColumn d in dt.Columns)
            {
                if (d.ColumnName == "yzy003")
                    xml += "<" + d.ColumnName + ">" + dt.Rows[0][d.ColumnName].ToString() + "</" + d.ColumnName + ">";
            }

            xml += "<detail>";


            foreach (DataRow d in dt.Rows)
            {
                xml += "<row>";
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName == "yzy003") { continue; }
                    xml += "<" + dc.ColumnName + ">" +sjcl( d[dc.ColumnName].ToString(),dc.ColumnName) + "</" + dc.ColumnName + ">";
                }
                xml += "</row>";
            }

            xml += "</detail></input>";




            foreach (var c in cns)
            {
                DataGridViewTextBoxColumn dgt = new DataGridViewTextBoxColumn();
                dgt.DataPropertyName = c.Key;
                dgt.HeaderText = c.Value;
                dgt.Name = c.Key;
                dgt.ReadOnly = true;
                dgt.Width = 110;
                dg.Columns.Add(dgt);
            }

            dg.DataSource = dt;
            panel1.Controls.Clear();
            panel1.Controls.Add(dg);

            return dt;
        }
        #endregion


        #region  详情资料
        public void xqzl(DataTable dt)
        {
            string msg = "";
            foreach (DataColumn d in dt.Columns)
            {
                foreach (var z in qj.zd)
                {
                    if (z.Key == d.ColumnName)
                    {
                        msg += z.Value + " : " + dt.Rows[0][d.ColumnName].ToString() + "\n";
                    }
                }
            }

            MessageBox.Show(msg);
        }
        #endregion


        private void btn1_Click(object sender, EventArgs e)
        {
            xqzl(zldt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string resultxml = qj.cscf(TransNo, xml);

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
                MessageBox.Show("身份证为：" + gx.aac044 + "  录入成功");
                qj.gxStatus(5,gx.aac044,str_id);
            }
        }

        //动态添加控件
        public void Addkj(zybrxx zb)
        {
            this.panel1.Controls.Clear();
            var props = zb.GetType().GetProperties();
            int r = 0;
            int a = 1;
            int b = 0;
            int c = 1;
            for (int i = 0; i < props.Length; i++)
            {
                Label l1 = new Label();
                l1.AutoSize = true;
                l1.Name = props[i].Name;
                foreach (var z in qj.zd)
                {
                    if (z.Key == props[i].Name)
                    {

                        l1.Text = z.Value + "：" + props[i].GetValue(zb, null).ToString();

                    }
                }
                l1.Size = new Size(41, 12);
                l1.Location = new Point(30 + r, 20 * c);
                c++;
                if (i % 25 == 0 && i != 0) { r += 450; a = 6; b = 20; c = 1; }
                this.panel1.Controls.Add(l1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            brxxInit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            zdxxInit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ssxxInit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    brxxInit();
                    break;
                case 1:
                    zdxxInit();
                    break;
                case 2:
                    ssxxInit();
                    break;
                case 3:
                    ckfmxxInit();
                    break;
                case 4:
                    zlzkInit();
                    break;
                default:
                    break;
            }
        }
    }
}
