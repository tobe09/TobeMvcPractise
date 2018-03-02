using System.Data.Entity;

namespace TobeMvcPractise.Models
{   
    public class MyDbContext : DbContext              //for entity framework
    {
        public MyDbContext()
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Course> Courses { get; set; }


        private static MyDbContext _myDbContext;
        //singleton pattern not good for EF DbContext, tracking db state can introduce bugs
        public static MyDbContext myDbContextSingleton
        {
            get
            {
                if (_myDbContext == null)
                {
                    lock (new object())
                    {
                        if (_myDbContext == null) _myDbContext = new MyDbContext();
                    }
                }
                return _myDbContext;
            }
        }
    }
}