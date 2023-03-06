using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBLib
{
    class MySQLDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseMySQL("Server=127.0.0.1;Port=3306;Database=bomberman;Uid=root;Pwd=;");
        }
    }
}
