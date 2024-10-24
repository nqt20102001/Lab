using System;
using System.Collections.Generic;

namespace Lab.Application.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string GenreName { get; set; }
        public string Author { get; set; }
        public bool Available { get; set; }
        public string Publisher { get; set; }
        public DateTime CreatedDate{ get; set;}
        public List<string> CatalogTitles { get; set; }
    }
}
