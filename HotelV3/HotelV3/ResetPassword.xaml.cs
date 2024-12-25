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
using System.Data;
using System.Data.SqlClient;
using BLL.Service;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
namespace HotelV3
{
    /// <summary>
    /// Interaction logic for ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Window
    {
        private MemberService _memberService = new();
        private string _email; 
        public ResetPassword(string email)
        {
            InitializeComponent();
            _email = email; // Lưu lại email khi chuyển qua cửa sổ ResetPassword
            _memberService = new MemberService();
        }

        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem mật khẩu mới có hợp lệ không
            if (string.IsNullOrEmpty(newPasswordBox.Text))
            {
                MessageBox.Show("Please enter a new password.");
                return;
            }

            // Cập nhật mật khẩu mới
            var member = _memberService.GetMemberByEmail(_email); 
            if (member != null)
            {
                member.Password = newPasswordBox.Text; // Cập nhật mật khẩu mới
                _memberService.Update(member); // Cập nhật vào cơ sở dữ liệu

                MessageBox.Show("Password updated successfully.");
                this.Close(); // Đóng cửa sổ ResetPassword
            }
            else
            {
                MessageBox.Show("Member not found.");
            }

        }
    }
}
