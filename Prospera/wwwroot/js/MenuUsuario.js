// ===================================
// MENU TOGGLE
// ===================================
const sidebar = document.getElementById('sidebar');
const sidebarToggle = document.getElementById('sidebarToggle');
const mobileMenuToggle = document.getElementById('mobileMenuToggle');

function toggleSidebar() {
    sidebar.classList.toggle('open');
}

if (sidebarToggle) {
    sidebarToggle.addEventListener('click', toggleSidebar);
}

if (mobileMenuToggle) {
    mobileMenuToggle.addEventListener('click', toggleSidebar);
}

// Fechar sidebar ao clicar fora (apenas mobile)
document.addEventListener('click', (e) => {
    if (window.innerWidth <= 1024) {
        if (!sidebar.contains(e.target) && !mobileMenuToggle.contains(e.target)) {
            sidebar.classList.remove('open');
        }
    }
});

// ===================================
// COTAÇÕES DE MOEDAS
// ===================================
async function carregarCotacoes() {
    try {
        const response = await fetch('https://economia.awesomeapi.com.br/json/last/USD-BRL,EUR-BRL,CNY-BRL');
        const data = await response.json();
        
        // Atualizar valores na página
        const dolarElement = document.getElementById('txtDolar');
        const euroElement = document.getElementById('txtEuro');
        const yuanElement = document.getElementById('txtYuan');
        const updateTimeElement = document.getElementById('updateTime');
        
        if (dolarElement && data.USDBRL) {
            dolarElement.textContent = `R$ ${parseFloat(data.USDBRL.bid).toFixed(2)}`;
        }
        
        if (euroElement && data.EURBRL) {
            euroElement.textContent = `R$ ${parseFloat(data.EURBRL.bid).toFixed(2)}`;
        }
        
        if (yuanElement && data.CNYBRL) {
            yuanElement.textContent = `R$ ${parseFloat(data.CNYBRL.bid).toFixed(2)}`;
        }
        
        if (updateTimeElement) {
            const now = new Date();
            updateTimeElement.textContent = `Atualizado às ${now.getHours().toString().padStart(2, '0')}:${now.getMinutes().toString().padStart(2, '0')}`;
        }
    } catch (error) {
        console.error('Erro ao carregar cotações:', error);
    }
}

// Carregar cotações ao iniciar
carregarCotacoes();

// Atualizar cotações a cada 5 minutos
setInterval(carregarCotacoes, 5 * 60 * 1000);

// ===================================
// NOTÍCIAS
// ===================================
let noticias = [];
let noticiaAtual = 0;

const newsContainer = document.getElementById('newsContainer');
const newsCounter = document.getElementById('newsCounter');
const prevNewsBtn = document.getElementById('prevNews');
const nextNewsBtn = document.getElementById('nextNews');
const reloadNewsBtn = document.getElementById('reloadNews');

async function carregarNoticias() {
    if (!newsContainer) return;
    
    try {
        // Mostrar loading
        newsContainer.innerHTML = `
            <div class="news-loading">
                <div class="spinner"></div>
                <p>Carregando notícias...</p>
            </div>
        `;
        
        // API de notícias (você pode usar qualquer API de notícias aqui)
        // Por enquanto, vou usar dados mock
        const response = await fetch('https://servicodados.ibge.gov.br/api/v3/noticias/?qtd=10');
        const data = await response.json();
        
        noticias = data.items || [];
        
        if (noticias.length > 0) {
            noticiaAtual = 0;
            exibirNoticia();
        } else {
            newsContainer.innerHTML = `
                <div style="text-align: center; color: var(--text-secondary);">
                    <p>Nenhuma notícia disponível no momento.</p>
                </div>
            `;
        }
    } catch (error) {
        console.error('Erro ao carregar notícias:', error);
        newsContainer.innerHTML = `
            <div style="text-align: center; color: var(--danger-color);">
                <p>Erro ao carregar notícias. Tente novamente mais tarde.</p>
            </div>
        `;
    }
}

function exibirNoticia() {
    if (noticias.length === 0 || !newsContainer) return;
    
    const noticia = noticias[noticiaAtual];
    
    // Extrair imagem se disponível
    let imagemHtml = '';
    if (noticia.imagens) {
        const imagem = JSON.parse(noticia.imagens);
        if (imagem.image_intro) {
            imagemHtml = `<img src="https://agenciadenoticias.ibge.gov.br/${imagem.image_intro}" alt="${noticia.titulo}" style="width: 100%; height: 200px; object-fit: cover; border-radius: var(--radius-md); margin-bottom: 1rem;">`;
        }
    }
    
    newsContainer.innerHTML = `
        <div style="text-align: left;">
            ${imagemHtml}
            <h3 style="font-size: 1.125rem; font-weight: 600; color: var(--text-primary); margin-bottom: 0.75rem;">
                ${noticia.titulo}
            </h3>
            <p style="font-size: 0.9375rem; color: var(--text-secondary); line-height: 1.6; margin-bottom: 1rem;">
                ${noticia.introducao}
            </p>
            <a href="${noticia.link}" target="_blank" style="color: var(--primary-color); font-weight: 500; text-decoration: none; font-size: 0.875rem;">
                Ler mais ?
            </a>
            <p style="font-size: 0.8125rem; color: var(--text-tertiary); margin-top: 1rem;">
                ${formatarData(noticia.data_publicacao)}
            </p>
        </div>
    `;
    
    // Atualizar contador
    if (newsCounter) {
        newsCounter.textContent = `${noticiaAtual + 1} / ${noticias.length}`;
    }
    
    // Atualizar estado dos botões
    if (prevNewsBtn) {
        prevNewsBtn.disabled = noticiaAtual === 0;
    }
    
    if (nextNewsBtn) {
        nextNewsBtn.disabled = noticiaAtual === noticias.length - 1;
    }
}

function proximaNoticia() {
    if (noticiaAtual < noticias.length - 1) {
        noticiaAtual++;
        exibirNoticia();
    }
}

function noticiaAnterior() {
    if (noticiaAtual > 0) {
        noticiaAtual--;
        exibirNoticia();
    }
}

function formatarData(dataString) {
    const data = new Date(dataString);
    const agora = new Date();
    const diffMs = agora - data;
    const diffDias = Math.floor(diffMs / (1000 * 60 * 60 * 24));
    
    if (diffDias === 0) {
        return 'Hoje';
    } else if (diffDias === 1) {
        return 'Ontem';
    } else if (diffDias < 7) {
        return `Há ${diffDias} dias`;
    } else {
        return data.toLocaleDateString('pt-BR');
    }
}

// Event listeners para notícias
if (prevNewsBtn) {
    prevNewsBtn.addEventListener('click', noticiaAnterior);
}

if (nextNewsBtn) {
    nextNewsBtn.addEventListener('click', proximaNoticia);
}

if (reloadNewsBtn) {
    reloadNewsBtn.addEventListener('click', () => {
        reloadNewsBtn.style.animation = 'spin 1s ease-in-out';
        setTimeout(() => {
            reloadNewsBtn.style.animation = '';
        }, 1000);
        carregarNoticias();
    });
}

// Carregar notícias ao iniciar
carregarNoticias();

// ===================================
// ANIMAÇÕES E EFEITOS
// ===================================

// Adicionar animação de entrada aos cards
const observerOptions = {
    threshold: 0.1,
    rootMargin: '0px 0px -50px 0px'
};

const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.style.opacity = '0';
            entry.target.style.transform = 'translateY(20px)';
            
            setTimeout(() => {
                entry.target.style.transition = 'all 0.5s ease-out';
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
            }, 100);
            
            observer.unobserve(entry.target);
        }
    });
}, observerOptions);

// Observar todos os cards
document.querySelectorAll('.stat-card, .dashboard-card').forEach(card => {
    observer.observe(card);
});

// ===================================
// UTILITÁRIOS
// ===================================

// Formatar valores monetários
function formatarMoeda(valor) {
    return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL'
    }).format(valor);
}

// Adicionar classe aos links ativos
function marcarLinkAtivo() {
    const currentPath = window.location.pathname;
    const navItems = document.querySelectorAll('.nav-item');
    
    navItems.forEach(item => {
        if (item.getAttribute('href') === currentPath) {
            item.classList.add('active');
        } else {
            item.classList.remove('active');
        }
    });
}

// Executar ao carregar a página
document.addEventListener('DOMContentLoaded', () => {
    marcarLinkAtivo();
});

// ===================================
// TRATAMENTO DE ERROS GLOBAL
// ===================================
window.addEventListener('error', (e) => {
    console.error('Erro capturado:', e.error);
});

window.addEventListener('unhandledrejection', (e) => {
    console.error('Promise rejeitada:', e.reason);
});

console.log('? MenuUsuario.js carregado com sucesso!');
