using ProveOfConcept.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace ProveOfConcept.ViewModels
{
    public class RunCalculatedDataViewModel
    {
        [Key, Column("RunDBCalculationID", Order = 0)]
        public int RunDBCalculationID { get; set; }
        public int RunPaisIDViewMod { get; set; }
        [ForeignKey("OriginalModel")]
        public int RunDbID { get; set; }
        public virtual RunViewModel OriginalModel { get; set; }

        public string CalculationType { get; set; }
        public double Average { get; set; }
        public double StandardDeviation { get; set; }
        public double LeftAverage { get; set; }
        public double RightAverage { get; set; }
        public double LeftStandardDeviation { get; set; }
        public double RightStandardDeviation { get; set; }

        delegate bool IsGreter(int x, bool i);
        private List<int> fullList = new List<int>();
        private List<int> rightList = new List<int>();
        private List<int> leftList = new List<int>();
        private readonly PropertyInfo[] properties = null;
        private bool IsGraterThenThree(int x, bool i)
        {

            if (i == true)
            {
                return x > 3;
            }
            else
            {
                return x < 3;
            }
        }
        private List<int> DevideLeftRight(List<int> origList, IsGreter f, bool isRightSide)
        {
            return origList.Select((numberToAverage, index) => new { numberToAverage, index }).Where(x => (f(x.index, isRightSide))).Select(x => x.numberToAverage).ToList();
        }
        private double CalculateStandardDeviation(List<int> list)
        {
            return Math.Pow((list.Select(x => Math.Pow((x - Average), 2)).ToList().Sum() / (list.Count - 1)), 0.5);
        }
        private void CalculateStatistics(IRunAbstractViewModel localRun)
        {
            fullList = properties.Where(x => x.Name.Substring(0, 3) == "Num" && x.Name != "NumS").Select(z => z.GetValue(localRun, null)).OfType<int>().ToList();
            Average = fullList.Average();
            StandardDeviation = CalculateStandardDeviation(fullList);
            leftList = DevideLeftRight(fullList, IsGraterThenThree, false);
            LeftAverage = leftList.Average();
            rightList = DevideLeftRight(fullList, IsGraterThenThree, true);
            RightAverage = rightList.Average();
        }
        public RunCalculatedDataViewModel(RunViewModel realtedRun)
        {
            this.OriginalModel = realtedRun;
            this.RunPaisIDViewMod = realtedRun.RunPaisID;

            Type localVariableType = realtedRun.GetType();
            properties = typeof(RunViewModel).GetProperties();
            CalculationType = Enum.GetName(typeof(StatisticCalculationType), 0);
            CalculateStatistics(realtedRun);
        }
        public RunCalculatedDataViewModel(FirstDifferenceViewModel realtedRun)
        {
            this.OriginalModel = realtedRun.OriginalModel;
            this.RunPaisIDViewMod = realtedRun.OriginalModel.RunPaisID;
            properties = typeof(FirstDifferenceViewModel).GetProperties();
            CalculationType = Enum.GetName(typeof(StatisticCalculationType), 1);
            CalculateStatistics(realtedRun);
        }
        public override string ToString()
        {
            return "Calculated Statistics for " + this.CalculationType.ToString() + " - " + this.RunPaisIDViewMod;
        }

    }
}
