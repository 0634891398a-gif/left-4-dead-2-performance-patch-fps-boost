```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Left4Dead2Trainer
{
    public class ProcessMemory
    {
        private Process processHandle;
        private IntPtr processBaseAddress;

        [DllImport("kernel32.dll")]
        private static extern int OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(int hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

        const uint PROCESS_VM_READ = 0x0010;
        const uint PROCESS_VM_WRITE = 0x0020;
        const uint PROCESS_VM_OPERATION = 0x0008;

        private static readonly int playerHealthAddress = 0x12345678; // Example static address
        private static readonly int ammoAddress = 0x87654321; // Example static address

        public bool AttachToProcess(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0)
            {
                return false;
            }

            processHandle = processes[0];
            processBaseAddress = processHandle.MainModule.BaseAddress;
            return true;
        }

        public bool IsGameRunning(string processName)
        {
            return Process.GetProcessesByName(processName).Length > 0;
        }

        public float ReadFloat(IntPtr address)
        {
            byte[] buffer = new byte[4];
            ReadProcessMemory(processHandle.Handle.ToInt32(), address, buffer, buffer.Length, out _);
            return BitConverter.ToSingle(buffer, 0);
        }

        public void WriteFloat(IntPtr address, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(processHandle.Handle.ToInt32(), address, buffer, buffer.Length, out _);
        }

        public int ReadInt(IntPtr address)
        {
            byte[] buffer = new byte[4];
            ReadProcessMemory(processHandle.Handle.ToInt32(), address, buffer, buffer.Length, out _);
            return BitConverter.ToInt32(buffer, 0);
        }

        public void WriteInt(IntPtr address, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(processHandle.Handle.ToInt32(), address, buffer, buffer.Length, out _);
        }

        public float GetPlayerHealth()
        {
            return ReadFloat(processBaseAddress + playerHealthAddress);
        }

        public void SetPlayerHealth(float health)
        {
            WriteFloat(processBaseAddress + playerHealthAddress, health);
        }