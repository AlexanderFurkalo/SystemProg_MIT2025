using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                DirectoriesTreeView.Nodes.Clear();
                DirectoryInfo rootDir = new DirectoryInfo(folderDialog.SelectedPath);
                TreeNode rootNode = new TreeNode(rootDir.Name) { Tag = rootDir };
                DirectoriesTreeView.Nodes.Add(rootNode);
                LoadDirectories(rootDir, rootNode);
            }
        }

        private void LoadDirectories(DirectoryInfo dir, TreeNode node)
        {
            try
            {
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    TreeNode subNode = new TreeNode(subDir.Name) { Tag = subDir };
                    node.Nodes.Add(subNode);
                    LoadDirectories(subDir, subNode);
                }

                foreach (FileInfo file in dir.GetFiles())
                {
                    node.Nodes.Add(new TreeNode(file.Name) { Tag = file });
                }
            }
            catch (UnauthorizedAccessException)
            {
                node.Nodes.Add(new TreeNode("Access Denied"));
            }
        }

        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            listViewSearchResults.Items.Clear();

            if (DirectoriesTreeView.SelectedNode?.Tag is DirectoryInfo startDir && !string.IsNullOrWhiteSpace(textBoxSearchFile.Text))
            {
                SearchFiles(startDir, textBoxSearchFile.Text);
            }
            else
            {
                MessageBox.Show("Please select a directory in the tree and enter a search term.");
            }
        }

        private void SearchFiles(DirectoryInfo dir, string pattern)
        {
            try
            {
                foreach (FileInfo file in dir.GetFiles(pattern))
                {
                    ListViewItem item = new ListViewItem(file.Name);
                    item.SubItems.Add(file.FullName);
                    item.SubItems.Add(file.Length.ToString());
                    item.SubItems.Add(file.CreationTime.ToString());
                    listViewSearchResults.Items.Add(item);
                }

                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    SearchFiles(subDir, pattern); 
                }
            }
            catch (UnauthorizedAccessException)
            {
                System.Diagnostics.Debug.WriteLine($"Access denied");
            }
        }
    }
}
