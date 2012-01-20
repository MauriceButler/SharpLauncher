using System;
using System.Collections.Generic;
using System.Text;

namespace LibHid
{
	/// <summary>
	/// 
	/// </summary>
    public class DataRecievedEventArgs : EventArgs
    {
    	/// <summary>
    	/// 
    	/// </summary>
        public readonly byte[] data;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public DataRecievedEventArgs(byte[] data)
        {
            this.data = data;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DataSendEventArgs : EventArgs
    {
    	/// <summary>
    	/// 
    	/// </summary>
        public readonly byte[] data;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public DataSendEventArgs(byte[] data)
        {
            this.data = data;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public delegate void DataRecievedEventHandler(object sender, DataRecievedEventArgs args);
    
    /// <summary>
    /// 
    /// </summary>
    public delegate void DataSendEventHandler(object sender, DataSendEventArgs args);

    /// <summary>
    /// 
    /// </summary>
    public class SpecifiedDevice : HIDDevice
    {
    	/// <summary>
    	/// 
    	/// </summary>
        public event DataRecievedEventHandler DataRecieved;
        
        /// <summary>
        /// 
        /// </summary>
        public event DataSendEventHandler DataSend;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override InputReport CreateInputReport()
        {
            return new SpecifiedInputReport(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendor_id"></param>
        /// <param name="product_id"></param>
        /// <returns></returns>
        public static SpecifiedDevice FindSpecifiedDevice(int vendor_id, int product_id)
        {
            return (SpecifiedDevice)FindDevice(vendor_id, product_id, typeof(SpecifiedDevice));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oInRep"></param>
        protected override void HandleDataReceived(InputReport oInRep)
        {
            // Fire the event handler if assigned
            if (DataRecieved != null)
            {
                SpecifiedInputReport report = (SpecifiedInputReport)oInRep;
                DataRecieved(this, new DataRecievedEventArgs(report.Data));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void SendData(byte[] data)
        {
            SpecifiedOutputReport oRep = new SpecifiedOutputReport(this);	// create output report
            oRep.SendData(data);	// set the lights states
            try
            {
                Write(oRep); // write the output report
                if (DataSend != null)
                {
                    DataSend(this, new DataSendEventArgs(data));
                }
            }catch (HIDDeviceException ex)
            {
                // Device may have been removed!
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bDisposing"></param>
        protected override void Dispose(bool bDisposing)
        {
            if (bDisposing)
            {
                // to do's before exit
            }
            base.Dispose(bDisposing);
        }

    }
}
