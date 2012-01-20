/*
 * Created by SharpDevelop.
 * User: Anthony.Mason
 * Date: 5/22/2007
 * Time: 8:38 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SharpLauncher
{
	partial class DiscoveryForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiscoveryForm));
			this.btnPerform = new System.Windows.Forms.Button();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.lblAction = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnStop = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// btnPerform
			// 
			this.btnPerform.Location = new System.Drawing.Point(80, 172);
			this.btnPerform.Name = "btnPerform";
			this.btnPerform.Size = new System.Drawing.Size(53, 23);
			this.btnPerform.TabIndex = 8;
			this.btnPerform.Text = "Send";
			this.btnPerform.UseVisualStyleBackColor = true;
			this.btnPerform.Click += new System.EventHandler(this.BtnPerformClick);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(151, 134);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(71, 20);
			this.numericUpDown1.TabIndex = 10;
			// 
			// lblAction
			// 
			this.lblAction.Location = new System.Drawing.Point(29, 136);
			this.lblAction.Name = "lblAction";
			this.lblAction.Size = new System.Drawing.Size(104, 20);
			this.lblAction.TabIndex = 9;
			this.lblAction.Text = "Hexidecimal Value:";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(100, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 18);
			this.label1.TabIndex = 11;
			this.label1.Text = "Warning!";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 27);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(236, 93);
			this.label2.TabIndex = 12;
			this.label2.Text = resources.GetString("label2.Text");
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(141, 172);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(53, 23);
			this.btnStop.TabIndex = 13;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// DiscoveryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 211);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnPerform);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.lblAction);
			this.Name = "DiscoveryForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblAction;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Button btnPerform;
	}
}
