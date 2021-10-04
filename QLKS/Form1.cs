using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//dùng thư viện này để kết nối sql server
using System.Data.SqlClient;

namespace QLKS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //lưu chuỗi kết nối
        string chuoiketnoi = @"Data Source=DESKTOP-CT5FGGC;Initial Catalog=QLKS;Integrated Security=True";
        
        //lưu câu lệnh truy vấn trong sql
        string sql;

        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i;

        #region chạy form
        private void Form1_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            hienthi();
        }
        #endregion

        #region Hiển Thị
        public void hienthi()
        {
            lvThongTinPhong.Items.Clear();
            ketnoi.Open();
            sql = @"Select * from PHONG";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                lvThongTinPhong.Items.Add(docdulieu[0].ToString());
                lvThongTinPhong.Items[i].SubItems.Add(docdulieu[1].ToString());
                lvThongTinPhong.Items[i].SubItems.Add(docdulieu[2].ToString());
                i++;
            }
            docdulieu.Close();
            ketnoi.Close();
        }
        #endregion

        #region Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            lvThongTinPhong.Items.Clear();
            ketnoi.Open();
            sql = @"insert into PHONG 
                    values ('" + txtMaPhong.Text +"',N'" + txtDonGia.Text +"',N'" + cbbTinhTrang.Text +"')";
            thuchien = new SqlCommand(sql, ketnoi);
            int kq = thuchien.ExecuteNonQuery();
            if(kq > 0)
            {
                MessageBox.Show("Thêm Dữ Liệu Thành Công!!!");
            }
            else
            {
                MessageBox.Show("Không Thể Thêm Dữ Liệu!!!");
            }
            ketnoi.Close();
            hienthi();
        }
        #endregion

        #region Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            lvThongTinPhong.Items.Clear();
            ketnoi.Open();
            sql = @"update PHONG set
                    MaPhong = '" + txtMaPhong.Text + "', DonGia = N'" + txtDonGia.Text + "', TinhTrang = N'" + cbbTinhTrang.Text + @"'
                    where (MaPhong = N'" + txtMaPhong.Text + @"')";
            thuchien = new SqlCommand(sql, ketnoi);
            //thuchien.ExecuteNonQuery();
            int kq = thuchien.ExecuteNonQuery();
            if (kq > 0)
            {
                MessageBox.Show("Sửa Dữ Liệu Thành Công!!!");
            }
            else
            {
                MessageBox.Show("Không Thể Sửa Dữ Liệu\nVui Lòng Không Sửa Mã Phòng!!!");
            }    
            ketnoi.Close();
            hienthi();
        }
        #endregion

        #region Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            lvThongTinPhong.Items.Clear();
            ketnoi.Open();
            sql = @"delete from PHONG where(MaPhong = '" + txtMaPhong.Text + "')";
            thuchien = new SqlCommand(sql, ketnoi);
            int kq = thuchien.ExecuteNonQuery();
            if (kq > 0)
            {
                MessageBox.Show("Xóa Dữ Liệu Thành Công!!!");
            }
            else
            {
                MessageBox.Show("Không Thể Xửa Dữ Liệu!!!");
            }
            ketnoi.Close();
            hienthi();
        }
        #endregion

        #region Tìm Kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            lvThongTinPhong.Items.Clear();
            ketnoi.Open();
            sql = @"select * from PHONG where (MaPhong = '" + txtTimKiem.Text + "')";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                lvThongTinPhong.Items.Add(docdulieu[0].ToString());
                lvThongTinPhong.Items[i].SubItems.Add(docdulieu[1].ToString());
                lvThongTinPhong.Items[i].SubItems.Add(docdulieu[2].ToString());
                i++;
            }
            docdulieu.Close();
            ketnoi.Close();
        }
        #endregion

        //thao tác của bài cũ
        #region Bài Cũ
        #region chuyencode
        private void lvThongTinPhong_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text = lvThongTinPhong.SelectedItems[0].SubItems[0].Text;
            txtDonGia.Text = lvThongTinPhong.SelectedItems[0].SubItems[1].Text;
            cbbTinhTrang.Text = lvThongTinPhong.SelectedItems[0].SubItems[2].Text;
        }
        #endregion
        #region Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat = MessageBox.Show("Bạn Có Muốn Thoát Chương Trình?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (thoat == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }
        #endregion

        #endregion
    }
}
