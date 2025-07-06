using System;
using System.Configuration;
using System.Data;
using Npgsql;

namespace WebET1
{
    public partial class EditarPropietarioPredio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["prp_id"] != null)
                {
                    int prp_id = Convert.ToInt32(Request.QueryString["prp_id"]);
                    CargarDatos(prp_id);
                }
                else
                {
                    Response.Redirect("PropietariosPredios.aspx");
                }
            }
        }

        private void CargarDatos(int id)
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(conexion))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_obtener_propietario_predio", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_prp_id", id);

                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtAlicuota.Text = dr["prp_alicuota"] != DBNull.Value ? dr["prp_alicuota"].ToString() : "";
                            txtAniosPosesion.Text = dr["prp_anios_posesion"] != DBNull.Value ? dr["prp_anios_posesion"].ToString() : "";
                            txtObservacion.Text = dr["prp_observacion"] != DBNull.Value ? dr["prp_observacion"].ToString() : "";
                            txtTieneEscritura.Text = dr["prp_tiene_escritura"] != DBNull.Value ? dr["prp_tiene_escritura"].ToString() : "";
                            txtFechaInscripcion.Text = dr["prp_fecha_inscripcion"] != DBNull.Value ? Convert.ToDateTime(dr["prp_fecha_inscripcion"]).ToString("yyyy-MM-dd") : "";
                            txtFechaRegistro.Text = dr["prp_fecha_registro"] != DBNull.Value ? Convert.ToDateTime(dr["prp_fecha_registro"]).ToString("yyyy-MM-dd") : "";
                            txtAreaEscritura.Text = dr["prp_area_escritura"] != DBNull.Value ? dr["prp_area_escritura"].ToString() : "";
                        }
                    }
                    con.Close();
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int prp_id = Convert.ToInt32(Request.QueryString["prp_id"]);
                string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

                using (NpgsqlConnection con = new NpgsqlConnection(conexion))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_actualizar_propietario_predio", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_prp_id", prp_id);
                        cmd.Parameters.AddWithValue("p_prp_alicuota", decimal.Parse(txtAlicuota.Text));
                        cmd.Parameters.AddWithValue("p_prp_anios_posesion", int.Parse(txtAniosPosesion.Text));
                        cmd.Parameters.AddWithValue("p_prp_observacion", txtObservacion.Text);
                        cmd.Parameters.AddWithValue("p_prp_tiene_escritura", short.Parse(txtTieneEscritura.Text));

                        cmd.Parameters.AddWithValue("p_prp_fecha_inscripcion",
                            string.IsNullOrWhiteSpace(txtFechaInscripcion.Text) ? (object)DBNull.Value : DateTime.Parse(txtFechaInscripcion.Text));

                        cmd.Parameters.AddWithValue("p_prp_fecha_registro",
                            string.IsNullOrWhiteSpace(txtFechaRegistro.Text) ? (object)DBNull.Value : DateTime.Parse(txtFechaRegistro.Text));

                        cmd.Parameters.AddWithValue("p_prp_area_escritura", decimal.Parse(txtAreaEscritura.Text));

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                Response.Redirect("PropietariosPredios.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al actualizar: {ex.Message}');</script>");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PropietariosPredios.aspx");
        }
    }
}
