using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.DataSets;
using Bogus.Extensions;
using Bogus;
using DAL.Entity;
using System.Globalization;

namespace RadomDataFiller
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BookShopContext();

            Randomizer.Seed = new Random((int)DateTime.Now.Ticks);

            //var testBooks = new Faker<Book>()
            //                    .CustomInstantiator(f => new Book() { ImageId = 44 } )
            //                    .RuleFor(u => u.AuthorId, f => f.PickRandom(context.Authors.Select(x => x.AuthorId)))
            //                    .RuleFor(u => u.BookName, f => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(f.Random.Word()))
            //                    .RuleFor(u => u.CategoryId, f => f.PickRandom(context.Categories.Select(x => x.CategoryId)))
            //                    .RuleFor(u => u.Count, f => f.Random.Int(0))
            //                    .RuleFor(u => u.DatePublished, f => f.Date.Past())
            //                    .RuleFor(u => u.Description, f => f.Lorem.Paragraph())
            //                    .RuleFor(u => u.Language, f => f.PickRandom<string>(context.Nationalities.Select(u => u.NationalityName)))
            //                    .RuleFor(u => u.Price, f => f.Random.Decimal() * 10)
            //                    .RuleFor(u => u.PublisherId, f => f.PickRandom(context.Publishers.Select(u => u.PublisherId)))
            //                    .RuleFor(u => u.Volume, f => f.Random.Int(0, 2000));
            var a = context.Books.Select(x => x.BookId);
            var b = context.Users.Select(x => x.UserId);
            var testReviews = new Faker<Review>("uk")
                                    .RuleFor(u => u.BookId, f => f.PickRandom(a))
                                    .RuleFor(u => u.Date, f => f.Date.Past())
                                    .RuleFor(u => u.Message, f => f.Lorem.Sentence(2))
                                    .RuleFor(u => u.UserId, f => f.PickRandom(b));

            for (int i = 0; i < 100; i++)
            {
                var review = testReviews.Generate();
                context.Reviews.Add(review);
            }
            context.SaveChanges();

        }
    }
}
