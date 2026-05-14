```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class TrainerCore
{
    private const string ProcessName = "left4dead2";
    private static Process gameProcess;
    private static IntPtr processHandle;

    private static readonly IntPtr HealthAddress = (IntPtr)0x01234567; // Example address
    private static readonly IntPtr StaminaAddress = (IntPtr)0x01234568; // Example address

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint size, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint size, out int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr handle);

    private const uint PROCESS_VM_READ = 0x0010;
    private const uint PROCESS_VM_WRITE = 0x0020;
    private const uint PROCESS_VM_OPERATION = 0x0008;

    public static bool AttachToProcess()
    {
        gameProcess = Process.GetProcessesByName(ProcessName)[0];
        if (gameProcess != null)
        {
            processHandle = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, gameProcess.Id);
            return processHandle != IntPtr.Zero;
        }
        return false;
    }

    public static bool IsGameRunning()
    {
        return Process.GetProcessesByName(ProcessName).Length > 0;
    }

    public static float ReadFloat(IntPtr address)
    {
        byte[] buffer = new byte[4];
        int bytesRead;
        ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out bytesRead);
        return BitConverter.ToSingle(buffer, 0);
    }

    public static void WriteFloat(IntPtr address, float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        int bytesWritten;
        WriteProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out bytesWritten);
    }

    public static int ReadInt(IntPtr address)
    {
        byte[] buffer = new byte[4];
        int bytesRead;
        ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out bytesRead);
        return BitConverter.ToInt32(buffer, 0);
    }

    public static void WriteInt(IntPtr address, int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        int bytesWritten;
        WriteProcessMemory(processHandle, address, buffer