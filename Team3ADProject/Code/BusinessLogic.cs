﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Team3ADProject.Model;
using System.Transactions;

namespace Team3ADProject.Code
{

    public class BusinessLogic
    {
        public static LogicUniversityEntities context = new LogicUniversityEntities();

        public static List<getpendingrequestsbydepartment_Result> ViewPendingRequests(string deptid)
        {
            List<getpendingrequestsbydepartment_Result> list = new List<getpendingrequestsbydepartment_Result>();
            return list = context.getpendingrequestsbydepartment(deptid).ToList();
        }

        public static string getdepartment(string userid)
        {
            var k = (from employee in context.employees where employee.user_id == userid select employee);
            string dept = k.FirstOrDefault().department_id;
            return dept;
        }

        public static List<spAcknowledgeDistributionList_Result> ViewAcknowledgementList(int collection_id)
        {
            List<spAcknowledgeDistributionList_Result> list = new List<spAcknowledgeDistributionList_Result>();
            return list = context.spAcknowledgeDistributionList(collection_id).ToList();
        }

        public static getpendingrequestdetails_Result getdetails(string id)
        {
            return context.getpendingrequestdetails(id).ToList().Single();
        }

        public static List<getitemdetails_Result> pendinggetitemdetails(string reqid)
        {
            List<getitemdetails_Result> list = new List<getitemdetails_Result>();
            return list = context.getitemdetails(reqid).ToList();
        }

        public static void approvestatus(string id)
        {
            var k = from requisition_order in context.requisition_order where requisition_order.requisition_id == id select requisition_order;
            k.FirstOrDefault().requisition_status = "Approved";
            context.SaveChanges();
        }
        public static void rejectstatus(string id)
        {
            var k = from requisition_order in context.requisition_order where requisition_order.requisition_id == id select requisition_order;
            k.FirstOrDefault().requisition_status = "Rejected";
            context.SaveChanges();
        }
        public static List<getrequesthistory_Result> gethistory(string dept)
        {
            List<getrequesthistory_Result> list = new List<getrequesthistory_Result>();
            return list = context.getrequesthistory(dept).ToList();
        }
        public static List<getrequesthistory_Result> gethistorybyname(string name, string dept)
        {
            List<getrequesthistory_Result> list = new List<getrequesthistory_Result>();
            list = (from requisitionorder in context.requisition_order
                    join employee in context.employees on requisitionorder.employee_id equals employee.employee_id
                    join requisitionorderdetails in context.requisition_order_detail on requisitionorder.requisition_id equals requisitionorderdetails.requisition_id
                    where (employee.department_id.Equals(dept) && employee.employee_name.Contains(name))
                    group requisitionorderdetails by requisitionorderdetails.requisition_id into reqgp
                    select new
                    {
                        id = reqgp.FirstOrDefault().requisition_id,
                        Date = reqgp.FirstOrDefault().requisition_order.requisition_date,
                        Name = reqgp.FirstOrDefault().requisition_order.employee.employee_name,
                        status = reqgp.FirstOrDefault().requisition_order.requisition_status,
                        Sum = reqgp.Sum(pt => pt.item_requisition_price)
                    }).ToList().
                          Select(x => new getrequesthistory_Result()
                          {
                              id = x.id,
                              Date = x.Date,
                              Name = x.Name,
                              status = x.status,
                              Sum = x.Sum,
                          }).ToList();
            return list;
        }
        public static List<getrequesthistory_Result> gethistorybynameandstatus(string name, string dept, string status)
        {
            List<getrequesthistory_Result> list = new List<getrequesthistory_Result>();
            list = (from requisitionorder in context.requisition_order
                    join employee in context.employees on requisitionorder.employee_id equals employee.employee_id
                    join requisitionorderdetails in context.requisition_order_detail on requisitionorder.requisition_id equals requisitionorderdetails.requisition_id
                    where (employee.department_id.Equals(dept) && employee.employee_name.Contains(name) && requisitionorder.requisition_status.Equals(status))
                    group requisitionorderdetails by requisitionorderdetails.requisition_id into reqgp
                    select new
                    {
                        id = reqgp.FirstOrDefault().requisition_id,
                        Date = reqgp.FirstOrDefault().requisition_order.requisition_date,
                        Name = reqgp.FirstOrDefault().requisition_order.employee.employee_name,
                        status = reqgp.FirstOrDefault().requisition_order.requisition_status,
                        Sum = reqgp.Sum(pt => pt.item_requisition_price)
                    }).ToList().
                          Select(x => new getrequesthistory_Result()
                          {
                              id = x.id,
                              Date = x.Date,
                              Name = x.Name,
                              status = x.status,
                              Sum = x.Sum,
                          }).ToList();
            return list;
        }

        public static List<getrequesthistory_Result> gethistorybystatus(string dept, string status)
        {
            List<getrequesthistory_Result> list = new List<getrequesthistory_Result>();
            list = (from requisitionorder in context.requisition_order
                    join employee in context.employees on requisitionorder.employee_id equals employee.employee_id
                    join requisitionorderdetails in context.requisition_order_detail on requisitionorder.requisition_id equals requisitionorderdetails.requisition_id
                    where (employee.department_id.Equals(dept) && requisitionorder.requisition_status.Equals(status))
                    group requisitionorderdetails by requisitionorderdetails.requisition_id into reqgp
                    select new
                    {
                        id = reqgp.FirstOrDefault().requisition_id,
                        Date = reqgp.FirstOrDefault().requisition_order.requisition_date,
                        Name = reqgp.FirstOrDefault().requisition_order.employee.employee_name,
                        status = reqgp.FirstOrDefault().requisition_order.requisition_status,
                        Sum = reqgp.Sum(pt => pt.item_requisition_price)
                    }).ToList().
                          Select(x => new getrequesthistory_Result()
                          {
                              id = x.id,
                              Date = x.Date,
                              Name = x.Name,
                              status = x.status,
                              Sum = x.Sum,
                          }).ToList();
            return list;
        }

        public static List<employee> getemployeenames(string dept)
        {
            //var q=from employee in context.employees where employee.department_id.Equals(dept) select employee.employee_name;
            return context.employees.Where(x => x.department_id == dept).ToList();

        }

        public static int getemployeeid(string name)
        {
            var q = from employee in context.employees where employee.employee_name == name select employee.employee_id;
            return q.FirstOrDefault();
        }

        public static void updatetemporaryhead(int id, string dept)
        {
            var q = from department in context.departments where department.department_id == dept select department;
            department d = q.FirstOrDefault();
            d.temp_head_id = id;
            context.SaveChanges();
        }

        public static string gettemporaryheadname(string dept)
        {
            var q = from department in context.departments
                    join employee in context.employees on
                    department.temp_head_id equals employee.employee_id
                    where department.department_id.Equals(dept)
                    select employee.employee_name;
            return q.FirstOrDefault();

        }

        public static void revoketemporaryhead(string dept)
        {
            var q = from department in context.departments where department.department_id == dept select department;
            department d = q.FirstOrDefault();
            d.temp_head_id = null;
            context.SaveChanges();

        }

        public static List<employee> getemployeenamebysearch(string dept, string name)
        {
            var q = from e in context.employees
                    join d in context.departments on e.department_id equals d.department_id
                    where e.employee_name.Contains(name) && e.department_id.Equals(dept)
                    select e;
            return q.ToList();

        }

        public static List<getrepdetails_Result> getpreviousrepdetails(string dept)
        {
            List<getrepdetails_Result> list = new List<getrepdetails_Result>();
            list = context.getrepdetails(dept).ToList();
            return list;

        }

        public static void saverepdetails(string dept, int id)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                var q = from department_rep in context.department_rep
                        where department_rep.department_id.Equals(dept) &&
                        department_rep.representative_status.Equals("Active")
                        select department_rep;
                department_rep d = q.FirstOrDefault();
                d.representative_status = "InActive";
                context.SaveChanges();
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                department_rep dr = new department_rep
                {
                    department_id = dept,
                    representative_id = id,
                    appointed_date = DateTime.ParseExact(today, "yyyy-MM-dd", null),
                    representative_status = "Active"
                };
                context.department_rep.Add(dr);
                context.SaveChanges();
                ts.Complete();
            }


        }
        public static void updatepassword(string dept, int password)
        {
            var q = from department in context.departments
                    where
department.department_id.Equals(dept)
                    select department;
            department d = q.FirstOrDefault();
            d.department_pin = password;
            context.SaveChanges();
        }

        public static List<collection> GetCollection()
        {
            var q = from collection c in context.collections select c;
            return q.ToList();

        }

        public static void updatelocation(string dept, int id)
        {
            //var q=from collection_de

        }

        public static List<spGetCollectionList_Result> GetCollectionList()
        {
            List<spGetCollectionList_Result> list = new List<spGetCollectionList_Result>();
            return list = context.spGetCollectionList().ToList();
        }

        public static void DeductFromInventory(List<CollectionListItem> list)
        {
            foreach (var item in list)
            {
                inventory i = context.inventories.Where(x => x.item_number == item.itemNum).First();
                i.current_quantity -= item.qtyPrepared;
                context.SaveChanges();
            }
        }

        public static List<spGetUndisbursedROList_Result> GetUndisbursedROList()
        {
            List<spGetUndisbursedROList_Result> list = new List<spGetUndisbursedROList_Result>();
            return list = context.spGetUndisbursedROList().ToList();
        }

        public static requisition_order_detail GetRODetailByROIdAndItemNum(string roId, string itemNum)
        {
            requisition_order_detail rod = new requisition_order_detail();
            return rod = context.requisition_order_detail.Where(x => (x.requisition_id == roId) && (x.item_number == itemNum)).FirstOrDefault();
        }


        public static void UpdateRODetails(requisition_order_detail rod)
        {
            requisition_order_detail rodUpdate = context.requisition_order_detail.Where(x => (x.requisition_id == rod.requisition_id) && (x.item_number == rod.item_number)).First();
            rodUpdate.item_distributed_quantity = rod.item_distributed_quantity;
            rodUpdate.item_pending_quantity = rod.item_pending_quantity;
            context.SaveChanges();
        }

        public static List<spGetDepartmentList_Result> GetDepartmentList()
        {
            List<spGetDepartmentList_Result> list = new List<spGetDepartmentList_Result>();
            return list = context.spGetDepartmentList().ToList();
        }

        public static List<spGetRODetailsByROId_Result> GetRODetailsByROId(string roId)
        {
            List<spGetRODetailsByROId_Result> list = new List<spGetRODetailsByROId_Result>();
            return list = context.spGetRODetailsByROId(roId).ToList();
        }

        public static List<spViewCollectionList_Result> ViewCollectionList()
        {
            List<spViewCollectionList_Result> list = new List<spViewCollectionList_Result>();
            return list = context.spViewCollectionList().ToList();
        }

        public static int GetDepartmentPin(string departmentname)
        {
            return (int)context.spGetDepartmentPin(departmentname).ToList().Single();
        }

        
        
        public static inventory GetInventory(string id)
        {
            return context.inventories.Where(i => i.item_number == id).ToList<inventory>()[0];
        }

        public static System.Collections.IEnumerable GetSupplier(string id)
        //     public static List<(string supplier_name, double unit_price)> GetSupplier(string id)
        {
            var nestedQuery = from s in context.suppliers
                              from sid in s.supplier_itemdetail
                              from i in context.inventories
                              where (sid.item_number == id && i.item_number == id)
                              orderby (sid.priority)
                              select new { s.supplier_name, sid.unit_price, i.description };
            return nestedQuery.ToList();
            //return context.supplier_itemdetail.Where(i => i.item_number == id).OrderBy(i => i.priority).ToList<supplier_itemdetail>();
        }
        public static List<inventory> GetActiveInventories()
        {
            return context.inventories.Where(x => x.item_status.ToLower() == "active").ToList();
        }
        public static List<inventory> GetAllInventories()
        {
            return context.inventories.ToList();
        }
        public static List<supplier> GetActiveSuppliers()
        {
            return context.suppliers.Distinct().Where(s => s.supplier_status.ToLower() == "active").ToList();
        }
        public static List<string> GetCategories()
        {
            return context.inventories.OrderBy(x => x.category).Select(x => x.category).Distinct().ToList();
        }
        public static int ReturnPendingPOqtyByStatus(inventory item, string status)
        {
            var q = context.purchase_order_detail.Where(x => x.item_purchase_order_status.ToLower().Trim() == "pending"
            && x.purchase_order.purchase_order_status.ToLower().Trim() == status);
            int qty = 0;
            foreach (var a in q)
            {
                if (a.item_number.ToLower().Trim().Equals(item.item_number.ToLower().Trim()))
                {
                    qty += a.item_purchase_order_quantity;
                }
            }
            return qty;
        }
        public static int ReturnPendingAdjustmentQty(inventory item)
        {
            var q = context.adjustments.Where(x => x.adjustment_status.ToLower().Trim() == "pending");
            int qty = 0;
            foreach (var a in q)
            {
                if (a.item_number.ToLower().Trim().Equals(item.item_number.ToLower().Trim()))
                {
                    qty += a.adjustment_quantity;
                }
            }
            return qty;
        }
        public static List<inventory> GetInventoriesByCategory(string category)
        {
            return context.inventories.Where(x => x.category.Trim().ToLower() == category.Trim().ToLower()).ToList();
        }

        // Returns a suggested reorder quantity when give an item code
        // Returns zero if there are no purchase order in the past.
        public static int GetSuggestedReorderQuantity(string itemCode)
        {

            var context = new LogicUniversityEntities();
            var result = context.getRequestedItemQuantityLastYear(itemCode).ToList();
            if (result.Count == 1)
            {
                // Formula: Quantity requested every month
                int quantity = (int)result.First().quantity_requested;
                quantity = quantity / 12;
                return quantity;
            }

            return 0;
        }

        //ViewRO
        public static int GetPlaceIdFromDptId(string dptId)
        {
            spGetPlaceIdFromDptId_Result result = context.spGetPlaceIdFromDptId(dptId).FirstOrDefault();
            return (int)result.place_id;
        }

        //ViewRO
        public static void SpecialRequestReadyUpdates(int placeId, DateTime collectionDate, string collectionStatus, string ro_id)
        {
            context.spSpecialRequestReady(placeId, collectionDate, collectionStatus, ro_id);
        }

        //Alan-start

        //List all adjustment form
        public static List<adjustment> StoreSupGetAdj()
        {



            return context.adjustments.Where(x => x.adjustment_status == "pending" && x.adjustment_price <= 250).ToList();


        }
        public static List<adjustment> StoreManagerGetAdj()
        {


            return context.adjustments.Where(x => x.adjustment_status == "pending" && x.adjustment_price >= 250).ToList();

        }


        //update upon approval adjustment form
        public static void Updateadj(int id, string comment)
        {
            adjustment adj = context.adjustments.Where(x => x.adjustment_id == id).FirstOrDefault<adjustment>();
            adj.adjustment_status = "Approved";
            adj.manager_remark = comment;
            context.SaveChanges();

        }

        //update upon reject adjustment form
        public static void RejectAdj(int id, string comment)
        {
            adjustment adj = context.adjustments.Where(x => x.adjustment_id == id).FirstOrDefault<adjustment>();
            adj.adjustment_status = "Rejected";
            adj.manager_remark = comment;
            context.SaveChanges();

        }


        //To search adjustment form base on date search
        public static List<adjustment> SearchAdj(DateTime date)
        {
            return context.adjustments.Where(x => x.adjustment_date == date).ToList<adjustment>();
        }


        //To list pending purchase orders 
        public static List<sp_getPendingPOList_Result> GetPurchaseOrders()
        {
            return context.sp_getPendingPOList().ToList();
        }

        //To pending PO purchase order details
        public static List<sp_getPendingPODetails_Result> GetPODetails(int id)
        {
            return context.sp_getPendingPODetails(id).ToList();
        }

        //extra details for individual PO
        public static List<sp_geteachPendingPOList_Result> GetEachPODetail(int id)
        {
            return context.sp_geteachPendingPOList(id).ToList();
        }

        //approval of PO
        public static void UpdateUponPOApproval(int id, string mremark, string email)
        {
            purchase_order po = context.purchase_order.Where(x => x.purchase_order_number == id).FirstOrDefault<purchase_order>();
            po.purchase_order_status = "Pending";
            po.manager_remark = mremark;
            //needs to be modfied
            sendMail(email, "test", "testing");

            context.SaveChanges();
        }

        //reject of PO
        public static void UpdateUponPOReject(int id, string mremark)
        {
            purchase_order po = context.purchase_order.Where(x => x.purchase_order_number == id).FirstOrDefault<purchase_order>();
            po.purchase_order_status = "Rejected";
            po.manager_remark = mremark;
            context.SaveChanges();
        }

        //E-mail util class
        public static void sendMail(string to, string sub, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("adteam3@gmail.com");
            mail.To.Add(to);
            mail.Subject = sub;
            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("adteam3@gmail.com", "testing!23");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }


        //alan-end

        // Tharrani start
        //List All Active Inventory
        public static List<inventory> GetActiveInventory()
        {
            return context.inventories.Where(p => p.item_status == "Active").ToList<inventory>();
        }

        //Return inventory which matches description
        public static List<inventory> SearchActiveInventory(string search)
        {
            return context.inventories.Where(P => P.item_status == "Active" && P.description.Contains(search)).ToList<inventory>();
        }

        //Return selected inventory item
        public static inventory GetInventoryById(string Id)
        {
            return context.inventories.Where(P => P.item_number == Id).FirstOrDefault();
        }

        //Return selected inventory unit price
        public static double GetItemUnitPrice(string Id)
        {
            List<supplier_itemdetail> l = new List<supplier_itemdetail>();
            l = context.supplier_itemdetail.Where(P => P.item_number == Id).ToList<supplier_itemdetail>();
            double s = 0;
            s = l[0].unit_price;
            return s;
        }

        //Add new entry to Requisition order table
        public static void AddNewRequisitionOrder(string id, int emp, DateTime d)
        {

            requisition_order r = new requisition_order();
            r.requisition_id = id;
            r.employee_id = emp;
            r.requisition_date = d;
            r.requisition_status = "Pending";
            context.requisition_order.Add(r);
            context.SaveChanges();
        }

        //Add new entry to Requisition order detail table
        public static void AddRequisitionOrderDetail(cart c, string id)
        {
            requisition_order_detail rod = new requisition_order_detail();
            rod.requisition_id = id;
            rod.item_number = c.Inventory.item_number;
            rod.item_requisition_quantity = c.Quantity;
            rod.item_distributed_quantity = 0;
            rod.item_pending_quantity = c.Quantity;
            rod.item_requisition_price = c.Itemprice;
            context.requisition_order_detail.Add(rod);
            context.SaveChanges();
        }

        //return detail of requisition order by Id (For Request Confirmation)
        public static requisition_order GetRequisitionOrderById(string Id)
        {
            return context.requisition_order.Where(x => x.requisition_id == Id).First();
        }

        //Return status of requisition orders for dropdownlist
        public static List<GetRequisitionStatus_Result> GetRequisitionStatus()
        {
            return context.GetRequisitionStatus().ToList();
        }

        //Return Pending requisition order by employee
        public static List<requisition_order> GetPendingRequisitionByEmployee(int id)
        {
            return context.requisition_order.Where(x => x.employee_id == id && x.requisition_status == "Pending").ToList<requisition_order>();
        }

        //Return all requisition by employee
        public static List<requisition_order> GetAllRequisitionByEmployee(int id)
        {
            return context.requisition_order.Where(x => x.employee_id == id).ToList<requisition_order>();
        }

        //Return requisition by employee with date for all status
        public static List<requisition_order> GetRequisitionByEmployeeSearchDateAllStatus(int i, DateTime d)
        {
            return context.requisition_order.Where(x => x.employee_id == i && x.requisition_date == d).ToList();
        }

        //return requisition by employee with date for status
        public static List<requisition_order> GetRequisitionByEmployeeSearchDate(int id, DateTime d, string status)
        {
            return context.requisition_order.Where(x => x.employee_id == id && x.requisition_status == status && x.requisition_date == d).ToList();
        }

        //return requisition by employee with status
        public static List<requisition_order> GetRequisitionByEmployeeSearchStatus(int id, string status)
        {
            return context.requisition_order.Where(x => x.employee_id == id && x.requisition_status == status).ToList();
        }

        //Return requisition order detail by id
        public static List<getRequisitionOrderDetails_Result> GetRequisitionorderDetail(string id)
        {
            List<getRequisitionOrderDetails_Result> l = new List<getRequisitionOrderDetails_Result>();
            return l = context.getRequisitionOrderDetails(id).ToList();
        }

        //Cancel requisition order status to cancel
        public static void Cancelrequisition(string id)
        {
            requisition_order r = context.requisition_order.Where(x => x.requisition_id == id).First();
            r.requisition_status = "Cancel";
            context.SaveChanges();
        }

        //Save changes in edit request to requisition order
        public static void UpdateRequisitionOrderDetail(string id, List<getRequisitionOrderDetails_Result> order)
        {
            if (order.Count > 0)
            {
                List<requisition_order_detail> rod = new List<requisition_order_detail>();
                rod = context.requisition_order_detail.Where(x => x.requisition_id == id).ToList();
                for (int i = 0; i < rod.Count; i++)
                {
                    bool found = false;
                    for (int j = 0; j < order.Count; j++)
                    {
                        if (rod[i].item_number == order[j].item_number)
                        {
                            found = true;
                            string item = order[j].item_number;
                            requisition_order_detail r = context.requisition_order_detail.Where(x => x.requisition_id == id && x.item_number == item).First<requisition_order_detail>();
                            r.item_requisition_quantity = order[j].item_requisition_quantity;
                            r.item_distributed_quantity = 0;
                            r.item_pending_quantity = order[j].item_requisition_quantity;
                            r.item_requisition_price = order[j].item_requisition_price;
                            context.SaveChanges();
                        }
                    }
                    if (!found)
                    {
                        string it = rod[i].item_number;
                        requisition_order_detail re = context.requisition_order_detail.Where(x => x.requisition_id == id && x.item_number == it).First<requisition_order_detail>();
                        context.requisition_order_detail.Remove(re);
                    }
                }
            }
            else
            {
                Cancelrequisition(id);
            }
        }

        //return all PO - with total PO item count
        public static List<getAllViewPOHistorytotalcount_Result> viewpohistorytotal()
        {
            return context.getAllViewPOHistorytotalcount().ToList();
        }

        //return list of supplier for dropdown in ViewPoHistory
        public static List<supplier> getSupplierNames()
        {
            return context.suppliers.ToList();
        }

        //return supplier code based on supplier name
        public static supplier getSupplierCode(string text)
        {
            return context.suppliers.Where(x => x.supplier_name == text).FirstOrDefault();
        }

        //Return list of PO status for dropdown in ViewPoHistory
        public static List<purchase_order> getPOStatus()
        {
            return context.purchase_order.ToList();
        }

        //return PO by status- with total PO item count
        public static List<getViewPOHistorytotalcountByStatus_Result> ViewPOHistorytotalcountByStatus(string s)
        {
            return context.getViewPOHistorytotalcountByStatus(s).ToList();
        }

        //return  PO by Po number - with total PO item count
        public static List<getViewPOHistorytotalcountbyPO_Result> viewPOHistorytotalcountbyPO(int po)
        {
            return context.getViewPOHistorytotalcountbyPO(po).ToList();
        }

        //return  PO by Po number and status - with total PO item count
        public static List<getViewPOHistorytotalcountbyPOandstatus_Result> ViewPOHistorytotalcountbyPOandstatus(int po, string status)
        {
            return context.getViewPOHistorytotalcountbyPOandstatus(po, status).ToList();
        }

        //return  PO by supplier - with total PO item count
        public static List<getViewPOHistorytotalcountbySupplier_Result> viewPOHistorytotalcountbySupplier(string supplier)
        {
            return context.getViewPOHistorytotalcountbySupplier(supplier).ToList();
        }

        //return  PO by supplier and PO number - with total PO item count
        public static List<getViewPOHistorytotalcountbyPOandSupplier_Result> viewPOHistorytotalcountbyPOandSupplier(int po, string supplier)
        {
            return context.getViewPOHistorytotalcountbyPOandSupplier(po, supplier).ToList();
        }

        //return  PO by supplier and status - with total PO item count
        public static List<getViewPOHistorytotalcountbysupandstatus_Result> viewPOHistorytotalcountbysupandstatus(string supplier, string status)
        {
            return context.getViewPOHistorytotalcountbysupandstatus(supplier, status).ToList();
        }

        //return  PO by supplier and status and Po number - with total PO item count
        public static List<getViewPOHistorytotalcountbyPOandstatusandSupplier_Result> viewPOHistorytotalcountbyPOandstatusandSupplier(string supplier, int po, string status)
        {
            return context.getViewPOHistorytotalcountbyPOandstatusandSupplier(supplier, po, status).ToList();
        }

        //return all PO - with total PO item count
        public static List<getAllViewPOHistorypendingcount_Result> viewPOHistorypendingcount()
        {
            return context.getAllViewPOHistorypendingcount().ToList();
        }

        //return PO detail based on PO ID
        public static List<purchase_order_detail> getpurchaseorderdetail(int id)
        {
            return context.purchase_order_detail.Where(x => x.purchase_order_number == id).ToList();
        }

        //return supplier for PO by PO id
        public static supplier getSupplierNameforPurchaseorder(int id)
        {
            purchase_order p = getpurchaseorder(id);
            string sid = p.suppler_id;
            return context.suppliers.Where(x => x.supplier_id == sid).FirstOrDefault();
        }

        //return po for id
        public static purchase_order getpurchaseorder(int id)
        {
            return context.purchase_order.Where(x => x.purchase_order_number == id).FirstOrDefault();
        }

        //return employee name
        public static employee GetEmployee(int id)
        {
            return context.employees.Where(x => x.employee_id == id).FirstOrDefault();
        }

        //accept item from supplier
        public static void acceptitemfromsupplier(int po, string item, int quantity, string remark)
        {
            purchase_order_detail pod = context.purchase_order_detail.Where(x => x.purchase_order_number == po && x.item_number == item).FirstOrDefault();
            pod.item_accept_quantity = quantity;
            pod.purchase_order_item_remark = remark;
            pod.item_purchase_order_status = "Accepted";
            pod.item_accept_date = DateTime.Now.Date;
            context.SaveChanges();
        }
        // Tharrani end



    }
}