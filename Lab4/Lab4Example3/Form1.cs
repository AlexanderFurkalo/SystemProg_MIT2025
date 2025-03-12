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

namespace Lab4Example3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            IProgress<int> onChangeProgress = new Progress<int>((i) =>
            {
                label1.Text = i.ToString();
                progressBar1.Value = i;
            });
            CancellationTokenSource cts = new CancellationTokenSource();
            button2.Click += delegate { cts.Cancel(); };
            label1.Text = (await Process(100, onChangeProgress, cts.Token)).ToString();
        }
        Task<int> Process(int count, IProgress<int> ChangeProgressBar, CancellationToken cancellTocken)
        {
            return Task.Run(() =>
            {
                int i;
                for (i = 1; i <= count; i++)
                {
                    //label1.Text = i.ToString();
                    //progressBar1.Value = i;
                    if (cancellTocken.IsCancellationRequested)
                        return i;
                    ChangeProgressBar.Report(i);
                    Thread.Sleep(100);
                }
                return i;
            });
        }

        //private void button2_Click(object sender, EventArgs e)
        //{

        //}

        //private void Form1_Load(object sender, EventArgs e)
        //{

        //}
    }
}
