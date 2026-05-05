using System;
using static HotelSystem_OOP.Program;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HotelSystem_OOP
{
    internal class Program
    {
        
        static bool exit = false;


        static public void displayMenu()
        {
            
            Console.WriteLine("===== Grand Azure Hotel System =====");
            Console.WriteLine("1. Add Guest");
            Console.WriteLine("2. Add Room");
            Console.WriteLine("3. Book a Room");
            Console.WriteLine("4. Cancel Booking");
            Console.WriteLine("5. Display Available Rooms");
            Console.WriteLine("6. Display Booked Rooms");
            Console.WriteLine("7. Search Guest by National ID");
            Console.WriteLine("8. Show Hotel Statistics");
            Console.WriteLine("9. Filter Available Rooms by Type");
            Console.WriteLine("10. Display All Guests");
            Console.WriteLine("11. Most Expensive Active Booking");
            Console.WriteLine("12. Guest Lifetime Booking Count");
            Console.WriteLine("13. Exit");

        }


        static void Main(string[] args)
        {

            Hotel hotel = new Hotel("Grand Azure");

            while (exit == false)
            {
                displayMenu();

                Console.Write("Choose option: ");

                int choice = 0;

                try
                {

                    choice = int.Parse(Console.ReadLine() ?? string.Empty);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Invalid input. Please choose a number from 1 to 12.");
                }

                switch (choice)
                {
                    case 1:

                        Console.Write("Enter guest name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter national ID: ");
                        string id = Console.ReadLine();

                        hotel.AddGuest(name, id);


                        break;


                    case 2:

                        Console.Write("Enter room number: ");
                        int roomNumber = int.Parse(Console.ReadLine());

                        Console.Write("Enter room type: ");
                        string roomType = Console.ReadLine();

                        Console.Write("Enter nightly rate: ");
                        decimal rate = decimal.Parse(Console.ReadLine());

                        hotel.AddRoom(roomNumber, roomType, rate);

                        break;


                    case 3:

                        Console.Write("Enter guest national ID: ");
                        string guestId = Console.ReadLine();

                        Console.Write("Enter room number: ");
                        int bookRoomNumber = int.Parse(Console.ReadLine());

                        Console.Write("Enter number of nights: ");
                        int nights = int.Parse(Console.ReadLine());

                        hotel.BookRoom(guestId, bookRoomNumber, nights);

                        break;

                        
                    case 4:

                        Console.Write("Enter booking ID to cancel: ");
                        int bookingId = int.Parse(Console.ReadLine());

                        hotel.CancelBooking(bookingId);

                        break;



                    case 5:

                        hotel.DisplayAvailableRooms();

                        break;



                    case 6:

                        hotel.DisplayBookedRooms();

                        break;


                    case 7:

                        Console.Write("Enter guest national ID: ");
                        string searchId = Console.ReadLine();

                        hotel.SearchGuestBookings(searchId);

                        break;


                    case 8:

                        hotel.DisplayStatistics();
                        
                        break;


                    case 9:

                        Console.Write("Enter room type (Standard / Deluxe / Suite): ");
                        string filterType = Console.ReadLine();

                        hotel.DisplayAvailableRoomsByType(filterType);


                        break;

                    case 10:

                        hotel.DisplayAllGuests();

                        break;

                    case 11:

                        hotel.FindMostExpensiveBooking();

                        break;

                    case 12:

                        Console.Write("Enter guest national ID: ");
                        string guestIdForStats = Console.ReadLine();

                        Guest foundGuest = hotel.FindGuest(guestIdForStats);

                        if (foundGuest == null)
                        {
                            Console.WriteLine("Guest not found.");
                        }
                        else
                        {
                            foundGuest.DisplayInfo();
                            Console.WriteLine($"Total bookings ever made: {foundGuest.TotalBookingsMade}");
                        }

                        break;



                    case 13:

                        exit = true;
                        Console.WriteLine("Exiting system...");
                        
                        break;


                    default:

                        Console.WriteLine("Invalid input. Please choose a number from 1 to 13.");
                        
                        break;


                }
               
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();


            }

        }

       

    public class Guest

    {
        
        private static int totalGuestsCreated;
        private string nationalID;
        private string fullName;
        private int totalBookingsMade = 0;

            // Properties
            public string NationalID

        {
            get { return nationalID; }
        }


            public string FullName
            {
                get { return fullName; }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        Console.WriteLine("Error: Name cannot be empty or whitespace");
                        return;
                    }

                    string trimmedName = value.Trim();

                    if (trimmedName.Length < 3)
                    {
                        Console.WriteLine("Error: Name must be at least 3 characters long");
                        return;
                    }

                    fullName = trimmedName;
                }
            }

            public int TotalBookingsMade
            {
                get { return totalBookingsMade; }
            }

            // Constructor
       public Guest(string name, string id)

         {
            
            FullName = name;

            
            if (!string.IsNullOrWhiteSpace(id))
            {
                 nationalID = id;
            }
  
            totalGuestsCreated++;
        }

        
        public static int GetTotalGuestsCreated()
        {
           
                return totalGuestsCreated;

        }

        
        public void DisplayInfo()

        {
            Console.WriteLine($"Guest Name: {FullName}");
            Console.WriteLine($"National ID: {NationalID}");
        }

        public void IncrementBookingCount()
            {
                totalBookingsMade++;
            }
        }

        

     public class Room

     {
        
        private int roomNumber;
        private string roomType;
        private bool isBooked;
        private decimal nightlyRate;



        public int RoomNumber
        {
            get { return roomNumber; }
        }

        public string RoomType
        {
            get { return roomType; }
        }

        public bool IsBooked
        {
            get { return isBooked; }
        }

        public decimal NightlyRate

            {
                get { return nightlyRate; }
            }

            // Constructor
            public Room(int number, string type, decimal rate)
            {
                if (string.IsNullOrWhiteSpace(type))
                {
                    throw new ArgumentException("Room type cannot be null or empty.");
                }

                if (rate <= 0)
                {
                    throw new ArgumentException("Nightly rate must be greater than zero.");
                }

                roomNumber = number;
                roomType = type;
                nightlyRate = rate;
                isBooked = false;
            }

            // Methods

            public bool Book()
        {
            if (isBooked)
            {
                return false; 
            }

            isBooked = true;
            return true;
        }

        public void CancelBooking()
        {
            isBooked = false;
        }

        public void DisplayInfo()
        {
            string status = isBooked ? "Booked" : "Available";

            Console.WriteLine($"Room Number: {RoomNumber}");
            Console.WriteLine($"Room Type: {RoomType}");
            Console.WriteLine($"Status: {status}");
        }
    }


   

     public class Booking
   
        {
        
        private static int nextBookingID = 1001;

        private int bookingID;
        private Guest guest;
        private Room room;
        private int nights;
        private decimal totalCost;



       public int BookingID

        {
            get { return bookingID; }
        }

        public Guest Guest
        {
            get { return guest; }
        }

        public Room Room
        {
            get { return room; }
        }

       public int Nights
         {
            get { return nights; }
         }

       public decimal TotalCost
         {
            get { return totalCost; }
         }

            // Constructor
            public Booking(Guest guest, Room room, int nights)
            {
                if (guest == null)
                    throw new ArgumentNullException(nameof(guest));

                if (room == null)
                    throw new ArgumentNullException(nameof(room));

                if (nights <= 0)
                    
                    throw new ArgumentException("Nights must be greater than zero.");

                bookingID = nextBookingID++;

                this.guest = guest;
                this.room = room;
                this.nights = nights;

                totalCost = room.NightlyRate * nights;
            }

            public void DisplayInfo()
        {
            Console.WriteLine($"Booking ID: {BookingID}");
            Console.WriteLine($"Guest Name: {Guest.FullName}");
            Console.WriteLine($"Room Number: {Room.RoomNumber}");
            Console.WriteLine($"Room Type: {Room.RoomType}");
            Console.WriteLine($"Nights: {Nights}");
            Console.WriteLine($"Total Cost: {TotalCost:F3} OMR");

            }
    }



     public class Hotel
    
        {
        // Fields
        private List<Guest> guests;
        private List<Room> rooms;
        private List<Booking> bookings;

        // Property
        public string HotelName { get; }

        // Constructor
        public Hotel(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                

            HotelName = name;

            guests = new List<Guest>();
            rooms = new List<Room>();
            bookings = new List<Booking>();
        }

            // =====================
            // Guest Methods
            // =====================

        public void AddGuest(string name, string id)
          {
                Guest newGuest = new Guest(name, id);

                if (newGuest.FullName == null)
                {
                    Console.WriteLine("Guest was not added due to invalid name");
                    return;
                }

                guests.Add(newGuest);
                Console.WriteLine("Guest added successfully");
           }

        public Guest FindGuest(string nationalID)
        {
            return guests.Find(g => g.NationalID == nationalID);
        }

            // =====================
            // Room Methods
            // =====================

        public void AddRoom(int number, string type, decimal rate)
            {
                if (rooms.Exists(r => r.RoomNumber == number))
                {
                    Console.WriteLine("Room with this number already exists.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(type))
                {
                    Console.WriteLine("Invalid room type.");
                    return;
                }

                string normalizedType = type.Trim().ToLower();

                if (normalizedType == "standard")
                {
                    normalizedType = "Standard";
                }
                else if (normalizedType == "deluxe")
                {
                    normalizedType = "Deluxe";
                }
                else if (normalizedType == "suite")
                {
                    normalizedType = "Suite";
                }
                else
                {
                    Console.WriteLine("Invalid room type. Allowed types: Standard, Deluxe, Suite");
                    return;
                }

                Room newRoom = new Room(number, normalizedType, rate);
                rooms.Add(newRoom);

                Console.WriteLine("Room added successfully ");
            }

        public void DisplayAvailableRooms()
        {
            Console.WriteLine("\nAvailable Rooms:");

            foreach (var room in rooms)
            {
                if (!room.IsBooked)
                {
                    room.DisplayInfo();
                    Console.WriteLine("-------------------");
                }
            }
        }

        public void DisplayBookedRooms()
        {
            Console.WriteLine("\nBooked Rooms (Bookings):");

            foreach (var booking in bookings)
            {
                booking.DisplayInfo();
                Console.WriteLine("-------------------");
            }
        }

            // =====================
            // Booking Methods
            // =====================

        public void BookRoom(string nationalID, int roomNumber, int nights)
            {

            Guest guest = FindGuest(nationalID);
            if (guest == null)
            {
                Console.WriteLine("Guest not found.");
                return;
            }

            Room room = rooms.Find(r => r.RoomNumber == roomNumber);
            if (room == null)
            {
                Console.WriteLine("Room not found.");
                return;
            }

            if (!room.Book())
            {
                Console.WriteLine("Room is already booked.");
                return;
            }

             Booking booking = new Booking(guest, room, nights);
             bookings.Add(booking);
             guest.IncrementBookingCount();

             Console.WriteLine($"Booking successful! Booking ID: {booking.BookingID}");
        }

        public void CancelBooking(int bookingID)
        {
            Booking booking = bookings.Find(b => b.BookingID == bookingID);

                if (booking == null)
                {
                    Console.WriteLine("Booking not found.");
                    return;
                }

                
                Console.WriteLine("\n=== Booking Cancellation Summary ===");
                Console.WriteLine($"Guest Name: {booking.Guest.FullName}");
                Console.WriteLine($"Room Number: {booking.Room.RoomNumber}");
                Console.WriteLine($"Nights Stayed: {booking.Nights}");
                Console.WriteLine($"Total Cost Charged: {booking.TotalCost:F3} OMR");
                Console.WriteLine("====================================");

                // Free the room
                booking.Room.CancelBooking();

                // Remove booking
                bookings.RemoveAll(b => b.BookingID == bookingID);

                Console.WriteLine("Booking cancelled successfully.");
            }

        public void SearchGuestBookings(string nationalID)
        {
            Guest guest = FindGuest(nationalID);

            if (guest == null)
            {
                Console.WriteLine("Guest not found.");
                return;
            }

            Console.WriteLine($"\nBookings for {guest.FullName}:");

            var guestBookings = bookings.Where(b => b.Guest.NationalID == nationalID).ToList();

            if (guestBookings.Count == 0)
            {
                Console.WriteLine("No bookings found.");
                return;
            }

            foreach (var booking in guestBookings)
            {
                booking.DisplayInfo();
                Console.WriteLine("-------------------");
            }
        }

        // =====================
        // Report Methods
        // =====================

        public void DisplayStatistics()
        {
            int totalGuests = guests.Count;
            int totalRooms = rooms.Count;
            int bookedRooms = rooms.Count(r => r.IsBooked);
            int availableRooms = rooms.Count(r => !r.IsBooked);
            int totalGuestsEver = Guest.GetTotalGuestsCreated();

            decimal totalRevenue = 0;

                foreach (var booking in bookings)
                {
                    totalRevenue += booking.TotalCost;
                }

                decimal averageRevenue = 0;

                if (bookings.Count > 0)
                {
                    averageRevenue = totalRevenue / bookings.Count;
                }


            Console.WriteLine("\n=== Hotel Statistics ===");
            Console.WriteLine($"Hotel Name: {HotelName}");
            Console.WriteLine($"Total Registered Guests: {totalGuests}");
            Console.WriteLine($"Total Rooms: {totalRooms}");
            Console.WriteLine($"Booked Rooms: {bookedRooms}");
            Console.WriteLine($"Available Rooms: {availableRooms}");
            Console.WriteLine($"Total Guests Ever Created: {totalGuestsEver}");
            Console.WriteLine($"Total Revenue: {totalRevenue:F3} OMR");
            Console.WriteLine($"Average Booking Value: {averageRevenue:F3} OMR");


            }

        public void DisplayAvailableRoomsByType(string type)
            {
                if (string.IsNullOrWhiteSpace(type))
                {
                    Console.WriteLine("Invalid room type.");
                    return;
                }

                Console.WriteLine($"\nAvailable {type} Rooms:");

                bool found = false;

                foreach (var r in rooms)
                {
                    if (!r.IsBooked && string.Equals(r.RoomType, type, StringComparison.OrdinalIgnoreCase))
                    {
                        r.DisplayInfo();
                        Console.WriteLine("-------------------");
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("No available rooms found for this type.");
                }
            }

        public void DisplayAllGuests()
            {
                if (guests.Count == 0)
                {
                    Console.WriteLine("No guests registered.");
                    return;
                }

                Console.WriteLine("\n=== All Registered Guests ===");

                foreach (var g in guests)
                {
                    g.DisplayInfo();
                    Console.WriteLine("-------------------");
                }

                Console.WriteLine($"Total registered guests: {guests.Count}");
            }

        public void FindMostExpensiveBooking()
            {
                if (bookings.Count == 0)
                {
                    Console.WriteLine("No active bookings.");
                    return;
                }

                Booking max = null;

                foreach (Booking b in bookings)
                {
                    if (max == null || b.TotalCost > max.TotalCost)
                    {
                        max = b;
                    }
                }

                Console.WriteLine("\n=== Most Expensive Booking ===");

                if (max != null)
                {
                    max.DisplayInfo();
                }
            }

        }
}
}
