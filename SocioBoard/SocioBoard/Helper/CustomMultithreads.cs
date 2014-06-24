using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace SocioBoard.Model
{
    public class CustomMultithreads
    {
        public readonly object lockerManageThreads = new object();

        Func<object, int> myMethodName;

        private bool isCounterIncremented = false;

        //public bool isStopCalled = false;
        public static bool isStopCalled = false;

        private readonly object lockerisCounterIncremented = new object();

        //private List<Thread> listThreads = new List<Thread>();
        private static List<Thread> listThreads = new List<Thread>();

        int maxNoOfThreads = 10;

        int counterWorkerThreads = 0;

        //public Events loggingEvents = new Events();

        public CustomMultithreads(Func<object, int> myMethodName, int maxNoOfThreads)
        {
            this.myMethodName = myMethodName;
            this.maxNoOfThreads = maxNoOfThreads;
        }

        public void ThreadMethod(object parameters)
        {
            try
            {
                //Interlocked.Increment(ref counterWorkerThreads);

                if (!isStopCalled)
                {
                    SetTrueIsCounterIncrementedThreadSafe();

                    listThreads.Add(Thread.CurrentThread);

                    Console.WriteLine("Worker Threads : " + counterWorkerThreads);
                    Log("Worker Threads : " + counterWorkerThreads);

                    Log("Thread ID: " + Thread.CurrentThread.ManagedThreadId + " Started");
                    myMethodName(parameters); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Interlocked.Decrement(ref counterWorkerThreads);
                lock (lockerManageThreads)
                {
                    Monitor.Pulse(lockerManageThreads);
                }

                if (counterWorkerThreads == 0)
                {
                    SetFalseIsCounterIncrementedThreadSafe();
                    Thread.Sleep(3000);
                    if (counterWorkerThreads == 0 && !isCounterIncremented)
                    {
                        Console.WriteLine("CustomMultithreads Process Accomplished");
                        //Log("Process Accomplished");
                    }
                }
                Log("Thread ID: " + Thread.CurrentThread.ManagedThreadId + " Completed");
            }
        }

        public void StartMultithreadedMethods(ICollection arrayparameters)
        {
            foreach (object item in arrayparameters)
            {
                if (!isStopCalled)
                {
                    lock (lockerManageThreads)
                    {
                        if (counterWorkerThreads >= maxNoOfThreads)
                        {
                            lock (lockerManageThreads)
                            {
                                Monitor.Wait(lockerManageThreads);
                            }
                        }
                    }

                    if (!isStopCalled)
                    {
                        Thread threadThreadMethod = new Thread(ThreadMethod);
                        threadThreadMethod.Start(item);
                        Interlocked.Increment(ref counterWorkerThreads);
                    }
                }
            }
        }

        public void StartSingleMultithreadedMethod(object item)
        {
            //foreach (object item in arrayparameters)
            {
                if (!isStopCalled)
                {
                    lock (lockerManageThreads)
                    {
                        if (counterWorkerThreads >= maxNoOfThreads)
                        {
                            lock (lockerManageThreads)
                            {
                                Monitor.Wait(lockerManageThreads);
                            }
                        }
                    }

                    if (!isStopCalled)
                    {
                        Thread threadThreadMethod = new Thread(ThreadMethod);
                        threadThreadMethod.Start(item);
                        Interlocked.Increment(ref counterWorkerThreads);
                    }
                }
            }
        }

        public void StopMultithreadedMethods()
        {
            try
            {
                isStopCalled = true;

                for (int i = 0; i < 2; i++)
                {
                    List<Thread> temp = new List<Thread>();
                    temp.AddRange(listThreads);

                    foreach (Thread thread in temp)
                    {
                        try
                        {
                            thread.Abort();
                            listThreads.Remove(thread);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                    Thread.Sleep(1000);
                }
                Log("Threads Stopped");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void SetFalseIsCounterIncrementedThreadSafe()
        {
            lock (lockerisCounterIncremented)
            {
                isCounterIncremented = false;
            }
        }

        private void SetTrueIsCounterIncrementedThreadSafe()
        {
            lock (lockerisCounterIncremented)
            {
                isCounterIncremented = true;
            }
        }

        private void Log(string log)
        {
            //EventsArgs eArgs = new EventsArgs(log);
            //loggingEvents.LogText(eArgs);
        }
      
    }

 
}
