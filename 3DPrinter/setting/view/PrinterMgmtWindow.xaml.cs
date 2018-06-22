using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using _3DPrinter.gcode;
using _3DPrinter.projectManager;

namespace _3DPrinter.setting.view
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PrinterMgmtWindow : Window
    {
        private MainWindow main = null;
        private System.Windows.Threading.DispatcherTimer timer;
        private bool switchPower = false;
        private bool switchHeatExtruder = false;
        private bool switchHeatBed = false;
        

        public PrinterMgmtWindow()
        {
            InitializeComponent();
        }

        public PrinterMgmtWindow(MainWindow main)
        {
            this.main = main;

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();

            InitializeComponent();
            
            // init extruders
            cmbExtruders.Items.Clear();
            foreach (_3DPrinter.setting.model.ExtruderModel ext in ProjectManager.Instance.CurrentProject.projectSettings.PrinterSettings.Extruders)
            {
                cmbExtruders.Items.Add(ext.Name);
            }
            if (ProjectManager.Instance.CurrentProject.projectSettings.PrinterSettings.Extruders.Count() > 0)
                cmbExtruders.SelectedIndex = 0;
            

        }

       

        

        private void btnHomeX_Click(object sender, RoutedEventArgs e)
        {
            main.connection.connector.GetInjectLock();
            main.connection.injectManualCommand("G28 X0");
            main.connection.connector.ReturnInjectLock();            
        }

        private void btnLeftX_Click(object sender, RoutedEventArgs e)
        {
            float d = -10;
            if (main.connection.analyzer.hasXHome && d + main.connection.analyzer.x < ProjectManager.Instance.CurrentProject.projectSettings.PrinterSettings.XMin) d = ProjectManager.Instance.CurrentProject.projectSettings.PrinterSettings.XMin - main.connection.analyzer.x;
            moveHead("X", d);
        }

        private void btnRightX_Click(object sender, RoutedEventArgs e)
        {
            float d = 10;
            if (main.connection.analyzer.hasXHome && d + main.connection.analyzer.x > ProjectManager.Instance.CurrentProject.projectSettings.PrinterSettings.XMax) d = ProjectManager.Instance.CurrentProject.projectSettings.PrinterSettings.XMax - main.connection.analyzer.x;
            
            moveHead("X", d);
        }



        private void moveHead(string axis, float amount)
        {
            main.connection.connector.GetInjectLock();

            main.connection.injectManualCommand("G91");
            if (axis.Equals("Z"))
                main.connection.injectManualCommand("G1 " + axis + amount.ToString(GCode.format) + " F" + main.connection.maxZFeedRate.ToString(GCode.format));
            else
                main.connection.injectManualCommand("G1 " + axis + amount.ToString(GCode.format) + " F" + main.connection.travelFeedRate.ToString(GCode.format));

            main.connection.injectManualCommand("G90");
            main.connection.connector.ReturnInjectLock();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // update ui positions
            txtCurrentXPosition.Text = main.connection.analyzer.RealX.ToString("0.00");
            txtCurrentYPosition.Text = main.connection.analyzer.RealY.ToString("0.00");
            txtCurrentZPosition.Text = main.connection.analyzer.RealZ.ToString("0.00");

            // update temp extruders
            if (cmbExtruders.SelectedIndex != -1) {
                lblExtruderTemp.Text = "  " + main.connection.getTemperature(cmbExtruders.SelectedIndex).ToString("0.00") + "°C /   ";
            }

            // update ui temp bed
            lblBedTemp.Text = main.connection.bedTemp.ToString("0.00") + "°C   ";
            
        }

        private void btnHomeY_Click(object sender, RoutedEventArgs e)
        {
            main.connection.connector.GetInjectLock();
            main.connection.injectManualCommand("G28 Y0");
            main.connection.connector.ReturnInjectLock();        
        }


        private void btnBottomY_Click(object sender, RoutedEventArgs e)
        {            
            float d = -10;
            if (main.connection.analyzer.hasYHome && d + main.connection.analyzer.y < ProjectManager.Instance.CurrentProject.projectSettings.PrinterSettings.YMin) d = ProjectManager.Instance.CurrentProject.projectSettings.PrinterSettings.YMin - main.connection.analyzer.y;
            moveHead("Y", d);
        }


        private void btnTopY_Click(object sender, RoutedEventArgs e)
        {            
            float d = 10;
            if (main.connection.analyzer.hasYHome && d + main.connection.analyzer.y > ProjectManager.Instance.CurrentProject.projectSettings.PrinterSettings.YMax) d = ProjectManager.Instance.CurrentProject.projectSettings.PrinterSettings.YMax - main.connection.analyzer.y;           
            moveHead("Y", d);
        }

        private void btnHomeZ_Click(object sender, RoutedEventArgs e)
        {
            main.connection.connector.GetInjectLock();
            main.connection.injectManualCommand("G28 Z0");
            main.connection.connector.ReturnInjectLock();
        }

        private void btnBottomZ_Click(object sender, RoutedEventArgs e)
        {
            float d = -10;
            if (main.connection.analyzer.hasZHome && d + main.connection.analyzer.z < 0) d = -main.connection.analyzer.z;
            moveHead("Z", d);
        }

        private void btnTopZ_Click(object sender, RoutedEventArgs e)
        {
            float d = 10;
            if (main.connection.analyzer.hasZHome && d + main.connection.analyzer.z > ProjectManager.Instance.CurrentProject.projectSettings.PrinterSettings.PrintAreaHeight) d = ProjectManager.Instance.CurrentProject.projectSettings.PrinterSettings.PrintAreaHeight - main.connection.analyzer.z;
            moveHead("Z", d);

        }

        private void btnRunGCode_Click(object sender, RoutedEventArgs e)
        {           
            if (textGCode.Text.Length < 2) return;
            main.connection.injectManualCommand(textGCode.Text);
            textGCode.Text = "";
        }

        private void btnCommandPower_Click(object sender, RoutedEventArgs e)
        {
            if (main.connection.connector.IsConnected() == false) return;
            main.connection.connector.GetInjectLock();
            if (switchPower)
            {
                main.connection.injectManualCommand("M80");
                switchPower = false;
            }
            else
            {
                main.connection.injectManualCommand("M81");
                switchPower = true;
            }
            main.connection.connector.ReturnInjectLock();
        }
        

        private void btnCommandStopMotor_Click(object sender, RoutedEventArgs e)
        {
            main.connection.injectManualCommand("M84");
        }

        private void btnCommandPark_Click(object sender, RoutedEventArgs e)
        {
            main.connection.doDispose();
        }

        private void btnCommandHome_Click(object sender, RoutedEventArgs e)
        {
            main.connection.connector.GetInjectLock();
            main.connection.injectManualCommand("G28");
            main.connection.connector.ReturnInjectLock();
        }




        private void txtExtruderTemp_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtExtruderTemp.Text = System.Text.RegularExpressions.Regex.Replace(txtExtruderTemp.Text, "[^0-9]+", "");
        }



        private void btnExtruderHeat_Click(object sender, RoutedEventArgs e)
        {
            if (main.connection.connector.IsConnected() == false) return;

            main.connection.connector.GetInjectLock();
            if (switchHeatExtruder == false)
            {
                main.connection.injectManualCommand("M104 S" + txtExtruderTemp.Text);
                switchHeatExtruder = true;
            }
            else
            {
                main.connection.injectManualCommand("M104 S0");
                switchHeatExtruder = false;
            }
            main.connection.connector.ReturnInjectLock();
        }

        private void btnBedHeat_Click(object sender, RoutedEventArgs e)
        {
            if (main.connection.connector.IsConnected() == false) return;

            main.connection.connector.GetInjectLock();
            if (switchHeatBed == false)
            {
                main.connection.injectManualCommand("M140 S" + txtBedTemp.Text);
                switchHeatBed = true;
            }
            else
            {
                main.connection.injectManualCommand("M140 S0");
                switchHeatBed = false;
            }
            main.connection.connector.ReturnInjectLock();
        }

        
    }
}
