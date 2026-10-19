using ProveOfConcept.ViewModels;
using System.Data.Entity;

namespace ProveOfConcept.DAL
{
    public class RunTestContext : DbContext
    {

        public DbSet<RunViewModel> RunViews { get; set; }
        public DbSet<FirstDifferenceViewModel> FirstDifferenceViewModels { get; set; }
        public DbSet<RunCalculatedDataViewModel> RunCalculatedDataViewModels { get; set; }
        public RunTestContext()
        {

        }
    }
}

