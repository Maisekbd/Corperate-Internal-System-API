using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class MainCategoryDTO
    {
        public MainCategoryDTO()
        {
        }
        #region Properties
        public int Id { get; set; }

        public string Description { get; set; }

        public int CouncilTypeId { get; set; }

        #endregion
    }
}
