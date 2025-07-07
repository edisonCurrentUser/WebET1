using System;
using System.Data;
using System.Web.UI;
using Npgsql;
using System.Configuration;
using System.Web.UI.WebControls;

namespace WebET1
{
    public partial class Predios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPredios();
            }
        }

        private void CargarPredios()
        {
            string connStr = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_listar_predios", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridViewPredios.DataSource = dt;
                    GridViewPredios.DataBind();
                }
            }
        }

        protected void GridViewPredios_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(GridViewPredios.DataKeys[index].Value);
                Response.Redirect($"EditarPredio.aspx?id={id}");
            }
            else if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(GridViewPredios.DataKeys[index].Value);
                EliminarPredio(id);
            }
        }

        private void EliminarPredio(int id)
        {
            try
            {
                string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

                using (NpgsqlConnection con = new NpgsqlConnection(conexion))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("catastro.sp_eliminar_predio", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_pre_id", id);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                CargarPredios();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al eliminar: {ex.Message.Replace("'", "")}');</script>");
            }
        }


        protected void btnAgregarPredio_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarPredio.aspx");
        }

        protected void GridViewPredios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPredios.PageIndex = e.NewPageIndex;
            CargarPredios(); 
        }

    }
}
