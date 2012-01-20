/*
 * Created by SharpDevelop.
 * User: Anthony
 * Date: 5/17/2007
 * Time: 1:29 AM
 * 
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using LibUSBLauncher;

namespace SharpLauncher
{
	/// <summary>
	/// Opens a form that allows viewing / deleting of snapshots taken from the webcam
	/// </summary>
	public partial class ViewSnapshotsForm : BaseForm
	{
		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public ViewSnapshotsForm()
		{
			InitializeComponent();
			this.Refresh();
		}
		#endregion
		
		#region Event Handlers
		/// <summary>
		/// Called when the list box is clicked and a new selection is made
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void LstImagesSelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string filename = this.lstImages.SelectedItem.ToString();
				Image image = Image.FromFile("images\\" + filename);
				this.pictureBox1.Image = image;
			}
			catch(Exception excep)
			{
				Log.Instance.Out(excep);
			}
		}
		
		/// <summary>
		/// Called when the delete button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnDeleteClick(object sender, EventArgs e)
		{
			if(this.lstImages.SelectedItem  == null)
				return;
			
			string filename = this.lstImages.SelectedItem.ToString();
			FileInfo info = new FileInfo("images\\" + filename);
			
			if(info.Exists)
			{
				if(MessageBox.Show("Delete " + filename + "?","Confirm",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation) == DialogResult.OK)
				{
					try
					{
						this.pictureBox1.Image.Dispose();
						info.Delete();
					}
					catch(Exception excep)
					{
						MessageBox.Show("Could not delete file: \n\t" + excep.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
				}
				else
					return;

			}
			
			this.Refresh();
		}
		#endregion
		
		#region Public overrides
		/// <summary>
		/// Refreshes the list box by clearing it and reloading it from all files in the 
		/// images directory.
		/// </summary>
		public override void Refresh()
		{
			this.lstImages.Items.Clear();
			DirectoryInfo info = new DirectoryInfo("images");
			if(!info.Exists)
				return;
			
			FileInfo[] files = info.GetFiles("*.jpg");
			foreach(FileInfo file in files)
			{
				this.lstImages.Items.Add(file.Name);
			}
		}
		#endregion
	}
}
