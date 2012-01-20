using System;
using System.Collections.Generic;
using System.Text;

namespace LibHid
{
	/// <summary>
	/// 
	/// </summary>
    public class SpecifiedOutputReport : OutputReport
    {
    	/// <summary>
    	/// 
    	/// </summary>
    	/// <param name="oDev"></param>
        public SpecifiedOutputReport(HIDDevice oDev) : base(oDev) {

        }

    	/// <summary>
    	/// 
    	/// </summary>
    	/// <param name="data"></param>
    	/// <returns></returns>
        public bool SendData(byte[] data)
        {
            byte[] arrBuff = Buffer; //new byte[Buffer.Length];
            for (int i = 1; i < arrBuff.Length; i++)
            {
                arrBuff[i] = data[i];
            }

            //Buffer = arrBuff;

            //returns false if the data does not fit in the buffer. else true
            if (arrBuff.Length < data.Length)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
