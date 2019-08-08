using FinalProjectMain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models
{
    public class ShoppingDbContext : DbContext
    {
        public ShoppingDbContext()
            : base("name=cString")
        {
            //Configuration.LazyLoadingEnabled=false;
            //Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Product> products { get; set; }
        public DbSet<additionalInformation> additionalInformation { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public DbSet<ManagerStatus> ManagerStatus { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<ProducerCompany> producers { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<ContactUS> ContactUs { get; set; }
        public DbSet<OrderHistory> OrderHistory { get; set; }
        public DbSet<shoppingAdresses> shoppingAdresses { get; set; }
        public DbSet<imageProduct> imageProducts { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<CategoryAndProduct> CategoryAndProduct { get; set; }
        public DbSet<mainCarousel> mainCarousels { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<addAdressToAccount> addAdressToAccounts { get; set; }
        public DbSet<wishListProductAndUser> wishListProductAndUsers { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<answerComment> AnswerComments { get; set; }
        public DbSet<ErrorHistory> ErrorHistory { get; set; }
        public DbSet<shopList> shopList { get; set; }
        public DbSet<Rating> rating { get; set; }
        public DbSet<siteInfo> siteInfo { get; set; }

    }
}