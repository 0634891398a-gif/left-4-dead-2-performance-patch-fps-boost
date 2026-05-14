```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class ProcessMemory
{
    private const string ProcessName = "left4dead2";
    private static Process process;

    // Static addresses for Left 4 Dead 2 specific values (example addresses)
    private static readonly IntPtr PlayerHealthAddress = new IntPtr(0x00C40FBC); // example address
    private static readonly IntPtr PlayerAmmoAddress = new IntPtr(0x00C40F8C); // example address

    public static bool AttachToProcess()
    {
        var processes = Process.GetProcessesByName(ProcessName);
        if (processes.Length > 0)
        {
            process = processes[0];
            return true;
        }
        return false;
    }

    public static bool IsGameRunning()
    {
        return process != null && !process.HasExited;
    }

    public static float ReadFloat(IntPtr address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(process.Handle, address, buffer, buffer.Length, out _);
        return BitConverter.ToSingle(buffer, 0);
    }

    public static void WriteFloat(IntPtr address, float value)
    {
        var bytes = BitConverter.GetBytes(value);
        WriteProcessMemory(process.Handle, address, bytes, bytes.Length, out _);
    }

    public static int ReadInt(IntPtr address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(process.Handle, address, buffer, buffer.Length, out _);
        return BitConverter.ToInt32(buffer, 0);
    }

    public static void WriteInt(IntPtr address, int value)
    {
        var bytes = BitConverter.GetBytes(value);
        WriteProcessMemory(process.Handle, address, bytes, bytes.Length, out _);
    }

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);
}
```