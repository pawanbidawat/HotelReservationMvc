using HotelReservation.Models;
using HotelReservation.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Nodes;
using System.Text;
using HotelReservation.Repository.Interfaces;

namespace HotelReservation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHotel _hotelService;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger , IHotel hotelService, IConfiguration configuration)
        {
            _logger = logger;
            _hotelService = hotelService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {


            var data = await _hotelService.GetAllHotelsDetails();

            if (data != null && data.Count > 0) 
            { 
            return View(data);
            }
            return View();
        }

        public async Task<IActionResult> HotelRooms(int hotelId)
        {
           
            var data = await _hotelService.GetRoomDetailsByHotelId(hotelId);
            return View(data);

        }

        
        public async Task<IActionResult> FilterRooms(int id )
        {
            var model = new HotelFilterModel
            {
                Adult = UniversalFilterValue.Adult,
                Child = UniversalFilterValue.Child,
                DateFrom = UniversalFilterValue.CheckIn,
                DateTo = UniversalFilterValue.CheckOut,
                hotelId = id
            };
            var client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("HotelApiUrl").Value}/GetHotelFilterRoom";
            var jsonValue = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            var data = JsonConvert.DeserializeObject<List<HotelRoomModel>>(await response.Content.ReadAsStringAsync());


            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> FilterHotel(HotelFilterModel model)
        {
            UniversalFilterValue.Adult = model.Adult;
            UniversalFilterValue.Child = model.Child;
            UniversalFilterValue.CheckIn = model.DateFrom;
            UniversalFilterValue.CheckOut = model.DateTo;
            var client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("HotelApiUrl").Value}/GetHotelAndRoomByDate";
            var jsonValue = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            var data = JsonConvert.DeserializeObject<List<HotelDetailsModel>>(await response.Content.ReadAsStringAsync());


            return View(data);
        }


        [HttpGet]
        public async Task<IActionResult> SearchHotel(string searchValue)
        {
            var client = new HttpClient();
            var apiurl = $"{_configuration.GetSection("HotelApiUrl").Value}/GetHotelSearchResult?searchValue={searchValue}";
            var response = await client.GetAsync(apiurl);

            var data = JsonConvert.DeserializeObject<List<HotelDetailsModel>>(await response.Content.ReadAsStringAsync());
            return PartialView("_HotelDisplay", data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
