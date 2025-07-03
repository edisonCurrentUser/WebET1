using System;
using System.Web.UI;
using Npgsql;
using System.Configuration;

namespace WebET1
{
    public partial class AgregarPredio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si es necesario, puedes cargar algo en la página
        }

        // Método para guardar el predio en la base de datos
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_insertar_predio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Agregar los parámetros al procedimiento almacenado
                    cmd.Parameters.AddWithValue("pre_codigo_catastral", txtCodigoCatastral.Text);
                    cmd.Parameters.AddWithValue("pre_fecha_ingreso", DateTime.Now);
                    cmd.Parameters.AddWithValue("pre_codigo_anterior", txtCodigoAnterior.Text);
                    cmd.Parameters.AddWithValue("pre_numero", txtNumero.Text);
                    cmd.Parameters.AddWithValue("pre_nombre_predio", txtNombrePredio.Text);
                    cmd.Parameters.AddWithValue("pre_area_total_ter", Convert.ToDecimal(txtAreaTerreno.Text));
                    cmd.Parameters.AddWithValue("pre_area_total_const", Convert.ToDecimal(txtAreaConstruccion.Text));
                    cmd.Parameters.AddWithValue("pre_fondo_relativo", Convert.ToDecimal(txtFondoRelativo.Text));
                    cmd.Parameters.AddWithValue("pre_frente_fondo", Convert.ToDecimal(txtFrenteFondo.Text));
                    cmd.Parameters.AddWithValue("pre_observaciones", txtObservaciones.Text);
                    // Agregar los demás parámetros de forma similar...

                    // Ejecutar el procedimiento
                    cmd.ExecuteNonQuery();
                }
            }

            // Después de guardar, redirige a la página de listado o muestra un mensaje de éxito
            Response.Redirect("Predios.aspx");
        }
    }
}
