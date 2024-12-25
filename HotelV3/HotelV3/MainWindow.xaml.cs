using BLL.Service;
using DAL.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelV3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer player = new MediaPlayer();
        public Member currentAccount { get; set; }
        private BookService _service = new BookService();
        private RoomService _roomService = new RoomService();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow log = new();
            log.Show();
            this.Close();

        }
        public void UpdateGreetingMessage()
        {
            HelloMsgLabel.Content = "Hello " + currentAccount.Role.ToString();
        }

        private void btnService_Click(object sender, RoutedEventArgs e)
        {
            ServiceWindow service = new ServiceWindow();
            service.Show();

        }

       

        private void btnMember_Click(object sender, RoutedEventArgs e)
        {
            if (currentAccount.Role != "Admin")
            {
                MessageBox.Show("You have no permission!!!");
                return;
            }

            MemberWindow member = new MemberWindow();   
            member.Show();
        }

        private void Room_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button roomButton)
            {

                int roomNumber = int.Parse(roomButton.Tag.ToString());

                // Check the background color to determine the room's status
                if (roomButton.Background == Brushes.Green)
                {
                    // Open the BookRoomWindow
                    BookedRoomWindow bookedRoomWindow = new BookedRoomWindow(roomNumber);
                    bookedRoomWindow.ShowDialog();

                    // After booking, update the room status
                    UpdateRoomStatusDisplay();
                   
                }
                else if (roomButton.Background == Brushes.Red)
                {

                    string cuong = _service.GetGuestNameByRoomNumber(roomNumber);

                    RoomOptionsWindow optionsWindow = new RoomOptionsWindow();
                    optionsWindow.ShowDialog();

                    if (optionsWindow.Result == RoomOptionsWindow.RoomOptionResult.CheckOut)
                    {
                        CheckOutWindow checkOutWindow = new CheckOutWindow(roomNumber, cuong);
                        checkOutWindow.ShowDialog();
                    }
                    else if (optionsWindow.Result == RoomOptionsWindow.RoomOptionResult.BookedService)
                    {
                        CreateSer addServiceWindow = new CreateSer(roomNumber);
                        addServiceWindow.ShowDialog();
                    }

                    UpdateRoomStatusDisplay();

                }



            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             player.Open(new Uri("E:\\Sound\\separation-185196.mp3"));
            player.Play();
            UpdateRoomStatusDisplay();
        }
        private void UpdateRoomStatusDisplay()
        {
            var rooms = _roomService.GetAll(); // Get all rooms from the service

            foreach (var room in rooms)
            {
                var roomBorder = (Button)this.panelDesktop.FindName($"room{room.RoomNumber}");
                if (roomBorder != null)
                {
                    roomBorder.Background = room.RoomStatus == "Booked" ? Brushes.Red : Brushes.Green; // Update color based on status
                }
            }
        }

        private void invoiceListButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentAccount.Role != "Admin")
            {
                MessageBox.Show("You have no permission!!!");
                return;
            }
            ListInformation listInformation = new ListInformation();
            listInformation.ShowDialog();
        }
    }
}