using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using AutoNuoma.Models;
using MySql.Data.MySqlClient;

namespace AutoNuoma.Repos
{
    public class ParduotaKnygaRepository
    {
        public List<ParduotaKnyga> GetParduotosKnygos(string user)
        {
            List<ParduotaKnyga> knyga = new List<ParduotaKnyga>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            // string user = "tomas";
            string sqlquery = @"SELECT a.parduotu_knygu_kiekis, a.id_Parduota_knyga, a.fk_KnygaISBN, b.pavadinimas as pavad, b.autorius as aut, 
            b.fk_Pardavejasprisijungimo_vardas AS par                      
            FROM parduota_knyga a
            LEFT JOIN knyga b ON b.ISBN = a.fk_KnygaISBN
            WHERE  b.fk_Pardavejasprisijungimo_vardas=" + "'" + user + "'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                knyga.Add(new ParduotaKnyga
                {
                    kiekis = Convert.ToInt32(item["parduotu_knygu_kiekis"]),
                    id = Convert.ToString(item["id_Parduota_knyga"]),
                    isbn = Convert.ToString(item["fk_KnygaISBN"]),
                    pavadinimas = Convert.ToString(item["pavad"]),
                    autorius = Convert.ToString(item["aut"]),
                    pardavejas = Convert.ToString(item["par"])
                });
            }
            return knyga;
        }
    }
}