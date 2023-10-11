using System.Text;
using AutoMapper;
using Identity.Application.Services.Interfaces;
using Identity.Domain;
using Identity.Domain.Entities;
using Konscious.Security.Cryptography;

namespace Identity.Infrastructure.Services;

internal class PasswordManager : IPasswordManager
{
    private readonly DefaultHashSettings _defaultHashSettings;
    private readonly IMapper _mapper;
    private readonly IRandomGeneratorService _randomGeneratorService;

    public PasswordManager(
        DefaultHashSettings defaultDefaultHashSettings,
        IRandomGeneratorService randomGeneratorService,
        IMapper mapper)
    {
        _defaultHashSettings = defaultDefaultHashSettings;
        _randomGeneratorService = randomGeneratorService;
        _mapper = mapper;
    }

    public async Task<PasswordEntity> CreatePasswordAsync(
        string password,
        CancellationToken cancellationToken = default)
    {
        var saltBytes = _randomGeneratorService.GenerateBytes(_defaultHashSettings.SaltLength);

        using var argon = GetArgon(password, saltBytes, _defaultHashSettings);
        var hash = await GetHashAsync(argon, _defaultHashSettings.HashLength, cancellationToken);

        var argonSettings = _mapper.Map<HashSettingsEntity>(_defaultHashSettings);
        var pass = new PasswordEntity
        {
            HashPassword = hash,
            Salt = _randomGeneratorService.ConvertToString(saltBytes),
            HashSettings = argonSettings,
        };

        return pass;
    }

    public async Task<bool> IsEqualsAsync(
        PasswordEntity passwordEntityHash,
        string password,
        CancellationToken cancellationToken = default)
    {
        var argonSetting = passwordEntityHash.HashSettings;
        using var argon = GetArgon(password, _randomGeneratorService.ConvertToBytes(passwordEntityHash.Salt), argonSetting);
        var pass = await GetHashAsync(argon, argonSetting.HashLength, cancellationToken);

        return passwordEntityHash.HashPassword == pass;
    }

    private async Task<string> GetHashAsync(
        Argon2 argon,
        int hashLength,
        CancellationToken cancellationToken)
    {
        var task = argon.GetBytesAsync(hashLength);
        var hashBytes = await Task.Run(() => task, cancellationToken);

        return _randomGeneratorService.ConvertToString(hashBytes);
    }

    private static Argon2 GetArgon(
        string password,
        byte[] salt,
        HashSettings settings)
    {
        var passwordByte = Encoding.ASCII.GetBytes(password);

        var argon = new Argon2id(passwordByte)
        {
            Salt = salt,
            Iterations = settings.Iterations,
            DegreeOfParallelism = settings.DegreeOfParallelism,
            MemorySize = settings.MemorySize,
        };

        return argon;
    }
}