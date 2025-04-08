
namespace Lab6
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.DirectoriesTreeView = new System.Windows.Forms.TreeView();
            this.btnLoadDirectory = new System.Windows.Forms.Button();
            this.btnSearchFile = new System.Windows.Forms.Button();
            this.textBoxSearchFile = new System.Windows.Forms.TextBox();
            this.listViewSearchResults = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // DirectoriesTreeView
            // 
            this.DirectoriesTreeView.Location = new System.Drawing.Point(26, 52);
            this.DirectoriesTreeView.Name = "DirectoriesTreeView";
            this.DirectoriesTreeView.Size = new System.Drawing.Size(258, 261);
            this.DirectoriesTreeView.TabIndex = 0;
            // 
            // btnLoadDirectory
            // 
            this.btnLoadDirectory.Location = new System.Drawing.Point(26, 12);
            this.btnLoadDirectory.Name = "btnLoadDirectory";
            this.btnLoadDirectory.Size = new System.Drawing.Size(102, 23);
            this.btnLoadDirectory.TabIndex = 1;
            this.btnLoadDirectory.Text = "Load Directory";
            this.btnLoadDirectory.UseVisualStyleBackColor = true;
            this.btnLoadDirectory.Click += new System.EventHandler(this.btnLoadDirectory_Click);
            // 
            // btnSearchFile
            // 
            this.btnSearchFile.Location = new System.Drawing.Point(343, 83);
            this.btnSearchFile.Name = "btnSearchFile";
            this.btnSearchFile.Size = new System.Drawing.Size(80, 23);
            this.btnSearchFile.TabIndex = 2;
            this.btnSearchFile.Text = "Search File";
            this.btnSearchFile.UseVisualStyleBackColor = true;
            this.btnSearchFile.Click += new System.EventHandler(this.btnSearchFile_Click);
            // 
            // textBoxSearchFile
            // 
            this.textBoxSearchFile.Location = new System.Drawing.Point(343, 52);
            this.textBoxSearchFile.Name = "textBoxSearchFile";
            this.textBoxSearchFile.Size = new System.Drawing.Size(80, 20);
            this.textBoxSearchFile.TabIndex = 3;
            // 
            // listViewSearchResults
            // 
            this.listViewSearchResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewSearchResults.FullRowSelect = true;
            this.listViewSearchResults.GridLines = true;
            this.listViewSearchResults.HideSelection = false;
            this.listViewSearchResults.Location = new System.Drawing.Point(456, 52);
            this.listViewSearchResults.Name = "listViewSearchResults";
            this.listViewSearchResults.Size = new System.Drawing.Size(612, 170);
            this.listViewSearchResults.TabIndex = 5;
            this.listViewSearchResults.UseCompatibleStateImageBehavior = false;
            this.listViewSearchResults.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 167;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Path";
            this.columnHeader2.Width = 196;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Size";
            this.columnHeader3.Width = 69;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Created";
            this.columnHeader4.Width = 70;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 343);
            this.Controls.Add(this.listViewSearchResults);
            this.Controls.Add(this.textBoxSearchFile);
            this.Controls.Add(this.btnSearchFile);
            this.Controls.Add(this.btnLoadDirectory);
            this.Controls.Add(this.DirectoriesTreeView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView DirectoriesTreeView;
        private System.Windows.Forms.Button btnLoadDirectory;
        private System.Windows.Forms.Button btnSearchFile;
        private System.Windows.Forms.TextBox textBoxSearchFile;
        private System.Windows.Forms.ListView listViewSearchResults;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}

