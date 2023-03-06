using DBLib;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using Google.Protobuf.WellKnownTypes;
using System.Collections.ObjectModel;

namespace ConnexioBD
{
    public class Intro : Level
    {
        public Intro(string nom, string descripcio, int hores, int minuts, int segons, bool actiu, string url) : base(nom, descripcio, hores, minuts, segons, actiu, url)
        {
        }






        //Métodes SQL


        public static Boolean Inserir(Intro entrada)
        {

            int last_id=0;

            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {

                        comanda.CommandText = @"select max(id) as id from introduccio";

                        DbDataReader reader = comanda.ExecuteReader();

                        while (reader.Read())
                        {
                            last_id = reader.GetInt32(reader.GetOrdinal("id"));

                        }
                           
                        last_id++;

                        reader.Close();
                        comanda.CommandText = @"insert into introduccio(id,intro_nom,intro_desc,hores,minuts,segons,intro_imatge,estat) values(@idBD,@intro_nom,@intro_desc,@hores,@minuts,@segons,@intro_imatge,@estat)";
                        DBUtils.afegirParametre(comanda, "idBD", last_id, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "intro_nom", entrada.Nom, DbType.String);
                        DBUtils.afegirParametre(comanda, "intro_desc", entrada.Descripcio, DbType.String);
                        DBUtils.afegirParametre(comanda, "hores", entrada.Hores, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "minuts", entrada.Minuts, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "segons", entrada.Segons, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "intro_imatge", entrada.Url, DbType.String);
                        DBUtils.afegirParametre(comanda, "estat", entrada.Actiu, DbType.Boolean);

                        Int32 filesInserides = comanda.ExecuteNonQuery();

                        return filesInserides == 1;
                    }
                }
            }


            return false;
        }






        public static List<Level> getIntro()
        {
            List<Level> resultat = new List<Level>();
            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {
                        comanda.CommandText = @"select * from introduccio";
                       

                        DbDataReader reader = comanda.ExecuteReader();
                        while (reader.Read())
                        {

                          
                            string intro_nom = reader.GetString(reader.GetOrdinal("intro_nom"));
                            string intro_desc = reader.GetString(reader.GetOrdinal("intro_desc"));
                            int hores = reader.GetInt32(reader.GetOrdinal("hores"));
                            int minuts = reader.GetInt32(reader.GetOrdinal("minuts"));
                            int segons = reader.GetInt32(reader.GetOrdinal("segons"));
                            string intro_imatge = reader.GetString(reader.GetOrdinal("intro_imatge"));
                            bool estat = reader.GetBoolean(reader.GetOrdinal("estat"));


                            Intro nou = new Intro(intro_nom, intro_desc, hores, minuts, segons,  estat, intro_imatge);
                            resultat.Add(nou);
                        }
                    }
                }
            }
            return resultat;
        }









    }
    
}
