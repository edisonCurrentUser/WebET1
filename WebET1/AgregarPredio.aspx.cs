using System;
using System.Configuration;
using System.Data;
using Npgsql;

namespace WebET1
{
    public partial class AgregarPredio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarManzanas();
            }
        }

        private void CargarManzanas()
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(conexion))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT man_id FROM catastro.cat_manzana ORDER BY man_id", con))
                {
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);

                        ddlManzana.DataSource = dt;
                        ddlManzana.DataTextField = "man_id";
                        ddlManzana.DataValueField = "man_id";
                        ddlManzana.DataBind();
                        ddlManzana.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --", ""));
                    }
                    con.Close();
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

                using (NpgsqlConnection con = new NpgsqlConnection(conexion))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_insertar_predio", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_pre_codigo_catastral", txtCodigoCatastral.Text);
                        cmd.Parameters.AddWithValue("p_pre_codigo_anterior", txtCodigoAnterior.Text);
                        cmd.Parameters.AddWithValue("p_pre_numero", txtNumero.Text);
                        cmd.Parameters.AddWithValue("p_pre_nombre_predio", txtNombrePredio.Text);
                        cmd.Parameters.AddWithValue("p_pre_area_total_ter", decimal.Parse(txtAreaTerreno.Text));
                        cmd.Parameters.AddWithValue("p_pre_area_total_const", string.IsNullOrEmpty(txtAreaConstruccion.Text) ? (object)DBNull.Value : decimal.Parse(txtAreaConstruccion.Text));
                        cmd.Parameters.AddWithValue("p_pre_estado", string.IsNullOrEmpty(txtEstado.Text) ? (object)DBNull.Value : int.Parse(txtEstado.Text));
                        cmd.Parameters.AddWithValue("p_pre_dominio", string.IsNullOrEmpty(txtDominio.Text) ? (object)DBNull.Value : int.Parse(txtDominio.Text));
                        cmd.Parameters.AddWithValue("p_pre_direccion_principal", txtDireccionPrincipal.Text);
                        cmd.Parameters.AddWithValue("p_pre_num_habitantes", string.IsNullOrEmpty(txtNumHabitantes.Text) ? (object)DBNull.Value : int.Parse(txtNumHabitantes.Text));
                        cmd.Parameters.AddWithValue("p_pre_propietario_anterior", txtPropietarioAnterior.Text);
                        cmd.Parameters.AddWithValue("p_man_id", int.Parse(ddlManzana.SelectedValue));

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                Response.Redirect("Predios.aspx");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al guardar: {ex.Message}');</script>");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Predios.aspx");
        }
    }
}
