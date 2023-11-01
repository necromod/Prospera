$(document).ready(function () {
    $("#exibirMenu").click(function () {
        console.log("Botão 'Exibir Menu' clicado.");
        carregarMenu();
    });

    $("#mostrarTerceiros").click(function () {
        carregarViewComponent("Terceiros", "ComponentesView");
    });

    $("#MostrarContas").click(function () {
        carregarViewContas("Contas", "ComponentesView");
    });

    $(".cancelarExibicaoTerceiros").click(function () {
        $(".ComponentesView").empty();       
    });


});

function carregarMenu() {
    console.log("Chamando a função 'carregarMenu'."); // Adicione esta linha para depuração
    $.ajax({
        url: "/Home/CarregarMenu", // Certifique-se de que o URL esteja correto
        type: "GET",
        success: function (data) {
            console.log("Sucesso ao carregar o menu dinamicamente."); // Adicione esta linha para depuração
            $(".ComponentesView").html(data);
        },
        error: function (error) {
            console.log("Erro ao carregar o menu dinamicamente: " + error); // Adicione esta linha para depuração
        }
    });
}

function carregarViewComponent(viewComponentName, targetElementId) {
    $.ajax({
        url: "/Home/CarregarViewComponent?viewComponentName=" + viewComponentName,
        type: "GET",
        success: function (data) {
            $("." + targetElementId).html(data);
        },
        error: function (error) {
            console.log("Erro ao carregar o ViewComponent: " + error);
        }
    });
}
function carregarViewContas(viewComponentContas, targetElementId) {
    $.ajax({
        url: "/Home/carregarViewContas?viewComponentContas=" + viewComponentContas,
        type: "GET",
        success: function (data) {
            $("." + targetElementId).html(data);
        },
        error: function (error) {
            console.log("Erro ao carregar o ViewComponent: " + error);
        }
    });
}