using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public class CourseOfStudy
    {
        public string Name { get; set; }

        public List<Semester> Semester { get; set; }
    }
}
