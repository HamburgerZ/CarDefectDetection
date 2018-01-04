namespace SoftwareTrigger
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSoftwareTrigger = new System.Windows.Forms.Button();
            this.pbImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(67, 23);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "打开相机";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(261, 23);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭相机";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSoftwareTrigger
            // 
            this.btnSoftwareTrigger.Location = new System.Drawing.Point(478, 23);
            this.btnSoftwareTrigger.Name = "btnSoftwareTrigger";
            this.btnSoftwareTrigger.Size = new System.Drawing.Size(119, 23);
            this.btnSoftwareTrigger.TabIndex = 3;
            this.btnSoftwareTrigger.Text = "二值化";
            this.btnSoftwareTrigger.UseVisualStyleBackColor = true;
            this.btnSoftwareTrigger.Click += new System.EventHandler(this.btnSoftwareTrigger_Click);
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(27, 72);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(800, 600);
            this.pbImage.TabIndex = 4;
            this.pbImage.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 741);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.btnSoftwareTrigger);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Name = "Form1";
            this.Text = "汽车转向节的缺陷假侧";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSoftwareTrigger;
        private System.Windows.Forms.PictureBox pbImage;
    }
}

