/*
 * Created by SharpDevelop.
 * User: Anthony.Mason
 * Date: 5/7/2007
 * Time: 10:35 AM
 * 
 */
namespace SharpLauncher
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
					_manager.Disconnect();
					_manager.Dispose();
					if(_candidShotThread != null && _candidShotThread.ThreadState == System.Threading.ThreadState.Running)
						_candidShotThread.Abort();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStop = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnFire = new System.Windows.Forms.Button();
            this.btnUpRight = new System.Windows.Forms.Button();
            this.btnUpLeft = new System.Windows.Forms.Button();
            this.btnDownRight = new System.Windows.Forms.Button();
            this.btnDownLeft = new System.Windows.Forms.Button();
            this.chkSlow = new System.Windows.Forms.CheckBox();
            this.chkFire = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webcamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snapshotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snapPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discoverCommandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.snapPictureToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPrime = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStop.Location = new System.Drawing.Point(99, 135);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(56, 42);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStopClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 329);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(588, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(455, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnUp
            // 
            this.btnUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUp.BackgroundImage")));
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUp.Location = new System.Drawing.Point(99, 87);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(56, 42);
            this.btnUp.TabIndex = 9;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.BtnUpClick);
            // 
            // btnDown
            // 
            this.btnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDown.BackgroundImage")));
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDown.Location = new System.Drawing.Point(99, 183);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(56, 42);
            this.btnDown.TabIndex = 10;
            this.btnDown.UseVisualStyleBackColor = true;
            // 
            // btnRight
            // 
            this.btnRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRight.BackgroundImage")));
            this.btnRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRight.Location = new System.Drawing.Point(161, 135);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(56, 42);
            this.btnRight.TabIndex = 11;
            this.btnRight.UseVisualStyleBackColor = true;
            // 
            // btnLeft
            // 
            this.btnLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLeft.BackgroundImage")));
            this.btnLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnLeft.Location = new System.Drawing.Point(37, 135);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(56, 42);
            this.btnLeft.TabIndex = 12;
            this.btnLeft.UseVisualStyleBackColor = true;
            // 
            // btnFire
            // 
            this.btnFire.Location = new System.Drawing.Point(70, 239);
            this.btnFire.Name = "btnFire";
            this.btnFire.Size = new System.Drawing.Size(56, 24);
            this.btnFire.TabIndex = 13;
            this.btnFire.Text = "Fire";
            this.btnFire.UseVisualStyleBackColor = true;
            this.btnFire.Click += new System.EventHandler(this.BtnFireClick);
            // 
            // btnUpRight
            // 
            this.btnUpRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpRight.BackgroundImage")));
            this.btnUpRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUpRight.Location = new System.Drawing.Point(161, 87);
            this.btnUpRight.Name = "btnUpRight";
            this.btnUpRight.Size = new System.Drawing.Size(56, 42);
            this.btnUpRight.TabIndex = 14;
            this.btnUpRight.UseVisualStyleBackColor = true;
            // 
            // btnUpLeft
            // 
            this.btnUpLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpLeft.BackgroundImage")));
            this.btnUpLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUpLeft.Location = new System.Drawing.Point(37, 87);
            this.btnUpLeft.Name = "btnUpLeft";
            this.btnUpLeft.Size = new System.Drawing.Size(56, 42);
            this.btnUpLeft.TabIndex = 15;
            this.btnUpLeft.UseVisualStyleBackColor = true;
            // 
            // btnDownRight
            // 
            this.btnDownRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDownRight.BackgroundImage")));
            this.btnDownRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDownRight.Location = new System.Drawing.Point(161, 183);
            this.btnDownRight.Name = "btnDownRight";
            this.btnDownRight.Size = new System.Drawing.Size(56, 42);
            this.btnDownRight.TabIndex = 16;
            this.btnDownRight.UseVisualStyleBackColor = true;
            // 
            // btnDownLeft
            // 
            this.btnDownLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDownLeft.BackgroundImage")));
            this.btnDownLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDownLeft.Location = new System.Drawing.Point(37, 183);
            this.btnDownLeft.Name = "btnDownLeft";
            this.btnDownLeft.Size = new System.Drawing.Size(56, 42);
            this.btnDownLeft.TabIndex = 17;
            this.btnDownLeft.UseVisualStyleBackColor = true;
            // 
            // chkSlow
            // 
            this.chkSlow.Location = new System.Drawing.Point(31, 48);
            this.chkSlow.Name = "chkSlow";
            this.chkSlow.Size = new System.Drawing.Size(104, 24);
            this.chkSlow.TabIndex = 18;
            this.chkSlow.Text = "Slow Motion";
            this.chkSlow.UseVisualStyleBackColor = true;
            this.chkSlow.CheckedChanged += new System.EventHandler(this.ChkSlowCheckedChanged);
            // 
            // chkFire
            // 
            this.chkFire.Location = new System.Drawing.Point(151, 43);
            this.chkFire.Name = "chkFire";
            this.chkFire.Size = new System.Drawing.Size(104, 35);
            this.chkFire.TabIndex = 19;
            this.chkFire.Text = "Attempt Fire While Moving";
            this.chkFire.UseVisualStyleBackColor = true;
            this.chkFire.CheckedChanged += new System.EventHandler(this.ChkFireCheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.launchersToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(588, 24);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.webcamToolStripMenuItem,
            this.snapshotsToolStripMenuItem,
            this.logToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // webcamToolStripMenuItem
            // 
            this.webcamToolStripMenuItem.CheckOnClick = true;
            this.webcamToolStripMenuItem.Name = "webcamToolStripMenuItem";
            this.webcamToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.webcamToolStripMenuItem.Text = "Webcam";
            this.webcamToolStripMenuItem.Click += new System.EventHandler(this.WebcamToolStripMenuItemClick);
            // 
            // snapshotsToolStripMenuItem
            // 
            this.snapshotsToolStripMenuItem.Name = "snapshotsToolStripMenuItem";
            this.snapshotsToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.snapshotsToolStripMenuItem.Text = "Snapshots";
            this.snapshotsToolStripMenuItem.Click += new System.EventHandler(this.SnapshotsToolStripMenuItemClick);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.logToolStripMenuItem.Text = "Log";
            this.logToolStripMenuItem.Click += new System.EventHandler(this.LogToolStripMenuItemClick);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.snapPictureToolStripMenuItem,
            this.discoverCommandsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItemClick);
            // 
            // snapPictureToolStripMenuItem
            // 
            this.snapPictureToolStripMenuItem.Name = "snapPictureToolStripMenuItem";
            this.snapPictureToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.snapPictureToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.snapPictureToolStripMenuItem.Text = "Snap Picture";
            this.snapPictureToolStripMenuItem.Click += new System.EventHandler(this.snapPictureToolStripMenuItem_Click);
            // 
            // discoverCommandsToolStripMenuItem
            // 
            this.discoverCommandsToolStripMenuItem.Name = "discoverCommandsToolStripMenuItem";
            this.discoverCommandsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.discoverCommandsToolStripMenuItem.Text = "Discover Commands";
            this.discoverCommandsToolStripMenuItem.Click += new System.EventHandler(this.DiscoverCommandsToolStripMenuItemClick);
            // 
            // launchersToolStripMenuItem
            // 
            this.launchersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reconnectToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.toolStripSeparator1});
            this.launchersToolStripMenuItem.Name = "launchersToolStripMenuItem";
            this.launchersToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.launchersToolStripMenuItem.Text = "Launchers";
            // 
            // reconnectToolStripMenuItem
            // 
            this.reconnectToolStripMenuItem.Name = "reconnectToolStripMenuItem";
            this.reconnectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.reconnectToolStripMenuItem.Text = "Reconnect";
            this.reconnectToolStripMenuItem.Click += new System.EventHandler(this.ReconnectToolStripMenuItemClick);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.DisconnectToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controlsToolStripMenuItem,
            this.donateToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // controlsToolStripMenuItem
            // 
            this.controlsToolStripMenuItem.Name = "controlsToolStripMenuItem";
            this.controlsToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.controlsToolStripMenuItem.Text = "Controls";
            this.controlsToolStripMenuItem.Click += new System.EventHandler(this.ControlsToolStripMenuItemClick);
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.donateToolStripMenuItem.Text = "Donate";
            this.donateToolStripMenuItem.Click += new System.EventHandler(this.DonateToolStripMenuItemClick);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(278, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(274, 220);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "SharpLauncher";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.snapPictureToolStripMenuItem1,
            this.restoreToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(141, 70);
            // 
            // snapPictureToolStripMenuItem1
            // 
            this.snapPictureToolStripMenuItem1.Name = "snapPictureToolStripMenuItem1";
            this.snapPictureToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.snapPictureToolStripMenuItem1.Text = "Snap Picture";
            this.snapPictureToolStripMenuItem1.Click += new System.EventHandler(this.SnapPictureToolStripMenuItem1Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.RestoreToolStripMenuItemClick);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.ExitToolStripMenuItem1Click);
            // 
            // btnPrime
            // 
            this.btnPrime.Location = new System.Drawing.Point(132, 239);
            this.btnPrime.Name = "btnPrime";
            this.btnPrime.Size = new System.Drawing.Size(56, 24);
            this.btnPrime.TabIndex = 22;
            this.btnPrime.Text = "Prime";
            this.btnPrime.UseVisualStyleBackColor = true;
            this.btnPrime.Click += new System.EventHandler(this.BtnPrimeClick);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(0, 280);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(588, 46);
            this.txtLog.TabIndex = 23;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnStop;
            this.ClientSize = new System.Drawing.Size(588, 351);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnPrime);
            this.Controls.Add(this.chkFire);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkSlow);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnFire);
            this.Controls.Add(this.btnDownLeft);
            this.Controls.Add(this.btnDownRight);
            this.Controls.Add(this.btnUpLeft);
            this.Controls.Add(this.btnUpRight);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnStop);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reconnectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem launchersToolStripMenuItem;
		private System.Windows.Forms.Button btnPrime;
		private System.Windows.Forms.ToolStripMenuItem discoverCommandsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem snapPictureToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem snapshotsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem controlsToolStripMenuItem;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem webcamToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.CheckBox chkFire;
		private System.Windows.Forms.CheckBox chkSlow;
		private System.Windows.Forms.Button btnDownLeft;
		private System.Windows.Forms.Button btnDownRight;
		private System.Windows.Forms.Button btnUpLeft;
		private System.Windows.Forms.Button btnUpRight;
		private System.Windows.Forms.Button btnFire;
		private System.Windows.Forms.Button btnLeft;
		private System.Windows.Forms.Button btnRight;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ToolStripMenuItem snapPictureToolStripMenuItem;
        private System.Windows.Forms.TextBox txtLog;

	}
}
