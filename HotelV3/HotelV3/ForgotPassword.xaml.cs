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
using System.Net;
using System.Net.Mail;
using System.Security.RightsManagement;
using BLL.Service;

namespace HotelV3
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        string randomCode;
        public static string to;

        private MemberService _service = new();
       
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = emailTextBox.Text;

            if (!_service.IsEmailExist(email))
            {
                MessageBox.Show("Email không tồn tại");
                return;
            }

            string from = "sinhviengadon@gmail.com";
            string pass = "pfzb kvin tnir zsog";
            randomCode = new Random().Next(100000, 999999).ToString();
            string messageBody = "Your reset code is " + randomCode;

            using (MailMessage message = new MailMessage())
            {
                message.To.Add(email);
                message.From = new MailAddress(from);
                message.Body = messageBody;
                message.Subject = "Password Reset Code";

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(from, pass);

                    try
                    {
                        smtp.Send(message);
                        MessageBox.Show("Code sent successfully: ");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (randomCode == (codeTextBox.Text).ToString())
            {
                to = emailTextBox.Text;
                ResetPassword rp = new ResetPassword(to);
                this.Hide();
                rp.ShowDialog();
        
            }
            else
            {
                MessageBox.Show("wrong code");
            }
        }
    }
}
