namespace CinemaBookingConsoleApp
{
    static class AuthService
    {
        public const string AdminLogin = "admin";
        public const string AdminPassword = "admin";

        public static void EnsureAdminUser(InMemoryDb db)
        {
            if (FindUser(db, AdminLogin) != null)
                return;

            db.Users.Add(new UserAccount
            {
                Login = AdminLogin,
                Password = AdminPassword,
                DisplayName = "Адміністратор",
                RegisteredAt = DateTime.Now
            });
        }

        public static bool IsAdmin(UserAccount user)
        {
            return string.Equals(user.Login, AdminLogin, StringComparison.OrdinalIgnoreCase);
        }

        public static UserAccount? Login(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== ВХІД ===");

            var login = Ui.ReadNonEmpty("Логін: ");
            var password = Ui.ReadNonEmpty("Пароль: ");

            var user = FindUser(db, login);
            if (user == null || user.Password != password)
            {
                Ui.Pause("Невірний логін або пароль.");
                return null;
            }

            return user;
        }

        public static void Register(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== РЕЄСТРАЦІЯ ===");

            string login;
            while (true)
            {
                login = Ui.ReadNonEmpty("Логін: ");

                if (IsLoginReserved(login))
                {
                    Console.WriteLine("Цей логін зарезервований для адміністратора.");
                    continue;
                }

                if (FindUser(db, login) != null)
                {
                    Console.WriteLine("Такий логін вже існує. Спробуйте інший.");
                    continue;
                }

                break;
            }

            var password = Ui.ReadNonEmpty("Пароль: ");
            var name = Ui.ReadOptional("Ім'я/ПІБ (Enter - як логін): ");
            if (string.IsNullOrWhiteSpace(name))
                name = login;

            db.Users.Add(new UserAccount
            {
                Login = login,
                Password = password,
                DisplayName = name.Trim(),
                RegisteredAt = DateTime.Now
            });

            Ui.Pause("Реєстрація успішна. Тепер можна увійти.");
        }

        private static bool IsLoginReserved(string login)
        {
            return string.Equals(login, AdminLogin, StringComparison.OrdinalIgnoreCase);
        }

        private static UserAccount? FindUser(InMemoryDb db, string login)
        {
            foreach (var user in db.Users)
            {
                if (string.Equals(user.Login, login, StringComparison.OrdinalIgnoreCase))
                    return user;
            }

            return null;
        }
    }
}
