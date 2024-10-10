

namespace HospitalManagementSystem.Controllers
{
    public class PatientController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public PatientController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]

        public IActionResult Dashboard()
        {
            new PatientDashBoardModel
            return View(new Patient { Appointments = new(),MedicalRecords = new()});
        }
    }
}
