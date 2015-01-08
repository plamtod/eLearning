using System.Linq;
using System.Data.Entity;

namespace Learning.EF
{
    /*
        The Query method provides access to the underlying query that the Entity Framework will use when loading related entities.
        You can then use LINQ to apply filters to the query before executing it with a call to a LINQ extension method such as ToList, Load, etc.
        The Query method can be used with both reference and collection navigation properties but is most useful for collections where it can be used to load only part of the collection.
         
        When using the Query method it is usually best to turn off lazy loading for the navigation property.
        This is because otherwise the entire collection may get loaded automatically by the lazy loading mechanism either before or after the filtered query has been executed.
    */
    public class ExplicitlyLoadingWithFilters
    {

        public void Method1() { 

            using (var context = new BloggingContext()) 
            { 
                var blog = context.Blogs.Find(1); 
 
                // Load the posts with the 'entity-framework' tag related to a given blog 
                context.Entry(blog) 
                    .Collection(b => b.Posts) 
                    .Query() 
                    .Where(p => p.Tags.Contains("entity-framework") )
                    .Load(); 
 
                 //Load the posts with the 'entity-framework' tag related to a given blog  
                 //using a string to specify the relationship  
                //context.Entry(blog) 
                //    .Collection("Posts") 
                //    .Query() 
                //    .Where(p => p.Tags.Contains("entity-framework") )
                //    .Load(); 
            }

        }
    }
}
