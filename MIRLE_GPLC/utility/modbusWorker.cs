using MIRLE_GPLC.form;
using MIRLE_GPLC.Model;
using Modbus.TCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MIRLE_GPLC.Utility
{
    internal class modbusWorker
    {
        public static ProjectDataView presentView = null;

        private Thread workerThread;
        private ModbusSocket socket;

        public modbusWorker(PLC plc)
        {
            workerThread = new Thread(new ParameterizedThreadStart(modbusTCPWorker));
            workerThread.Start(plc);
        }

        private void modbusTCPWorker(object o)
        {
            try
            {
                modbusTCPWorker(o as PLC);
            }
            catch (ThreadAbortException)
            {
            }
        }
        private void modbusTCPWorker(PLC plc)
        {
            if (plc == null)
            {
                return;
            }

            // initialize modbus TCP/IP
            socket = new ModbusSocket(plc.ip, Convert.ToUInt16(plc.port));

            // read periodically
            while (socket != null && socket.connected)
            {
                try
                {
                    List<string> resultList = new List<string>();
                    foreach (Tag r in plc.tags)
                    {
                        resultList.Add(modbusRead(r.id, r.addr, r.type).ToString());
                    }
                    if (presentView != null && !presentView.IsDisposed && !presentView.Disposing)
                    {
                        presentView.RefreshTagList(resultList);
                    }
                    // spin wait
                    SpinWait.SpinUntil(() => false, plc.polling_rate);
                }
                catch (Exception)
                {
                }
            }
        }

        private long modbusRead(long id, int addr, DataType type)
        {
            return modbusRead(Convert.ToByte(id), Convert.ToUInt16(addr), DataTypeUtil.size(type));
        }
        private long modbusRead(byte id, ushort addr, ushort length)
        {
            // get the most significant digit
            int pow = (int)Math.Pow(10, Math.Floor(Math.Log10(addr)));
            int ldigit = (int)Math.Floor((double)addr / pow);
            addr = (pow > 1) ? (ushort)(addr % pow) : addr;
            byte[] data = new byte[length * 2];
            // four operation type
            switch (ldigit)
            {
                case 1:
                    socket.ReadCoils(1, id, addr, length, ref data);
                    break;
                case 2:
                    socket.ReadDiscreteInputs(2, id, addr, length, ref data);
                    break;
                case 3:
                    socket.ReadInputRegister(3, id, addr, length, ref data);
                    return (data[0] << 8) + data[1];
                case 4:
                    socket.ReadHoldingRegister(4, id, addr, length, ref data);
                    return (data[0] << 8) + data[1];
                default:
                    break;
            }
            return 0;
        }

        public void abort()
        {
            this.workerThread.Abort();
        }
    }
}
