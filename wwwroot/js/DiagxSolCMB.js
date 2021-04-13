$(".cmborigen").change(function () {
    fillCombo("cmbdestino", $(".cmborigen").val(), $(".cmborigen").data("id"));
    $(".cmbdestino").change();
});

$(".cmbdestino").change(function () {
    fillCombo2("cmbdestino2", $(".cmbdestino").val(), $(".cmbdestino").data("id"));
});

//$(".cmbdestino").focus(function () {
//    fillCombo2("cmbdestino2", $(".cmbdestino").val(), $(".cmbdestino").data("id"));
//});


$(".cmborigen1").change(function () {
    fillCombo("cmbdestino1", $(".cmborigen1").val(), $(".cmborigen1").data("id"));
});


function fillCombo(updateId, value, action) {
    $.getJSON(action
        + "/" + value,
        function (data) {
            $("." + updateId).empty();
            $.each(data, function (i, item) {
                $("." + updateId).append("<option  value='"
                    + item.diagnosticoID + "'>" + item.descripcion
                    + "</option >");
            });
        });
}

function fillCombo2(updateId, value, action) {
    $.getJSON(action
        + "/" + value,
        function (data) {
            $("." + updateId).empty();
            $.each(data, function (i, item) {
                $("." + updateId).append("<option  value='"
                    + item.solucionID + "'>" + item.descripcion
                    + "</option >");
            });
        });
}
