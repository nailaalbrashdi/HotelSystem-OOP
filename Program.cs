using static HotelSystem_OOP.Program;

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
            Console.WriteLine("9. Exit");
            
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

                        hotel.AddRoom(roomNumber, roomType);


                        break;



                    case 3:

                        Console.Write("Enter guest national ID: ");
                        string guestId = Console.ReadLine();

                        Console.Write("Enter room number: ");
                        int bookRoomNumber = int.Parse(Console.ReadLine());

                        hotel.BookRoom(guestId, bookRoomNumber);

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

                        exit = true;
                        Console.WriteLine("Exiting system...");
                        break;


                    default:
                        Console.WriteLine("Invalid choice. Please select from 1 to 9.");
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
                if (!string.IsNullOrWhiteSpace(value))
                {
                        fullName = value;
                }
                
            }
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
    }

        

     public class Room

     {
        
        private int roomNumber;
        private string roomType;
        private bool isBooked;

        

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

        // Constructor
        public Room(int number, string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("Room type cannot be null or empty.");
            }

            roomNumber = number;
            roomType = type;
            isBooked = false; // default
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

        // Constructor
        public Booking(Guest guest, Room room)
        {
            
            if (guest == null)
                throw new ArgumentNullException(nameof(guest));

            if (room == null)
                throw new ArgumentNullException(nameof(room));

            
            bookingID = nextBookingID++;

            this.guest = guest;
            this.room = room;
        }

        
        public void DisplayInfo()
        {
            Console.WriteLine($"Booking ID: {BookingID}");
            Console.WriteLine($"Guest Name: {Guest.FullName}");
            Console.WriteLine($"Room Number: {Room.RoomNumber}");
            Console.WriteLine($"Room Type: {Room.RoomType}");
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
            guests.Add(newGuest);

            Console.WriteLine("Guest added successfully.");
        }

        public Guest FindGuest(string nationalID)
        {
            return guests.Find(g => g.NationalID == nationalID);
        }

        // =====================
        // Room Methods
        // =====================

        public void AddRoom(int number, string type)
        {
            if (rooms.Exists(r => r.RoomNumber == number))
            {
                Console.WriteLine("Room with this number already exists.");
                return;
            }

            Room newRoom = new Room(number, type);
            rooms.Add(newRoom);

            Console.WriteLine("Room added successfully.");
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

        public void BookRoom(string nationalID, int roomNumber)
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

            Booking booking = new Booking(guest, room);
            bookings.Add(booking);

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

            Console.WriteLine("\n=== Hotel Statistics ===");
            Console.WriteLine($"Hotel Name: {HotelName}");
            Console.WriteLine($"Total Registered Guests: {totalGuests}");
            Console.WriteLine($"Total Rooms: {totalRooms}");
            Console.WriteLine($"Booked Rooms: {bookedRooms}");
            Console.WriteLine($"Available Rooms: {availableRooms}");
            Console.WriteLine($"Total Guests Ever Created: {totalGuestsEver}");
        }
    }
}
}
