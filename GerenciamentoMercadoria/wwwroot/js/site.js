$(document).ready(function () {

    $('.close-alert').click(function () {
        $('.alert').hide('hide');
    });

    $("#entrada").datepicker({
        monthNamesShort: ["Janeiro", "Fevereiro", "Março", "Abril", "Março", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
        changeMonth: true,
        changeYear: true,
        showButtonPanel: false,
        dateFormat: 'MM yy',
        language: "pt-BR",
        onChangeMonthYear: function (year, month) {
            var dateJs = "1/" + month + "/" + year;

            $.post('/Entrada', {
                seachData: dateJs

            }, function (resposta) {
                $("#listaEntrada").html(resposta);
            })

        }
    });

    $("#entradaSaida").datepicker({
        monthNamesShort: ["Janeiro", "Fevereiro", "Março", "Abril", "Março", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
        changeMonth: true,
        changeYear: true,
        showButtonPanel: false,
        dateFormat: 'MM yy',
        language: "pt-BR",
        onChangeMonthYear: function (year, month) {
            var dateJs = "1/" + month + "/" + year;

            $.post('/Home', {
                seachData: dateJs

            }, function (resposta) {
                $("#listaHome").html(resposta);
            })
        }
    });

    $("#saida").datepicker({
        monthNamesShort: ["Janeiro", "Fevereiro", "Março", "Abril", "Março", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
        changeMonth: true,
        changeYear: true,
        showButtonPanel: false,
        dateFormat: 'MM yy',
        language: "pt-BR",
        onChangeMonthYear: function (year, month) {
            var dateJs = "1/" + month + "/" + year;

            $.post('/Saida', {
                seachData: dateJs

            }, function (resposta) {
                $("#listaSaida").html(resposta);
            })
        }
    });

    fetch('/EntradaSaida')
        .then((response) => response.json())
        .then((data) => {
            let value = (tipo, mes) => { return data.filter(p => p.tipo === tipo && (new Date(p.dataHora).getMonth() + 1) === mes).reduce((acc, p) => { return acc + p.quantidade }, 0) }
            
            var data = {
                labels: ["Janeiro", "Fevereiro", "Março", "Abril", "Março", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
                datasets: [
                    {
                        label: "Entrada",
                        backgroundColor: "green",
                        data: [
                            value("Entrada", 1),
                            value("Entrada", 2),
                            value("Entrada", 3),
                            value("Entrada", 4),
                            value("Entrada", 5),
                            value("Entrada", 6),
                            value("Entrada", 7),
                            value("Entrada", 8),
                            value("Entrada", 9),
                            value("Entrada", 10),
                            value("Entrada", 11),
                            value("Entrada", 12),
                        ]
                    },
                    {
                        label: "Saida",
                        backgroundColor: "red",
                        data: [
                            value("Saida", 1),
                            value("Saida", 2),
                            value("Saida", 3),
                            value("Saida", 4),
                            value("Saida", 5),
                            value("Saida", 6),
                            value("Saida", 7),
                            value("Saida", 8),
                            value("Saida", 9),
                            value("Saida", 10),
                            value("Saida", 11),
                            value("Saida", 12),
                        ]
                    }
                ]
            };

            new Chart("myChart", {
                type: "bar",
                data: data,
                options: {
                    legend: { display: false },
                    title: {
                        display: true,
                        text: "Grafico Mensal"
                    },
                    barValueSpacing: 20,
                    scales: {
                        yAxes: [{
                            ticks: {
                                min: 0,
                            }
                        }]
                    }
                }
            });

        });
});






