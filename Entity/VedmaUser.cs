using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma_backend.Entity
{
    public class VedmaUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public IList<CharSheet> CharSheets { get; set;
        }
    }
}
