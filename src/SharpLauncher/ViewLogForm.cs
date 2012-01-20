/*
 * Created by SharpDevelop.
 * User: Anthony
 * Date: 5/17/2007
 * Time: 12:55 AM
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
	/// Opens a form that allows the viewing of the log file.
	/// </summary>
	public partial class ViewLogForm : BaseForm
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ViewLogForm()
		{
			InitializeComponent();
			try
			{	
				TextReader reader = TextReader.Synchronized(new StreamReader(Log.Filename));
				this.txtLog.Text = reader.ReadToEnd();	
				reader.Close();
			}
			catch(Exception e)
			{
				Log.Instance.Out(e);
				this.txtLog.Text = "Log file not found.";
			}
		}
	}
}
