using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class RoundDTO
    {


        public int Id { get; set; }

        public string RoundNumber { get; set; }


        public DateTime FromDate
        {
            get
            {
                return this.fromDate.HasValue
                   ? this.fromDate.Value
                   : new DateTime(DateTime.Now.Year, 01, 01);
            }

            set { this.fromDate = value; }
        }

        public DateTime ToDate
        {
            get
            {
                return this.toDate.HasValue
                   ? this.toDate.Value
                   : new DateTime(DateTime.Now.Year, 12, 31);
            }

            set { this.toDate = value; }
        }


        public DateTime? CreateDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string LastUpdateBy { get; set; }


        private DateTime? fromDate = null;
        private DateTime? toDate = null;
    }
}
