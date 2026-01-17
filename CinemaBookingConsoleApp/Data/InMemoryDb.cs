namespace CinemaBookingConsoleApp
{
    class InMemoryDb
    {
        public List<Movie> Movies { get; } = new List<Movie>();
        public List<Hall> Halls { get; } = new List<Hall>();
        public List<Session> Sessions { get; } = new List<Session>();
        public List<Booking> Bookings { get; } = new List<Booking>();
        public List<UserAccount> Users { get; } = new List<UserAccount>();

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
    }
}
