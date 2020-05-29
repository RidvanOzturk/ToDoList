using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27.DBEntities
{
    public class Note
    {
        [Key]
        public int notlarId { get; set; }
        public string konu { get; set; }
        public string baslik { get; set; }
    }
}
