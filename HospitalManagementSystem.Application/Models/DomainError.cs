
namespace HospitalManagementSystem.Application.Models
{
    public class DomainError
    {
        public DomainError(string error)
        {
            errors.Add(error);

        }
        public DomainError(IEnumerable<string> errors)
        {
            this.errors = errors.ToList() ?? new();
        }

        public List<string> errors { get; set; } = new();
    }

}
