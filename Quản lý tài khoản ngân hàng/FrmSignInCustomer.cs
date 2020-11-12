using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_lý_tài_khoản_ngân_hàng
{
    public partial class FrmSignInCustomer : Form
    {
        private string connectionString = @"Data Source=DESKTOP-78M9MAQ\SQLEXPRESS;Initial Catalog=dbo_qltaikhoan;Integrated Security=True";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        Form1 f1;
        public FrmSignInCustomer()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string query = "select taikhoan_ten, taikhoan_matkhau, dadong, tbl_taikhoan.ghichu from tbl_taikhoan, tbl_khachhang where tbl_taikhoan.soTK = tbl_khachhang.soTK and taikhoan_ten = N'" + txtUsername.Text + "' and taikhoan_matkhau = N'" + txtPassword.Text + "'";
                using (cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            Boolean active = Convert.ToBoolean(row["dadong"]);
                            if (active == true)
                            {
                                MessageBox.Show(row["ghichu"].ToString());
                            }
                            else
                            {
                                f1 = new Form1();
                                f1.Show();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên tài khoản hoặc mật khẩu không hợp lệ");
                    }
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
    }
}
