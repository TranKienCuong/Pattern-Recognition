namespace PatternRecognition
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.recognizeButton = new System.Windows.Forms.Button();
            this.picture = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.browsingButton = new System.Windows.Forms.Button();
            this.learningButton = new System.Windows.Forms.Button();
            this.resultBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // recognizeButton
            // 
            this.recognizeButton.Location = new System.Drawing.Point(255, 103);
            this.recognizeButton.Name = "recognizeButton";
            this.recognizeButton.Size = new System.Drawing.Size(97, 48);
            this.recognizeButton.TabIndex = 1;
            this.recognizeButton.Text = "Recognize";
            this.recognizeButton.UseVisualStyleBackColor = true;
            this.recognizeButton.Click += new System.EventHandler(this.recognizeButton_Click);
            // 
            // picture
            // 
            this.picture.BackColor = System.Drawing.Color.White;
            this.picture.Image = ((System.Drawing.Image)(resources.GetObject("picture.Image")));
            this.picture.Location = new System.Drawing.Point(35, 36);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(67, 53);
            this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picture.TabIndex = 2;
            this.picture.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // browsingButton
            // 
            this.browsingButton.Location = new System.Drawing.Point(255, 36);
            this.browsingButton.Name = "browsingButton";
            this.browsingButton.Size = new System.Drawing.Size(97, 45);
            this.browsingButton.TabIndex = 3;
            this.browsingButton.Text = "Browse...";
            this.browsingButton.UseVisualStyleBackColor = true;
            this.browsingButton.Click += new System.EventHandler(this.browsingButton_Click);
            // 
            // learningButton
            // 
            this.learningButton.Location = new System.Drawing.Point(35, 170);
            this.learningButton.Name = "learningButton";
            this.learningButton.Size = new System.Drawing.Size(75, 20);
            this.learningButton.TabIndex = 4;
            this.learningButton.Text = "Learn";
            this.learningButton.UseVisualStyleBackColor = true;
            this.learningButton.Click += new System.EventHandler(this.learningButton_Click);
            // 
            // resultBox
            // 
            this.resultBox.Location = new System.Drawing.Point(133, 170);
            this.resultBox.Name = "resultBox";
            this.resultBox.Size = new System.Drawing.Size(83, 20);
            this.resultBox.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 198);
            this.Controls.Add(this.resultBox);
            this.Controls.Add(this.learningButton);
            this.Controls.Add(this.browsingButton);
            this.Controls.Add(this.picture);
            this.Controls.Add(this.recognizeButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button recognizeButton;
        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button browsingButton;
        private System.Windows.Forms.Button learningButton;
        private System.Windows.Forms.TextBox resultBox;
    }
}

