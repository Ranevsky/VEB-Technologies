namespace Identity.Application.Services.Interfaces;

public interface IRandomGeneratorService
{
    string Generate(int count);
    byte[] GenerateBytes(int count);
    string ConvertToString(byte[] bytes);
    byte[] ConvertToBytes(string text);
}