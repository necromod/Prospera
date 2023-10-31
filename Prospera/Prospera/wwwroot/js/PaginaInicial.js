
// Funcao que ira mostrar as telas quando clicar nos botões dos menus 

//exibindo a tela
document.addEventListener("DOMContentLoaded", function () {
  //Tela Receitas
  const VoltarReceitas = document.getElementById("BtnReceitasVoltar");
  const botaoMostrarReceitas = document.getElementById("BtnReceitas");
  const telaOcultaReceita = document.getElementById("FundoMenusReceita");

  //Tela Despesas
  const VoltarDespesas = document.getElementById("BtnDespesasVoltar");
  const botaoMostrarDespesas = document.getElementById("BtnDespesas");
  const telaOcultaDespesas = document.getElementById("FundoMenusDespesa");

  //Animacao de carregamento
  const AnimacaoCarregamento = document.getElementById("CarregamentoMenu");

  //BotaoReceitas
  botaoMostrarReceitas.addEventListener("click", function () {
    // Mostrar o elemento de carregamento

    AnimacaoCarregamento.style.display = "block";
    // Ocultar a tela de receitas
    telaOcultaReceita.style.display = "none";

    // Simular uma operação demorada (substitua isso pela sua própria função)
    setTimeout(function () {
      // Ative a tela definindo a opacidade para 1
      telaOcultaReceita.style.opacity = 1;

      // Ocultar o elemento de carregamento
      AnimacaoCarregamento.style.display = "none";

      // Mostrar a tela de receitas novamente
      telaOcultaReceita.style.display = "block";

    }, 500); // Tempo de simulação (.5 segundos)
  });

  VoltarReceitas.addEventListener("click", function () {

    // Mostrar a tela de receitas novamente
    telaOcultaReceita.style.display = "none";
  });

  //BotaoDespesas
  botaoMostrarDespesas.addEventListener("click", function () {
    // Mostrar o elemento de carregamento

    AnimacaoCarregamento.style.display = "block";
    // Ocultar a tela de receitas
    telaOcultaDespesas.style.display = "none";

    // Simular uma operação demorada (substitua isso pela sua própria função)
    setTimeout(function () {
      // Ative a tela definindo a opacidade para 1
      telaOcultaDespesas.style.opacity = 1;

      // Ocultar o elemento de carregamento
      AnimacaoCarregamento.style.display = "none";

      // Mostrar a tela de receitas novamente
      telaOcultaDespesas.style.display = "block";

    }, 500); // Tempo de simulação (.5 segundos)
  });

  VoltarDespesas.addEventListener("click", function () {


    telaOcultaDespesas.style.display = "none";
  })




  //#region Alteraçao sabado


  //Conta Bancaria

  const VoltarContBanc = document.getElementById("BtnContBancVoltar");
  const botaoMostrarContBanc = document.getElementById("BtnContasBancaria");
  const telaOcultaContBanc = document.getElementById("FundoMenusContasBanc");



  botaoMostrarContBanc.addEventListener("click", function () {
    // Mostrar o elemento de carregamento

    AnimacaoCarregamento.style.display = "block";
    // Ocultar a tela de receitas
    telaOcultaContBanc.style.display = "none";

    // Simular uma operação demorada (substitua isso pela sua própria função)
    setTimeout(function () {
      // Ative a tela definindo a opacidade para 1
      telaOcultaContBanc.style.opacity = 1;

      // Ocultar o elemento de carregamento
      AnimacaoCarregamento.style.display = "none";

      // Mostrar a tela de receitas novamente
      telaOcultaContBanc.style.display = "block";

    }, 500); // Tempo de simulação (.5 segundos)
  });

  VoltarContBanc.addEventListener("click", function () {


    telaOcultaContBanc.style.display = "none";
  })
  
//Consulta contas bancarias
//Conta Bancaria

  const VoltarContBancConsult = document.getElementById("BtnVoltarConsultCont");
  const botaoMostrarContBancConsult = document.getElementById("BtnContaBancLista");
  const telaOcultaContBancConsult = document.getElementById("ConsultContBanc");


  botaoMostrarContBancConsult.addEventListener("click", function () {
    // Mostrar o elemento de carregamento

    AnimacaoCarregamento.style.display = "block";
    // Ocultar a tela de receitas
    telaOcultaContBancConsult.style.zIndex="99999";
    telaOcultaContBancConsult.style.display = "none";

    // Simular uma operação demorada (substitua isso pela sua própria função)
    setTimeout(function () {
      // Ative a tela definindo a opacidade para 1
      telaOcultaContBancConsult.style.opacity = 1;

      // Ocultar o elemento de carregamento
      AnimacaoCarregamento.style.display = "none";

      // Mostrar a tela de receitas novamente
      telaOcultaContBancConsult.style.display = "block";

    }, 200); // Tempo de simulação (.5 segundos)
  });

  VoltarContBancConsult.addEventListener("click", function () {


    telaOcultaContBancConsult.style.display = "none";
  })

// FIM CONSULTA CONT BANCARIA







 //Conta Devedor/pagador

 const VoltarTerceiros = document.getElementById("BtnTerceirosVoltar");
 const botaoMostrarTerceiros = document.getElementById("BtnCadastroDevedorPagador");
 const telaOcultaTerceiros = document.getElementById("FundoMenusTerceiros");



 botaoMostrarTerceiros.addEventListener("click", function () {
   // Mostrar o elemento de carregamento

   AnimacaoCarregamento.style.display = "block";
   // Ocultar a tela de receitas
   telaOcultaTerceiros.style.display = "none";

   // Simular uma operação demorada (substitua isso pela sua própria função)
   setTimeout(function () {
     // Ative a tela definindo a opacidade para 1
     telaOcultaTerceiros.style.opacity = 1;

     // Ocultar o elemento de carregamento
     AnimacaoCarregamento.style.display = "none";

     // Mostrar a tela de receitas novamente
     telaOcultaTerceiros.style.display = "block";

   }, 500); // Tempo de simulação (.5 segundos)
 });

 VoltarTerceiros.addEventListener("click", function () {
  //Ocultar
   telaOcultaTerceiros.style.display = "none";
 })


 //Abrir tela de consulta
 const BtnTerceirosVoltarConsult = document.getElementById("BtnTerceirosVoltarConsult");
 const BtnTerceirosConsultar = document.getElementById("BtnTerceirosConsultar");
 const FundoMenusTerceirosConsult = document.getElementById("FundoMenusTerceirosConsult");



 BtnTerceirosConsultar .addEventListener("click", function () {
   // Mostrar o elemento de carregamento

   // Simular uma operação demorada (substitua isso pela sua própria função)
   setTimeout(function () {
     // Ative a tela definindo a opacidade para 1
     FundoMenusTerceirosConsult.style.opacity = 1;

     // Ocultar o elemento de carregamento
     AnimacaoCarregamento.style.display = "none";

     // Mostrar a tela de receitas novamente
     FundoMenusTerceirosConsult.style.display = "block";

   }, 200); // Tempo de simulação (.5 segundos)
 });

 BtnTerceirosVoltarConsult.addEventListener("click", function () {
  //Ocultar
  FundoMenusTerceirosConsult.style.display = "none";
  telaOcultaTerceiros.style.display="block";
 })



 //TelaConsulta endereço

 const BtnTerceirosVoltarConsultEnd = document.getElementById("BtnTerceirosVoltarConsultEnd");
 const BtnTerceirosConsultarEnd = document.getElementById("BtnTerceirosEndercosConsult");
 const FundoMenusTerceirosConsultEnd = document.getElementById("TerceirosConsultEnd");

 BtnTerceirosConsultarEnd .addEventListener("click", function () {
  // Mostrar o elemento de carregamento

  // Simular uma operação demorada (substitua isso pela sua própria função)
  setTimeout(function () {

    FundoMenusTerceirosConsultEnd.style.opacity = 1;
    FundoMenusTerceirosConsultEnd.style.display = "block";
    FundoMenusTerceirosConsultEnd.style.zIndex = "99999";

  }, 200); // Tempo de simulação (.5 segundos)
});

BtnTerceirosVoltarConsultEnd.addEventListener("click", function () {
  FundoMenusTerceirosConsultEnd.style.display = "none";
  FundoMenusTerceirosConsult.style.display="block";
})



 //TRANSACAO 
 
 const VoltarTransacao = document.getElementById("BtnTransacaoVoltarConsult");
 const botaoMostrarTransacao = document.getElementById("BtnTransacoes");
 const telaOcultaTransacao= document.getElementById("FundoMenusTransacaoConsult");



 botaoMostrarTransacao.addEventListener("click", function () {
   // Mostrar o elemento de carregamento

   AnimacaoCarregamento.style.display = "block";
   // Ocultar a tela de receitas
   telaOcultaTransacao.style.display = "none";

   // Simular uma operação demorada (substitua isso pela sua própria função)
   setTimeout(function () {
     // Ative a tela definindo a opacidade para 1
     telaOcultaTransacao.style.opacity = 1;

     // Ocultar o elemento de carregamento
     AnimacaoCarregamento.style.display = "none";

     // Mostrar a tela de receitas novamente
     telaOcultaTransacao.style.display = "block";

   }, 500); // Tempo de simulação (.5 segundos)
 });

 VoltarTransacao.addEventListener("click", function () {


  telaOcultaTransacao.style.display = "none";
 })

















  //Logo dos bancos



});




