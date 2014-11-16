using System.Collections.Generic;

namespace ProcessKiller.Logic
{
    using System;
    using System.Linq;
    using System.Threading;

    using global::ProcessKiller.Annotations;

    public class ProcessKiller
    {
        private const string ProcessListFile = "ProcessList.txt";

        private readonly ProcessManager processManager;

        private readonly FileReader fileReader;

        [UsedImplicitly]
        private Timer timer;

        private Dictionary<string, DateTime> startTimes = new Dictionary<string, DateTime>() ;

        public ProcessKiller()
        {
            this.processManager = new ProcessManager();
            this.fileReader = new FileReader(ProcessListFile);
        }

        public void Run()
        {
            var defaultRunTime = TimeSpan.FromSeconds(1);
            this.timer = new Timer(o => this.KillProcesses(), null, defaultRunTime, defaultRunTime);
        }

        private void KillProcesses()
        {
            var processesToKill = this.fileReader.ReadProcessesToKill();
            var allProcesses = this.processManager.GetRunningProcesses();
            var runningProcessesToKill =
                allProcesses.Join(
                    processesToKill,
                    p => p.Name,
                    pc => pc.ProcessName,
                    (p, pc) => new { Process = p, ProcessConfiguration = pc }).ToList();

            foreach (var processToKill in runningProcessesToKill)
            {
                try
                {
                    DateTime startTime = DateTime.Now;

                    if (!this.startTimes.ContainsKey(processToKill.Process.Name))
                    {
                        this.startTimes.Add(processToKill.Process.Name, startTime);
                    }
                    else
                    {
                        startTime = this.startTimes[processToKill.Process.Name];
                    }

                    var secondsFromStart = (DateTime.Now - startTime).TotalSeconds;
                    
                    if (processToKill.ProcessConfiguration.ProcessKillDelayInSeconds > secondsFromStart)
                    {
                        continue;
                    }

                    this.startTimes[processToKill.Process.Name] = DateTime.Now;
                    this.processManager.Kill(processToKill.Process);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch
                {
                }
            }
        }
    }
}