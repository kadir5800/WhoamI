using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.DataTable
{
    public class dataTableRequest
    {
        public string Draw { get; set; }
        public string Start { get; set; }
        public string Length { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDir { get; set; }
        public string SearchValue { get; set; }
    }
}
