namespace FinalProjectMain.Migrations
{
    using FinalProjectMain.Models.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FinalProjectMain.Models.ShoppingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FinalProjectMain.Models.ShoppingDbContext context)
        {
            try
            {
                if (!context.Manager.Any())
                {
                    context.Manager.AddRange(new[]
                    {
                        new Manager
                        {
                             Name="Iqbal",
                              Surname = "Həsənli",
                               Email = "iqbal.hoff@list.ru",
                                 CreatedDate = DateTime.Now,
                                  
                        }

                    });
                }



                if (!context.ManagerStatus.Any())
                {
                    context.ManagerStatus.AddRange(new[]
                    {
                        new ManagerStatus
                        {
                              StatusName = "Admin",
                              CreatedDate = DateTime.Now,
                        },

                        new ManagerStatus
                        {
                              StatusName = "SuperAdmin",
                              CreatedDate = DateTime.Now,
                        }


                    });
                }

                if (!context.categories.Any())
                {

                    context.categories.AddRange(new[]
                    {
                        new Category
                        {

                             CategoryName = "Suvenirlər",
                             CreatedDate = DateTime.Now,

                        },
                        new Category
                        {

                             CategoryName = "Yemək Dəsti",
                             CreatedDate = DateTime.Now,
                             withSubCtegory =true,

                        },
                        new Category
                        {

                             CategoryName = "Çay Dəsti",
                             CreatedDate = DateTime.Now,
                             withSubCtegory =true,

                        }

                    });
                }
                if (!context.categories.Any())
                {

                    context.Color.AddRange(new[]
                    {
                        new Color                        {

                             ColorName = "Qırmızı",
                             CreatedDate = DateTime.Now,
                             
                        },
                        new Color                        {

                             ColorName = "Göy",
                             CreatedDate = DateTime.Now,
                             
                        },
                       
                    });
                }
                if (!context.Countries.Any())
                {

                    context.Countries.AddRange(new[]
                    {
                        new Countries                        {

                             CountryName = "Azərbaycan",
                             CreatedDate = DateTime.Now,
                        },
                        
                    });
                }
                if (!context.siteInfo.Any())
                {

                    context.siteInfo.AddRange(new[]
                    {
                        new siteInfo                        {

                             Phone = "(+994)50 6705569",
                             Phone_2 = "(+994)12 5644488",
                             Instgram = "www.Instagram.Com",
                             Email = "zahidhesenov96@gmail.com",
                             CreatedDate = DateTime.Now,
                        },
                        
                    });
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
