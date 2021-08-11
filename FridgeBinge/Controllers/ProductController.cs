using FridgeBinge.Models;
using FridgeBinge.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeBinge.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            ProductsDAO products = new();
            return View("Index", products.GetAllProducts());
        }

        public IActionResult SearchResults(string searchTerm)
        {
            ProductsDAO products = new();
            return View("Index", products.SearchProducts(searchTerm)); //TODO display message null list?
        }

        public IActionResult SearchForm()
        {
            return View();
        }

        // GET: HomeController1/Create
        public IActionResult Create()
        {
            return View("Create");
        }

        public IActionResult ProcessCreate(ProductModel model)
        {
            ProductsDAO products = new();
            int newId = products.Insert(model);
            if (newId == -1)
            {
                ViewBag.Message = "Failed to create new product.";
                return View("Index", products.GetAllProducts());
            }
            model.Id = newId;
            return View("Details", model);
        }

        public IActionResult Details(int id)
        {
            ProductsDAO products = new();
            ProductModel foundProduct = products.GetProductById(id);
            if (foundProduct == null)
            {
                ViewBag.Message = "Product with id " + id + " was not found.";
                return View("Index", products.GetAllProducts());
            }
            return View("Details", foundProduct);
        }

        // GET: HomeController1/Edit/5
        public IActionResult Edit(int id)
        {
            ProductsDAO products = new();
            ProductModel editProduct = products.GetProductById(id);
            if (editProduct == null)
            {
                ViewBag.Message = "Product with id " + id + " was not found.";
                return View("Index", products.GetAllProducts());
            }
            ViewBag.Id = id;
            return View("Edit", editProduct);
        }

        public IActionResult ProcessEdit(ProductModel model)
        {
            ProductsDAO products = new();
            if (products.Update(model) == -1)
            {
                ViewBag.Message = "Failed to update product with id " + model.Id + ".";
            }
            ViewBag.Message = "Product update ";
            return View("Details", model);
        }

        // GET: HomeController1/Delete/5
        public IActionResult Delete(int id)
        {
            ProductsDAO products = new();
            ProductModel deleteMe = products.GetProductById(id);
            ViewBag.Message = "Product with id " + id + " was not found.";
            if (deleteMe != null)
            {
                ViewBag.Message = "Product with id " + id + " was " + ((products.Delete(deleteMe) != 1) ? "not " : "") + "deleted.";
            }
            return View("Index", products.GetAllProducts()); //TODO change to Redirect?
        }
    }
}
