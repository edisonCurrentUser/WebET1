using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
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

                // Alerta de éxito y redirección
                string successScript = @"
                    Swal.fire({
                        title: '¡Predio agregado!',
                        text: 'El predio se ha guardado correctamente.',
                        icon: 'success',
                        confirmButtonText: 'Ver listado'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            window.location = 'Predios.aspx';
                        }
                    });
                ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "swalSuccess", successScript, true);
            }
            catch (Exception ex)
            {
                // Alerta de error
                string errMsg = ex.Message.Replace("'", "\\'");
                string errorScript = $@"
                    Swal.fire({{
                        title: 'Error al guardar',
                        text: '{errMsg}',
                        icon: 'error',
                        confirmButtonText: 'OK'
                    }});
                ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "swalError", errorScript, true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Predios.aspx");
        }
    }
}
