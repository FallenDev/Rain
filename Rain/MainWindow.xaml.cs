using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

using Rain.Properties;

namespace Rain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private static object DarkAgesPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        //         System.Threading.Thread openC =
        //             new System.Threading.Thread(OpenClient);

        private void client_start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //openC.Start();
                OpenClient((string)null, null);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Check the Path in settings.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenClient(object sender, EventArgs e)
        {
            client_start.IsEnabled = false;

            var clientPath = Properties.Resources.DarkAgesPath;
            var clientHash = string.Empty;
            var result = ClientLoadResult.Success;

            try
            {
                if (!File.Exists(Rain.Properties.Resources.DarkAgesPath))
                {
                    MessageBox.Show("Check the Path in settings.");
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Error occurred, check your path; Or permissions.");
            }
            finally
            {
                HandleClientLoadResult(result);
            }
        }



        //             else
        //             {
        //                 StartupInfo startupInfo = new StartupInfo();
        //                 startupInfo.Size = Marshal.SizeOf<StartupInfo>(startupInfo);
        //                 ProcessInformation processInfo;
        //                 Kernel32.CreateProcess(darkAgesPath, (string)null, IntPtr.Zero, IntPtr.Zero, false, ProcessCreationFlags.Suspended, IntPtr.Zero, (string)null, ref startupInfo, out processInfo);
        //                 using (ProcessMemoryStream processMemoryStream = new ProcessMemoryStream(processInfo.ProcessId, ProcessAccess.VmOperation | ProcessAccess.VmRead | ProcessAccess.VmWrite))
        //                 {
        //                     processMemoryStream.Position = 4384293L;
        //                     processMemoryStream.WriteByte((byte)144);
        //                     processMemoryStream.WriteByte((byte)144);
        //                     processMemoryStream.WriteByte((byte)144);
        //                     processMemoryStream.WriteByte((byte)144);
        //                     processMemoryStream.WriteByte((byte)144);
        //                     processMemoryStream.WriteByte((byte)144);
        //                     processMemoryStream.Position = 4404130L;
        //                     processMemoryStream.WriteByte((byte)235);
        //                     processMemoryStream.Position = 4404162L;
        //                     processMemoryStream.WriteByte((byte)106);
        //                     processMemoryStream.WriteByte((byte)1);
        //                     processMemoryStream.WriteByte((byte)106);
        //                     processMemoryStream.WriteByte((byte)0);
        //                     processMemoryStream.WriteByte((byte)106);
        //                     processMemoryStream.WriteByte((byte)0);
        //                     processMemoryStream.WriteByte((byte)106);
        //                     processMemoryStream.WriteByte((byte)127);
        //                     processMemoryStream.Position = 5744601L;
        //                     processMemoryStream.WriteByte((byte)235);
        //                     processMemoryStream.Position = 7290020L;
        //                     processMemoryStream.WriteString("Rain");
        //                     Kernel32.ResumeThread(processInfo.ThreadHandle);
        //                  }
        //                 Process processById = Process.GetProcessById(processInfo.ProcessId);
        //                 do
        //                     ;
        //                 while (processById.MainWindowHandle == IntPtr.Zero);
        //                 User32.SetWindowText(processById.MainWindowHandle, "Dark Ages : Rain");
        //                 App.ProcessIds.Add(processInfo.ProcessId);
        //             }
    }
}
