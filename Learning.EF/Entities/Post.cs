using System.Collections.Generic;

namespace Learning.EF.Entities
{
    public class Post
    {
        public string Tags { get; set; }  

        public virtual Blog Blog { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
