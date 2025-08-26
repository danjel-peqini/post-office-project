using System;
using Helpers;

namespace DTO
{
    public class RoomDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RoomType RoomType { get; set; }
        public int? SeatsNumber { get; set; }
    }

    public class RoomPostDTO
    {
        public string? Name { get; set; }
        public RoomType? RoomType { get; set; }
        public int? SeatsNumber { get; set; }
    }
}
