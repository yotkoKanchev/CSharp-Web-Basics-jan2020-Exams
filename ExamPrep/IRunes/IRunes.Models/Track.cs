﻿namespace IRunes.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Track
    {
        public Track()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string Link { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string AlbumId { get; set; }

        public virtual Album Album { get; set; }
    }
}
