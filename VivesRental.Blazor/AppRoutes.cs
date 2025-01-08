namespace VivesRental.Blazor
{
    public static class AppRoutes
    {
        public static class Home
        {
            public const string Index = "/";
        }

        public static class Account
        {
            public const string SignIn = "/account/sign-in";
            public const string Register = "/account/register";
        }

        public static class Customers
        {
            private const string Base = "/customer-management";

            public const string Index = $"{Base}";
            public const string Create = $"{Base}/create";
            public const string Edit = $"{Base}/edit/{{id:guid}}";

            public static string EditUrl(Guid id)
            {
                return Edit.Replace("{id:guid}", id.ToString());
            }
        }

        public static class Articles
        {
            private const string Base = "/article-management";

            public const string Index = $"{Base}";
        }

        public static class Rentals
        {
            private const string Base = "/rental-info";

            public const string Index = $"{Base}";
        }

        public static class Orders
        {
            private const string Base = "/new-rental-order";

            public const string Index = $"{Base}";
        }

        public static class Returns
        {
            private const string Base = "/return-order";

            public const string Index = $"{Base}";
        }
    }
}




















