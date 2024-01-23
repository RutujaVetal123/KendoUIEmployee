using KendoEmployeeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using KendoEmployeeApp.Models.SubModels;
using Kendo.Mvc.Extensions;
//using Kendo.Mvc.Extensions;

namespace KendoEmployeeApp.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        private static RegistrationEntities db = new RegistrationEntities();
        public JsonResult GetData([DataSourceRequest] DataSourceRequest request)
        {
            _Employee employee = new _Employee();
            var i = db.Employees.ToList();
            List<_Employee> list = new List<_Employee>();
            list.Clear();

            i.ForEach(j =>
            {
                employee = new _Employee();
                employee.EmpId = j.EmpId;
                employee.EmpName = j.EmpName;
                employee.Address = j.Address;
                list.Add(employee);
            });
           
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Create_Employee([DataSourceRequest] DataSourceRequest request, Employee employee)
        {
            using (RegistrationEntities db = new RegistrationEntities()) 
            {

                if (employee != null && ModelState.IsValid)
                {
                    Employee emp = new Employee();
                    emp.EmpName = employee.EmpName;
                    emp.Address = employee.Address;
                    db.Employees.Add(emp);

                    db.SaveChanges();
                    employee.EmpId = emp.EmpId;
                }

                return Json(new[] { employee }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Update_Employee([DataSourceRequest] DataSourceRequest request, Employee employee) 
        {
            using (RegistrationEntities db = new RegistrationEntities())
            {

                if (employee != null && ModelState.IsValid)
                {
                    Employee emp = db.Employees.Single(c => c.EmpId == employee.EmpId);
                    emp.EmpName = employee.EmpName;
                    emp.Address = employee.Address;
                }
                    db.SaveChanges();
                    
                

                return Json(ModelState.IsValid?true:ModelState.ToDataSourceResult());
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Delete_Employee([DataSourceRequest] DataSourceRequest request, Employee employee)
        {
            using (RegistrationEntities db = new RegistrationEntities())
            {

                if (employee != null && ModelState.IsValid)
                {
                    Employee emp = db.Employees.Single(c => c.EmpId == employee.EmpId);
                    db.Employees.Remove(emp);
                }
                db.SaveChanges();



                return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
            }
        }

    }
}