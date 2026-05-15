```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class TrainerCore
{
    private Process gameProcess;
    
    // Static addresses for Left 4 Dead 2 (example addresses, modify as necessary)
    private const int HealthAddress = 0x01234567; // Replace with actual address
    private const int AmmoAddress = 0x01234568;   // Replace with actual address
    private const int SpeedAddress = 0x01234569;  // Replace with actual address

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

    private const uint PROCESS_ALL_ACCESS = 0x1F0FFF;

    public TrainerCore()
    {
        // Constructor can initialize variables if needed
    }

    public bool IsGameRunning()
    {
        gameProcess = Process.GetProcessesByName("Left4Dead2").FirstOrDefault();
        return gameProcess != null;
    }

    public bool AttachToProcess()
    {
        if (IsGameRunning())
        {
            IntPtr handle = OpenProcess(PROCESS_ALL_ACCESS, false, gameProcess.Id);
            return handle != IntPtr.Zero;
        }
        return false;
    }

    public float ReadFloat(int address)
    {
        byte[] buffer = new byte[4];
        int bytesRead;
        IntPtr handle = OpenProcess(PROCESS_ALL_ACCESS, false, gameProcess.Id);
        ReadProcessMemory(handle, (IntPtr)address, buffer, buffer.Length, out bytesRead);
        return BitConverter.ToSingle(buffer, 0);
    }

    public void WriteFloat(int address, float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        int bytesWritten;
        IntPtr handle = OpenProcess(PROCESS_ALL_ACCESS, false, gameProcess.Id);
        WriteProcessMemory(handle, (IntPtr)address, buffer, buffer.Length, out bytesWritten);
    }

    public int ReadInt(int address)
    {
        byte[] buffer = new byte[4];
        int bytesRead;
        IntPtr handle = OpenProcess(PROCESS_ALL_ACCESS, false, gameProcess.Id);
        ReadProcessMemory(handle, (IntPtr)address, buffer, buffer.Length, out bytesRead);
        return BitConverter.ToInt32(buffer, 0);
    }

    public void WriteInt(int address, int value)
    {
        byte[] buffer =