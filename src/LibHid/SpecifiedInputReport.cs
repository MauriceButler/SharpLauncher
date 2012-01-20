using System;
using System.Collections.Generic;
using System.Text;

namespace LibHid
{
	/// <summary>
	/// 
	/// </summary>
    public class SpecifiedInputReport : InputReport
    {
        private byte[] arrData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oDev"></param>
        public SpecifiedInputReport(HIDDevice oDev) : base(oDev)
		{

		}

        /// <summary>
        /// 
        /// </summary>
        public override void ProcessData()
        {
            this.arrData = Buffer;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Data
        {
            get
            {
                return arrData;
            }
        }
    }
}
