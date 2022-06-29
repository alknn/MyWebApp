using MyWebApp.Insfrastructure.Context;
using MyWebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        MyDbContext db = new MyDbContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddCart(int id)
        {
            //id ile olan ürün nesnesini çekiyoruz.
            var product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return RedirectToAction("Index", "Product");
            }
            //Session cart nesnesi oluşup oluşmadığına bakıyoruz. Daha önceden var olup olmadığına bakıyoruz.
            //string sessionName = User.Identity.Name + "_cart";
            if (Session["cart"] == null)
            {
                //Session nesnesi daha önceden oluşmadıysa
                //liste oluşturuyorum.
                List<CartViewModel> list = new List<CartViewModel>();
                //modelimi oluşturuyorum.
                CartViewModel model = new CartViewModel
                {
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Photo = product.Photo,
                    Quantity = 1
                };
                //oluşturduğum listeye ekleme yapıyorum.
                list.Add(model);
                //ve bu listeyi de Session cart nesnesine eşitleyip session cart nesnesini oluşturuyorum.
                Session["cart"] = list;
            }
            else
            {
                //session cart nesnesi varsa
                List<CartViewModel> cart = (List<CartViewModel>)Session["cart"];
                //eklemek istediğim ürün daha önceden eklenmiş mi diye kontrol ediyorum.
                //ve burada eklemek istediğim ürün daha önce varsa bunu index ini alıyorum.
                int index = -1;
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].ProductName == product.ProductName)
                    {
                        index = i;
                    }
                }
                if (index != -1)
                {
                    //Bu ürünün adedini arttıralım.
                    cart[index].Quantity++;
                }
                else
                {
                    //eklemek istediğimiz yoksa modelimi oluşturup listeye
                    CartViewModel model = new CartViewModel
                    {
                        ProductName = product.ProductName,
                        Price = product.Price,
                        Photo = product.Photo,
                        Quantity = 1
                    };
                    cart.Add(model);
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }
    }
}