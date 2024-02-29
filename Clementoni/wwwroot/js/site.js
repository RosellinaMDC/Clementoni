// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function caricamentoListPersona() {
    $.get({
        url: "/Home/_ListPersona",
        cache: false
    }).done(function (data) {
        $("#ListPersona").html(data);
    }).fail(function (data) {
        console.log("Errore");
    });
}

function caricamentoFormPersona() {
    $.get({
        url: "/Home/_FormPersona",
        cache: false
    }).done(function (data) {
        $("#FormPersona").html(data);
    }).fail(function (data) {
        console.log("Errore");
    });
}

function modificaPersona(id) {
    $.ajax({
        type: "POST",
        url: "/Home/EditPersona",
        data: { id: id },

        success: function (data) {
            $("#PopupPersona").html(data);
        }
    });
}
