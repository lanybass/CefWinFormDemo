using CefSharp;
using CefSharp.Internals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YwCEF;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string url = "https://www.baidu.com/";
            BrowserManager.Instance.Browser.Dock = DockStyle.Fill;
            BrowserManager.Instance.Browser.Load(url);
            panel1.Controls.Add(BrowserManager.Instance.Browser);
        }
    }
}