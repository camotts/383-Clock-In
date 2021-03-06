namespace ClockIn_ClockOut.Migrations
{
    using ClockIn_ClockOut.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Helpers;
    
    internal sealed class Configuration : DbMigrationsConfiguration<ClockIn_ClockOut.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ClockIn_ClockOut.Models.DatabaseContext";
        }

        protected override void Seed(ClockIn_ClockOut.Models.DatabaseContext context)
        {
            
            IList<User> defaultUser = new List<User>();
            
            defaultUser.Add(new User() { ID = 1, Username = "Admin", FirstName = "Group", LastName = "Six", Password = Crypto.HashPassword("selu2014"), Role = 2, Timed = false});
            defaultUser.Add(new User() { ID = 2, Username = "bcornett", FirstName = "Brandon", LastName = "Cornett",Password = Crypto.HashPassword("Envoc1") , Role = 1, Timed = false});
            defaultUser.Add(new User() { ID = 3, Username = "kjoiner", FirstName = "Kyle", LastName = "Joiner", Password = Crypto.HashPassword("Envoc2"), Role = 1, Timed = false});
            defaultUser.Add(new User() { ID = 4, Username = "WiiKingJoe", FirstName = "Joe", LastName = "Naquin",Password = Crypto.HashPassword("1Hefebeer") , Role = 1, Timed = false});
            
            foreach (User std in defaultUser)
                context.Users.AddOrUpdate(std);
            context.SaveChanges();

            IList<Role> defaultRole = new List<Role>();

            defaultRole.Add(new Role() { ID = 2, Name = "Admin" });
            defaultRole.Add(new Role() { ID = 1, Name = "User" });

            foreach (Role std in defaultRole)
                context.Roles.AddOrUpdate(std);
            context.SaveChanges();

        }
    }
}
