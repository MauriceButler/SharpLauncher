/*
 * Created by SharpDevelop.
 * User: Anthony
 * Date: 5/16/2007
 * Time: 12:55 AM
 * 
 */

using System;
using System.Collections.Generic;
using LibHid;

namespace LibUSBLauncher
{
	/// <summary>
	/// Description of LauncherManager.
	/// </summary>
	public class LauncherManager
	{
		#region Private Data
		private List<USBLauncher> _launchers = new List<USBLauncher>();
		private const int _writeEndPoint = 0x81;
		private const int _readEndPoint = 0x01;
		private const int _timeout = 4096;
		#endregion
		
		#region Dispose
		private bool isDisposed = false;
	 	protected virtual void Dispose(bool disposing)
	  	{
	    	if (!isDisposed) // only dispose once!
	    	{
	    		if(disposing)
	    		{
	    			Disconnect();
	    		}
	    	}
	    	this.isDisposed = true;
	  	}
	 
	 	public void Dispose()
	  	{
	    	Dispose(true);
	    	// tell the GC not to finalize
	    	GC.SuppressFinalize(this);
	  	}
		#endregion
		
		#region Properties
		public int Count
		{
			get { return _launchers.Count; }
		}
		
		public List<USBLauncher> Launchers
		{
			get { return _launchers; }
		}
		#endregion
		
		#region Public Methods
		/// <summary>
		/// Connects to usb launchers, returns true if at least one is found
		/// </summary>
		/// <returns>True if at least one launcher is found</returns>
		public bool Connect(LibHid.UsbHidPort dreamCheeky, LibHid.UsbHidPort rocketBaby)
		{	
			DreamCheekyLauncher launcher1 = new DreamCheekyLauncher(dreamCheeky);
            RocketBabyLauncher launcher2 = new RocketBabyLauncher(rocketBaby);

            if (launcher1.Port.SpecifiedDevice != null)
                Launchers.Add(launcher1);
            
            if (launcher2.Port.SpecifiedDevice != null)
                Launchers.Add(launcher2);

            return Launchers.Count > 0;
		}
		
		/// <summary>
		/// Closes connection to all devices
		/// </summary>
		public void Disconnect()
		{
			foreach(USBLauncher l in _launchers)
			{
				l.Close();
			}
			
			_launchers.Clear();
		}
		#endregion
		
		public void PassMessages(ref System.Windows.Forms.Message m)
		{
			foreach(USBLauncher launcher in Launchers)
			{
				launcher.Port.ParseMessages(ref m);
				
			}
		}
		
		public void PassHandle(System.IntPtr Handle)
		{
			
			foreach(USBLauncher launcher in Launchers)
			{
				launcher.Port.RegisterHandle(Handle);
			}
		}
	}
}
