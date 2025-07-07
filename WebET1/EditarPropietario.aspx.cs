using System;
using System.Configuration;
using System.Data;
using Npgsql;
using System.Web.UI;

namespace WebET1
{
    public partial class EditarPropietario : System.Web.UI.Page
    {
        int propietarioId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    propietarioId = int.Parse(Request.QueryString["id"]);
                    CargarCatalogos();
                    CargarDatos(propietarioId);
                }
                else
                {
                    Response.Redirect("Propietarios.aspx");
                }
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

        private void CargarDatos(int id)
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(conexion))
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM gestion.ges_propietario WHERE pro_id = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();

                using (NpgsqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        ddlTipoIdentificacion.SelectedValue = dr["opc_tipoidentificacion"].ToString();
                        txtNumIdentificacion.Text = dr["pro_num_identificacion"].ToString();
                        txtNombre.Text = dr["pro_nombre"].ToString();
                        txtApellido.Text = dr["pro_apellido"].ToString();
                        txtCiudad.Text = dr["pro_direccion_ciudad"].ToString();
                        txtCorreo.Text = dr["pro_correo_electronico"].ToString();
                        txtTelefono.Text = dr["pro_telefono1"].ToString();
                        ddlEstadoCivil.SelectedValue = dr["opc_estado_civil"].ToString();
                        ddlTipoConadis.SelectedValue = dr["opc_tipo_conadis"].ToString();
                        ddlTipoEntidad.SelectedValue = dr["opc_tipo_entidad"].ToString();
                    }
                    else
                    {
                        Response.Redirect("Propietarios.aspx");
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Propietarios.aspx");
                return;
            }

            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

                using (NpgsqlConnection con = new NpgsqlConnection(conexion))
                using (NpgsqlCommand cmd = new NpgsqlCommand("gestion.sp_actualizar_propietario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_pro_id", id);
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

                // Alerta de éxito y redirección
                string successScript = @"
                    Swal.fire({
                        title: '¡Actualizado!',
                        text: 'Propietario modificado correctamente.',
                        icon: 'success',
                        confirmButtonText: 'Volver'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            window.location = 'Propietarios.aspx';
                        }
                    });
                ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "swalUpdateSuccess", successScript, true);
            }
            catch (Exception ex)
            {
                // Alerta de error
                string errMsg = ex.Message.Replace("'", "\\'");
                string errorScript = $@"
                    Swal.fire({{
                        title: 'Error al actualizar',
                        text: '{errMsg}',
                        icon: 'error',
                        confirmButtonText: 'OK'
                    }});
                ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "swalUpdateError", errorScript, true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Propietarios.aspx");
        }
    }
}
