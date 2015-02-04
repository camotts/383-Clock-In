namespace ClockIn_ClockOut.Migrations
{
    using ClockIn_ClockOut.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ClockIn_ClockOut.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ClockIn_ClockOut.Models.DatabaseContext";
        }

        protected override void Seed(ClockIn_ClockOut.Models.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            IList<User> defaultUser = new List<User>();

            defaultUser.Add(new User() { ID = 1, Username = "Admin", FirstName = "Group", LastName = "Six", Password = "selu2014", Role = 2, Timed = false});
            defaultUser.Add(new User() { ID = 2, Username = "bcornett", FirstName = "Brandon", LastName = "Cornett", Password = "Envoc1", Role = 1, Timed = false});
            defaultUser.Add(new User() { ID = 3, Username = "kjoiner", FirstName = "Kyle", LastName = "Joiner", Password = "Envoc2", Role = 1, Timed = false});
            defaultUser.Add(new User() { ID = 4, Username = "WiiKingJoe", FirstName = "Joe", LastName = "Naquin", Password = "1Hefebeer", Role = 1, Timed = false});

            foreach (User std in defaultUser)
                context.Users.AddOrUpdate(std);
            context.SaveChanges();


        }
    }
}
