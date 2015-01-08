using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Learning.EF
{
    public class EagerLoadingMultipleLevels
    {
        public void Method1()
        {

            using (var context = new BloggingContext())
            {
                // Load all blogs, all related posts, and all related comments 
                var blogs1 = context.Blogs
                                   .Include(b => b.Posts.Select(p => p.Comments))
                                   .ToList();

                // Load all users their related profiles, and related avatar 
                var users1 = context.Users
                                    .Include(u => u.Profile.Avatar)
                                    .ToList();

                // Load all blogs, all related posts, and all related comments  
                // using a string to specify the relationships 
                var blogs2 = context.Blogs
                                   .Include("Posts.Comments")
                                   .ToList();

                // Load all users their related profiles, and related avatar  
                // using a string to specify the relationships 
                var users2 = context.Users
                                    .Include("Profile.Avatar")
                                    .ToList();
            }
        }
    }
}
