using eveDirect.Shared.Helper;
using Hangfire.Common;
using Hangfire.Server;
using Hangfire.States;
using Hangfire.Storage;
using System;

namespace Hangfire
{
    /// <summary>
    /// Attribute to skip a job execution if the same job is already running.
    /// Mostly taken from: http://discuss.hangfire.io/t/job-reentrancy-avoidance-proposal/607
    /// </summary>
    public class SkipConcurrentExecutionAttribute : JobFilterAttribute, IServerFilter, IApplyStateFilter
    {
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly int _timeoutInSeconds;

        public SkipConcurrentExecutionAttribute(int timeoutInSeconds)
        {
            if (timeoutInSeconds < 0) throw new ArgumentException("Timeout argument value should be greater that zero.");

            _timeoutInSeconds = timeoutInSeconds;
        }


        public void OnPerforming(PerformingContext filterContext)
        {
            var resource = String.Format(
                                 "{0}.{1}",
                                filterContext.BackgroundJob.Job.Type.FullName,
                                filterContext.BackgroundJob.Job.Method.Name);

            var timeout = TimeSpan.FromSeconds(_timeoutInSeconds);

            try
            {
                var distributedLock = filterContext.Connection.AcquireDistributedLock(resource, timeout);
                filterContext.Items["DistributedLock"] = distributedLock;
            }
            catch (Exception)
            {
                filterContext.Canceled = true;
                filterContext.SetJobParameter("_distributedLock", true);
                //logger.Warn("Cancelling run for {0} job, id: {1} ", resource, filterContext.JobId);
            }
        }

        public void OnPerformed(PerformedContext filterContext)
        {
            if (!filterContext.Items.ContainsKey("DistributedLock"))
            {
                throw new InvalidOperationException("Can not release a distributed lock: it was not acquired.");
            }

            var distributedLock = (IDisposable)filterContext.Items["DistributedLock"];
            distributedLock.Dispose();
        }

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            if (context.NewState.Name == SucceededState.StateName && (context.Connection.GetJobParameter(context.BackgroundJob.Id, "_distributedLock")?.ToBoolean() ?? false))
            {
                context.JobExpirationTimeout = TimeSpan.FromSeconds(1);
            }
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            //throw new NotImplementedException();
        }
    }
}
