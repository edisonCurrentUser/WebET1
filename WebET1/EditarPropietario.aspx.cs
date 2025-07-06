using System;
using System.Configuration;
using System.Data;
using Npgsql;

namespace WebET1
{
    public partial class EditarPropietario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["pro_id"] != null)
                {
                    int pro_id = Convert.ToInt32(Request.QueryString["pro_id"]);
                    CargarDatos(pro_id);
                }
                else
                {
                    Response.Redirect("Propietarios.aspx");
                }
            }
        }

        private void CargarDatos(int id)
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(conexion))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("gestion.sp_obtener_propietario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_pro_id", id);

                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtTipoIdentificacion.Text = dr["opc_tipoidentificacion"] != DBNull.Value ? dr["opc_tipoidentificacion"].ToString() : "";
                            txtNumIdentificacion.Text = dr["pro_num_identificacion"] != DBNull.Value ? dr["pro_num_identificacion"].ToString() : "";
                            txtNombre.Text = dr["pro_nombre"] != DBNull.Value ? dr["pro_nombre"].ToString() : "";
                            txtApellido.Text = dr["pro_apellido"] != DBNull.Value ? dr["pro_apellido"].ToString() : "";
                            txtCiudad.Text = dr["pro_direccion_ciudad"] != DBNull.Value ? dr["pro_direccion_ciudad"].ToString() : "";
                            txtCorreo.Text = dr["pro_correo_electronico"] != DBNull.Value ? dr["pro_correo_electronico"].ToString() : "";
                            txtTelefono.Text = dr["pro_telefono1"] != DBNull.Value ? dr["pro_telefono1"].ToString() : "";
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
                int pro_id = Convert.ToInt32(Request.QueryString["pro_id"]);
                string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

                using (NpgsqlConnection con = new NpgsqlConnection(conexion))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("gestion.sp_actualizar_propietario", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_pro_id", pro_id);
                        cmd.Parameters.AddWithValue("p_opc_tipoidentificacion", int.Parse(txtTipoIdentificacion.Text));
                        cmd.Parameters.AddWithValue("p_pro_num_identificacion", txtNumIdentificacion.Text);
                        cmd.Parameters.AddWithValue("p_pro_nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("p_pro_apellido", txtApellido.Text);
                        cmd.Parameters.AddWithValue("p_pro_direccion_ciudad", txtCiudad.Text);
                        cmd.Parameters.AddWithValue("p_pro_direccion_domicilio", "");
                        cmd.Parameters.AddWithValue("p_pro_direccion_referencia", "");
                        cmd.Parameters.AddWithValue("p_pro_fecha_nacimiento", DBNull.Value);
                        cmd.Parameters.AddWithValue("p_opc_estado_civil", 0);
                        cmd.Parameters.AddWithValue("p_pro_sexo", (short)0);
                        cmd.Parameters.AddWithValue("p_pro_correo_electronico", txtCorreo.Text);
                        cmd.Parameters.AddWithValue("p_pro_telefono1", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("p_pro_telefono2", "");
                        cmd.Parameters.AddWithValue("p_pro_codigo_postal", "");
                        cmd.Parameters.AddWithValue("p_pro_nro_conadis", "");
                        cmd.Parameters.AddWithValue("p_pro_porcentaje_conadis", 0);
                        cmd.Parameters.AddWithValue("p_opc_tipo_conadis", 0);
                        cmd.Parameters.AddWithValue("p_pro_validado", DBNull.Value);
                        cmd.Parameters.AddWithValue("p_opc_tipo_entidad", 0);
                        cmd.Parameters.AddWithValue("p_pro_tipo_persona", (short)0);
                        cmd.Parameters.AddWithValue("p_pro_numero_registro", "");
                        cmd.Parameters.AddWithValue("p_pro_genero", "");
                        cmd.Parameters.AddWithValue("p_pro_inscrito_en", "");
                        cmd.Parameters.AddWithValue("p_pro_lugar_inscripcion", "");
                        cmd.Parameters.AddWithValue("p_pro_id_cliente", (long)0);
                        cmd.Parameters.AddWithValue("p_pro_tiene_ruc", (short)0);
                        cmd.Parameters.AddWithValue("p_pro_ruc", "");
                        cmd.Parameters.AddWithValue("p_pro_razon_social_pn", "");
                        cmd.Parameters.AddWithValue("p_pro_fecha_fallecido", DBNull.Value);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                Response.Redirect("Propietarios.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al actualizar: {ex.Message}');</script>");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Propietarios.aspx");
        }
    }
}
