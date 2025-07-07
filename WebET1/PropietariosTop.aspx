<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PropietariosTop.aspx.cs" Inherits="WebET1.PropietariosTop" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KPI: Top 5 Propietarios</title>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Top 5 Propietarios con Más Propiedades</h2>
            <canvas id="propietariosChart" width="400" height="200"></canvas>
        </div>

        <script>
            $(document).ready(function () {
                $.ajax({
                    type: "POST",
                    url: "PropietariosTop.aspx/ObtenerTopPropietarios",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var nombres = [];
                        var totales = [];

                        $.each(response.d, function (index, item) {
                            nombres.push(item.nombre);
                            totales.push(item.total);
                        });

                        var ctx = document.getElementById('propietariosChart').getContext('2d');
                        var myChart = new Chart(ctx, {
                            type: 'bar',
                            data: {
                                labels: nombres,
                                datasets: [{
                                    label: 'Total de Propiedades',
                                    data: totales,
                                    backgroundColor: 'rgba(54, 162, 235, 0.5)',
                                    borderColor: 'rgba(54, 162, 235, 1)',
                                    borderWidth: 1
                                }]
                            },
                            options: {
                                responsive: true,
                                scales: {
                                    y: {
                                        beginAtZero: true
                                    }
                                }
                            }
                        });
                    },
                    error: function (xhr, status, error) {
                        alert('Error al cargar datos: ' + error);
                    }
                });
            });
        </script>
    </form>
</body>
</html>
