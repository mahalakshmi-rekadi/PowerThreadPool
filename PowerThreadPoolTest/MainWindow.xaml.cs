﻿using PowerThreadPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PowerThreadPoolTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PowerPool powerPool = new PowerPool(new ThreadPoolOption() { MaxThreads = 3 });
        public MainWindow()
        {
            InitializeComponent();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            powerPool.Stop();
        }

        private void OutputMsg(string msg)
        {
            this.Dispatcher.Invoke(() =>
            {
                log.Text += msg + "\n";
                sv.ScrollToEnd();
            });
            OutputCount();
        }

        private void OutputCount()
        {
            this.Dispatcher.Invoke(() =>
            {
                string countTxt = "waiting: " + powerPool.WaitingThreadCount + "\n" + "running: " + powerPool.RunningThreadCount;
                count.Text = countTxt;
            });
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            powerPool.QueueWorkItem(() =>
            {
                OutputMsg("Thread0: START");
                Thread.Sleep(10000);
                OutputMsg("Thread0: END");
            }, (res) =>
            {
                // OutputMsg("Thread4: End");
            });

            powerPool.QueueWorkItem(() => 
            {
                for (int i = 0; i < 20; ++i)
                {
                    OutputMsg("Thread1: " + i.ToString());
                    Thread.Sleep(1000);
                }
                OutputMsg("Thread1: END");
                return true;
            }, (res) => 
            {
                // OutputMsg("Thread1: End");
            });

            powerPool.QueueWorkItem(() =>
            {
                for (int i = 0; i < 20; ++i)
                {
                    OutputMsg("Thread2: " + i.ToString());
                    Thread.Sleep(700);
                }
                OutputMsg("Thread2: END");
            }, (res) =>
            {
                // OutputMsg("Thread2: End");
            });

            powerPool.QueueWorkItem(() =>
            {
                for (int i = 0; i < 20; ++i)
                {
                    OutputMsg("Thread3: " + i.ToString());
                    Thread.Sleep(500);
                }
                OutputMsg("Thread3: END");
                return new ThreadPoolOption();
            }, (res) =>
            {
                // OutputMsg("Thread3: End");
            });

            powerPool.QueueWorkItem(() =>
            {
                for (int i = 0; i < 20; ++i)
                {
                    OutputMsg("Thread4: " + i.ToString());
                    Thread.Sleep(500);
                }
                OutputMsg("Thread4: END");
            }, (res) =>
            {
                // OutputMsg("Thread4: End");
            });
        }

        private async void wait_Click(object sender, RoutedEventArgs e)
        {
            await powerPool.WaitAsync();
            OutputMsg("ALL Thread End");
        }
    }
}
