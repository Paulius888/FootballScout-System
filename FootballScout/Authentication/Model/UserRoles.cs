namespace FootballScout.Authentication.Model
{
    public static class UserRoles
    {
        public const string Admin = nameof(Admin);
        public const string Scout = nameof(Scout);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, Scout };
    }
}
