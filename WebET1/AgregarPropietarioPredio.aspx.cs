using System;
using System.Configuration;
using System.Data;
using Npgsql;

namespace WebET1
{
    public partial class AgregarPropietarioPredio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPropietarios();
                CargarPredios();
            }
        }

        private void CargarPropietarios()
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(conexion))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("gestion.sp_listar_propietarios_combo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        ddlPropietario.DataSource = dt;
                        ddlPropietario.DataTextField = "nombre_completo";
                        ddlPropietario.DataValueField = "pro_id";
                        ddlPropietario.DataBind();
                    }
                }
            }
        }

        private void CargarPredios()
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(conexion))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_listar_predios_combo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        ddlPredio.DataSource = dt;
                        ddlPredio.DataTextField = "descripcion_predio";
                        ddlPredio.DataValueField = "pre_id";
                        ddlPredio.DataBind();
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(conexion))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_agregar_propietario_predio", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros
                        cmd.Parameters.AddWithValue("p_pro_id", Convert.ToInt32(ddlPropietario.SelectedValue));
                        cmd.Parameters.AddWithValue("p_pre_id", Convert.ToInt64(ddlPredio.SelectedValue));
                        cmd.Parameters.AddWithValue("p_prp_alicuota", Convert.ToDecimal(txtAlicuota.Text));
                        cmd.Parameters.AddWithValue("p_prp_anios_posesion", Convert.ToInt32(txtAniosPosesion.Text));
                        cmd.Parameters.AddWithValue("p_prp_observacion", txtObservacion.Text);
                        cmd.Parameters.AddWithValue("p_prp_tiene_escritura", Convert.ToInt16(txtTieneEscritura.Text));
                        cmd.Parameters.AddWithValue("p_prp_fecha_inscripcion", DateTime.Parse(txtFechaInscripcion.Text));
                        cmd.Parameters.AddWithValue("p_prp_fecha_registro", DateTime.Parse(txtFechaRegistro.Text));
                        cmd.Parameters.AddWithValue("p_prp_area_escritura", Convert.ToDecimal(txtAreaEscritura.Text));

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        // Redirigir al listado después de guardar
                        Response.Redirect("PropietariosPredios.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al guardar: {ex.Message}');</script>");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PropietariosPredios.aspx");
        }
    }
}
