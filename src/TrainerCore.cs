```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class ProcessMemory
{
    private IntPtr processHandle;
    private Process process;
    
    public bool AttachToProcess(string processName)
    {
        process = Process.GetProcessesByName(processName)[0];
        if (process == null) return false;

        processHandle = OpenProcess(ProcessAccessFlags.All, false, process.Id);
        return processHandle != IntPtr.Zero;
    }

    public bool IsGameRunning(string processName)
    {
        return Process.GetProcessesByName(processName).Length > 0;
    }

    public float ReadFloat(IntPtr address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(processHandle, address, buffer, buffer.Length, out _);
        return BitConverter.ToSingle(buffer, 0);
    }

    public void WriteFloat(IntPtr address, float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, address, buffer, buffer.Length, out _);
    }

    public int ReadInt(IntPtr address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(processHandle, address, buffer, buffer.Length, out _);
        return BitConverter.ToInt32(buffer, 0);
    }

    public void WriteInt(IntPtr address, int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, address, buffer, buffer.Length, out _);
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);
    
    [Flags]
    private enum ProcessAccessFlags : uint
    {
        All = 0x001F0FFF,
        QueryInformation = 0x0400,
        VirtualMemoryRead = 0x0010,
        VirtualMemoryWrite = 0x0020,
    }

    public static class Addresses
    {
        public static readonly IntPtr PlayerHealth = new IntPtr(0x01234567); // Example address
        public static readonly IntPtr GameSpeed = new IntPtr(0x89ABCDEF); // Example address
    }
}
```