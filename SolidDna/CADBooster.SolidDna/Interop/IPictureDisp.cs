using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Created a separate namespace for interop types because we only need them for very specific interop calls.
namespace CADBooster.SolidDna.Interop;

/// <summary>
/// The picture disp interface from the stdole library.
/// Used for getting preview images from SolidWorks using <see cref="SolidWorksApplication.GetPreviewBitmap"/> or <see cref="Model.GetPreviewBitmap"/>
/// </summary>
[ComImport]
[Guid("7BF80981-BF32-101A-8BBB-00AA00300CAB")]
[ComVisible(false)]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[DefaultMember("Handle")]
[ComConversionLoss]
internal interface IPictureDisp
{
    /// <summary>
    /// The Windows GDI handle of the picture
    /// </summary>
    [DispId(0)]
    [ComAliasName("stdole.OLE_HANDLE")]
    int Handle
    {
        [DispId(0)]
        get;
    }

    /// <summary>
    /// The Windows handle of the palette used by the picture.
    /// </summary>
    [DispId(2)]
    [ComAliasName("stdole.OLE_HANDLE")]
    int hPal
    {
        [DispId(2)]
        get;
        [DispId(2)]
        set;
    }

    /// <summary>
    /// The type of picture (see PICTYPE).
    /// </summary>
    [DispId(3)]
    short Type
    {
        [DispId(3)]
        get;
    }

    /// <summary>
    /// The width of the picture.
    /// </summary>
    [DispId(4)]
    [ComAliasName("stdole.OLE_XSIZE_HIMETRIC")]
    int Width
    {
        [DispId(4)]
        get;
    }

    /// <summary>
    /// The height of the picture.
    /// </summary>
    [DispId(5)]
    [ComAliasName("stdole.OLE_YSIZE_HIMETRIC")]
    int Height
    {
        [DispId(5)]
        get;
    }

    /// <summary>
    /// Draw the picture into a specified device context.
    /// </summary>
    /// <param name="hdc"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="cx"></param>
    /// <param name="cy"></param>
    /// <param name="xSrc"></param>
    /// <param name="ySrc"></param>
    /// <param name="cxSrc"></param>
    /// <param name="cySrc"></param>
    /// <param name="prcWBounds"></param>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
    [DispId(6)]
    void Render(int hdc, int x, int y, int cx, int cy,
        [ComAliasName("stdole.OLE_XPOS_HIMETRIC")] int xSrc, [ComAliasName("stdole.OLE_YPOS_HIMETRIC")] int ySrc,
        [ComAliasName("stdole.OLE_XSIZE_HIMETRIC")] int cxSrc, [ComAliasName("stdole.OLE_YSIZE_HIMETRIC")] int cySrc, IntPtr prcWBounds);
}