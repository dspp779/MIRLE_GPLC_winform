using MIRLE_GPLC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Utility
{
    public static class modbusWorkerPool
    {
        // worker resonsible for present project data view
        private static modbusWorker viewWorker;
        // worker thread pool: for worker management and history recording
        private static Dictionary<PLC, modbusWorker> workerPool = new Dictionary<PLC, modbusWorker>();

        // lauch  a view data worker
        public static void lauchViewWorker(PLC plc)
        {
            // stop present worker if exist
            stopViewWorker();
            // start a new worker
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
