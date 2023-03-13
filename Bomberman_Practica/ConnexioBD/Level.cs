using DBLib;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Bomberman_Practica.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        int [][] elements = null;
        int id;
        

        public Level(int id,string nom, string descripcio, int hores, int minuts, int segons, bool actiu)
        {
            Id = id;
            Nom = nom;
            Descripcio = descripcio;
            Hores = hores;
            Minuts = minuts;
            Segons = segons;
            Actiu = actiu;
          
        }

        public Level(string nom, string descripcio, int hores, int minuts, int segons, bool actiu) 
        {
           
            Nom = nom;
            Descripcio = descripcio;
            Hores = hores;
            Minuts = minuts;
            Segons = segons;
            Actiu = actiu;
        }

        public string Nom { get => nom; set => nom = value; }
        public string Descripcio { get => descripcio; set => descripcio = value; }
        public int Hores { get => hores; set => hores = value; }
        public int Minuts { get => minuts; set => minuts = value; }
        public int Segons { get => segons; set => segons = value; }
        public bool Actiu { get => actiu; set => actiu = value; }
        public int[][] Elements { get => elements; set => elements = value; }
        public int Id { get => id; set => id = value; }

        public List<Int32> recuperarTemps()
        {
            List<Int32> temps = new List<Int32>();



            temps.Add(this.hores);
            temps.Add(this.minuts);
            temps.Add(this.segons);
            return temps;
        }


        //Métodes SQL


        public static Boolean InserirNivell(Level entrada)
        {

            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {

                        

                       
                        comanda.CommandText = @"insert into nivell(nivell_nom,nivell_desc,hores,minuts,segons,estat) values(@nivell_nom,@nivell_desc,@hores,@minuts,@segons,@estat)";
                 
                        DBUtils.afegirParametre(comanda, "nivell_nom", entrada.Nom, DbType.String);
                        DBUtils.afegirParametre(comanda, "nivell_desc", entrada.Descripcio, DbType.String);
                        DBUtils.afegirParametre(comanda, "hores", entrada.Hores, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "minuts", entrada.Minuts, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "segons", entrada.Segons, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "estat", entrada.Actiu, DbType.Boolean);

                        Int32 filesInserides = comanda.ExecuteNonQuery();

                        return filesInserides == 1;
                    }
                }
            }


            return false;
        }






        public static ObservableCollection<Level> getNivell()
        {
            ObservableCollection<Level> resultat = new ObservableCollection<Level>();
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

                            int id = reader.GetInt32(reader.GetOrdinal("id_nivell"));
                            string intro_nom = reader.GetString(reader.GetOrdinal("nivell_nom"));
                            string intro_desc = reader.GetString(reader.GetOrdinal("nivell_desc"));
                            int hores = reader.GetInt32(reader.GetOrdinal("hores"));
                            int minuts = reader.GetInt32(reader.GetOrdinal("minuts"));
                            int segons = reader.GetInt32(reader.GetOrdinal("segons"));
                            bool estat = reader.GetBoolean(reader.GetOrdinal("estat"));


                            Level nou = new Level(id, intro_nom, intro_desc, hores, minuts, segons, estat);
                            resultat.Add(nou);
                        }
                    }
                }
            }
            return resultat;
        }


      




        public static Boolean eliminarLevel(Level entrada)
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
                                "delete from nivell where id_nivell=@nivell_id";
                            //-----------------------------------------------
                            //    IMPORTANT !!! posem la comanda dins de la transacció
                            //-----------------------------------------------
                            comanda.Transaction = transaccio; //
                            //-----------------------------------------------
                            DBUtils.afegirParametre(comanda, "nivell_id", entrada.Id, DbType.Int32);
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



        public static Boolean UpdateLevel(Level nou, Level antic)
        {


            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {

                        


                        comanda.CommandText = @"update nivell set nivell_nom=@nivell_nom,nivell_desc=@nivell_desc,hores=@hores,minuts=@minuts,segons=@segons,estat=@estat where id_nivell = @id_antic";

                        DBUtils.afegirParametre(comanda, "id_antic", antic.Id, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "nivell_nom", nou.Nom, DbType.String);
                        DBUtils.afegirParametre(comanda, "nivell_desc", nou.Descripcio, DbType.String);
                        DBUtils.afegirParametre(comanda, "hores", nou.Hores, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "minuts", nou.Minuts, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "segons", nou.Segons, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "estat", nou.Actiu, DbType.Boolean);

                        Int32 filesInserides = comanda.ExecuteNonQuery();




                        return filesInserides == 1;
                    }
                }
            }


            return false;
        }

        public static Boolean guardarBlocs(int id, int x, int j, int num_casella)
        {
            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {


                        comanda.CommandText = @"insert into nivell_caselles(id,cX,cY,valor) values(@id_nivell,@x,@y,@num_casella)";

                        DBUtils.afegirParametre(comanda, "id_nivell", id, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "x", x, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "y", j, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "num_casella", num_casella, DbType.Int32);

                        Int32 filesInserides = comanda.ExecuteNonQuery();

                        return filesInserides == 1;
                    }
                }
            }


            return false;
        }



        public static Boolean getNomNivell(string nom)
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
                        comanda.CommandText = @"select nivell_nom from nivell where nivell_nom = @nom";
                        DBUtils.afegirParametre(comanda, "nom", nom, DbType.String);


                        DbDataReader reader = comanda.ExecuteReader();
                        while (reader.Read())
                        {
                            intro_nom = reader.GetString(reader.GetOrdinal("nivell_nom"));

                        }


                        if (intro_nom != null)
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










        public static int getIdLevel(String nom)
        {
            int resultat = 0;
            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {
                        comanda.CommandText = @"select id_nivell from nivell where nivell_nom like @nivell_nom";
                        DBUtils.afegirParametre(comanda, "nivell_nom", nom, DbType.String);

                        DbDataReader reader = comanda.ExecuteReader();
                        while (reader.Read())
                        {

                            resultat = reader.GetInt32(reader.GetOrdinal("id_nivell"));
                        }
                    }
                }
            }
            return resultat;
        }






        //retorna els blocs del nivell i la seva posicio


        public static List<Casella> getBlocsNivell(int id)
        {
            List<Casella> blocs = new List<Casella>();
            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {
                        comanda.CommandText = @"select * from nivell_caselles where id = @id";
                        DBUtils.afegirParametre(comanda, "id", id, DbType.Int32);

                        DbDataReader reader = comanda.ExecuteReader();
                        while (reader.Read())
                        {
                        
                            int cX = reader.GetInt32(reader.GetOrdinal("cX"));
                            int cY = reader.GetInt32(reader.GetOrdinal("cY"));
                            int id_nivell = reader.GetInt32(reader.GetOrdinal("valor"));

                            Casella nova = new Casella(id_nivell,cX,cY);
                            blocs.Add(nova);
                        }
                    }
                }
            }
            return blocs;
        }

        public static Boolean BlocsNivellUpdateBD(int id, int x, int j, int num_casella)
        {
            using (MySQLDbContext context = new MySQLDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var comanda = connection.CreateCommand())
                    {

                        comanda.CommandText = @"update nivell_caselles set valor=@num_casella where id = @id_nivell and cX=@x and cY=@y";

                        DBUtils.afegirParametre(comanda, "id_nivell", id, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "x", x, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "y", j, DbType.Int32);
                        DBUtils.afegirParametre(comanda, "num_casella", num_casella, DbType.Int32);

                        Int32 filesInserides = comanda.ExecuteNonQuery();




                        return filesInserides >= 1;
                    }
                }
            }


            return false;
        }



        public static Boolean DeleteBlocsNivell(int id)
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
                                "delete from nivell_caselles where id=@nivell_id";
                            //-----------------------------------------------
                            //    IMPORTANT !!! posem la comanda dins de la transacció
                            //-----------------------------------------------
                            comanda.Transaction = transaccio; //
                            //-----------------------------------------------
                            DBUtils.afegirParametre(comanda, "nivell_id", id, DbType.Int32);
                            int liniesAfectades = comanda.ExecuteNonQuery();
                            if (liniesAfectades <1)
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


    }



   
}





