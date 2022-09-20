$('.close-alert').click(function () {
    $('.alert').hide('hide');
});

$("#datepicker").datepicker({
    changeMonth: true,
    changeYear: true,
    showButtonPanel: false,
    dateFormat: 'MM yy',
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




