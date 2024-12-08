using System.ComponentModel.DataAnnotations;

namespace JOB_PORTAL.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; } 
        public int CompanyId { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Salary { get; set; }
        //public bool Status { get; set; }
    }

}
