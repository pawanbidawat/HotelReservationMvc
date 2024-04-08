$(document).ready(function () {
    
    $('.delete').click(function () {

        var id = $(this).attr('hotel-id');


        $.ajax({
            type: "DELETE",
            url: `${HotelApi}/RemoveHotel?id=${id}`,
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