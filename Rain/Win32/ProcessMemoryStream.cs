using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using static Rain.Win32.NativeMethods;

namespace Rain.Win32
{
    internal sealed class ProcessMemoryStream : Stream, IDisposable
    {
        private ProcessAccessFlags access;
        private bool disposed;
        private IntPtr hProcess;        
        long position = 0x400000;
        byte[] internalBuffer = new byte[256];

        public override bool CanRead
        {
            get
            {
                return (access & ProcessAccessFlags.VmRead) > ProcessAccessFlags.None;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return (access & (ProcessAccessFlags.VmOperation | ProcessAccessFlags.VmWrite)) > ProcessAccessFlags.None;
            }
        }

        public override long Length
        {
            get
            {
                throw new NotSupportedException("Length is not supported.");
            }
        }

        public override long Position
        {
            get { return position; }
            set { position = value; }
        }

        public int ProcessId { get; private set; }

        ~ProcessMemoryStream()
        {
            Dispose(false);
        }

        public override void Flush()
        {
            CheckIfDisposed();
        }

        internal ProcessMemoryStream(int processId, ProcessAccessFlags access)
        {
            this.access = access;
            ProcessId = processId;
            hProcess = OpenProcess(access, false, processId);
            if (hProcess == IntPtr.Zero)
                throw new ArgumentException("Unable to open the process.");
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            CheckIfDisposed();
            CheckBufferSize(count);

            int numberOfBytesRead = 0;
            bool success = ReadProcessMemory(hProcess, (IntPtr)position, internalBuffer, count, out numberOfBytesRead);

            if (!success || numberOfBytesRead != count)
            {
                int error = GetLastError();
                throw new Win32Exception(error, "Unable to read from process memory.");
            }

            position += numberOfBytesRead;

            Buffer.BlockCopy(internalBuffer, 0, buffer, offset, count);
            return numberOfBytesRead;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            CheckIfDisposed();

            switch (origin)
            {
                case SeekOrigin.Begin:
                    position = offset;
                    break;

                case SeekOrigin.Current:
                    position += offset;
                    break;

                case SeekOrigin.End:
                    position -= offset;
                    throw new NotSupportedException("SeekOrigin.End not supported.");
            }

            return position;
        }

        public override void SetLength(long value)
        {
            CheckIfDisposed();

            throw new NotSupportedException("Setting length for this stream is not supported.");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            CheckIfDisposed();
            CheckBufferSize(count);

            Buffer.BlockCopy(buffer, offset, internalBuffer, 0, count);

            int numberOfBytesWritten = 0;
            bool success = WriteProcessMemory(hProcess, (IntPtr)position, internalBuffer, count, out numberOfBytesWritten);

            if (!success || numberOfBytesWritten != count)
            {
                int error = GetLastError();
                throw new Win32Exception(error, "Unable to write to process memory.");
            }


            position += numberOfBytesWritten;
        }

        public override void WriteByte(byte value)
        {
            CheckIfDisposed();

            internalBuffer[0] = value;

            int numberOfBytesWritten = 0;
            bool success = WriteProcessMemory(hProcess, (IntPtr)position, internalBuffer, 1, out numberOfBytesWritten);

            if (!success || numberOfBytesWritten != 1)
            {
                int error = GetLastError();
                throw new Win32Exception(error, "Unable to write to process memory.");
            }


            position += numberOfBytesWritten;
        }

        public void WriteString(string value)
        {
            CheckIfDisposed();

            byte[] bytes = Encoding.ASCII.GetBytes(value);
            Write(bytes, 0, bytes.Length);
        }

        protected override void Dispose(bool isDisposing)
        {
            if (disposed) return;

            if (isDisposing)
            {
                
            }

            if (hProcess != IntPtr.Zero)
                CloseHandle(hProcess);

            hProcess = IntPtr.Zero;

            base.Dispose(isDisposing);
            disposed = true;
        }

        void CheckIfDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException("ProcessMemoryStream");
        }

        void CheckBufferSize(int count, bool copyContents = false)
        {
            if (internalBuffer.Length >= count) return;

            var newBuffer = new byte[count];

            if (copyContents)
                Buffer.BlockCopy(internalBuffer, 0, newBuffer, 0, internalBuffer.Length);

            internalBuffer = newBuffer;
        }

        public override void Close()
        {
            CheckIfDisposed();

            if (hProcess != IntPtr.Zero)
            {
                CloseHandle(this.hProcess);
                hProcess = IntPtr.Zero;
            }
            base.Close();
        }
    }
}
