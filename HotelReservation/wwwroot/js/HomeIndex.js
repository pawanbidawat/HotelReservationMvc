
    $(document).ready(function () {
        $('.ParentBox').on('click', function () {
            var hotelId = $(this).find('.hotel-id').val();
            console.log(hotelId);
            //var url = '@Url.Action("HotelRooms", "Home")' + '?hotelId=' + hotelId;
            window.location.href = "/Home/HotelRooms?hotelId=" + hotelId;
        });

        //date method
        $('#DateFrom, #DateTo').change(function () {
            var dateFrom = $('#DateFrom').val().substring(0, 10);
            var dateTo = $('#DateTo').val().substring(0, 10);
            var currentDate = new Date().toISOString().substring(0, 10);
            if (dateFrom && dateTo) {
                if (dateFrom < currentDate || dateTo < dateFrom) {
                    $('#DateFrom').val('');
                    $('#DateTo').val('');

                    alert("Slected date are invalid");
                    $('.CheckBtn button').prop('disabled', true);
                }
                $('.CheckBtn button').prop('disabled', false);
            }

        });

  


    $("#searchForm").submit(function (event) {
        event.preventDefault();
    var searchValue = $('input[name="search"]').val();
    var selectedAdults = $('#Adults').val();
    console.log(selectedAdults);
    console.log(searchValue);

    $.ajax({
        method:"GET",
    url: "/Home/SearchHotel",
    data:{
        searchValue: searchValue
                },
    success: function (data) {
        $("#HotelDisplay").html(data);
    console.log(data);
                },
    error: function () {
        console.error("Failed to load the partial view.");
                }
            });
        })
    
    });

