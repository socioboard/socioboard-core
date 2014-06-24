using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;
using System.IO;

namespace SocioboardSchedulerWindowsService
{
    public partial class SocioBoardWindowsService : ServiceBase
    {
          /// <summary>
        /// Public Constructor for WindowsService.
        /// - Put all of your Initialization code here.
        /// </summary>
        public SocioBoardWindowsService()
        {

            this.ServiceName = "SocioBoardWindowsService";
            this.EventLog.Source = "SocioBoardWindowsService";
            this.EventLog.Log = "Application";

            // These Flags set whether or not to handle that specific
            //  type of event. Set to true if you need it, false otherwise.
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;

            if (!EventLog.SourceExists("SocioBoardWindowsService"))
                EventLog.CreateEventSource("SocioBoardWindowsService", "Application");
        }

        static System.Timers.Timer timer = new System.Timers.Timer();
        /// <summary>
        /// The Main Thread: This is where your Service is Run.
        /// </summary>
        /// 
        static void Main()
        {

            bool brasterStatus = false;
            bool dataServicetatus = false;
            var prc = System.Diagnostics.Process.GetProcesses();
            foreach (var item in prc)
            {
                if (item.ProcessName.Contains("SchedularService") && !item.ProcessName.Contains(".vshost"))    
                {
                    TraceService("SocioBoardScheduler has run mode Time : " + DateTime.Now);
                    brasterStatus = true;
                    break;
                }
                
            }
            if (!brasterStatus)
            {
                Process cmd = new Process();
                System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Default Company Name\moopleSchedulerSetup\SocioBoardScheduler.exe");
                try
                {
                    TraceService("SocioBoardScheduler has stop mode Time : " + DateTime.Now);
                }
                catch (Exception ex)
                {
                    TraceService(ex.Message + DateTime.Now);
                }
            }
           
            //handle Elapsed event
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            //This statement is used to set interval to 1 minute (= 60,000 milliseconds)
            timer.Interval = 60000;
            //enabling the timer
            if (!timer.Enabled)
            {
                timer.Enabled = true;
            }
            ServiceBase.Run(new SocioBoardWindowsService());
        }

        /// <summary>
        /// Dispose of objects that need it here.
        /// </summary>
        /// <param name="disposing">Whether or not disposing is going on.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>
        /// OnStart: Put startup code here
        ///  - Start threads, get inital data, etc.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            TraceService("Service Start " + DateTime.Now);
        }
        /// <summary>
        /// OnStop: Put your stop code here
        /// - Stop threads, set final data, etc.
        /// </summary>     
        protected override void OnStop()
        {
            timer.Enabled = false;
            TraceService("Service Stop " + DateTime.Now);


            base.OnStop();
        }

        public static void OnElapsedTime(object source, ElapsedEventArgs e)
        {

            try
            {
                TraceService("Another entry at " + DateTime.Now);
                bool brasterStatus = false;
                var prc = System.Diagnostics.Process.GetProcesses();
                foreach (var item in prc)
                {
                    if (item.ProcessName.Contains("SocioBoardScheduler") && !item.ProcessName.Contains(".vshost"))
                    {
                        TraceService("SocioBoardScheduler has run mode Time : " + DateTime.Now);
                        brasterStatus = true;
                        break;
                    }
                }
                if (!brasterStatus)
                {
                    System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Default Company Name\moopleSchedulerSetup\SocioBoardScheduler.exe").StartInfo.CreateNoWindow = false;
                    try
                    {
                        TraceService("SocioBoardScheduler has stop mode Time : " + DateTime.Now);
                    }
                    catch (Exception ex)
                    {
                        TraceService(ex.Message + DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                TraceService("Error : " + ex.Message);
            }
        }
        /// <summary>
        /// OnPause: Put your pause code here
        /// - Pause working threads, etc.
        /// </summary>
        protected override void OnPause()
        {
            TraceService("Service Paused");
            base.OnPause();
        }

        /// <summary>
        /// OnContinue: Put your continue code here
        /// - Un-pause working threads, etc.
        /// </summary>
        protected override void OnContinue()
        {
            TraceService("Service Started");
            base.OnContinue();
        }

        /// <summary>
        /// OnShutdown(): Called when the System is shutting down
        /// - Put code here when you need special handling
        ///   of code that deals with a system shutdown, such
        ///   as saving special data before shutdown.
        /// </summary>
        protected override void OnShutdown()
        {
            TraceService("Service Shutdown");
            base.OnShutdown();
        }


        public static void TraceService(string content)
        {

            //set up a filestream  ///////////server path/////////// C:\Program Files(x86)\Scheduler\ScheduleWindowService.txt
            //"C:\Program Files (x86)\SocioWindowService\SocioWindowService\"
            FileStream fs = new FileStream(@"C:\Program Files (x86)\Default Company Name\moopleSchedulerSetup\ScheduleWindowService.txt", FileMode.OpenOrCreate, FileAccess.Write);

            //set up a streamwriter for adding text
            StreamWriter sw = new StreamWriter(fs);

            //find the end of the underlying filestream
            sw.BaseStream.Seek(0, SeekOrigin.End);

            //add the text
            sw.WriteLine(content);
            //add the text to the underlying filestream

            sw.Flush();
            //close the writer
            sw.Close();
        }

        /// <summary>
        /// OnCustomCommand(): If you need to send a command to your
        ///   service without the need for Remoting or Sockets, use
        ///   this method to do custom methods.
        /// </summary>
        /// <param name="command">Arbitrary Integer between 128 & 256</param>
        protected override void OnCustomCommand(int command)
        {
            //  A custom command can be sent to a service by using this method:
            //#  int command = 128; //Some Arbitrary number between 128 & 256
            //#  ServiceController sc = new ServiceController("NameOfService");
            //#  sc.ExecuteCommand(command);

            base.OnCustomCommand(command);
        }

        /// <summary>
        /// OnPowerEvent(): Useful for detecting power status changes,
        ///   such as going into Suspend mode or Low Battery for laptops.
        /// </summary>
        /// <param name="powerStatus">The Power Broadcase Status (BatteryLow, Suspend, etc.)</param>
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }

        /// <summary>
        /// OnSessionChange(): To handle a change event from a Terminal Server session.
        ///   Useful if you need to determine when a user logs in remotely or logs off,
        ///   or when someone logs into the console.
        /// </summary>
        /// <param name="changeDescription"></param>
        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
        }

    }
}
