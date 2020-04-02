using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            
            
        }
        int num = 0;
        int qian = 0;
        //声明一个API函数 
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool BitBlt(
            IntPtr hdcDest, // 目标 DC的句柄 
            int nXDest,
            int nYDest,
            int nWidth,
            int nHeight,
            IntPtr hdcSrc, // 源DC的句柄 
            int nXSrc,
            int nYSrc,
            System.Int32 dwRop // 光栅的处理数值 
            );
        private void button1_Click(object sender, EventArgs e)
        {
            //获得当前屏幕的大小 
            Rectangle rect = new Rectangle();
            rect = Screen.GetWorkingArea(this);
            //创建一个以当前屏幕为模板的图象 
            Graphics g1 = this.CreateGraphics();
            //创建以屏幕大小为标准的位图 
            Image MyImage = new Bitmap(rect.Width, rect.Height, g1);
            Graphics g2 = Graphics.FromImage(MyImage);
            //得到屏幕的DC 
            IntPtr dc1 = g1.GetHdc();
            //得到Bitmap的DC 
            IntPtr dc2 = g2.GetHdc();
            //调用此API函数，实现屏幕捕获 
            BitBlt(dc2, 0, 0, rect.Width, rect.Height, dc1, 0, 0, 13369376);
            //释放掉屏幕的DC 
            g1.ReleaseHdc(dc1);
            //释放掉Bitmap的DC 
            g2.ReleaseHdc(dc2);
            //以JPG文件格式来保存 
            MyImage.Save(@"D:\Capture.jpg", ImageFormat.Jpeg);

            //if (textBox1.Text.Trim() == "")
            //{
            //    MessageBox.Show("输入金额啊");
            //    return;
            //}

            //if (rd.Next(0, 100) < 40)
            //{
            //    jn++;
            //}
            //if (Regex.Match(textBox1.Text.Trim(), "^\\d+$").Success)
            //{
            //    List<string> ls = new List<string>();
            //    List<int> fanwei1 = new List<int>();
            //    List<int> fanwei2 = new List<int>();
            //    List<string> lx = new List<string>();
            //    switch (combuwei.SelectedItem.ToString())
            //    {
            //        case "武器":
            //            ls.Add("必杀");
            //            ls.Add("狂暴");
            //            ls.Add("连击");
            //            ls.Add("伤害溅射");
            //            ls.Add("法术暴击");
            //            fanwei1.Add(440);
            //            fanwei1.Add(488);
            //            lx.Add("攻击");

            //            shua(fanwei1, fanwei2, ls, lx);
            //            break;
            //        case "帽子":
            //            ls.Add("精准");
            //            fanwei1.Add(51);
            //            fanwei1.Add(68);
            //            fanwei2.Add(310);
            //            fanwei2.Add(376);
            //            lx.Add("防御");
            //            lx.Add("魔法值");
            //            shua(fanwei1, fanwei2, ls, lx);
            //            break;
            //        case "衣服":
            //            ls.Add("躲避");
            //            ls.Add("强化防御");
            //            fanwei1.Add(171);
            //            fanwei1.Add(198);
            //            lx.Add("防御");
            //            shua(fanwei1, fanwei2, ls, lx);
            //            break;
            //        case "项链":
            //            ls.Add("药师");
            //            ls.Add("神佑");
            //            ls.Add("药王");
            //            ls.Add("药香吸收");
            //            ls.Add("冥想");
            //            fanwei1.Add(118);
            //            fanwei1.Add(143);
            //            lx.Add("灵力");
            //            shua(fanwei1, fanwei2, ls, lx);

            //            break;
            //        case "腰带":
            //            ls.Add("愤怒");
            //            ls.Add("指挥");
            //            fanwei1.Add(51);
            //            fanwei1.Add(68);
            //            fanwei2.Add(310);
            //            fanwei2.Add(376);
            //            lx.Add("防御");
            //            lx.Add("气血");
            //            shua(fanwei1, fanwei2, ls, lx);
            //            break;
            //        case "鞋子":
            //            ls.Add("遁甲");
            //            ls.Add("迅捷");
            //            ls.Add("追捕");
            //            fanwei1.Add(51);
            //            fanwei1.Add(68);
            //            fanwei2.Add(41);
            //            fanwei2.Add(55);
            //            lx.Add("防御");
            //            lx.Add("速度");
            //            shua(fanwei1, fanwei2, ls, lx);
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }

        public void shua(List<int> fanwei1, List<int> fanwei2, List<string> ls, List<string> lx)
        {
            ls.Add("易成长");
            ls.Add("永固");
            ls.Add("幸运数字");
            textBox1.Enabled = false;
            Random rd = new Random();
            int ling = rd.Next(fanwei1[0], fanwei1[1]);
            label1.Text = lx[0] + " :" + ling.ToString();
            if (lx.Count > 1)
            {
                int f = rd.Next(fanwei2[0], fanwei2[1]);
                label1.Text += "       " + lx[1] + " :" + f.ToString();
            }


            Random dsjia = new Random();
            int ds = 0;
            if (dsjia.Next(1000) > 500)
            {
                ds = 1;//表示中奖
            }
            else
            {
                ds = 2;//表示不中奖
            }

            int qsxz = 0;
            int hsxz = 0;
            int sxr = dsjia.Next(14, 32);

            if (ds == 1)
            {
                if (sxr > 23)
                {
                    qsxz = sxr;
                    hsxz = 0 - rd.Next(8, 11);
                }
                else
                {
                    qsxz = sxr;
                    hsxz = 0;
                }
            }
            else
            {
                qsxz = rd.Next(sxr / 2, sxr / 2 + 4);
                hsxz = sxr - qsxz;
            }
            if (hsxz == 0)
            {
                label2.Text = "属性1 ：" + qsxz.ToString();
                label3.Text = "";
            }
            else
            {
                label2.Text = "属性1 ：" + qsxz.ToString();
                label3.Text = "属性2 ：" + hsxz.ToString();
            }



            int texiao1 = 0;
            int texiao2 = 0;
            int teji = 0;
            if (rd.Next(100) < 4)
            {
                texiao1 = 1;

                if (rd.Next(100) < 4)
                {
                    texiao2 = 1;
                }
            }
            if (rd.Next(100) < 16)
            {
                teji = 1;
            }

            label6.Text = "";
            label5.Text = "";
            label4.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";


            List<string> tls = new List<string>(); //特技
            tls.Add("嗜血连击");
            tls.Add("烽火连城");
            tls.Add("蓄力一击");
            tls.Add("冥王斩");
            tls.Add("乱刃斩");
            tls.Add("破力式");
            tls.Add("破敏式");
            tls.Add("破魔式");
            tls.Add("破魔斩");
            tls.Add("破耐式");
            tls.Add("破体式");
            tls.Add("涅槃重生");
            tls.Add("回生之术");
            tls.Add("唤魂诀");
            tls.Add("愈气诀");
            tls.Add("愈命诀");
            tls.Add("愈心诀");
            tls.Add("回气术");
            tls.Add("回天术");
            tls.Add("天降甘霖");
            tls.Add("归气诀");
            tls.Add("归神诀");
            tls.Add("请心术");
            tls.Add("净心术");
            tls.Add("群体清心");
            tls.Add("激励");
            tls.Add("疾速");
            tls.Add("守护");
            tls.Add("真元护法");
            tls.Add("驾驭术");
            tls.Add("群体激励");
            tls.Add("群体疾速");
            tls.Add("群体守护");
            tls.Add("天地护法");
            tls.Add("刺甲咒");
            tls.Add("法术反制");
            tls.Add("化险为夷");
            tls.Add("明镜非台");
            tls.Add("仙衣云裳");
            tls.Add("移魂大法");
            tls.Add("迟滞");
            tls.Add("止戈");
            tls.Add("卸甲");
            tls.Add("群体迟滞");
            tls.Add("群体卸甲");
            tls.Add("群体止戈");
            tls.Add("锦里藏针");
            tls.Add("魔法之伤");
            tls.Add("魔音摄心");
            tls.Add("生命之伤");
            tls.Add("噬魔");
            tls.Add("吸血");

            if (texiao1 == 1)
            {
                int a = rd.Next(0, ls.Count - 1);
                label4.Text = ls[a];
                if (ls[a] == "法术暴击" || ls[a] == "必杀")
                {
                    ls.Remove("法术暴击");
                    ls.Remove("必杀");
                }
                else
                {
                    ls.RemoveAt(a);
                }
            }
            if (texiao2 == 1)
            {
                label5.Text = ls[rd.Next(0, ls.Count - 1)];
            }
            if (teji == 1)
            {
                label6.Text = "特技";


                int lsc = tls.Count;
                int a1 = rd.Next(0, lsc - 1);
                label9.Text = tls[a1];
                tls.RemoveAt(a1);
                lsc = tls.Count;
                int a2 = rd.Next(0, lsc - 1);
                label10.Text = tls[a2];
                tls.RemoveAt(a2);
                lsc = tls.Count;
                label11.Text = tls[rd.Next(0, lsc - 1)];
            }


            num += int.Parse(textBox1.Text.Trim());
            label8.Text = "本次打造金额：" + num;


            if (label4.Text != "" && label5.Text != "" && label6.Text != "")
            {
                MessageBox.Show("三红你不要嘛");
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            combuwei.SelectedIndex = 0;
        }
    }
}
