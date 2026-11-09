using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClosedXML.Excel;
using QuanLyKhachSan.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan.ThongKe
{
    public partial class FormThongKe : Form
    {
        private readonly CultureInfo viVN = new CultureInfo("vi-VN");

        public FormThongKe()
        {
            InitializeComponent();
        }

        private void FormThongKe_Load(object? sender, EventArgs e)
        {
            // Mặc định khoảng thời gian: Đầu tháng này đến hôm nay
            DateTime homNay = DateTime.Today;
            dtpTuNgay.Value = new DateTime(homNay.Year, homNay.Month, 1);
            dtpDenNgay.Value = homNay;

            CauHinhBangDuLieu();
            NapThongKe();
        }

        private void CauHinhBangDuLieu()
        {
            // Bảng loại phòng
            dtgDoanhThuLoaiPhong.AutoGenerateColumns = false;
            dtgDoanhThuLoaiPhong.RowHeadersVisible = false;
            dtgDoanhThuLoaiPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgDoanhThuLoaiPhong.Columns.Clear();
            dtgDoanhThuLoaiPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenLPH",
                DataPropertyName = "TenLPH",
                HeaderText = "Loại phòng",
                FillWeight = 40
            });
            dtgDoanhThuLoaiPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoLuot",
                DataPropertyName = "SoLuot",
                HeaderText = "Lượt thuê",
                FillWeight = 25
            });
            var colTienLP = new DataGridViewTextBoxColumn
            {
                Name = "DoanhThu",
                DataPropertyName = "DoanhThu",
                HeaderText = "Doanh thu",
                FillWeight = 35
            };
            colTienLP.DefaultCellStyle.Format = "N0";
            colTienLP.DefaultCellStyle.FormatProvider = viVN;
            colTienLP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgDoanhThuLoaiPhong.Columns.Add(colTienLP);

            // Bảng Top Dịch vụ
            dtgTopDichVu.AutoGenerateColumns = false;
            dtgTopDichVu.RowHeadersVisible = false;
            dtgTopDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgTopDichVu.Columns.Clear();

            var colHuyChuong = new DataGridViewImageColumn
            {
                Name = "HuyChuong",
                HeaderText = "Hạng",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dtgTopDichVu.Columns.Add(colHuyChuong);

            dtgTopDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenDV",
                DataPropertyName = "TenDV",
                HeaderText = "Tên dịch vụ",
                FillWeight = 40
            });
            dtgTopDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoLuong",
                DataPropertyName = "SoLuong",
                HeaderText = "SL dùng",
                FillWeight = 25
            });
            var colTienDV = new DataGridViewTextBoxColumn
            {
                Name = "TongTien",
                DataPropertyName = "TongTien",
                HeaderText = "Tổng thu",
                FillWeight = 35
            };
            colTienDV.DefaultCellStyle.Format = "N0";
            colTienDV.DefaultCellStyle.FormatProvider = viVN;
            colTienDV.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgTopDichVu.Columns.Add(colTienDV);

            // Bảng Chi tiết hóa đơn
            dtgChiTietHoaDon.AutoGenerateColumns = false;
            dtgChiTietHoaDon.RowHeadersVisible = false;
            dtgChiTietHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgChiTietHoaDon.Columns.Clear();
            dtgChiTietHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaHD",
                DataPropertyName = "MaHD",
                HeaderText = "Mã HĐ",
                FillWeight = 12
            });
            var colNgay = new DataGridViewTextBoxColumn
            {
                Name = "NgayHD",
                DataPropertyName = "NgayHD",
                HeaderText = "Ngày lập",
                FillWeight = 14
            };
            colNgay.DefaultCellStyle.Format = "dd/MM/yyyy";
            dtgChiTietHoaDon.Columns.Add(colNgay);
            dtgChiTietHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenKH",
                DataPropertyName = "TenKH",
                HeaderText = "Khách hàng",
                FillWeight = 22
            });
            dtgChiTietHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Phong",
                DataPropertyName = "Phong",
                HeaderText = "Phòng",
                FillWeight = 12
            });
            var colTienP = new DataGridViewTextBoxColumn
            {
                Name = "TienPhong",
                DataPropertyName = "TienPhong",
                HeaderText = "Tiền phòng",
                FillWeight = 20
            };
            colTienP.DefaultCellStyle.Format = "N0";
            colTienP.DefaultCellStyle.FormatProvider = viVN;
            colTienP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgChiTietHoaDon.Columns.Add(colTienP);

            var colTienDichVu = new DataGridViewTextBoxColumn
            {
                Name = "TienDichVu",
                DataPropertyName = "TienDichVu",
                HeaderText = "Tiền dịch vụ",
                FillWeight = 20
            };
            colTienDichVu.DefaultCellStyle.Format = "N0";
            colTienDichVu.DefaultCellStyle.FormatProvider = viVN;
            colTienDichVu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgChiTietHoaDon.Columns.Add(colTienDichVu);

            var colTong = new DataGridViewTextBoxColumn
            {
                Name = "TongCong",
                DataPropertyName = "TongCong",
                HeaderText = "Tổng thanh toán",
                FillWeight = 20
            };
            colTong.DefaultCellStyle.Format = "N0";
            colTong.DefaultCellStyle.FormatProvider = viVN;
            colTong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgChiTietHoaDon.Columns.Add(colTong);
        }

        private void btnThongKe_Click(object? sender, EventArgs e)
        {
            NapThongKe();
        }

        private void NapThongKe()
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Từ ngày không thể lớn hơn đến ngày!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var db = new HotelManagementContext();

                // Lấy tất cả hóa đơn trong khoảng thời gian đã chọn
                var danhSachHD = db.HoaDons
                    .AsNoTracking()
                    .Where(h => h.DaXoa != true && h.NgHd >= tuNgay && h.NgHd <= denNgay)
                    .Select(h => new
                    {
                        h.MaHd,
                        h.NgHd,
                        h.MaCtdp,
                        TenKH = h.MaCtdpNavigation.MaPtNavigation.MaKhNavigation.TenKh,
                        MaPH = h.MaCtdpNavigation.MaPh,
                        TenLPH = h.MaCtdpNavigation.MaPhNavigation.MaLphNavigation.TenLph,
                        TienPhong = h.MaCtdpNavigation.ThanhTien ?? 0,
                        TienDichVu = h.MaCtdpNavigation.Ctdvs
                            .Where(c => c.DaXoa != true)
                            .Sum(c => (decimal?)c.MaDvNavigation.DonGia * c.Sl) ?? 0
                    })
                    .ToList();

                decimal tongTienPhong = danhSachHD.Sum(h => h.TienPhong);
                decimal tongTienDV = danhSachHD.Sum(h => h.TienDichVu);
                decimal tongDoanhThu = tongTienPhong + tongTienDV;
                int soLuotThue = danhSachHD.Count;

                lblCardTienPhong.Text = tongTienPhong.ToString("N0", viVN) + " VND";
                lblCardTienDV.Text = tongTienDV.ToString("N0", viVN) + " VND";
                lblCardTongTien.Text = tongDoanhThu.ToString("N0", viVN) + " VND";
                lblCardSoPhong.Text = soLuotThue + " lượt";

                // Thống kê theo loại phòng
                var dsLoaiPhong = danhSachHD
                    .GroupBy(h => h.TenLPH ?? "Khác")
                    .Select(g => new
                    {
                        TenLPH = g.Key,
                        SoLuot = g.Count(),
                        DoanhThu = g.Sum(h => h.TienPhong)
                    })
                    .OrderByDescending(x => x.DoanhThu)
                    .ToList();

                dtgDoanhThuLoaiPhong.DataSource = dsLoaiPhong;

                // Thống kê Top dịch vụ
                var dsMaCtdp = danhSachHD.Select(h => h.MaCtdp).Distinct().ToList();
                var topDV = db.Ctdvs
                    .AsNoTracking()
                    .Where(c => c.DaXoa != true && dsMaCtdp.Contains(c.MaCtdp))
                    .GroupBy(c => new { c.MaDv, c.MaDvNavigation.TenDv, c.MaDvNavigation.DonGia })
                    .Select(g => new
                    {
                        TenDV = g.Key.TenDv,
                        SoLuong = g.Sum(c => c.Sl),
                        TongTien = g.Sum(c => c.Sl * g.Key.DonGia)
                    })
                    .OrderByDescending(x => x.SoLuong)
                    .Take(10)
                    .ToList();

                dtgTopDichVu.Rows.Clear();
                int hang = 1;
                foreach (var item in topDV)
                {
                    Image? iconHang = null;
                    if (hang == 1) iconHang = Properties.Resources.top1;
                    else if (hang == 2) iconHang = Properties.Resources.top2;
                    else if (hang == 3) iconHang = Properties.Resources.top3;

#pragma warning disable CS8604
                    dtgTopDichVu.Rows.Add(iconHang, item.TenDV, item.SoLuong, item.TongTien);
#pragma warning restore CS8604
                    hang++;
                }

                // Chi tiết hóa đơn
                var chiTietList = danhSachHD.Select(h => new
                {
                    MaHD = h.MaHd,
                    NgayHD = h.NgHd,
                    TenKH = h.TenKH,
                    Phong = h.MaPH,
                    TienPhong = h.TienPhong,
                    TienDichVu = h.TienDichVu,
                    TongCong = h.TienPhong + h.TienDichVu
                }).ToList();

                dtgChiTietHoaDon.DataSource = chiTietList;

                dtgDoanhThuLoaiPhong.ClearSelection();
                dtgTopDichVu.ClearSelection();
                dtgChiTietHoaDon.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính toán thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatExcel_Click(object? sender, EventArgs e)
        {
            if (dtgChiTietHoaDon.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu thống kê để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var saveDialog = new SaveFileDialog
            {
                Title = "Xuất báo cáo thống kê",
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = $"BaoCaoThongKe_{dtpTuNgay.Value:yyyyMMdd}_{dtpDenNgay.Value:yyyyMMdd}.xlsx"
            };

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                using var workbook = new XLWorkbook();

                // Sheet 1: Tổng quan & Chi tiết
                var sheet1 = workbook.Worksheets.Add("Chi tiết doanh thu");

                sheet1.Cell(1, 1).Value = "BÁO CÁO THỐNG KÊ DOANH THU KHÁCH SẠN";
                sheet1.Cell(1, 1).Style.Font.Bold = true;
                sheet1.Cell(1, 1).Style.Font.FontSize = 16;
                sheet1.Range(1, 1, 1, 7).Merge();

                sheet1.Cell(2, 1).Value = $"Từ ngày: {dtpTuNgay.Value:dd/MM/yyyy}  -  Đến ngày: {dtpDenNgay.Value:dd/MM/yyyy}";
                sheet1.Cell(2, 1).Style.Font.Italic = true;
                sheet1.Range(2, 1, 2, 7).Merge();

                sheet1.Cell(4, 1).Value = "Doanh thu phòng:";
                sheet1.Cell(4, 2).Value = lblCardTienPhong.Text;
                sheet1.Cell(4, 4).Value = "Doanh thu dịch vụ:";
                sheet1.Cell(4, 5).Value = lblCardTienDV.Text;

                sheet1.Cell(5, 1).Value = "Tổng doanh thu:";
                sheet1.Cell(5, 2).Value = lblCardTongTien.Text;
                sheet1.Cell(5, 4).Value = "Số lượt thuê phòng:";
                sheet1.Cell(5, 5).Value = lblCardSoPhong.Text;

                sheet1.Range(4, 1, 5, 5).Style.Font.Bold = true;

                int rowStart = 7;
                string[] headers = { "Mã HĐ", "Ngày lập", "Khách hàng", "Phòng", "Tiền phòng", "Tiền dịch vụ", "Tổng cộng" };
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = sheet1.Cell(rowStart, i + 1);
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.LightBlue;
                }

                int r = rowStart + 1;
                foreach (DataGridViewRow row in dtgChiTietHoaDon.Rows)
                {
                    sheet1.Cell(r, 1).Value = row.Cells["MaHD"].Value?.ToString() ?? "";
                    if (row.Cells["NgayHD"].Value is DateTime dt)
                    {
                        sheet1.Cell(r, 2).Value = dt;
                        sheet1.Cell(r, 2).Style.DateFormat.Format = "dd/MM/yyyy";
                    }
                    sheet1.Cell(r, 3).Value = row.Cells["TenKH"].Value?.ToString() ?? "";
                    sheet1.Cell(r, 4).Value = row.Cells["Phong"].Value?.ToString() ?? "";
                    sheet1.Cell(r, 5).Value = Convert.ToDecimal(row.Cells["TienPhong"].Value ?? 0);
                    sheet1.Cell(r, 5).Style.NumberFormat.Format = "#,##0";
                    sheet1.Cell(r, 6).Value = Convert.ToDecimal(row.Cells["TienDichVu"].Value ?? 0);
                    sheet1.Cell(r, 6).Style.NumberFormat.Format = "#,##0";
                    sheet1.Cell(r, 7).Value = Convert.ToDecimal(row.Cells["TongCong"].Value ?? 0);
                    sheet1.Cell(r, 7).Style.NumberFormat.Format = "#,##0";
                    r++;
                }

                sheet1.Columns().AdjustToContents();

                // Sheet 2: Doanh thu theo loại phòng
                var sheet2 = workbook.Worksheets.Add("Doanh thu loại phòng");
                sheet2.Cell(1, 1).Value = "Loại phòng";
                sheet2.Cell(1, 2).Value = "Số lượt thuê";
                sheet2.Cell(1, 3).Value = "Doanh thu phòng";
                sheet2.Range(1, 1, 1, 3).Style.Font.Bold = true;
                sheet2.Range(1, 1, 1, 3).Style.Fill.BackgroundColor = XLColor.LightGreen;

                int r2 = 2;
                foreach (DataGridViewRow row in dtgDoanhThuLoaiPhong.Rows)
                {
                    sheet2.Cell(r2, 1).Value = row.Cells["TenLPH"].Value?.ToString() ?? "";
                    sheet2.Cell(r2, 2).Value = Convert.ToInt32(row.Cells["SoLuot"].Value ?? 0);
                    sheet2.Cell(r2, 3).Value = Convert.ToDecimal(row.Cells["DoanhThu"].Value ?? 0);
                    sheet2.Cell(r2, 3).Style.NumberFormat.Format = "#,##0";
                    r2++;
                }
                sheet2.Columns().AdjustToContents();

                workbook.SaveAs(saveDialog.FileName);

                DialogResult moFile = MessageBox.Show(
                    "Xuất báo cáo thống kê thành công!\nBạn có muốn mở file vừa xuất không?",
                    "Thông báo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (moFile == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(saveDialog.FileName)
                    {
                        UseShellExecute = true
                    });
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Không thể ghi file. Có thể file đang được mở trong Excel.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
