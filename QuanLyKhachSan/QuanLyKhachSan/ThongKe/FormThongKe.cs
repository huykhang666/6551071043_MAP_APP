using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
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

        // Dữ liệu phục vụ vẽ biểu đồ GDI+
        private sealed class ThongKeLoaiPhongItem
        {
            public string TenLPH { get; set; } = "";
            public int SoLuot { get; set; }
            public decimal DoanhThu { get; set; }
        }

        private sealed class ThongKeDichVuItem
        {
            public string TenDV { get; set; } = "";
            public int SoLuong { get; set; }
            public decimal TongTien { get; set; }
        }

        private List<ThongKeLoaiPhongItem> _duLieuLoaiPhong = new List<ThongKeLoaiPhongItem>();
        private List<ThongKeDichVuItem> _duLieuTopDV = new List<ThongKeDichVuItem>();

        // Bảng màu cho biểu đồ
        private readonly Color[] _mauBieuDo = new[]
        {
            Color.FromArgb(78, 115, 223),  // Xanh dương
            Color.FromArgb(28, 200, 138),  // Xanh ngọc
            Color.FromArgb(54, 185, 204),  // Xanh da trời
            Color.FromArgb(246, 194, 62),  // Vàng hổ phách
            Color.FromArgb(231, 74, 59),   // Đỏ cam
            Color.FromArgb(111, 66, 193),  // Tím
            Color.FromArgb(253, 126, 20),  // Cam
            Color.FromArgb(108, 117, 125)  // Xám
        };

        public FormThongKe()
        {
            InitializeComponent();

            pnlChartLoaiPhong.Resize += (s, e) => pnlChartLoaiPhong.Invalidate();
            pnlChartTopDV.Resize += (s, e) => pnlChartTopDV.Invalidate();
        }

        private void FormThongKe_Load(object? sender, EventArgs e)
        {
            // Tự động tìm ngày hóa đơn đầu tiên trong CSDL để mặc định khoảng thời gian hiển thị toàn bộ dữ liệu mẫu
            try
            {
                using var db = new HotelManagementContext();
                var minNgay = db.HoaDons
                    .Where(h => h.DaXoa != true && h.NgHd != null)
                    .Select(h => h.NgHd)
                    .Min();

                dtpTuNgay.Value = minNgay?.Date ?? new DateTime(2022, 1, 1);
            }
            catch
            {
                dtpTuNgay.Value = new DateTime(2022, 1, 1);
            }

            dtpDenNgay.Value = DateTime.Today;

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
                _duLieuLoaiPhong = danhSachHD
                    .GroupBy(h => h.TenLPH ?? "Khác")
                    .Select(g => new ThongKeLoaiPhongItem
                    {
                        TenLPH = g.Key,
                        SoLuot = g.Count(),
                        DoanhThu = g.Sum(h => h.TienPhong)
                    })
                    .OrderByDescending(x => x.DoanhThu)
                    .ToList();

                dtgDoanhThuLoaiPhong.DataSource = _duLieuLoaiPhong;

                // Thống kê Top dịch vụ
                var dsMaCtdp = danhSachHD.Select(h => h.MaCtdp).Distinct().ToList();
                _duLieuTopDV = db.Ctdvs
                    .AsNoTracking()
                    .Where(c => c.DaXoa != true && dsMaCtdp.Contains(c.MaCtdp))
                    .GroupBy(c => new { c.MaDv, c.MaDvNavigation.TenDv, c.MaDvNavigation.DonGia })
                    .Select(g => new ThongKeDichVuItem
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
                foreach (var item in _duLieuTopDV)
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

                // Vẽ lại 2 biểu đồ GDI+ với dữ liệu mới
                pnlChartLoaiPhong.Invalidate();
                pnlChartTopDV.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính toán thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // VẼ BIỂU ĐỒ TRÒN (PIE/DONUT CHART) BẰNG GDI+
        // ==========================================
        private void pnlChartLoaiPhong_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            int pWidth = pnlChartLoaiPhong.Width;
            int pHeight = pnlChartLoaiPhong.Height;

            if (_duLieuLoaiPhong.Count == 0 || _duLieuLoaiPhong.All(x => x.DoanhThu <= 0))
            {
                using var fontThongBao = new Font("Segoe UI", 12F, FontStyle.Italic);
                using var brushThongBao = new SolidBrush(Color.Gray);
                string tb = "Chưa có dữ liệu doanh thu trong khoảng thời gian này.";
                var size = g.MeasureString(tb, fontThongBao);
                g.DrawString(tb, fontThongBao, brushThongBao, (pWidth - size.Width) / 2, (pHeight - size.Height) / 2);
                return;
            }

            decimal tongDoanhThu = _duLieuLoaiPhong.Sum(x => x.DoanhThu);

            // Kích thước vòng tròn Donut
            int minDim = Math.Min(pWidth, pHeight);
            int pieSize = (int)(minDim * 0.58f);
            int pieX = 25;
            int pieY = (pHeight - pieSize) / 2 - 15;
            if (pieY < 20) pieY = 20;
            var pieRect = new Rectangle(pieX, pieY, pieSize, pieSize);

            float startAngle = -90f;
            using var penWhite = new Pen(Color.White, 2.5f);

            // Vẽ từng hình quạt biểu đồ tròn
            for (int i = 0; i < _duLieuLoaiPhong.Count; i++)
            {
                var item = _duLieuLoaiPhong[i];
                float sweepAngle = (float)(item.DoanhThu / tongDoanhThu * 360m);
                if (sweepAngle <= 0.01f) continue;

                Color mau = _mauBieuDo[i % _mauBieuDo.Length];
                using (var brush = new SolidBrush(mau))
                {
                    g.FillPie(brush, pieRect, startAngle, sweepAngle);
                }
                g.DrawPie(penWhite, pieRect, startAngle, sweepAngle);

                startAngle += sweepAngle;
            }

            // Vẽ lỗ tròn ở giữa để tạo thành Donut Chart hiện đại
            int holeSize = (int)(pieSize * 0.52f);
            int holeX = pieX + (pieSize - holeSize) / 2;
            int holeY = pieY + (pieSize - holeSize) / 2;
            var holeRect = new Rectangle(holeX, holeY, holeSize, holeSize);
            using (var brushHole = new SolidBrush(Color.White))
            {
                g.FillEllipse(brushHole, holeRect);
            }
            g.DrawEllipse(penWhite, holeRect);

            // Chữ "Tổng thu" ở giữa Donut
            using (var fontGiuaTitle = new Font("Segoe UI", 9.5F, FontStyle.Regular))
            using (var fontGiuaValue = new Font("Segoe UI", 11.5F, FontStyle.Bold))
            using (var brushText = new SolidBrush(Color.FromArgb(40, 40, 40)))
            {
                string textTitle = "Tổng thu";
                string textVal = tongDoanhThu >= 1_000_000 
                    ? (tongDoanhThu / 1_000_000m).ToString("0.#") + " tr"
                    : tongDoanhThu.ToString("N0");

                var sTitle = g.MeasureString(textTitle, fontGiuaTitle);
                var sVal = g.MeasureString(textVal, fontGiuaValue);

                g.DrawString(textTitle, fontGiuaTitle, brushText, holeX + (holeSize - sTitle.Width) / 2, holeY + holeSize / 2 - sTitle.Height);
                g.DrawString(textVal, fontGiuaValue, new SolidBrush(Color.FromArgb(21, 101, 192)), holeX + (holeSize - sVal.Width) / 2, holeY + holeSize / 2 + 2);
            }

            // Vẽ phần Chú thích (Legend) bên phải
            int legendX = pieX + pieSize + 25;
            int legendY = 35;
            using var fontLegendTitle = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            using var fontLegendSub = new Font("Segoe UI", 9F, FontStyle.Regular);
            using var brushSub = new SolidBrush(Color.FromArgb(90, 90, 90));

            for (int i = 0; i < _duLieuLoaiPhong.Count; i++)
            {
                var item = _duLieuLoaiPhong[i];
                Color mau = _mauBieuDo[i % _mauBieuDo.Length];
                decimal phanTram = (item.DoanhThu / tongDoanhThu) * 100m;

                // Ô vuông màu
                using (var brushBox = new SolidBrush(mau))
                {
                    g.FillRectangle(brushBox, legendX, legendY + 3, 14, 14);
                }
                g.DrawRectangle(penWhite, legendX, legendY + 3, 14, 14);

                // Tên loại phòng & %
                string tieuDe = $"{item.TenLPH} ({phanTram:0.0}%)";
                using (var brushTitle = new SolidBrush(Color.FromArgb(30, 30, 30)))
                {
                    g.DrawString(tieuDe, fontLegendTitle, brushTitle, legendX + 22, legendY);
                }

                // Chi tiết số tiền & lượt
                string sub = $"{item.DoanhThu:N0} VND - {item.SoLuot} lượt";
                g.DrawString(sub, fontLegendSub, brushSub, legendX + 22, legendY + 20);

                legendY += 46;
                if (legendY > pHeight - 40) break;
            }
        }

        // ==========================================
        // VẼ BIỂU ĐỒ CỘT (BAR/COLUMN CHART) BẰNG GDI+
        // ==========================================
        private void pnlChartTopDV_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            int pWidth = pnlChartTopDV.Width;
            int pHeight = pnlChartTopDV.Height;

            if (_duLieuTopDV.Count == 0 || _duLieuTopDV.All(x => x.SoLuong <= 0))
            {
                using var fontThongBao = new Font("Segoe UI", 12F, FontStyle.Italic);
                using var brushThongBao = new SolidBrush(Color.Gray);
                string tb = "Chưa có dữ liệu dịch vụ sử dụng trong kỳ.";
                var size = g.MeasureString(tb, fontThongBao);
                g.DrawString(tb, fontThongBao, brushThongBao, (pWidth - size.Width) / 2, (pHeight - size.Height) / 2);
                return;
            }

            // Lấy tối đa top 6 dịch vụ để biểu đồ thoáng đẹp
            var topList = _duLieuTopDV.Take(6).ToList();
            int maxSL = topList.Max(x => x.SoLuong);
            if (maxSL <= 0) maxSL = 1;

            // Làm tròn thang đo Y lên bậc chẵn
            int stepY = Math.Max(1, (int)Math.Ceiling(maxSL / 4.0));
            int maxY = stepY * 4;

            int padLeft = 55;
            int padRight = 30;
            int padTop = 40;
            int padBottom = 85;

            int plotWidth = pWidth - padLeft - padRight;
            int plotHeight = pHeight - padTop - padBottom;
            if (plotWidth <= 50 || plotHeight <= 50) return;

            using var penGrid = new Pen(Color.FromArgb(235, 235, 235), 1f) { DashStyle = DashStyle.Dash };
            using var penAxis = new Pen(Color.FromArgb(180, 180, 180), 1.5f);
            using var fontAxis = new Font("Segoe UI", 9F, FontStyle.Regular);
            using var brushAxis = new SolidBrush(Color.FromArgb(100, 100, 100));

            // Vẽ lưới trục Y và nhãn số
            for (int i = 0; i <= 4; i++)
            {
                int val = stepY * i;
                float y = padTop + plotHeight - (i / 4f) * plotHeight;

                g.DrawLine(penGrid, padLeft, y, padLeft + plotWidth, y);

                string valStr = val.ToString();
                var sSize = g.MeasureString(valStr, fontAxis);
                g.DrawString(valStr, fontAxis, brushAxis, padLeft - sSize.Width - 8, y - sSize.Height / 2);
            }

            // Vẽ trục X cơ sở
            g.DrawLine(penAxis, padLeft, padTop + plotHeight, padLeft + plotWidth, padTop + plotHeight);

            // Vẽ từng cột dịch vụ
            float colSlot = plotWidth / (float)topList.Count;
            float barWidth = Math.Min(55f, colSlot * 0.55f);

            using var fontLabel = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            using var fontValue = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            using var brushTextValue = new SolidBrush(Color.FromArgb(30, 30, 30));

            for (int i = 0; i < topList.Count; i++)
            {
                var dv = topList[i];
                float barHeight = (dv.SoLuong / (float)maxY) * plotHeight;
                float x = padLeft + i * colSlot + (colSlot - barWidth) / 2;
                float y = padTop + plotHeight - barHeight;

                var barRect = new RectangleF(x, y, barWidth, barHeight);

                // Màu sắc cột gradient
                Color colStart = _mauBieuDo[i % _mauBieuDo.Length];
                Color colEnd = Color.FromArgb(Math.Min(255, colStart.R + 40), Math.Min(255, colStart.G + 40), Math.Min(255, colStart.B + 40));

                if (barHeight > 2)
                {
                    using (var brushBar = new LinearGradientBrush(barRect, colStart, colEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(brushBar, barRect);
                    }
                }
                else
                {
                    using var brushFlat = new SolidBrush(colStart);
                    g.FillRectangle(brushFlat, x, padTop + plotHeight - 3, barWidth, 3);
                }

                // Vẽ viền trên cột
                using var penBorder = new Pen(colStart, 1f);
                g.DrawRectangle(penBorder, barRect.X, barRect.Y, barRect.Width, barRect.Height);

                // Vẽ số lượng trên đầu cột
                string valText = dv.SoLuong.ToString();
                var sVal = g.MeasureString(valText, fontValue);
                g.DrawString(valText, fontValue, brushTextValue, x + (barWidth - sVal.Width) / 2, y - sVal.Height - 3);

                // Tên dịch vụ bên dưới (Cắt ngắn nếu quá dài để không bị tràn chữ)
                string ten = dv.TenDV;
                if (ten.Length > 12) ten = ten.Substring(0, 10) + "..";

                var sName = g.MeasureString(ten, fontLabel);
                g.DrawString(ten, fontLabel, brushAxis, x + (barWidth - sName.Width) / 2, padTop + plotHeight + 8);

                // Doanh thu dịch vụ bên dưới tên
                string tienStr = dv.TongTien >= 1_000_000 
                    ? (dv.TongTien / 1_000_000m).ToString("0.#") + " tr"
                    : dv.TongTien.ToString("N0");
                var sTien = g.MeasureString(tienStr, fontAxis);
                g.DrawString(tienStr, fontAxis, new SolidBrush(Color.FromArgb(120, 120, 120)), x + (barWidth - sTien.Width) / 2, padTop + plotHeight + 28);
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
