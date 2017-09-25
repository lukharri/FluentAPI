using CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAPI.EntityConfigurations
{
    public class CourseConfiguration : EntityTypeConfiguration<Course>
    {
        public CourseConfiguration()
        {
            // Organizing Configurations
            // 1. table overwrites such as changing table name
            // 2. overwrite primary keys
            // 3. property configs sorted alphabetically
            // 4. relationships sorted alphabetically

            // make 'description' and 'title' required and set max length
            Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(2000);

            Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(255);


            // change name of Author_Id prop to AuthorId
            // first configure the relationship and then rename the foreign key
            // have to add the AuthorId prop to the course class
            HasRequired(c => c.Author)
            .WithMany(a => a.Courses)
            .HasForeignKey(c => c.AuthorId)
            // prevents author records who have a course from being deleted
            .WillCascadeOnDelete(false);


            // configure one-to-one relationship btw course and cover
            // specify principal and dependent
            HasRequired(c => c.Cover)
            .WithRequiredPrincipal(c => c.Course);


            // change name of intermediary table between courses and tags
            // first configure the relationship then change the table name
            HasMany(c => c.Tags)
            .WithMany(t => t.Courses)
            // map method takes an action (delegate) 
            // need to supply a pointer to a method that takes the action as a parameter
            // imagine this is a method call that takes m as a parameter
            .Map(m =>
            {
                m.ToTable("CourseTags");
                // change name of indices in CourseTags table from Course_Id, Tag_Id, to CourseId and TagId
                // previous way of changing Author_Id to AuthorId cannot be used here b/c the 
                // CourseTags table doesn't have a representation in the domain model. There is no
                // CourseTags class so CourseId and TagId properties cannot be created. The table is purely
                // in the relational model and belongs to a relational database.
                m.MapLeftKey("CourseId");
                m.MapRightKey("TagId");
            });



        }
    }
}
