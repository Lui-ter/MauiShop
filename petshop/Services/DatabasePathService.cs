using Microsoft.Maui.Storage;

namespace petshop.Services;

public static class DatabasePath
{
    public static string GetPath() =>
        Path.Combine(FileSystem.AppDataDirectory, "ShopPet.db3");
}

public sealed class DatabasePathService : IDatabasePathService
{
    public string GetDatabasePath() => DatabasePath.GetPath();
}
