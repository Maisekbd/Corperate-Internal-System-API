//using AAAID.Common.Model;
//using Repository.Pattern.Ef6;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DocumentsExplorer.Model.Models
//{
//    public partial class Contributor : BaseEntity
//    {
//        public Contributor()
//        {
//        }

//        #region Properties
//        //[Key]
//        //public int Id { get; set; }

//        public string Name { get; set; }

//        public DateTime? CreateDate { get; set; }

//        //public string CreatedBy { get; set; }

//        //public DateTime? LastUpdateDate { get; set; }

//        //public string LastUpdateBy { get; set; }

//        [ForeignKey("Company")]
//        public int CompanyId { get; set; }

//        public Company Company { get; set; }
//        #endregion
//    }
//}
