$(document).ready(function () {
    // Quando o formulário de login for enviado
    $('#loginForm').submit(function (e) {
        e.preventDefault(); // Evita o envio padrão do formulário

        // Obtém os dados do formulário
        var formData = $(this).serialize();

        // Realiza uma solicitação AJAX para o método de login
        $.ajax({
            type: 'POST',
            url: '/Usuario/Login', // Substitua pelo URL correto do seu controlador
            data: formData,
            success: function (response) {
                // Redireciona para a página de destino (por exemplo, "Index")
                window.location.href = response.redirectUrl;
            },
            error: function () {
                // Lida com erros de autenticação, exibe uma mensagem, etc.
                alert('Credenciais inválidas. Verifique seu email e senha.');
            }
        });
    });
});
