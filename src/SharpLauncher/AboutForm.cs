/*
 * Created by SharpDevelop.
 * User: Anthony.Mason
 * Date: 5/8/2007
 * Time: 8:14 AM
 * 
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using System.Reflection;

namespace SharpLauncher
{
	/// <summary>
	/// The about box for the application.
	/// </summary>
	public partial class AboutForm : BaseForm
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public AboutForm()
		{
			InitializeComponent();
			this.lblVersion.Text = Assembly.GetExecutingAssembly().GetName(false).Version.ToString();
		}
		
		/// <summary>
		/// Opens the systems default browser for the url on the about box.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.antmason.com");
		}
	}
}
