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
