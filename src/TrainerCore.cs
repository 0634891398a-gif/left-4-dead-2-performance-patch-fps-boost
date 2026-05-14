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
        try
        {
            process = Process.GetProcessesByName(processName)[0];
            processHandle = OpenProcess(ProcessAccessFlags.VirtualMemoryRead | ProcessAccessFlags.VirtualMemoryWrite, false, process.Id);
            return processHandle != IntPtr.Zero;
        }
        catch
        {
            return false;
        }
    }

    public bool IsGameRunning(string processName)
    {
        return Process.GetProcessesByName(processName).Length > 0;
    }

    public float ReadFloat(IntPtr address)
    {
        float value = 0f;
        ReadProcessMemory(processHandle, address, out value, Marshal.SizeOf(typeof(float)), out _);
        return value;
    }

    public void WriteFloat(IntPtr address, float value)
    {
        WriteProcessMemory(processHandle, address, ref value, Marshal.SizeOf(typeof(float)), out _);
    }

    public int ReadInt(IntPtr address)
    {
        int value = 0;
        ReadProcessMemory(processHandle, address, out value, Marshal.SizeOf(typeof(int)), out _);
        return value;
    }

    public void WriteInt(IntPtr address, int value)
    {
        WriteProcessMemory(processHandle, address, ref value, Marshal.SizeOf(typeof(int)), out _);
    }

    #region PInvoke

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, out float lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, out int lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref float lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref int lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);

    #endregion

    [Flags]
    private enum ProcessAccessFlags : uint
    {
        VirtualMemoryRead = 0x0010,
        VirtualMemoryWrite = 0x0020
    }
}

// Example static addresses for Left 4 Dead 2
public static class GameAddresses
{
    public static readonly IntPtr Health