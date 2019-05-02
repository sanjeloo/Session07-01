using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Session7;

namespace Session7.Ui
{
    public class Program
    {
        static void Main(string[] args)
        {
            //BlogContextSample();
            PersonContext pc = new PersonContext();
            CreateAndInsertHierarchieMapping(pc);
            // pc.People.ToList();
            //var account = pc.accounts.ToList();
            //var all= pc.People.Include(c => c.acc).ToList();

        }

        private static void CreateAndInsertHierarchieMapping(PersonContext pc)
        {
            pc.Database.EnsureDeleted();
            pc.Database.EnsureCreated();

            pc.People.Add(new Person()
            {
                LastName = "nosratian",
                Home = new Address()
                {
                    HomeAddress = "mamulo te darokhona",
                    OfficeAddress = "09161258749"
                },
                acc = new BacnkAccount
                {
                    AccountNumber = "12345678"
                }

            });
            pc.Teachers.Add(new Teacher()
            {
                LastName = "nosratian",
                Home = new Address()
                {
                    HomeAddress = "mamulo te darokhona",
                    OfficeAddress = "09161258749"
                },
                Code = "1258",
                acc = new BacnkAccount
                {
                    AccountNumber = "09876543"
                }
            });
            
            pc.SaveChanges();
            Console.WriteLine("its done");
        }

        private static void BlogContextSample()
        {
            BlogContext db = new BlogContext();
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            //Blog b = new Blog()
            //{
            //    Name = "Elnaz",
            //    CreateDate = DateTime.Now
            //};
            //db.Blogs.Add(b);
            //db.SaveChanges();

            //List<Post> posts = new List<Post> {
            //    new Post { BlogId = b.Id,Body ="some post0"},
            //    new Post { BlogId = b.Id,Body ="some post1"},
            //    new Post { BlogId = b.Id,Body ="some post2"},
            //    };
            //db.AddRange(posts);
            //db.SaveChanges();

            //db.Blogs.Remove(b);
            //db.SaveChanges();
            //Console.WriteLine("Hello World!");
            var posts = db.Posts.ToList();
            db.Blogs.Remove(new Blog { Id = 1 });
            db.SaveChanges();
        }
    }
}
