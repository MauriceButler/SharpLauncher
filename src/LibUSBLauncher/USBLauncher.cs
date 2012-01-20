/*
 * Created by SharpDevelop.
 * User: Anthony.Mason
 * Date: 5/16/2007
 * Time: 8:20 AM
 * 
 */

using System;
using System.Windows.Forms;
using LibHid;

namespace LibUSBLauncher
{
	/// <summary>
	/// Abstract class to provide partial functionality and for concrete launchers to inherit from
	/// </summary>
	public abstract class USBLauncher
	{
		#region Data
		protected Status _horizontalStatus;
		protected Status _verticalStatus;
		protected Status _firingStatus;
		protected bool _enabled = true;
		protected bool _slow;
		protected bool _fireWhileMoving;
		protected int _command = 0x00;
		private Timer _timer = new Timer();
		private UsbHidPort _port;
		#endregion
		
		#region Structs
		/// <summary>
		/// Enumerated values so we can figure out the status of the
		/// launcher in a generic way without directly comparing status
		/// bytes to constants
		/// </summary>
		public enum Status
		{
			FullLeft,
			FullRight,
			FullUp,
			FullDown,
			DoneFiring,
			Normal,
			Unknown,
			DonePriming,
			Firing
		}
		#endregion
		
		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="usb"></param>
		public USBLauncher(UsbHidPort port)
		{
			_port = port;
			_horizontalStatus = Status.Normal;
			_verticalStatus = Status.Normal;
			_firingStatus = Status.DoneFiring;
			//_timer.Tick += new EventHandler(timer_Tick);
			//_timer.Interval = 100;
			//_timer.Start();
		}
		#endregion
			
		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public UsbHidPort Port
		{
			get
			{
				return _port;
			}
		}
		
		/// <summary>
		/// This tells the launcher whether it should move slowly or normal pace when moving.
		/// </summary>
		/// <remarks>
		/// This doesn't work on all movements in all directions, mainly just the 
		/// primary motion direction (up,down,left,right)
		/// </remarks>
		public bool Slow
		{
			get { return _slow; }
			set { _slow = value; }
		}
		
		/// <summary>
		/// If this value is true, the device will attempt to fire while you move the device.
		/// </summary>
		/// <remarks>
		/// Note: this is not always successful, especially if the device hits its
		/// horizontal or vertical limit before it fire.  Helps if you prime the tank (in the case
		/// of the dreamcheeky.com launcher) before attempting this)
		/// </remarks>
		public bool FireWhileMoving
		{
			get { return _fireWhileMoving; }
			set { _fireWhileMoving = value; }
		}
		
		/// <summary>
		/// Enables or disables the launcher where commands send to it are not executed.
		/// </summary>
		public bool Enabled
		{
			get { return _enabled; }
			set { _enabled = value; }
		}
		#endregion
		
		#region Private Methods
		/// <summary>
		/// This is called the timer object so we update the status every certain number
		/// of milliseconds
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer_Tick(object sender, System.EventArgs e)
		{
			UpdateStatus();
		}
		#endregion
		
		#region Public Abstract Methods
		/// <summary>
		/// Updates the enumerated horizontal,vertical, and firing status of the launcher
		/// to a meaningful value.
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public abstract bool UpdateStatus();
		
		/// <summary>
		/// Sends a custom hexidecimal value to the launcher to execute
		/// </summary>
		/// <param name="command">Hex command to be performed</param>
		/// <returns>True if successful, false if not</returns>
		public abstract bool PerformCustomCommand(int command);
		
		/// <summary>
		/// Moves the launcher left
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public abstract bool MoveLeft();
		
		/// <summary>
		/// Moves the launcher right.
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public abstract bool MoveRight();
		
		/// <summary>
		/// Moves the launcher up.
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public abstract bool MoveUp();
		
		/// <summary>
		/// Moves the launcher down.
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public abstract bool MoveDown();
		
		/// <summary>
		/// Moves the launcher up and left 
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public abstract bool MoveUpLeft();
		
		/// <summary>
		/// Moves the launcher up and right 
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public abstract bool MoveUpRight();
		
		/// <summary>
		/// Moves the launcher down and left.
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public abstract bool MoveDownLeft();
		
		/// <summary>
		/// Moves the launcher down and right.
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public abstract bool MoveDownRight();
		
		/// <summary>
		/// Fires the launcher once.
		/// </summary>
		/// <returns>True if successful, false if not.  This is usually threaded, so isn't always dependable</returns>
		public abstract bool Fire();
		
		/// <summary>
		/// Fires the launcher more than once
		/// </summary>
		/// <param name="times">Number of times to fire.</param>
		/// <returns>True if successful, false if not.  Not always dependenable since this is often threaded.</returns>
		public abstract bool Fire(int times);
		
		/// <summary>
		/// Sends the kill command to the launcher.
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public abstract bool Stop();
		
		/// <summary>
		/// Should center the launcher in its center most position.
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public abstract bool Center();
		#endregion
		
		#region Public Contrete Methods
		/// <summary>
		/// Gets or sets the horizontal status of the launcher
		/// </summary>
		public Status HorizontalStatus
		{
			get { return _horizontalStatus; }
		}
		
		/// <summary>
		/// Gets or sets the vertical status of the launcher
		/// </summary>
		public Status VerticalStatus
		{
			get { return _verticalStatus; }
		}
		
		/// <summary>
		/// Gets or sets the firing status of the launcher
		/// </summary>
		public Status FiringStatus
		{
			get { return _firingStatus; }
		}
		
		/// <summary>
		/// Closes the connection to the usb bus for this device.
		/// </summary>
		public virtual void Close()
		{
			_timer.Stop();
		}
		
		/// <summary>
		/// Pauses timers associated with this device.
		/// </summary>
		public virtual void Pause()
		{
			Stop();
			_timer.Stop();
		}
		
		/// <summary>
		/// Starts back timers associated with this object.
		/// </summary>
		public virtual void Continue()
		{
			//_timer.Start();
		}
		#endregion
		
	}
}
