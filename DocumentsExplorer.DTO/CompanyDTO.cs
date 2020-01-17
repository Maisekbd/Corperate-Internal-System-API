using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class CompanyDTO
    {
        public CompanyDTO()
        {
            ActivitySectors = new List<ActivitySectorDTO>();
            Contributors = new List<ContributorDTO>();
        }

        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateOfIncorporation { get; set; }

        public int CountryId { get; set; }

        public virtual CountryDTO Country { get; set; }

        public string CountryName { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string LastUpdateBy { get; set; }

        public List<int> SelectedSectors { get; set; }

        public virtual ICollection<ActivitySectorDTO> ActivitySectors { get; set; }

        public virtual ICollection<ContributorDTO> Contributors { get; set; }

        public string ActivitySectorsName
        {
            get
            {
                return String.Join(", ", ActivitySectors.Select(c => c.Name).ToArray());
            }
        }

        public string DateOfIncorporationString
        {
            get
            {
                return DateOfIncorporation.HasValue ? DateOfIncorporation.Value.ToShortDateString(): "";
            }
        }

        #endregion
    }
}
