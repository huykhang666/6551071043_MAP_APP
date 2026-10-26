using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyKhachSan.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan.SoDoPhong
{
    public partial class FormSoDoPhong : Form
    {
        private bool _dangKhoiTao;

        public FormSoDoPhong()
        {
            InitializeComponent();
        }

        private void FormSoDoPhong_Load(object sender, EventArgs e)
        {
            // Đặt trạng thái ban đầu của các control, trong lúc này chưa cho các sự kiện lọc chạy
            _dangKhoiTao = true;

            // Mỗi nhóm lọc mặc định chọn "Tất cả"
            rdbTatCaTrangThaiPhong.Checked = true;
            rdbTatCaLoaiPhong.Checked = true;
            rdbTatCaPhong.Checked = true;

            // Ô chọn ngày có thêm checkbox: tick mới lọc "phòng còn trống theo ngày", bỏ tick thì xem tất cả
            dtNgayChon.ShowCheckBox = true;
            dtNgayChon.Checked = false;

            // Gợi ý cho ô tìm kiếm
            toolTip1.SetToolTip(txtSearchMaPhong, "Ví dụ: 101, P20...");

            // Cấu hình lưới
            dataGridViewPhong.ReadOnly = true;
            dataGridViewPhong.AllowUserToAddRows = false;
            dataGridViewPhong.AllowUserToDeleteRows = false;
            dataGridViewPhong.MultiSelect = false;
            dataGridViewPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            _dangKhoiTao = false;
            TaiLaiDanhSach(); // Load dữ liệu khi form được mở
        }

        // ===== 13 radio button: cùng gọi LocTheoRadio, mọi tiêu chí được gom lại trong TaiLaiDanhSach() =====
        private void rdbTatCaPhong_CheckedChanged(object sender, EventArgs e) => LocTheoRadio(sender);
        private void rdbPhongChuaDon_CheckedChanged(object sender, EventArgs e) => LocTheoRadio(sender);
        private void rdbPhongDaDonDep_CheckedChanged(object sender, EventArgs e) => LocTheoRadio(sender);
        private void rdbTatCaLoaiPhong_CheckedChanged(object sender, EventArgs e) => LocTheoRadio(sender);
        private void rdbPhongVipDoi_CheckedChanged(object sender, EventArgs e) => LocTheoRadio(sender);
        private void rdbPhongVipDon_CheckedChanged(object sender, EventArgs e) => LocTheoRadio(sender);
        private void rdbPhongThuongDoi_CheckedChanged(object sender, EventArgs e) => LocTheoRadio(sender);
        private void rdbPhongThuongDon_CheckedChanged(object sender, EventArgs e) => LocTheoRadio(sender);
        private void rdbTatCaTrangThaiPhong_CheckedChanged(object sender, EventArgs e) => LocTheoRadio(sender);
        private void rdbPhongDangSuaChua_CheckedChanged(object sender, EventArgs e) => LocTheoRadio(sender);
        private void rdbPhongDangThue_CheckedChanged(object sender, EventArgs e) => LocTheoRadio(sender);
        private void rdbPhongDaDat_CheckedChanged(object sender, EventArgs e) => LocTheoRadio(sender);
        private void rdbPhongTrong_CheckedChanged(object sender, EventArgs e) => LocTheoRadio(sender);

        private void txtSearchMaPhong_TextChanged(object sender, EventArgs e)
        {
            if (_dangKhoiTao) return;
            TaiLaiDanhSach();
        }

        private void dtNgayChon_ValueChanged(object sender, EventArgs e)
        {
            if (_dangKhoiTao) return;
            TaiLaiDanhSach();
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            string? maPhong = MaPhongDangChon();
            MessageBox.Show(
                "Chức năng đặt phòng đang được xây dựng."
                + (maPhong != null ? Environment.NewLine + "Phòng đang chọn: " + maPhong : ""),
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ================= Các hàm hỗ trợ =================
        private void LocTheoRadio(object sender)
        {
            if (_dangKhoiTao) return;
            if (sender is RadioButton rdb && !rdb.Checked) return;
            TaiLaiDanhSach();
        }

        private string? TrangThaiPhongDangChon()
        {
            if (rdbPhongTrong.Checked) return "Bình thường";
            if (rdbPhongDaDat.Checked) return "Đã đặt";
            if (rdbPhongDangThue.Checked) return "Đang thuê";
            if (rdbPhongDangSuaChua.Checked) return "Đang sửa chữa";
            return null;
        }

        private string? MaLoaiPhongDangChon()
        {
            if (rdbPhongThuongDon.Checked) return "NOR01";
            if (rdbPhongThuongDoi.Checked) return "NOR02";
            if (rdbPhongVipDon.Checked) return "VIP01";
            if (rdbPhongVipDoi.Checked) return "VIP02";
            return null;
        }

        private string? TinhTrangDonDepDangChon()
        {
            if (rdbPhongDaDonDep.Checked) return "Đã dọn dẹp";
            if (rdbPhongChuaDon.Checked) return "Chưa dọn dẹp";
            return null;
        }

        private string? MaPhongDangChon()
        {
            return dataGridViewPhong.CurrentRow?.Cells["MaPH"].Value?.ToString();
        }

        private void TaiLaiDanhSach()
        {
            try
            {
                string? ttph = TrangThaiPhongDangChon();
                string? maLph = MaLoaiPhongDangChon();
                string? ttdd = TinhTrangDonDepDangChon();
                string maPhong = txtSearchMaPhong.Text.Trim();

                using var db = new HotelManagementContext();
                var query = db.Phongs.AsNoTracking().Where(p => p.DaXoa != true);

                if (ttph != null) query = query.Where(p => p.Ttph == ttph);
                if (maLph != null) query = query.Where(p => p.MaLph == maLph);
                if (ttdd != null) query = query.Where(p => p.Ttdd == ttdd);
                if (maPhong.Length > 0) query = query.Where(p => p.MaPh.Contains(maPhong));

                if (dtNgayChon.Checked)
                {
                    DateTime ngayChon = dtNgayChon.Value.Date;
                    query = query.Where(p => p.Ttph == "Bình thường"
                        && !p.Ctdps.Any(c => c.DaXoa != true
                            && ngayChon >= c.CheckIn.Date
                            && ngayChon < c.CheckOut.Date));
                }

                var dsPhong = query
                    .OrderBy(p => p.MaPh)
                    .Select(p => new
                    {
                        p.MaPh,
                        TenLph = p.MaLphNavigation.TenLph,
                        p.Ttph,
                        p.Ttdd,
                        p.GhiChu
                    })
                    .ToList();

                dataGridViewPhong.DataSource = dsPhong
                    .Select(p => new
                    {
                        MaPH = p.MaPh,
                        Tang = p.MaPh.Length > 1 ? p.MaPh.Substring(1, 1) : "",
                        TenLPH = p.TenLph,
                        TTPH = p.Ttph,
                        TTDD = p.Ttdd,
                        GhiChu = p.GhiChu
                    })
                    .ToList();

                DatTieuDeCot("MaPH", "Mã phòng");
                DatTieuDeCot("Tang", "Tầng");
                DatTieuDeCot("TenLPH", "Loại phòng");
                DatTieuDeCot("TTPH", "Trạng thái phòng");
                DatTieuDeCot("TTDD", "Tình trạng dọn dẹp");
                DatTieuDeCot("GhiChu", "Ghi chú");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phòng: " + ex.Message);
            }
        }

        private void DatTieuDeCot(string tenCot, string tieuDe)
        {
            if (dataGridViewPhong.Columns.Contains(tenCot))
                dataGridViewPhong.Columns[tenCot]!.HeaderText = tieuDe;
        }
    }
}
