using BLL.Service;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;



namespace HotelV3
{
    /// <summary>
    /// Interaction logic for HoaDonWindow.xaml
    /// </summary>
    public partial class HoaDonWindow : Window
    {
        public List<BookedService> ServiceDetail { get; set; }
        public Booked Booked { get; set; }
        private readonly BookService _bookService = new();
        private readonly ServiceDetailService _serviceDetailService = new();

        public HoaDonWindow()
        {
            InitializeComponent();

        }

        private void LoadInvoiceDetails()
        {

            RoomNumberTextBlock.Text = Booked.RoomNumber.ToString();
            GuestNameTextBlock.Text = Booked.GuestName;
            CheckInTimeTextBlock.Text = Booked.DateCreate.ToString("g");
            CheckOutTimeTextBlock.Text = DateTime.Now.ToString("g");

            ServicesListView.ItemsSource = ServiceDetail.Select(s => new
            {
                s.NameService,
                s.Quantity,
                Price = s.NameServiceNavigation.Price
            });

            var roomCharge = CalculateRoomCharge(Booked.DateCreate, DateTime.Now);
            RoomChargeTextBlock.Text = roomCharge.ToString("C");

            var serviceCharge = ServiceDetail.Sum(s => s.Quantity * s.NameServiceNavigation.Price);
            var totalCharge = roomCharge + serviceCharge;
            TotalChargeTextBlock.Text = totalCharge.ToString("C");
        }

        private decimal CalculateRoomCharge(DateTime checkIn, DateTime checkOut)
        {
            TimeSpan stayDuration = checkOut - checkIn;
            int days = stayDuration.Days;
            int hours = stayDuration.Hours;
            decimal roomCharge = 0;

            if (days > 0)
            {
                // Charge for full days
                roomCharge += 300000 * days;

                // Check if the stay goes past midday into the next day
                if (checkOut.TimeOfDay.TotalHours >= 12)
                {
                    roomCharge += 300000; // Additional charge for past midday
                }
            }
            else
            {
                // Charge for less than a day
                if (hours < 1)
                {
                    roomCharge = 200000; // First hour charge
                }
                else
                {
                    roomCharge = 200000 + (30000 * (hours - 1)); // Additional hours charge
                    roomCharge = Math.Min(roomCharge, 300000); // Cap at 300000
                }
            }

            return roomCharge;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInvoiceDetails();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime checkOut = DateTime.Now;

            BookListRepo bookListRepo = new BookListRepo();
            BookedList bookList = new BookedList();
            BookRepository bookRepository = new BookRepository();



            bookList.GuestName = GuestNameTextBlock.Text;
            bookList.CheckIn = Booked.DateCreate;
            bookList.CheckOut = checkOut;
            bookList.RoomNumber = int.Parse(RoomNumberTextBlock.Text);
            bookList.Services = bookRepository.getListServicesByRoomNumber(int.Parse(RoomNumberTextBlock.Text));
            bookList.PhoneNumber = Booked.PhoneNumber;

            var serviceCharge = ServiceDetail.Sum(s => s.Quantity * s.NameServiceNavigation.Price);
            var roomCharge = CalculateRoomCharge(Booked.DateCreate, DateTime.Now);

            var totalCharge = roomCharge + serviceCharge;
            bookList.TotalBill = totalCharge;

            bookList.Email = Booked.Email;
            bookListRepo.saveBookList(bookList);

            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(PrintPage, "invoice");
            }

            this.Close();
        }

    }
}
