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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarPropietario.aspx");
        }

        protected void GridViewPropietarios_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            GridViewPropietarios.PageIndex = e.NewPageIndex;
            CargarPropietarios();
        }

        protected void GridViewPropietarios_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar" || e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int pro_id = Convert.ToInt32(GridViewPropietarios.DataKeys[index].Value);

                if (e.CommandName == "Editar")
                {
                    Response.Redirect($"EditarPropietario.aspx?pro_id={pro_id}");
                }
                else if (e.CommandName == "Eliminar")
                {
                    EliminarPropietario(pro_id);
                    CargarPropietarios();
                }
            }
        }

        private void EliminarPropietario(int pro_id)
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(conexion))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("gestion.sp_eliminar_propietario", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_pro_id", pro_id);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al eliminar: {ex.Message}');</script>");
            }
        }
    }
}
