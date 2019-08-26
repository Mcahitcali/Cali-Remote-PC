$(document).ready(function () {
    NewActionModal();
    NewClientModel();

    var clientData;
    $('[data-client]').click(function () {
        clientData = $(this).data('client');
        console.log(clientData)
        $.ajax({
            type: "POST",
            data: clientData,
            url: '/Default/GetCurrentClient',
            success: function (data) {
                //console.log(data);
                $('#current-client-area').fadeOut(300, function () {
                    $('#current-client-area').html(data).fadeIn();
                });
            },
            error: function (err) {
                console.log(err);
            }
        });

    });
    
});

$("#actions").click(function (event) {
    $("input[name='ActionValue']").val($(event.target).text());
});

function NewClientModel() {
    var url;
    var placeholderElement = $('#client-modal-placeholder');
    $('div[data-toggle="ajax-client-modal"]').click(function (event) {
        url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });
    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        console.log(url);
        if ($('#StateBox').is(':checked')) {
            $('#HiddenState').prop('disabled', true);
        }
        var form = $(this).parents('.modal').find('form');
        var seriForm = form.serializeArray();

        $.post(url, seriForm).done(function (data) {
            placeholderElement.find('.modal').modal('hide');
            $('.left-sidebar').html(data);
        }).fail(
            function (error) {
                placeholderElement.find('.modal').modal('hide');
                alert(error.responseText);
            }
        );
    });
}

function NewActionModal() {
    var url;
    var placeholderElement = $('#action-modal-placeholder');
    $('div[data-toggle="ajax-action-modal"]').click(function (event) {
        url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });
    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        var form = $(this).parents('.modal').find('form');
        var seriForm = form.serializeArray();
        $.post(url, seriForm).done(function (data) {
            $('.right-sidebar').html(data);
            placeholderElement.find('.modal').modal('hide');
        }).fail(
            function (error) {
                placeholderElement.find('.modal').modal('hide');
                alert("Error:" +error.responseText);
            }
        );
    });
}

