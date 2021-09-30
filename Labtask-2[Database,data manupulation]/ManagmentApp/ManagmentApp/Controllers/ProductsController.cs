using ManagmentApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using ManagmentApp.Models;
using System.Web.Script.Serialization;

namespace ManagmentApp.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        
        public ActionResult Index()
        {
            
            Database db = new Database();
            var Products = db.Products.GetAll();
            return View(Products);
           
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Add(Product p)
        {
            if (ModelState.IsValid)
            {
                Database db = new Database();
                db.Products.Add(p);
                return RedirectToAction("Index");
            }
            return View();

        }
        [HttpPost]
        public ActionResult Update(Product p)
        {

            if (ModelState.IsValid)
            {
                Database db = new Database();
                db.Products.Update(p);
                return RedirectToAction("Index");
            }
            return View(p);
        }
        [HttpGet]
        public ActionResult GetUpdateProduct(int id)
        {
            
            
                Database db = new Database();
                
                var p = db.Products.GetOneProduct(id);

                return View(p);

            

        }
        public ActionResult Delete(int id)
        {


            Database db = new Database();

            var p = db.Products.DeleteProduct(id);

            return RedirectToAction("Index");



        }
        public ActionResult AddToCart(int id)
        {
            List<Product> products = null;
            Database db = new Database();
            var product = db.Products.GetOneProduct(id);
            if(Session["Card"]==null)
            {
                products = new List<Product>();
                products.Add(product);
                string json = new JavaScriptSerializer().Serialize(products);
                Session["Card"] = json;
                return View(products);
            }
            else
            {
                var item = Session["Card"].ToString();
                products = new JavaScriptSerializer().Deserialize<List<Product>>(item);
                string json = new JavaScriptSerializer().Serialize(products);
                Session["Card"] = json;
                return View(products);
            }
            
        }
    }
}