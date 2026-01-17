
namespace CinemaBookingConsoleApp
{
    static class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var db = new InMemoryDb();
            SeedDemoData(db);
            AuthService.EnsureAdminUser(db);

            while (true)
            {
                var user = AuthMenu(db);
                if (user == null)
                    return;

                if (AuthService.IsAdmin(user))
                    AdminMenu(db, user);
                else
                    ClientMenu(db, user);
            }
        }

        private static UserAccount? AuthMenu(InMemoryDb db)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== КІНОТЕАТР: БРОНЮВАННЯ КВИТКІВ (Консольний застосунок) ===");
                Console.WriteLine("1) Увійти");
                Console.WriteLine("2) Зареєструватися");
                Console.WriteLine("0) Вихід");
                Console.WriteLine();
                Console.WriteLine($"Підказка: адмін логін {AuthService.AdminLogin}, пароль {AuthService.AdminPassword}");
                Console.Write("\nВаш вибір: ");

                var choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        var user = AuthService.Login(db);
                        if (user != null)
                            return user;
                        break;
                    case "2":
                        AuthService.Register(db);
                        break;
                    case "0":
                        return null;
                    default:
                        Ui.Pause("Невірний вибір.");
                        break;
                }
            }
        }

        private static void ClientMenu(InMemoryDb db, UserAccount user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== МЕНЮ КЛІЄНТА ===");
                Console.WriteLine($"Профіль: {user.DisplayName} ({user.Login})");
                Console.WriteLine("1) Переглянути фільми");
                Console.WriteLine("2) Переглянути найближчі сеанси");
                Console.WriteLine("3) Забронювати квитки (обрати сеанс і місця)");
                Console.WriteLine("4) Скасувати бронювання");
                Console.WriteLine("5) Мої бронювання");
                Console.WriteLine("0) Вийти з акаунта");
                Console.Write("\nВаш вибір: ");

                var choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        ShowMovies(db);
                        break;
                    case "2":
                        ShowUpcomingSessions(db);
                        break;
                    case "3":
                        BookTicketsFlow(db, user);
                        break;
                    case "4":
                        CancelBookingFlow(db);
                        break;
                    case "5":
                        ShowBookingsByCustomerFlow(db, user);
                        break;
                    case "0":
                        return;
                    default:
                        Ui.Pause("Невірний вибір.");
                        break;
                }
            }
        }

        private static void AdminMenu(InMemoryDb db, UserAccount user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== МЕНЮ АДМІНІСТРАТОРА ===");
                Console.WriteLine($"Профіль: {user.DisplayName} ({user.Login})");
                Console.WriteLine("1) Керування фільмами");
                Console.WriteLine("2) Керування залами");
                Console.WriteLine("3) Керування сеансами (розклад)");
                Console.WriteLine("4) Історія сеансів (минулі)");
                Console.WriteLine("5) Перегляд усіх бронювань");
                Console.WriteLine("0) Вийти з акаунта");
                Console.Write("\nВаш вибір: ");

                var choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        MoviesAdminMenu(db);
                        break;
                    case "2":
                        HallsAdminMenu(db);
                        break;
                    case "3":
                        SessionsAdminMenu(db);
                        break;
                    case "4":
                        ShowPastSessionsAndMarkCompleted(db);
                        break;
                    case "5":
                        ShowAllBookings(db);
                        break;
                    case "0":
                        return;
                    default:
                        Ui.Pause("Невірний вибір.");
                        break;
                }
            }
        }

        private static void MoviesAdminMenu(InMemoryDb db)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ФІЛЬМИ (АДМІН) ===");
                Console.WriteLine("1) Список фільмів");
                Console.WriteLine("2) Додати фільм");
                Console.WriteLine("3) Редагувати фільм");
                Console.WriteLine("4) Видалити фільм");
                Console.WriteLine("0) Назад");
                Console.Write("\nВаш вибір: ");

                var choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        ShowMovies(db);
                        break;
                    case "2":
                        AddMovieFlow(db);
                        break;
                    case "3":
                        EditMovieFlow(db);
                        break;
                    case "4":
                        DeleteMovieFlow(db);
                        break;
                    case "0":
                        return;
                    default:
                        Ui.Pause("Невірний вибір.");
                        break;
                }
            }
        }

        private static void HallsAdminMenu(InMemoryDb db)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ЗАЛИ (АДМІН) ===");
                Console.WriteLine("1) Список залів");
                Console.WriteLine("2) Додати зал");
                Console.WriteLine("3) Переглянути схему місць залу");
                Console.WriteLine("0) Назад");
                Console.Write("\nВаш вибір: ");

                var choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        ShowHalls(db);
                        break;
                    case "2":
                        AddHallFlow(db);
                        break;
                    case "3":
                        ShowHallLayoutFlow(db);
                        break;
                    case "0":
                        return;
                    default:
                        Ui.Pause("Невірний вибір.");
                        break;
                }
            }
        }

        private static void SessionsAdminMenu(InMemoryDb db)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== СЕАНСИ / РОЗКЛАД (АДМІН) ===");
                Console.WriteLine("1) Список майбутніх сеансів");
                Console.WriteLine("2) Додати сеанс");
                Console.WriteLine("3) Видалити сеанс");
                Console.WriteLine("0) Назад");
                Console.Write("\nВаш вибір: ");

                var choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        ShowUpcomingSessions(db);
                        break;
                    case "2":
                        AddSessionFlow(db);
                        break;
                    case "3":
                        DeleteSessionFlow(db);
                        break;
                    case "0":
                        return;
                    default:
                        Ui.Pause("Невірний вибір.");
                        break;
                }
            }
        }
        private static void ShowMovies(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК ФІЛЬМІВ ===");

            var movies = GetMoviesSortedById(db);
            if (movies.Count == 0)
            {
                Ui.Pause("Немає жодного фільму.");
                return;
            }

            foreach (var m in movies)
                Console.WriteLine($"{m.Id}) {m.Title} | {m.DurationMinutes} хв | {m.AgeRating}+");

            Ui.Pause();
        }

        private static void AddMovieFlow(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== ДОДАТИ ФІЛЬМ ===");

            var title = Ui.ReadNonEmpty("Назва: ");
            var duration = Ui.ReadInt("Тривалість (хв): ", 1, 600);
            var age = Ui.ReadInt("Вікове обмеження (0..21): ", 0, 21);

            var movie = new Movie
            {
                Id = db.NextMovieId(),
                Title = title,
                DurationMinutes = duration,
                AgeRating = age
            };

            db.Movies.Add(movie);
            Ui.Pause("Фільм додано.");
        }

        private static void EditMovieFlow(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== РЕДАГУВАТИ ФІЛЬМ ===");
            ShowMoviesInline(db);

            if (db.Movies.Count == 0)
            {
                Ui.Pause("Немає фільмів для редагування.");
                return;
            }

            var id = Ui.ReadInt("Введіть код фільму: ", 1, int.MaxValue);
            var movie = FindMovieById(db, id);
            if (movie == null)
            {
                Ui.Pause("Фільм не знайдено.");
                return;
            }

            Console.WriteLine($"Поточна назва: {movie.Title}");
            var title = Ui.ReadOptional("Нова назва (Enter - без змін): ");
            if (!string.IsNullOrWhiteSpace(title))
                movie.Title = title.Trim();

            Console.WriteLine($"Поточна тривалість: {movie.DurationMinutes}");
            var durStr = Ui.ReadOptional("Нова тривалість (хв) (Enter - без змін): ");
            if (int.TryParse(durStr, out var newDur) && newDur > 0)
                movie.DurationMinutes = newDur;

            Console.WriteLine($"Поточне вікове обмеження: {movie.AgeRating}+");
            var ageStr = Ui.ReadOptional("Нове вікове обмеження (0..21) (Enter - без змін): ");
            if (int.TryParse(ageStr, out var newAge) && newAge >= 0 && newAge <= 21)
                movie.AgeRating = newAge;

            Ui.Pause("Фільм оновлено.");
        }

        private static void DeleteMovieFlow(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== ВИДАЛИТИ ФІЛЬМ ===");
            ShowMoviesInline(db);

            if (db.Movies.Count == 0)
            {
                Ui.Pause("Немає фільмів для видалення.");
                return;
            }

            var id = Ui.ReadInt("Введіть код фільму: ", 1, int.MaxValue);
            var movie = FindMovieById(db, id);
            if (movie == null)
            {
                Ui.Pause("Фільм не знайдено.");
                return;
            }

            var hasSessions = false;
            foreach (var s in db.Sessions)
            {
                if (s.MovieId == id)
                {
                    hasSessions = true;
                    break;
                }
            }

            if (hasSessions)
            {
                Ui.Pause("Неможливо видалити: існують сеанси з цим фільмом.");
                return;
            }

            db.Movies.Remove(movie);
            Ui.Pause("Фільм видалено.");
        }

        private static void ShowHalls(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК ЗАЛІВ ===");

            var halls = GetHallsSortedById(db);
            if (halls.Count == 0)
            {
                Ui.Pause("Немає жодного залу.");
                return;
            }

            foreach (var h in halls)
                Console.WriteLine($"{h.Id}) {h.Name} | Рядів: {h.Rows} | Місць у ряду: {h.SeatsPerRow}");

            Ui.Pause();
        }

        private static void AddHallFlow(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== ДОДАТИ ЗАЛ ===");

            var name = Ui.ReadNonEmpty("Назва залу: ");
            var rows = Ui.ReadInt("Кількість рядів: ", 1, 50);
            var seatsPerRow = Ui.ReadInt("Місць у ряду: ", 1, 50);

            var hall = new Hall
            {
                Id = db.NextHallId(),
                Name = name,
                Rows = rows,
                SeatsPerRow = seatsPerRow
            };

            db.Halls.Add(hall);
            Ui.Pause("Зал додано.");
        }

        private static void ShowHallLayoutFlow(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== СХЕМА МІСЦЬ ЗАЛУ ===");
            ShowHallsInline(db);

            if (db.Halls.Count == 0)
            {
                Ui.Pause("Немає залів.");
                return;
            }

            var id = Ui.ReadInt("Введіть код залу: ", 1, int.MaxValue);
            var hall = FindHallById(db, id);
            if (hall == null)
            {
                Ui.Pause("Зал не знайдено.");
                return;
            }

            Console.Clear();
            Console.WriteLine($"=== {hall.Name} (Рядів: {hall.Rows}, Місць у ряду: {hall.SeatsPerRow}) ===");
            Console.WriteLine("Позначення: O - вільне місце (схема без прив'язки до сеансу)");
            PrintHallLayout(hall, new HashSet<Seat>());

            Ui.Pause();
        }
        private static void ShowUpcomingSessions(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== МАЙБУТНІ СЕАНСИ ===");

            var sessions = GetUpcomingSessions(db);
            if (sessions.Count == 0)
            {
                Ui.Pause("Немає майбутніх сеансів.");
                return;
            }

            PrintSessionsList(db, sessions);
            Ui.Pause();
        }

        private static void AddSessionFlow(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== ДОДАТИ СЕАНС ===");

            if (db.Movies.Count == 0)
            {
                Ui.Pause("Спочатку додайте фільми.");
                return;
            }

            if (db.Halls.Count == 0)
            {
                Ui.Pause("Спочатку додайте зали.");
                return;
            }

            Console.WriteLine("\nФільми:");
            ShowMoviesInline(db);
            var movieId = Ui.ReadInt("Код фільму: ", 1, int.MaxValue);
            if (FindMovieById(db, movieId) == null)
            {
                Ui.Pause("Фільм не знайдено.");
                return;
            }

            Console.WriteLine("\nЗали:");
            ShowHallsInline(db);
            var hallId = Ui.ReadInt("Код залу: ", 1, int.MaxValue);
            if (FindHallById(db, hallId) == null)
            {
                Ui.Pause("Зал не знайдено.");
                return;
            }

            var dt = Ui.ReadDateTime("Дата і час сеансу (наприклад 15.01.2026 18:30): ");

            var collision = false;
            foreach (var s in db.Sessions)
            {
                if (s.HallId == hallId && s.StartTime == dt)
                {
                    collision = true;
                    break;
                }
            }

            if (collision)
            {
                Ui.Pause("У цьому залі вже є сеанс на цей час.");
                return;
            }

            var session = new Session
            {
                Id = db.NextSessionId(),
                MovieId = movieId,
                HallId = hallId,
                StartTime = dt
            };

            db.Sessions.Add(session);
            Ui.Pause("Сеанс додано.");
        }

        private static void DeleteSessionFlow(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== ВИДАЛИТИ СЕАНС ===");
            ShowSessionsInline(db);

            if (db.Sessions.Count == 0)
            {
                Ui.Pause("Немає сеансів.");
                return;
            }

            var id = Ui.ReadInt("Введіть код сеансу: ", 1, int.MaxValue);
            var session = FindSessionById(db, id);
            if (session == null)
            {
                Ui.Pause("Сеанс не знайдено.");
                return;
            }

            var hasActiveBookings = false;
            foreach (var b in db.Bookings)
            {
                if (b.SessionId == id && b.Status == BookingStatus.Active)
                {
                    hasActiveBookings = true;
                    break;
                }
            }

            if (hasActiveBookings)
            {
                Ui.Pause("Неможливо видалити: є активні бронювання на цей сеанс.");
                return;
            }

            db.Sessions.Remove(session);
            Ui.Pause("Сеанс видалено.");
        }

        private static void BookTicketsFlow(InMemoryDb db, UserAccount user)
        {
            Console.Clear();
            Console.WriteLine("=== БРОНЮВАННЯ КВИТКІВ ===");

            var upcoming = GetUpcomingSessions(db);
            if (upcoming.Count == 0)
            {
                Ui.Pause("Немає майбутніх сеансів для бронювання.");
                return;
            }

            Console.WriteLine("Оберіть сеанс:");
            PrintSessionsList(db, upcoming);

            var sessionId = Ui.ReadInt("Код сеансу: ", 1, int.MaxValue);
            var session = FindSessionById(db, sessionId);
            if (session == null || session.StartTime < DateTime.Now)
            {
                Ui.Pause("Сеанс не знайдено або він вже минув.");
                return;
            }

            var hall = FindHallById(db, session.HallId);
            var movie = FindMovieById(db, session.MovieId);
            if (hall == null || movie == null)
            {
                Ui.Pause("Помилка в даних сеансу.");
                return;
            }

            var occupied = db.GetOccupiedSeats(sessionId);

            Console.Clear();
            Console.WriteLine($"Фільм: {movie.Title}");
            Console.WriteLine($"Час: {session.StartTime:dd.MM.yyyy HH:mm}");
            Console.WriteLine($"Зал: {hall.Name}");
            Console.WriteLine("\nПозначення: O - вільно, X - зайнято");
            PrintHallLayout(hall, occupied);

            var customer = Ui.ReadOptional($"Ім'я/ПІБ (Enter - {user.DisplayName}): ");
            if (string.IsNullOrWhiteSpace(customer))
                customer = user.DisplayName;

            var seatsToBook = new List<Seat>();

            while (true)
            {
                Console.WriteLine("\nВведіть місце для бронювання.");
                var row = Ui.ReadInt($"Ряд (1..{hall.Rows}): ", 1, hall.Rows);
                var number = Ui.ReadInt($"Місце (1..{hall.SeatsPerRow}): ", 1, hall.SeatsPerRow);

                var seat = new Seat(row, number);

                if (occupied.Contains(seat))
                {
                    Console.WriteLine("Це місце вже зайняте. Оберіть інше.");
                    continue;
                }

                if (seatsToBook.Contains(seat))
                {
                    Console.WriteLine("Ви вже додали це місце в бронювання.");
                    continue;
                }

                seatsToBook.Add(seat);

                var more = Ui.ReadOptional("Додати ще місце? (т/н): ");
                if (!IsYes(more))
                    break;
            }

            if (seatsToBook.Count == 0)
            {
                Ui.Pause("Жодного місця не вибрано. Операцію скасовано.");
                return;
            }

            occupied = db.GetOccupiedSeats(sessionId);
            foreach (var s in seatsToBook)
            {
                if (occupied.Contains(s))
                {
                    Ui.Pause("Поки ви вводили місця, частина з них стала зайнятою. Спробуйте ще раз.");
                    return;
                }
            }

            var booking = new Booking
            {
                Id = db.NextBookingId(),
                SessionId = sessionId,
                CustomerName = customer.Trim(),
                Seats = seatsToBook,
                Status = BookingStatus.Active,
                CreatedAt = DateTime.Now
            };

            db.Bookings.Add(booking);

            Ui.Pause($"Бронювання створено. Код бронювання: {booking.Id}");
        }

        private static void CancelBookingFlow(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== СКАСУВАННЯ БРОНЮВАННЯ ===");

            if (db.Bookings.Count == 0)
            {
                Ui.Pause("Немає бронювань.");
                return;
            }

            var id = Ui.ReadInt("Введіть код бронювання: ", 1, int.MaxValue);
            var booking = FindBookingById(db, id);
            if (booking == null)
            {
                Ui.Pause("Бронювання не знайдено.");
                return;
            }

            if (booking.Status != BookingStatus.Active)
            {
                Ui.Pause("Неможливо скасувати. Поточний статус: " + GetStatusText(booking.Status));
                return;
            }

            booking.Status = BookingStatus.Cancelled;
            booking.CancelledAt = DateTime.Now;

            Ui.Pause("Бронювання скасовано.");
        }
        private static void ShowBookingsByCustomerFlow(InMemoryDb db, UserAccount user)
        {
            Console.Clear();
            Console.WriteLine("=== МОЇ БРОНЮВАННЯ ===");

            var name = Ui.ReadOptional($"Ім'я/ПІБ (Enter - {user.DisplayName}): ");
            if (string.IsNullOrWhiteSpace(name))
                name = user.DisplayName;

            var list = new List<Booking>();
            foreach (var b in db.Bookings)
            {
                if (string.Equals(b.CustomerName, name, StringComparison.OrdinalIgnoreCase))
                    list.Add(b);
            }

            list.Sort((a, b) => b.CreatedAt.CompareTo(a.CreatedAt));

            if (list.Count == 0)
            {
                Ui.Pause("Бронювань не знайдено.");
                return;
            }

            PrintBookings(db, list);
            Ui.Pause();
        }

        private static void ShowAllBookings(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== УСІ БРОНЮВАННЯ ===");

            if (db.Bookings.Count == 0)
            {
                Ui.Pause("Немає бронювань.");
                return;
            }

            var list = new List<Booking>(db.Bookings);
            list.Sort((a, b) => b.CreatedAt.CompareTo(a.CreatedAt));

            PrintBookings(db, list);
            Ui.Pause();
        }

        private static void ShowPastSessionsAndMarkCompleted(InMemoryDb db)
        {
            Console.Clear();
            Console.WriteLine("=== ІСТОРІЯ СЕАНСІВ (МИНУЛІ) ===");

            var past = GetPastSessions(db);
            if (past.Count == 0)
            {
                Ui.Pause("Немає минулих сеансів.");
                return;
            }

            foreach (var session in past)
            {
                foreach (var b in db.Bookings)
                {
                    if (b.SessionId == session.Id && b.Status == BookingStatus.Active)
                    {
                        b.Status = BookingStatus.Completed;
                        b.CompletedAt = DateTime.Now;
                    }
                }
            }

            PrintSessionsList(db, past);
            Ui.Pause("Активні бронювання на минулі сеанси позначено як завершені.");
        }

        private static void PrintSessionsList(InMemoryDb db, List<Session> sessions)
        {
            foreach (var s in sessions)
            {
                var movie = FindMovieById(db, s.MovieId);
                var hall = FindHallById(db, s.HallId);

                var movieTitle = movie == null ? "Невідомий фільм" : movie.Title;
                var hallName = hall == null ? "Невідомий зал" : hall.Name;

                Console.WriteLine($"{s.Id}) {s.StartTime:dd.MM.yyyy HH:mm} | {movieTitle} | {hallName}");
            }
        }

        private static void PrintBookings(InMemoryDb db, List<Booking> bookings)
        {
            foreach (var b in bookings)
            {
                var session = FindSessionById(db, b.SessionId);
                var movie = session == null ? null : FindMovieById(db, session.MovieId);
                var hall = session == null ? null : FindHallById(db, session.HallId);

                var time = session == null ? "?" : session.StartTime.ToString("dd.MM.yyyy HH:mm");
                var movieTitle = movie == null ? "?" : movie.Title;
                var hallName = hall == null ? "?" : hall.Name;

                var seats = new List<Seat>(b.Seats);
                seats.Sort((a, b2) =>
                {
                    if (a.Row != b2.Row)
                        return a.Row.CompareTo(b2.Row);

                    return a.Number.CompareTo(b2.Number);
                });

                var seatsText = "";
                for (int i = 0; i < seats.Count; i++)
                {
                    if (i > 0)
                        seatsText += ", ";
                    seatsText += seats[i].Row + "-" + seats[i].Number;
                }

                Console.WriteLine($"Код:{b.Id} | {b.CustomerName} | {time} | {movieTitle} | {hallName}");
                Console.WriteLine($"   Місця: {seatsText}");
                Console.WriteLine($"   Статус: {GetStatusText(b.Status)} | Створено: {b.CreatedAt:dd.MM.yyyy HH:mm}");
                if (b.CancelledAt != null)
                    Console.WriteLine($"   Скасовано: {b.CancelledAt:dd.MM.yyyy HH:mm}");
                if (b.CompletedAt != null)
                    Console.WriteLine($"   Завершено: {b.CompletedAt:dd.MM.yyyy HH:mm}");
            }
        }

        private static void PrintHallLayout(Hall hall, HashSet<Seat> occupied)
        {
            Console.Write("     ");
            for (int seat = 1; seat <= hall.SeatsPerRow; seat++)
                Console.Write($"{seat,2} ");
            Console.WriteLine();

            for (int row = 1; row <= hall.Rows; row++)
            {
                Console.Write($"Р{row,2}: ");
                for (int seat = 1; seat <= hall.SeatsPerRow; seat++)
                {
                    var s = new Seat(row, seat);
                    var isOcc = occupied.Contains(s);
                    Console.Write(isOcc ? " X " : " O ");
                }
                Console.WriteLine();
            }
        }
        private static void ShowMoviesInline(InMemoryDb db)
        {
            var movies = GetMoviesSortedById(db);
            if (movies.Count == 0)
            {
                Console.WriteLine("Немає фільмів.");
                return;
            }

            foreach (var m in movies)
                Console.WriteLine($"{m.Id}) {m.Title} | {m.DurationMinutes} хв | {m.AgeRating}+");
        }

        private static void ShowHallsInline(InMemoryDb db)
        {
            var halls = GetHallsSortedById(db);
            if (halls.Count == 0)
            {
                Console.WriteLine("Немає залів.");
                return;
            }

            foreach (var h in halls)
                Console.WriteLine($"{h.Id}) {h.Name} | Рядів: {h.Rows} | Місць у ряду: {h.SeatsPerRow}");
        }

        private static void ShowSessionsInline(InMemoryDb db)
        {
            if (db.Sessions.Count == 0)
            {
                Console.WriteLine("Немає сеансів.");
                return;
            }

            var list = new List<Session>(db.Sessions);
            list.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
            PrintSessionsList(db, list);
        }

        private static List<Session> GetUpcomingSessions(InMemoryDb db)
        {
            var list = new List<Session>();
            var now = DateTime.Now;

            foreach (var s in db.Sessions)
            {
                if (s.StartTime >= now)
                    list.Add(s);
            }

            list.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
            return list;
        }

        private static List<Session> GetPastSessions(InMemoryDb db)
        {
            var list = new List<Session>();
            var now = DateTime.Now;

            foreach (var s in db.Sessions)
            {
                if (s.StartTime < now)
                    list.Add(s);
            }

            list.Sort((a, b) => b.StartTime.CompareTo(a.StartTime));
            return list;
        }

        private static List<Movie> GetMoviesSortedById(InMemoryDb db)
        {
            var list = new List<Movie>(db.Movies);
            list.Sort((a, b) => a.Id.CompareTo(b.Id));
            return list;
        }

        private static List<Hall> GetHallsSortedById(InMemoryDb db)
        {
            var list = new List<Hall>(db.Halls);
            list.Sort((a, b) => a.Id.CompareTo(b.Id));
            return list;
        }

        private static Movie? FindMovieById(InMemoryDb db, int id)
        {
            foreach (var m in db.Movies)
            {
                if (m.Id == id)
                    return m;
            }

            return null;
        }

        private static Hall? FindHallById(InMemoryDb db, int id)
        {
            foreach (var h in db.Halls)
            {
                if (h.Id == id)
                    return h;
            }

            return null;
        }

        private static Session? FindSessionById(InMemoryDb db, int id)
        {
            foreach (var s in db.Sessions)
            {
                if (s.Id == id)
                    return s;
            }

            return null;
        }

        private static Booking? FindBookingById(InMemoryDb db, int id)
        {
            foreach (var b in db.Bookings)
            {
                if (b.Id == id)
                    return b;
            }

            return null;
        }

        private static string GetStatusText(BookingStatus status)
        {
            if (status == BookingStatus.Active)
                return "Активне";
            if (status == BookingStatus.Cancelled)
                return "Скасоване";
            if (status == BookingStatus.Completed)
                return "Завершене";

            return status.ToString();
        }

        private static bool IsYes(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            var s = input.Trim().ToLowerInvariant();
            return s == "y" || s == "т" || s == "так" || s == "yes";
        }

        private static void SeedDemoData(InMemoryDb db)
        {
            var m1 = new Movie { Id = db.NextMovieId(), Title = "Інтерстеллар", DurationMinutes = 169, AgeRating = 12 };
            var m2 = new Movie { Id = db.NextMovieId(), Title = "Дюна", DurationMinutes = 155, AgeRating = 12 };
            var m3 = new Movie { Id = db.NextMovieId(), Title = "Джокер", DurationMinutes = 122, AgeRating = 16 };
            db.Movies.AddRange(new[] { m1, m2, m3 });

            var h1 = new Hall { Id = db.NextHallId(), Name = "Зал 1", Rows = 6, SeatsPerRow = 10 };
            var h2 = new Hall { Id = db.NextHallId(), Name = "Зал 2", Rows = 8, SeatsPerRow = 12 };
            db.Halls.AddRange(new[] { h1, h2 });

            var s1 = new Session { Id = db.NextSessionId(), MovieId = m1.Id, HallId = h1.Id, StartTime = DateTime.Now.AddHours(3) };
            var s2 = new Session { Id = db.NextSessionId(), MovieId = m2.Id, HallId = h2.Id, StartTime = DateTime.Now.AddDays(1).AddHours(2) };
            var s3 = new Session { Id = db.NextSessionId(), MovieId = m3.Id, HallId = h1.Id, StartTime = DateTime.Now.AddDays(-1) };
            db.Sessions.Add(s1);
            db.Sessions.Add(s2);
            db.Sessions.Add(s3);

            db.Bookings.Add(new Booking
            {
                Id = db.NextBookingId(),
                SessionId = s1.Id,
                CustomerName = "Дмитро",
                Seats = new List<Seat> { new Seat(1, 1), new Seat(1, 2) },
                Status = BookingStatus.Active,
                CreatedAt = DateTime.Now.AddMinutes(-30)
            });
        }
    }
}
