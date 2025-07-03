using System;
using System.Web.UI;
using Npgsql;
using System.Configuration;
using System.Data;

namespace WebET1
{
    public partial class EditarPredio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                // Obtener el ID del predio desde la URL
                int preId = Convert.ToInt32(Request.QueryString["pre_id"]);

                // Cargar los datos del predio
                CargarPredio(preId);
            }
        }

        private void CargarPredio(int preId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM catastro.cat_predio WHERE pre_id = @preId", conn))
                {
                    cmd.Parameters.AddWithValue("@preId", preId);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Cargar los datos del predio en los controles
                            txtCodigoCatastral.Text = reader["pre_codigo_catastral"].ToString();
                            txtCodigoAnterior.Text = reader["pre_codigo_anterior"].ToString();
                            txtNumero.Text = reader["pre_numero"].ToString();
                            txtNombrePredio.Text = reader["pre_nombre_predio"].ToString();
                            txtAreaTerreno.Text = reader["pre_area_total_ter"].ToString();
                            txtAreaConstruccion.Text = reader["pre_area_total_const"].ToString();
                            txtFondoRelativo.Text = reader["pre_fondo_relativo"].ToString();
                            txtFrenteFondo.Text = reader["pre_frente_fondo"].ToString();
                            txtObservaciones.Text = reader["pre_observaciones"].ToString();
                            txtDimTomadoPlanos.Text = reader["pre_dim_tomado_planos"].ToString();
                            txtOtraFuenteInfo.Text = reader["pre_otra_fuente_info"].ToString();
                            txtNumNuevoBloque.Text = reader["pre_num_nuevo_bloque"].ToString();
                            txtNumAmpliBloque.Text = reader["pre_num_ampli_bloque"].ToString();
                            txtTipo.Text = reader["pre_tipo"].ToString();
                            txtEstado.Text = reader["pre_estado"].ToString();
                            txtDominio.Text = reader["pre_dominio"].ToString();
                            txtDireccionPrincipal.Text = reader["pre_direccion_principal"].ToString();
                            txtNumHabitantes.Text = reader["pre_num_habitantes"].ToString();
                            txtPropietarioAnterior.Text = reader["pre_propietario_anterior"].ToString();
                        }
                    }
                }
            }
        }

        // Método para guardar los cambios
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener el ID del predio desde la URL
            int preId = Convert.ToInt32(Request.QueryString["pre_id"]);

            // Variables para almacenar valores convertidos
            decimal fondoRelativo = 0, frenteFondo = 0, areaTerreno = 0, areaConstruccion = 0;
            int ampliBloque = 0, tipo = 0, estado = 0, dominio = 0, habitantes = 0;

            // Validar y convertir los campos numéricos
            Decimal.TryParse(txtFondoRelativo.Text, out fondoRelativo);
            Decimal.TryParse(txtFrenteFondo.Text, out frenteFondo);
            Decimal.TryParse(txtAreaTerreno.Text, out areaTerreno);
            Decimal.TryParse(txtAreaConstruccion.Text, out areaConstruccion);
            Int32.TryParse(txtNumAmpliBloque.Text, out ampliBloque);
            Int32.TryParse(txtTipo.Text, out tipo);
            Int32.TryParse(txtEstado.Text, out estado);
            Int32.TryParse(txtDominio.Text, out dominio);
            Int32.TryParse(txtNumHabitantes.Text, out habitantes);

            string connStr = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_actualizar_predio", conn))  // Usar el esquema catastro
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Asignar parámetros al procedimiento
                    cmd.Parameters.AddWithValue("@preId", preId);
                    cmd.Parameters.AddWithValue("@codigo", txtCodigoCatastral.Text);
                    cmd.Parameters.AddWithValue("@codigoAnterior", txtCodigoAnterior.Text);
                    cmd.Parameters.AddWithValue("@numero", txtNumero.Text);
                    cmd.Parameters.AddWithValue("@nombre", txtNombrePredio.Text);
                    cmd.Parameters.AddWithValue("@areaTerreno", areaTerreno);
                    cmd.Parameters.AddWithValue("@areaConstruccion", areaConstruccion);
                    cmd.Parameters.AddWithValue("@fondoRelativo", fondoRelativo);
                    cmd.Parameters.AddWithValue("@frenteFondo", frenteFondo);
                    cmd.Parameters.AddWithValue("@observaciones", txtObservaciones.Text);
                    cmd.Parameters.AddWithValue("@dimPlanos", txtDimTomadoPlanos.Text);
                    cmd.Parameters.AddWithValue("@otraFuente", txtOtraFuenteInfo.Text);
                    cmd.Parameters.AddWithValue("@nuevoBloque", txtNumNuevoBloque.Text);
                    cmd.Parameters.AddWithValue("@ampliBloque", ampliBloque);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@dominio", dominio);
                    cmd.Parameters.AddWithValue("@direccion", txtDireccionPrincipal.Text);
                    cmd.Parameters.AddWithValue("@habitantes", habitantes);
                    cmd.Parameters.AddWithValue("@propietarioAnterior", txtPropietarioAnterior.Text);

                    // Ejecutar el procedimiento
                    cmd.ExecuteNonQuery();
                }
            }


            // Redirigir a la página de listado después de guardar los cambios
            Response.Redirect("Predios.aspx");
        }
    }
}
