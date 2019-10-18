using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using SortingApi.Model;
using SortingApi.Repository;

namespace SortingApi.SortingLogic
{
    public class SortingScheduler : ISortingScheduler
    {
        // Normally, the repository would be a DB
        // However, because, from the specification of the task, I don't know what DB software the machine this will be running on
        // will have, I decided to use files instead
        private static readonly IRepository<Execution> Repository = new ExecutionRepository(".\\Execution");
        private static readonly ISortAlgorithm<int> SortAlgorithm = new MergeSortAlgorithm();
        private Timer _timer;

        // This scheduler would be a service that reads from the DB, sorts and writes back
        // However, for simplicity, I made it a timer
        public void ScheduleSorting()
        {
            _timer = new Timer(1);
            _timer.Elapsed += (sender, e) => TimerElapsed(sender, e);
            _timer.Start();
        }

        private void TimerElapsed(
            object sender,
            ElapsedEventArgs e)
        {
            _timer.Stop();

            List<Execution> executions = Repository.GetAll();
            if (executions != null && executions.Count > 0)
            {
                List<Execution> pendingExecutions = executions.Where(x => x.Status == ExecutionStatus.Pending).ToList();
                foreach (Execution pendingExecution in pendingExecutions)
                {
                    pendingExecution.Output = SortAlgorithm.Sort(pendingExecution.Input);
                    pendingExecution.Duration = (int)(DateTime.Now - pendingExecution.TimeStamp).TotalMilliseconds;
                    pendingExecution.Status = ExecutionStatus.Completed;
                    Repository.Store(pendingExecution);
                }
            }

            ScheduleSorting();
        }
    }
}
