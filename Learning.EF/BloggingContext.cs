using Learning.EF.Entities;
using System.Data.Entity;

namespace Learning.EF
{
    public class BloggingContext:DbContext
    {
        public IDbSet<Blog> Blogs;
        public IDbSet<Post> Posts;
        public IDbSet<User> Users;
        public IDbSet<Comment> Comments;


        public BloggingContext():base("eLearningConnection2")
        {
            //Turn of lazy loading
            //this.Configuration.LazyLoadingEnabled = false; 
        }

         
        
    }
}
