$(document).ready(function () {

    //initializing multipal date picker function
    //function InitDatepicker() {
    //    $('#datepicker').multiDatesPicker({
    //        beforeShowDay: function (date) {
    //            var dateString = $.datepicker.formatDate('yy-mm-dd', date);
    //            var isBlackoutDate = $('.blackoutDate[value="' + dateString + '"]').length > 0;
    //            if (isBlackoutDate) {
    //                return [true, 'custom-color', 'Blackout Date'];
    //            } else {
    //                return [true, '', ''];
    //            }
    //        }
    //    }).focus();
    //}
    //fetching blackout dates 
    $('.showCalendar').click(function (e) {
        e.preventDefault();
        var roomId = $(this).attr("data-roomId");
        console.log(roomId);


        $.ajax({
            url: `https://localhost:44368/api/HotelApi/getblackoutdatesbyroomid?roomid=${roomId}`,
            method: 'GET',
            success: function (response) {
                console.log(response);

                response.forEach(function (item) {
                    var date = new Date(item.date);            
                   
                    $('#blackoutDates').append('<input type="hidden" class="blackoutDate" value="' + date.toISOString().split('T')[0] + '">');
               
                });

              
                $('#datepicker').multiDatesPicker({
                    beforeShowDay: function (date) {
                        var dateString = $.datepicker.formatDate('yy-mm-dd', date);

                        var isBlackoutDate = $('.blackoutDate[value="' + dateString + '"]').length > 0;

                        if (isBlackoutDate) {
                            return [true, 'custom-color', 'BlackoutDate'];
                        } else {
                            return [true, '', ''];

                        }
                    }
                    
                }).focus();
               
            },
            complete: function () {
                
                console.log("AJAX complete");
           /*     $('#blackoutDates').empty(); */
                
            },
            error: function (xhr, status, error) {
                console.error('Failed to fetch blackout dates:', error);
            }
        });



    });
        //delete method
        $(".delete-room").click(function () {
            var roomId = $(this).attr("room-id");
            console.log(roomId);
            $.ajax({
                type: "DELETE",
                url: `https://localhost:44368/api/HotelApi/DeleteRoomById?id=${roomId}`,
                success: function (data) {
                    console.log(data);

                    window.location.reload();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error("Error deleting hotel:", textStatus, errorThrown);
                }
            });
        });


});