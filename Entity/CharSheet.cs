using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma_backend.Entity
{
    public class CharSheet
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Property> Properties {get; set;}
        public Guid? UserId { get; set; }
        public VedmaUser  User { get; set; }
    }
}
