namespace DAL.Migrations
{
    using DAL.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Entity.BookShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.Entity.BookShopContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            //context.Authors.AddOrUpdate(
            //  p => p.AuthorId,
            //  new Author { AuthorId = 5, AuthorName = "Йо́ганн Во́льфґанґ фон Ге́те", Description = "Йо́ганн Во́льфґанґ фон Ге́те — німецький поет, прозаїк, драматург, мислитель і натураліст.", NationalityId = 1 },
            //  new Author { AuthorId = 6, AuthorName = "Никола́й Васи́льевич Го́голь", Description = "Никола́й Васи́льевич Го́голь — русский прозаик, драматург, поэт, критик, публицист, признанный одним из классиков русской литературы.", NationalityId = 2 },
            //  new Author { AuthorId = 7, AuthorName = "Алекса́ндр Серге́евич Пу́шкин", Description = "Алекса́ндр Серге́евич Пу́шкин — русский поэт, драматург и прозаик, заложивший основы русского реалистического направления.", NationalityId = 3 }
            //);


        }
    }
}
