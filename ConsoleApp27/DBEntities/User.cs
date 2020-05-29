using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27.DBEntities
{
    public class User
    {
        [Key]
        public int usersId { get; set; }
        public string kullaniciAdi { get; set; }
        public int sifre { get; set; }
        public List<Note> notlar { get; set; }
    }
}
