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
        onChangeMonthYear: function (year, month, inst) {
           var produtoJs = $('#produto').val();
           var dateJs = "1/"+month+"/"+year;

           $.post('/Entrada', {
               produto: produtoJs,
               seachData: dateJs

           }, function (resposta) {
               $("#listaEntrada").html(resposta);
           })
        }
    });

    $('#produto').on('input', function () {
        var produtoJs = $('#produto').val();
        var dateJs = $("#datepicker").val();

        $.post('/Entrada', {
            produto: produtoJs,
            seachData: dateJs

        }, function (resposta) {
            $("#listaEntrada").html(resposta);
        })
    });

});






