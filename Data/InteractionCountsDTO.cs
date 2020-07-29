using SharpCounter.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Data
{
    public class InteractionCountsDTO
    {
        public string Path { get; set; }
        public DateTime Hour { get; set; }
        public int Total { get; set; }
    }
}
