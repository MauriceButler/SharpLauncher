/*
 * Created by SharpDevelop.
 * User: Anthony.Mason
 * Date: 5/8/2007
 * Time: 8:15 AM
 * 
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharpLauncher
{
	/// <summary>
	/// BaseForm for other forms to inherit from.
	/// </summary>
	public partial class BaseForm : Form
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public BaseForm()
		{
			InitializeComponent();
		}
	}
}
