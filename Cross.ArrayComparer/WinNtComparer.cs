using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross.ArrayComparer
{
    using System.Runtime.InteropServices;
    using System.Security;

    public class WinNtComparer
    {
        private static Lazy<bool> _IsSupported = new Lazy<bool>(() =>
        {
            // this is optional check, but it fast
            // by the way, we can run mono on Windows with patched Environment.OSVersion.Platform
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                return false;

            // We are check full trust
            // This check is enough
            return PInvokeProbe.Check();
        });

        public static bool IsSupported
        {
            get { return _IsSupported.Value; }
        }

        unsafe public static bool Equals(byte[] x, byte[] y)
        {
            if (!IsSupported) throw new NotSupportedException();
            fixed (void* px = x, py = y)
            {
                return Equals(px, py, x.Length);
            }
        }

        unsafe public static bool Equals(decimal[] x, decimal[] y)
        {
            if (!IsSupported) throw new NotSupportedException();
            fixed (void* px = x, py = y)
            {
                return Equals(px, py, x.Length * sizeof(decimal));
            }
        }

        private static unsafe bool Equals(void* x, void* y, int length)
        {
            if (length == 0) return true;
            var ret = PInvoke.RtlCompareMemory((IntPtr) (void*) x, (IntPtr) (void*) y, length);
            return ret == length;
        }

    }

/*
http://stackoverflow.com/questions/18563080/does-the-windows-api-not-crt-have-a-memory-compare-function
https://msdn.microsoft.com/en-us/library/windows/hardware/ff561778.aspx
SIZE_T RtlCompareMemory(
  _In_ const VOID   *Source1,
  _In_ const VOID   *Source2,
  _In_       SIZE_T Length
);    
*/
    internal class PInvoke
    {
        [DllImport("NtDll.dll")]
        public static extern int RtlCompareMemory(IntPtr source1, IntPtr source2, int length);
    }

    internal class PInvokeProbe
    {
        // returns true, if RtlCompareMemory works
        public static unsafe bool Check()
        {
            byte[] b1 = new byte[1], b2 = new byte[1];
            fixed (byte* pb1 = b1, pb2 = b2)
            {
                try
                {
                    var ret = PInvoke.RtlCompareMemory((IntPtr) pb1, (IntPtr) pb2, 1);
                    return ret == 1;
                }
                catch (SecurityException)
                {
                    // Full Trust is Required. Why?
                    return false;
                }
                catch (DllNotFoundException)
                {
                    // Q: May running mono on non-Windows os with patched Environment.OSVersion.Platform? 
                    // A: may. So, Environment.OSVersion.Platform checking isn't enough
                    return false;
                }
            }

        }
    }
}
