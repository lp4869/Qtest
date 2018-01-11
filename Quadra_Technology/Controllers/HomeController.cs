using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quadra_Technology.Service;
using Quadra_Technology.Service.Models;
using System.Web.Script.Serialization;

namespace Quadra_Technology.Controllers
{
    public class HomeController : Controller
    {
        InformationService service = new InformationService();
        public ActionResult Index()
        {
            @ViewBag.menuList = service.LoadDepartment();

            return View();
        }
        public PartialViewResult getDepartmentList()
        {
            var result = service.LoadDepartment();
            return PartialView("DepartmentList", result);
        }
        [HttpPost]
        public ContentResult getStaff(int id)
        {
            var result = service.getStaff(id);
            //var test = new object { result };
            var json = new JavaScriptSerializer().Serialize(result);
            // return Json(result, JsonRequestBehavior.AllowGet);
            return Content(json, "application/json; charset=utf-8");
        }
        public ActionResult EditForm(Guid id)
        {
            StaffModel result = service.getOneStaffdata(id);
            @ViewBag.Department = result.department;
            @ViewBag.Title = "แก้ไขข้อมูลพนักงาน";
            return View("EditForm", result);
        }
        public ActionResult Addstaff()
        {
            @ViewBag.Title = "เพิ่มข้อมูลพนักงาน";
            
            return View("EditForm");
        }
        [HttpGet]
        public PartialViewResult SomeAction()
        {
            return PartialView("View");
        }
        [HttpPost]
        public ContentResult SaveStaff(StaffModel staff)
        {
            String Status = string.Empty;
            if (Guid.Empty.Equals(staff.guid))
            {
                Status = service.StaffAdd(staff);
            }
            else
            {
                Status = service.StaffUpdate(staff);
            }
            
            return Content(Status);
        }
        [HttpPost]
        public ContentResult Deldata(Guid id)
        {
            String Status = service.StaffDelete(id);
            //String Status = "For Test delete row";
            return Content(Status);
        }
    }
}