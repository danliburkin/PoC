using ProveOfConcept.Enums;
using ProveOfConcept.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace ProveOfConcept.Managers
{
    public class StatisticsCalculator
    {
        [Key, Column("RunDbID", Order = 0)]
        [ForeignKey("OriginalModel")]
        public int RunDbIdViewMod { get; set; }

        [Key, Column("RunPaisID", Order = 1)]
        [ForeignKey("OriginalModel")]
        public int RunPaisIDViewMod { get; set; }
        public virtual RunViewModel OriginalModel { get; set; }

        public StatisticCalculationType CalculationType { get; set; }
        public double Average { get; set; }
        public double StandardDeviation { get; set; }
        public double LeftAverage { get; set; }
        public double RightAverage { get; set; }
        public double LeftStandardDeviation { get; set; }
        public double RightStandardDeviation { get; set; }

        delegate bool IsGreater(int x, bool i);
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
        private List<int> DevideLeftRight(List<int> origList, IsGreater f, bool isRightSide)
        {
            return origList.Select((numberToAverage, index) => new { numberToAverage, index }).Where(x => (f(x.index, isRightSide))).Select(x => x.numberToAverage).ToList();
        }
        private double CalculateStandardDeviation(List<int> list)
        {
            return Math.Pow((list.Select(x => Math.Pow((x - Average), 2)).ToList().Sum() / (list.Count - 1)), 0.5);
        }
        public StatisticsCalculator(IRunAbstractViewModel realtedRun)
        {
            //this.OriginalRun = realtedRun;
            this.RunPaisIDViewMod = realtedRun.RunPaisID;
            List<int> fullList = new List<int>();
            List<int> rightList = new List<int>();
            List<int> leftList = new List<int>();
            Type localVariableType = realtedRun.GetType();
            PropertyInfo[] properties = null;
            if (realtedRun is RunViewModel)
            {
                properties = typeof(RunViewModel).GetProperties();
                CalculationType = StatisticCalculationType.OriginalRun;
            }
            else
            {
                if (realtedRun is FirstDifferenceViewModel)
                {
                    properties = typeof(FirstDifferenceViewModel).GetProperties();
                    CalculationType = StatisticCalculationType.FirstDifference;
                }
            }
            fullList = properties.Where(x => x.Name.Substring(0, 3) == "Num" && x.Name != "NumS").Select(z => z.GetValue(realtedRun, null)).OfType<int>().ToList();
            Average = fullList.Average();
            StandardDeviation = CalculateStandardDeviation(fullList);
            //StandardDeviation = Math.Pow((fullList.Select(x => Math.Pow((x - Average), 2)).ToList().Sum()/(fullList.Count-1)), 0.5);
            //leftList = fullList.Select((numberToAverage, index) => new { numberToAverage, index }).Where(x => x.index < 3).Select(x => x.numberToAverage).ToList();
            leftList = DevideLeftRight(fullList, IsGraterThenThree, false);
            LeftAverage = leftList.Average();
            //LeftAverage = fullList.Select((numberToAverage, index) => new { numberToAverage, index }).Where(x => x.index < 3).Select(x => x.numberToAverage).ToList().Average();
            rightList = DevideLeftRight(fullList, IsGraterThenThree, true);
            RightAverage = rightList.Average();
            //RightAverage = fullList.Select((numberToAverage, index) => new { numberToAverage, index }).Where(x => x.index >= 3).Select(x => x.numberToAverage).ToList().Average();
        }
        public override string ToString()
        {
            return "Calculated Statistics for " + this.CalculationType.ToString() + " - ";
            //+ this.OriginalRun.RunPaisID;
        }

    }
}
