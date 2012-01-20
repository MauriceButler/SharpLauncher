/*
 * Created by SharpDevelop.
 * User: Anthony
 * Date: 5/16/2007
 * Time: 12:56 AM
 * 
 */

using System;
using System.Threading;
using LibHid;

namespace LibUSBLauncher
{
	/// <summary>
	/// Description of RocketLauncher.
	/// </summary>
	public class DreamCheekyLauncher : USBLauncher
	{
		#region Private Data
		private int _dataSize = 9;
		private byte[] _data;
		private int _firingTimes;
		private bool _primeAfterFire;
		protected static int _productId = 0x8021;
		protected static int _vendorId = 0x1941;
		private Thread _workerThread = null;
		private int _lastRead1;
		private int _lastRead2;
		#endregion
		
		#region Constants
		/// <summary>
		/// These are the hex values that achieve specific known results for the DreamCheeky
		/// USB Rocket Launcher
		/// </summary>
		private struct WriteConstants
		{
			public const int STOP = 0x00;
			public const int UP = 0x01;
			public const int DOWN = 0x02;
			public const int LEFT = 0x04;
			public const int UP_LEFT = 0x05;
			public const int DOWN_LEFT = 0x06;
			public const int RIGHT = 0x08;
			public const int UP_RIGHT = 0x09;
			public const int FIRE = 0x10;
			public const int DOWN_RIGHT = 0x0A;
			public const int SLOW_LEFT =0x07;
			public const int SLOW_RIGHT = 0x0B;
			public const int SLOW_UP = 0x0D;
			public const int SLOW_DOWN = 0x0E;
			public const int FIRE_LEFT = 0x14;
			public const int FIRE_RIGHT = 0x18;
			public const int FIRE_UP_LEFT = 0x15;
			public const int FIRE_UP_RIGHT = 0x19;
			public const int FIRE_DOWN_RIGHT = 0x1A;
			public const int REQUEST = 0x0000009;
			public const int REQUEST_TYPE = 0x21;
			public const int REQUEST_VAL = 0x0000200;
		}
		
		/// <summary>
		/// Known status codes that are returned from a bulkread to the DreamCheeky USB
		/// Rocket Launcher
		/// </summary>
		private struct ReadConstants
		{
        	public const int FULL_LEFT = 0x4; //in second byte
        	public const int FULL_RIGHT = 0x8; //in second byte
        	public const int FULL_DOWN = 0x40; //in first byte
        	public const int FULL_UP = 0x80; //in first byte
        	public const int PRIME_DONE = 0x80; //in second byte
        	public const int FULL_LEFT_PRIME_DONE = 0x84; //in second byte 
        	public const int FULL_RIGHT_PRIME_DONE = 0x88; //in second byte
		}
		#endregion
		
		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="usb"></param>
		public DreamCheekyLauncher(UsbHidPort port) : base(port)
		{
			_data = new byte[_dataSize];
			Port.VendorId = VendorId;
			Port.ProductId = ProductId;
            Port.CheckDevicePresent();
            if(Port.SpecifiedDevice != null)
			    Port.SpecifiedDevice.DataRecieved += new LibHid.DataRecievedEventHandler(onDataReceived);
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Returns the productId in integer form. 
		/// </summary>
		/// <example>Call ProductId.ToString("X") to get Hex equivalent</example>
		public static int ProductId
		{
			get { return _productId; }
		}
		
		/// <summary>
		/// Returns the vendor id in integer form
		/// </summary>
		/// <example>Call ProductId.ToString("X") to get Hex equivalent</example>
		public static int VendorId
		{
			get { return _vendorId; }
		}
		
		/// <summary>
		/// Tells the launcher whether or not it should prime the air tank after fire.
		/// </summary>
		public bool PrimeAfterFire
		{
			get { return _primeAfterFire; }
			set { _primeAfterFire = value; }
		}
		#endregion
		
		#region Public Overridden Methods
		/// <summary>
		/// Sends a hexidecimal value to the rocket launcher to achieve an action
		/// </summary>
		/// <param name="command">The hex constant of the command you wish to achieve</param>
		/// <returns>True if successful, false if not</returns>
		/// <example>mLauncher.PerformCustomCommand(RocketLauncher.WriteConstants.FIRE)</example>
		public override bool PerformCustomCommand(int command)
		{
			if(!Enabled)
				return false;
			
			byte[] data = new byte[_dataSize];
			data[1] = (byte)command;
			int ret = -1;
			if(command != _command)
			{
				Port.SpecifiedDevice.SendData(data);
			}

			_command = command;
			
			return true;
		}
		
		/// <summary>
		/// Moves the launcher up
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public override bool MoveUp()
		{
			if(_verticalStatus == Status.FullUp)
				return Stop();
			
			return PerformCustomCommand(Slow ? WriteConstants.SLOW_UP : WriteConstants.UP);
		}
		
		/// <summary>
		/// Move the launcher down.
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public override bool MoveDown()
		{
			if(_verticalStatus == Status.FullDown)
				return Stop();
			
			return PerformCustomCommand(Slow ? WriteConstants.SLOW_DOWN : WriteConstants.DOWN);
		}
		
		/// <summary>
		/// Move the launcher left.
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public override bool MoveLeft()
		{
			if(_horizontalStatus == Status.FullLeft)
				return Stop();
			
			if(Slow)
				return PerformCustomCommand(WriteConstants.SLOW_LEFT);
			else if(FireWhileMoving)
				return PerformCustomCommand(WriteConstants.FIRE_LEFT);
			else
				return PerformCustomCommand(WriteConstants.LEFT);
		}
		
		/// <summary>
		/// Moves the launcher right.
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public override bool MoveRight()
		{
			if(_horizontalStatus == Status.FullRight)
				return Stop();
			
			if(Slow)
				return PerformCustomCommand(WriteConstants.SLOW_RIGHT);
			else if(FireWhileMoving)
				return PerformCustomCommand(WriteConstants.FIRE_RIGHT);
			else
				return PerformCustomCommand(WriteConstants.RIGHT);
		}
		
		/// <summary>
		/// Moves the launcher up and left simultaneously
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public override bool MoveUpLeft()
		{
			if(_horizontalStatus == Status.FullLeft)
				return MoveUp();
			
			if(_verticalStatus == Status.FullUp)
				return MoveLeft();
			
			return PerformCustomCommand(FireWhileMoving ? WriteConstants.FIRE_UP_LEFT : WriteConstants.UP_LEFT);
		}
		
		/// <summary>
		/// Moves the launcher up and right simultaneously
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public override bool MoveUpRight()
		{
			if(_horizontalStatus == Status.FullRight)
				return MoveUp();
			
			if(_verticalStatus == Status.FullUp)
				return MoveRight();
			
			return PerformCustomCommand(FireWhileMoving ? WriteConstants.FIRE_UP_RIGHT : WriteConstants.UP_RIGHT);
		}
		
		/// <summary>
		/// Move the launcher down and left simultaneously
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public override bool MoveDownLeft()
		{
			if(_horizontalStatus == Status.FullLeft)
				return MoveDown();
			
			if(_verticalStatus == Status.FullDown)
				return MoveLeft();
			
			return PerformCustomCommand(WriteConstants.DOWN_LEFT);
		}
		
		/// <summary>
		/// Move the launcher down and right simultaneously
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public override bool MoveDownRight()
		{
			if(_horizontalStatus == Status.FullRight)
				return MoveDown();
			
			if(_verticalStatus == Status.FullDown)
				return MoveRight();
			
			return PerformCustomCommand(FireWhileMoving ? WriteConstants.FIRE_DOWN_RIGHT : WriteConstants.DOWN_RIGHT);
		}
		
		/// <summary>
		/// Fires the launcher once
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public override bool Fire()
		{
			return Fire(1);
		}
		
		/// <summary>
		/// Fire the launcher more than once
		/// </summary>
		/// <param name="times">number of times to fire</param>
		/// <returns>True if successful, false if not</returns>
		public override bool Fire(int times)
		{
			_firingTimes = times;
			if(_workerThread != null)
			{
				if(_workerThread.ThreadState == ThreadState.Running)
					_workerThread.Abort();
				_workerThread = null;
				Thread.Sleep(500);
			}
			
			_workerThread = new Thread(new ThreadStart(fireHelper));
			_workerThread.Start();
			return true;
		}
		
		/// <summary>
		/// Sends the kill command to stop whatever command is currently operating
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public override bool Stop()
		{
			return PerformCustomCommand(WriteConstants.STOP);
		}
		
		/// <summary>
		/// Overloads ToString just to provide something to know which kind of launcher
		/// we have
		/// </summary>
		/// <returns>A description of the device</returns>
		public override string ToString()
		{
			return "DreamCheeky USB Rocket Launcher";
		}
		
		/// <summary>
		/// Reads the status bytes off of the device, and updates 
		/// the objects enumerated horizontal, vertical, and firing 
		/// Status property to some meaningful value
		/// </summary>
		/// <returns>True if successful, false if not</returns>
		public override bool UpdateStatus()
		{
			//int read = _endPoint.Read(_data);
			//if(read < 0)
			//{
				//Log.Instance.Out("Error while reading: " + _usb.LastError());
			//	return false;
			//}
			
			int b1 = _data[1];
			int b2 = _data[2];
			
			//Log.Instance.Out(b1.ToString("X") + "\t" + b2.ToString("X"));
			
			switch(b1)
			{
				case 0x00:
					_verticalStatus = Status.Normal;
					break;
				case ReadConstants.FULL_DOWN:
					_verticalStatus = Status.FullDown;
					break;
				case ReadConstants.FULL_UP:
					_verticalStatus = Status.FullUp;
					break;
				default:
					_verticalStatus = Status.Unknown;
					Log.Instance.Out("Unknown Data in First Reading Byte: " + b1.ToString("X"));
					break;
			}
			
			switch(b2)
			{
				case 0x00:
					_horizontalStatus = Status.Normal;
					_firingStatus = Status.DoneFiring;
					break;
				case ReadConstants.FULL_LEFT:
					if(_lastRead2 == ReadConstants.FULL_LEFT_PRIME_DONE)
						_firingStatus = Status.DoneFiring;
					_horizontalStatus = Status.FullLeft;
					break;
				case ReadConstants.FULL_RIGHT:
					if(_lastRead2 == ReadConstants.FULL_RIGHT_PRIME_DONE)
						_firingStatus = Status.DoneFiring;
					_horizontalStatus = Status.FullRight;
					break;
				case ReadConstants.PRIME_DONE:
					if(_firingStatus == Status.DonePriming)
						_horizontalStatus = Status.Normal;
					_firingStatus = Status.DonePriming;
					break;
				case ReadConstants.FULL_LEFT_PRIME_DONE:
					_horizontalStatus = Status.FullLeft;
					_firingStatus = Status.DonePriming;
					break;
				case ReadConstants.FULL_RIGHT_PRIME_DONE:
					_horizontalStatus = Status.FullRight;
					_firingStatus = Status.DonePriming;
					break;
				default:
					_horizontalStatus = Status.Unknown;
					Log.Instance.Out("Unknown Data in Second Reading Byte: " + b2.ToString("X"));
					break;
			}
			
			
			switch(_command)
			{
				case WriteConstants.UP:
					if(_verticalStatus == Status.FullUp) Stop();
					break;
					
				case WriteConstants.DOWN:
					if(_verticalStatus == Status.FullDown) Stop();
					break;
					
				case WriteConstants.LEFT:
					if(_horizontalStatus == Status.FullLeft) Stop();
					break;
					
				case WriteConstants.RIGHT:
					if(_horizontalStatus == Status.FullRight) Stop();
					break;
					
				case WriteConstants.UP_LEFT:
					if(_horizontalStatus == Status.FullLeft) MoveUp();
					else if(_verticalStatus == Status.FullUp) MoveLeft();
					break;
					
				case WriteConstants.UP_RIGHT:
					if(_horizontalStatus == Status.FullRight) MoveUp();
					else if(_verticalStatus == Status.FullUp) MoveRight();
					break;
					
				case WriteConstants.DOWN_LEFT:
					if(_horizontalStatus == Status.FullLeft) MoveDown();
					else if(_verticalStatus == Status.FullDown) MoveLeft();
					break;
					
				case WriteConstants.DOWN_RIGHT:
					if(_horizontalStatus == Status.FullRight) MoveDown();
					else if(_verticalStatus == Status.FullDown) MoveRight();
					break;
			}
			
			_lastRead1 = b1;
			_lastRead2 = b2;
			return true;
		}
		
		/// <summary>
		/// Attempts to fill the air tank as much as possible without firing
		/// </summary>
		/// <returns>True if successful, false if not, but not always reliable since its threaded</returns>
		public bool Prime()
		{
			if(_workerThread != null)
			{
				if(_workerThread.ThreadState == ThreadState.Running)
					_workerThread.Abort();
				_workerThread = null;
				Thread.Sleep(500);
			}
			
			_workerThread = new Thread(new ThreadStart(primeHelper));
			_workerThread.Start();
			
			return true;
		}
		
		//TODO: Implement!
		/// <summary>
		/// Not yet implemented, but will one day calibrate the device to 
		/// its absolute center position
		/// </summary>
		/// <returns>false, not yet implemented</returns>
		public override bool Center()
		{
			return false;
		}
		
		/// <summary>
		/// Closes the connection and related threads to the launcher
		/// </summary>
		public override void Close()
		{
			if(_workerThread != null)
			{
				_workerThread.Abort();
				_workerThread = null;
			}
			base.Close();
		}
		
		/// <summary>
		/// Suspends timers and threads related to the launcher
		/// </summary>
		public override void Pause()
		{
			base.Pause();
			if(_workerThread != null && _workerThread.ThreadState == ThreadState.Running)
				_workerThread.Suspend();
		}
		
		/// <summary>
		/// Starts back suspended timers and threads related to the launcher
		/// </summary>
		public override void Continue()
		{
			base.Continue();
			if(_workerThread != null && _workerThread.ThreadState == ThreadState.Suspended)
				_workerThread.Start();
		}
		#endregion
		
		#region Private Methods
		/// <summary>
		/// A helper method call on by a Thread so that we can return interactivity to the user
		/// </summary>
		private void fireHelper()
		{
			for(int i = 0; i<_firingTimes; i++)
			{
				primeHelper();
				PerformCustomCommand(WriteConstants.FIRE);
				while(true)
				{
					if(_firingStatus == Status.DoneFiring)
					{
						Stop();
						Thread.Sleep(500);
						if(PrimeAfterFire)
							primeHelper();
						return;
					}
				}
			}
		}
		
		/// <summary>
		/// A helper method called by a thread to prime the air tank so we can 
		/// return interactivity to the user
		/// </summary>
		private void primeHelper()
		{
			if(_firingStatus == Status.DonePriming)
				return;
			
			PerformCustomCommand(WriteConstants.FIRE);

			while(true)
			{
				if(_firingStatus == Status.DonePriming)
				{
					Stop();
					return;
				}
			}
		}
		#endregion
		
		
		private void onDataReceived(object sender, LibHid.DataRecievedEventArgs e)
		{
			_data = e.data;
			this.UpdateStatus();
			
		}
		
	}
}
