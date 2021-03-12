
namespace Services_Industry_Simulation.Loader
{
    partial class LoaderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FileDialog_OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.button_SelectFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FileDialog_OpenFile
            // 
            this.FileDialog_OpenFile.FileName = "model.png";
            this.FileDialog_OpenFile.Filter = "Images (*.png)|*.png";
            // 
            // button_SelectFile
            // 
            this.button_SelectFile.Location = new System.Drawing.Point(16, 15);
            this.button_SelectFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_SelectFile.Name = "button_SelectFile";
            this.button_SelectFile.Size = new System.Drawing.Size(156, 73);
            this.button_SelectFile.TabIndex = 0;
            this.button_SelectFile.Text = "Select File";
            this.button_SelectFile.UseVisualStyleBackColor = true;
            this.button_SelectFile.Click += new System.EventHandler(this.button_SelectFile_Click);
            // 
            // LoaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.button_SelectFile);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LoaderForm";
            this.Text = "LoaderForm";
            this.Load += new System.EventHandler(this.LoaderForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog FileDialog_OpenFile;
        private System.Windows.Forms.Button button_SelectFile;
    }
}