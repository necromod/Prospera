function ativarEstiloAoRolar() {
    const li = document.getElementById("#EstiloScrooll"); // Obtém o elemento li
    const scrollY = window.scrollY; // Obtém a posição vertical da janela

    if (scrollY > 100) { // Defina a posição em que você deseja ativar o estilo
        li.classList.add(".EstiloScrooll"); // Adiciona a classe CSS para ativar o estilo
    } else {
        li.classList.remove(".EstiloScrooll"); // Remove a classe CSS para desativar o estilo
    }
}

// Adiciona um ouvinte de evento de rolagem (scroll) à janela
window.addEventListener("scroll", ativarEstiloAoRolar);