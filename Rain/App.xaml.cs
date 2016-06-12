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
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Rain
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IPEndPoint DarkAgesBaseIP = new IPEndPoint(IPAddress.Parse("52.88.55.94"), 2610);
        public static int InitSocketPort = 2710;
        public const int PORT = 2710;
        public const long ADDRESS_IP = 4407738;
        public const long ADDRESS_MI = 5830325;
        public const long ADDRESS_PORT = 4407780;
        private static MainWindow window;
        private static List<int> _processIds = new List<int>();

        internal static List<int> ProcessIds
        {
            get
            {
                return App._processIds;
            }
        }

        internal static MainWindow Window
        {
            get
            {
                if (App.window == null)
                    Application.Current.Dispatcher.Invoke((Action)(() => App.window = (MainWindow)Application.Current.MainWindow));
                return App.window;
            }
        }

        public static IPEndPoint Redirect { get; set; }

        public static Socket Socket { get; private set; }

        public static string StartupPath { get; private set; }

        public static string users { get; private set; }

//         private static void EndAccept(IAsyncResult ar)
//         {
//             lock (Program.Clients)
//             {
//                 Client temp_7 = new Client(Program.Socket.EndAccept(ar), Program.Redirect ?? Program.DarkAgesBaseIP);
//                 Program.Redirect = (IPEndPoint)null;
//                 Program.Socket.BeginAccept(new AsyncCallback(Program.EndAccept), (object)null);
//             }
//         }
    }
}
