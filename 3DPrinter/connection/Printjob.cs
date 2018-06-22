using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _3DPrinter.gcode;
using _3DPrinter.view.sliceVisual;

namespace _3DPrinter.connection
{
    public class Printjob
    {
        public class PrintTime
        {
            public int line;
            public long time;
        }
        public bool etaModeNormal = true;
        public bool dataComplete = false;
        public int totalLines;
        public int linesSend;
        public bool exclusive = false;
        public int maxLayer = -1;
        public int mode = 0; // 0 = no job defines, 1 = printing, 2 = finished, 3 = aborted
        public double computedPrintingTime = 0;
        public DateTime jobStarted, jobFinished;
        LinkedList<GCodeCompressed> jobList = new LinkedList<GCodeCompressed>();
        //LinkedList<PrintTime> times = new LinkedList<PrintTime>();
        PrinterConnection con;
        GCodeAnalyzer ana = null;

        public Printjob(PrinterConnection c)
        {
            con = c;
        }

        public void BeginJob()
        {
            con.firePrinterAction("L_BUILDING_PRINT_JOB..."); //"Building print job...");
            dataComplete = false;
            jobList.Clear();
            //times.Clear();
            totalLines = 0;
            linesSend = 0;
            computedPrintingTime = 0;
            con.lastlogprogress = -1000;
            maxLayer = -1;
            mode = 1;
            ana = new GCodeAnalyzer(true);
            con.analyzer.StartJob();
            con.Main.main.UpdateJobButtons.Invoke();
        }
        public void EndJob()
        {
            if (jobList.Count == 0)
            {
                mode = 0;
                con.firePrinterAction("L_IDLE");
                con.Main.main.UpdateJobButtons.Invoke();
//*/                Main.main.printPanel.Invoke(Main.main.printPanel.SetStatusJobFinished);
                return;
            }
            dataComplete = true;
            jobStarted = DateTime.Now;
            con.firePrinterAction("L_PRINTING...");
        }
        public void KillJob()
        {
            mode = 3;
            lock (jobList)
            {
                if (dataComplete == false && jobList.Count == 0) return;
                dataComplete = false;
                jobFinished = DateTime.Now;
                jobList.Clear();
                totalLines = linesSend;
            }
            exclusive = false;
            con.injectManualCommandFirst("M29");
            foreach (GCodeShort code in con.Main.main.codeEditor.getContentArray(3))
            {
                con.injectManualCommand(code.text);
            }
            con.Main.main.UpdateJobButtons.Invoke();
            con.firePrinterAction("L_JOB_KILLED"); //"Job killed");
            DoEndKillActions();
//|*/            Main.main.printPanel.Invoke(Main.main.printPanel.SetStatusJobKilled);
        }
        public void DoEndKillActions()
        {
            if (exclusive) // not a normal print job
            {
                exclusive = false;
                return;
            }
            con.connector.GetInjectLock();
            if (con.afterJobDisableExtruder)
            {
                for (int i = 0; i < con.numberExtruder; i++)
                    con.injectManualCommand("M104 S0 T" + i.ToString());
            }
            if (con.afterJobDisablePrintbed)
                con.injectManualCommand("M140 S0");
            con.connector.ReturnInjectLock();
            if (con.afterJobGoDispose)
                con.doDispose();
            if (con.afterJobDisableMotors)
                con.injectManualCommand("M84");
        }
        public void PushData(string code)
        {
            code = code.Replace('\r', '\n');
            string[] lines = code.Split('\n');
            foreach (string line in lines)
            {
                if (line.Length == 0) continue;
                GCode gcode = new GCode();
                gcode.Parse(line);
                if (!gcode.comment)
                {
                    jobList.AddLast(new GCodeCompressed(gcode));
                    totalLines++;
                }
            }
        }
        public void PushGCodeShortArray(List<GCodeShort> codes)
        {
            foreach (GCodeShort line in codes)
            {
                if (line.Length == 0) continue;
                ana.analyzeShort(line);
                GCode gcode = new GCode();
                gcode.Parse(line.text);
                if (!gcode.comment)
                {
                    jobList.AddLast(new GCodeCompressed(gcode));
                    totalLines++;
                }
                if (line.hasLayer)
                    maxLayer = line.layer;
            }
            computedPrintingTime = ana.printingTime;
        }
        /// <summary>
        /// Check, if more data is stored
        /// </summary>
        /// <returns></returns>
        public bool hasData()
        {
            return linesSend < totalLines;
        }
        public GCode PeekData()
        {
            if (jobList.Count == 0) return null;
            return new GCode(jobList.First.Value);
        }
        public GCode PopData()
        {
            GCode gc = null;
            bool finished = false;
            lock (jobList)
            {
                if (jobList.Count == 0) return null;
                try
                {
                    gc = new GCode(jobList.First.Value);
                    jobList.RemoveFirst();
                    linesSend++;
                    /*PrintTime pt = new PrintTime();
                    pt.line = linesSend;
                    pt.time = DateTime.Now.Ticks;
                    lock (times)
                    {
                        times.AddLast(pt);
                        if (times.Count > 1500)
                            times.RemoveFirst();
                    }*/
                }
                catch { };
                finished = jobList.Count == 0 && mode != 3;
            }
            if (finished)
            {
                dataComplete = false;
                mode = 2;
                jobFinished = DateTime.Now;
                long ticks = (jobFinished.Ticks - jobStarted.Ticks) / 10000;
                long hours = ticks / 3600000;
                ticks -= 3600000 * hours;
                long min = ticks / 60000;
                ticks -= 60000 * min;
                long sec = ticks / 1000;
                //Main.conn.log("Printjob finished at " + jobFinished.ToShortDateString()+" "+jobFinished.ToShortTimeString(),false,3);
                con.log("L_PRINTJOB_FINISHED_AT", false, 3);
                StringBuilder s = new StringBuilder();
                if (hours > 0)
                    s.Append("L_TIME_H:"+ hours.ToString()); //"h:");
                if (min > 0 || hours > 0)
                    s.Append("L_TIME_M:"+ min.ToString());
                s.Append("L_TIME_S"+sec.ToString());
                //Main.conn.log("Printing time:"+s.ToString(),false,3);
                //Main.conn.log("Lines send:" + linesSend.ToString(), false, 3);
                //Main.conn.firePrinterAction("Finished in "+s.ToString());
                con.log("L_PRINTING_TIME:"+s.ToString(), false, 3);
                con.log("L_LINES_SEND:X"+ linesSend.ToString(), false, 3);
                con.firePrinterAction("L_FINISHED_IN"+ s.ToString());
                DoEndKillActions();
               con.Main.main.UpdateJobButtons.Invoke();
//*/                Main.main.printPanel.Invoke(Main.main.printPanel.SetStatusJobFinished);
//                RepetierHost.view.SoundConfig.PlayPrintFinished(false);
            }
            return gc;
        }
        public float PercentDone
        {
            get
            {
                if (totalLines == 0) return 100f;
                return 100f * (float)linesSend / (float)totalLines;
            }
        }
        public static String DoubleToTime(double time)
        {
            long ticks = (long)(time * 1000);
            long hours = ticks / 3600000;
            ticks -= 3600000 * hours;
            long min = ticks / 60000;
            ticks -= 60000 * min;
            long sec = ticks / 1000;
            StringBuilder s = new StringBuilder();
            if (hours > 0)
                s.Append("L_TIME_H:"+hours.ToString()); //"h:");
            if (min > 0 || hours > 0)
                s.Append("L_TIME_M:"+min.ToString());
            s.Append("L_TIME_S"+sec.ToString());
            return s.ToString();
        }
        public String ETA
        {
            get
            {
                //if (linesSend < 3) return "---";
                try
                {
                    long ticks = 0;
                    /*lock (times)
                    {
                        if (times.Count > 100)
                        {
                            PrintTime t1 = times.First.Value;
                            PrintTime t2 = times.Last.Value;
                            ticks = (t2.time - t1.time) / 10000 * (totalLines - linesSend) / (t2.line - t1.line + 1);
                        }
                        else
                            ticks = (DateTime.Now.Ticks - jobStarted.Ticks) / 10000 * (totalLines - linesSend) / linesSend; // Milliseconds
                    }*/
                    if (etaModeNormal)
                    {
                        ticks = (long)(1000.0 * (computedPrintingTime - con.analyzer.printingTime) * (1.0 + 0.01 * con.addPrintingTime) * 100.0 / (float)con.speedMultiply);
                        long hours = ticks / 3600000;
                        ticks -= 3600000 * hours;
                        long min = ticks / 60000;
                        ticks -= 60000 * min;
                        long sec = ticks / 1000;
                        StringBuilder s = new StringBuilder();
                        if (hours > 0)
                            s.Append("L_TIME_H:"+ hours.ToString()); //"h:");
                        if (min > 0 || hours > 0)
                            s.Append("L_TIME_M:"+min.ToString());
                        s.Append("L_TIME_S"+sec.ToString());
                        return s.ToString();
                    }
                    else
                    {
                        DateTime dt = DateTime.Now;
                        dt = dt.AddSeconds(computedPrintingTime - con.analyzer.printingTime);
                        //dt.ToLocalTime();
                        return dt.ToLongTimeString();
                    }
                }
                catch
                {
                    return "-"; // Overflow somewhere
                }
            }
        }
        public LinkedList<GCodeCompressed> GetPendingJobCommands()
        {
            return new LinkedList<GCodeCompressed>(jobList);
        }
    }
}