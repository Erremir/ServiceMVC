    $(".cmborigen").change(function () {
        fillCombo("cmbdestino", $(".cmborigen").val(),$(".cmborigen").data("id"));
    });


    function fillCombo(updateId, value,action) {
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