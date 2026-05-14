```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class ProcessMemory
{
    private Process process;
    private IntPtr processHandle;

    public void AttachToProcess(string processName)
    {
        process = Process.GetProcessesByName(processName)[0];
        processHandle = OpenProcess(ProcessAccessFlags.VirtualRead | ProcessAccessFlags.VirtualWrite, false, process.Id);
    }

    public bool IsGameRunning()
    {
        return process != null && !process.HasExited;
    }

    public float ReadFloat(long address)
    {
        byte[] buffer = new byte[sizeof(float)];
        ReadProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out int bytesRead);
        return BitConverter.ToSingle(buffer, 0);
    }

    public void WriteFloat(long address, float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out int bytesWritten);
    }

    public int ReadInt(long address)
    {
        byte[] buffer = new byte[sizeof(int)];
        ReadProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out int bytesRead);
        return BitConverter.ToInt32(buffer, 0);
    }

    public void WriteInt(long address, int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out int bytesWritten);
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

    [Flags]
    public enum ProcessAccessFlags : uint
    {
        VirtualRead = 0x0010,
        VirtualWrite = 0x0020,
        All = 0x001F0FFF
    }

    // Static addresses
    public const long PlayerHealthAddress = 0x00ABCDEF; // Example static address
    public const long AmmoCountAddress = 0x00ABCDE0; // Example static address
    public const long GameTimeAddress = 0x00ABCDE5; // Example static address
}

public class TrainerCore
{
    private ProcessMemory processMemory = new ProcessMemory();
    
    public void Activate()
    {