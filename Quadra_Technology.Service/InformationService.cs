using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quadra_Technology.DB;
using Quadra_Technology.Service.Models;

namespace Quadra_Technology.Service
{
    public class InformationService
    {
        public List<DepartmentModel> LoadDepartment()
        {
            List<DepartmentModel> departmentList;
            try
            {

                using (Quadra_TechnologyEntities db = new Quadra_TechnologyEntities())
                {
                    departmentList = db.Department
                        .Select(s => new DepartmentModel
                        {
                            DepartmentID = s.DepartmentID,
                            DepartmentName = s.DepartmentName,
                            Description = s.Description,
                            DepartmentIcon = s.DepartmentIcon,
                            CreatedOn = s.CreatedOn
                        }).OrderBy(o => o.CreatedOn).ToList();
                    db.Dispose();
                }
            }
            catch (Exception ex) {
                departmentList = null;
            }
            return departmentList;
        }
        public dataClass getStaff(int id)
        {
            List<StaffModel> staffmodel;
            IQueryable<Staff> temp;
            using (Quadra_TechnologyEntities db = new Quadra_TechnologyEntities())
            {
                if (id == 0)
                {
                    temp = db.Staff;
                }
                else
                {
                    temp = db.Staff.Where(w => w.DepartmentID == id);
                }
                staffmodel = temp.Join(db.Department, staff => staff.DepartmentID, dep => dep.DepartmentID, (staff, dep) => new { staff = staff, dep = dep })
                    .Select(s => new StaffModel
                    {
                        department = s.dep.DepartmentName,
                        name = s.staff.StaffName + " " + s.staff.StaffLastName,
                        address = s.staff.StaffAddress,
                        birthdate = s.staff.StaffbirthDate.Value,
                        email = s.staff.StaffEmail,
                        phone = s.staff.StaffTel,
                        position = s.staff.Position,
                        workingday = s.staff.Begin_Working_day.Value,
                        guid = s.staff.StaffId

                    }).ToList();
                db.Dispose();
            }

            return new dataClass
            {
                data = staffmodel
            };
        }
        public StaffModel getOneStaffdata(Guid id)
        {
            StaffModel staffmodel;
            IQueryable<Staff> temp;
            using (Quadra_TechnologyEntities db = new Quadra_TechnologyEntities())
            {
                temp = db.Staff.Where(w => w.StaffId == id);

                staffmodel = temp.Join(db.Department, staff => staff.DepartmentID, dep => dep.DepartmentID, (staff, dep) => new { staff = staff, dep = dep })
                    .Select(s => new StaffModel
                    {
                        department = s.dep.DepartmentName,
                        name = s.staff.StaffName,
                        lastname = s.staff.StaffLastName,
                        address = s.staff.StaffAddress,
                        birthdate = s.staff.StaffbirthDate.Value,
                        email = s.staff.StaffEmail,
                        phone = s.staff.StaffTel,
                        position = s.staff.Position,
                        workingday = s.staff.Begin_Working_day.Value,
                        createdOn = s.staff.CreatedOn,
                        guid = s.staff.StaffId
                    }).FirstOrDefault();
                db.Dispose();
            }
            return staffmodel;
        }
        public String StaffDelete(Guid id)
        {
            String Status = String.Empty;
            try
            {
                Staff staffDestination = new Staff { StaffId = id };
                using (Quadra_TechnologyEntities db = new Quadra_TechnologyEntities())
                {
                    Status = db.Staff.Where(w => w.StaffId.Equals(id)).Select(s => new { fullname = s.StaffName + " " + s.StaffLastName }).FirstOrDefault().ToString();
                    db.Entry(staffDestination).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    db.Dispose();
                }
                Status = "Deleted " + Status + " success...";
                
            }
            catch (Exception ex)
            {
                Status = Error_handle(ex);
            }
            return Status;
        }
        public String StaffAdd(StaffModel staff)
        {
            String Status = String.Empty;

            try
            {
                Staff staffDestination = SetStaff(staff);
                using (Quadra_TechnologyEntities db = new Quadra_TechnologyEntities())
                {
                    Status = staff.name + " " + staff.lastname;
                    staffDestination.StaffId = Guid.NewGuid();
                    staffDestination.CreatedOn = staffDestination.ModifyOn;
                    staffDestination.DepartmentID = db.Department.Where(w => w.DepartmentName == staff.department).Select(s => s.DepartmentID).FirstOrDefault();
                    db.Entry(staffDestination).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                    db.Dispose();
                }
                Status = "Create profile " + Status + " success...";
                
            }
            catch (Exception ex)
            {
                Status = Error_handle(ex);
            }
            return Status;
        }
        public String StaffUpdate(StaffModel staff)
        {
            String Status = String.Empty;

            try
            {
                Staff staffDestination = SetStaff(staff);
                using (Quadra_TechnologyEntities db = new Quadra_TechnologyEntities())
                {
                    Status = staff.name + " " + staff.lastname;
                    staffDestination.DepartmentID = db.Department.Where(w => w.DepartmentName == staff.department).Select(s => s.DepartmentID).FirstOrDefault();
                    db.Entry(staffDestination).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    db.Dispose();
                }
                Status = "Updated profile "+ Status + " success...";
                
            }
            catch (Exception ex)
            {
                Status = Error_handle(ex);
            }
            return Status;
        }
        private String Error_handle(Exception ex) { return "Error : " + ex.Message; }
        private Staff SetStaff(StaffModel staffSource)
        {
            Staff staff = new Staff();
            staff.StaffId = staffSource.guid;
            //staff.DepartmentID = staffSource.department;
            staff.StaffName = staffSource.name;
            staff.StaffLastName = staffSource.lastname;
            staff.StaffbirthDate = staffSource.birthdate;
            staff.StaffAddress = staffSource.address;
            staff.StaffTel = staffSource.phone;
            staff.StaffEmail = staffSource.email;
            staff.Position = staffSource.position;
            staff.Begin_Working_day = staffSource.workingday;
            staff.CreatedOn = staffSource.createdOn;
            staff.ModifyOn = DateTime.Now;
            return staff;
        }
    }
}
