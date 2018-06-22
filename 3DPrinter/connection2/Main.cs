using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




using RepetierHostTester.model;

 using RepetierHostTester.connector;


namespace RepetierHostTester
{
    public class Main
    {
        public static Main main;
       
        
        // public static FormPrinterSettings printerSettings;
        public static PrinterConnection conn;
        public static _3DPrinter.MainWindow window;

        //public static GlobalSettings globalSettings = null;


        public static bool IsMono = Type.GetType("Mono.Runtime") != null;

        
       

        public Main(_3DPrinter.MainWindow wnd)
        {
            main = this;

            window = wnd;



            //Custom.Initialize();

            conn = new PrinterConnection();

            //conn.connector = new SerialConnector();
            
            //printerSettings = new FormPrinterSettings();
            //printerModel = new PrinterModel();

            conn.connector = new SerialConnector();
            
            
            conn.analyzer.start(true);
          
        }

        public void Connect()
        {
            if (conn.connector.IsConnected())
            {
                conn.close();
            }
            else
            {
                conn.open();
            }
        }

        public PrinterConnection GetConnection()
        {
            return conn;
        }

        public _3DPrinter.MainWindow GetMain()
        {
            return window;
        }
        

    }
}
