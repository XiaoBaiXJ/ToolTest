using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserChart
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                thread.Abort();
            }
            catch (Exception)
            {
            }
            thread = new Thread(StartThread);
            thread.IsBackground = true;
            thread.Start();
        }


        Thread thread;

        private void Form3_Load(object sender, EventArgs e)
        {
            thread = new Thread(StartThread);
            thread.IsBackground = true;
            thread.Start();
        }

        private void StartThread()
        {
            this.Invoke(new Action(() => 
            {
                MessageBox.Show("开启了一个线程！");
            }));
            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
