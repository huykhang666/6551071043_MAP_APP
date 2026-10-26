using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace QuanLyKhachSan.Models;

public partial class HotelManagementContext
{
    private const string ConnectionString =
        "Server=.;Database=HotelManagement;Trusted_Connection=True;TrustServerCertificate=True;";
    public HotelManagementContext()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
