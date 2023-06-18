
namespace CameraRFID
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.btnCardRead = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.btnReadDevice = new System.Windows.Forms.Button();
            this.btnPost = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(484, 729);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // btnCardRead
            // 
            this.btnCardRead.Location = new System.Drawing.Point(385, 25);
            this.btnCardRead.Name = "btnCardRead";
            this.btnCardRead.Size = new System.Drawing.Size(75, 23);
            this.btnCardRead.TabIndex = 1;
            this.btnCardRead.Text = "카드읽기";
            this.btnCardRead.UseVisualStyleBackColor = true;
            this.btnCardRead.Click += new System.EventHandler(this.btnCardRead_Click);
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(312, 139);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(148, 218);
            this.tbLog.TabIndex = 2;
            // 
            // btnReadDevice
            // 
            this.btnReadDevice.Location = new System.Drawing.Point(385, 55);
            this.btnReadDevice.Name = "btnReadDevice";
            this.btnReadDevice.Size = new System.Drawing.Size(75, 23);
            this.btnReadDevice.TabIndex = 3;
            this.btnReadDevice.Text = "장치값읽기";
            this.btnReadDevice.UseVisualStyleBackColor = true;
            this.btnReadDevice.Click += new System.EventHandler(this.btnReadDevice_Click);
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(385, 85);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(75, 23);
            this.btnPost.TabIndex = 4;
            this.btnPost.Text = "서버전송";
            this.btnPost.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 729);
            this.Controls.Add(this.btnPost);
            this.Controls.Add(this.btnReadDevice);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.btnCardRead);
            this.Controls.Add(this.pictureBox);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnCardRead;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Button btnReadDevice;
        private System.Windows.Forms.Button btnPost;
    }
}

