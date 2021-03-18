// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#showModal').click(function () {
        var url = $('#myModal').data('url');

        $.get(url, function (data) {
            $('#modal-body').html(data);

            $('#modal-content').modal('show');
        });
    });
});

$(".btnEquipos").click(function(eve) {
    $('#modal-content-personalizado').load("/Equipos/CreatePartial/" + $(this).data("id"));
});

$(".btnLoadModal").click(function (eve) {
    $('#modal-content-personalizado').load($(this).data("id"));
});

