using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Курсовой_проект.Models
{
    public class MarkModel
    {
        public int MarkId { get; set; }
        public string MarkName { get; set; }
        public string Description { get; set; }

        public MarkModel() { }
        public MarkModel(int markId, string markName, string description) {
            MarkId = markId;
            MarkName = markName;
            Description = description;
        }
    }
}
