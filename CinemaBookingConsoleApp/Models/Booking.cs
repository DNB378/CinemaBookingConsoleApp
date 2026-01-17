namespace CinemaBookingConsoleApp
{
    class Booking
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public string CustomerName { get; set; } = "";
        public List<Seat> Seats { get; set; } = new List<Seat>();
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
