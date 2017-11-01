var jbkrAlert = (function () {
    var criarModal = function () {
        var modal = $('<div class="modal modal-alerta"><div class="modal-dialog"><div class="modal-content"><div class="modal-body"></div></div></div></div>');
        modal.modal('show').on('hidden', function () {
            $('.modal-alerta').remove();
        });
    }

    var exibirAlerta = function (titulo, mensagem) {
        criarModal();
        var conteudo = $('<div class="alert alert-warning">' +
        '<b><i class="icon-warning-sign"></i> ' + titulo + '</b></br>' +
        '' + mensagem + '' +
        '<button type="button" class="btn btn-warning" data-dismiss="modal">Fechar</button></div>');
        $('.modal-alerta .modal-body').html(conteudo);

    };

    var exibirErro = function (titulo, mensagem) {

        criarModal();
        var conteudo = $('<div class="alert alert-danger">' +
        '<b><i class="icon-exclamation-sign"></i> ' + titulo + '</b></br>' +
        '' + mensagem + '' +
        '<button type="button" class="btn btn-danger" data-dismiss="modal">Fechar</button></div>');
        $('.modal-alerta .modal-body').html(conteudo);
    };

    var exibirSucesso = function (titulo, mensagem, recarregarEmSegundos) {
        debugger;
        criarModal();
        var conteudo = $('<div class="alert alert-success">' +
        '<b><i class="icon-info-sign"></i> ' + titulo + '</b></br>' +
        '<label>' + mensagem + '</label>');
        $('.modal-alerta .modal-body').html(conteudo);

        if (recarregarEmSegundos === true) {
            setTimeout(function () {
                location.reload();
            }, 2000);
        }
    };

    return {
        alerta: exibirAlerta,
        erro: exibirErro,
        sucesso: exibirSucesso
    };
})();
