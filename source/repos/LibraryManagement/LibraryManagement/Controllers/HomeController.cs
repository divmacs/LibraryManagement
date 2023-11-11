using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        DataAccess dataAccess = new DataAccess();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CaluatePenalty(string fromDate,string toDate, string Country)
        {
            var totalDays = 0;
            int businessDays = 0;
            string resp = "";
            try
            {
                DateTime from = Convert.ToDateTime(fromDate);
                DateTime to = Convert.ToDateTime(toDate);
                int fromWeek = (int)from.DayOfWeek;
                int toWeek = (int)to.DayOfWeek;
                if(fromWeek == 0 || fromWeek == 6 || toWeek == 0 || toWeek == 6)
                {
                    businessDays = 5;
                    totalDays = businessDays;
                }
                totalDays = (int)(to - from).TotalDays;

                //calling DB
                resp = dataAccess.PenaltyForCountry(totalDays, Country);
                if(resp != "")
                {
                    return Content(resp);
                }

            }
            catch (Exception ex)
            {
                log.Debug("Exception occured in HomeController "+ex.Message);
            }
            return Content(resp);
        }

    }
}