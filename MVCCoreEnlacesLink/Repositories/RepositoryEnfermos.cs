using MVCCoreEnlacesLink.Models;
using System.Data;
using System.Data.SqlClient;

namespace NetCoreLinqToSql.Repositories
{
    public class RepositoryEnfermos
    {
        private DataTable tablaEnfermos;
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;

        public RepositoryEnfermos()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;User ID=sa;Password=";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            string sql = "SELECT * FROM ENFERMO";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
            this.tablaEnfermos = new DataTable();
            adapter.Fill(this.tablaEnfermos);
        }

        public List<Enfermo> GetEnfermos()
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable()
                           select datos;
            List<Enfermo> enfermos = new List<Enfermo>();
            foreach (var row in consulta)
            {
                Enfermo enfermo = new Enfermo
                {
                    Inscripcion = row.Field<string>("INSCRIPCION"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Direccion = row.Field<string>("DIRECCION"),
                    Fecha_Nac = row.Field<DateTime>("FECHA_NAC"),
                    S = row.Field<string>("S"),
                    NSS = row.Field<string>("NSS")
                };
                enfermos.Add(enfermo);
            }
            return enfermos;
        }

        /*public Enfermo FindEmpleado(int idenfermo)
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable()
                           where datos.Field<int>("EMP_NO") == idenfermo
                           select datos;
            var row = consulta.First();
            Enfermo enfermo = new Enfermo
            {
                Inscripcion = row.Field<string>("INSCRIPCION"),
                Apellido = row.Field<string>("APELLIDO"),
                Direccion = row.Field<string>("DIRECCION"),
                Fecha_Nac = row.Field<DateTime>("FECHA_NAC"),
                S = row.Field<string>("S"),
                NSS = row.Field<string>("NSS")
            };
            return enfermo;
        }*/

        public List<Enfermo> BuscarEnfermo(DateTime fecha)
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable()
                           where datos.Field<DateTime>("FECHA_NAC") == fecha
                           /*&& datos.Field<int>("SALARIO") >= salario*/
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                List<Enfermo> enfermos = new List<Enfermo>();
                foreach (var row in consulta)
                {
                    Enfermo enfermo = new Enfermo
                    {
                        Inscripcion = row.Field<string>("INSCRIPCION"),
                        Apellido = row.Field<string>("APELLIDO"),
                        Direccion = row.Field<string>("DIRECCION"),
                        Fecha_Nac = row.Field<DateTime>("FECHA_NAC"),
                        S = row.Field<string>("S"),
                        NSS = row.Field<string>("NSS")
                    };
                    enfermos.Add(enfermo);
                }
                return enfermos;
            }
        }

        public void DeleteEnfermo(int id)
        {
            string sql = "DELETE FROM ENFERMO WHERE INSCRIPCION=@ID";
            SqlParameter pamid = new SqlParameter("@ID", id);
            this.com.Parameters.Add(pamid);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
