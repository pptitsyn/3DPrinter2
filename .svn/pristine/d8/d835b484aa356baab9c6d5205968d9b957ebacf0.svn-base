using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AurelienRibon.Ui.SyntaxHighlightBox;
using ICSharpCode.AvalonEdit.Editing;
using Microsoft.Win32;
using _3DPrinter.model;
using _3DPrinter.projectManager;
using _3DPrinter.setting;
using _3DPrinter.view.sliceVisual;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using Pen = System.Windows.Media.Pen;

namespace _3DPrinter.view.editor
{
    /// <summary>
    /// Логика взаимодействия для CodeEditor.xaml
    /// </summary>
    /// 
    public delegate void ContentChangedEvent(CodeEditor editor);



    public partial class CodeEditor : UserControl
    {


        public enum UndoAction
        {
            ReplaceSelection = 1,

        }

        public class Undo
        {
            int col, row, selCol, selRow;
            string text, oldtext;
            UndoAction action;
            public Undo(UndoAction act, string t, string ot, int c, int r, int sc, int sr)
            {
                action = act;
                text = t;
                oldtext = ot;
                col = c;
                row = r;
                selCol = sc;
                selRow = sr;
            }

        }

        public class Content
        {
            //public string Text;
            public List<GCodeShort> textArray;
            int col = 0, row = 0, selCol = 0, selRow = 0;
            int topRow = 0, topCol = 0;
            bool hasSel;
            LinkedList<Undo> undo = new LinkedList<Undo>();
            LinkedList<Undo> redo = new LinkedList<Undo>();
            CodeEditor editor = null;
            public string name;
            public int etype; // 0 = G-Code, 1 = prepend, 2 = append
            public Content(CodeEditor e, int tp, string _name)
            {
                name = _name;
                //Text = "";
                textArray = new List<GCodeShort>();
                textArray.Add(new GCodeShort(""));
                editor = e;
                etype = tp;
            }

            public void ToActive()
            {
                editor.lines = textArray;
            }
            public string Text
            {
                get
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (GCodeShort code in textArray)
                        sb.AppendLine(code.text);
                    return sb.ToString();
                }
                set
                {
                    string[] la = value.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
                    textArray.Clear();
                    if (la.Length == 0) la = new string[] { "" };
                    foreach (string s in la)
                    {
                        textArray.Add(new GCodeShort(s));
                    }
                    row = col = topRow = topCol = selRow = selCol = 0;
                }
            }

            public void ResetPos()
            {
                col = row = selCol = selRow = topCol = topRow = topCol = 0;
                hasSel = false;
            }


 

            public override string ToString()
            {
                return name;
            }



        }


        int _maxLayer = 0;
        int _showMode = 0;
        int _showMinLayer = 0;
        int _showMaxLayer = 1;

        public event EventHandler ShowModeChanged;
        public event EventHandler ShowMinLayerChanged;
        public event EventHandler ShowMaxLayerChanged;
        public event EventHandler MaxLayerChanged;

        public event ContentChangedEvent contentChangedEvent = null;
        
        List<GCodeShort> lines = new List<GCodeShort>();

        public List<Content> contentTypes = new List<Content>();

        public CodeEditor()
        {
            InitializeComponent();

            Content c = new Content(this, 0, "G-Code");
            contentTypes.Clear();
            contentTypes.Add(c);
            contentTypes.Add(new Content(this, 1, "Start code"));
            contentTypes.Add(new Content(this, 2, "End code"));
            contentTypes.Add(new Content(this, 3, "Run on kill"));
            contentTypes.Add(new Content(this, 4, "Run on pause"));
            contentTypes.Add(new Content(this, 5, "Script 1"));
            contentTypes.Add(new Content(this, 6, "Script 2"));
            contentTypes.Add(new Content(this, 7, "Script 3"));
            contentTypes.Add(new Content(this, 8, "Script 4"));
            contentTypes.Add(new Content(this, 9, "Script 5"));

            c.ToActive();
         //   editor1.CurrentHighlighter = HighlighterManager.Instance.Highlighters["GCODE"];

            // this.Visibility = Visibility.Collapsed;

        }


        public int FileIndex
        {
            get
            {
//                int i = toolFile.SelectedIndex;
//                if (i == 0) return 1;
//                if (i == 1) return 0;
//                return i;
                return 1;
            }
        }


        public void setContent(int idx, string text)
        {
            try
            {
                this.editor1.Text = text;
            }
            catch (Exception e)
            {
                string s = e.Message;
            }
//            this.Text = text;
//            fastLayerUpdate();
//            if (contentChangedEvent != null)
//            contentChangedEvent(this);
        }

        public void fastLayerUpdate()
        {
            GCodeAnalyzer a = new GCodeAnalyzer(true);
            foreach (GCodeShort code in getContentArray(1))
                a.analyzeShort(code);
            foreach (GCodeShort code in getContentArray(0))
                a.analyzeShort(code);
            foreach (GCodeShort code in getContentArray(2))
                a.analyzeShort(code);
            MaxLayer = a.layer;


            SettingsProvider.Instance.CommonSettings.MaxLayer = MaxLayer;
            SettingsProvider.Instance.CommonSettings.MinLayer = 1;
            
            SettingsProvider.Instance.CommonSettings.HigherLayer = MaxLayer;
            SettingsProvider.Instance.CommonSettings.LowerLayer = 1;


            SettingsProvider.Instance.PrintingStatistic.LayersCount = a.layer;
            SettingsProvider.Instance.PrintingStatistic.LineCodeCount = lines.Count;
            SettingsProvider.Instance.PrintingStatistic.FilamentLength = (int)a.filamentLength;
            if (a.printingTime > 0)
            {
              SettingsProvider.Instance.PrintingStatistic.PrintingTime = a.printingTime;
            }
        }

        public double printingTime = 0;

        public void ChangeGCode(string gCode)
        {
            Text = gCode;
        }


        public string Text
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (GCodeShort code in lines)
                    sb.AppendLine(code.text);
                return sb.ToString();
            }
            set
            {
                string[] la = value.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
                lines.Clear();

                if (la.Length == 0) la = new string[] { "" };
                foreach (string s in la)
                {
                    lines.Add(new GCodeShort(s));
                }
            }
        }

        public List<GCodeShort> getContentArray(int idx)
        {
            Content c = (Content)contentTypes[idx];
            return c.textArray;
//            return null;
        }

        public int MaxLayer
        {
            get { return _maxLayer; }
            set
            {
                if (value != _maxLayer)
                {
                    _maxLayer = value;
                }
            }
        }

        public int ShowMode
        {
            get { return _showMode; }
            set
            {
                if (value != _showMode)
                {
                    _showMode = value;
                    OnShowModeChanged(EventArgs.Empty);
                    if (contentChangedEvent != null)
                        contentChangedEvent(this);
                }
            }
        }
        public int ShowMinLayer
        {
            get { return _showMinLayer; }
            set
            {
                if (value < 0) value = 0;
                if (value > _maxLayer) value = MaxLayer;
                if (value != _showMinLayer)
                {
                    _showMinLayer = value;
                 //   sliderShowFirstLayer.Value = value;
                    OnShowMinLayerChanged(EventArgs.Empty);
                    if (_showMode == 1 || _showMinLayer > _showMaxLayer)
                    {
                        _showMaxLayer = value;
                 //       sliderShowMaxLayer.Value = value;
                        OnShowMaxLayerChanged(EventArgs.Empty);
                    }
                    if (_showMode != 0)
                        if (contentChangedEvent != null)
                            contentChangedEvent(this);
                }
            }
        }
        public int ShowMaxLayer
        {
            get { return _showMaxLayer; }
            set
            {
                if (value < 0) value = 0;
                if (value > _maxLayer) value = MaxLayer;
                if (value != _showMaxLayer)
                {
                    _showMaxLayer = value;
                //    sliderShowMaxLayer.Value = value;
                    OnShowMaxLayerChanged(EventArgs.Empty);
                    if (_showMode == 1 || _showMaxLayer < _showMinLayer)
                    {
                        _showMinLayer = value;
                     //   sliderShowFirstLayer.Value = value;
                        OnShowMinLayerChanged(EventArgs.Empty);
                    }
                    if (_showMode != 0)
                        if (contentChangedEvent != null)
                            contentChangedEvent(this);
                }
            }
        }


        protected void OnShowModeChanged(EventArgs e)
        {
            if (ShowModeChanged != null)
            {
                ShowModeChanged(this, e);
            }
        }
        protected void OnShowMinLayerChanged(EventArgs e)
        {
            if (ShowMinLayerChanged != null)
            {
                ShowMinLayerChanged(this, e);
            }
        }
        protected void OnShowMaxLayerChanged(EventArgs e)
        {
            if (ShowMaxLayerChanged != null)
            {
                ShowMaxLayerChanged(this, e);
            }
        }
        protected void OnMaxLayerChanged(EventArgs e)
        {
            if (MaxLayerChanged != null)
            {
                MaxLayerChanged(this, e);
            }
        }



        public int selectedLine = 0;
        public int selectedLinesCount = 0;


        private void Editor_OnTextChanged(object sender, TextChangedEventArgs e)
        {

            this.Text = this.editor1.Text;
            ProjectManager.Instance.CurrentProject.gCode = Text;
            fastLayerUpdate();
            if (contentChangedEvent != null)
                contentChangedEvent(this);

        }

        private void Editor_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            selectedLine = tb.GetLineIndexFromCharacterIndex(tb.SelectionStart);
            selectedLinesCount = tb.GetLineIndexFromCharacterIndex(tb.SelectionStart + tb.SelectionLength) - selectedLine+1;
            if (contentChangedEvent != null)
                contentChangedEvent(this);
        }

        internal void SaveCode()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "G-Code Files | *.gcode";
            if (saveFileDialog.ShowDialog() == true)
            {
                string file = saveFileDialog.FileName;
                File.WriteAllText(saveFileDialog.FileName, this.editor1.Text);
            }
        }

        private void Save_GCode_Click(object sender, RoutedEventArgs e)
        {
            SaveCode();
        }

        private void Editor1_OnTextChanged(object sender, EventArgs e)
        {
            this.Text = this.editor1.Text;
            ProjectManager.Instance.CurrentProject.gCode = Text;
            fastLayerUpdate();
            if (contentChangedEvent != null)
                contentChangedEvent(this);
        }

        private void Editor1_OnPositionChanged(object sender, EventArgs e)
        {
            Caret caret = (Caret) sender;
            selectedLine = caret.Line;
            selectedLinesCount = 1;
            if (contentChangedEvent != null)
                contentChangedEvent(this);
        }

        private void Editor1_OnSelectionChanged(object sender, EventArgs e)
        {
            TextArea textArea = (TextArea) sender;
            selectedLine = Math.Min(textArea.Selection.EndPosition.Line, textArea.Selection.StartPosition.Line);
            selectedLinesCount = Math.Abs(textArea.Selection.EndPosition.Line - textArea.Selection.StartPosition.Line);
            if (contentChangedEvent != null)
                contentChangedEvent(this);
        }
    }


    public static class CaretBehavior
    {
        public static readonly DependencyProperty ObserveCaretProperty =
            DependencyProperty.RegisterAttached
            (
                "ObserveCaret",
                typeof(bool),
                typeof(CaretBehavior),
                new UIPropertyMetadata(false, OnObserveCaretPropertyChanged)
            );
        public static bool GetObserveCaret(DependencyObject obj)
        {
            return (bool)obj.GetValue(ObserveCaretProperty);
        }
        public static void SetObserveCaret(DependencyObject obj, bool value)
        {
            obj.SetValue(ObserveCaretProperty, value);
        }
        private static void OnObserveCaretPropertyChanged(DependencyObject dpo,
                                                       DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = dpo as TextBox;
            if (textBox != null)
            {
                if ((bool)e.NewValue == true)
                {
                    textBox.SelectionChanged += textBox_SelectionChanged;
                }
                else
                {
                    textBox.SelectionChanged -= textBox_SelectionChanged;
                }
            }
        }

        static void textBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int caretIndex = textBox.CaretIndex;
            SetCaretIndex(textBox, caretIndex);
            SetLineIndex(textBox, textBox.GetLineIndexFromCharacterIndex(caretIndex));
        }

        private static readonly DependencyProperty CaretIndexProperty =
            DependencyProperty.RegisterAttached("CaretIndex", typeof(int), typeof(CaretBehavior));
        public static void SetCaretIndex(DependencyObject element, int value)
        {
            element.SetValue(CaretIndexProperty, value);
        }
        public static int GetCaretIndex(DependencyObject element)
        {
            return (int)element.GetValue(CaretIndexProperty);
        }

        private static readonly DependencyProperty LineIndexProperty =
            DependencyProperty.RegisterAttached("LineIndex", typeof(int), typeof(CaretBehavior));
        public static void SetLineIndex(DependencyObject element, int value)
        {
            element.SetValue(LineIndexProperty, value);
        }
        public static int GetLineIndex(DependencyObject element)
        {
            return (int)element.GetValue(LineIndexProperty);
        }


        private static readonly DependencyProperty SelectedLinesCountProperty =
            DependencyProperty.RegisterAttached("SelectedLinesCount", typeof(int), typeof(CaretBehavior));
        public static void SetSelectedLinesCount(DependencyObject element, int value)
        {
            element.SetValue(SelectedLinesCountProperty, value);
        }
        public static int GetSelectedLinesCount(DependencyObject element)
        {
            return (int)element.GetValue(SelectedLinesCountProperty);
        }

    }
}
