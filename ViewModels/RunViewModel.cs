using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace ProveOfConcept.ViewModels
{

    public class RunViewModel : IRunAbstractViewModel
    {

        [Key, Column("RunDbID", Order = 0)]

        public int RunDbID { get; set; }
        //[Key, Column("RunPaisID", Order = 1)]
        public int RunPaisID { get; set; }
        public DateTime RunDate { get; set; }
        public int Num1 { get; set; }
        public int Num2 { get; set; }
        public int Num3 { get; set; }
        public int Num4 { get; set; }
        public int Num5 { get; set; }
        public int Num6 { get; set; }
        public int NumS { get; set; }
        public RunViewModel()
        {

        }
        public RunViewModel(RunViewModel cloneRunModel) //It's to clone
        {
            PropertyInfo[] properties = typeof(RunViewModel).GetProperties();
            foreach (PropertyInfo currentProperty in properties)
            {
                currentProperty.SetValue(this, currentProperty.GetValue(cloneRunModel));
            }
        }
        public override string ToString()
        {
            return this.RunPaisID + " " + this.RunDate.ToString("dd/MM/yyyy");
        }
    }
}
