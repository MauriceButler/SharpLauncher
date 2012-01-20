/*
 * Created by SharpDevelop.
 * User: Anthony.Mason
 * Date: 5/8/2007
 * Time: 8:14 AM
 * 
 */
namespace SharpLauncher
{
	partial class SettingsForm
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
			this.label2 = new System.Windows.Forms.Label();
			this.txtRefreshRate = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtCandidShotsMS = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtCandidShots = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtCamHeight = new System.Windows.Forms.TextBox();
			this.txtCamWidth = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkPrime = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			this.btnDefault = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(152, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Webcam Refresh Rate (ms):";
			// 
			// txtRefreshRate
			// 
			this.txtRefreshRate.Location = new System.Drawing.Point(200, 16);
			this.txtRefreshRate.Name = "txtRefreshRate";
			this.txtRefreshRate.Size = new System.Drawing.Size(55, 20);
			this.txtRefreshRate.TabIndex = 3;
			this.txtRefreshRate.TextChanged += new System.EventHandler(this.txtRefreshRate_TextChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.txtCandidShotsMS);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.txtCandidShots);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtCamHeight);
			this.groupBox1.Controls.Add(this.txtCamWidth);
			this.groupBox1.Controls.Add(this.txtRefreshRate);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(273, 152);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Webcam Options";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(7, 123);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(167, 17);
			this.label7.TabIndex = 11;
			this.label7.Text = "Time Between Candid Shots(ms):";
			// 
			// txtCandidShotsMS
			// 
			this.txtCandidShotsMS.Location = new System.Drawing.Point(200, 120);
			this.txtCandidShotsMS.Name = "txtCandidShotsMS";
			this.txtCandidShotsMS.Size = new System.Drawing.Size(55, 20);
			this.txtCandidShotsMS.TabIndex = 10;
			this.txtCandidShotsMS.TextChanged += new System.EventHandler(this.TxtCandidShotsMSTextChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(7, 97);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(152, 17);
			this.label6.TabIndex = 9;
			this.label6.Text = "Candid Shots After Fire:";
			// 
			// txtCandidShots
			// 
			this.txtCandidShots.Location = new System.Drawing.Point(200, 94);
			this.txtCandidShots.Name = "txtCandidShots";
			this.txtCandidShots.Size = new System.Drawing.Size(55, 20);
			this.txtCandidShots.TabIndex = 8;
			this.txtCandidShots.TextChanged += new System.EventHandler(this.TxtCandidShotsTextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7, 71);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(152, 17);
			this.label5.TabIndex = 7;
			this.label5.Text = "Wecam Height(px):";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7, 45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 20);
			this.label1.TabIndex = 6;
			this.label1.Text = "Webcam Width(px):";
			// 
			// txtCamHeight
			// 
			this.txtCamHeight.Location = new System.Drawing.Point(200, 68);
			this.txtCamHeight.Name = "txtCamHeight";
			this.txtCamHeight.Size = new System.Drawing.Size(55, 20);
			this.txtCamHeight.TabIndex = 5;
			this.txtCamHeight.TextChanged += new System.EventHandler(this.TxtCamHeightTextChanged);
			// 
			// txtCamWidth
			// 
			this.txtCamWidth.Location = new System.Drawing.Point(200, 42);
			this.txtCamWidth.Name = "txtCamWidth";
			this.txtCamWidth.Size = new System.Drawing.Size(55, 20);
			this.txtCamWidth.TabIndex = 4;
			this.txtCamWidth.TextChanged += new System.EventHandler(this.TxtCamWidthTextChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkPrime);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Location = new System.Drawing.Point(12, 170);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(273, 47);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Rocket Launcher Options";
			// 
			// chkPrime
			// 
			this.chkPrime.Location = new System.Drawing.Point(201, 16);
			this.chkPrime.Name = "chkPrime";
			this.chkPrime.Size = new System.Drawing.Size(34, 24);
			this.chkPrime.TabIndex = 3;
			this.chkPrime.UseVisualStyleBackColor = true;
			this.chkPrime.CheckedChanged += new System.EventHandler(this.ChkPrimeCheckedChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(7, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(187, 23);
			this.label4.TabIndex = 2;
			this.label4.Text = "Prime Air Tank After Fire?";
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// btnDefault
			// 
			this.btnDefault.Location = new System.Drawing.Point(154, 223);
			this.btnDefault.Name = "btnDefault";
			this.btnDefault.Size = new System.Drawing.Size(75, 23);
			this.btnDefault.TabIndex = 6;
			this.btnDefault.Text = "Default";
			this.btnDefault.UseVisualStyleBackColor = true;
			this.btnDefault.Click += new System.EventHandler(this.BtnDefaultClick);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(73, 223);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 7;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.BtnOKClick);
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(297, 261);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnDefault);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "SettingsForm";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.TextBox txtCandidShots;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtCandidShotsMS;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnDefault;
		private System.Windows.Forms.TextBox txtCamWidth;
		private System.Windows.Forms.TextBox txtCamHeight;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox chkPrime;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.TextBox txtRefreshRate;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
	}
}
