using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LightAnti_CheatWindows
{
    internal class NativeImport
    {
        [DllImport("kernel32.dll")]
        internal static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll")]
        internal static extern uint SuspendThread(IntPtr hThread);

        [DllImport("kernel32.dll")]
        internal static extern int ResumeThread(IntPtr hThread);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);

        [DllImport("kernel32.dll")]
        public static extern unsafe bool VirtualProtect(byte* lpAddress, int dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll")]
        public static extern bool QueryFullProcessImageName(IntPtr hprocess, int dwFlags, StringBuilder lpExeName, out int size);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccess dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, ref COPYDATASTRUCT lParam);

        #region Enums

        #endregion

        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        internal enum VirtualProtectionType : uint
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            Readonly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }

        [Flags]
        public enum ProcessAccess
        {
            CreateThread = 0x0002, //Required to Open a Process
            SetSessionId = 0x0004, //Required to get the Process ID;
            VmOperation = 0x0008, //Required to perform an operation on the address space of a process
            VmRead = 0x0010, //Required to read memory in a process using ReadProcessMemory
            VmWrite = 0x0020, //Required to write to memory in a process using WriteProcessMemory
            DupHandle = 0x0040, //Required to duplicate a handle using DuplicateHandle
            CreateProcess = 0x0080, //Required to create a process
            SetQuota = 0x0100, //Required to set memory limits using SetProcessWorkingSetSize
            SetInformation = 0x0200, //Required to set certain information about a process, such as its priority class (see SetPriorityClass)
            QueryInformation = 0x0400, //Required to retrieve certain information about a process, such as its token, exit code, and priority class (see OpenProcessToken)
            SuspendResume = 0x0800, //Required to suspend or resume a process
            QueryLimitedInformation = 0x1000, //Required to retrieve certain information about a process (see GetExitCodeProcess, GetPriorityClass, IsProcessInJob, QueryFullProcessImageName)
            Synchronize = 0x100000, //Required to wait for the process to terminate using the wait functions
            Delete = 0x00010000, //Required to delete the object
            ReadControl = 0x00020000, //Required to read information in the security descriptor for the object, not including the information in the SACL
            WriteDac = 0x00040000, //Required to modify the DACL in the security descriptor for the object
            WriteOwner = 0x00080000, //Required to change the owner in the security descriptor for the object
            StandardRightsRequired = 0x000F0000, //Required to check Standard rights
            AllAccess = StandardRightsRequired | Synchronize | 0xFFFF //All possible access rights for a process object
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public int dwData;
            public int cbData;
            public int lpData;
        }
    }
}
