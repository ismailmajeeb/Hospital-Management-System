using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.MedicalRecords
{
    public class EditMedicalRecordModel
    {
        public int Id { get; set; }
        [Required]
        public string? Discription { get; set; }
        [Required]
        public string? Diagnosis { get; set; }
        [Required]
        public string? Treatment { get; set; }
        [Required]
        public int AppointmentId { get; set; }
        [Required]
    }
}
