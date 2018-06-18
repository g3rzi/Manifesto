using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Manifesto
{
    class ResourceManager
    {
        public static byte[] GetResourceFromExecutable(string lpFileName, uint lpName, uint lpType)
        {
            IntPtr hModule = NativeAPI.LoadLibraryEx(lpFileName, IntPtr.Zero, NativeAPI.LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE);
            if (hModule != IntPtr.Zero)
            {
                IntPtr hResource = NativeAPI.FindResource(hModule, lpName, lpType);
                if (hResource != IntPtr.Zero)
                {
                    uint resSize = NativeAPI.SizeofResource(hModule, hResource);
                    IntPtr resData = NativeAPI.LoadResource(hModule, hResource);
                    if (resData != IntPtr.Zero)
                    {
                        byte[] uiBytes = new byte[resSize];
                        IntPtr ipMemorySource = NativeAPI.LockResource(resData);
                        Marshal.Copy(ipMemorySource, uiBytes, 0, (int)resSize);
                        return uiBytes;
                    }
                }
            }
            return null;
        }
    }
}
