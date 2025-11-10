namespace Sanibell_ProductionModule.Services;

public interface IBarCodeGenService
{
    string GenerateEan13Base64(long eancode);
}