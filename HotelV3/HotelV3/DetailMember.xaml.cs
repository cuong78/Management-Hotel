using BLL.Service;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

namespace AppHotel
{
    /// <summary>
    /// Interaction logic for DetailMember.xaml
    /// </summary>
    public partial class DetailMember : Window
    {
        private MemberService _service = new MemberService();

        public Member? EdiEditedOne { get; set; }

        public DetailMember()
        {
            InitializeComponent();
        }
       

       



        private bool IsValidString(string str, string error)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                ShowStringErrorNotification(error);
                return false;
            }

            return true;
        }

        private bool IsValidPhone(string str)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text, @"^\d{10}$"))
            {
                ShowPhoneErrorNotification();


                return false;

            }

            return true;
        }

        private bool IsValidRole()
        {
            if (cmbRole.SelectedItem == null)
            {
                ShowRoleErrorNotification();
                return false;
            }
            return true;
        }

        private void ShowStringErrorNotification(string error)
        {
            MessageBox.Show($"{error} cant be empty", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ShowPhoneErrorNotification()
        {
            MessageBox.Show($"Phone is not Valid", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ShowRoleErrorNotification()
        {
            MessageBox.Show($"Please select a role to continue", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValidString(txtName.Text, "Name") ||
                !IsValidString(txtEmail.Text, "Email") ||
                !IsValidString(txtPassword.Password, "Password") ||
                !IsValidString(txtPhone.Text, "Phone") ||               
                !IsValidString(txtAddress.Text, "Address") ||
                !IsValidRole())
            {
                return;
            }
            string email = txtEmail.Text;
            if(!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            if (EdiEditedOne == null)
            {
                Member member = new Member();
                member.Name = txtName.Text;
                member.Email = txtEmail.Text;
                member.Password = txtPassword.Password;
                member.Phone = txtPhone.Text;
                member.Address = txtAddress.Text;
                member.Role = cmbRole.SelectedValue.ToString();
                member.Status = chkStatus.IsChecked == true ? 1 : 0;

                _service.AddMember(member);
                MessageBox.Show("Member Create successfully.");
                this.Close();
            }
            else
            {
                EdiEditedOne.Name = txtName.Text;
                EdiEditedOne.Email = txtEmail.Text;
                EdiEditedOne.Password = txtPassword.Password;
                EdiEditedOne.Phone = txtPhone.Text;
                EdiEditedOne.Address = txtAddress.Text;
                EdiEditedOne.Role = cmbRole.SelectedValue.ToString();
                EdiEditedOne.Status = chkStatus.IsChecked == true ? 1 : 0;

                _service.Update(EdiEditedOne);
                MessageBox.Show("Member Update successfully.");
                this.Close();
            }
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbRole.ItemsSource = FillCmbRole();

            if (EdiEditedOne != null)
            {
                txtID.Text = EdiEditedOne.MemberId.ToString();
                txtID.IsEnabled = false;
                txtName.Text = EdiEditedOne.Name;
                txtEmail.Text = EdiEditedOne.Email;
                txtPassword.Password = EdiEditedOne.Password;
                txtPhone.Text = EdiEditedOne.Phone;
                txtAddress.Text = EdiEditedOne.Address;
                cmbRole.Text = EdiEditedOne.Role;
                chkStatus.IsChecked = EdiEditedOne.Status == 1;
                return;
            }

            txtID.IsEnabled = false;
        }

        private List<string> FillCmbRole()
        {
            List<string> roles = new();
            roles.Add("Admin");
            roles.Add("Staff");
            return roles;
        }

        private void SuggestionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (suggestionList.SelectedItem is Member selectedMember)
            {
                // Populate fields with the selected member's details
                txtID.Text = selectedMember.MemberId.ToString();
                txtID.IsEnabled = false; // Prevent editing the ID
                txtName.Text = selectedMember.Name;
                txtEmail.Text = selectedMember.Email;
                txtPassword.Password = selectedMember.Password; // Consider whether you want to show this
                txtPhone.Text = selectedMember.Phone;
                txtAddress.Text = selectedMember.Address;
                cmbRole.Text = selectedMember.Role;
                chkStatus.IsChecked = selectedMember.Status == 1;

                suggestionList.Visibility = Visibility.Collapsed; // Hide suggestions after selection
            }
        }
    

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string nameInput = txtName.Text;
            if (!string.IsNullOrWhiteSpace(nameInput))
            {
                var suggestions = _service.GetMembersByName(nameInput);
                UpdateSuggestionList(suggestions);
            }
            else
            {
                suggestionList.Visibility = Visibility.Collapsed;
            }
        }

        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            string phoneInput = txtPhone.Text;
            if (!string.IsNullOrWhiteSpace(phoneInput))
            {
                var suggestions = _service.GetMembersByPhone(phoneInput);
                UpdateSuggestionList(suggestions);
            }
            else
            {
                suggestionList.Visibility = Visibility.Collapsed;
            }
        }
        private void UpdateSuggestionList(List<Member> suggestions)
        {
            suggestionList.ItemsSource = suggestions;
            suggestionList.Visibility = suggestions.Any() ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
