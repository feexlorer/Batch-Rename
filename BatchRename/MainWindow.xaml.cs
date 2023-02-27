using Contact;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;
using Microsoft.WindowsAPICodePack.Dialogs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Data;
using System.ComponentModel;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class Files: INotifyPropertyChanged
        {
            public string ShortName { get; set; } = "";
            public string NewName { get; set; } = "";
            public string FullPath { get; set; } = "";
            public string BatchState { get; set; } = "";
            public string Directory { get; set; } = "";

            public event PropertyChangedEventHandler? PropertyChanged;
        }
        public class RuleDisplay
        {
            public string Name { get; set; } = "";
        };
        public class Rules
        {
            public string Name { get; set; } = "";
            public iRule Rule { get; set; }
        };
        ObservableCollection<RuleDisplay> _ruleNames = new ObservableCollection<RuleDisplay>();
        ObservableCollection<Files> _sourceFiles = new ObservableCollection<Files>();
        ObservableCollection<Files> _sourceFolders = new ObservableCollection<Files>();
        ObservableCollection<Rules> _rules = new ObservableCollection<Rules>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var exeFolder = AppDomain.CurrentDomain.BaseDirectory;
            var folderInfo = new DirectoryInfo(exeFolder);
            var dllFiles = folderInfo.GetFiles("*.dll");

            foreach (var file in dllFiles)
            {
                var assembly = Assembly.LoadFrom(file.FullName);
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.IsClass && typeof(iRule).IsAssignableFrom(type))
                    {
                        iRule rule = (iRule)Activator.CreateInstance(type)!;
                        FactoryRule.Register(rule);
                    }
                }
            }
            foreach( string name in FactoryRule.ShowName())
            {
                _ruleNames.Add(new RuleDisplay
                {
                    Name = name,
                });
            }
            ActionListView.ItemsSource= _ruleNames;
            AddListView.ItemsSource = _rules;
        }
        private void AddFileButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new CommonOpenFileDialog();
            screen.Multiselect = true;
            if (screen.ShowDialog() == CommonFileDialogResult.Ok)
            {
                foreach (string file in screen.FileNames)
                {
                    var fullPath = file;
                    var info = new FileInfo(fullPath);
                    var shortName = info.Name;
                    var directory = info.Directory.FullName;

                    _sourceFiles.Add(new
                    Files
                    {
                        ShortName = shortName,
                        NewName = "",
                        FullPath = fullPath,
                        BatchState = "In Progress",
                        Directory = directory
                    });
                }
            }
            FileListView.ItemsSource = _sourceFiles;
        }

        private void AddFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new CommonOpenFileDialog();
            screen.IsFolderPicker = true;
            screen.Multiselect = true;
            if (screen.ShowDialog() == CommonFileDialogResult.Ok)
            {
                foreach (string file in screen.FileNames)
                {
                    var fullPath = file;
                    var info = new FileInfo(fullPath);
                    var shortName = info.Name;
                    var directory = info.Directory.FullName;

                    _sourceFolders.Add(new
                    Files
                    {
                        ShortName = shortName,
                        NewName = "",
                        FullPath = fullPath,
                        BatchState = "In Progress",
                        Directory = directory
                    });
                }
            }
            FolderListView.ItemsSource = _sourceFolders;
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _sourceFiles.Clear();
            _sourceFolders.Clear();
        }

        private void BatchButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _sourceFiles.Count; i++)
            {
                if (_sourceFiles[i].NewName != "")
                {// Rename files
                    string newFullPath = $"{_sourceFiles[i].Directory}\\{_sourceFiles[i].NewName}";
                    System.IO.File.Move(_sourceFiles[i].FullPath, newFullPath);
                    _sourceFiles[i].BatchState = "Done";
                }
            }
            for (int i = 0; i < _sourceFiles.Count; i++)
            {
                if (_sourceFolders[i].NewName != "")
                {// Rename files
                    string newFullPath = $"{_sourceFolders[i].Directory}\\{_sourceFolders[i].NewName}";
                    System.IO.File.Move(_sourceFolders[i].FullPath, newFullPath);
                    _sourceFolders[i].BatchState = "Done";
                }
            }
        }

        private void ActionListView_MouseUp(object sender, MouseButtonEventArgs e)
        {// Add Rule to List Rules
            int i = ActionListView.SelectedIndex;
            iRule currentRule = FactoryRule.Instance().Parse(_ruleNames[i].Name);
            if((_rules.Any(p => p.Name == _ruleNames[i].Name))== false)
            {
                _rules.Add(new Rules
                {
                    Name = _ruleNames[i].Name,
                    Rule = currentRule
                });
            }
        }

        private void ApplyRulesButton_Click(object sender, RoutedEventArgs e)
        {
            if(_sourceFiles.Count() == 0 && _sourceFolders.Count() == 0 || _rules.Count()==0)
            {
                MessageBox.Show("You must add at least a file or a folder and one Renaming rule!");
            }
            else
            {
                for (int i = 0; i < _rules.Count; i++)
                {
                    for (int j = 0; j < _sourceFiles.Count(); j++)
                    {
                        if(_sourceFiles[j].NewName == "")
                        {
                            _sourceFiles[j].NewName = _rules[i].Rule.Rename(_sourceFiles[j].ShortName);
                        }
                        else
                        {
                            _sourceFiles[j].NewName = _rules[i].Rule.Rename(_sourceFiles[j].NewName);
                        }
                        
                    }
                    for (int k = 0; k < _sourceFolders.Count(); k++)
                    {
                        if (_sourceFolders[k].NewName == "")
                        {
                            _sourceFolders[k].NewName = _rules[i].Rule.Rename(_sourceFolders[k].ShortName);
                        }
                        else
                        {
                            _sourceFolders[k].NewName = _rules[i].Rule.Rename(_sourceFolders[k].NewName);
                        }
                    }
                }
            }
            
        }

        private void SavePresetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadPresetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ActionListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
