using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Win32;

namespace _3DPrinter.connection
{
    public abstract class PrinterConnectorBase
    {
        public delegate void OnPauseChanged(bool paused);
        /// <summary>
        /// These delegate methods are called after pause state is changed.
        /// </summary>
        public OnPauseChanged eventPauseChanged;

        abstract public void Activate();
        abstract public void Deactivate();
        abstract public bool Connect();
        abstract public bool Disconnect(bool force);
        abstract public bool IsConnected();
        abstract public void InjectManualCommand(string command);
        abstract public void InjectManualCommandFirst(string command);
        abstract public bool HasInjectedMCommand(int code);
        abstract public UserControl ConnectionDialog();
        abstract public string Name { get; }
        abstract public string Id { get; }
        abstract public void SetConfiguration(RegistryKey key);
        abstract public void SaveToRegistry();
        abstract public void LoadFromRegistry();
        abstract public void Emergency();
        abstract public void RunJob();
        abstract public void PauseJob(string text);
        abstract public void ContinueJob();
        abstract public void KillJob();
        abstract public bool IsJobRunning();
        abstract public void TrySendNextLine();
        abstract public void ResendLine(int line);
        abstract public void GetInjectLock();
        abstract public void ReturnInjectLock();
        abstract public bool IsPaused { get; }
        abstract public int MaxLayer { get; }
        abstract public void RunPeriodicalTasks();
        abstract public void ToggleETAMode();
        abstract public string ETA { get; }
        abstract public Printjob Job { get; }
        abstract public int InjectedCommands { get; }
        abstract public void AnalyzeResponse(string res);
    }
}
