using HotelReservation.Models.DTO;

namespace HotelReservation.Repository.Interfaces
{
    public interface IHotel
    {
        Task<HotelDetailsModel> UploadHotelImage(HotelDetailsModel model);
        Task<HotelRoomModel> UploadRoomImage(HotelRoomModel model);
        Task<List<HotelDetailsModel>> GetAllHotelsDetails();
        Task<List<HotelRoomModel>> GetRoomDetailsByHotelId(int id);
        Task<HotelRoomModel> GetRoomDetailsByRoomId(int id);
        Task<bool> AddHotelDetails(HotelDetailsModel Hotelmodel);
        Task<HotelDetailsModel> GetHotelsDetailsById(int id);
        Task<bool> EditHotelDetail(HotelDetailsModel hotelModel);
        Task<bool> EditRoomByRoomId(HotelRoomModel roomModel);
        Task<bool> AddRoomDetails(HotelRoomModel model);
    }
}
