using AAAID.Common.Model;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model.Models
{
    public partial class Country : BaseEntity
    {
        public Country()
        {
        }

        #region Properties
        public string Name { get; set; }

        #endregion
    }
}
