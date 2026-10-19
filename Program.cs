using ProveOfConcept.DAL;
using ProveOfConcept.Managers;
using ProveOfConcept.ViewModels;
using System;
using System.Collections.Generic;

namespace ProveOfConcept
{
    class Program
    {
        static void Main(string[] args)
        {
            List<RunViewModel> fullReadList;
            List<FirstDifferenceViewModel> firstDiffList = new List<FirstDifferenceViewModel>();
            List<RunCalculatedDataViewModel> origunalStatisticsList = new List<RunCalculatedDataViewModel>();
            List<RunCalculatedDataViewModel> firstDiffStatisticsList = new List<RunCalculatedDataViewModel>();
            int indexRows = CsVManager.Instance.ReadFile(@"C:\Users\local\Desktop\Lotto.csv", out fullReadList);
            Console.WriteLine("Hello World!");
            Console.WriteLine("Read {0} rows", indexRows);
            foreach (RunViewModel currentRunView in fullReadList)
            {
                firstDiffList.Add(new FirstDifferenceViewModel(currentRunView));
                origunalStatisticsList.Add(new RunCalculatedDataViewModel(currentRunView));
                firstDiffStatisticsList.Add(new RunCalculatedDataViewModel(new FirstDifferenceViewModel(currentRunView)));
            }

            using (RunTestContext dbContext = new RunTestContext())
            {
                dbContext.RunViews.AddRange(fullReadList);
                dbContext.FirstDifferenceViewModels.AddRange(firstDiffList);
                dbContext.RunCalculatedDataViewModels.AddRange(origunalStatisticsList);
                dbContext.RunCalculatedDataViewModels.AddRange(firstDiffStatisticsList);
                dbContext.SaveChanges();
            }
        }
    }
}
