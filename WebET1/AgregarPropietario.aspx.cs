using System;
using System.Configuration;
using Npgsql;

namespace WebET1
{
    public partial class AgregarPropietario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

                using (NpgsqlConnection con = new NpgsqlConnection(conexion))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("gestion.sp_insertar_propietario", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

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
                Response.Write($"<script>alert('Error al registrar: {ex.Message}');</script>");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Propietarios.aspx");
        }
    }
}
