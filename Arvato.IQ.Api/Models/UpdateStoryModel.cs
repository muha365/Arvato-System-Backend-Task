using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Arvato.IQ.Api.Models
{
    public class StoryModel
    {
        [Required]
        public long StoryId { get; set; }
        [Required]
        [MaxLength(500)]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PublishedAt { get; set; }
    }
}