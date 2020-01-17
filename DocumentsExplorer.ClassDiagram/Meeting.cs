using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentsExplorer.ClassDiagram
{
    public class Meeting
    {
        public List<Attendence> Attendence
        {
            get;
            set;
        }

        public int MeetingNumber
        {
            get => default(int);
            set
            {
            }
        }

        public int DayText
        {
            get => default(int);
            set
            {
            }
        }

        public int Date
        {
            get => default(int);
            set
            {
            }
        }

        public int Time
        {
            get => default(int);
            set
            {
            }
        }

        public int Location
        {
            get => default(int);
            set
            {
            }
        }

        public List<DocumentsExplorer.ClassDiagram.Meeting> MinutesOfMeeting
        {
            get;
            set;
        }

        public int Objective
        {
            get => default(int);
            set
            {
            }
        }

        public int PreparedById
        {
            get => default(int);
            set
            {
            }
        }

        public int PreparedByName
        {
            get => default(int);
            set
            {
            }
        }

        public List<DocumentsExplorer.ClassDiagram.AgendaItem> AgendaItem
        {
            get;
            set;
        }
        public int MeetingIndexNumber
        {
            get => default(int);
            set
            {
            }
        }

        public Round Round
        {
            get => default(Round);
            set
            {
            }
        }
    }
}