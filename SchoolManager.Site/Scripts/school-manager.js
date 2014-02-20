
// Inicia o loader
function startLoader() {
    $('.loader').fadeTo(100, 0.4);
}

// Para o loading

function stopLoader() {
    $('.loader').fadeOut(100);
}
    
// Ao clicar no botão Salvar, verifica se houve algum erro no ModelState e exibe o retorno com o erro
function bindForm(dialogDiv, dialogContent) {
    $('form', dialogContent).submit(function () {
        startLoader();
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                var isValid = $(result).find(".validation-summary-errors").length == 0;
                if (isValid) {
                    $(dialogDiv).modal('hide');
                    location.reload();
                } else {
                    $(dialogContent).html(result);
                    // Cria novamente o evento submit do form
                    bindForm(dialogDiv, dialogContent);
                }
                stopLoader();
            }
        });

        return false;
    });
}

// Ao clicar no botão Confirmar, verifica se houve algum erro
function bindConfirm(dialogDiv, dialogContent) {
    $('form', dialogContent).submit(function () {
        startLoader();
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                console.log(result.success);
                if (result.success) {
                    $(dialogDiv).modal('hide');
                    location.reload();
                } else {
                    $('#message-error').html(result.message);
                    $('#message-error').show();
                    // Cria novamente o evento submit do form
                    bindForm(dialogDiv, dialogContent);
                }
                stopLoader();
            }
        });

        return false;
    });
}

// Permite que digite apenas números
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}