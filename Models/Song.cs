using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TestAPI.Models
{
    public class Song
    {
        // primary key 
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public int Length { get; set; }

        // foreign keys
        public int ArtistID { get; set; }
        public int CategoryID { get; set; }

        public Artist? Artist { get; set; }
        public Category? Category { get; set; }

    }
}

