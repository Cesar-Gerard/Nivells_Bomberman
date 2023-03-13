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
using Org.BouncyCastle.Utilities.Collections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ConnexioBD
{
    public class Intro : Level
    {
        public string url;

        

        public Intro(int id, string nom, string descripcio, int hores, int minuts, int segons, bool actiu, string url) : base(id,nom, descripcio, hores, minuts, segons, actiu)
        {
            Url = url;
        }

        public Intro(string nom, string descripcio, int hores, int minuts, int segons, bool actiu, string url) : base(nom, descripcio, hores, minuts, segons, actiu)
        {
            Url = url;
        }


        public string Url { get => url; set => url = value; }







        //Métodes SQL


        public static Boolean Inserir(Intro entrada)
        {

         

            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {

                       
                        comanda.CommandText = @"insert into introduccio(intro_nom,intro_desc,hores,minuts,segons,intro_imatge,estat) values(@intro_nom,@intro_desc,@hores,@minuts,@segons,@intro_imatge,@estat)";
                       
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






        public static ObservableCollection<Intro> getIntro()
        {
            ObservableCollection<Intro> resultat = new ObservableCollection<Intro>();
            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {
                        comanda.CommandText = @"select id_introduccio,intro_nom,intro_desc,hores,minuts,segons,intro_imatge,estat from introduccio";
                       

                        DbDataReader reader = comanda.ExecuteReader();
                        while (reader.Read())
                        {

                            int id = reader.GetInt32(reader.GetOrdinal("id_introduccio"));
                            string intro_nom = reader.GetString(reader.GetOrdinal("intro_nom"));
                            string intro_desc = reader.GetString(reader.GetOrdinal("intro_desc"));
                            int hores = reader.GetInt32(reader.GetOrdinal("hores"));
                            int minuts = reader.GetInt32(reader.GetOrdinal("minuts"));
                            int segons = reader.GetInt32(reader.GetOrdinal("segons"));
                            string intro_imatge = reader.GetString(reader.GetOrdinal("intro_imatge"));
                            bool estat = reader.GetBoolean(reader.GetOrdinal("estat"));


                            Intro nou = new Intro(id,intro_nom,intro_desc,hores,minuts,segons,estat,intro_imatge);

                            resultat.Add(nou);
                        }
                    }
                }
            }
            return resultat;
        }



        public static Boolean eliminarIntro(Intro entrada)
        {
            DbTransaction transaccio = null;
            try
            {
                using (MySQLDbContext context = new MySQLDbContext())
                {
                    using (var connection = context.Database.GetDbConnection())
                    {
                        connection.Open();

                        transaccio = connection.BeginTransaction();
                        using (var comanda = connection.CreateCommand())
                        {



                            comanda.CommandText =
                                "delete from introduccio where id_introduccio=@intro_id";
                            //-----------------------------------------------
                            //    IMPORTANT !!! posem la comanda dins de la transacció
                            //-----------------------------------------------
                            comanda.Transaction = transaccio; //
                            //-----------------------------------------------
                            DBUtils.afegirParametre(comanda, "intro_id", entrada.Id, DbType.Int32);
                            int liniesAfectades = comanda.ExecuteNonQuery();
                            if (liniesAfectades != 1)
                            {
                                transaccio.Rollback();

                            }
                            else
                            {
                                transaccio.Commit();
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR" + ex);
            }
            return false;
        }




        public static Boolean getNom(string nom)
        {

            string intro_nom = null;
            ObservableCollection<Level> resultat = new ObservableCollection<Level>();
            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {
                        comanda.CommandText = @"select intro_nom from introduccio where intro_nom = @nom";
                        DBUtils.afegirParametre(comanda, "nom", nom, DbType.String);


                        DbDataReader reader = comanda.ExecuteReader();
                        while (reader.Read())
                        {
                             intro_nom = reader.GetString(reader.GetOrdinal("intro_nom"));

                        }


                        if(intro_nom != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }


                    }
                }
            }
            
        }


        public static Boolean Update(Intro nou, Intro antic)
        {

            int last_id = 0;

            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {


                       

                        comanda.CommandText = @"update introduccio set intro_nom=@intro_nom,intro_desc=@intro_desc,hores=@hores,minuts=@minuts,segons=@segons,intro_imatge=@intro_imatge,estat=@estat where id_introduccio = @id_antic";

                        DBUtils.afegirParametre(comanda, "id_antic", antic.Id, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "intro_nom", nou.Nom, DbType.String);
                        DBUtils.afegirParametre(comanda, "intro_desc", nou.Descripcio, DbType.String);
                        DBUtils.afegirParametre(comanda, "hores", nou.Hores, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "minuts", nou.Minuts, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "segons", nou.Segons, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "intro_imatge", nou.Url, DbType.String);
                        DBUtils.afegirParametre(comanda, "estat", nou.Actiu, DbType.Boolean);

                        Int32 filesInserides = comanda.ExecuteNonQuery();




                        return filesInserides == 1;
                    }
                }
            }


            return false;
        }




        public static int getIdIntro(Intro entrada)
        {
            int resultat = 0;
            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {
                        comanda.CommandText = @"select id_introduccio from introduccio where intro_nom like @intro_nom";
                        DBUtils.afegirParametre(comanda, "intro_nom", entrada.Nom, DbType.String);

                        DbDataReader reader = comanda.ExecuteReader();
                        while (reader.Read())
                        {

                            resultat = reader.GetInt32(reader.GetOrdinal("id_introduccio"));
                        }
                    }
                }
            }
            return resultat;
        }
    }








}
    

