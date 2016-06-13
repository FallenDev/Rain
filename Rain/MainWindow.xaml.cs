#region License/Copyleft
//    Rain - DarkAges Hunting Companion
//    Copyright (C) 2016  FallenDev
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.
//        
//    Should a problem occur, contact me via Skype - FallenDev
//    Credit to the DA community of programmers, for most of their open source code.
#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

using MahApps.Metro.Controls;
using Rain.Win32;

namespace Rain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void client_start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenClient(null, null);
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
            var clientPath = Properties.Resources.DarkAgesPath;
            try
            {
                if (!File.Exists(Rain.Properties.Resources.DarkAgesPath))
                {
                    MessageBox.Show("Check the Path in settings.");
                }
                else
                {
                    NativeMethods.StartupInfo startupInfo = new NativeMethods.StartupInfo();
                    startupInfo.Size = Marshal.SizeOf(startupInfo);
                    NativeMethods.ProcessInformation processInfo;
                    NativeMethods.CreateProcess(Properties.Resources.DarkAgesPath, (string)null, IntPtr.Zero, IntPtr.Zero, false, NativeMethods.ProcessCreationFlags.Suspended, IntPtr.Zero, (string)null, ref startupInfo, out processInfo);
                    using (ProcessMemoryStream processMemoryStream = new ProcessMemoryStream(processInfo.ProcessId, NativeMethods.ProcessAccessFlags.VmOperation | NativeMethods.ProcessAccessFlags.VmRead | NativeMethods.ProcessAccessFlags.VmWrite))
                    {
                        processMemoryStream.Position = 4384293L;
                        processMemoryStream.WriteByte((byte)144);
                        processMemoryStream.WriteByte((byte)144);
                        processMemoryStream.WriteByte((byte)144);
                        processMemoryStream.WriteByte((byte)144);
                        processMemoryStream.WriteByte((byte)144);
                        processMemoryStream.WriteByte((byte)144);
                        processMemoryStream.Position = 4404130L;
                        processMemoryStream.WriteByte((byte)235);
                        processMemoryStream.Position = 4404162L;
                        processMemoryStream.WriteByte((byte)106);
                        processMemoryStream.WriteByte((byte)1);
                        processMemoryStream.WriteByte((byte)106);
                        processMemoryStream.WriteByte((byte)0);
                        processMemoryStream.WriteByte((byte)106);
                        processMemoryStream.WriteByte((byte)0);
                        processMemoryStream.WriteByte((byte)106);
                        processMemoryStream.WriteByte((byte)127);
                        processMemoryStream.Position = 5744601L;
                        processMemoryStream.WriteByte((byte)235);
                        processMemoryStream.Position = 7290020L;
                        processMemoryStream.WriteString("Rain");
                        NativeMethods.ResumeThread(processInfo.ThreadHandle);
                    }
                    Process processById = Process.GetProcessById(processInfo.ProcessId);
                    do
                        ;
                    while (processById.MainWindowHandle == IntPtr.Zero);
                    NativeMethods.SetWindowText(processById.MainWindowHandle, "Dark Ages : Rain");
                    App.ProcessIds.Add(processInfo.ProcessId);
                }

            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Error occurred, check your path; Or permissions.");
            }
        }
    }
}