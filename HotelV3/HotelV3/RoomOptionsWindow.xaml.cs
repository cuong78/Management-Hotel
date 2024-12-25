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
    /// Interaction logic for RoomOptionsWindow.xaml
    /// </summary>
    public partial class RoomOptionsWindow : Window
    {
        public enum RoomOptionResult
        {
            CheckOut,
            BookedService,
            Cancel
        }

        public RoomOptionResult Result { get; private set; } = RoomOptionResult.Cancel;

        public RoomOptionsWindow()
        {
            InitializeComponent();
        }

        private void CheckOutButton_Click(object sender, RoutedEventArgs e)
        {
            Result = RoomOptionResult.CheckOut;
            this.Close();
        }

        private void BookedServiceButton_Click(object sender, RoutedEventArgs e)
        {
            Result = RoomOptionResult.BookedService;
            this.Close();
        }
    }
}
