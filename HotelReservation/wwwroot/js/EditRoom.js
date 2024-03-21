$(document).ready(function () {

    $("#singleEqualDouble").change(function () {

        if (this.checked) {

            var doubleRateValue = $('#doubleRate').val();
            $('#singleRate').val(doubleRateValue).prop('disabled', true);
        } else {

            $('#singleRate').prop('disabled', false);
        }
    });

    $("#noExtraAdult").change(function () {

        if (this.checked) {

            $('#adultRate').val('').prop('disabled', true);
        } else {

            $('#adultRate').prop('disabled', false);
        }
    });

    $("#noChild").change(function () {
        debugger;
        if (this.checked) {
            $('#childRate').val('').prop('disabled', true);
        } else {
            $('#childRate').val('').prop('disabled', false);
        }
    });

});
