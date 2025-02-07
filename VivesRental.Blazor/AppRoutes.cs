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

        public static class Products
        {
            private const string Base = "/product-management";

            public const string Index = $"{Base}";
            public const string Create = $"{Base}/create";
            public const string Edit = $"{Base}/edit/{{id:guid}}";
            public const string Details = $"{Base}/details/{{id:guid}}";
         

            public static string EditUrl(Guid id)
            {
                return Edit.Replace("{id:guid}", id.ToString());
            }

            public static string DetailsUrl(Guid id)
            {
                return Details.Replace("{id:guid}", id.ToString());
            }

           
        }

        public static class Orders
        {
            private const string Base = "/order-management";

            public const string Index = $"{Base}";
            public const string Create = $"{Base}/create";
            public const string Details = $"{Base}/details/{{id:guid}}";
            public const string Return = $"{Base}/return";

            public static string DetailsUrl(Guid id)
            {
                return Details.Replace("{id:guid}", id.ToString());
            }
        }

        public static class Reservations
        {
            private const string Base = "/reservation-management";

            public const string Index = $"{Base}";
            public const string Create = $"{Base}/create";
            public const string Edit = $"{Base}/edit/{{id:guid}}";

            public static string EditUrl(Guid id)
            {
                return Edit.Replace("{id:guid}", id.ToString());
            }
        }

        public static class Dashboard
        {
            public const string Index = "/dashboard";
        }
    }
}

























