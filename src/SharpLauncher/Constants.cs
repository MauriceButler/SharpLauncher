/*
 * Created by SharpDevelop.
 * User: Anthony.Mason
 * Date: 5/8/2007
 * Time: 11:09 AM
 * 
 */

using System;

namespace SharpLauncher
{
	/// <summary>
	/// This contains useful constants used throughout the windows application.
	/// </summary>
	public class Constants
	{
			/// <summary>
			/// These are constants used to get / set values in the application settings
			/// xml file. 
			/// </summary>
			public struct Settings
			{
				public const string WEBCAM_ON_START = "webcam_on_start";
				public const string RESET_LAUNCHER_ON_START = "reset_launcher_on_start";
				public const string WEBCAM_REFRESH_RATE = "webcam_refresh_rate";
				public const string PRIME_AIRTANK = "launcher_prime_tank";
				public const string IMAGE_NUMBER = "image_number";
				public const string CAMERA_WIDTH = "camera_width";
				public const string CAMERA_HEIGHT = "camera_height";
				public const string SLOW = "movement_slow";
				public const string FIRE_WHILE_MOVING = "fire_while_moving";
				public const string CANDID_SHOTS = "candid_shots";
				public const string CANDID_SHOTS_TIMING = "candid_shots_timing";
			}
			
			/// <summary>
			/// The defaults for the application settings xml file.
			/// </summary>
			public struct Defaults
			{
				public const string WEBCAM_ON_START = "N";
				public const string RESET_LAUNCHER_ON_START = "N";
				public const string WEBCAM_REFRESH_RATE = "20";
				public const string PRIME_AIRTANK = "N";
				public const string IMAGE_NUMBER = "1";
				public const string CAMERA_WIDTH = "320";
				public const string CAMERA_HEIGHT = "240";
				public const string SLOW = "N";
				public const string FIRE_WHILE_MOVING = "N";
				public const string CANDID_SHOTS = "0";
				public const string CANDID_SHOTS_TIMING = "500";
			}
			
			/// <summary>
			/// These are set true / false by the application for 
			/// more accurate keyboard control over the launchers.
			/// </summary>
			public struct KeysDown
			{
				public static bool UP;
				public static bool DOWN;
				public static bool LEFT;
				public static bool RIGHT;
				public static bool SHIFT;
				public static bool SPACE;
			}
	}
}
