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

    //recarregar notícias do dia
    document.getElementById('').addEventListener('click', function () {

    })

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