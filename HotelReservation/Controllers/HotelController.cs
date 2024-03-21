using HotelReservation.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using Newtonsoft;
using HotelReservation.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HotelReservation.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotel _hotelService;
        public HotelController(IHotel hotelService)
        {
            _hotelService = hotelService;
        }
        public async Task<IActionResult> Index()
        {          

            var data = await _hotelService.GetAllHotelsDetails();

            return View(data);
        }

        public async Task<IActionResult> HotelRoomDetails(int id)
        {            
            var data =await _hotelService.GetRoomDetailsByHotelId(id);
            ViewData["HotelId"] = id;

            return View(data);
        }

        //Add Hotel display
        [Authorize(Roles = "admin")]
        public IActionResult AddHotelDisplay()
        {
            return View();
        }
        //adding new hotel
        
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddHotel(HotelDetailsModel Hotelmodel)
        {
            var data = await _hotelService.AddHotelDetails(Hotelmodel);
            if (data != null)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("AddHotelDisplay");
        }

        //edit hotel display
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditHotelDisplay(int id)
        {
            var data = await _hotelService.GetHotelsDetailsById(id);

            if (data != null)
            {
                return View(data);
            }

            return View();
        }


        //updating hotel details
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateHotelDetails(HotelDetailsModel hotelModel)
        {
            
            var response = await  _hotelService.EditHotelDetail(hotelModel);

            if (response != null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("EditHotelDisplay");
        }


        //EditRoomDisplay
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditRoomDisplay(int id)
        {           
            var data = await _hotelService.GetRoomDetailsByRoomId(id);
            ViewData["RoomId"] = id;

            return View(data);
        }

        //edit room action
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditRoom(HotelRoomModel roomModel)
        {

            var response = await _hotelService.EditRoomByRoomId(roomModel);
            if (response != null) {
                return RedirectToAction("HotelRoomDetails", new {id= roomModel.HotelId});
            }
            return RedirectToAction("EditRoomDisplay");
        }


        //AddRoom Display
        [Authorize(Roles = "admin")]
        public IActionResult AddRoomDisplay(int id)
        {
            ViewData["HotelId"] = id;
            return View();
        }

        //AddRoom Method
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddRoom(HotelRoomModel roomModel)
        {

            var response = await _hotelService.AddRoomDetails(roomModel);

            if (response!=null)
            {
                return RedirectToAction("HotelRoomDetails", new { id = roomModel.HotelId });
            }

            return RedirectToAction("AddRoomDisplay");
        }





    }
}
