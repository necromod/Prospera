// ===================================
// MODAL SYSTEM - JAVASCRIPT
// ===================================

class ModalSystem {
    constructor(overlayId) {
        this.overlay = document.getElementById(overlayId);
        this.container = this.overlay?.querySelector('.modal-container');
        this.closeBtn = this.overlay?.querySelector('.modal-close-btn');
        
        if (!this.overlay) {
            console.error(`Modal overlay with id "${overlayId}" not found`);
            return;
        }
        
        this.init();
    }
    
    init() {
        // Fechar ao clicar no botão X
        if (this.closeBtn) {
            this.closeBtn.addEventListener('click', () => this.close());
        }
        
        // Fechar ao clicar fora do modal
        this.overlay.addEventListener('click', (e) => {
            if (e.target === this.overlay) {
                this.close();
            }
        });
        
        // Fechar com tecla ESC
        document.addEventListener('keydown', (e) => {
            if (e.key === 'Escape' && this.isOpen()) {
                this.close();
            }
        });
        
        // Prevenir scroll do body quando modal está aberto
        this.overlay.addEventListener('transitionend', () => {
            if (this.isOpen()) {
                document.body.style.overflow = 'hidden';
            } else {
                document.body.style.overflow = '';
            }
        });
    }
    
    open() {
        if (!this.overlay) return;
        this.overlay.classList.add('active');
        document.body.style.overflow = 'hidden';
    }
    
    close() {
        if (!this.overlay) return;
        this.overlay.classList.remove('active');
        document.body.style.overflow = '';
        
        // Redirecionar para o menu principal após fechar
        setTimeout(() => {
            window.location.href = '/Home/MenuUsuario';
        }, 300);
    }
    
    isOpen() {
        return this.overlay?.classList.contains('active');
    }
}

// ===================================
// FORM UTILITIES
// ===================================

function clearForm(formId) {
    const form = document.getElementById(formId);
    if (!form) return;
    
    // Limpar todos os inputs (exceto hidden e readonly)
    form.querySelectorAll('input:not([type="hidden"]):not([readonly])').forEach(input => {
        if (input.type === 'checkbox' || input.type === 'radio') {
            input.checked = false;
        } else {
            input.value = '';
        }
    });
    
    // Limpar selects
    form.querySelectorAll('select').forEach(select => {
        select.selectedIndex = 0;
    });
    
    // Limpar textareas
    form.querySelectorAll('textarea').forEach(textarea => {
        textarea.value = '';
    });
    
    // Remover mensagens de erro
    form.querySelectorAll('.text-danger').forEach(error => {
        error.textContent = '';
    });
}

function validateForm(formId) {
    const form = document.getElementById(formId);
    if (!form) return false;
    
    let isValid = true;
    
    // Validar campos obrigatórios
    form.querySelectorAll('[required]').forEach(field => {
        const errorSpan = field.nextElementSibling;
        
        if (!field.value.trim()) {
            isValid = false;
            if (errorSpan && errorSpan.classList.contains('text-danger')) {
                errorSpan.textContent = 'Este campo é obrigatório';
            }
            field.style.borderColor = '#EF4444';
        } else {
            if (errorSpan && errorSpan.classList.contains('text-danger')) {
                errorSpan.textContent = '';
            }
            field.style.borderColor = '#E2E8F0';
        }
    });
    
    return isValid;
}

function formatCurrency(input) {
    let value = input.value.replace(/\D/g, '');
    value = (parseFloat(value) / 100).toFixed(2);
    input.value = value;
}

function formatDate(dateString) {
    if (!dateString) return '';
    const date = new Date(dateString);
    return date.toLocaleDateString('pt-BR');
}

// ===================================
// SEARCH FUNCTIONALITY
// ===================================

function setupSearch(searchInputId, tableBodyId) {
    const searchInput = document.getElementById(searchInputId);
    const tableBody = document.getElementById(tableBodyId);
    
    if (!searchInput || !tableBody) return;
    
    searchInput.addEventListener('input', (e) => {
        const searchTerm = e.target.value.toLowerCase();
        const rows = tableBody.querySelectorAll('tr');
        
        rows.forEach(row => {
            const text = row.textContent.toLowerCase();
            if (text.includes(searchTerm)) {
                row.style.display = '';
            } else {
                row.style.display = 'none';
            }
        });
    });
}

// ===================================
// LOADING STATE
// ===================================

function setButtonLoading(button, loading = true) {
    if (!button) return;
    
    if (loading) {
        button.disabled = true;
        button.dataset.originalText = button.innerHTML;
        button.innerHTML = '<span class="loading-spinner"></span> Carregando...';
    } else {
        button.disabled = false;
        button.innerHTML = button.dataset.originalText || button.innerHTML;
    }
}

// ===================================
// NOTIFICATIONS
// ===================================

function showAlert(message, type = 'info', duration = 5000) {
    const alertDiv = document.createElement('div');
    alertDiv.className = `alert alert-${type}`;
    alertDiv.innerHTML = `
        <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            ${getAlertIcon(type)}
        </svg>
        <span>${message}</span>
    `;
    
    // Adicionar ao corpo do modal ou ao body
    const modalBody = document.querySelector('.modal-body');
    if (modalBody) {
        modalBody.insertBefore(alertDiv, modalBody.firstChild);
    } else {
        document.body.appendChild(alertDiv);
    }
    
    // Remover após duração
    setTimeout(() => {
        alertDiv.style.opacity = '0';
        setTimeout(() => alertDiv.remove(), 300);
    }, duration);
}

function getAlertIcon(type) {
    const icons = {
        success: '<path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"></path><polyline points="22 4 12 14.01 9 11.01"></polyline>',
        error: '<circle cx="12" cy="12" r="10"></circle><line x1="15" y1="9" x2="9" y2="15"></line><line x1="9" y1="9" x2="15" y2="15"></line>',
        warning: '<path d="M10.29 3.86L1.82 18a2 2 0 0 0 1.71 3h16.94a2 2 0 0 0 1.71-3L13.71 3.86a2 2 0 0 0-3.42 0z"></path><line x1="12" y1="9" x2="12" y2="13"></line><line x1="12" y1="17" x2="12.01" y2="17"></line>',
        info: '<circle cx="12" cy="12" r="10"></circle><line x1="12" y1="16" x2="12" y2="12"></line><line x1="12" y1="8" x2="12.01" y2="8"></line>'
    };
    return icons[type] || icons.info;
}

// ===================================
// CONFIRMATION DIALOG
// ===================================

function confirmAction(message, callback) {
    const confirmed = confirm(message);
    if (confirmed && typeof callback === 'function') {
        callback();
    }
    return confirmed;
}

// ===================================
// DATA TABLE UTILITIES
// ===================================

function sortTable(tableId, columnIndex) {
    const table = document.getElementById(tableId);
    if (!table) return;
    
    const tbody = table.querySelector('tbody');
    const rows = Array.from(tbody.querySelectorAll('tr'));
    
    rows.sort((a, b) => {
        const aText = a.cells[columnIndex].textContent.trim();
        const bText = b.cells[columnIndex].textContent.trim();
        
        // Tentar comparar como número
        const aNum = parseFloat(aText.replace(/[^\d.-]/g, ''));
        const bNum = parseFloat(bText.replace(/[^\d.-]/g, ''));
        
        if (!isNaN(aNum) && !isNaN(bNum)) {
            return aNum - bNum;
        }
        
        // Comparar como string
        return aText.localeCompare(bText);
    });
    
    // Re-anexar linhas ordenadas
    rows.forEach(row => tbody.appendChild(row));
}

// ===================================
// AUTO-OPEN MODAL
// ===================================

// Abrir automaticamente o modal quando a página carregar
document.addEventListener('DOMContentLoaded', () => {
    const modalOverlay = document.querySelector('.modal-overlay');
    if (modalOverlay) {
        setTimeout(() => {
            modalOverlay.classList.add('active');
            document.body.style.overflow = 'hidden';
        }, 100);
    }
});

// ===================================
// EXPORT
// ===================================

window.ModalSystem = ModalSystem;
window.clearForm = clearForm;
window.validateForm = validateForm;
window.formatCurrency = formatCurrency;
window.formatDate = formatDate;
window.setupSearch = setupSearch;
window.setButtonLoading = setButtonLoading;
window.showAlert = showAlert;
window.confirmAction = confirmAction;
window.sortTable = sortTable;

console.log('? ModalSystem.js carregado com sucesso!');
