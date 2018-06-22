using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using _3DPrinter.setting;

namespace _3DPrinter.utils
{
    public class FileAssociator
    {


        public static void Associate(string extension,string progID, string description)
        {
            string icon = System.AppDomain.CurrentDomain.BaseDirectory + "ptr_3d.ico";
            string application = System.AppDomain.CurrentDomain.BaseDirectory + "3DPrinter.exe";

            RegistryKey classes = Registry.CurrentUser.OpenSubKey("Software\\Classes", true);
            classes.CreateSubKey(extension).SetValue("", progID);

            if (progID != null && progID.Length > 0)
            {
                using (RegistryKey key = classes.CreateSubKey(progID))
                {
                    if (description != null)
                        key.SetValue("", description);
                    if (icon != null)
                        key.CreateSubKey("DefaultIcon").SetValue("", ToShortPathName(icon));
                    if (application != null)
                        key.CreateSubKey(@"Shell\Open\Command").SetValue("", ToShortPathName(application) + " \"%1\"");
                }
            }
        }

        private static string ToShortPathName(string longName)
        {
            StringBuilder s = new StringBuilder(1000);
            uint iSize = (uint)s.Capacity;
            uint iRet = GetShortPathName(longName, s, iSize);
            return s.ToString();
        }


        [DllImport("Kernel32.dll")]
        private static extern uint GetShortPathName(string lpszLongPath, [Out] StringBuilder lpszShortPath, uint cchBuffer);

        [DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);


        public static void AssociateFiles()
        {
            string progid = "ptr_3d";
/*
            int p = -1,p2 = -1;
            for (int i = 0; i < progid.Length; i++)
            {
                char c = progid[i];
                if (c == ' ') p2 = i;
                if (c >= '0' && c <= '9')
                {
                    p = i;
                    break;
                }
            }

            if (p > 0)
                progid = progid.Substring(0, p2>0 ? p2 : p).Trim();
            progid = progid.Replace(" ", "-");
*/
            progid = "ptr_3d";

            if (SettingsProvider.Instance.Global_Settings.StlFile)
                Associate(".stl", progid, "STL file");
            if (SettingsProvider.Instance.Global_Settings.ObjFile)
                Associate(".obj", progid, "OBJ file");
            if (SettingsProvider.Instance.Global_Settings.GFile)
                Associate(".g", progid, "G-Code");
            if (SettingsProvider.Instance.Global_Settings.GcoFile)
                Associate(".gco", progid, "G-Code");
            if (SettingsProvider.Instance.Global_Settings.GcodeFile)
                Associate(".gcode", progid, "G-Code");
            if (SettingsProvider.Instance.Global_Settings.NcFile)
                Associate(".nc", progid, "G-Code");

            SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero); 
        }

    }

}
