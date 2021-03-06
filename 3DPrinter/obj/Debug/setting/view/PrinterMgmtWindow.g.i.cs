﻿#pragma checksum "..\..\..\..\setting\view\PrinterMgmtWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4A3F9411C284A4BEA74899C4B7259A19"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using _3DPrinter.setting;
using _3DPrinter.setting.model;
using _3DPrinter.setting.view;


namespace _3DPrinter.setting.view {
    
    
    /// <summary>
    /// PrinterMgmtWindow
    /// </summary>
    public partial class PrinterMgmtWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 62 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textGCode;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCurrentXPosition;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCurrentYPosition;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCurrentZPosition;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbExtruders;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblExtruderTemp;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtExtruderTemp;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblBedTemp;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBedTemp;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/3DPrinter;component/setting/view/printermgmtwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.textGCode = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            
            #line 63 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnRunGCode_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtCurrentXPosition = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtCurrentYPosition = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtCurrentZPosition = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            
            #line 97 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnRightX_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 98 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnLeftX_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 99 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnHomeX_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 103 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnTopY_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 104 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnBottomY_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 105 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnHomeY_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 108 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnTopZ_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 109 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnBottomZ_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 110 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnHomeZ_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 120 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnCommandPower_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 121 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnCommandStopMotor_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 122 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnCommandPark_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 123 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnCommandHome_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            this.cmbExtruders = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 20:
            this.lblExtruderTemp = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 21:
            this.txtExtruderTemp = ((System.Windows.Controls.TextBox)(target));
            
            #line 139 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            this.txtExtruderTemp.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtExtruderTemp_TextChanged);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 141 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnExtruderHeat_Click);
            
            #line default
            #line hidden
            return;
            case 23:
            this.lblBedTemp = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 24:
            this.txtBedTemp = ((System.Windows.Controls.TextBox)(target));
            
            #line 155 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            this.txtBedTemp.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtExtruderTemp_TextChanged);
            
            #line default
            #line hidden
            return;
            case 25:
            
            #line 157 "..\..\..\..\setting\view\PrinterMgmtWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnBedHeat_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

