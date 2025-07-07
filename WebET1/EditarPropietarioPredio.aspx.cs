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
            CargarPropietarios();
            CargarPredios();

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarDatos(id);
                }
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

        private void CargarDatos(int id)
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(conexion))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_buscar_propietario_predio", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_prp_id", id);

                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ddlPropietario.SelectedValue = dr["pro_id"].ToString();
                            ddlPredio.SelectedValue = dr["pre_id"].ToString();
                            txtAlicuota.Text = dr["prp_alicuota"].ToString();
                            txtAniosPosesion.Text = dr["prp_anios_posesion"].ToString();
                            txtObservacion.Text = dr["prp_observacion"].ToString();
                            ddlTieneEscritura.SelectedValue = dr["prp_tiene_escritura"].ToString();
                            txtFechaInscripcion.Text = dr["prp_fecha_inscripcion"] == DBNull.Value ? "" : Convert.ToDateTime(dr["prp_fecha_inscripcion"]).ToString("yyyy-MM-dd");
                            txtFechaRegistro.Text = dr["prp_fecha_registro"] == DBNull.Value ? "" : Convert.ToDateTime(dr["prp_fecha_registro"]).ToString("yyyy-MM-dd");
                            txtAreaEscritura.Text = dr["prp_area_escritura"].ToString();
                        }
                    }
                    con.Close();
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
                return;

            int id = int.Parse(Request.QueryString["id"]);

            try
            {
                string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

                using (NpgsqlConnection con = new NpgsqlConnection(conexion))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_actualizar_propietario_predio", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_prp_id", id);
                        cmd.Parameters.AddWithValue("p_pro_id", int.Parse(ddlPropietario.SelectedValue));
                        cmd.Parameters.AddWithValue("p_pre_id", long.Parse(ddlPredio.SelectedValue));
                        cmd.Parameters.AddWithValue("p_prp_alicuota", decimal.Parse(txtAlicuota.Text));
                        cmd.Parameters.AddWithValue("p_prp_anios_posesion", int.Parse(txtAniosPosesion.Text));
                        cmd.Parameters.AddWithValue("p_prp_observacion", txtObservacion.Text);
                        cmd.Parameters.AddWithValue("p_prp_tiene_escritura", short.Parse(ddlTieneEscritura.SelectedValue));
                        cmd.Parameters.AddWithValue("p_prp_fecha_inscripcion", DateTime.Parse(txtFechaInscripcion.Text));
                        cmd.Parameters.AddWithValue("p_prp_fecha_registro", DateTime.Parse(txtFechaRegistro.Text));
                        cmd.Parameters.AddWithValue("p_prp_area_escritura", decimal.Parse(txtAreaEscritura.Text));

                        con.Open();

                        // Debug temporal para comprobar que los datos llegan
                        Response.Write($"<script>alert('ID: {id}');</script>");

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                Response.Write("<script>alert('Registro actualizado correctamente.'); window.location='PropietariosPredios.aspx';</script>");
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
