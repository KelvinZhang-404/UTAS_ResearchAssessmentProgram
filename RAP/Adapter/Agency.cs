using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAPWPF.RAPModel;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Collections.ObjectModel;

namespace RAPWPF
{
    abstract class Agency
    {
        //These would not be hard coded in the source file normally, but read from the application's settings (and, ideally, with some amount of basic encryption applied)
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;

        //Part of step 2.3.3 in Week 9 tutorial. This method is a gift to you because .NET's approach to converting strings to enums is so horribly broken
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Creates and returns (but does not open) the connection to the database.
        /// </summary>
        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                //Note: This approach is not thread-safe
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        public static List<Researcher> LoadResearchers()
        {
            List<Researcher> researchers = new List<Researcher>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                string query = "select id, given_name, family_name, type, level, title from researcher";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Note that in your assignment you will need to inspect the *type* of the
                    //employee/researcher before deciding which kind of concrete class to create.
                    if(rdr.GetString(3) == "Staff")
                    {
                        researchers.Add(new Staff {
                            ID = rdr.GetInt32(0),
                            Name = rdr.GetString(2) + ", " + rdr.GetString(1),
                            GivenName = rdr.GetString(1),
                            FamilyName = rdr.GetString(2),
                            Type =ParseEnum<RAPModel.Type>(rdr.GetString(3)),
                            Level = ParseEnum<Level>(rdr.GetString(4)),
                            Title = rdr.GetString(5)
                        });
                    }
                    else if(rdr.GetString(3) == "Student")
                    {
                        researchers.Add(new Student {
                            ID = rdr.GetInt32(0),
                            Name = rdr.GetString(2) + ", " + rdr.GetString(1),
                            GivenName = rdr.GetString(1),
                            FamilyName = rdr.GetString(2),
                            Type = ParseEnum<RAPModel.Type>(rdr.GetString(3)),
                            Level = Level.Null,
                            Title = rdr.GetString(5)
                        });;
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return researchers;
        }

        //For step 2.2 in Week 9 tutorial
        public static List<Student> LoadSupervision(Staff staff)
        {
            List<Student> supervisions = new List<Student>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                string query = "select id, given_name, family_name, title from researcher where type='Student' and supervisor_id=?id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("id", staff.ID);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Note that in your assignment you will need to inspect the *type* of the
                    //employee/researcher before deciding which kind of concrete class to create.
                    supervisions.Add(new Student {
                        ID = rdr.GetInt32(0),
                        Name = rdr.GetString(2) + ", " + rdr.GetString(1),
                        Title = rdr.GetString(3)
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return supervisions;
        }

        //For step 2.3 in Week 9 tutorial
        public static List<Publication> LoadPublications(int id)
        {
            List<Publication> publications = new List<Publication>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select title, year, type, available, pub.doi " +
                                                    "from publication as pub, researcher_publication as respub " +
                                                    "where pub.doi=respub.doi and researcher_id=?id order by year desc", conn);

                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    publications.Add(new Publication
                    {
                        Title = rdr.GetString(0),
                        Year = rdr.GetInt32(1),
                        Mode = ParseEnum<Mode>(rdr.GetString(2)),
                        Certified = rdr.GetDateTime(3),
                        DOI = rdr.GetString(4)
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return publications;
        }
        public static void LoadPublicationDetails(Publication publication)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select authors, cite_as " +
                                                    "from publication " +
                                                    "where publication.doi=?doi", conn);

                cmd.Parameters.AddWithValue("doi", publication.DOI);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    publication.Authors = rdr.GetString(0);
                    publication.CiteAs = rdr.GetString(1);
                    publication.loaded = true;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public static List<Tuple<string, int>> PublicationCumulativeCount(Researcher e)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            int count = 0;
            var counts = new List<Tuple<string, int>>();
            try
            {
                conn.Open();
                // Specify a year range query
                //query = "select count(*) from publication as pub, researcher_publication as respub " +
                //"where pub.doi = respub.doi and researcher_id = ?id and year >= ?start and year <= ?end"
                string query = "select year, count(*) from publication as pub, researcher_publication as respub " +
                    "where pub.doi = respub.doi and researcher_id = ?id group by year";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("id", e.ID);
                //cmd.Parameters.AddWithValue("start", startYear);
                //cmd.Parameters.AddWithValue("end", endYear);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string year = rdr.GetString(0);
                    int number_in_year = Int32.Parse(rdr.GetString(1));
                    counts.Add(new Tuple<string, int>(year, number_in_year));
                    count += number_in_year;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error connecting to database: " + ex);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return counts;
        }
        public static List<Position> LoadPositions(int id)
        {
            List<Position> positions = new List<Position>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select start, end, level " +
                                                    "from position " +
                                                    "where position.id=?id", conn);

                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    positions.Add(new Position
                    {
                        ID = id,
                        StartDate = rdr.GetDateTime(0),
                        EndDate = Convert.IsDBNull(rdr.GetValue(1)) ? (DateTime?)null : rdr.GetDateTime(1),
                        Level = ParseEnum<Level>(rdr.GetString(2))
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return positions;
        }

        public static void LoadPrimaryDetails(Researcher researcher)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select unit, campus, email, photo " +
                                                    "from researcher " +
                                                    "where researcher.id=?id", conn);

                cmd.Parameters.AddWithValue("id", researcher.ID);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    researcher.Unit = rdr.GetString(0);
                    researcher.Campus = rdr.GetString(1) == "Cradle Coast" ? Campus.CradleCoast : ParseEnum<Campus>(rdr.GetString(1));
                    //researcher.Campus = ParseEnum<Campus>(rdr.GetString(1));
                    researcher.Email = rdr.GetString(2);
                    researcher.Photo = rdr.GetString(3);
                    researcher.Loaded = true;
                    switch (researcher.Level)
                    {
                        case Level.A:
                            researcher.JobTitle = "Postdoc";
                            break;
                        case Level.B:
                            researcher.JobTitle = "Lecturer";
                            break;
                        case Level.C:
                            researcher.JobTitle = "Senior Lecturer";
                            break;
                        case Level.D:
                            researcher.JobTitle = "Associate Professor";
                            break;
                        case Level.E:
                            researcher.JobTitle = "Others";
                            break;
                        default:
                            researcher.JobTitle = "Student";
                            break;
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

        public static int YearRangePublicationCount(Researcher r, int startYear, int endYear)
        {
            MySqlConnection conn = GetConnection();
            int count = 0;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select count(*) from publication as pub, researcher_publication as respub " +
                                                    "where pub.doi = respub.doi and researcher_id = ?id and year >= ?start and year <= ?end", conn);
                cmd.Parameters.AddWithValue("id", r.ID);
                cmd.Parameters.AddWithValue("start", startYear);
                cmd.Parameters.AddWithValue("end", endYear);
                count = Int32.Parse(cmd.ExecuteScalar().ToString());
        }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error connecting to database: " + ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return count;
        }

        public static void LoadOtherDetails(Researcher researcher)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select degree, supervisor_id, utas_start, current_start " +
                                                    "from researcher " +
                                                    "where researcher.id=?id", conn);

                cmd.Parameters.AddWithValue("id", researcher.ID);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    researcher.CommencedIns = rdr.GetDateTime(2);
                    researcher.CommencedPos = rdr.GetDateTime(3);
                    researcher.Tenure = Math.Round(((DateTime.Now - researcher.CommencedIns).TotalDays / 365), 2);

                    if (researcher.Type == RAPModel.Type.Student)
                    {
                        (researcher as Student).Degree = rdr.GetString(0);
                        (researcher as Student).SupervisorID = rdr.GetInt32(1);
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static void LoadSupervisor(Student student)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select given_name, family_name " +
                                                    "from researcher " +
                                                    "where researcher.id=?id", conn);

                cmd.Parameters.AddWithValue("id", student.SupervisorID);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    student.Supervisor = rdr.GetString(0) + " " + rdr.GetString(1);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
