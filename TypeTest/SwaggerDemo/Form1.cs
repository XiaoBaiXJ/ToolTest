using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwaggerDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FluentFTP.FtpClient client = new FluentFTP.FtpClient("192.168.0.175");

        }
    }
}
