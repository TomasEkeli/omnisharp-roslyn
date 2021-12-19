using System;
using OmniSharp.MSBuild.Discovery;
using OmniSharp.Utilities;

namespace OmniSharp
{
    public static class HostHelpers
    {
        public static int Start(Func<int> action)
        {
            try
            {
                if (PlatformHelper.IsMono)
                {
                    // Mono uses ThreadPool threads for its async/await implementation.
                    // Ensure we have an acceptable lower limit on the threadpool size to avoid deadlocks and ThreadPool starvation.
                    const int MIN_WORKER_THREADS = 8;

                    System.Threading.ThreadPool.GetMinThreads(out int currentWorkerThreads, out int currentCompletionPortThreads);

                    if (currentWorkerThreads < MIN_WORKER_THREADS)
                    {
                        System.Threading.ThreadPool.SetMinThreads(MIN_WORKER_THREADS, currentCompletionPortThreads);
                    }
                }

                return action();
            }
            catch (MSBuildNotFoundException mnfe)
            {
                Console.Error.WriteLine(mnfe.Message);
                return 0xbad;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.ToString());
                return 0xbad;
            }
        }
    }
}
