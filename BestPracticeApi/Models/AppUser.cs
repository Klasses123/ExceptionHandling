using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestPracticeApi.Models
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
    }
}
