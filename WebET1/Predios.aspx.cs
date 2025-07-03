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

        // Evento para redirigir a la página de edición
        protected void GridViewPredios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Obtener el ID del predio de la fila seleccionada
            int preId = Convert.ToInt32(GridViewPredios.DataKeys[e.NewEditIndex].Value);

            // Redirigir a la página EditarPredio.aspx con el ID del predio
            Response.Redirect($"EditarPredio.aspx?pre_id={preId}");
        }

        // Evento para eliminar un predio
        protected void GridViewPredios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Obtener el ID del predio de la fila seleccionada
            int preId = Convert.ToInt32(GridViewPredios.DataKeys[e.RowIndex].Value);

            // Eliminar el predio de la base de datos
            string connStr = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;

            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM catastro.cat_predio WHERE pre_id = @preId";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@preId", preId);
                    cmd.ExecuteNonQuery();
                }
            }

            // Recargar el GridView después de eliminar
            CargarPredios();
        }

        // Evento para redirigir a la página AgregarPredio.aspx
        protected void btnAgregarPredio_Click(object sender, EventArgs e)
        {
            // Redirigir a la página AgregarPredio.aspx
            Response.Redirect("AgregarPredio.aspx");
        }
    }
}
