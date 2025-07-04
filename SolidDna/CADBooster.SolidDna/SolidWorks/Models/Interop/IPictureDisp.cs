using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CADBooster.SolidDna.Interop
{
    [ComImport]
    [Guid("7BF80981-BF32-101A-8BBB-00AA00300CAB")]
    [ComVisible(false)]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [DefaultMember("Handle")]
    [ComConversionLoss]
    internal interface IPictureDisp
    {
        [DispId(0)]
        [ComAliasName("stdole.OLE_HANDLE")]
        int Handle
        {
            [DispId(0)]
            get;
        }

        [DispId(2)]
        [ComAliasName("stdole.OLE_HANDLE")]
        int hPal
        {
            [DispId(2)]
            get;
            [DispId(2)]
            set;
        }

        [DispId(3)]
        short Type
        {
            [DispId(3)]
            get;
        }

        [DispId(4)]
        [ComAliasName("stdole.OLE_XSIZE_HIMETRIC")]
        int Width
        {
            [DispId(4)]
            get;
        }

        [DispId(5)]
        [ComAliasName("stdole.OLE_YSIZE_HIMETRIC")]
        int Height
        {
            [DispId(5)]
            get;
        }

        [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
        [DispId(6)]
        void Render(int hdc, int x, int y, int cx, int cy, [ComAliasName("stdole.OLE_XPOS_HIMETRIC")] int xSrc, [ComAliasName("stdole.OLE_YPOS_HIMETRIC")] int ySrc, [ComAliasName("stdole.OLE_XSIZE_HIMETRIC")] int cxSrc, [ComAliasName("stdole.OLE_YSIZE_HIMETRIC")] int cySrc, IntPtr prcWBounds);
    }
}
