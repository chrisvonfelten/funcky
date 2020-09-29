using System;

namespace Funcky
{
    public sealed class LinearBackoffRetryPolicy : IRetryPolicy
    {
        private readonly TimeSpan _firstDelay;

        public LinearBackoffRetryPolicy(int maxRetry, TimeSpan firstDelay)
        {
            MaxRetries = maxRetry;
            _firstDelay = firstDelay;
        }

        public int MaxRetries { get; }

#if TIMESPAN_MULTIPLY_SUPPORTED
        public TimeSpan Duration(int onRetryCount)
            => _firstDelay * onRetryCount;
#else
        public TimeSpan Duration(int onRetryCount)
            => TimeSpan.FromTicks(_firstDelay.Ticks * onRetryCount);
#endif
    }
}
