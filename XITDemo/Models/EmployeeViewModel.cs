using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XITDemo.Models
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            this.Countries = new List<SelectListItem>();
            this.States = new List<SelectListItem>();
            this.Cities = new List<SelectListItem>();
            this.Employees = new Employee();
        }

        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> Cities { get; set; }

        public List<CheckModels> Hobbies { get; set; }

        public Employee Employees { get; set; }

      

        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }


        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
        //public int CityId { get; set; }
        //public int StateId { get; set; }
        //public int CountryId { get; set; }
        public string PinCode { get; set; }
        public string EmployeePhoto { get; set; }
        //public string Hobbies { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual Department Department { get; set; }
        public virtual State State { get; set; }

        public class CheckModels
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Checked { get; set; }
        }

    }
}