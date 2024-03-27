using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Models.DTO
{
    public class HotelRoomModel
    {
        [Key]
        public int RoomId { get; set; }
        public string? RoomType { get; set; }
        public string? Description { get; set; }
        public string? RoomImage { get; set; }
        public IFormFile? RoomImageFile { get; set; }

        // Foreign key for Hotel
        public int HotelId { get; set; }
        public HotelDetailsModel? Hotel { get; set; }
        public List<BlackoutDateModel>? BlackoutDates { get; set; }

        // Collection of date ranges for the room
        public List<RoomDateRangeModel>? DateRanges { get; set; }
    }
}
