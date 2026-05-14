```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class ProcessMemory
{
    private const int PROCESS_VM_READ = 0x0010;
    private const int PROCESS_VM_WRITE = 0x0020;
    private const int PROCESS_VM_OPERATION = 0x0008;

    private static Process gameProcess;
    private static IntPtr processHandle;

    // Static addresses for Left 4 Dead 2-specific values (example addresses)
    private const int PLAYER_HEALTH_ADDRESS = 0x01234567;
    private const int PLAYER_AMMO_ADDRESS = 0x01234568;
    
    public static bool AttachToProcess(string processName)
    {
        try
        {
            gameProcess = Process.GetProcessesByName(processName)[0];
            processHandle = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, gameProcess.Id);
            return processHandle != IntPtr.Zero;
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Game process not found.");
            return false;
        }
    }

    public static bool IsGameRunning(string processName)
    {
        return Process.GetProcessesByName(processName).Length > 0;
    }

    public static float ReadFloat(int address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out int bytesRead);
        return BitConverter.ToSingle(buffer, 0);
    }

    public static void WriteFloat(int address, float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out int bytesWritten);
    }

    public static int ReadInt(int address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out _);
        return BitConverter.ToInt32(buffer, 0);
    }

    public static void WriteInt(int address, int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out int bytesWritten);
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);
}
```