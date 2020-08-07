using System;

namespace Application.DTO
{
    public class InteractionByPathCountsDTO
    {
        public string Path { get; set; }
        public DateTime Hour { get; set; }
        public int Total { get; set; }
    }
}
