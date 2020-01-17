using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class DecisionTypeDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string LastUpdateBy { get; set; }
    }
}
