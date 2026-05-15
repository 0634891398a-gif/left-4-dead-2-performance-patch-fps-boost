```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class ProcessMemory
{
    private Process process;
    private IntPtr processHandle;

    // Example static addresses (you'll need to find the actual ones)
    private const int BASE_ADDRESS = 0x12345678; // Base address for the game's memory
    private const int PLAYER_HEALTH_OFFSET = 0x0000ABCD; // Example offset for player's health

    public bool AttachToProcess(string processName)
    {
        try
        {
            process = Process.GetProcessesByName(processName)[0];
            processHandle = OpenProcess(ProcessAccessFlags.All, false, process.Id);
            return true;
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

    public float ReadFloat(int offset)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(processHandle, BASE_ADDRESS + offset, buffer, buffer.Length, out _);
        return BitConverter.ToSingle(buffer, 0);
    }

    public void WriteFloat(int offset, float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, BASE_ADDRESS + offset, buffer, buffer.Length, out _);
    }

    public int ReadInt(int offset)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(processHandle, BASE_ADDRESS + offset, buffer, buffer.Length, out _);
        return BitConverter.ToInt32(buffer, 0);
    }

    public void WriteInt(int offset, int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, BASE_ADDRESS + offset, buffer, buffer.Length, out _);
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
        All = 0x1F0FFF
    }
}
```
This C# class `ProcessMemory` contains necessary methods for reading and writing memory in a process, specifically tuned for a hypothetical Left 4 Dead 2 patch. The static addresses for the game's memory should be replaced with actual values. The code uses PInvoke to access Windows API