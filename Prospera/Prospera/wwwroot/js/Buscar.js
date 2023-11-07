$(document).ready(function () {
    $("#BtnTerceirosBuscar").click(function () {
        buscarTerceiros();
    });
    $("#BtnTerceirosLimpar").click(function () {
        // Desbloqueia o input
        limparInputs();
    });
});

var idTerceirosTemp;

function buscarTerceiros() {
    var idTerceiros = $("#TxtTerceirosId").val();

    $.ajax({
        url: "/MenuUsuario/BuscarTerceiros",
        method: "GET",
        data: { id: idTerceiros },
        success: function (data) {
            if (data) {
                preencherCampos(data);
                console.log("asdasdasd.");
                console.log(data);
            } else {
                console.log("Terceiros não encontrado no banco de dados.");
            }
        },
        error: function () {
            alert("Erro ao buscar Terceiros.");
        }
    });
}

function preencherCampos(terceiros) {
    $("#TxtTerceirosId").val(terceiros.idTerceiros);
    $("#TxtTerceirosNome").val(terceiros.nomeTerceiros);
    $("#TxtTerceirosEmail").val(terceiros.emailTerceiros);
    $("#TxtTerceirosTelefone").val(terceiros.telefoneTerceiros);
    $("#TxtTerceirosTelefone2").val(terceiros.telefone2Terceiros);
    $("#TxtTerceirosEndereco").val(terceiros.enderecoTerceiros);
    $("#TxtTerceirosCidade").val(terceiros.cidadeTerceiros);
    $("#TxtTerceirosBairro").val(terceiros.bairroTerceiros);
    $("#TxtTerceirosUF").val(terceiros.ufTerceiros);
    $("#TxtTerceirosCEP").val(terceiros.cepTerceiros);
    $("#TxtTerceirosObservacao").val(terceiros.observacaoTerceiros);
    $("IdTerceirosTemp").val(terceiros.idTerceiros);
    idTerceirosTemp = terceiros.idTerceiros;

    // Bloqueia o input ID
    $("#TxtTerceirosEmail").prop("readonly", true);

}

function limparInputs() {
    $("#TxtTerceirosId").prop("disabled", false);
    $("#TxtTerceirosId").val("");
    $("#TxtTerceirosNome").val("");
    $("#TxtTerceirosEmail").val("");
    $("#TxtTerceirosTelefone").val("");
    $("#TxtTerceirosTelefone2").val("");
    $("#TxtTerceirosEndereco").val("");
    $("#TxtTerceirosCidade").val("");
    $("#TxtTerceirosBairro").val("");
    $("#TxtTerceirosUF").val("");
    $("#TxtTerceirosCEP").val("");
    $("#TxtTerceirosObservacao").val("");
}

