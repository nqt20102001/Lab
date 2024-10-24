﻿using System.Collections.Generic;

namespace Lab.Application.DTOs
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<BookDTO> Books { get; set; }
    }
}
