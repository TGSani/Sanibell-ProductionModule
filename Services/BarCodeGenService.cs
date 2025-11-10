using Sanibell_ProductionModule.Models;
using NetBarcode;

namespace Sanibell_ProductionModule.Services;

public class BarCodeGenService : IBarCodeGenService
{
    public string GenerateEan13Base64(long eanCode)
    {
        var barcode = new Barcode(eanCode.ToString(), NetBarcode.Type.EAN13, showLabel: true);
        return barcode.GetBase64Image();
    }
}