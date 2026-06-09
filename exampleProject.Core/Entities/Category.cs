using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exampleProject.Core.Entities
{
    public class Category
    {
        public int Id { get; set; } // MSSQL bunu otomatik Primary Key ve Identity (1,1) yapar.
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}