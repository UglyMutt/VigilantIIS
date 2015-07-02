using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VigilantIIS.WebDashboard.Repository;

namespace VigilantIIS.WebDashboard.Controllers
{

    //[Authorize]
    public class HomeController : Controller
    {

        private readonly IMongoDbRepository _repository;

        public HomeController() : this(new MongoDbRepository()) { }
        public HomeController(IMongoDbRepository repository)
        {
            _repository = repository;
        }
        public async Task<ActionResult> Index()
        {
            var list = await _repository.GetAllRecords();
            ViewBag.requests = list.OrderByDescending(r => r.RequestedOn);
            return View();
        }
    }
}
