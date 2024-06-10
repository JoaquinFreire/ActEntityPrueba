using System;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        using (var context = new CustomDbContext())
        {
            // Verificar si hay datos iniciales, de lo contrario agregar algunos
            if (!context.Customers.Any())
            {
                context.Customers.Add(new Customer { FirstName = "John", LastName = "Doe", Gender = Gender.Male, Age = 30 });
                context.Destinations.Add(new Destination { City = "Paris" });
                context.SaveChanges();
            }

            // Crear una nueva reserva
            var customer = context.Customers.First(c => c.Id == 1);
            var destination = context.Destinations.First(d => d.Id == 1);

            var booking = new Booking
            {
                CustomerId = customer.Id,
                DestinationId = destination.Id,
                ReservationDate = new DateTime(2024, 6, 15),
                ReservedDate = DateTime.Now
            };

            context.Bookings.Add(booking);
            context.SaveChanges();

            Console.WriteLine($"Reserva creada para {customer.FirstName} {customer.LastName} a {destination.City} el {booking.ReservationDate.ToShortDateString()}");
        }
    }
}
public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public ICollection<Booking> Bookings { get; set; }
}

// Booking
public class Booking
{
    public int Id { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime ReservedDate { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int DestinationId { get; set; }
    public Destination Destination { get; set; }
}

// Destination
public class Destination
{
    public int Id { get; set; }
    public string City { get; set; }
    public ICollection<Booking> Bookings { get; set; }
}

// Gender
public enum Gender
{
    Male,
    Female,
    Other
}

//hola 