namespace CinemaBookingConsoleApp
{
    class InMemoryDb
    {
        public List<Movie> Movies { get; set; } = new List<Movie>();
        public List<Hall> Halls { get; set; } = new List<Hall>();
        public List<Session> Sessions { get; set; } = new List<Session>();
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public List<UserAccount> Users { get; set; } = new List<UserAccount>();

        private int _movieId;
        private int _hallId;
        private int _sessionId;
        private int _bookingId;

        public int NextMovieId()
        {
            _movieId++;
            return _movieId;
        }

        public int NextHallId()
        {
            _hallId++;
            return _hallId;
        }

        public int NextSessionId()
        {
            _sessionId++;
            return _sessionId;
        }

        public int NextBookingId()
        {
            _bookingId++;
            return _bookingId;
        }

        public HashSet<Seat> GetOccupiedSeats(int sessionId)
        {
            var occ = new HashSet<Seat>();

            foreach (var b in Bookings)
            {
                if (b.SessionId != sessionId || b.Status != BookingStatus.Active)
                    continue;

                foreach (var seat in b.Seats)
                    occ.Add(seat);
            }

            return occ;
        }

        public void RecalculateIds()
        {
            _movieId = GetMaxMovieId();
            _hallId = GetMaxHallId();
            _sessionId = GetMaxSessionId();
            _bookingId = GetMaxBookingId();
        }

        private int GetMaxMovieId()
        {
            var max = 0;
            foreach (var movie in Movies)
            {
                if (movie.Id > max)
                    max = movie.Id;
            }
            return max;
        }

        private int GetMaxHallId()
        {
            var max = 0;
            foreach (var hall in Halls)
            {
                if (hall.Id > max)
                    max = hall.Id;
            }
            return max;
        }

        private int GetMaxSessionId()
        {
            var max = 0;
            foreach (var session in Sessions)
            {
                if (session.Id > max)
                    max = session.Id;
            }
            return max;
        }

        private int GetMaxBookingId()
        {
            var max = 0;
            foreach (var booking in Bookings)
            {
                if (booking.Id > max)
                    max = booking.Id;
            }
            return max;
        }
    }
}
