```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Left4Dead2Trainer
{
    public class ProcessMemory
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint dwSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        private const uint PROCESS_VM_READ = 0x0010;
        private const uint PROCESS_VM_WRITE = 0x0020;
        private const uint PROCESS_VM_OPERATION = 0x0008;

        private Process _process;
        private IntPtr _processHandle;

        public bool AttachToProcess(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0)
                return false;

            _process = processes[0];
            _processHandle = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, _process.Id);
            return _processHandle != IntPtr.Zero;
        }

        public void Detach()
        {
            if (_processHandle != IntPtr.Zero)
                CloseHandle(_processHandle);
        }

        public float ReadFloat(IntPtr address)
        {
            byte[] buffer = new byte[4];
            ReadProcessMemory(_processHandle, address, buffer, (uint)buffer.Length, out _);
            return BitConverter.ToSingle(buffer, 0);
        }

        public void WriteFloat(IntPtr address, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(_processHandle, address, buffer, (uint)buffer.Length, out _);
        }

        public int ReadInt(IntPtr address)
        {
            byte[] buffer = new byte[4];
            ReadProcessMemory(_processHandle, address, buffer, (uint)buffer.Length, out _);
            return BitConverter.ToInt32(buffer, 0);
        }

        public void WriteInt(IntPtr address, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(_processHandle, address, buffer, (uint)buffer.Length, out _);
        }

        public bool IsGameRunning(string processName)
        {
            return Process.GetProcessesByName(processName).Length > 0;
        }

        // Static addresses specific to Left 4 Dead 2
        public static IntPtr PlayerHealthAddress =