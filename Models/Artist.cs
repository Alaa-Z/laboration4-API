using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestAPI.Models
{
	public class Artist
	{
        // Primary key
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<Song>? Songs { get; set; }      
    }
}

