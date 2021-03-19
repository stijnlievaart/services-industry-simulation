
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
            this.MaskBox = new System.Windows.Forms.ComboBox();
            this.Confirm_Button = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // FileDialog_OpenFile
            // 
            this.FileDialog_OpenFile.FileName = "model.png";
            this.FileDialog_OpenFile.Filter = "Images (*.png)|*.png";
            // 
            // button_SelectFile
            // 
            this.button_SelectFile.Location = new System.Drawing.Point(12, 12);
            this.button_SelectFile.Name = "button_SelectFile";
            this.button_SelectFile.Size = new System.Drawing.Size(129, 59);
            this.button_SelectFile.TabIndex = 0;
            this.button_SelectFile.Text = "Select File";
            this.button_SelectFile.UseVisualStyleBackColor = true;
            this.button_SelectFile.Click += new System.EventHandler(this.button_SelectFile_Click);
            // 
            // MaskBox
            // 
            this.MaskBox.FormattingEnabled = true;
            this.MaskBox.Items.AddRange(new object[] {
            "No Masks",
            "Non-Medical Masks",
            "N-95 Masks"});
            this.MaskBox.Location = new System.Drawing.Point(12, 77);
            this.MaskBox.Name = "MaskBox";
            this.MaskBox.Size = new System.Drawing.Size(129, 21);
            this.MaskBox.TabIndex = 8;
            this.MaskBox.SelectedIndexChanged += new System.EventHandler(this.MaskBox_SelectedIndexChanged);
            // 
            // Confirm_Button
            // 
            this.Confirm_Button.Location = new System.Drawing.Point(12, 379);
            this.Confirm_Button.Name = "Confirm_Button";
            this.Confirm_Button.Size = new System.Drawing.Size(129, 59);
            this.Confirm_Button.TabIndex = 9;
            this.Confirm_Button.Text = "Confirm";
            this.Confirm_Button.UseVisualStyleBackColor = true;
            this.Confirm_Button.Click += new System.EventHandler(this.Confirm_Button_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(169, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(822, 426);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // LoaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Confirm_Button);
            this.Controls.Add(this.MaskBox);
            this.Controls.Add(this.button_SelectFile);
            this.Name = "LoaderForm";
            this.Text = "LoaderForm";
            this.Load += new System.EventHandler(this.LoaderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog FileDialog_OpenFile;
        private System.Windows.Forms.Button button_SelectFile;
        private System.Windows.Forms.ComboBox MaskBox;
        private System.Windows.Forms.Button Confirm_Button;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}