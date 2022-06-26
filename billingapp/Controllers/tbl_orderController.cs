using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using billingapp;
using Microsoft.Ajax.Utilities;

namespace billingapp.Controllers
{
    public class tbl_orderController : Controller
    {
        private billingappdbEntities db = new billingappdbEntities();

        // GET: tbl_order
        public ActionResult Index()
        {
            var tbl_order = db.tbl_order.Include(t => t.tbl_customer).Include(t => t.tbl_product);
            return View(tbl_order.ToList());
        }

        public ActionResult order_by_cust()
        {
            //var order_cust = db.tbl_order.Distinct().ToList();
            //List<tbl_order> distinctcust = db.tbl_order.GroupBy(c => c.order_id).Select(g => g.FirstOrDefault()).ToList();
            //var tbl_order = (from p in db.tbl_order
            //              orderby p.cust_id
            //              select p).Distinct();

            var tbl_order = db.tbl_order.DistinctBy(x => x.cust_id).ToList();
            //return View(orders.AsEnumerable());
            return View(tbl_order);
        }


        public ActionResult listorderbycust(int? id)
        {
            var tbl_order = db.tbl_order.Include(t => t.tbl_customer).Include(t => t.tbl_product);
            return View(tbl_order.Where(p => p.cust_id == id).ToList());
        }


        public ActionResult orderbyproduct()
        {
            var tbl_order = db.tbl_order.DistinctBy(x => x.product_id).ToList();
            return View(tbl_order);
        }

        public ActionResult listorderbyproduct(int? id)
        {
            var tbl_order = db.tbl_order.Include(t => t.tbl_customer).Include(t => t.tbl_product);
            return View(tbl_order.Where(p => p.product_id == id).ToList());
        }


        public ActionResult viewbill_cust(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_order tbl_order = db.tbl_order.Find(id);
            if (tbl_order == null)
            {
                return HttpNotFound();
            }
            return View(tbl_order);
        }



        // GET: tbl_order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_order tbl_order = db.tbl_order.Find(id);
            if (tbl_order == null)
            {
                return HttpNotFound();
            }
            return View(tbl_order);
        }

        // GET: tbl_order/Create
        public ActionResult Create()
        {
            ViewBag.cust_id = new SelectList(db.tbl_customer, "cust_id", "cust_name");
            ViewBag.product_id = new SelectList(db.tbl_product, "product_id", "product_name");
            return View();
        }

        // POST: tbl_order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_id,order_date,prod_qty,product_id,cust_id")] tbl_order tbl_order)
        {
            if (ModelState.IsValid)
            {
                db.tbl_order.Add(tbl_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cust_id = new SelectList(db.tbl_customer, "cust_id", "cust_name", tbl_order.cust_id);
            ViewBag.product_id = new SelectList(db.tbl_product, "product_id", "product_name", tbl_order.product_id);
            return View(tbl_order);
        }

        // GET: tbl_order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_order tbl_order = db.tbl_order.Find(id);
            if (tbl_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.cust_id = new SelectList(db.tbl_customer, "cust_id", "cust_name", tbl_order.cust_id);
            ViewBag.product_id = new SelectList(db.tbl_product, "product_id", "product_name", tbl_order.product_id);
            return View(tbl_order);
        }

        // POST: tbl_order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_id,order_date,prod_qty,product_id,cust_id")] tbl_order tbl_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cust_id = new SelectList(db.tbl_customer, "cust_id", "cust_name", tbl_order.cust_id);
            ViewBag.product_id = new SelectList(db.tbl_product, "product_id", "product_name", tbl_order.product_id);
            return View(tbl_order);
        }

        // GET: tbl_order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_order tbl_order = db.tbl_order.Find(id);
            if (tbl_order == null)
            {
                return HttpNotFound();
            }
            return View(tbl_order);
        }

        // POST: tbl_order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_order tbl_order = db.tbl_order.Find(id);
            db.tbl_order.Remove(tbl_order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
