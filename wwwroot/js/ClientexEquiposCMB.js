$("#ClienteID").change(function () {
    fillCombo("EquipoID", $("#ClienteID").val());
});

function fillCombo(updateId, value) {
    $.getJSON("Home/ClientChange"
        + "/" + value,
        function (data) {
            $("#" + updateId).empty();
            $.each(data, function (i, item) {
                $("#" + updateId).append("<option  value='"
                    + item.equipoID + "'>" + item.descripcion
                    + "</option >");
            });
        });
}