using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace h5todos.Models
{
    public class TodosContext : DbContext
    {
        public TodosContext(DbContextOptions<TodosContext> options) : base(options)
        {

        }
        public DbSet<Login> Login { get; set; }

        public DbSet<TodosItem> TodosItem { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder.UseSqlServer(@"Data Source=DESKTOP-JBE33R4\BACON;Initial Catalog=TodosDB;Integrated Security=True");
        //}
    }
}
