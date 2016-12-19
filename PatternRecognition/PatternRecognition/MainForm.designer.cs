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
            this.pictureRecognition = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.browsingButton = new System.Windows.Forms.Button();
            this.learningButton = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.browseFolderButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureRecognition)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // recognizeButton
            // 
            this.recognizeButton.Location = new System.Drawing.Point(280, 104);
            this.recognizeButton.Name = "recognizeButton";
            this.recognizeButton.Size = new System.Drawing.Size(97, 48);
            this.recognizeButton.TabIndex = 1;
            this.recognizeButton.Text = "Recognize";
            this.recognizeButton.UseVisualStyleBackColor = true;
            this.recognizeButton.Click += new System.EventHandler(this.recognizeButton_Click);
            // 
            // pictureRecognition
            // 
            this.pictureRecognition.BackColor = System.Drawing.Color.White;
            this.pictureRecognition.Image = ((System.Drawing.Image)(resources.GetObject("pictureRecognition.Image")));
            this.pictureRecognition.Location = new System.Drawing.Point(28, 37);
            this.pictureRecognition.Name = "pictureRecognition";
            this.pictureRecognition.Size = new System.Drawing.Size(67, 53);
            this.pictureRecognition.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureRecognition.TabIndex = 2;
            this.pictureRecognition.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // browsingButton
            // 
            this.browsingButton.Location = new System.Drawing.Point(280, 37);
            this.browsingButton.Name = "browsingButton";
            this.browsingButton.Size = new System.Drawing.Size(97, 45);
            this.browsingButton.TabIndex = 3;
            this.browsingButton.Text = "Browse picture";
            this.browsingButton.UseVisualStyleBackColor = true;
            this.browsingButton.Click += new System.EventHandler(this.browsingButton_Click);
            // 
            // learningButton
            // 
            this.learningButton.Location = new System.Drawing.Point(280, 83);
            this.learningButton.Name = "learningButton";
            this.learningButton.Size = new System.Drawing.Size(110, 32);
            this.learningButton.TabIndex = 4;
            this.learningButton.Text = "Learn";
            this.learningButton.UseVisualStyleBackColor = true;
            this.learningButton.Click += new System.EventHandler(this.learningButton_Click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(50, 38);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(214, 20);
            this.pathTextBox.TabIndex = 6;
            // 
            // browseFolderButton
            // 
            this.browseFolderButton.Location = new System.Drawing.Point(280, 33);
            this.browseFolderButton.Name = "browseFolderButton";
            this.browseFolderButton.Size = new System.Drawing.Size(110, 28);
            this.browseFolderButton.TabIndex = 7;
            this.browseFolderButton.Text = "Browse folder";
            this.browseFolderButton.UseVisualStyleBackColor = true;
            this.browseFolderButton.Click += new System.EventHandler(this.browseFolderButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Path :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureRecognition);
            this.groupBox1.Controls.Add(this.recognizeButton);
            this.groupBox1.Controls.Add(this.browsingButton);
            this.groupBox1.Location = new System.Drawing.Point(18, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 173);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recognition";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.learningButton);
            this.groupBox2.Controls.Add(this.pathTextBox);
            this.groupBox2.Controls.Add(this.browseFolderButton);
            this.groupBox2.Location = new System.Drawing.Point(18, 191);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 139);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Learning";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.SelectedPath = "D:\\Documents\\Visual Studio 2015\\Projects\\PatternRecognition\\PatternRecognition\\Sa" +
    "mples";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 345);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Pattern Recognition";
            ((System.ComponentModel.ISupportInitialize)(this.pictureRecognition)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button recognizeButton;
        private System.Windows.Forms.PictureBox pictureRecognition;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button browsingButton;
        private System.Windows.Forms.Button learningButton;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button browseFolderButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

