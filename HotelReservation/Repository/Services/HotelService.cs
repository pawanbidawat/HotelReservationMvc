using HotelReservation.Models.DTO;
using HotelReservation.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelReservation.Repository.Services
{
    public class HotelService:IHotel
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly IConfiguration _configuration;
        public HotelService(IWebHostEnvironment webHost, IConfiguration configuration)
        {

            _webHost = webHost;
            _configuration = configuration;
        }

        //get All Hotel details
        public async Task<List<HotelDetailsModel>> GetAllHotelsDetails()
        {
            var Client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("HotelApiUrl").Value}/GetAllHotelsDetails";


            var response = await Client.GetAsync(apiUrl);

            var data = JsonConvert.DeserializeObject<List<HotelDetailsModel>>(await response.Content.ReadAsStringAsync());
            return data;
        }

        //get hotel room detail by hotel id
        public async Task<List<HotelRoomModel>> GetRoomDetailsByHotelId(int id)
        {
            var client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("HotelApiUrl").Value}/GetRoomDetailsByHotelId?Id={id}";
            var response = await client.GetAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<HotelRoomModel>>(await response.Content.ReadAsStringAsync());
            return data;
        }

        //get room date range price detail by room id 
        public async Task<List<RoomDateRangeModel>> RoomsPriceDetail(int id)
        {
            var client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("HotelApiUrl").Value}/GetRoomDateRange?id={id}";
            var response = await client.GetAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<List<RoomDateRangeModel>>(await response.Content.ReadAsStringAsync());
            return data;
        }

        //add new room price details
        public async Task<bool> AddRoomPrice(RoomDateRangeModel model)
        {
            var client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("HotelApiUrl").Value}/AddRoomDateRange";
            var jsonvalue = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonvalue, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }

        //get room date range price details by daterangeid
        public async Task<RoomDateRangeModel> EditRoomPrice(int id)
        {
            var client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("HotelApiUrl").Value}/GetRoomDateRangeByDateRangeId?id={id}";
            var response = await client.GetAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<RoomDateRangeModel>(await response.Content.ReadAsStringAsync());
            return data;
        }

        //updating room date range price details
        public  async Task<bool> UpdateRoomPriceDetails(RoomDateRangeModel model)
        {           
            var Client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("HotelApiUrl").Value}/UpdateRoomDateRangeByDateRangeId";
            var jasonValue = JsonConvert.SerializeObject(model);
            var content = new StringContent(jasonValue, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        //get room details by room id 
        public async Task<HotelRoomModel> GetRoomDetailsByRoomId(int id)
        {
            var Client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("HotelApiUrl").Value}/GetRoomDetailsByRoomId?id={id}";
            var response = await Client.GetAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<HotelRoomModel>(await response.Content.ReadAsStringAsync());
            return data;
        }

        //add hotel details
        public async Task<bool> AddHotelDetails(HotelDetailsModel Hotelmodel)
        {
            var UpdatedModel = await UploadHotelImage(Hotelmodel);


            var client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("HotelApiUrl").Value}/AddHotelDetails";
            var jsonvalue = JsonConvert.SerializeObject(UpdatedModel);
            var content = new StringContent(jsonvalue, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        //get hotel detail by id 
        public async Task<HotelDetailsModel> GetHotelsDetailsById(int id)
        {
            var Client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("HotelApiUrl").Value}/GetHotelsDetailsById?id={id}";

            var response = await Client.GetAsync(apiUrl);

            var data = JsonConvert.DeserializeObject<HotelDetailsModel>(await response.Content.ReadAsStringAsync());
            return data;
        }
        
        //edit hotel details
        public async Task<bool> EditHotelDetail(HotelDetailsModel hotelModel)
        {
            var model =await UploadHotelImage(hotelModel);
            var Client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("HotelApiUrl").Value}/EditHotelDetail?id={hotelModel.HotelId}";
            var jasonValue = JsonConvert.SerializeObject(model);
            var content = new StringContent(jasonValue, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(apiUrl, content);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        //edit room details
        public async Task<bool> EditRoomByRoomId(HotelRoomModel roomModel)
        {
            var model = await UploadRoomImage(roomModel);
            var Client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("HotelApiUrl").Value}/EditRoomByRoomId?id={roomModel.RoomId}";
            var jsonValue = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(apiUrl, content);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        //add new room 
        public async Task<bool> AddRoomDetails(HotelRoomModel model)
        {
            var updatedModel = await UploadRoomImage(model);
            var client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("HotelApiUrl").Value}/AddRoomDetails";
            var jasonValue = JsonConvert.SerializeObject(updatedModel);
            var content = new StringContent(jasonValue, encoding: Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        //image method 
        public async Task<HotelDetailsModel> UploadHotelImage(HotelDetailsModel model)
        {
            if (model.HotelImage != null && model.HotelImageFile !=null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/HotelImages", model.HotelImage);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

            }
            if (model.HotelImageFile != null && model.HotelImageFile.Length > 0)
            {

                var uploadFolder = Path.Combine(_webHost.WebRootPath, "images/HotelImages");
                var fileName = model.HotelName.ToString() + "." + model.HotelImageFile.FileName;
                var filePath = Path.Combine(uploadFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.HotelImageFile.CopyToAsync(fileStream);
                }
                model.HotelImage = fileName;
            }
            return model;
        }

        public async Task<HotelRoomModel> UploadRoomImage(HotelRoomModel model)
        {
            if (model.RoomImage != null && model.RoomImageFile != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/RoomImages", model.RoomImage);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

            }
            if (model.RoomImageFile != null && model.RoomImageFile.Length > 0)
            {

                var uploadFolder = Path.Combine(_webHost.WebRootPath, "images/RoomImages");
                var fileName = model.RoomType.ToString() + "." + model.RoomImageFile.FileName;
                var filePath = Path.Combine(uploadFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.RoomImageFile.CopyToAsync(fileStream);
                }
                model.RoomImage = fileName;
            }
            return model;
        }

    }
}
