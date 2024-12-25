using AppHotel;
using BLL.Service;
using DAL.Models;
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
    /// Interaction logic for MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {
        // da chinh sua
        private MemberService _memberService = new();
        public MemberWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = txtSearchName.Text;
            string role = cmbRoleFilter.Text;

            if (role == "All")
            {
                role = "";
            }

            List<Member> member = _memberService.Search(name, role);

            if (member.Count == 0)
            {
                MessageBox.Show("have no member", "notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dgMembers.ItemsSource = member;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DetailMember member = new DetailMember();
            member.ShowDialog();
            FillData();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Member? selected = dgMembers.SelectedItem as Member;

            if (selected == null)
            {
                MessageBox.Show("Please select an air-con before updating", "Select one", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DetailMember detail = new();
            detail.EdiEditedOne = selected;
            detail.ShowDialog();
            FillData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Member? selected = dgMembers.SelectedItem as Member;

            if (selected == null)
            {
                MessageBox.Show("Please select an air-con before deleting", "Select one", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult answer = MessageBox.Show("Do You want to delete?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
                return;

            _memberService.Remove(selected);
            FillData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillData();
        }

        private void FillData()
        {
            cmbRoleFilter.ItemsSource = FillCmbRole();
            dgMembers.ItemsSource = null;
            dgMembers.ItemsSource = _memberService.GetAllMembers();
        }

        private List<string> FillCmbRole()
        {
            List<string> roles = new();
            roles.Add("All");
            roles.Add("Admin");
            roles.Add("Staff");
            return roles;
        }


    }
}
