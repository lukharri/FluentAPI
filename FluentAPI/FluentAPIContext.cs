using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FluentAPI.EntityConfigurations;

namespace CodeFirst
{
    class FLuentAPIContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public FLuentAPIContext()
            : base("name=FluentAPIContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseConfiguration());
        }


        /* OVERRIDING EF CONVENTIONS using Fluent API you must override the OnModelCreating
         * method in your DB context
         * 
         * modelBuilder.Entity<GenericTargetType>()
         *              // USE the following methods to configure
         *              
         *              // override the name of a table and change the schema
         *              .toTable("tbl_Course", "catalog");
         *              
         *              // configure primary key
         *              .HasKey(t => t.ExProperty);
         *              
         *              // configure composite primary key
         *              .HasKey(t => new { t.exProp1, t.exProp2 });
         *              
         *              // changing column names
         *              .Property(t => t.Name) // lambda expression determines the name of the target property
         *              .HasColumnName("ColumnName");
         *              
         *              // change the type of a column
         *              .Property(t => t.Name)
         *              .HasColumnType("varchar");
         *              
         *              // change the order of columns in a table
         *              .Property(t => t.Name)
         *              .HasColumnOrder(2);
         *              
         *              // turn off identity column
         *              .Property(t => t.PrimaryKeyName)
         *              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
         *              
         *              // override nullable types
         *              .Property(t => t.Name)
         *              .IsRequired();
         *              
         *              // change the length of strings
         *              .Property(t => t.Name)
         *              .HasMaxLength(255); 
         *              // or use IsMaxLength() - sets the type of the prop to nvarchar of max
         *               
         */


        /* CONFIGURING RELATIONSHIPS Using Fluent API
         * Relationships can be configured starting in either direction
         * 
         * Ex. 1 - One-to-Many
         * Author(1)-----Course(*)
         * modelBuilder.Entity<Author>()
         *          // an author has many courses
         *          .HasMany(a => a.Courses) 
         *          // a course has only one author
         *          // supply a lambda expression that specifies a target property
         *          // c.Author represents the author property in the course class that is a
         *          // naviagation property to the end of the relationship
         *          .WithRequired(c => c.Author) 
         *          // can optionally specify the foreign key
         *          
         * Ex. 2 - Many-to-Many
         * Course(*)------Tag(*)
         * modelBuilder.Entity<Course>()
         *          // a course has many tags
         *          .HasMany(c => c.Tags) // LE specifies the name of the navigation property in the course class
         *          // a tag belongs to many courses
         *          .WithMany(t => t.Courses)
         *          // override the name of the intermediary table 
         *          .Map(m => m.ToTable("CourseTags")
         *          
         * Ex. 3 One-to-Zero/One
         * Course(1)-----Caption(0...1)
         * modelBuilder.Entity<Course>()
         *          // a course can have zero or one captions
         *          .HasOptional(c => c.Caption)
         *          // a caption has one course
         *          .WithRequired(c => c.Course)
         *          
         * Ex. 4 One-to-One
         * Must specify which type is the parent(principal) and which is the child(dependent)
         * Cannot use HasRequired or WithRequired b/c EF doesn't know which is the parent/child
         * Course is the parent b/c we cannot have a cover without a course
         * A course has one cover and a cover belongs to one course
         * LEFT to RIGHT configuration
         * Course(1)-----Cover(1)
         * modelBuilder.Entity<Course>()
         *          .HasRequired(c => c.Cover)
         *          .WithRequiredPrincipal(c => c.Course)
         *          
         * RIGHT to LEFT configuration
         * modelBuilder.Entity<Cover>()
         *          .HasRequired(c => c.Course)
         *          .WithRequiredDependent(c => c.Cover)
         */
    }
}
