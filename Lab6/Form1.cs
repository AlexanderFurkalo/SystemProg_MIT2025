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

        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            if (DirectoriesTreeView.SelectedNode?.Tag is DirectoryInfo selectedDir)
            {
                string newFolderName = textBoxFolderName.Text.Trim();

                if (string.IsNullOrWhiteSpace(newFolderName))
                {
                    MessageBox.Show("Please enter a folder name.");
                    return;
                }

                string newFolderPath = Path.Combine(selectedDir.FullName, newFolderName);

                try
                {
                    if (!Directory.Exists(newFolderPath))
                    {
                        Directory.CreateDirectory(newFolderPath);
                        MessageBox.Show("Folder created successfully!");

                        selectedDir.Refresh();
                        TreeNode newNode = new TreeNode(newFolderName)
                        {
                            Tag = new DirectoryInfo(newFolderPath)
                        };
                        DirectoriesTreeView.SelectedNode.Nodes.Add(newNode);
                        DirectoriesTreeView.SelectedNode.Expand();
                    }
                    else
                    {
                        MessageBox.Show("A folder with this name already exists.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating folder: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a directory in the tree.");
            }
        }

        private void btnCreateTextFile_Click(object sender, EventArgs e)
        {
            if (DirectoriesTreeView.SelectedNode?.Tag is DirectoryInfo selectedDir)
            {
                string fileName = textBoxFileName.Text.Trim();
                string content = textBoxFileContent.Text;

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    MessageBox.Show("Please enter a file name.");
                    return;
                }

                if (!fileName.EndsWith(".txt"))
                {
                    fileName += ".txt";
                }

                string fullPath = Path.Combine(selectedDir.FullName, fileName);

                try
                {
                    if (!File.Exists(fullPath))
                    {
                        File.WriteAllText(fullPath, content);
                        MessageBox.Show("Text file created successfully!");

                        TreeNode newFileNode = new TreeNode(fileName)
                        {
                            Tag = new FileInfo(fullPath)
                        };
                        DirectoriesTreeView.SelectedNode.Nodes.Add(newFileNode);
                        DirectoriesTreeView.SelectedNode.Expand();
                    }
                    else
                    {
                        MessageBox.Show("A file with this name already exists.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating file: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a directory in the tree.");
            }
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (DirectoriesTreeView.SelectedNode?.Tag is FileSystemInfo selectedItem)
            {
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete \"{selectedItem.Name}\"?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (selectedItem is FileInfo file)
                        {
                            file.Delete();
                        }
                        else if (selectedItem is DirectoryInfo dir)
                        {
                            dir.Delete(true);
                        }

                        DirectoriesTreeView.SelectedNode.Remove();

                        MessageBox.Show("Item deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting item: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a file or folder to delete.");
            }
        }
    }
}
