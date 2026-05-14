```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class ProcessMemory
{
    private Process gameProcess;
    private IntPtr processHandle;

    // Static addresses for Left 4 Dead 2
    private static readonly IntPtr healthAddress = (IntPtr)0x01234567; // Example address
    private static readonly IntPtr ammoAddress = (IntPtr)0x01234568;   // Example address

    public bool AttachToProcess(string processName)
    {
        foreach (var process in Process.GetProcessesByName(processName))
        {
            gameProcess = process;
            processHandle = OpenProcess(ProcessAccessFlags.All, false, process.Id);
            return processHandle != IntPtr.Zero;
        }

        return false;
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
    public enum ProcessAccessFlags : uint
    {
        All = 0x1F0FFF,
        Read = 0x0010,
        Write = 0x0020,
        VMOperation = 0x0008,
        VMRead = 0x0010,
        VMWrite = 0x0020,
        Duplicating = 0x0040
    }
}
```