$(document).ready(function () {

    //Exibir e formatar data/hora
    function atualizarHora() {
        var dateString = new Date().toLocaleString("pt-br", { timeZone: "America/Sao_Paulo" });
        var formattedString = dateString.replace(", ", " - ");
        $('#relogio').html("<strong>"+formattedString+"</strong>");
    }
    //atualizar hora em tempo real
    setInterval(atualizarHora, 1000)

    //Botão close alerta mensagem
    $('.close-alert').click(function () {
        $('.alert').hide('hide');
    });

    //request controller entrada datapiker dropdown 
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

    //request controller entrada e saida datapiker dropdown 
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

    //request controller saida datapiker dropdown 
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

    //request post dados do grafico entrada e saida de mercadoria
    fetch('/EntradaSaida')
        .then((response) => response.json())
        .then((data) => {

            //função que retorna quantidade de mercadoria por entrada/saida e mes de movimentação
            let value = (tipo, mes) => { return data.filter(p => p.tipo === tipo && (new Date(p.dataHora).getMonth() + 1) === mes).reduce((acc, p) => { return acc + p.quantidade }, 0) }

            //setup grafico de barras
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

            //style e controle do grafico de barras
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






