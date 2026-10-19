using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProveOfConcept.ViewModels
{
    public class FirstDifferenceViewModel : IRunAbstractViewModel
    {
        [Key, Column("RunDbFirstDiffId", Order = 0)]
        public int RunDbFirstDiffId { get; set; }
        [ForeignKey("OriginalModel")]
        public int RunDbID { get; set; }
        public int RunPaisID { get; set; }

        public virtual RunViewModel OriginalModel { get; set; }
        public int NumFirstDiff1 { get; set; }
        public int NumFirstDiff2 { get; set; }
        public int NumFirstDiff3 { get; set; }
        public int NumFirstDiff4 { get; set; }
        public int NumFirstDiff5 { get; set; }


        public FirstDifferenceViewModel(RunViewModel realtedRun)
        {
            this.OriginalModel = realtedRun;
            this.RunPaisID = realtedRun.RunPaisID;
            this.RunDbFirstDiffId = realtedRun.RunDbID;
            NumFirstDiff1 = realtedRun.Num2 - realtedRun.Num1;
            NumFirstDiff2 = realtedRun.Num3 - realtedRun.Num2;
            NumFirstDiff3 = realtedRun.Num4 - realtedRun.Num3;
            NumFirstDiff4 = realtedRun.Num5 - realtedRun.Num4;
            NumFirstDiff5 = realtedRun.Num6 - realtedRun.Num5;
        }
        public override string ToString()
        {
            return "First Difference of " + this.OriginalModel.RunPaisID;
        }
    }
}
