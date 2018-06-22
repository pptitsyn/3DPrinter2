using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3DPrinter.connection
{
    public delegate void OnEEPROMAdded(EEPROMParameter param);

    public class EEPROMParameter
    {
        public string description;
        public int type;
        public int position;
        string val = "";
        bool changed = false;

        public EEPROMParameter(string line)
        {
            update(line);
        }
        public void update(string line)
        {
            string[] lines = line.Substring(4).Split(' ');
            int.TryParse(lines[0], out type);
            int.TryParse(lines[1], out position);
            val = lines[2];
            description = line.Substring(7 + lines[0].Length + lines[1].Length + lines[2].Length);
            changed = false;
        }
        public void save(PrinterConnection conn)
        {
            if (!changed) return; // nothing changed
            string cmd = "M206 T" + type + " P" + position + " ";
            if (type == 3) cmd += "X" + val;
            else cmd += "S" + val;
            conn.injectManualCommand(cmd);
            changed = false;
        }
        //[DisplayName("Description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        //[DisplayName("Value")]
        public string Value
        {
            get { return val; }
            set
            {
                value = value.Replace(',', '.').Trim();
                if (val.Equals(value)) return;
                val = value;
                changed = true;
            }
        }
    }
    public class EEPROMStorage
    {
        public Dictionary<int, EEPROMParameter> list;
        public event OnEEPROMAdded eventAdded = null;

        public EEPROMStorage(PrinterConnection conn)
        {
            this.conn = conn;
            list = new Dictionary<int, EEPROMParameter>();
        }

        private PrinterConnection conn;
        public void Clear()
        {
            list.Clear();
        }
        public void Save()
        {
            foreach (EEPROMParameter p in list.Values)
                p.save(conn);
        }
        public void Add(string line)
        {
            if (!line.StartsWith("EPR:")) return;
            EEPROMParameter p = new EEPROMParameter(line);
            if (list.ContainsKey(p.position))
                list.Remove(p.position);
            list.Add(p.position, p);
            if (eventAdded != null)
                Application.Current.MainWindow.Dispatcher.Invoke(eventAdded, p);

        }
        public void Update()
        {
            conn.injectManualCommand("M205");
        }
        public EEPROMParameter Get(int pos)
        {
            return list[pos];
        }
    }
}
