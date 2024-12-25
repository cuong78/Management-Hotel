using DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Interaction logic for ListInformation.xaml
    /// </summary>
    public partial class ListInformation : Window
    {
        private string _connectionString;
        public ListInformation()
        {
            InitializeComponent();
            _connectionString = GetConnectionString();
        }
        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

            return strConn;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection c = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("select * from BookedList", c);
                SqlDataAdapter d = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                d.Fill(dt);
                dataTextGird.ItemsSource = dt.DefaultView;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {// Lấy dữ liệu từ các ô đã chọn trong DataGrid
         // Lấy dữ liệu từ các ô đã chọn trong DataGrid
            StringBuilder clipboardContent = new StringBuilder();
            dataTextGird.SelectAll();

            // Mở ứng dụng Excel
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            xlapp.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook xlwbook;
            Microsoft.Office.Interop.Excel.Worksheet xlsheet;
            object misedata = System.Reflection.Missing.Value;
            xlwbook = xlapp.Workbooks.Add(misedata);
            xlsheet = (Microsoft.Office.Interop.Excel.Worksheet)xlwbook.Worksheets.get_Item(1);

            // Duyệt qua các hàng đã chọn và thêm vào Excel
            int rowIndex = 1; // Excel bắt đầu từ hàng 1
            foreach (var row in dataTextGird.SelectedItems)
            {
                // Chuyển đối tượng thành DataRowView để truy cập dữ liệu của mỗi hàng
                var dataRow = row as DataRowView;
                if (dataRow != null)
                {
                    List<string> rowData = new List<string>();

                    // Duyệt qua các cột trong DataRow và thêm vào rowData
                    int columnIndex = 1; // Excel bắt đầu từ cột 1
                    foreach (DataColumn column in dataRow.Row.Table.Columns)
                    {
                        var cellValue = dataRow.Row[column].ToString();
                        xlsheet.Cells[rowIndex, columnIndex] = cellValue; // Gán giá trị cho ô Excel

                        // Kiểm tra nếu là kiểu thời gian và áp dụng định dạng ngày giờ
                        if (DateTime.TryParse(cellValue, out DateTime dateValue))
                        {
                            // Định dạng thời gian cho ô này
                            xlsheet.Cells[rowIndex, columnIndex].NumberFormat = "mm/dd/yyyy hh:mm:ss AM/PM";
                        }

                        columnIndex++;
                    }

                    rowIndex++;
                }
            }

            // Tự động điều chỉnh chiều rộng các cột trong Excel để phù hợp với nội dung
            xlsheet.Columns.AutoFit();

            // Hiển thị kết quả
            xlapp.Visible = true;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }
    }
}
