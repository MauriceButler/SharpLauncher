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
using System.Configuration;
using LibUSBLauncher;

namespace SharpLauncher
{
	/// <summary>
	/// Form to change the settings in the application.
	/// </summary>
	public partial class SettingsForm : BaseForm
	{
		#region Private Data
		private Configuration config = null;
		private bool _cameraSettingsChanged = false;
		#endregion
		
		#region Constructor
		/// <summary>
		/// Construcor.
		/// </summary>
		/// <param name="conf">The configuration file passed in from the main form.</param>
		public SettingsForm(Configuration conf)
		{
			InitializeComponent();
			config = conf;
			this.Refresh();
		}
		#endregion
		
		#region Event Handlers
		/// <summary>
		/// Called when the Refresh Rate text box value changes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void txtRefreshRate_TextChanged(object sender, EventArgs e)
		{
			int num;
			try
			{
				num = Convert.ToInt32(this.txtRefreshRate.Text);
				this.errorProvider1.SetError(this.txtRefreshRate,"");
			}
			catch(Exception e1)
			{
				num = 20;
				this.errorProvider1.SetError(this.txtRefreshRate,"This is not a valid value");
				Log.Instance.Out(e1);
			}
			
			config.AppSettings.Settings[Constants.Settings.WEBCAM_REFRESH_RATE].Value = num.ToString();
			_cameraSettingsChanged = true;
		}
		
		/// <summary>
		/// Called when the Prime After Fire checkbox is clicked or unclicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ChkPrimeCheckedChanged(object sender, EventArgs e)
		{
			config.AppSettings.Settings[Constants.Settings.PRIME_AIRTANK].Value = this.chkPrime.Checked ? "Y" : "N";
		}
		
		/// <summary>
		/// Called when the camera height text box value changes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void TxtCamWidthTextChanged(object sender, EventArgs e)
		{
			try
			{
				int x = Convert.ToInt32(this.txtCamWidth.Text);
			}
			catch(Exception e1)
			{
				Log.Instance.Out(e1);
				this.errorProvider1.SetError(this.txtCamWidth,"Camera Width must be a valid number");
				return;
			}
			config.AppSettings.Settings[Constants.Settings.CAMERA_WIDTH].Value = this.txtCamWidth.Text;
			_cameraSettingsChanged = true;
		}
		
		/// <summary>
		/// Called when the camera height text boxes value changes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void TxtCamHeightTextChanged(object sender, EventArgs e)
		{
			try
			{
				int x = Convert.ToInt32(this.txtCamHeight.Text);
			}
			catch(Exception e1)
			{
				Log.Instance.Out(e1);
				this.errorProvider1.SetError(this.txtCamWidth,"Camera Height must be a valid number");
				return;
			}
			config.AppSettings.Settings[Constants.Settings.CAMERA_HEIGHT].Value = this.txtCamHeight.Text;
			_cameraSettingsChanged = true;
		}
		
		/// <summary>
		/// Called when the Default button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnDefaultClick(object sender, EventArgs e)
		{
			this.resetToDefault();
			this.Refresh();
		}
		
		/// <summary>
		/// Called when Ok is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnOKClick(object sender, EventArgs e)
		{
			this.Close();
		}
		#endregion
		
		#region Overrides
		/// <summary>
		/// Refreshes the form and sets loads the settings from file.
		/// </summary>
		public override void Refresh()
		{
			string rocketReset,refreshRate,primeTank,camWidth,camHeight, candidShots, candidShotsMS;
			rocketReset = config.AppSettings.Settings[Constants.Settings.RESET_LAUNCHER_ON_START].Value;
			refreshRate = config.AppSettings.Settings[Constants.Settings.WEBCAM_REFRESH_RATE].Value;
			primeTank = config.AppSettings.Settings[Constants.Settings.PRIME_AIRTANK].Value;
			camWidth = config.AppSettings.Settings[Constants.Settings.CAMERA_WIDTH].Value;
			camHeight = config.AppSettings.Settings[Constants.Settings.CAMERA_HEIGHT].Value;
			candidShots = config.AppSettings.Settings[Constants.Settings.CANDID_SHOTS].Value;
			candidShotsMS = config.AppSettings.Settings[Constants.Settings.CANDID_SHOTS_TIMING].Value;
			
			
			this.txtRefreshRate.Text = refreshRate;
			this.fillCheckBox(primeTank,this.chkPrime);
			this.txtCamWidth.Text = camWidth;
			this.txtCamHeight.Text = camHeight;
			this.txtCandidShots.Text = candidShots;
			this.txtCandidShotsMS.Text = candidShotsMS;
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Tells the main form whether or not to refresh the camera's image
		/// </summary>
		public bool CameraSettingsChanged
		{
			get { return _cameraSettingsChanged; }
		}
		#endregion
		
		#region Utility Functions
		/// <summary>
		/// This is used to make it easier to set checkboxes based on the setting result
		/// </summary>
		/// <param name="setting"></param>
		/// <param name="box"></param>
		private void fillCheckBox(string setting, CheckBox box)
		{
			if(setting == null || setting == "" || setting == "N")
				box.Checked = false;
			else
				box.Checked = true;
		}
		
		/// <summary>
		/// Resets all settings back to their default value as defined in Constants.Settings struct.
		/// </summary>
		private void resetToDefault()
		{
			config.AppSettings.Settings[Constants.Settings.CAMERA_HEIGHT].Value = Constants.Defaults.CAMERA_HEIGHT;
			config.AppSettings.Settings[Constants.Settings.CAMERA_WIDTH].Value = Constants.Defaults.CAMERA_WIDTH;
			config.AppSettings.Settings[Constants.Settings.IMAGE_NUMBER].Value = Constants.Defaults.IMAGE_NUMBER;
			config.AppSettings.Settings[Constants.Settings.PRIME_AIRTANK].Value = Constants.Defaults.PRIME_AIRTANK;
			config.AppSettings.Settings[Constants.Settings.RESET_LAUNCHER_ON_START].Value = Constants.Defaults.RESET_LAUNCHER_ON_START;
			config.AppSettings.Settings[Constants.Settings.WEBCAM_ON_START].Value = Constants.Defaults.WEBCAM_ON_START;
			config.AppSettings.Settings[Constants.Settings.WEBCAM_REFRESH_RATE].Value = Constants.Defaults.WEBCAM_REFRESH_RATE;
			config.AppSettings.Settings[Constants.Settings.CANDID_SHOTS].Value = Constants.Defaults.CANDID_SHOTS;
			config.AppSettings.Settings[Constants.Settings.CANDID_SHOTS_TIMING].Value = Constants.Defaults.CANDID_SHOTS_TIMING;
		}
		#endregion

		
		void TxtCandidShotsTextChanged(object sender, EventArgs e)
		{
			if(!checkValidNumberRange(this.txtCandidShots.Text,0,20))
			{
				this.errorProvider1.SetError(this.txtCandidShots,"Must be a valid number between 0 and 20");
				return;
			}
			else
			{
				this.errorProvider1.SetError(this.txtCandidShots,"");			
				config.AppSettings.Settings[Constants.Settings.CANDID_SHOTS].Value = this.txtCandidShots.Text;
			}
		}
		
		void TxtCandidShotsMSTextChanged(object sender, EventArgs e)
		{
			if(!checkValidNumberRange(this.txtCandidShotsMS.Text,100,5000))
			{
				this.errorProvider1.SetError(this.txtCandidShotsMS,"Must be a valid number between 100 and 5000");
				return;
			}
			else
			{
				this.errorProvider1.SetError(this.txtCandidShotsMS,"");			
				config.AppSettings.Settings[Constants.Settings.CANDID_SHOTS_TIMING].Value = this.txtCandidShotsMS.Text;
			}
		}
		
		/// <summary>
		/// Checks to see if a string is a valid integer, and that it is between the range specified by low and high (inclusive)
		/// </summary>
		/// <param name="number"></param>
		/// <param name="low"></param>
		/// <param name="high"></param>
		/// <returns></returns>
		private bool checkValidNumberRange(string number,int low, int high)
		{
			int num = 0;
			try
			{
				num = Convert.ToInt32(number);
			}
			catch(Exception e)
			{
				Log.Instance.Out(e);
				return false;
			}
			
			if(num < low || num > high)
				return false;
			
			return true;
		}
	}
}
