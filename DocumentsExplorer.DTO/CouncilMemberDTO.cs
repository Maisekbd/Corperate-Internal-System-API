using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class CouncilMemberDTO
    {
        public CouncilMemberDTO()
        {
            //CouncilTypes = new List<CouncilTypeDTO>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int CouncilTypeId { get; set; }

        public virtual CouncilTypeDTO CouncilType { get; set; }

        //public virtual IList<CouncilTypeDTO> CouncilTypes { get; set; }

        public int CountryId { get; set; }

        public virtual CountryDTO Country { get; set; }

        public string Position { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public int memberRole { get; set; }

        public bool IsActive { get; set; }

        public string Email { get; set; }

        public string PhotoPath { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string LastUpdateBy { get; set; }

        public List<int> SelectedCouncilTypes { get; set; }

    }
}
