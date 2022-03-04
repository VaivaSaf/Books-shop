using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using AutoNuoma.Models;
using AutoNuoma.ViewModels;
using MySql.Data.MySqlClient;

namespace AutoNuoma.Repos
{
    public class NaujasKnyguUzsakymasPardavejasRepository
    {
        public List<Nauju_knygu_uzsakymas> getUzsakymai()
        {
            List<Nauju_knygu_uzsakymas> uzsakymai = new List<Nauju_knygu_uzsakymas>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string user = "tomas";
            string sqlquery = "SELECT * FROM `nauju_knygu_uzsakymas` WHERE `fk_Pardavejasprisijungimo_vardas`="+"'"+user+"'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                uzsakymai.Add(new Nauju_knygu_uzsakymas
                {
                    id = Convert.ToInt16(item["uzsakymo_nr"]),
                    knygos_isleidimo_metai_nuo = Convert.ToInt16(item["Knygos_isleidimo_metai_nuo"]),
                    knygos_isleidimo_metai_iki = Convert.ToInt16(item["Knygos_isleidimo_metai_iki"]),
                    kiekis = Convert.ToInt16(item["Kiekis"]),
                    knygos_zanras = Convert.ToInt16(item["Knygos_zanras"]),
                    kalba = Convert.ToInt16(item["Kalba"])
                });
            }
            return uzsakymai;
        }
        public Nauju_knygu_uzsakymas getUzsakymas(int id)
        {
            Nauju_knygu_uzsakymas uzsakymas = new Nauju_knygu_uzsakymas();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from nauju_knygu_uzsakymas where uzsakymo_nr=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                uzsakymas.knygos_isleidimo_metai_nuo = Convert.ToInt16(item["Knygos_isleidimo_metai_nuo"]);
                uzsakymas.knygos_isleidimo_metai_iki = Convert.ToInt16(item["Knygos_isleidimo_metai_iki"]);
                uzsakymas.kiekis = Convert.ToInt16(item["Kiekis"]);
                uzsakymas.knygos_zanras = Convert.ToInt16(item["Knygos_zanras"]);
                uzsakymas.kalba = Convert.ToInt16(item["Knygos_zanras"]);

            }
            return uzsakymas;
        }





        public void deleteUsakymas(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM `nauju_knygu_uzsakymas` WHERE `uzsakymo_nr`='" + id + "'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.AddWithValue("`id`", id);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
        public bool updateKnyga(Nauju_knygu_uzsakymas uzsakymas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `nauju_knygu_uzsakymas` SET 
           `isleidimo_metai_nuo` = ?nuo, `isleidimo_metai_iki`=?iki, `kiekis`=?kiek, `zanras`=?zan, `kalba`=?kal
         WHERE uzsakymo_nr='" + uzsakymas.id + "'";
            Console.WriteLine(sqlquery);
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?nuo", MySqlDbType.VarChar).Value = uzsakymas.knygos_isleidimo_metai_nuo;
            mySqlCommand.Parameters.Add("?iki", MySqlDbType.VarChar).Value = uzsakymas.knygos_isleidimo_metai_iki;
            mySqlCommand.Parameters.Add("?kiek", MySqlDbType.Double).Value = uzsakymas.kiekis;
            mySqlCommand.Parameters.Add("?kal", MySqlDbType.Int32).Value = uzsakymas.kalba;
            mySqlCommand.Parameters.Add("?zan", MySqlDbType.Int32).Value = uzsakymas.knygos_zanras;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }
        
    }
}