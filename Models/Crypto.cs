namespace Models
{
    public class Crypto
    {
        public string? Initials { get; set; }
        public string? Name { get; set; }
        public double BasePrice { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }

        public Crypto(string initials, string name, double basePrice, double price, DateTime date)
        {
            Initials = initials;
            Name = name;
            BasePrice = basePrice;
            Price = price;
            Date = date;
        }
    }
}