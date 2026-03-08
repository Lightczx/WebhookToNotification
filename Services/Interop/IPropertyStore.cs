using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WebhookToNotification.Services.Interop;

[ComImport]
[Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IPropertyStore
{
    void GetCount(out uint count);

    void GetAt(uint iProp, out PropertyKey pkey);

    void GetValue(ref PropertyKey key, out PropVariant pv);

    void SetValue(ref PropertyKey key, ref PropVariant pv);

    void Commit();
}