using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuvenTur_CRM.Models;
using System.Web.Services;
using System.Web.Script;
using System.Configuration;

namespace GuvenTur_CRM.Controllers
{
    public class CommonController : Controller
    {
        GuvenTurDBModel db = new GuvenTurDBModel();
        // GET: Common
        public ActionResult GetTownList(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Town> townList = db.Town.Where(o => o.County_Id == id).OrderBy(o => o.Town_Name).ToList();
            return Json(townList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVehiclesInfoByServiceId(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Vehicles> vehiclePlate = db.Vehicles.Where(o => o.Service_Id == id).OrderBy(o => o.Vehicle_Plate).ToList();
            return Json(vehiclePlate, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMembersByMemGroupId(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Members> members = db.Members.Where(o => o.Member_Group_Id == id).OrderBy(o => o.Member_Name).ToList();
            return Json(members, JsonRequestBehavior.AllowGet);
        }
    }
}