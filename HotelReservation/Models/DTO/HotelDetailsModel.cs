using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Models.DTO
{
    public class HotelDetailsModel
    {
        [Key]
        public int HotelId { get; set; }
        [Required]
        public string HotelName { get; set; }
        [Required]
        public string HotelAddress { get; set; }
        public string? HotelDescription { get; set; }
        public string? BlockedChildRange { get; set; }
        public string? HotelImage { get; set; }
        public IFormFile? HotelImageFile { get; set; }
        public decimal Rating { get; set; }
        public ICollection<HotelRoomModel>? RoomDetails { get; set; }
     

    }
}
