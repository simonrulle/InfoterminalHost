using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }
        public string Role { get; set; }
        public string Faculty { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public string Building { get; set; }
        public string Room { get; set; }
        public List<string> Emails { get; set; }
    }
}
