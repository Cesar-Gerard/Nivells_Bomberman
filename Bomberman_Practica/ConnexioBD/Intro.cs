using DBLib;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace ConnexioBD
{
    public class Intro : Level
    {
        String url;

        public Intro(string nom, string descripcio, string image, int hores,int minuts, int segons, bool actiu) : base(nom, descripcio, image, hores, minuts,segons, actiu)
        {
            Url = image;
        }

        public string Url { get => url; set => url = value; }





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




    }
    
}
