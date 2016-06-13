using System;
using System.Runtime.InteropServices;

namespace Rain.Win32
{
    internal static class NativeMethods
    {
        #region ProcessAccessFlags
        [Flags]
        internal enum ProcessAccessFlags
        {
            None = 0x0,
            // Required to create a thread.
            Terminate = 0x0001,
            CreateThread = 0x0002,
            SetSessionId = 0x0004,
            // Required to perform an operation on the address space of a process.
            VmOperation = 0x0008,
            // Required to read memory in a process using ReadProcessMemory.
            VmRead = 0x0010,
            // Required to write to memory in a process using WriteProcessMemory.
            VmWrite = 0x0020,
            // Required to duplicate a handle using DuplicateHandle.
            DuplicateHandle = 0x0040,
            // Required to create a process.
            CreateProcess = 0x0080,
            // Required to set memory limits using SetProcessWorkingSetSize.
            SetQuota = 0x0100,
            // Required to set certain information about a process, such as its priority class.
            SetInformation = 0x0200,
            // Required to retrieve certain information about a process, such as its token, exit code, and priority class.
            QueryInformation = 0x0400,
            // Required to suspend or resume a process.
            SuspendResume = 0x800,
            // Required to retrieve certain information about a process.
            QueryLimitedInformation = 0x1000,
            // Required to wait for the process to terminate using the wait functions.
            Synchronize = 0x100000,
            // Required to delete the object.
            Delete = 0x00010000,
            // Required to read information in the security descriptor for the object, not including the information in the SACL.
            ReadControl = 0x00020000,
            // Required to modify the DACL in the security descriptor for the object.
            WriteDac = 0x00040000,
            // Required to change the owner in the security descriptor for the object.
            WriteOwner = 0x00080000,
            StandardRightRequired = 0x000F0000,
            // All possible access rights for a process object.
            AllAccess = StandardRightRequired | Synchronize | 0xFFFF
        }
        #endregion

        #region ProcessCreationFlags
        [Flags]
        internal enum ProcessCreationFlags : uint
        {
            None = 0x0,
            DebugProcess = 0x00000001,
            DebugOnlyThisProcess = 0x00000002,
            Suspended = 0x00000004,
            DetachedProcess = 0x00000008,
            NewConsole = 0x00000010,
            NewProcessGroup = 0x00000200,
            UnicodeEnvironment = 0x00000400,
            SeparateWowVdm = 0x00000800,
            SharedWowVdm = 0x00001000,
            InheritParentAffinity = SharedWowVdm,
            ProtectedProcess = 0x00040000,
            ExtendedStartupInfoPresent = 0x00080000,
            BreakawayFromJob = 0x01000000,
            PreserveCodeAuthZlevel = 0x02000000,
            DefaultErrorMode = 0x04000000,
            NoWindow = 0x08000000
        }
        #endregion

        #region ProcessInformation
        [StructLayout(LayoutKind.Sequential)]
        internal struct ProcessInformation
        {
            public IntPtr ProcessHandle { get; set; }

            public IntPtr ThreadHandle { get; set; }

            public int ProcessId { get; set; }

            public int ThreadId { get; set; }
        }
        #endregion

        #region StartupInformation
        [StructLayout(LayoutKind.Sequential)]
        internal struct StartupInfo
        {
            int size;
            string reserved;
            string desktop;
            string title;
            int x;
            int y;
            int width;
            int height;
            int xCountChars;
            int yCountChars;
            int fillAttribute;
            int flags;
            short showWindow;
            short reserved2;
            IntPtr reserved3;
            IntPtr stdInputHandle;
            IntPtr stdOutputHandle;
            IntPtr stdErrorHandle;

            public int Size
            {
                get { return size; }
                set { size = value; }
            }

            public string Reserved { get { return reserved; } }

            public string Desktop
            {
                get { return desktop; }
                set { desktop = value; }
            }

            public string Title
            {
                get { return title; }
                set { title = value; }
            }

            public int X
            {
                get { return x; }
                set { x = value; }
            }

            public int Y
            {
                get { return y; }
                set { y = value; }
            }

            public int Width
            {
                get { return width; }
                set { width = value; }
            }

            public int Height
            {
                get { return height; }
                set { height = value; }
            }

            public int XCountChars
            {
                get { return xCountChars; }
                set { xCountChars = value; }
            }

            public int YCountChars
            {
                get { return yCountChars; }
                set { yCountChars = value; }
            }

            public int FillAttribute
            {
                get { return fillAttribute; }
                set { fillAttribute = value; }
            }

            public int Flags
            {
                get { return flags; }
                set { flags = value; }
            }

            public short ShowWindow
            {
                get { return showWindow; }
                set { showWindow = value; }
            }

            public short Reserved2 { get { return reserved2; } }

            public IntPtr Reserved3 { get { return reserved3; } }

            public IntPtr StandardInput
            {
                get { return stdInputHandle; }
                set { stdInputHandle = value; }
            }

            public IntPtr StandardOutput
            {
                get { return stdOutputHandle; }
                set { stdOutputHandle = value; }
            }

            public IntPtr StandardError
            {
                get { return stdErrorHandle; }
                set { stdErrorHandle = value; }
            }
        }
        #endregion

        #region Kernel32
        [DllImport("kernel32", EntryPoint = "CreateProcess")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CreateProcess(string applicationName,
            string commandLine, IntPtr processAttributes,
            IntPtr threadAttributes, bool inheritHandles,
            ProcessCreationFlags creationFlags, IntPtr environment,
            string currentDirectory, ref StartupInfo startupInfo,
            out ProcessInformation processInfo);

        [DllImport("kernel32", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags access,
            bool inheritHandle, int processId);

        [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadProcessMemory(IntPtr hProcess,
            IntPtr lpBaseAddress, byte[] lpBuffer,
            int nSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WriteProcessMemory(IntPtr hProcess,
            IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32", EntryPoint = "ResumeThread")]
        public static extern int ResumeThread(IntPtr hThread);

        [DllImport("kernel32", EntryPoint = "CloseHandle")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", EntryPoint = "GetLastError")]
        public static extern int GetLastError();
        #endregion

        #region User32
        private const int WH_MOUSE_LL = 14;

        [DllImport("user32", EntryPoint = "SetWindowText")]
        internal static extern int SetWindowText(IntPtr hWnd, string text);
        #endregion
    }
}
