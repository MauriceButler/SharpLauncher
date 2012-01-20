/*
 * Created by SharpDevelop.
 * User: Anthony.Mason
 * Date: 5/22/2007
 * Time: 8:38 AM
 * 
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using LibUSBLauncher;

namespace SharpLauncher
{
	/// <summary>
	/// Form that allows you to send custom hex codes for command discovery.
	/// </summary>
	public partial class DiscoveryForm : BaseForm
	{
		#region Data
		private LauncherManager _manager;
		#endregion
		
		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="manager"></param>
		public DiscoveryForm(LauncherManager manager)
		{
			InitializeComponent();
			_manager = manager;
			this.numericUpDown1.Hexadecimal = true;
		}
		#endregion
		
		#region Event Handlers
		/// <summary>
		/// Called when Perform button is clicked.
		/// Sends a custom hexidecimal command to the rocket launcher.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnPerformClick(object sender, EventArgs e)
		{
			int action = (int)this.numericUpDown1.Value;

			foreach(USBLauncher l in _manager.Launchers)
				l.PerformCustomCommand(action);
		}
		
		/// <summary>
		/// Called when Stop button is click. 
		/// This sends the kill command to whatever command is currently
		/// running on the rocket launcher
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnStopClick(object sender, EventArgs e)
		{
			foreach(USBLauncher l in _manager.Launchers)
				l.Stop();
		}
		#endregion
	}
}
