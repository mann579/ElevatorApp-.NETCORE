using Microsoft.AspNetCore.Mvc;
using NLog;

namespace ElevatorApp.Controllers
{
    public class BaseController : Controller
    {
        private readonly Logger _logger;
        protected Logger Logger { get { return _logger; } }

        public BaseController()
        {
            _logger = LogManager.GetLogger(GetType().FullName);
        }
    }
}
