using HotelReservation.Models.DTO;

namespace HotelReservation.Repository.Interfaces
{
    public interface IHotel
    {
        Task<HotelDetailsModel> UploadHotelImage(HotelDetailsModel model);
        Task<HotelRoomModel> UploadRoomImage(HotelRoomModel model);
        Task<List<HotelDetailsModel>> GetAllHotelsDetails();
        Task<List<RoomDateRangeModel>> RoomsPriceDetail(int id);
        Task<bool> AddRoomPrice(RoomDateRangeModel model);
        Task<RoomDateRangeModel> EditRoomPrice(int id);
        Task<bool> UpdateRoomPriceDetails(RoomDateRangeModel model);
        Task<List<HotelRoomModel>> GetRoomDetailsByHotelId(int id);
        Task<HotelRoomModel> GetRoomDetailsByRoomId(int id);
        Task<bool> AddHotelDetails(HotelDetailsModel Hotelmodel);
        Task<HotelDetailsModel> GetHotelsDetailsById(int id);
        Task<bool> EditHotelDetail(HotelDetailsModel hotelModel);
        Task<bool> EditRoomByRoomId(HotelRoomModel roomModel);
        Task<bool> AddRoomDetails(HotelRoomModel model);
    }
}
