namespace QuanLyKhachSan.ThongKe
{
    partial class FormThongKe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            panelHeader = new Panel();
            btnXuatExcel = new Button();
            btnThongKe = new Button();
            dtpDenNgay = new DateTimePicker();
            labelDenNgay = new Label();
            dtpTuNgay = new DateTimePicker();
            labelTuNgay = new Label();
            lblTieuDe = new Label();
            panelCard1 = new Panel();
            lblCardTienPhong = new Label();
            lblTitleCard1 = new Label();
            picCard1 = new PictureBox();
            panelCard2 = new Panel();
            lblCardTienDV = new Label();
            lblTitleCard2 = new Label();
            picCard2 = new PictureBox();
            panelCard3 = new Panel();
            lblCardTongTien = new Label();
            lblTitleCard3 = new Label();
            picCard3 = new PictureBox();
            panelCard4 = new Panel();
            lblCardSoPhong = new Label();
            lblTitleCard4 = new Label();
            picCard4 = new PictureBox();
            groupBoxLoaiPhong = new GroupBox();
            dtgDoanhThuLoaiPhong = new DataGridView();
            groupBoxTopDV = new GroupBox();
            dtgTopDichVu = new DataGridView();
            groupBoxChiTiet = new GroupBox();
            dtgChiTietHoaDon = new DataGridView();
            panelHeader.SuspendLayout();
            panelCard1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCard1).BeginInit();
            panelCard2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCard2).BeginInit();
            panelCard3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCard3).BeginInit();
            panelCard4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCard4).BeginInit();
            groupBoxLoaiPhong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgDoanhThuLoaiPhong).BeginInit();
            groupBoxTopDV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgTopDichVu).BeginInit();
            groupBoxChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgChiTietHoaDon).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelHeader.BackColor = Color.White;
            panelHeader.BorderStyle = BorderStyle.FixedSingle;
            panelHeader.Controls.Add(btnXuatExcel);
            panelHeader.Controls.Add(btnThongKe);
            panelHeader.Controls.Add(dtpDenNgay);
            panelHeader.Controls.Add(labelDenNgay);
            panelHeader.Controls.Add(dtpTuNgay);
            panelHeader.Controls.Add(labelTuNgay);
            panelHeader.Controls.Add(lblTieuDe);
            panelHeader.Location = new Point(12, 12);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1128, 75);
            panelHeader.TabIndex = 0;
            // 
            // btnXuatExcel
            // 
            btnXuatExcel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnXuatExcel.BackColor = Color.DarkTurquoise;
            btnXuatExcel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnXuatExcel.ForeColor = Color.White;
            btnXuatExcel.Location = new Point(995, 16);
            btnXuatExcel.Name = "btnXuatExcel";
            btnXuatExcel.Size = new Size(120, 42);
            btnXuatExcel.TabIndex = 6;
            btnXuatExcel.Text = "Xuất Excel";
            btnXuatExcel.UseVisualStyleBackColor = false;
            btnXuatExcel.Click += btnXuatExcel_Click;
            // 
            // btnThongKe
            // 
            btnThongKe.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnThongKe.BackColor = Color.SeaGreen;
            btnThongKe.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnThongKe.ForeColor = Color.White;
            btnThongKe.Location = new Point(870, 16);
            btnThongKe.Name = "btnThongKe";
            btnThongKe.Size = new Size(115, 42);
            btnThongKe.TabIndex = 5;
            btnThongKe.Text = "Thống kê";
            btnThongKe.UseVisualStyleBackColor = false;
            btnThongKe.Click += btnThongKe_Click;
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtpDenNgay.Font = new Font("Segoe UI", 11F);
            dtpDenNgay.Format = DateTimePickerFormat.Short;
            dtpDenNgay.Location = new Point(710, 22);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(145, 32);
            dtpDenNgay.TabIndex = 4;
            // 
            // labelDenNgay
            // 
            labelDenNgay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelDenNgay.AutoSize = true;
            labelDenNgay.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            labelDenNgay.Location = new Point(610, 26);
            labelDenNgay.Name = "labelDenNgay";
            labelDenNgay.Size = new Size(97, 25);
            labelDenNgay.TabIndex = 3;
            labelDenNgay.Text = "Đến ngày:";
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtpTuNgay.Font = new Font("Segoe UI", 11F);
            dtpTuNgay.Format = DateTimePickerFormat.Short;
            dtpTuNgay.Location = new Point(450, 22);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(145, 32);
            dtpTuNgay.TabIndex = 2;
            // 
            // labelTuNgay
            // 
            labelTuNgay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelTuNgay.AutoSize = true;
            labelTuNgay.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            labelTuNgay.Location = new Point(365, 26);
            labelTuNgay.Name = "labelTuNgay";
            labelTuNgay.Size = new Size(85, 25);
            labelTuNgay.TabIndex = 1;
            labelTuNgay.Text = "Từ ngày:";
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.DarkSlateBlue;
            lblTieuDe.Location = new Point(12, 17);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(297, 37);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "THỐNG KÊ DOANH THU";
            // 
            // panelCard1
            // 
            panelCard1.BackColor = Color.FromArgb(232, 245, 253);
            panelCard1.BorderStyle = BorderStyle.FixedSingle;
            panelCard1.Controls.Add(lblCardTienPhong);
            panelCard1.Controls.Add(lblTitleCard1);
            panelCard1.Controls.Add(picCard1);
            panelCard1.Location = new Point(12, 100);
            panelCard1.Name = "panelCard1";
            panelCard1.Size = new Size(265, 105);
            panelCard1.TabIndex = 1;
            // 
            // lblCardTienPhong
            // 
            lblCardTienPhong.AutoSize = true;
            lblCardTienPhong.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblCardTienPhong.ForeColor = Color.FromArgb(21, 101, 192);
            lblCardTienPhong.Location = new Point(80, 48);
            lblCardTienPhong.Name = "lblCardTienPhong";
            lblCardTienPhong.Size = new Size(89, 35);
            lblCardTienPhong.TabIndex = 2;
            lblCardTienPhong.Text = "0 VND";
            // 
            // lblTitleCard1
            // 
            lblTitleCard1.AutoSize = true;
            lblTitleCard1.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblTitleCard1.ForeColor = Color.FromArgb(66, 66, 66);
            lblTitleCard1.Location = new Point(80, 15);
            lblTitleCard1.Name = "lblTitleCard1";
            lblTitleCard1.Size = new Size(162, 25);
            lblTitleCard1.TabIndex = 1;
            lblTitleCard1.Text = "Doanh thu phòng";
            // 
            // picCard1
            // 
            picCard1.Image = Properties.Resources.DoanhThuThue;
            picCard1.Location = new Point(15, 20);
            picCard1.Name = "picCard1";
            picCard1.Size = new Size(55, 60);
            picCard1.SizeMode = PictureBoxSizeMode.Zoom;
            picCard1.TabIndex = 0;
            picCard1.TabStop = false;
            // 
            // panelCard2
            // 
            panelCard2.BackColor = Color.FromArgb(243, 233, 253);
            panelCard2.BorderStyle = BorderStyle.FixedSingle;
            panelCard2.Controls.Add(lblCardTienDV);
            panelCard2.Controls.Add(lblTitleCard2);
            panelCard2.Controls.Add(picCard2);
            panelCard2.Location = new Point(299, 100);
            panelCard2.Name = "panelCard2";
            panelCard2.Size = new Size(265, 105);
            panelCard2.TabIndex = 2;
            // 
            // lblCardTienDV
            // 
            lblCardTienDV.AutoSize = true;
            lblCardTienDV.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblCardTienDV.ForeColor = Color.Purple;
            lblCardTienDV.Location = new Point(80, 48);
            lblCardTienDV.Name = "lblCardTienDV";
            lblCardTienDV.Size = new Size(89, 35);
            lblCardTienDV.TabIndex = 2;
            lblCardTienDV.Text = "0 VND";
            // 
            // lblTitleCard2
            // 
            lblTitleCard2.AutoSize = true;
            lblTitleCard2.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblTitleCard2.ForeColor = Color.FromArgb(66, 66, 66);
            lblTitleCard2.Location = new Point(80, 15);
            lblTitleCard2.Name = "lblTitleCard2";
            lblTitleCard2.Size = new Size(167, 25);
            lblTitleCard2.TabIndex = 1;
            lblTitleCard2.Text = "Doanh thu dịch vụ";
            // 
            // picCard2
            // 
            picCard2.Image = Properties.Resources.DichVuChart;
            picCard2.Location = new Point(15, 20);
            picCard2.Name = "picCard2";
            picCard2.Size = new Size(55, 60);
            picCard2.SizeMode = PictureBoxSizeMode.Zoom;
            picCard2.TabIndex = 0;
            picCard2.TabStop = false;
            // 
            // panelCard3
            // 
            panelCard3.BackColor = Color.FromArgb(254, 249, 231);
            panelCard3.BorderStyle = BorderStyle.FixedSingle;
            panelCard3.Controls.Add(lblCardTongTien);
            panelCard3.Controls.Add(lblTitleCard3);
            panelCard3.Controls.Add(picCard3);
            panelCard3.Location = new Point(587, 100);
            panelCard3.Name = "panelCard3";
            panelCard3.Size = new Size(265, 105);
            panelCard3.TabIndex = 3;
            // 
            // lblCardTongTien
            // 
            lblCardTongTien.AutoSize = true;
            lblCardTongTien.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblCardTongTien.ForeColor = Color.DarkOrange;
            lblCardTongTien.Location = new Point(80, 48);
            lblCardTongTien.Name = "lblCardTongTien";
            lblCardTongTien.Size = new Size(89, 35);
            lblCardTongTien.TabIndex = 2;
            lblCardTongTien.Text = "0 VND";
            // 
            // lblTitleCard3
            // 
            lblTitleCard3.AutoSize = true;
            lblTitleCard3.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblTitleCard3.ForeColor = Color.FromArgb(66, 66, 66);
            lblTitleCard3.Location = new Point(80, 15);
            lblTitleCard3.Name = "lblTitleCard3";
            lblTitleCard3.Size = new Size(149, 25);
            lblTitleCard3.TabIndex = 1;
            lblTitleCard3.Text = "Tổng doanh thu";
            // 
            // picCard3
            // 
            picCard3.Image = Properties.Resources.DoanhThu;
            picCard3.Location = new Point(15, 20);
            picCard3.Name = "picCard3";
            picCard3.Size = new Size(55, 60);
            picCard3.SizeMode = PictureBoxSizeMode.Zoom;
            picCard3.TabIndex = 0;
            picCard3.TabStop = false;
            // 
            // panelCard4
            // 
            panelCard4.BackColor = Color.FromArgb(232, 248, 245);
            panelCard4.BorderStyle = BorderStyle.FixedSingle;
            panelCard4.Controls.Add(lblCardSoPhong);
            panelCard4.Controls.Add(lblTitleCard4);
            panelCard4.Controls.Add(picCard4);
            panelCard4.Location = new Point(875, 100);
            panelCard4.Name = "panelCard4";
            panelCard4.Size = new Size(265, 105);
            panelCard4.TabIndex = 4;
            // 
            // lblCardSoPhong
            // 
            lblCardSoPhong.AutoSize = true;
            lblCardSoPhong.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblCardSoPhong.ForeColor = Color.FromArgb(0, 150, 136);
            lblCardSoPhong.Location = new Point(80, 48);
            lblCardSoPhong.Name = "lblCardSoPhong";
            lblCardSoPhong.Size = new Size(95, 35);
            lblCardSoPhong.TabIndex = 2;
            lblCardSoPhong.Text = "0 lượt";
            // 
            // lblTitleCard4
            // 
            lblTitleCard4.AutoSize = true;
            lblTitleCard4.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblTitleCard4.ForeColor = Color.FromArgb(66, 66, 66);
            lblTitleCard4.Location = new Point(80, 15);
            lblTitleCard4.Name = "lblTitleCard4";
            lblTitleCard4.Size = new Size(157, 25);
            lblTitleCard4.TabIndex = 1;
            lblTitleCard4.Text = "Lượt thuê phòng";
            // 
            // picCard4
            // 
            picCard4.Image = Properties.Resources.SoPhongDat;
            picCard4.Location = new Point(15, 20);
            picCard4.Name = "picCard4";
            picCard4.Size = new Size(55, 60);
            picCard4.SizeMode = PictureBoxSizeMode.Zoom;
            picCard4.TabIndex = 0;
            picCard4.TabStop = false;
            // 
            // groupBoxLoaiPhong
            // 
            groupBoxLoaiPhong.Controls.Add(dtgDoanhThuLoaiPhong);
            groupBoxLoaiPhong.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            groupBoxLoaiPhong.Location = new Point(12, 220);
            groupBoxLoaiPhong.Name = "groupBoxLoaiPhong";
            groupBoxLoaiPhong.Size = new Size(552, 260);
            groupBoxLoaiPhong.TabIndex = 5;
            groupBoxLoaiPhong.TabStop = false;
            groupBoxLoaiPhong.Text = "Doanh thu theo Loại phòng";
            // 
            // dtgDoanhThuLoaiPhong
            // 
            dtgDoanhThuLoaiPhong.AllowUserToAddRows = false;
            dtgDoanhThuLoaiPhong.AllowUserToDeleteRows = false;
            dtgDoanhThuLoaiPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgDoanhThuLoaiPhong.BackgroundColor = Color.White;
            dtgDoanhThuLoaiPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgDoanhThuLoaiPhong.Dock = DockStyle.Fill;
            dtgDoanhThuLoaiPhong.Location = new Point(3, 27);
            dtgDoanhThuLoaiPhong.Name = "dtgDoanhThuLoaiPhong";
            dtgDoanhThuLoaiPhong.ReadOnly = true;
            dtgDoanhThuLoaiPhong.RowHeadersVisible = false;
            dtgDoanhThuLoaiPhong.RowHeadersWidth = 51;
            dtgDoanhThuLoaiPhong.Size = new Size(546, 230);
            dtgDoanhThuLoaiPhong.TabIndex = 0;
            // 
            // groupBoxTopDV
            // 
            groupBoxTopDV.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxTopDV.Controls.Add(dtgTopDichVu);
            groupBoxTopDV.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            groupBoxTopDV.Location = new Point(587, 220);
            groupBoxTopDV.Name = "groupBoxTopDV";
            groupBoxTopDV.Size = new Size(553, 260);
            groupBoxTopDV.TabIndex = 6;
            groupBoxTopDV.TabStop = false;
            groupBoxTopDV.Text = "Top dịch vụ sử dụng nhiều nhất";
            // 
            // dtgTopDichVu
            // 
            dtgTopDichVu.AllowUserToAddRows = false;
            dtgTopDichVu.AllowUserToDeleteRows = false;
            dtgTopDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgTopDichVu.BackgroundColor = Color.White;
            dtgTopDichVu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgTopDichVu.Dock = DockStyle.Fill;
            dtgTopDichVu.Location = new Point(3, 27);
            dtgTopDichVu.Name = "dtgTopDichVu";
            dtgTopDichVu.ReadOnly = true;
            dtgTopDichVu.RowHeadersVisible = false;
            dtgTopDichVu.RowHeadersWidth = 51;
            dtgTopDichVu.Size = new Size(547, 230);
            dtgTopDichVu.TabIndex = 0;
            // 
            // groupBoxChiTiet
            // 
            groupBoxChiTiet.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxChiTiet.Controls.Add(dtgChiTietHoaDon);
            groupBoxChiTiet.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            groupBoxChiTiet.Location = new Point(12, 490);
            groupBoxChiTiet.Name = "groupBoxChiTiet";
            groupBoxChiTiet.Size = new Size(1128, 350);
            groupBoxChiTiet.TabIndex = 7;
            groupBoxChiTiet.TabStop = false;
            groupBoxChiTiet.Text = "Chi tiết danh sách hóa đơn trong kỳ";
            // 
            // dtgChiTietHoaDon
            // 
            dtgChiTietHoaDon.AllowUserToAddRows = false;
            dtgChiTietHoaDon.AllowUserToDeleteRows = false;
            dtgChiTietHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgChiTietHoaDon.BackgroundColor = Color.White;
            dtgChiTietHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgChiTietHoaDon.Dock = DockStyle.Fill;
            dtgChiTietHoaDon.Location = new Point(3, 27);
            dtgChiTietHoaDon.Name = "dtgChiTietHoaDon";
            dtgChiTietHoaDon.ReadOnly = true;
            dtgChiTietHoaDon.RowHeadersVisible = false;
            dtgChiTietHoaDon.RowHeadersWidth = 51;
            dtgChiTietHoaDon.Size = new Size(1122, 320);
            dtgChiTietHoaDon.TabIndex = 0;
            // 
            // FormThongKe
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(1152, 850);
            Controls.Add(groupBoxChiTiet);
            Controls.Add(groupBoxTopDV);
            Controls.Add(groupBoxLoaiPhong);
            Controls.Add(panelCard4);
            Controls.Add(panelCard3);
            Controls.Add(panelCard2);
            Controls.Add(panelCard1);
            Controls.Add(panelHeader);
            Name = "FormThongKe";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thống kê báo cáo";
            Load += FormThongKe_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelCard1.ResumeLayout(false);
            panelCard1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCard1).EndInit();
            panelCard2.ResumeLayout(false);
            panelCard2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCard2).EndInit();
            panelCard3.ResumeLayout(false);
            panelCard3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCard3).EndInit();
            panelCard4.ResumeLayout(false);
            panelCard4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCard4).EndInit();
            groupBoxLoaiPhong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgDoanhThuLoaiPhong).EndInit();
            groupBoxTopDV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgTopDichVu).EndInit();
            groupBoxChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgChiTietHoaDon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label lblTieuDe;
        private Label labelTuNgay;
        private DateTimePicker dtpTuNgay;
        private Label labelDenNgay;
        private DateTimePicker dtpDenNgay;
        private Button btnThongKe;
        private Button btnXuatExcel;
        private Panel panelCard1;
        private PictureBox picCard1;
        private Label lblTitleCard1;
        private Label lblCardTienPhong;
        private Panel panelCard2;
        private PictureBox picCard2;
        private Label lblTitleCard2;
        private Label lblCardTienDV;
        private Panel panelCard3;
        private PictureBox picCard3;
        private Label lblTitleCard3;
        private Label lblCardTongTien;
        private Panel panelCard4;
        private PictureBox picCard4;
        private Label lblTitleCard4;
        private Label lblCardSoPhong;
        private GroupBox groupBoxLoaiPhong;
        private DataGridView dtgDoanhThuLoaiPhong;
        private GroupBox groupBoxTopDV;
        private DataGridView dtgTopDichVu;
        private GroupBox groupBoxChiTiet;
        private DataGridView dtgChiTietHoaDon;
    }
}
