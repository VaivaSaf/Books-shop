﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using AutoNuoma.ViewModels;
using AutoNuoma.Models;
using MySql.Data.MySqlClient;

namespace AutoNuoma.Repos
{
    public class KnygaRepository
    {
        public List<KnygaViewModel> GetKnyga1()
        {
            List<KnygaViewModel> knygaViewModels = new List<KnygaViewModel>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM `knyga`";
          //  string sqlquery = @"SELECT * FROM `knyga ` WHERE `iskeliama_saraso_pradzia ` = `1 ` ";
            //string sqlquery = @"SELECT * FROM `administratorius` ";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                knygaViewModels.Add(new KnygaViewModel
                {
                    pavadinimas = Convert.ToString(item["pavadinimas"]),
                    autorius = Convert.ToString(item["autorius"]),
                    kaina = Convert.ToDouble(item["kaina"]),
                    ISBN = Convert.ToString(item["ISBN"]),
                    isleidimo_metai = Convert.ToInt32(item["isleidimo_metai"]),
                    puslapiu_skaicius= Convert.ToInt32(item["puslapiu_skaicius"]),
                    leidykla = Convert.ToString(item["leidykla"]),
                    kiekis = Convert.ToInt32(item["kiekis"]),
                    //iskeliama_saraso_pradzia = Convert.ToInt32(item["iskeliama_saraso_pradzia"]),
                    kalba = Convert.ToString(item["kalba"]),
                    zanras = Convert.ToString(item["zanras"])
                    
                });
            }

            /*string conn1 = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection1 = new MySqlConnection(conn1);
            string sqlquery1 = @"SELECT * FROM `knyga` WHERE `iskeliama_saraso_pradzia` = '0' ";
            //string sqlquery = @"SELECT * FROM `administratorius` ";
            MySqlCommand mySqlCommand1 = new MySqlCommand(sqlquery1, mySqlConnection1);
            mySqlConnection1.Open();
            MySqlDataAdapter mda1 = new MySqlDataAdapter(mySqlCommand1);
            DataTable dt1 = new DataTable();
            mda1.Fill(dt);
            mySqlConnection1.Close();

            foreach (DataRow item in dt.Rows)
            {
                knygaViewModels.Add(new KnygaViewModel
                {
                    pavadinimas = Convert.ToString(item["pavadinimas"]),
                    autorius = Convert.ToString(item["autorius"]),
                    kaina = Convert.ToDouble(item["kaina"]),
                    ISBN = Convert.ToString(item["ISBN"]),
                    isleidimo_metai = Convert.ToInt32(item["isleidimo_metai"]),
                    puslapiu_skaicius = Convert.ToInt32(item["puslapiu_skaicius"]),
                    leidykla = Convert.ToString(item["leidykla"]),
                    kiekis = Convert.ToInt32(item["kiekis"]),
                    //      iskeliama_saraso_pradzia = Convert.ToInt32(item["iskeliama_saraso_pradzia"]),
                    kalba = Convert.ToString(item["kalba"]),
                    zanras = Convert.ToString(item["zanras"])

                });
            }*/



            return knygaViewModels;
        }
        public List<KnygaViewModel> GetKnygaP(string id)
        {
            List<KnygaViewModel> knygaViewModels = new List<KnygaViewModel>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM `knyga` WHERE `fk_Pardavejasprisijungimo_vardas`='" + id + "'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                knygaViewModels.Add(new KnygaViewModel
                {
                    pavadinimas = Convert.ToString(item["pavadinimas"]),
                    autorius = Convert.ToString(item["autorius"]),
                    kaina = Convert.ToDouble(item["kaina"]),
                    ISBN = Convert.ToString(item["ISBN"]),
                    isleidimo_metai = Convert.ToInt32(item["isleidimo_metai"]),
                    puslapiu_skaicius = Convert.ToInt32(item["puslapiu_skaicius"]),
                    leidykla = Convert.ToString(item["leidykla"]),
                    kiekis = Convert.ToInt32(item["kiekis"]),
                    //      iskeliama_saraso_pradzia = Convert.ToInt32(item["iskeliama_saraso_pradzia"]),
                    kalba = Convert.ToString(item["kalba"]),
                    zanras = Convert.ToString(item["zanras"])

                });
            }

            return knygaViewModels;
        }
        public KnygaEditViewModel GetKnyga(string id)
        {
            KnygaEditViewModel knyga = new KnygaEditViewModel();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM `knyga` WHERE ISBN='" + id+"'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                knyga.pavadinimas = Convert.ToString(item["pavadinimas"]);
                knyga.autorius = Convert.ToString(item["autorius"]);
                knyga.kaina = Convert.ToDouble(item["kaina"]);
                knyga.ISBN = Convert.ToString(item["ISBN"]);
                knyga.isleidimo_metai = Convert.ToInt32(item["isleidimo_metai"]);
                knyga.puslapiu_skaicius = Convert.ToInt32(item["puslapiu_skaicius"]);
                knyga.leidykla = Convert.ToString(item["leidykla"]);
                knyga.kiekis = Convert.ToInt32(item["kiekis"]);
                //knyga.kalba = Convert.ToInt32(item["kalba"]);
                //knyga.zanras = Convert.ToInt32(item["zanras"]);
            }

            return knyga;
        }
        public bool updateKnygaP(KnygaEditViewModel knyga, string user)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `knyga` SET 
           `pavadinimas` = ?pav, `autorius`=?aut, `kaina`=?kai, `ISBN`=?isbn, `isleidimo_metai`=?isl, `puslapiu_skaicius`=?psl, `leidykla`=?leid,
        `kiekis`=?kiek, `iskeliama_saraso_pradzia`=?isk, `kalba`=?kal, `zanras`=?zan, `fk_Administratoriusprisijungimo_vardas`=NULL, `fk_Pardavejasprisijungimo_vardas`=?use
         WHERE ISBN='" + knyga.ISBN + "'";
            Console.WriteLine(sqlquery);
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.VarChar).Value = knyga.pavadinimas;
            mySqlCommand.Parameters.Add("?aut", MySqlDbType.VarChar).Value = knyga.autorius;
            mySqlCommand.Parameters.Add("?kai", MySqlDbType.Double).Value = knyga.kaina;
            mySqlCommand.Parameters.Add("?isbn", MySqlDbType.VarChar).Value = knyga.ISBN;
            mySqlCommand.Parameters.Add("?isl", MySqlDbType.Int32).Value = knyga.isleidimo_metai;
            mySqlCommand.Parameters.Add("?psl", MySqlDbType.Int32).Value = knyga.puslapiu_skaicius;
            mySqlCommand.Parameters.Add("?leid", MySqlDbType.VarChar).Value = knyga.leidykla;
            mySqlCommand.Parameters.Add("?kiek", MySqlDbType.Int32).Value = knyga.kiekis;
            mySqlCommand.Parameters.Add("?isk", MySqlDbType.Int32).Value = knyga.iskeliama_saraso_pradzia;
            mySqlCommand.Parameters.Add("?kal", MySqlDbType.Int32).Value = knyga.kalba;
            mySqlCommand.Parameters.Add("?zan", MySqlDbType.Int32).Value = knyga.zanras;
            mySqlCommand.Parameters.Add("?use", MySqlDbType.VarChar).Value = user;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool updateKnyga(KnygaEditViewModel knyga)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `knyga` SET 
           `pavadinimas` = ?pav, `autorius`=?aut, `kaina`=?kai, `ISBN`=?isbn, `isleidimo_metai`=?isl, `puslapiu_skaicius`=?psl, `leidykla`=?leid,
        `kiekis`=?kiek, `iskeliama_saraso_pradzia`=?isk, `kalba`=?kal, `zanras`=?zan, `fk_Administratoriusprisijungimo_vardas`=NULL, `fk_Pardavejasprisijungimo_vardas`=NULL
         WHERE ISBN='" + knyga.ISBN+"'";
            Console.WriteLine(sqlquery);
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.VarChar).Value = knyga.pavadinimas;
            mySqlCommand.Parameters.Add("?aut", MySqlDbType.VarChar).Value = knyga.autorius;
            mySqlCommand.Parameters.Add("?kai", MySqlDbType.Double).Value = knyga.kaina;
            mySqlCommand.Parameters.Add("?isbn", MySqlDbType.VarChar).Value = knyga.ISBN;
            mySqlCommand.Parameters.Add("?isl", MySqlDbType.Int32).Value = knyga.isleidimo_metai;
            mySqlCommand.Parameters.Add("?psl", MySqlDbType.Int32).Value = knyga.puslapiu_skaicius;
            mySqlCommand.Parameters.Add("?leid", MySqlDbType.VarChar).Value = knyga.leidykla;
            mySqlCommand.Parameters.Add("?kiek", MySqlDbType.Int32).Value = knyga.kiekis;
            mySqlCommand.Parameters.Add("?isk", MySqlDbType.Int32).Value = knyga.iskeliama_saraso_pradzia;
            mySqlCommand.Parameters.Add("?kal", MySqlDbType.Int32).Value = knyga.kalba;
            mySqlCommand.Parameters.Add("?zan", MySqlDbType.Int32).Value = knyga.zanras;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }
        public bool addKnygaP(KnygaEditViewModel knyga, int kiekis, int kalba, int zanras, string user)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `knyga`(`pavadinimas`, `autorius`, `kaina`, `ISBN`, `isleidimo_metai`, `puslapiu_skaicius`, `leidykla`, `kiekis`, `iskeliama_saraso_pradzia`, `kalba`, `zanras`, `fk_Administratoriusprisijungimo_vardas`, `fk_Pardavejasprisijungimo_vardas`)
        VALUES (?pav,?aut,?kai,?isbn,?isl,?psl,?leid,?kiek,?isk,?kal,?zan,NULL,?use)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.VarChar).Value = knyga.pavadinimas;
            mySqlCommand.Parameters.Add("?aut", MySqlDbType.VarChar).Value = knyga.autorius;
            mySqlCommand.Parameters.Add("?kai", MySqlDbType.Double).Value = knyga.kaina;
            mySqlCommand.Parameters.Add("?isbn", MySqlDbType.VarChar).Value = knyga.ISBN;
            mySqlCommand.Parameters.Add("?isl", MySqlDbType.Int32).Value = knyga.isleidimo_metai;
            mySqlCommand.Parameters.Add("?psl", MySqlDbType.Int32).Value = knyga.puslapiu_skaicius;
            mySqlCommand.Parameters.Add("?leid", MySqlDbType.VarChar).Value = knyga.leidykla;
            mySqlCommand.Parameters.Add("?kiek", MySqlDbType.Int32).Value = kiekis;// knyga.kiekis;
            mySqlCommand.Parameters.Add("?isk", MySqlDbType.Int32).Value = knyga.iskeliama_saraso_pradzia;
            mySqlCommand.Parameters.Add("?kal", MySqlDbType.Int32).Value = kalba;// knyga.kalba;
            mySqlCommand.Parameters.Add("?zan", MySqlDbType.Int32).Value = zanras;// knyga.zanras;
            mySqlCommand.Parameters.Add("?use", MySqlDbType.VarChar).Value = user;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }
        public bool addKnyga1(KnygaEditViewModel knyga, string user)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `knyga`(`pavadinimas`, `autorius`, `kaina`, `ISBN`, `isleidimo_metai`, `puslapiu_skaicius`, `leidykla`, `kiekis`, `iskeliama_saraso_pradzia`, `kalba`, `zanras`, `fk_Administratoriusprisijungimo_vardas`, `fk_Pardavejasprisijungimo_vardas`)
        VALUES (?pav,?aut,?kai,?isbn,?isl,?psl,?leid,?kiek,?isk,?kal,?zan,NULL,?use)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.VarChar).Value = knyga.pavadinimas;
            mySqlCommand.Parameters.Add("?aut", MySqlDbType.VarChar).Value = knyga.autorius;
            mySqlCommand.Parameters.Add("?kai", MySqlDbType.Double).Value = knyga.kaina;
            mySqlCommand.Parameters.Add("?isbn", MySqlDbType.VarChar).Value = knyga.ISBN;
            mySqlCommand.Parameters.Add("?isl", MySqlDbType.Int32).Value = knyga.isleidimo_metai;
            mySqlCommand.Parameters.Add("?psl", MySqlDbType.Int32).Value = knyga.puslapiu_skaicius;
            mySqlCommand.Parameters.Add("?leid", MySqlDbType.VarChar).Value = knyga.leidykla;
            mySqlCommand.Parameters.Add("?kiek", MySqlDbType.Int32).Value = knyga.kiekis;
            mySqlCommand.Parameters.Add("?isk", MySqlDbType.Int32).Value = knyga.iskeliama_saraso_pradzia;
            mySqlCommand.Parameters.Add("?kal", MySqlDbType.Int32).Value = knyga.kalba;
            mySqlCommand.Parameters.Add("?zan", MySqlDbType.Int32).Value = knyga.zanras;
            mySqlCommand.Parameters.Add("?use", MySqlDbType.VarChar).Value = user;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }
        public bool addKnyga(KnygaEditViewModel knyga)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `knyga`(`pavadinimas`, `autorius`, `kaina`, `ISBN`, `isleidimo_metai`, `puslapiu_skaicius`, `leidykla`, `kiekis`, `iskeliama_saraso_pradzia`, `kalba`, `zanras`, `fk_Administratoriusprisijungimo_vardas`, `fk_Pardavejasprisijungimo_vardas`)
        VALUES (?pav,?aut,?kai,?isbn,?isl,?psl,?leid,?kiek,?isk,?kal,?zan,NULL,NULL)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.VarChar).Value = knyga.pavadinimas;
            mySqlCommand.Parameters.Add("?aut", MySqlDbType.VarChar).Value = knyga.autorius;
            mySqlCommand.Parameters.Add("?kai", MySqlDbType.Double).Value = knyga.kaina;
            mySqlCommand.Parameters.Add("?isbn", MySqlDbType.VarChar).Value = knyga.ISBN;
            mySqlCommand.Parameters.Add("?isl", MySqlDbType.Int32).Value = knyga.isleidimo_metai;
            mySqlCommand.Parameters.Add("?psl", MySqlDbType.Int32).Value = knyga.puslapiu_skaicius;
            mySqlCommand.Parameters.Add("?leid", MySqlDbType.VarChar).Value = knyga.leidykla;
            mySqlCommand.Parameters.Add("?kiek", MySqlDbType.Int32).Value = knyga.kiekis;
            mySqlCommand.Parameters.Add("?isk", MySqlDbType.Int32).Value = knyga.iskeliama_saraso_pradzia;
            mySqlCommand.Parameters.Add("?kal", MySqlDbType.Int32).Value = knyga.kalba;
            mySqlCommand.Parameters.Add("?zan", MySqlDbType.Int32).Value = knyga.zanras;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        

        public void deleteKnyga(string id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM `knyga` WHERE `ISBN`='" + id+"'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.AddWithValue("`ISBN`", id);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
        public bool upKnyga(string id, int value)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `knyga` SET 
           `iskeliama_saraso_pradzia` = ?val WHERE ISBN='" + id + "'";
            Console.WriteLine(sqlquery);
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?val", MySqlDbType.Int32).Value = value;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }
        /*public void urpKnyga(string id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `knyga` SET 
           `iskeliama_saraso_pradzia` = 1 WHERE `ISBN`='" + id + "'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.AddWithValue("`ISBN`", id);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }*/

    }
}