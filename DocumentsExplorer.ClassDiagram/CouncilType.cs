using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentsExplorer.ClassDiagram
{
    public class CouncilType
    {
        public List<CouncilMember> CouncilMembers
        {
            get; set;
        }

        public string Description { get; set; }


        public List<DocumentsExplorer.ClassDiagram.Round> Rounds
        {
            get;
            set;
        }
    }
}