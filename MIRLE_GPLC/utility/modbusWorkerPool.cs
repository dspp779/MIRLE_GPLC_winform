using MIRLE_GPLC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Utility
{
    public static class modbusWorkerPool
    {
        private static modbusWorker viewWorker;
        private static Dictionary<PLC, modbusWorker> workerPool = new Dictionary<PLC, modbusWorker>();

        public static void lauchViewWorker(PLC plc)
        {
            stopViewWorker();
            viewWorker = new modbusWorker(plc);
        }

        public static void stopViewWorker()
        {
            if (viewWorker != null)
            {
                viewWorker.stop();
                viewWorker = null;
            }
        }
    }
}
