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

namespace Lab4Task
{
    public partial class Form1 : Form
    {
        private int waitButtonPressCount = 0;
        private CancellationTokenSource cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Task started...";
            progressBar1.Value = 0;
            IProgress<int> progress = new Progress<int>(percent =>
            {
                label3.Text = $"Progress: {percent}%"; 
                progressBar1.Value = percent;        
            });
            cancellationTokenSource = new CancellationTokenSource();
            int result = await Task.Run(() => RunLongTask(100, progress, cancellationTokenSource.Token));
            label1.Text = $"Task finished! Result: {result}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            waitButtonPressCount++;
            label2.Text = $"Wait button pressed: {waitButtonPressCount} times";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
            label1.Text = "Task canceled.";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Press 'Start' to begin.";
            label2.Text = "Wait button pressed: 0 times";
            label3.Text = "Progress: 0%";
        }

        private Task<int> RunLongTask(int count, IProgress<int> ChangeProgressBar, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                int sum = 0;
                for (int i = 1; i <= 100; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return sum; 
                    }
                    sum += i;
                    Thread.Sleep(100);
                    ChangeProgressBar.Report(i);
                }
                return sum;
            });
        }
    }
}
