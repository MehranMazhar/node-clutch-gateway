using System.Collections.ObjectModel;

namespace NodeClutchGateway.Shared.Authorization;

public static class FSHRoles
{
    public const string Admin = nameof(Admin);
    public const string Basic = nameof(Basic);
    public const string Passenger = nameof(Passenger);
    public const string Driver = nameof(Driver);

    public static IReadOnlyList<string> DefaultRoles { get; } = new ReadOnlyCollection<string>(new[]
    {
        Admin,
        Basic,
        Passenger,
        Driver,
    });

    public static bool IsDefault(string roleName) => DefaultRoles.Any(r => r == roleName);
}