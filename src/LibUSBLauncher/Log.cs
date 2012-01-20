/*
 * Created by SharpDevelop.
 * User: Anthony.Mason
 * Date: 5/7/2007
 * Time: 8:30 AM
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace LibUSBLauncher
{
	/// <summary>
	/// Singleton instance to access the log, should be thread-safe.
	/// </summary>
	public class Log 
	{
		//private bool isDisposed = false;
		private static Log instance = null;
		private static string _file = "SharpLauncher.log";
		
		/// <summary>
		/// Private since this is a singleton
		/// </summary>
		private Log()
		{
			TextWriter tempWriter = new StreamWriter(Log.Filename);
			TextWriter writer = TextWriter.Synchronized(tempWriter);
			writer.WriteLine("SharpLauncher Log Started At " + DateTime.Now.ToString());
			writer.Flush();
			writer.Close();
		}
		
		/// <summary>
		/// Returns the filename of the log, the complete path
		/// </summary>
		public static string Filename
		{
			get { return _file; }
		}
		
		/// <summary>
		/// Returns an instance(singleton) of the log object
		/// </summary>
		public static Log Instance
		{
			get
			{
				if(instance == null)
				{
					instance = new Log();
					return instance;
				}
				else
				{
					return instance;
				}
			}
		}

		/// <summary>
		/// Uses a threadsafe writer to write the msg string to the log file, preceded by the date and time
		/// </summary>
		/// <param name="msg"></param>
		public void Out(string msg)
		{
			TextWriter writer = TextWriter.Synchronized(File.AppendText(Log.Filename));
			writer.WriteLine(DateTime.Now.ToString() + ":\t" + msg);
			writer.Flush();
			writer.Close();
		}
		
		/// <summary>
		/// Uses a threadsafe writer to write information about an exception out to the log file with the date and time it occurred
		/// </summary>
		/// <param name="e"></param>
		public void Out(Exception e)
		{
			TextWriter writer = TextWriter.Synchronized(File.AppendText(Log.Filename));
			writer.WriteLine("");
			writer.WriteLine(DateTime.Now.ToString() + " Exception Occurred: ");
			writer.WriteLine(e.Message);
			writer.WriteLine(e.StackTrace);
			writer.WriteLine("");
			writer.Flush();
			writer.Close();
		}
	}
}
