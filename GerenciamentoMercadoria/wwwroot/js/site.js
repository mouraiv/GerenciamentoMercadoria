$(document).ready(function () {

    $('.close-alert').click(function () {
        $('.alert').hide('hide');
    });

    $('#produto').on('input', function() {
        $.ajax({
            url: "/Entrada",
            type: "POST",
            data: { produto: $('#produto').val() },
            dataType: "html"
        }).done(function (resposta) {
            $("#listaEntrada").html(resposta);
        });
    });

    $("#datepicker").datepicker({
        monthNamesShort: ["Janeiro", "Fevereiro", "Março", "Abril", "Março", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
        changeMonth: true,
        changeYear: true,
        showButtonPanel: false,
        dateFormat: 'MM yy',
        language: "pt-BR",
        onChangeMonthYear: function (year, month) {
            $.ajax({
                url: "/Entrada",
                type: "POST",
                data: { seachData: '1/' + month + '/' + year },
                dataType: "html"
            }).done(function (resposta) {
                $("#listaEntrada").html(resposta);
            });
        }
    }).val('');
});






