using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.DAL
{
    public class RestaurantDBContext : DbContext
    {
        public RestaurantDBContext(DbContextOptions<RestaurantDBContext> options) : base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<FoodType> FoodType { get; set; }
    }
}
