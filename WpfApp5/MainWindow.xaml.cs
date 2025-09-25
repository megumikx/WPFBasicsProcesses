using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<ProcessData> Processes { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Processes = new ObservableCollection<ProcessData>();
            ProcessesGrid.ItemsSource = Processes;
            RefreshProcesses();
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshProcesses();
        }

        private void KillBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessesGrid.SelectedItem is ProcessData selectedProcess)
            {
                try
                {
                    var process = Process.GetProcessById(selectedProcess.Id);
                    process.Kill();
                    RefreshProcesses();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void RefreshProcesses()
        {
            Processes.Clear();
            
            try
            {
                var processes = Process.GetProcesses();
                foreach (var process in processes)
                {
                    try
                    {
                        Processes.Add(new ProcessData
                        {
                            Id = process.Id,
                            Name = process.ProcessName,
                        });
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"Error:");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}