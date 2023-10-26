
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
      
            // Mostrar a tela de receitas novamente
            telaOcultaDespesas.style.display = "none";
          })







    });




