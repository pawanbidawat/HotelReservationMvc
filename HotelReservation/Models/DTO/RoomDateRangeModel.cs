using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Models.DTO
{
    public class RoomDateRangeModel
    {
        [Key]
        public int DateRangeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal? SingleRate { get; set; }
        public decimal? DoubleRate { get; set; }
        public decimal? TripleRate { get; set; }
        public decimal? AdultRate { get; set; }
        public decimal? ChildRate { get; set; }
        public bool SingleEqualDouble { get; set; }
        public bool ExceptionCase { get; set; }
        public bool NoExtraAdult { get; set; }
        public bool NoChild { get; set; }

        // Foreign key for Room
        public int RoomId { get; set; }
        public HotelRoomModel? Room { get; set; }
      
    }
}
