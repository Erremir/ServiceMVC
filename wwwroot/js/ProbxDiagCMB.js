$("#ProblemaID").change(function () {
    fillCombo("DiagnosticoID", $("#ProblemaID").val());
});

function fillCombo(updateId, value) {
    $.getJSON("Home/ClientChange"
        + "/" + value,
        function (data) {
            $("#" + updateId).empty();
            $.each(data, function (i, item) {
                $("#" + updateId).append("<option  value='"
                    + item.diagnosticoID + "'>" + item.descripcion
                    + "</option >");
            });
        });
}