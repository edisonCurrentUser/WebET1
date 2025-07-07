using System;
using System.Configuration;
using System.Data;
using Npgsql;
using System.Web.UI;

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

                // Alerta de éxito con redirección al confirmar
                string successScript = @"
                    Swal.fire({
                        title: '¡Éxito!',
                        text: 'Propietario agregado correctamente.',
                        icon: 'success',
                        confirmButtonText: 'Ver lista'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            window.location = 'Propietarios.aspx';
                        }
                    });
                ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "swalSuccess", successScript, true);
            }
            catch (Exception ex)
            {
                // Alerta de error
                string errorScript = $@"
                    Swal.fire({{
                        title: 'Error al guardar',
                        text: '{ex.Message.Replace("'", "\\'")}',
                        icon: 'error',
                        confirmButtonText: 'OK'
                    }});
                ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "swalError", errorScript, true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Propietarios.aspx");
        }
    }
}
