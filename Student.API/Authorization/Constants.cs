namespace Student.API.Authorization
{
    public static class PolicyNames
    {
        public const string AtLeast20 = "AtLeast20";
        public const string CreatedAtleast2Restaurants = "CreatedAtleast2Restaurants";
    }

    public static class AppClaimTypes
    {
        public const string DateOfBirth = "DateOfBirth";
    }
}
