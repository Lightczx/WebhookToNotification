using System;
using System.Runtime.InteropServices;

namespace WebhookToNotification.Services.Interop;

[StructLayout(LayoutKind.Explicit)]
public struct PropVariant
{
    [FieldOffset(0)] public ushort vt;
    [FieldOffset(8)] public IntPtr pwszVal;

    public PropVariant(string value)
    {
        vt = 31; // VT_LPWSTR
        pwszVal = Marshal.StringToCoTaskMemUni(value);
    }
}
