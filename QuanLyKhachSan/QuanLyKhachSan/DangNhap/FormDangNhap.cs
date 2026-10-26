using QuanLyKhachSan.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan.DangNhap
{
    public partial class FormDangNhap : Form
    {
        // Thông tin phiên đăng nhập – các form khác đọc trực tiếp
        public static string MaNV_DangNhap = string.Empty;
        public static string TenNV_DangNhap = string.Empty;
        public static int CapDoQuyen_DangNhap; // 1: Nhân viên, 2: Quản lý, 3: Admin

        public FormDangNhap()
        {
            InitializeComponent();
            ActiveControl = txtTenDangNhap; // Focus vào ô Tên đăng nhập khi mở form
        }

        private async void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            if (tenDangNhap.Length == 0 || matKhau.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnDangNhap.Enabled = false; // Tránh bấm nhiều lần khi đang truy vấn CSDL
            try
            {
                bool hopLe = await KiemTraDangNhapAsync(tenDangNhap, matKhau);
                if (hopLe)
                {
                    DialogResult = DialogResult.OK; // Program.cs sẽ mở form chính
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu.", "Đăng nhập thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnDangNhap.Enabled = true;
            }
        }

        private async Task<bool> KiemTraDangNhapAsync(string tenDangNhap, string matKhau)
        {
            await using var db = new HotelManagementContext();

            // Lấy tài khoản + tên nhân viên qua navigation property
            // DaXoa là bool? nên dùng != true để nhận cả false lẫn null
            var tk = await db.TaiKhoans
                .AsNoTracking()
                .Where(t => t.TenTk == tenDangNhap && t.DaXoa != true)
                .Select(t => new
                {
                    t.Password,
                    t.MaNv,
                    t.CapDoQuyen,
                    TenNv = t.MaNvNavigation.TenNv,
                    NhanVienDaXoa = t.MaNvNavigation.DaXoa
                })
                .FirstOrDefaultAsync();

            if (tk == null || tk.NhanVienDaXoa == true)
                return false;

            // So sánh mật khẩu phân biệt hoa thường bằng C#
            if (!string.Equals(tk.Password, matKhau, StringComparison.Ordinal))
                return false;

            MaNV_DangNhap = tk.MaNv;
            TenNV_DangNhap = tk.TenNv;
            CapDoQuyen_DangNhap = tk.CapDoQuyen;
            Tag = tk.TenNv;

            return true;
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            // Nhấn phím Enter ở ô mật khẩu sẽ tự kích hoạt nút Đăng nhập
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Bỏ tiếng "ding" của Windows
                btnDangNhap.PerformClick();
            }
        }
    }
}