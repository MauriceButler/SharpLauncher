/*
 * Created by SharpDevelop.
 * User: Anthony.Mason
 * Date: 5/7/2007
 * Time: 10:35 AM
 * 
 */

using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;
using System.Threading;
using System.Text;
using LibUSBLauncher;
using WebCam_Capture;
using LibHid;


namespace SharpLauncher
{
	/// <summary>
	/// Main Form of the application
	/// </summary>
	public partial class MainForm : BaseForm, IDisposable
	{
		#region Private Data
		private LauncherManager _manager = new LauncherManager();
		private WebCamCapture _camera = new WebCamCapture();
		private Configuration  _config = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
		private int _regWidth = 280;
		private int _regHeight = 356;
		private Thread _candidShotThread;
		private System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
		#endregion
		
		#region Program Entry Point
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//try
			//{
				Application.Run(new MainForm());
			//}
			//catch(Exception e)
			//{
			//	MessageBox.Show("An exception has occurred, and the application must shut down.  Please check " + Directory.GetCurrentDirectory() + Log.Filename + " for more information.", "Exception Has Occurred!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//	Log.Instance.Out(e);
			//}

		}
		#endregion
		
		#region Constructor
		/// <summary>
		/// Constructor.  Put additional events here.
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
			this.CheckSettings();
			this.Width = _regWidth;
			this.Height = _regHeight;
			this.btnDown.MouseUp += new MouseEventHandler(btn_Unclicked);
			this.btnDownLeft.MouseUp += new MouseEventHandler(btn_Unclicked);
			this.btnDownRight.MouseUp += new MouseEventHandler( btn_Unclicked);
			this.btnLeft.MouseUp += new MouseEventHandler(btn_Unclicked);
			this.btnRight.MouseUp += new MouseEventHandler(btn_Unclicked);
			this.btnUpLeft.MouseUp += new MouseEventHandler(btn_Unclicked);
			this.btnUp.MouseUp += new MouseEventHandler(btn_Unclicked);
			this.btnUpRight.MouseUp += new MouseEventHandler(btn_Unclicked);
			this.btnRight.MouseDown += new MouseEventHandler(BtnRightClick);
			this.btnLeft.MouseDown += new MouseEventHandler(BtnLeftClick);
			this.btnUp.MouseDown += new MouseEventHandler(BtnUpClick);
			this.btnDown.MouseDown += new MouseEventHandler(BtnDownClick);
			this.btnUpLeft.MouseDown += new MouseEventHandler(BtnUpLeftClick);
			this.btnUpRight.MouseDown += new MouseEventHandler(BtnUpRightClick);
			this.btnDownLeft.MouseDown += new MouseEventHandler(BtnDownLeftClick);
			this.btnDownRight.MouseDown += new MouseEventHandler(BtnDownRightClick);
			this.pictureBox1.VisibleChanged += new EventHandler(WebCamVisibilityChanged);
			_camera.ImageCaptured += new WebCamCapture.WebCamEventHandler(this.WebCamCapture_ImageCaptured);
			this.contextMenuStrip1.Opening += new CancelEventHandler(contextMenuStrip1_Opening);
			
			UsbHidPort dreamCheeky = new UsbHidPort();
			UsbHidPort rocketBaby = new UsbHidPort();
			
			dreamCheeky.OnSpecifiedDeviceArrived += new EventHandler(DreamCheekyArrived);
			dreamCheeky.OnSpecifiedDeviceRemoved += new EventHandler(DreamCheekyRemoved);
            rocketBaby.OnSpecifiedDeviceArrived += new EventHandler(RocketBabyArrived);
            rocketBaby.OnSpecifiedDeviceRemoved += new EventHandler(RocketBabyRemoved);
			
			if(this._manager.Connect(dreamCheeky, rocketBaby))
			{
				//TODO: put logic if(settings[rocket_start] == "Y") then recalibrate launcher
				this.updateLaunchersMenu();
			}
			else
			{
				this.AddText("No USB Launchers Found, check log");
			}
			
			this.chkFire.Checked = _config.AppSettings.Settings[Constants.Settings.FIRE_WHILE_MOVING].Value == "Y" ? true : false;
			this.chkSlow.Checked = _config.AppSettings.Settings[Constants.Settings.SLOW].Value == "Y" ? true : false;
			bool primeAfterFire = _config.AppSettings.Settings[Constants.Settings.PRIME_AIRTANK].Value == "Y" ? true : false;

            foreach (USBLauncher l in _manager.Launchers)
            {
                if (l is DreamCheekyLauncher)
                    (l as DreamCheekyLauncher).PrimeAfterFire = primeAfterFire;
                else if (l is RocketBabyLauncher)
                    (l as RocketBabyLauncher).PrimeAfterFire = primeAfterFire;
            }
			
			DirectoryInfo dInfo = new DirectoryInfo("images");
            if(!dInfo.Exists)
            	dInfo.Create();
            
            _timer.Interval = 5000;
            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Start();
			
		}
		#endregion
		
		#region Button Event Handlers
		/// <summary>
		/// Called when the Left arrow button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnUpLeftClick(object sender, EventArgs e)
		{
			foreach(USBLauncher l in _manager.Launchers)
				l.MoveUpLeft();
		}
		
		/// <summary>
		/// Called when the Up arrow button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnUpClick(object sender, EventArgs e)
		{
			foreach(USBLauncher l in _manager.Launchers)
				l.MoveUp();
		}
		
		/// <summary>
		/// Called when the Up-Right arrow button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnUpRightClick(object sender, EventArgs e)
		{
			foreach(USBLauncher l in _manager.Launchers)
				l.MoveUpRight();
		}
		
		/// <summary>
		/// Called when the Left arrow button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnLeftClick(object sender, EventArgs e)
		{
			foreach(USBLauncher l in _manager.Launchers)
				l.MoveLeft();
		}
		
		/// <summary>
		/// Called when the right arrow button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnRightClick(object sender, EventArgs e)
		{
			foreach(USBLauncher l in _manager.Launchers)
				l.MoveRight();
		}
		
		/// <summary>
		/// Called when the Down-Left arrow button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnDownLeftClick(object sender, EventArgs e)
		{
			foreach(USBLauncher l in _manager.Launchers)
				l.MoveDownLeft();
		}
		
		void BtnDownClick(object sender, EventArgs e)
		{
			foreach(USBLauncher l in _manager.Launchers)
				l.MoveDown();
		}
		
		/// <summary>
		/// Called when the Down-Right arrow button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnDownRightClick(object sender, EventArgs e)
		{
			foreach(USBLauncher l in _manager.Launchers)
				l.MoveDownRight();
		}
		
		/// <summary>
		/// Called when the Fire button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnFireClick(object sender, EventArgs e)
		{
			foreach(USBLauncher l in _manager.Launchers)
				l.Fire();
			
			this.checkCandidShots();
		}
		
		/// <summary>
		/// Called when the Prime button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnPrimeClick(object sender, EventArgs e)
		{
			foreach(DreamCheekyLauncher r in _manager.Launchers)
				r.Prime();
		}
		
		/// <summary>
		/// Called when any movement buttons on the form are released, sends the kill command
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btn_Unclicked(object sender, EventArgs e)
		{
			foreach(USBLauncher l in _manager.Launchers)
				l.Stop();
		}
		
		/// <summary>
		/// Called when the stop button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnStopClick(object sender, EventArgs e)
		{
			foreach(USBLauncher l in _manager.Launchers)
				l.Stop();
		}
		#endregion
		
		#region Other GUI Event Handlers
		/// <summary>
		/// Called when the Slow checkbox is clicked or unclicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ChkSlowCheckedChanged(object sender, EventArgs e)
		{
			bool visible = !this.chkSlow.Checked;
			if(chkSlow.Checked)
			{
				reset();
				chkFire.Checked = false;
				foreach(USBLauncher l in _manager.Launchers)
				{
					l.Slow = true;
					l.FireWhileMoving = false;
				}
			}
			else
			{
				foreach(USBLauncher l in _manager.Launchers)
				{
					l.Slow = false;
				}
			}
			this.btnDownLeft.Visible = visible;
			this.btnDownRight.Visible = visible;
			this.btnUpLeft.Visible = visible;
			this.btnUpRight.Visible = visible;
			
			_config.AppSettings.Settings[Constants.Settings.SLOW].Value = this.chkSlow.Checked ? "Y" : "N";
		}
		
		/// <summary>
		/// Called when the "Attempt Fire While Moving" checkbox is clicked or unclicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ChkFireCheckedChanged(object sender, EventArgs e)
		{
			bool visible = !this.chkFire.Checked;
			if(chkFire.Checked)
			{
				reset();
				chkSlow.Checked = false;
				foreach(USBLauncher l in _manager.Launchers)
				{
					l.FireWhileMoving = true;
					l.Slow = false;
				}
			}
			else
			{
				foreach(USBLauncher l in _manager.Launchers)
					l.FireWhileMoving = false;
			}
		
			this.btnUp.Visible = visible;
			this.btnDown.Visible = visible;
			this.btnDownLeft.Visible = visible;
			
			_config.AppSettings.Settings[Constants.Settings.FIRE_WHILE_MOVING].Value = this.chkFire.Checked ? "Y" : "N";
		
		}
				
		/// <summary>
		/// Called when View->Webcam is clicked(toggled).
		/// It is also called anytime you minimize / restore, 
		/// since we close the webcam while minimized
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void WebCamVisibilityChanged(object sender, EventArgs e)
		{
			if(this.pictureBox1.Visible)
			{
				int width = Convert.ToInt32(_config.AppSettings.Settings[Constants.Settings.CAMERA_WIDTH].Value);
				int height = Convert.ToInt32(_config.AppSettings.Settings[Constants.Settings.CAMERA_HEIGHT].Value);
				this.pictureBox1.Width = width;
				this.pictureBox1.Height = height;
				_camera.CaptureWidth = width;
				_camera.CaptureHeight = height;
				this.Width = _regWidth + width + 45;
				if(_regHeight > height + 30)
					this.Height = _regHeight;
				else
					this.Height = height + 130;
				
				_camera.TimeToCapture_milliseconds = Convert.ToInt32(_config.AppSettings.Settings[Constants.Settings.WEBCAM_REFRESH_RATE].Value);
				_camera.Start(0);
				this.snapPictureToolStripMenuItem.Enabled = true;
			}
			else
			{
				this.Width = _regWidth;
				this.Height = _regHeight;
				this.snapPictureToolStripMenuItem.Enabled = false;
				_camera.Stop();
			}
		}
		
		/// <summary>
		/// Called every time the webcam gets a new image, which depends
		/// on the refresh rate determined in the settings menu
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void WebCamCapture_ImageCaptured(object source, WebCam_Capture.WebcamEventArgs e)
		{
			// set the picturebox picture
			this.pictureBox1.Image = e.WebCamImage;
		}

		/// <summary>
		/// Called just before the form closes, we save the settings file
		/// and attempt to dispose of objects
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			this.pictureBox1.Visible = false;
			_camera.Stop();
			_manager.Disconnect();
			this.toolStripStatusLabel1.Text = "Saving settings...";
			Application.DoEvents();
			_config.Save(System.Configuration.ConfigurationSaveMode.Modified);

			_camera.Dispose();
			_manager.Dispose();
			//Log.Instance.Dispose();
			base.OnClosing(e);
		}
		
		/// <summary>
		/// Called the first time the form is opened and shown
		/// </summary>
		/// <param name="e"></param>
		protected override void OnShown(EventArgs e)
		{
			if(this.webcamToolStripMenuItem.Checked
			   || _config.AppSettings.Settings[Constants.Settings.WEBCAM_ON_START].Value == "Y")
			{
				this.pictureBox1.Visible = true;
				this.webcamToolStripMenuItem.Checked = true;
			}
			else
			{
				this.pictureBox1.Visible = false;
				this.WebCamVisibilityChanged(null,null);
				this.webcamToolStripMenuItem.Checked = false;
				this.Width = _regWidth;
				this.Height = _regHeight;
			}
		}
		
		/// <summary>
		/// Called anytime the Window is resized, if minimized, 
		/// we turn off the camera
		/// </summary>
		/// <param name="e"></param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if(this.WindowState == FormWindowState.Minimized)
			{
				foreach(USBLauncher l in _manager.Launchers)
					l.Pause();
				this.pictureBox1.Visible = false;
				this.Hide();
				this.notifyIcon1.Visible = true;
			}
		}
		
		/// <summary>
		/// Called when you double click the icon in the system tray,
		/// we restore the window to its previous state
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void NotifyIcon1MouseDoubleClick(object sender, MouseEventArgs e)
		{
			foreach(USBLauncher l in _manager.Launchers)
				l.Continue();
			this.Show();
			this.OnShown(null);
			this.WindowState = FormWindowState.Normal;
			this.notifyIcon1.Visible = false;
		}
		
		/// <summary>
		/// Called when the notifyicon is right clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void contextMenuStrip1_Opening(object sender, System.EventArgs e)
		{
			//if the camera isn't active, we disable the snap picture button
			this.snapPictureToolStripMenuItem1.Enabled = this.webcamToolStripMenuItem.Checked;
		}
		
		/// <summary>
		/// Called when notifyicon is right clicked -> Restore, we just manually call the same
		/// as if it was double clicked to eliminate copy and paste code reuse :)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void RestoreToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.NotifyIcon1MouseDoubleClick(null,null);
		}
		
		/// <summary>
		/// Called when any item in the Launchers menu is checked or unchecked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void launcherToolStripMenuItem_Checked(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			string name = item.Name;
			int num = -1;
			for(int i = 1; i<= _manager.Launchers.Count; i++)
			{
				if(name.EndsWith(i.ToString()))
				{
				   num = i;
				   break;
				}
			}
			
			if(num == -1)
				return;
			
			USBLauncher l = _manager.Launchers[num - 1]; //zero based, so so subtract one
			l.Enabled = item.Checked;
		}
		#endregion
		
		#region Menu Item Click Events
		/// <summary>
		/// Called when File->Exit is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		/// <summary>
		/// Called when View->Webcam is clicked(toggled)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void WebcamToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(this.webcamToolStripMenuItem.Checked)
			{
				_config.AppSettings.Settings[Constants.Settings.WEBCAM_ON_START].Value = "Y";
				this.pictureBox1.Visible = true;
			}
			else
			{
				_config.AppSettings.Settings[Constants.Settings.WEBCAM_ON_START].Value = "N";
				this.pictureBox1.Visible = false;
			}
			
		}
		
		/// <summary>
		/// Called when Help->About is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			AboutForm frm = new AboutForm();
			frm.ShowDialog(this);
		}
		
		/// <summary>
		/// Called when Tools->Settings is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void SettingsToolStripMenuItemClick(object sender, EventArgs e)
		{
			SettingsForm frm = new SettingsForm(_config);
			
			frm.ShowDialog(this);
			if(frm.CameraSettingsChanged)
			{
				_camera.Stop();
				this.pictureBox1.Visible = false;
				this.pictureBox1.Visible = this.webcamToolStripMenuItem.Checked;
				_camera.TimeToCapture_milliseconds = Convert.ToInt32(_config.AppSettings.Settings[Constants.Settings.WEBCAM_REFRESH_RATE].Value);
				_camera.Start(0);
			}
			
			bool primeAfterFire = _config.AppSettings.Settings[Constants.Settings.PRIME_AIRTANK].Value == "Y" ? true : false;
			
			foreach(DreamCheekyLauncher r in _manager.Launchers)
				r.PrimeAfterFire = primeAfterFire;
		}
		
		/// <summary>
		/// Called when Help->Donate is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void DonateToolStripMenuItemClick(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.antmason.com/wiki/index.php/AntMason:Site_support");
		}
		
		/// <summary>
		/// Called when Help->Controls is called
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ControlsToolStripMenuItemClick(object sender, EventArgs e)
		{
			ControlsForm frm = new ControlsForm();
			frm.ShowDialog(this);
		}
		
		/// <summary>
		/// Called when Tools->Snap Picture is called, or likewise when F10 is pressed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void snapPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
        	this.toolStripStatusLabel1.Text = "Saving image...";
        	Application.DoEvents();
            _camera.Stop();

            
            DirectoryInfo dInfo = new DirectoryInfo("images");
            
            if(!dInfo.Exists)
            {
            	this.toolStripStatusLabel1.Text = "Could not save picture, " + dInfo.FullName + " does not exist";
            	_camera.Start(0);
            	return;
            }
           	
            string filename = _config.AppSettings.Settings[Constants.Settings.IMAGE_NUMBER].Value + ".jpg";
            this.pictureBox1.Image.Save("images/" + filename);
            FileInfo fInfo = new FileInfo("images/" + filename);
            if(fInfo.Exists)
            {
           		this.toolStripStatusLabel1.Text = "Image " + filename + " saved successfully.";
           		int num = Convert.ToInt32(_config.AppSettings.Settings[Constants.Settings.IMAGE_NUMBER].Value);
           		num++;
           		_config.AppSettings.Settings[Constants.Settings.IMAGE_NUMBER].Value = num.ToString();
           		_config.Save();
            }
            else
            	this.toolStripStatusLabel1.Text = "Error: image not saved.";
            _camera.Start(0);
        }
		
		/// <summary>
		/// Called when exit is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ExitToolStripMenuItem1Click(object sender, EventArgs e)
		{
			this.Close();
		}
		
		/// <summary>
		/// called when view-> log is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void LogToolStripMenuItemClick(object sender, EventArgs e)
		{
			ViewLogForm frm = new ViewLogForm();
			frm.ShowDialog(this);
		}
		
		/// <summary>
		/// Called when view->snapshots is called
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void SnapshotsToolStripMenuItemClick(object sender, EventArgs e)
		{
			ViewSnapshotsForm frm = new ViewSnapshotsForm();
			frm.ShowDialog(this);
		}
		
		/// <summary>
		/// Called when system tray icon Right Click -> Snap Picture
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void SnapPictureToolStripMenuItem1Click(object sender, EventArgs e)
		{
			_camera.Start(0); //the camera is stopped in minimized mode, we restart it
            DirectoryInfo dInfo = new DirectoryInfo("images");
            if(!dInfo.Exists)
            	dInfo.Create();
            
            dInfo = new DirectoryInfo("images");
            
            if(!dInfo.Exists)
            {
            	MessageBox.Show("Error while saving: " + dInfo.FullName + " could not be found or created.","Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            	_camera.Stop();
            	return;
            }
           	
            string filename = _config.AppSettings.Settings[Constants.Settings.IMAGE_NUMBER].Value + ".jpg";
            this.pictureBox1.Image.Save("images/" + filename);
            FileInfo fInfo = new FileInfo("images/" + filename);
            if(fInfo.Exists)
            {
           		this.toolStripStatusLabel1.Text = "Image " + filename + " saved successfully.";
           		int num = Convert.ToInt32(_config.AppSettings.Settings[Constants.Settings.IMAGE_NUMBER].Value);
           		num++;
           		_config.AppSettings.Settings[Constants.Settings.IMAGE_NUMBER].Value = num.ToString();
           		_config.Save();
            }
            else
            	MessageBox.Show("Due to an unexpected error, " + fInfo.Name + " could not be saved","Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            	_camera.Stop();
		}
		
		/// <summary>
		/// Called when Tools -> Discover Commands
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void DiscoverCommandsToolStripMenuItemClick(object sender, EventArgs e)
		{
			DiscoveryForm frm = new DiscoveryForm(_manager);
			frm.Show(this);
		}
		
		/// <summary>
		/// Disconnects all launchers
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void DisconnectToolStripMenuItemClick(object sender, EventArgs e)
		{
			_manager.Disconnect();
			_manager.Launchers.Clear();
			this.updateLaunchersMenu();
		}
		
		/// <summary>
		/// Disconnects and reconnects the launchers
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ReconnectToolStripMenuItemClick(object sender, EventArgs e)
		{
			_manager.Disconnect();
			_manager.Launchers.Clear();
			//_manager.Connect();
			this.updateLaunchersMenu();
		}
		#endregion
		
		#region Keyboard Handling
		/// <summary>
		/// This is called when a key is released, and toggles
		/// its respective boolean value for whether the key is currently
		/// being held down or not
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyUp(KeyEventArgs e)
		{
			switch(e.KeyData)
			{
				case Keys.Left:
					Constants.KeysDown.LEFT = false;
					checkKeys();
					break;
				case Keys.Right:
					Constants.KeysDown.RIGHT = false;
					checkKeys();
					break;
				case Keys.Down:
					Constants.KeysDown.DOWN = false;
					checkKeys();
					break;
				case Keys.Up:
					Constants.KeysDown.UP = false;
					checkKeys();
					break;
			}
			
		}
		
		/// <summary>
		/// This is called when a key is pressed down,
		/// and we toggle an individual boolean value for each important 
		/// key.  We handle it this way because it is the easiest way to handle
		/// simultaneous key movements independently of one another
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected bool OnKeyDown(ref Message msg, Keys keyData)
		{
			
			switch(keyData)
			{
				case Keys.Left:
					Constants.KeysDown.LEFT = true;
					break;
				case Keys.Right:
					Constants.KeysDown.RIGHT = true;
					break;
				case Keys.Down:
					Constants.KeysDown.DOWN = true;
					break;
				case Keys.Up:
					Constants.KeysDown.UP = true;
					break;
				case Keys.Enter:
					foreach(USBLauncher l in _manager.Launchers)
						l.Fire();
					this.checkCandidShots();
					break;
				case Keys.P:
					foreach(DreamCheekyLauncher r in _manager.Launchers)
						r.Prime();
					break;
			}	
			this.checkKeys();
			return true;
		}
		
		/// <summary>
		/// When a key is pressed or released, this is called to recheck what 
		/// is currently being held down, and sets a command that should be 
		/// called that depends on what is held down, then calls that command.
		/// </summary>
		void checkKeys()
		{
			bool left, right, up, down;
			left = Constants.KeysDown.LEFT;
			right = Constants.KeysDown.RIGHT;
			up = Constants.KeysDown.UP;
			down = Constants.KeysDown.DOWN;
			
			if(left)
			{
				if(up) 
				{
					foreach(USBLauncher l in _manager.Launchers) l.MoveUpLeft();
					return;
				}
				else if(down)
				{
					foreach(USBLauncher l in _manager.Launchers) l.MoveDownLeft();
					return;
				}
				else
				{
					foreach(USBLauncher l in _manager.Launchers) l.MoveLeft();
					return;
				}
					
			}
			else if(right)
			{
				if(up)
				{
					foreach(USBLauncher l in _manager.Launchers) l.MoveUpRight();
					return;
				}
				else if(down)
				{
					foreach(USBLauncher l in _manager.Launchers) l.MoveDownRight();
					return;
				}
				else 
				{
					foreach(USBLauncher l in _manager.Launchers) l.MoveRight();
					return;
				}		
			}
			else if(up)
			{
				foreach(USBLauncher l in _manager.Launchers) l.MoveUp();
				return;
			}
			else if(down)
			{
				foreach(USBLauncher l in _manager.Launchers) l.MoveDown();
				return;
			}
			
			if(!left && !right && !up && !down)
				foreach(USBLauncher l in _manager.Launchers) 
					l.Stop();
		}
		
		/// <summary>
		/// This is where all key commands start for this particular form,
		/// if it isn't handled here, then KeyUp or KeyDown functions
		/// handle the key.  
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if(keyData == Keys.Tab)
			{
				return base.ProcessCmdKey(ref msg, keyData);
			}
			else if(keyData == Keys.F10)
			{
				if(this.snapPictureToolStripMenuItem.Enabled)
					this.snapPictureToolStripMenuItem_Click(this, null);
				return true;
			}
			else
			{
				return OnKeyDown(ref msg, keyData);
			}
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Allows the other forms to access the configuration settings for the application
		/// quickly and easily
		/// </summary>
		public Configuration AppSettings
		{
			get { return _config; }
		}
		#endregion
		
		#region Utility Functions
		/// <summary>
		/// Resets the visibility of the controls buttons to all visible
		/// </summary>
		private void reset()
		{
			this.btnUp.Visible = true;
			this.btnRight.Visible = true;
			this.btnLeft.Visible = true;
			this.btnDown.Visible = true;
			this.btnDownLeft.Visible = true;
			this.btnDownRight.Visible = true;
			this.btnUpLeft.Visible = true;
			this.btnUpRight.Visible = true;
		}
		
		/// <summary>
		/// Called when the application starts, this checks to see if the setting
		/// file exists, and if the individual settings have been intialized to their
		/// appropriate initial value, and if not, creates them.  
		/// If Clear is true, it clears the setting back to default anyway
		/// </summary>
		public void CheckSettings()
		{
			if(_config.AppSettings.Settings[Constants.Settings.PRIME_AIRTANK] == null)
				_config.AppSettings.Settings.Add(Constants.Settings.PRIME_AIRTANK,Constants.Defaults.PRIME_AIRTANK);
			if(_config.AppSettings.Settings[Constants.Settings.RESET_LAUNCHER_ON_START] == null)
				_config.AppSettings.Settings.Add(Constants.Settings.RESET_LAUNCHER_ON_START,Constants.Defaults.RESET_LAUNCHER_ON_START);
			if(_config.AppSettings.Settings[Constants.Settings.WEBCAM_ON_START] == null)
				_config.AppSettings.Settings.Add(Constants.Settings.WEBCAM_ON_START,Constants.Defaults.WEBCAM_ON_START);
			if(_config.AppSettings.Settings[Constants.Settings.WEBCAM_REFRESH_RATE] == null)
				_config.AppSettings.Settings.Add(Constants.Settings.WEBCAM_REFRESH_RATE,Constants.Defaults.WEBCAM_REFRESH_RATE);
			if(_config.AppSettings.Settings[Constants.Settings.IMAGE_NUMBER] == null)
				_config.AppSettings.Settings.Add(Constants.Settings.IMAGE_NUMBER,Constants.Defaults.IMAGE_NUMBER);
			if(_config.AppSettings.Settings[Constants.Settings.CAMERA_HEIGHT] == null)
				_config.AppSettings.Settings.Add(Constants.Settings.CAMERA_HEIGHT,Constants.Defaults.CAMERA_HEIGHT);
			if(_config.AppSettings.Settings[Constants.Settings.CAMERA_WIDTH] == null)
				_config.AppSettings.Settings.Add(Constants.Settings.CAMERA_WIDTH,Constants.Defaults.CAMERA_WIDTH);
			if(_config.AppSettings.Settings[Constants.Settings.SLOW] == null)
				_config.AppSettings.Settings.Add(Constants.Settings.SLOW,Constants.Defaults.SLOW);
			if(_config.AppSettings.Settings[Constants.Settings.FIRE_WHILE_MOVING] == null)
				_config.AppSettings.Settings.Add(Constants.Settings.FIRE_WHILE_MOVING,Constants.Defaults.FIRE_WHILE_MOVING);
			if(_config.AppSettings.Settings[Constants.Settings.CANDID_SHOTS] == null)
				_config.AppSettings.Settings.Add(Constants.Settings.CANDID_SHOTS,Constants.Defaults.CANDID_SHOTS);
			if(_config.AppSettings.Settings[Constants.Settings.CANDID_SHOTS_TIMING] == null)
				_config.AppSettings.Settings.Add(Constants.Settings.CANDID_SHOTS_TIMING,Constants.Defaults.CANDID_SHOTS_TIMING);
		}
		
		/// <summary>
		/// This iterates over all drop down items in the Launchers menu,
		/// and adds an entry for each usb launcher currently connected.
		/// </summary>
		private void updateLaunchersMenu()
		{
			int count = 1;
			ToolStripItem item1 = this.launchersToolStripMenuItem.DropDownItems[0];
			ToolStripItem item2 = this.launchersToolStripMenuItem.DropDownItems[1];
			ToolStripItem item3 = this.launchersToolStripMenuItem.DropDownItems[2];
			this.launchersToolStripMenuItem.DropDownItems.Clear();
			this.launchersToolStripMenuItem.DropDownItems.Add(item1);
			this.launchersToolStripMenuItem.DropDownItems.Add(item2);
			this.launchersToolStripMenuItem.DropDownItems.Add(item3);
			
			foreach(USBLauncher l in _manager.Launchers)
			{
                if (l is DreamCheekyLauncher)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem("Dream Cheeky Launcher " + count, null, null, "dreamCheekyLauncherToolStripMenuItem" + count);
                    item.CheckOnClick = true;
                    item.Checked = true;
                    item.CheckedChanged += new EventHandler(launcherToolStripMenuItem_Checked);
                    this.launchersToolStripMenuItem.DropDownItems.Add(item);
                    count++;
                }
                else if (l is RocketBabyLauncher)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem("Rocket Baby Launcher " + count, null, null, "rocketBabyLauncherToolStripMenuItem" + count);
                    item.CheckOnClick = true;
                    item.Checked = true;
                    item.CheckedChanged += new EventHandler(launcherToolStripMenuItem_Checked);
                    this.launchersToolStripMenuItem.DropDownItems.Add(item);
                    count++;
                }

			}
		}
		
		private bool checkCandidShots()
		{
			if(this.pictureBox1.Image == null || this.webcamToolStripMenuItem.Checked == false)
				return false;
			
			if(_candidShotThread != null && _candidShotThread.ThreadState == ThreadState.Running)
				return false;
			
			DirectoryInfo dInfo = new DirectoryInfo("images");
            if(!dInfo.Exists)
            	return false;
			
			_candidShotThread = new Thread(new ThreadStart(candidShotHelper));
			//_candidShotThread.Priority = ThreadPriority.Lowest;
			_candidShotThread.Start();
			return true;
		}
		
		private void candidShotHelper()
		{
			int times = Convert.ToInt32(_config.AppSettings.Settings[Constants.Settings.CANDID_SHOTS].Value);
			int spacing = Convert.ToInt32(_config.AppSettings.Settings[Constants.Settings.CANDID_SHOTS_TIMING].Value);
			List<Image> images = new List<Image>();
			
			for(int i = 0; i < times; i++)
			{
				images.Add(this.pictureBox1.Image);
				Thread.Sleep(spacing);
			}
			
			if(images.Count == 0)
				return;
			
			int count = Convert.ToInt32(_config.AppSettings.Settings[Constants.Settings.IMAGE_NUMBER].Value);
		
			//we need to wait at least 5 seconds so that we don't interfer with the firing or priming timing
			this.AddText("Waiting 5 seconds for firing to finish."); 
			Application.DoEvents();
			Thread.Sleep(5000);
			foreach(Image img in images)
			{
				StringBuilder filename = new StringBuilder("images/");
				filename.Append(count.ToString());
				filename.Append(".jpg");
				img.Save(filename.ToString());
				count++;
			}
			
			_config.AppSettings.Settings[Constants.Settings.IMAGE_NUMBER].Value = count.ToString();
			_config.Save(System.Configuration.ConfigurationSaveMode.Modified);
			this.AddText(images.Count.ToString() + " candid shots taken.");
		}
		
		private void timer_Tick(object sender, System.EventArgs e)
		{
			//if(_manager.Launchers.Count < 1 || _manager.Launchers[0].FiringStatus == USBLauncher.Status.DoneFiring)
			//	return;
			
			//if(_manager.Launchers[0].FiringStatus == USBLauncher.Status.DonePriming)
			//	this.toolStripStatusLabel2.Text = "Primed";
		}
		#endregion
		
		[System.ComponentModel.EditorBrowsableAttribute()]
		protected override void WndProc(ref Message m)
		{
			_manager.PassMessages(ref m);
			base.WndProc(ref m);
		}
		
		[System.ComponentModel.EditorBrowsableAttribute()]
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			_manager.PassHandle(Handle);
		}
		
		private void DreamCheekyArrived(object sender,System.EventArgs e)
		{
			this.AddText("Dream Cheeky Connected");
		}
		
		private void DreamCheekyRemoved(object sender, System.EventArgs e)
		{
			this.AddText("Dream Cheeky Removed");
		}

        private void RocketBabyArrived(object sender, System.EventArgs e)
        {
            this.AddText("Rocket Baby Connected");
        }

        private void RocketBabyRemoved(object sender, System.EventArgs e)
        {
            this.AddText("Rocket Baby Removed");
        }
        private void AddText(string text)
        {
            this.txtLog.Text = this.txtLog.Text + text + "\r\n\r\n";
        }

	}
	
}
