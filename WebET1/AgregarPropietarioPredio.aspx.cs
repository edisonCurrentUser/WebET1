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
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT pro_id, pro_nombre || ' ' || pro_apellido AS nombre_completo 
                                                               FROM gestion.ges_propietario 
                                                               ORDER BY pro_nombre", con))
                {
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);

                        ddlPropietario.DataSource = dt;
                        ddlPropietario.DataTextField = "nombre_completo";
                        ddlPropietario.DataValueField = "pro_id";
                        ddlPropietario.DataBind();
                        ddlPropietario.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --", ""));
                    }
                    con.Close();
                }
            }
        }

        private void CargarPredios()
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(conexion))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT pre_id, pre_codigo_catastral 
                                                               FROM catastro.cat_predio 
                                                               ORDER BY pre_codigo_catastral", con))
                {
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);

                        ddlPredio.DataSource = dt;
                        ddlPredio.DataTextField = "pre_codigo_catastral";
                        ddlPredio.DataValueField = "pre_id";
                        ddlPredio.DataBind();
                        ddlPredio.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --", ""));
                    }
                    con.Close();
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ddlPropietario.SelectedIndex == 0 || ddlPredio.SelectedIndex == 0)
            {
                Response.Write("<script>alert('Debe seleccionar un Propietario y un Predio.');</script>");
                return;
            }

            try
            {
                string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

                using (NpgsqlConnection con = new NpgsqlConnection(conexion))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_insertar_propietario_predio", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new NpgsqlParameter("p_pro_id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = int.Parse(ddlPropietario.SelectedValue) });
                        cmd.Parameters.Add(new NpgsqlParameter("p_pre_id", NpgsqlTypes.NpgsqlDbType.Bigint) { Value = long.Parse(ddlPredio.SelectedValue) });
                        cmd.Parameters.Add(new NpgsqlParameter("p_prp_alicuota", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = decimal.Parse(txtAlicuota.Text) });
                        cmd.Parameters.Add(new NpgsqlParameter("p_prp_anios_posesion", NpgsqlTypes.NpgsqlDbType.Integer) { Value = int.Parse(txtAniosPosesion.Text) });
                        cmd.Parameters.Add(new NpgsqlParameter("p_prp_observacion", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = txtObservacion.Text });
                        cmd.Parameters.Add(new NpgsqlParameter("p_prp_tiene_escritura", NpgsqlTypes.NpgsqlDbType.Smallint) { Value = short.Parse(ddlTieneEscritura.SelectedValue) });
                        cmd.Parameters.Add(new NpgsqlParameter("p_prp_fecha_inscripcion", NpgsqlTypes.NpgsqlDbType.Date) { Value = DateTime.Parse(txtFechaInscripcion.Text) });
                        cmd.Parameters.Add(new NpgsqlParameter("p_prp_fecha_registro", NpgsqlTypes.NpgsqlDbType.Date) { Value = DateTime.Parse(txtFechaRegistro.Text) });
                        cmd.Parameters.Add(new NpgsqlParameter("p_prp_area_escritura", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = decimal.Parse(txtAreaEscritura.Text) });

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                Response.Write("<script>alert('Registro guardado correctamente.'); window.location='PropietariosPredios.aspx';</script>");
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
