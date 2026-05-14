```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TrainerCore
{
    public class ProcessMemory
    {
        private Process gameProcess;
        private IntPtr processHandle;

        // Example static addresses for the game (these should be replaced with the actual addresses)
        private const int HealthAddress = 0x01234567;  // Replace with actual address
        private const int AmmoAddress = 0x01234568;    // Replace with actual address

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll")]
        public static extern void CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

        public bool AttachToProcess(string processName)
        {
            gameProcess = Process.GetProcessesByName(processName)[0];
            if (gameProcess == null)
                return false;

            processHandle = OpenProcess(0x1F0FFF, false, gameProcess.Id);
            return processHandle != IntPtr.Zero;
        }

        public bool IsGameRunning(string processName)
        {
            return Process.GetProcessesByName(processName).Length > 0;
        }

        public float ReadFloat(int address)
        {
            byte[] buffer = new byte[4];
            ReadProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out _);
            return BitConverter.ToSingle(buffer, 0);
        }

        public void WriteFloat(int address, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out _);
        }

        public int ReadInt(int address)
        {
            byte[] buffer = new byte[4];
            ReadProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out _);
            return BitConverter.ToInt32(buffer, 0);
        }

        public void WriteInt(int address, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out _);
        }

        public void Close()
        {
            if (processHandle != IntPtr.Zero)
            {
                CloseHandle(processHandle);
                processHandle = IntPtr.Zero;
            }
        }
    }
}
```