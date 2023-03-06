using DBLib;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ConnexioBD
{
    public class Level
    {
        String nom;
        String descripcio;
        int hores;
        int minuts;
        int segons;
        bool actiu;
        String url;

        public Level(string nom, string descripcio, int hores, int minuts, int segons, bool actiu, string url)
        {
            Nom = nom;
            Descripcio = descripcio;
            Hores = hores;
            Minuts = minuts;
            Segons = segons;
            Actiu = actiu;
            Url = url;
        }

        public string Nom { get => nom; set => nom = value; }
        public string Descripcio { get => descripcio; set => descripcio = value; }
        public int Hores { get => hores; set => hores = value; }
        public int Minuts { get => minuts; set => minuts = value; }
        public int Segons { get => segons; set => segons = value; }
        public bool Actiu { get => actiu; set => actiu = value; }
        public string Url { get => url; set => url = value; }



        //Métodes SQL


        public static Boolean InserirNivell(Level entrada)
        {

            int last_id = 0;

            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {

                        comanda.CommandText = @"select max(id) as id from nivell";

                        DbDataReader reader = comanda.ExecuteReader();

                        while (reader.Read())
                        {
                            last_id = reader.GetInt32(reader.GetOrdinal("id"));

                        }

                        last_id++;

                        reader.Close();
                        comanda.CommandText = @"insert into introduccio(id,nivell_nom,nivell_desc,hores,minuts,segons,nivell_imatge,estat) values(@idBD,@nivell_nom,@nivell_desc,@hores,@minuts,@segons,@nivell_imatge,@estat)";
                        DBUtils.afegirParametre(comanda, "idBD", last_id, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "nivell_nom", entrada.Nom, DbType.String);
                        DBUtils.afegirParametre(comanda, "nivell_desc", entrada.Descripcio, DbType.String);
                        DBUtils.afegirParametre(comanda, "hores", entrada.Hores, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "minuts", entrada.Minuts, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "segons", entrada.Segons, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "nivell_imatge", entrada.Url, DbType.String);
                        DBUtils.afegirParametre(comanda, "estat", entrada.Actiu, DbType.Boolean);

                        Int32 filesInserides = comanda.ExecuteNonQuery();

                        return filesInserides == 1;
                    }
                }
            }


            return false;
        }






        public static List<Level> getNivell()
        {
            List<Level> resultat = new List<Level>();
            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {
                        comanda.CommandText = @"select * from nivell";


                        DbDataReader reader = comanda.ExecuteReader();
                        while (reader.Read())
                        {


                            string intro_nom = reader.GetString(reader.GetOrdinal("nivell_nom"));
                            string intro_desc = reader.GetString(reader.GetOrdinal("nivell_desc"));
                            int hores = reader.GetInt32(reader.GetOrdinal("hores"));
                            int minuts = reader.GetInt32(reader.GetOrdinal("minuts"));
                            int segons = reader.GetInt32(reader.GetOrdinal("segons"));
                            string intro_imatge = reader.GetString(reader.GetOrdinal("nivell_imatge"));
                            bool estat = reader.GetBoolean(reader.GetOrdinal("estat"));


                            Level nou = new Level(intro_nom, intro_desc, hores, minuts, segons, estat, intro_imatge);
                            resultat.Add(nou);
                        }
                    }
                }
            }
            return resultat;
        }


        public static List<Level> getTot()
        {
            List<Level> resultat = new List<Level>();
            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {
                        comanda.CommandText = @"select * from nivell, introduccio";


                        DbDataReader reader = comanda.ExecuteReader();
                        while (reader.Read())
                        {


                            string nivell_nom = reader.GetString(reader.GetOrdinal("nivell_nom"));
                            string nivell_desc = reader.GetString(reader.GetOrdinal("nivell_desc"));
                            int hores = reader.GetInt32(reader.GetOrdinal("hores"));
                            int minuts = reader.GetInt32(reader.GetOrdinal("minuts"));
                            int segons = reader.GetInt32(reader.GetOrdinal("segons"));
                            string nivell_imatge = reader.GetString(reader.GetOrdinal("nivell_imatge"));
                            bool estat = reader.GetBoolean(reader.GetOrdinal("estat"));

                          


                            Level nou = new Level(nivell_nom, nivell_desc, hores, minuts, segons, estat, nivell_imatge);
                            resultat.Add(nou);
                        }


                        comanda.CommandText = @"select * from introduccio";

                        while (reader.Read())
                        {


                            string intro_nom = reader.GetString(reader.GetOrdinal("intro_nom"));
                            string intro_desc = reader.GetString(reader.GetOrdinal("intro_desc"));
                            int hores = reader.GetInt32(reader.GetOrdinal("hores"));
                            int minuts = reader.GetInt32(reader.GetOrdinal("minuts"));
                            int segons = reader.GetInt32(reader.GetOrdinal("segons"));
                            string intro_imatge = reader.GetString(reader.GetOrdinal("intro_imatge"));
                            bool estat = reader.GetBoolean(reader.GetOrdinal("estat"));

                            Intro nou = new Intro(intro_nom, intro_desc, hores, minuts, segons, estat, intro_imatge);
                            resultat.Add(nou);

                        }


                    }
                }
            }
            return resultat;
        }













    }
}
