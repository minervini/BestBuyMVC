﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BestBuyMVC.Models;

namespace BestBuyMVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            ProductRepository repo = new ProductRepository();
            List<Product> product = repo.GetAllProducts();

            return View(product);
        }

        public IActionResult ViewProduct(int id)
        {
            ProductRepository repo = new ProductRepository();
            Product product = repo.GetProduct(id);

            return View(product);
        }

        public IActionResult UpdateProduct(int id)
        {
            ProductRepository repo = new ProductRepository();
            Product prod = repo.GetProduct(id);

            repo.UpdateProduct(prod);

            if (prod == null)
            {
                return View("Product not found");
            }
            return View(prod);
        }

        public IActionResult UpdateProductToDatabase(Product product)
        {
            ProductRepository repo = new ProductRepository();
            repo.UpdateProduct(product);

            return RedirectToAction("ViewProduct", new { id = product.ProductID });
        }

        public IActionResult InsertProduct()
        {
            CategoryRepository repo = new CategoryRepository();
            repo.InsertProduct(product);

            return RedirectToAction("ViewProduct", new { id = product.ProductID });
        }
    }
}
