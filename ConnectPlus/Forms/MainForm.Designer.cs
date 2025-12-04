namespace ConnectPlus.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelVideo;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnLoop;
        private System.Windows.Forms.TrackBar trackVolume;
        private System.Windows.Forms.Button btnStartVirtualCamera;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Label lblVirtualCameraStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timerStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelVideo = new System.Windows.Forms.Panel();
            this.panelControls = new System.Windows.Forms.Panel();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnLoop = new System.Windows.Forms.Button();
            this.trackVolume = new System.Windows.Forms.TrackBar();
            this.btnStartVirtualCamera = new System.Windows.Forms.Button();
            this.lblVolume = new System.Windows.Forms.Label();
            this.lblVirtualCameraStatus = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // panelVideo
            // 
            this.panelVideo.BackColor = System.Drawing.Color.Black;
            this.panelVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVideo.Location = new System.Drawing.Point(0, 0);
            this.panelVideo.Name = "panelVideo";
            this.panelVideo.Size = new System.Drawing.Size(1200, 600);
            this.panelVideo.TabIndex = 0;
            // 
            // panelControls
            // 
            this.panelControls.BackColor = System.Drawing.Color.FromArgb(20, 22, 35);
            this.panelControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelControls.Controls.Add(this.lblStatus);
            this.panelControls.Controls.Add(this.lblVirtualCameraStatus);
            this.panelControls.Controls.Add(this.lblVolume);
            this.panelControls.Controls.Add(this.btnStartVirtualCamera);
            this.panelControls.Controls.Add(this.trackVolume);
            this.panelControls.Controls.Add(this.btnLoop);
            this.panelControls.Controls.Add(this.btnPause);
            this.panelControls.Controls.Add(this.btnPlay);
            this.panelControls.Controls.Add(this.btnOpenFile);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControls.Location = new System.Drawing.Point(0, 600);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(1200, 120);
            this.panelControls.TabIndex = 1;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.BackColor = System.Drawing.Color.FromArgb(123, 47, 247);
            this.btnOpenFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 234, 255);
            this.btnOpenFile.FlatAppearance.BorderSize = 2;
            this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenFile.ForeColor = System.Drawing.Color.White;
            this.btnOpenFile.Location = new System.Drawing.Point(20, 20);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(120, 35);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "OPEN FILE";
            this.btnOpenFile.UseVisualStyleBackColor = false;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(123, 47, 247);
            this.btnPlay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 234, 255);
            this.btnPlay.FlatAppearance.BorderSize = 2;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.Location = new System.Drawing.Point(160, 20);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(100, 35);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "PLAY";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.FromArgb(123, 47, 247);
            this.btnPause.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 234, 255);
            this.btnPause.FlatAppearance.BorderSize = 2;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPause.ForeColor = System.Drawing.Color.White;
            this.btnPause.Location = new System.Drawing.Point(280, 20);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(100, 35);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "PAUSE";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            this.btnPause.Enabled = false;
            // 
            // btnLoop
            // 
            this.btnLoop.BackColor = System.Drawing.Color.FromArgb(123, 47, 247);
            this.btnLoop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 234, 255);
            this.btnLoop.FlatAppearance.BorderSize = 2;
            this.btnLoop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLoop.ForeColor = System.Drawing.Color.White;
            this.btnLoop.Location = new System.Drawing.Point(400, 20);
            this.btnLoop.Name = "btnLoop";
            this.btnLoop.Size = new System.Drawing.Size(120, 35);
            this.btnLoop.TabIndex = 3;
            this.btnLoop.Text = "LOOP: OFF";
            this.btnLoop.UseVisualStyleBackColor = false;
            this.btnLoop.Click += new System.EventHandler(this.btnLoop_Click);
            // 
            // trackVolume
            // 
            this.trackVolume.BackColor = System.Drawing.Color.FromArgb(20, 22, 35);
            this.trackVolume.Location = new System.Drawing.Point(550, 20);
            this.trackVolume.Maximum = 100;
            this.trackVolume.Name = "trackVolume";
            this.trackVolume.Size = new System.Drawing.Size(200, 45);
            this.trackVolume.TabIndex = 4;
            this.trackVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackVolume.Value = 50;
            this.trackVolume.Scroll += new System.EventHandler(this.trackVolume_Scroll);
            // 
            // btnStartVirtualCamera
            // 
            this.btnStartVirtualCamera.BackColor = System.Drawing.Color.FromArgb(123, 47, 247);
            this.btnStartVirtualCamera.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 234, 255);
            this.btnStartVirtualCamera.FlatAppearance.BorderSize = 2;
            this.btnStartVirtualCamera.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartVirtualCamera.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStartVirtualCamera.ForeColor = System.Drawing.Color.White;
            this.btnStartVirtualCamera.Location = new System.Drawing.Point(780, 20);
            this.btnStartVirtualCamera.Name = "btnStartVirtualCamera";
            this.btnStartVirtualCamera.Size = new System.Drawing.Size(200, 35);
            this.btnStartVirtualCamera.TabIndex = 5;
            this.btnStartVirtualCamera.Text = "START VIRTUAL CAMERA";
            this.btnStartVirtualCamera.UseVisualStyleBackColor = false;
            this.btnStartVirtualCamera.Click += new System.EventHandler(this.btnStartVirtualCamera_Click);
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVolume.ForeColor = System.Drawing.Color.FromArgb(0, 234, 255);
            this.lblVolume.Location = new System.Drawing.Point(550, 65);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(65, 15);
            this.lblVolume.TabIndex = 6;
            this.lblVolume.Text = "Volume: 50%";
            // 
            // lblVirtualCameraStatus
            // 
            this.lblVirtualCameraStatus.AutoSize = true;
            this.lblVirtualCameraStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVirtualCameraStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblVirtualCameraStatus.Location = new System.Drawing.Point(780, 65);
            this.lblVirtualCameraStatus.Name = "lblVirtualCameraStatus";
            this.lblVirtualCameraStatus.Size = new System.Drawing.Size(123, 15);
            this.lblVirtualCameraStatus.TabIndex = 7;
            this.lblVirtualCameraStatus.Text = "Virtual Camera: OFF";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(0, 234, 255);
            this.lblStatus.Location = new System.Drawing.Point(20, 90);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(38, 15);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Ready";
            // 
            // timerStatus
            // 
            this.timerStatus.Enabled = true;
            this.timerStatus.Interval = 500;
            this.timerStatus.Tick += new System.EventHandler(this.timerStatus_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(13, 15, 26);
            this.ClientSize = new System.Drawing.Size(1200, 720);
            this.Controls.Add(this.panelVideo);
            this.Controls.Add(this.panelControls);
            this.Name = "MainForm";
            this.Text = "Connect+";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackVolume)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

