using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Threading;
using System.ComponentModel;
using System.Net;
namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        WebClient wc = new WebClient();
        WebClient wc1 = new WebClient();
        WebClient wc2 = new WebClient();   
        string text = null;
        Thread mythread;
        public delegate void InvokeDelegate();
        public  MainWindow()
        {
            InitializeComponent();          
        } 
        private void Button_Click(object sender, RoutedEventArgs e)
        {           
            mythread = new Thread(first);
            mythread.Start();                    
        }
        void first()
        {
            Dispatcher.Invoke(() => Progressbar.Maximum += 100);
            Image1.Dispatcher.BeginInvoke(new InvokeDelegate(InvokeMethod));
        }       
        public async void InvokeMethod()
        {
            Uri uri = null;
            Dispatcher.Invoke(() => text = url1.Text);
            try
            {
                uri = new Uri(text);
            }
            catch
            {
                MessageBox.Show("неверный формат url","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                Progressbar.Maximum -= 100;
            }
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloader_DownloadProgressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallback);
            try
            {
                await wc.DownloadFileTaskAsync(uri, @"img/image1.png");
                Image1.Source = new BitmapImage(uri);
            }
            catch{}  
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            wc.CancelAsync();         
            if (mythread != null)
            {
                mythread.Abort();
            }
            
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mythread = new Thread(second);
            mythread.Start();
        }
        void second()
        {
            Dispatcher.Invoke(() => Progressbar.Maximum += 100);
            Image2.Dispatcher.BeginInvoke(new InvokeDelegate(InvokeMethod2));
        }

        public async void InvokeMethod2()
        {
            Uri uri = null;
            Dispatcher.Invoke(() => text = url2.Text);
            try
            {
                uri = new Uri(text);
            }
            catch
            {
                MessageBox.Show("неверный формат url", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Progressbar.Maximum -= 100;
            }
            wc1.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloader_DownloadProgressChanged);
            wc1.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallback);
            try
            {
                await wc1.DownloadFileTaskAsync(uri, @"img/image2.png");
                image3.Source = new BitmapImage(uri);
            }
            catch { }
        }
            private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            wc1.CancelAsync();
            if (mythread != null)
            {
                mythread.Abort();
            }   
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {       
            mythread = new Thread(third);
            mythread.Start();
        }
        void third()
        {
            Dispatcher.Invoke(() => Progressbar.Maximum += 100);
            Image2.Dispatcher.BeginInvoke(new InvokeDelegate(InvokeMethod3));
        }
        public async void InvokeMethod3()
        {
            Uri uri = null;
            Dispatcher.Invoke(() => text = url3.Text);
            try
            {
                uri = new Uri(text);
            }
            catch
            {
                MessageBox.Show("неверный формат url", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Progressbar.Maximum -= 100;
            }
            wc2.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloader_DownloadProgressChanged);
            wc2.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallback);
            try
            {
                await wc2.DownloadFileTaskAsync(uri, @"img/image3.png");
                Image2.Source = new BitmapImage(uri);
            }
            catch{}
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            wc2.CancelAsync();
            if (mythread != null)
            {
                mythread.Abort();
            }
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            first();
            second();
            third();
        }
        private void DownloadFileCallback(object sender, AsyncCompletedEventArgs e)
        {           
            Progressbar.Maximum -= 100;
        }
        void downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Progressbar.Value = e.ProgressPercentage;
        }
    }
}
