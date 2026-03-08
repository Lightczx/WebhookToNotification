using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WebhookToNotification.Services.Interop;

[GeneratedComInterface]
[Guid("0000010B-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IPersistFile
{
    void GetClassID(out Guid pClassID);

    void IsDirty();

    void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, uint dwMode);

    void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.VariantBool)] bool fRemember);

    void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

    void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
}
