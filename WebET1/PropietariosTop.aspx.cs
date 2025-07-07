using System;
using System.Configuration;
using System.Data;
using System.Web.Services;
using Npgsql;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace WebET1
{
    public partial class PropietariosTop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string ObtenerTopPropietarios()
        {
            string conexion = ConfigurationManager.ConnectionStrings["conexionPostgres"].ConnectionString;
            List<object> listaPropietarios = new List<object>();

            using (NpgsqlConnection con = new NpgsqlConnection(conexion))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT 
                                                                  p.pro_id,
                                                                  p.pro_nombre || ' ' || p.pro_apellido AS nombre_completo,
                                                                  COUNT(pp.pre_id) AS total_propiedades
                                                              FROM 
                                                                  gestion.ges_propietario p
                                                              JOIN 
                                                                  catastro.cat_propietario_predio pp ON p.pro_id = pp.pro_id
                                                              GROUP BY 
                                                                  p.pro_id, p.pro_nombre, p.pro_apellido
                                                              ORDER BY 
                                                                  total_propiedades DESC
                                                              LIMIT 5", con))
                {
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaPropietarios.Add(new
                            {
                                nombre = dr["nombre_completo"].ToString(),
                                total = Convert.ToInt32(dr["total_propiedades"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(listaPropietarios);
        }
    }
}
