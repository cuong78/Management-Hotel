using BLL.Service;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace HotelV3
{
    /// <summary>
    /// Interaction logic for BookedRoomWindow.xaml
    /// </summary>
    public partial class BookedRoomWindow : Window
    {
        private RoomService _service = new();
        private BookService _bookService = new(); 

        private BookListRepo _bookedList = new();
        private int _roomNumber;
        public BookedRoomWindow()
        {
            InitializeComponent();
        }
        public BookedRoomWindow(int roomNumber)
        {
            InitializeComponent();
            _roomNumber = roomNumber;
        }
 
     
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs())
            {
                
                return;
            }
            string email = EmailTextBox.Text;
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error); 
                return;
            }
            Booked booked = new Booked
            {
                DateCreate = DateTime.Now,
                GuestName = GuestNameTextBox.Text,
                BookStatus = "Checked-in",
                RoomNumber = _roomNumber, 
                PhoneNumber = PhoneTextBox.Text,
                Email = EmailTextBox.Text,
                Address = AddressTextBox.Text,
            };



            _bookService.saveBooking(booked);
            _bookService.UpdateRoomStatus(booked.RoomNumber, "Booked");

           
            this.Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            IdTextBox.IsEnabled = false;

            // Optionally, set the room number in the UI if needed
            RoomTextBox.Text = _roomNumber.ToString();
            RoomTextBox.IsEnabled = false;
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    
        private bool ValidateInputs()
        {
            // Check if GuestName is empty
            if (string.IsNullOrWhiteSpace(GuestNameTextBox.Text))
            {
                MessageBox.Show("Please enter email.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Check if PhoneNumber is exactly 10 digits and contains only numbers
            if (!System.Text.RegularExpressions.Regex.IsMatch(PhoneTextBox.Text, @"^\d{10}$"))
            {
                MessageBox.Show("Please enter the correct phone number model.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
                
            }

            // Check if Email is empty and set it to "NA" if so
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailTextBox.Text = "NA";
            }

            // Check if Address is empty
            if (string.IsNullOrWhiteSpace(AddressTextBox.Text))
            {
                MessageBox.Show("Please enter address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }

       

      
       
    }
}
