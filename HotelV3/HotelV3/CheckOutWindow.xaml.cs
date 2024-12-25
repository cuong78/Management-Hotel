using BLL.Service;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelV3
{
    /// <summary>
    /// Interaction logic for CheckOutWindow.xaml
    /// </summary>
    public partial class CheckOutWindow : Window
    {
        private BookService _bookService = new(); // Add BookService instance
        private ServiceDetailService _serviceDetailService = new();
        private int _roomNumber;
        public CheckOutWindow(int roomNumber, string guestName)
        {
            InitializeComponent();
            _roomNumber = roomNumber;
            RoomNumberTextBox.Text = roomNumber.ToString();
            GuestNameTextBox.Text = guestName;
            CheckOutDateTextBox.Text = DateTime.Now.ToString("g"); // Current date and time
            Console.WriteLine(guestName);
        }

        private void CheckOutButton_Click(object sender, RoutedEventArgs e)
        { 
            HoaDonWindow hoaDonWindow = new HoaDonWindow();
            hoaDonWindow.ServiceDetail = _serviceDetailService.getServiceByRoomNumber(_roomNumber);
            hoaDonWindow.Booked = _bookService.GetBookedRoomNumbers(_roomNumber);
            hoaDonWindow.ShowDialog();
               
            _serviceDetailService.DeleteServiceCheckOut(_roomNumber);
            _bookService.RemoveBookingByRoomNumber(_roomNumber);


            MessageBox.Show($"Room {_roomNumber} has been successfully checked out.", "DONE", MessageBoxButton.OK);

            DialogResult = true; // Close the window and return success
            Close();


        }
    }
}
