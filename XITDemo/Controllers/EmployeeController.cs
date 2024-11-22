using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using XITDemo.Models;
using static XITDemo.Models.EmployeeViewModel;

namespace XITDemo.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        XITDBEntities db = new XITDBEntities();
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "FirstName")
            {
                var first = db.Employees.Where(c => c.FirstName.StartsWith(search) || search == null).ToList();
                return View(first);
            }
            else if (searchBy == "LastName")
            {
                var last = db.Employees.Where(c => c.LastName.StartsWith(search) || search == null).ToList();
                return View(last);
            }
            else
            {
                var email = db.Employees.Where(c => c.Email == search || search == null).ToList();
                return View(email);

            }
        }
            //public ActionResult Index()
            //{
                        //    //EmployeeViewModel model = new EmployeeViewModel();
            //    //foreach (var country in db.Countries)
            //    //{
            //    //    model.Countries.Add(new SelectListItem { Text = country.CountryName, Value = country.CountryId.ToString() });
            //    //}
            //    return View(db.Employees.ToList());
            //}
            //[HttpPost]
            //public ActionResult Index()
            //{
            //    //EmployeeViewModel model = new EmployeeViewModel();

            //    //foreach (var country in db.Countries)
            //    //{
            //    //    model.Countries.Add(new SelectListItem { Text = country.CountryName, Value = country.CountryId.ToString() });
            //    //}

            //    //if (countryId.HasValue)
            //    //{
            //    //    var states = (from state in db.States
            //    //                  where state.CountryId == countryId.Value
            //    //                  select state).ToList();
            //    //    foreach (var state in states)
            //    //    {
            //    //        model.States.Add(new SelectListItem { Text = state.StateName, Value = state.StateId.ToString() });
            //    //    }

            //    //    if (stateId.HasValue)
            //    //    {
            //    //        var cities = (from city in db.Cities
            //    //                      where city.StateId == stateId.Value
            //    //                      select city).ToList();
            //    //        foreach (var city in cities)
            //    //        {
            //    //            model.Cities.Add(new SelectListItem { Text = city.CityName, Value = city.CityId.ToString() });
            //    //        }
            //    //    }

            //    //}


            //     return View();
            //}

            //    if (Searchby == "Gender")
            //    {
            //        var model = db.Employees.Where(emp => emp.Gender == search || search == null).ToList();
            //        return View(model);

            //    }
            //    else
            //    {
            //        var model = db.Employees.Where(emp => emp.FirstName.StartsWith(search) || search == null).ToList();
            //        return View(model);
            //    }
            //}

            public ActionResult Manage(int id=0)
        {
            var emp = db.Employees.Find(id);
            //            EmployeeViewModel allmodel = new EmployeeViewModel();

            ////            var emp = db.Employees.Find(id);

            //            var dtEmployee = (from obj in db.Employees
            //                              where obj.Id == id
            //                              select obj).FirstOrDefault();

            //            allmodel.Employees = dtEmployee;

            //foreach (var country in db.Countries)
            //{
            //    model.Countries.Add(new SelectListItem { Text = country.CountryName, Value = country.CountryId.ToString() });
            //}
            

            ViewBag.lstDept = db.Departments.ToList().Select(c => new SelectListItem()
            {
                Text = c.DepartmentName,
                Value = c.DepartmentId.ToString()
            });
            ViewBag.lstCountry = db.Countries.ToList().Select(c => new SelectListItem()
            {
                Text = c.CountryName,
                Value = c.CountryId.ToString()
            });
            ViewBag.lstState = db.States.ToList().Select(c => new SelectListItem()
            {
                Text = c.StateName,
                Value = c.StateId.ToString()
            });
            ViewBag.lstCity = db.Cities.ToList().Select(c => new SelectListItem()
            {
                Text = c.CityName,
                Value = c.CityId.ToString()
            });
                      

            return View(emp);

         
        }

        

        public ActionResult Save(Employee employee, HttpPostedFileBase httpPostedFileBase)
        {
            if (employee.Id > 0)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }
            else
            {
                if (ModelState.IsValid)
                {
                   // if (httpPostedFileBase.ContentLength > 0 && httpPostedFileBase!= null)
                        if ( httpPostedFileBase != null)
                        {
                        string filename = Path.GetFileName(httpPostedFileBase.FileName);
                        string filepath = Path.Combine(Server.MapPath("~/FileUpload"), filename);
                        httpPostedFileBase.SaveAs(filepath);

                        employee.EmployeePhoto = filename;
                    
                        db.Employees.Add(employee);


                        db.SaveChanges();

                    }
                    else
                    {
                        db.Employees.Add(employee);

                        db.SaveChanges();
                    }
                }
               
                ViewBag.message = "Data save successfully";
                // ViewBag.Company=new SelectList(db.Companies,"CompanyId","CompanyName");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Employee employee = db.Employees.Where(c => c.Id == id).SingleOrDefault();
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}