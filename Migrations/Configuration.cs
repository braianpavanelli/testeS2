namespace TesteS2IT.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TesteS2IT.Models.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TesteS2IT.Models.DBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Games.Any())
            {
                context.Games.Add(new Models.Games
                {
                    Name = "Battlefield",
                    Type = "Tiro",
                    Year = "2008",
                    UrlImage = "~/Images/game_Battlefield2005.jpg"
                });
                context.Games.Add(new Models.Games
                {
                    Name = "Far Cry 5",
                    Type = "Ação",
                    Year = "2018",
                    UrlImage = "~/Images/game_farcry_5.jpg"
                });
                context.Games.Add(new Models.Games
                {
                    Name = "Call of Duty",
                    Type = "Tiro",
                    Year = "2012",
                    UrlImage = "~/Images/game_callofduty.jpg"
                });
                context.Games.Add(new Models.Games
                {
                    Name = "Super Mario World",
                    Type = "Aventura",
                    Year = "1990",
                    UrlImage = string.Empty
                });
                //context.SaveChanges();
            }

            if (!context.Friend.Any())
            {
                context.Friend.Add(new Models.Friend
                {
                    Name = "José Roberto",
                    Address = "Av. Raimundo Correa, 917",
                    Phone = "990908787",
                    Mail = "zebeto@gmail.com"
                });
                context.Friend.Add(new Models.Friend
                {
                    Name = "Ana Julia",
                    Address = "Rua Voluntarios da Pátria, 1022",
                    Phone = "9987654321",
                    Mail = "aninhaju@gmail.com"
                });
                context.Friend.Add(new Models.Friend
                {
                    Name = "André Rodrigues",
                    Address = "Av. 15 de novembro, 1131",
                    Phone = "9912345678",
                    Mail = "andre.ro91@gmail.com"
                });
            }

            if (!context.Lending.Any())
            {
                context.Lending.Add(new Models.Lending
                {
                    GameId = 1,
                    FriendId = 1,
                    DateLended = DateTime.Now
                });
            }
            context.SaveChanges();
        }
    }
}
