$(document).ready(function () {

    $('.close-alert').click(function () {
        $('.alert').hide('hide');
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
    });
});






