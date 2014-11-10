namespace ProcessKiller.Logic
{
    using System;
    using System.Linq;
    using System.Threading;

    public class ProcessKiller
    {
        private const string ProcessListFile = "ProcessList.txt";

        private readonly ProcessManager processManager;

        private readonly FileReader fileReader;

        private Timer timer;

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

            var names = allProcesses.Select(p => p.Name).OrderBy(p => p);

            var runningProcessesToKill = allProcesses.Where(p => processesToKill.Contains(p.Name));

            foreach (var processToKill in runningProcessesToKill)
            {
                this.processManager.Kill(processToKill);
            }
        }
    }
}