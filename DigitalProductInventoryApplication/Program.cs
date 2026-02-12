// ================================================================
// DIGITAL PRODUCT INVENTORY SYSTEM
// Demonstrates:
// 1. Generics with Factory Pattern
// 2. Abstract classes & interfaces
// 3. LINQ Join
// 4. Reusable object creation using generics
// ================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalProductInventoryApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lists to store all products and categories
            List<ProductBase> products = new List<ProductBase>();
            List<CategoryBase> categories = new List<CategoryBase>();

            // =========================================================
            // CREATE PRODUCTS USING GENERIC FACTORY PATTERN
            // =========================================================

            // Create Digital Book product object
            ProductBase digitalBook = FactoryPattern<DigitalBook, ProductBase>.GetInstance();
            AddPropertiesToProduct(digitalBook, 1, "Atomic Habits", 1);
            products.Add(digitalBook);

            // Create Movie product object
            ProductBase movie = FactoryPattern<Movie, ProductBase>.GetInstance();
            AddPropertiesToProduct(movie, 2, "Avengers Endgame", 2);
            products.Add(movie);

            // Create another movie
            movie = FactoryPattern<Movie, ProductBase>.GetInstance();
            AddPropertiesToProduct(movie, 3, "Interstellar", 2);
            products.Add(movie);

            // Create music album
            ProductBase album = FactoryPattern<MusicRecording, ProductBase>.GetInstance();
            AddPropertiesToProduct(album, 4, "Arijit Singh Hits", 3);
            products.Add(album);


            // =========================================================
            // CREATE CATEGORY OBJECTS USING GENERIC FACTORY
            // =========================================================

            CategoryBase digitalBookCategory = FactoryPattern<DigitalBookCategory, CategoryBase>.GetInstance();
            AddPropertiesToCategory(digitalBookCategory, 1, "Books", "E-books available for download");
            categories.Add(digitalBookCategory);

            CategoryBase movieCategory = FactoryPattern<MovieCategory, CategoryBase>.GetInstance();
            AddPropertiesToCategory(movieCategory, 2, "Movies", "HD movies available online");
            categories.Add(movieCategory);

            CategoryBase musicCategory = FactoryPattern<MusicCategory, CategoryBase>.GetInstance();
            AddPropertiesToCategory(musicCategory, 3, "Music", "MP3 songs available for streaming");
            categories.Add(musicCategory);


            // =========================================================
            // LINQ JOIN → Combine product with category info
            // =========================================================
            var queryResults = GetProducts(products, categories);

            // Print results
            foreach (var result in queryResults)
            {
                Console.WriteLine($"Product Id: {result.ProductId}");
                Console.WriteLine($"Title: {result.Title}");
                Console.WriteLine($"Category: {result.Category}");
                Console.WriteLine($"Category Description: {result.CategoryDescription}");
                Console.WriteLine();
            }

            Console.ReadKey();
        }


        // =========================================================
        // LINQ JOIN METHOD
        // Joins products with categories using CategoryId
        // =========================================================
        private static IEnumerable<ProductViewModel> GetProducts(List<ProductBase> products, List<CategoryBase> categories)
        {
            return from p in products
                   join c in categories on p.CategoryId equals c.Id
                   select new ProductViewModel
                   {
                       ProductId = p.Id,
                       Title = p.Title,
                       Category = c.Title,
                       CategoryDescription = c.Description
                   };
        }


        // =========================================================
        // Helper method to assign category properties
        // =========================================================
        private static void AddPropertiesToCategory(CategoryBase category, int id, string title, string description)
        {
            category.Id = id;
            category.Title = title;
            category.Description = description;
        }

        // =========================================================
        // Helper method to assign product properties
        // =========================================================
        private static void AddPropertiesToProduct(ProductBase product, int id, string title, int categoryId)
        {
            product.Id = id;
            product.Title = title;
            product.CategoryId = categoryId;
        }
    }


    // =========================================================
    // VIEW MODEL → Used for displaying final joined result
    // =========================================================
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string CategoryDescription { get; set; }
    }


    // =========================================================
    // INTERFACE → Common properties for Product & Category
    // =========================================================
    public interface IPrimaryPropeties
    {
        int Id { get; set; }
        string Title { get; set; }
    }


    // =========================================================
    // ABSTRACT BASE CLASS FOR ALL PRODUCTS
    // =========================================================
    public abstract class ProductBase : IPrimaryPropeties
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
    }


    // =========================================================
    // DIFFERENT PRODUCT TYPES
    // =========================================================
    public class Movie : ProductBase
    {
        public string Director { get; set; }
        public string Producer { get; set; }
    }

    public class DigitalBook : ProductBase
    {
        public string Author { get; set; }
    }

    public class MusicRecording : ProductBase
    {
        public string RecordCompany { get; set; }
    }


    // =========================================================
    // ABSTRACT BASE CLASS FOR CATEGORY
    // =========================================================
    public abstract class CategoryBase : IPrimaryPropeties
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    // Category types
    public class MovieCategory : CategoryBase { }
    public class DigitalBookCategory : CategoryBase { }
    public class MusicCategory : CategoryBase { }


    // =========================================================
    // GENERIC FACTORY PATTERN
    // Creates object dynamically using generics
    // =========================================================
    public static class FactoryPattern<T, U>
        where T : class, U, new()   // T must inherit from U and have constructor
        where U : class, IPrimaryPropeties
    {
        public static U GetInstance()
        {
            // Create instance of T and return as U type
            U objT = new T();
            return objT;
        }
    }
}
