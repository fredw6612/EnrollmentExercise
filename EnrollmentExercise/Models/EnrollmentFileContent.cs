using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrollmentExercise.Models
{
    public class EnrollmentFileContent : FileContentBase
    {
        public EnrollmentFileContent()
        {
            //useful if other FileContent types are added
            FileReaderType = typeof(EnrollmentFileReader);
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string PlanType { get; set; }
        [Required]
        public DateTime EffectiveDate { get; set; }

        public string Status { get { return (Age >= 18 && (EffectiveDate - DateTime.Today).TotalDays < 30) ? "Accepted" : "Rejected"; } }

        [NotMapped]
        public int Age { get { return (int)Math.Floor((DateTime.Today - DOB).TotalDays / 365); } }

        public override string ToString()
        {
            return Status + ", " + FirstName + ", " + LastName + ", " + 
                   DOB.ToShortDateString() + ", " + PlanType + ", " + EffectiveDate.ToShortDateString();
        }
    }
}
