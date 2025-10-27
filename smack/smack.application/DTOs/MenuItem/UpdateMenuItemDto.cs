using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smack.application.DTOs.MenuItem
{
    public class UpdateMenuItemDto
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool? IsAvailable { get; set; }
        public int? CategoryId { get; set; }
       
    }
}
