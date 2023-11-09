
// Funcao que ira mostrar as telas quando clicar nos botões dos menus 

//exibindo a tela
/*document.addEventListener("DOMContentLoaded", function () {*/
/*  //Tela Receitas
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
    telaOcultaContBancConsult.style.zIndex = "99999";
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



  BtnTerceirosConsultar.addEventListener("click", function () {
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
    telaOcultaTerceiros.style.display = "block";
  })



  //TelaConsulta endereço

  const BtnTerceirosVoltarConsultEnd = document.getElementById("BtnTerceirosVoltarConsultEnd");
  const BtnTerceirosConsultarEnd = document.getElementById("BtnTerceirosEndercosConsult");
  const FundoMenusTerceirosConsultEnd = document.getElementById("TerceirosConsultEnd");

  BtnTerceirosConsultarEnd.addEventListener("click", function () {
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
    FundoMenusTerceirosConsult.style.display = "block";
  })



  //TRANSACAO 

  const VoltarTransacao = document.getElementById("BtnTransacaoVoltarConsult");
  const botaoMostrarTransacao = document.getElementById("BtnTransacoes");
  const telaOcultaTransacao = document.getElementById("FundoMenusTransacaoConsult");



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


});
*/

//MenuHambuber 
// const AtivaMenu = document.getElementById("AtivaMenu"); 
// const menuAtivo = document.getElementById("OcultaMenu");


// AtivaMenu.addEventListener("click", function () {
//    if (menuAtivo.classList.contains("OcultaMenu")) {
//         menuAtivo.classList.remove("OcultaMenu")
//    }
//    else
//    {
//     menuAtivo.classList.add("OcultaMenu")
//    }
// });

//Fim MenuHamburguer


//API cotacão

//Link api usado https://docs.awesomeapi.com.br/api-de-moedas
$(document).ready(function () {
    // URL da API de cotações
    var apiUrl = 'https://economia.awesomeapi.com.br/last/USD-BRL,CNY-BRL,EUR-BRL';

    // Realize a solicitação AJAX
    $.ajax({
        url: apiUrl,
        type: 'GET',
        success: function (data) {
            // Acessando a taxa de conversão corretamente usando colchetes e aspas
            var Dolar = data.USDBRL.bid;
            var Euro = data.EURBRL.bid;
            var Yuan = data.CNYBRL.bid;

            var DolarFormat = parseFloat(Dolar).toFixed(2);
            var EuroFormat = parseFloat(Euro).toFixed(2);
            var YuanFormat = parseFloat(Yuan).toFixed(2);

            // Definindo o valor da conversão no campo de texto
            $('#TxtDolar').val(DolarFormat);
            $('#TxtEuro').val(EuroFormat);
            $('#TxtYuan').val(YuanFormat);
        },
        error: function (error) {
            console.error('Erro ao buscar a conversão de moeda:');
        }
    });
});

//FIM API Cotação

//API Busca noticias Atual Geral BR

document.addEventListener('DOMContentLoaded', function () {
    const apiKey = '50984f2a91ea4599b9bf798c3a7f6b48';
    const apiUrl = 'https://newsapi.org/v2/top-headlines?sources=google-news-br&apiKey=50984f2a91ea4599b9bf798c3a7f6b48'; // Substitua pela URL da API de notícias
    const newsContainer = document.getElementById('news');
    let currentNews = 0;
    const newsCounterHeading = document.getElementById('ContadorNews'); // Elemento <h4> para o contador
    const nextButton = document.getElementById('proximo');
    const prevButton = document.getElementById('Anterior');
    let totalNewsCount = 0; // Variável para contar o total de notícias encontradas
    //Variáveis pra Consulta de Terceiros


    function showNews(newsIndex) {
        newsCounterHeading.textContent = `Notícia ${currentNews + 1} de ${totalNewsCount}`;
        fetch(apiUrl)
            .then(response => response.json())
            .then(data => {
                if (data.articles && data.articles.length > 0) {
                    totalNewsCount = data.articles.length; // Atualiza o total de notícias
                    if (newsIndex < 0) {
                        currentNews = data.articles.length - 1;
                    } else if (newsIndex >= data.articles.length) {
                        currentNews = 0;
                    }

                    const newsData = data.articles[currentNews];
                    // Atualiza o contador
                    ContadorNews.textContent = `Notícia ${currentNews + 1} de ${totalNewsCount}`;
                    // Verificar se o autor é null e substituir por "Sem autor"
                    const author = newsData.source.name !== null ? newsData.source.name : "Sem autor";
                    const Description = newsData.description !== null ? newsData.description : "Sem Descrição";

                    // Traduzir o título e a descrição para o português
                    traduzirTexto(newsData.title, 'pt')
                        .then(tituloTraduzido => {
                            traduzirTexto(Description, 'pt')
                                .then(descricaoTraduzida => {



                                    const newsHTML = `
                                  <h2 class="NotAtual">Notícias Atuais </h2>
                                  <li class="NewTitulo">
                                      <h2>${tituloTraduzido}</h2>
                                      </li>
                                      <li class="NewAutor">
                                      <p>${author}</p>
                                      </li>
                                      <li class="NewDescrica">
                                      <p>${descricaoTraduzida}</p>
                                      </li>
      
                                      <li class="NewUrl">
                                      <a href="${newsData.url}" target="_blank">Leia mais</a>
                                      </li>
                                      <li class="NewDataPublic">
                                      <p>Data de Publicação: ${new Date(newsData.publishedAt).toLocaleString()}</p>
                                      </li>
                                    
                                  `;
                                    newsContainer.innerHTML = newsHTML;

                                });
                        });
                }
            })
            .catch(error => {
                console.error('Erro na solicitação à API de notícias:', error);
            });
    }

    showNews(currentNews);

    document.getElementById('Anterior').addEventListener('click', function () {
        currentNews--;
        showNews(currentNews);
    });

    document.getElementById('proximo').addEventListener('click', function () {
        currentNews++;
        showNews(currentNews);
    });

    // Função para traduzir o texto usando o Google Translate
    function traduzirTexto(texto, idiomaDestino) {
        const url = `https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=${idiomaDestino}&dt=t&q=${encodeURI(texto)}`;

        return fetch(url)
            .then(response => response.json())
            .then(data => {
                return data[0][0][0];
            })
            .catch(error => {
                console.error('Erro na tradução:', error);
                return texto; // Retorna o texto original em caso de erro
            });
    }
});
// Função para aplicar a classe de animação quando a página é carregada
window.onload = function () {
    var imagem = document.getElementById('ImgPrincipal');
    imagem.classList.add('InfSobrePros img:hover');
}