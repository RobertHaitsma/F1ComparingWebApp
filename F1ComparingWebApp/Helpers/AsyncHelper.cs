using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Helpers
{
    internal static class AsyncHelper
    {
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return Task.Factory.StartNew<Task<TResult>>(func)
                .Unwrap<TResult>()
                .GetAwaiter()
                .GetResult();
        } 

    }
}
