```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class ProcessMemory
{
    private Process process;

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);
    
    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint dwSize, out int lpNumberOfBytesRead);
    
    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint dwSize, out int lpNumberOfBytesWritten);
    
    private const uint PROCESS_VM_READ = 0x0010;
    private const uint PROCESS_VM_WRITE = 0x0020;
    private const uint PROCESS_VM_OPERATION = 0x0008;

    public bool AttachToProcess(string processName)
    {
        process = Process.GetProcessesByName(processName)[0];
        if (process == null) return false;
        IntPtr hProcess = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, process.Id);
        return hProcess != IntPtr.Zero;
    }

    public bool IsGameRunning(string processName)
    {
        return Process.GetProcessesByName(processName).Length > 0;
    }

    public float ReadFloat(IntPtr address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(process.Handle, address, buffer, (uint)buffer.Length, out _);
        return BitConverter.ToSingle(buffer, 0);
    }

    public void WriteFloat(IntPtr address, float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(process.Handle, address, buffer, (uint)buffer.Length, out _);
    }

    public int ReadInt(IntPtr address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(process.Handle, address, buffer, (uint)buffer.Length, out _);
        return BitConverter.ToInt32(buffer, 0);
    }

    public void WriteInt(IntPtr address, int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(process.Handle, address, buffer, (uint)buffer.Length, out _);
    }

    public static class Addresses
    {
        public static readonly IntPtr HealthAddress = new IntPtr(0x12345678); // Example address
        public static readonly IntPtr AmmoAddress = new IntPtr(0x87654321);   // Example address
    }
}
```