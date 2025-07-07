using System;
using System.Configuration;
using System.Data;
using Npgsql;

namespace WebET1
{
    public partial class EditarPredio : System.Web.UI.Page
    {
        int predioId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    predioId = int.Parse(Request.QueryString["id"]);
                    CargarManzanas();
                    CargarDatos(predioId);
                }
                else
                {
                    Response.Redirect("Predios.aspx");
                }
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

        private void CargarDatos(int id)
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(conexion))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM catastro.cat_predio WHERE pre_id = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtCodigoCatastral.Text = dr["pre_codigo_catastral"].ToString();
                            txtCodigoAnterior.Text = dr["pre_codigo_anterior"].ToString();
                            txtNumero.Text = dr["pre_numero"].ToString();
                            txtNombrePredio.Text = dr["pre_nombre_predio"].ToString();
                            txtAreaTerreno.Text = dr["pre_area_total_ter"].ToString();
                            txtAreaConstruccion.Text = dr["pre_area_total_const"].ToString();
                            txtEstado.Text = dr["pre_estado"].ToString();
                            txtDominio.Text = dr["pre_dominio"].ToString();
                            txtDireccionPrincipal.Text = dr["pre_direccion_principal"].ToString();
                            txtNumHabitantes.Text = dr["pre_num_habitantes"].ToString();
                            txtPropietarioAnterior.Text = dr["pre_propietario_anterior"].ToString();

                            if (ddlManzana.Items.FindByValue(dr["man_id"].ToString()) != null)
                            {
                                ddlManzana.SelectedValue = dr["man_id"].ToString();
                            }
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
                int id = int.Parse(Request.QueryString["id"]);
                string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

                using (NpgsqlConnection con = new NpgsqlConnection(conexion))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_actualizar_predio", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_pre_id", id);
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
                Response.Write($"<script>alert('Error al actualizar: {ex.Message.Replace("'", "")}');</script>");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Predios.aspx");
        }
    }
}
