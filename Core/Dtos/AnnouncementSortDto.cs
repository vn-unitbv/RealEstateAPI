using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Core.Enums;

namespace Core.Dtos
{
    public class AnnouncementSortDto
    {
        public AnnouncementSortCriterion SortCriterion { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}