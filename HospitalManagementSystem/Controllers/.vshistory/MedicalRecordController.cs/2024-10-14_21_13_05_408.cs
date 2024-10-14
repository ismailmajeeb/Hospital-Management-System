using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class MedicalRecord : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
