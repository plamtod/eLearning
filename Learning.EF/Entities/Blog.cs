﻿using System.Collections.Generic;

namespace Learning.EF.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Tags { get; set; } 

        public ICollection<Post> Posts { get; set; }
    }
}
