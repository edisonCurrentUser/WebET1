using System;
using System.Configuration;
using System.Data;
using Npgsql;

namespace WebET1
{
    public partial class AgregarPropietario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCatalogos();
            }
        }

        private void CargarCatalogos()
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(conexion))
            {
                con.Open();

                CargarCombo(con, "tipo_identificacion", ddlTipoIdentificacion);
                CargarCombo(con, "estado_civil", ddlEstadoCivil);
                CargarCombo(con, "tipo_conadis", ddlTipoConadis);
                CargarCombo(con, "tipo_entidad", ddlTipoEntidad);

                con.Close();
            }
        }

        private void CargarCombo(NpgsqlConnection con, string tipoCatalogo, System.Web.UI.WebControls.DropDownList ddl)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand("gestion.sp_listar_catalogo_tipo", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_tipo_catalogo", tipoCatalogo);

                using (NpgsqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    ddl.DataSource = dt;
                    ddl.DataTextField = "cat_nombre";
                    ddl.DataValueField = "cat_id";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --", ""));
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
                    using (NpgsqlCommand cmd = new NpgsqlCommand("gestion.sp_insertar_propietario", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_opc_tipoidentificacion", int.Parse(ddlTipoIdentificacion.SelectedValue));
                        cmd.Parameters.AddWithValue("p_pro_num_identificacion", txtNumIdentificacion.Text);
                        cmd.Parameters.AddWithValue("p_pro_nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("p_pro_apellido", txtApellido.Text);
                        cmd.Parameters.AddWithValue("p_pro_direccion_ciudad", txtCiudad.Text);
                        cmd.Parameters.AddWithValue("p_pro_correo_electronico", txtCorreo.Text);
                        cmd.Parameters.AddWithValue("p_pro_telefono1", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("p_opc_estado_civil", int.Parse(ddlEstadoCivil.SelectedValue));
                        cmd.Parameters.AddWithValue("p_opc_tipo_conadis", int.Parse(ddlTipoConadis.SelectedValue));
                        cmd.Parameters.AddWithValue("p_opc_tipo_entidad", int.Parse(ddlTipoEntidad.SelectedValue));

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                Response.Redirect("Propietarios.aspx");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al guardar: {ex.Message}');</script>");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Propietarios.aspx");
        }
    }
}
