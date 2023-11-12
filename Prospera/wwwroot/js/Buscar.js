$(document).ready(function () {
    $("#BtnTerceirosBuscar").click(function () {
        buscarTerceiros();
    });
    $("#BtnTerceirosLimpar").click(function () {
        // Desbloqueia o input
        limparTerceiros();
    }); 
    $("#BtnDespesasConsultar").click(function () {
        BuscarDespesas();
    });
    $("#BtnDespesasLimpar").click(function () {
        // Desbloqueia o input
        limparDespesas();
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
    $("#TxtTerceirosId").prop("readonly", true);

}

function limparTerceiros() {
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

function BuscarDespesas() {
    console.log("function JS chamada.");
    var CodigoCont = $("#TxtDespesasBuscaId").val();
    console.log(CodigoCont);

    $.ajax({
        url: "/Contas/BuscarDespesas",
        method: "GET",
        data: { id: CodigoCont },
        success: function (data) {
            if (data) {
                preencherDespesas(data);
                console.log("asdasdasd.");
                console.log(data);
            } else {
                console.log("Conta não encontrado no banco de dados.");
            }
        },
        error: function () {
            alert("Erro ao buscar Conta.");
        }
    });
}

function preencherDespesas(contas) {
    $("#TxtDespesasNome").val(contas.nomeCont);
    $("#TxtDespesasObservacao").val(contas.observacaoCont);
    $("#TxtDespesasValor").val(contas.valorCont);
    $("#TxtDespesasData").val(contas.datVenciCont);
    $("#MetodoPgtoCont").val(contas.metodoPgtoCont);
    $("#StatusCont").val(contas.statusCont);
    $("#DespesasDropPessoaDespesas").val(contas.recebedorCont);
    $("#TxtDespesasBuscaId").val(contas.codigoCont);
    $("#idContasTemp").val(contas.idCont);
    idContasTemp = contas.nomeCont;

    //Bloqueia o Input ID
    $("#TxtDespesasBuscaId").prop("readonly", true);
}

function limparDespesas() {
    $("#TxtDespesasNome").val("");
    $("#TxtDespesasObservacao").val("");
    $("#TxtDespesasValor").val("");
    $("#TxtDespesasData").val("");
    $("#MetodoPgtoCont").val("");
    $("#StatusCont").val("");
    $("#DespesasDropPessoaDespesas").val("");
    $("#TxtDespesasBuscaId").val("");
    $("IdTerceirosTemp").val("");

    //Desbloqueia o Input ID
    $("#TxtDespesasBuscaId").prop("readonly", false);
}