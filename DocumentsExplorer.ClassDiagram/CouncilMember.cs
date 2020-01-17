using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentsExplorer.ClassDiagram
{
    public class CouncilMember
    {
        public string Name { get; set; }

        public virtual Country Country { get; set; }

        public string Position { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public int memberRole { get; set; }

        public string PhotoPath { get; set; }

        public bool IsActive { get; set; }
    }
}