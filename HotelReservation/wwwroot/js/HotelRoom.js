$(document).ready(function () {

    //experiment
    $("#ClickMe").click(function () {
        debugger
        $(".FocusBlock").css("display","block");
    })

    //$('#datepicker').blur(function () {
    //    $("#blackoutDates").hide();
    //})    


    //fetching blackout dates 
    $('.showCalendar').click(function (e) {
        $('#blackoutDates').show();
        e.preventDefault();
        var roomId = $(this).attr("data-roomId");
        console.log(roomId);



        $.ajax({
            url: `${HotelApi}/getblackoutdatesbyroomid?roomid=${roomId}`,
            method: 'GET',
            success: function (response) {
                console.log(response);

                response.forEach(function (item) {
                    // Adjust the date to the local time zone
                    var date = new Date(item.date);
                    date.setMinutes(date.getMinutes() - date.getTimezoneOffset());
                    console.log(date);

                    // Append the adjusted date to #blackoutDates
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
            url: `${HotelApi}/DeleteRoomById?id=${roomId}`,
            success: function (data) {
                console.log(data);

                window.location.reload();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Error deleting hotel:", textStatus, errorThrown);
            }
        });
    });

    //delete Room Price details method
    $('.deleteRoomPrice').click(function () {

        var id = $(this).attr('date-range-id');


        $.ajax({
            type: "DELETE",
            url: `${HotelApi}/DeleteRoomPriceByDateRangeId?id=${id}`,
            success: function (data) {
                console.log(data);

                window.location.reload();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Error deleting hotel:", textStatus, errorThrown);
            }
        });
    });

    function formateDate(dates) {
       var formatedDates =  dates.split(',').map(function (dateString) {
            var parts = dateString.split('/');
            var year = parts[2];
            var month = parts[0].padStart(2, '0');
            var day = parts[1].padStart(2, '0');
            return `${year}-${month}-${day}`;
       });
        return formatedDates;
    }
    //blackout date submit button
    $("#submitBtn").click(function () {
        var dates = $("#datepicker").val();
        var roomId = $("#roomId").val();
       


        var formattedDates = formateDate(dates);
        console.log("Formatted Dates: ", formattedDates);

        $.ajax({
            type: "POST",
            url: `${HotelApi}/Addblackoutdates?roomId=${roomId}`,
            data: JSON.stringify(formattedDates),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log(data);

            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Error Adding BlackOut:", textStatus, errorThrown);
            },
            complete: function () {
                window.location.reload();
            }
        });
    });
    //blackOut date remove button method

    $("#removeBtn").click(function () {
        var dates = $("#datepicker").val();
        var roomId = $("#roomId").val();
  


        var formattedDates = formateDate(dates);
        console.log("Formatted Dates: ", formattedDates);

        $.ajax({
            type: "POST",
            url: `${HotelApi}/RemoveBlackoutDates?roomId=${roomId}`,
            data: JSON.stringify(formattedDates),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log(data);

            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Error Adding BlackOut:", textStatus, errorThrown);
            },
            complete: function () {
                window.location.reload();
            }
        });
    });

});