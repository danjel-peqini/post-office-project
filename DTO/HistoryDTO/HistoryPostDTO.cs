using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.HistoryDTO
{
    public class HistoryPostDTO
    {
        public int? ActionId { get; set; }
        public string? Description { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
