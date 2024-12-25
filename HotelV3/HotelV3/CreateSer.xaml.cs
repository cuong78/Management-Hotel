using BLL.Service;
using DAL.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HotelV3
{
    public partial class CreateSer : Window
    {
        private ServiceDetailService _servicesService = new();
        private int _memberRoom;

        public ObservableCollection<ServiceItem> ServiceItems { get; set; }

        public CreateSer()
        {
            InitializeComponent();
            ServiceItems = new ObservableCollection<ServiceItem>();
            DataContext = this; // Set DataContext here
        }

        public CreateSer(int room) : this()
        {
            _memberRoom = room;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillServiceList();
        }

        private void FillServiceList()
        {
            var services = _servicesService.GetAllService();
            foreach (var service in services)
            {
                ServiceItems.Add(new ServiceItem { NameService = service.NameService, Quantity = 0 });
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in ServiceItems)
            {
                if (item.Quantity > 0)
                {
                    var bookedService = new BookedService
                    {
                        DateCreated = DateTime.Now,
                        Quantity = item.Quantity,
                        TaskId = 1, // Adjust the TaskId as needed
                        RoomNumber = _memberRoom,
                        NameService = item.NameService
                    };

                    _servicesService.saveService(bookedService);
                }
            }

            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class ServiceItem
    {
        public string NameService { get; set; }
        public int Quantity { get; set; }
    }
}