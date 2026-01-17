namespace CinemaBookingConsoleApp
{
    class Seat
    {
        public int Row { get; }
        public int Number { get; }

        public Seat(int row, int number)
        {
            Row = row;
            Number = number;
        }

        public override bool Equals(object? obj)
        {
            var other = obj as Seat;
            if (other == null)
                return false;

            return Row == other.Row && Number == other.Number;
        }

        public override int GetHashCode()
        {
            return (Row * 1000) + Number;
        }

        public override string ToString()
        {
            return Row + "-" + Number;
        }
    }
}
