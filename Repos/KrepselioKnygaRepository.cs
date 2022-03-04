using System;
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
    public class KrepselioKnygaRepository
    {
        public KrepselioKnyga getKnyga(string id)
        {
            KrepselioKnyga knyga = new KrepselioKnyga();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM `krepselio_knyga` WHERE `fk_KnygaISBN`='" + id + "' and `fk_uzsakymas`=0";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                knyga.id = Convert.ToInt32(item["id"]);
                knyga.kiekis = Convert.ToInt32(item["kiekis"]);
                knyga.fk_KnygaISBN = Convert.ToString(item["fk_KnygaISBN"]);
                knyga.fk_Prekiu_krepselis = Convert.ToInt32(item["fk_Prekiu_krepselis"]);
                knyga.fk_uzsakymas = Convert.ToInt32(item["fk_uzsakymas"]);
            }

            return knyga;
        }
        public bool addKnyga(KrepselioKnyga knyga)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `krepselio_knyga`(`kiekis`, `fk_KnygaISBN`, `fk_Prekiu_krepselis`)
        VALUES (?kiekis,?isbn,?krepselis)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?kiekis", MySqlDbType.Int32).Value = 1;
            mySqlCommand.Parameters.Add("?isbn", MySqlDbType.VarChar).Value = knyga.fk_KnygaISBN;
            mySqlCommand.Parameters.Add("?krepselis", MySqlDbType.Int32).Value = knyga.fk_Prekiu_krepselis;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool updateKnygosKieki(KrepselioKnyga knyga)//update, reikes daryti kiekio didinima
        {
            KrepselioKnyga knygaKiekiui = getKnyga(knyga.fk_KnygaISBN);
            int kiek = knygaKiekiui.kiekis + 1;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `krepselio_knyga` SET 
           `kiekis` = ?kiekis WHERE fk_KnygaISBN=" + knyga.fk_KnygaISBN;
            Console.WriteLine(sqlquery);
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?kiekis", MySqlDbType.Int32).Value = kiek;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }
    }
}