using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentsExplorer.ClassDiagram
{
    public class Decision
    {
        public string Subject { get; set; }

        public string DecisionPath { get; set; }

        public string DecisionReference { get; set; }

        public string DecisionNumber { get; set; }

        public string DecisionText { get; set; }

        public DateTime? SuggestedExecutionDate { get; set; }

        public string DecisionAnnexPath { get; set; }

        public string keyWords { get; set; }

        public DecisionType DecisionType
        {
            get => default(DecisionType);
            set
            {
            }
        }

        public Action Action
        {
            get => default(Action);
            set
            {
            }
        }

        public AgendaItem AgendaItem
        {
            get => default(AgendaItem);
            set
            {
            }
        }

        public Decision ReferenceDecision
        {
            get => default(Decision);
            set
            {
            }
        }

        public Country Country
        {
            get => default(Country);
            set
            {
            }
        }

        public SubCategory SubCategory
        {
            get => default(SubCategory);
            set
            {
            }
        }

        public List<DocumentsExplorer.ClassDiagram.ActivitySector> ActivitySectors
        {
            get;
            set;
        }

        public List<DocumentsExplorer.ClassDiagram.DecisionAction> DecisionActions
        {
            get;
            set;
        }

        public List<DocumentsExplorer.ClassDiagram.Company> Companys
        {
            get;
            set;
        }

        public List<DocumentsExplorer.ClassDiagram.ReferenceItem> ReferenceItems
        {
            get;
            set;
        }
    }
}