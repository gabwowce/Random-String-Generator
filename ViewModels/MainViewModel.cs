using Random_String_Generator.Helpers;
using Random_String_Generator.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Random_String_Generator.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly DataRepository dataRepository;
        private ObservableCollection<GeneratedData> generatedData;
        private bool isRunning;
        private List<Thread> threads;
        private Random random = new Random();
        private int? threadCount;
        private bool hasErrorOccurred = false;



        public MainViewModel()
        {
            dataRepository = new DataRepository();
            GeneratedData = new ObservableCollection<GeneratedData>();
            StartCommand = new RelayCommand(Start, CanStart);
            StopCommand = new RelayCommand(Stop, CanStop);
            ClearCommand = new RelayCommand(Clear, CanClear);

        }


        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand ClearCommand { get; }

        public ObservableCollection<GeneratedData> GeneratedData
        {
            get { return generatedData; }
            set
            {
                generatedData = value;
                OnPropertyChanged(nameof(GeneratedData));
            }
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    OnPropertyChanged(nameof(IsRunning));
                    OnPropertyChanged(nameof(CanStart));
                    OnPropertyChanged(nameof(CanStop));
                }
            }
        }

        public int? ThreadCount
        {
            get { return threadCount; }
            set
            {
                threadCount = value;
                OnPropertyChanged(nameof(ThreadCount));
            }
        }

        private void Start()
        {
            if (ThreadCount < 2 || ThreadCount > 15 || ThreadCount == null)
            {
                MessageBox.Show("Thread count must be between 2 and 15.", "Invalid Thread Count", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                IsRunning = true;
                hasErrorOccurred = false;
                threads = new List<Thread>();

                for (int i = 1; i <= ThreadCount; i++)
                {
                    int threadId = i;
                    Thread thread = new Thread(() => GenerateData(threadId));
                    threads.Add(thread);
                    thread.Start();
                }
            }
            catch (Exception ex)
            {
                hasErrorOccurred = true;
                System.Windows.MessageBox.Show($"Failed to start threads: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private bool CanStart()
        {
            return !isRunning; 
        }

        public void Stop()
        {
            try
            {
                IsRunning = false;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to stop threads: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanStop()
        {
            return isRunning;
        }

        private void Clear()
        {
            try
            {
                GeneratedData.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to clear data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanClear()
        {
            return !isRunning && GeneratedData != null && GeneratedData.Count > 0;
        }

        private void GenerateData(int threadId)
        {
            try
            {
                while (IsRunning)
                {
                    int delay = random.Next(500, 2000);
                    Thread.Sleep(delay);

                    int length = random.Next(5, 11);
                    string data = GenerateRandomString(length);

                    var generatedDataItem = new GeneratedData
                    {
                        ThreadID = threadId,
                        Time = DateTime.Now,
                        Data = data
                    };

                    try
                    {
                        dataRepository.SaveGeneratedData(generatedDataItem);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show($"Thread {threadId} encountered an error and will be stopped: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                        return;
                    }

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            
                            GeneratedData.Add(generatedDataItem);
                            if (GeneratedData.Count > 20)
                            {
                                GeneratedData.RemoveAt(0);
                            }

                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show($"Thread {threadId} encountered an error updating UI and will be stopped: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                            return;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Thread {threadId} encountered a critical error: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                
            }
        }


        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }





        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}

