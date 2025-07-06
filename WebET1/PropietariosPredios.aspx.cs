using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Npgsql;

namespace WebET1
{
    public partial class PropietarisoPredios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPropietariosPredios();
            }
        }

        private void CargarPropietariosPredios()
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(conexion))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_listar_propietarios_predio", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            GridViewPropietariosPredios.DataSource = dt;
                            GridViewPropietariosPredios.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al cargar los datos: {ex.Message}');</script>");
            }

        }

        protected void GridViewPropietariosPredios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar" || e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int prp_id = Convert.ToInt32(GridViewPropietariosPredios.DataKeys[index].Value);

                if (e.CommandName == "Editar")
                {
                    Response.Redirect($"EditarPropietarioPredio.aspx?prp_id={prp_id}");
                }
                else if (e.CommandName == "Eliminar")
                {
                    EliminarPropietarioPredio(prp_id);
                    CargarPropietariosPredios(); // Refresca el listado después de eliminar
                }
            }
        }

        private void EliminarPropietarioPredio(int prp_id)
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(conexion))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_eliminar_propietario_predio", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_prp_id", prp_id);

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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarPropietarioPredio.aspx");
        }

        protected void GridViewPropietariosPredios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPropietariosPredios.PageIndex = e.NewPageIndex;
            CargarPropietariosPredios(); // Volvemos a cargar los datos
        }

    }
}
