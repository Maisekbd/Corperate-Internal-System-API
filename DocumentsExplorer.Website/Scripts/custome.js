//function DeleteGridRow(grid) {
//    grid.removeRow(row);
//    $('#modal-Confirmation-Delete').modal('hide');
//}

kendo.ui.validator.rules.mvcdate = function (input) {
    //use the custom date format here
    //kendo.parseDate - http://docs.telerik.com/kendo-ui/api/javascript/kendo#methods-parseDate

    return input.val() === "" || kendo.parseDate(input.val(), "dd/MM/yyyy") !== null;
};

$(document).ready(function () {
    kendo.culture("ar-AE");
    if ($(window).width() < 1200) {
        $('.sidebar-toggle')[0].click();
    }
});
