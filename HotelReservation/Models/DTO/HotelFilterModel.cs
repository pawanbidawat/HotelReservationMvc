namespace HotelReservation.Models.DTO
{
    public class HotelFilterModel
    {
        public int Adult { get; set; }
        public int Child { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public HotelRoomModel? HotelRoom { get; set; }
        public HotelDetailsModel? HotelDetails { get; set; }
    }
}
