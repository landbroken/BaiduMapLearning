using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Collections;

namespace MyMap
{
    //固定格式
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]  
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]   

    public partial class Form1 : Form
    {
        //初始化控件，固定格式
        public Form1()
        {
            InitializeComponent();
        }

        //载入百度地图
        private void Form1_Load(object sender, EventArgs e)
        {
             string str_url = Application.StartupPath + "\\IndexMap.html";  
             Uri url = new Uri(str_url);
             webBrowser1.Url = url;
             webBrowser1.ObjectForScripting = this;
             timer1.Enabled = true;
        }

        //定时刷新
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //获得当前鼠标所在的经纬度，然后显示在框体左下角
                string tag_lng = webBrowser1.Document.GetElementById("mouselng").InnerText;  
                string tag_lat = webBrowser1.Document.GetElementById("mouselat").InnerText;  
                double dou_lng, dou_lat;  
                if (double.TryParse(tag_lng, out dou_lng) && double.TryParse(tag_lat, out dou_lat))  
                {
                    this.toolStripStatusLabel1.Text ="当前坐标："+ dou_lng.ToString("F5") + "," + dou_lat.ToString("F5");  
                }  
            }  
            catch (Exception ee)  
            { 
                //MessageBox.Show(ee.Message); 
            }
        }

        //数字转字符串 
        public string setWhichCar(int num)
        {
            return num.ToString();
        }

        //开启测距工具按钮,点击测距按钮后触发 
        private void btnOpenDistance_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("openGetDistance");
        }
        
        //右键添加标记到图中,点击标记按钮后触发
        private void marker_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("PutInMarker");
            MessageBox.Show("请按右键添加标注！");
        }

        //清除监听事件，点击默认按钮后触发
        private void removeListen_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("CloseListener");  
        }

        //清除地图上的标记，点击清除按钮触发
        private void clearMarker_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("ClearAllMarkers");  
        } 

    }
}
