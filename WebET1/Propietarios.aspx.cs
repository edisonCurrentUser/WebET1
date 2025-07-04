using System;
using System.Configuration;
using System.Data;
using Npgsql;

namespace WebET1
{
    public partial class Propietarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPropietarios();
            }
        }

        private void CargarPropietarios()
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(conexion))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("gestion.sp_listar_propietarios", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                            da.Fill(dt);

                        GridViewPropietarios.DataSource = dt;
                        GridViewPropietarios.DataBind();
                    }
                }
            }
        }
    }
}
