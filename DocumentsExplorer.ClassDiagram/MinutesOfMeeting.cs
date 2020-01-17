using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentsExplorer.ClassDiagram
{
    public class AgendaItem
    {
        public int AgendaText
        {
            get => default(int);
            set
            {
            }
        }

        public int AgendaNumber
        {
            get => default(int);
            set
            {
            }
        }

        public int AgendaTitle
        {
            get => default(int);
            set
            {
            }
        }

        public int PresentedBy
        {
            get => default(int);
            set
            {
            }
        }

        public int Conclusion
        {
            get => default(int);
            set
            {
            }
        }

        public List<DocumentsExplorer.ClassDiagram.Action> Actions
        {
            get;
            set;
        }
    }
}