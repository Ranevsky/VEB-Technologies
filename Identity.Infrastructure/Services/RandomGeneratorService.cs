using System.Security.Cryptography;
using Identity.Application.Services.Interfaces;

namespace Identity.Infrastructure.Services;

internal class RandomGeneratorService : IRandomGeneratorService
{
    public string Generate(int count)
    {
        var bytes = GenerateBytes(count);
        var random = ConvertToString(bytes);

        return random;
    }

    public byte[] GenerateBytes(int count)
    {
        var bytes = RandomNumberGenerator.GetBytes(count);

        return bytes;
    }

    public string ConvertToString(byte[] bytes)
    {
        return Convert.ToBase64String(bytes);
    }

    public byte[] ConvertToBytes(string text)
    {
        return Convert.FromBase64String(text);
    }
}