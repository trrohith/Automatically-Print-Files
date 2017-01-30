using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automatically_Print
{
    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        ListBox listbox;
        FileSystemWatcher watcher;
        String location;

        public Form1()
        {
            InitializeComponent();
            listbox = listBox1;
            location = AppDomain.CurrentDomain.BaseDirectory;
            watch();
        }

        public void printThis(String FileName)
        {
            ProcessStartInfo info = new ProcessStartInfo(FileName.Trim());
            info.Verb = "Print";
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(info);
        }

        private void watch()
        {
            watcher = new FileSystemWatcher();
            watcher.Path = location;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.pdf";
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.IncludeSubdirectories = false;
            watcher.EnableRaisingEvents = true;
        }

        public void OnCreated(object sender, FileSystemEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                listbox.Items.Add(e.Name);
                printThis(e.FullPath);
            });
        }
        
    }
}
