using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breda_Maps.Model;
using Windows.Devices.Geolocation;
using SQLite;


namespace Breda_Maps.database
{
    class DataBase
    {

        /*
     * Hoofdverantwoordelijke:  Gerjan Holsappel
     * Beschrijving:            de connection met de database
     * Bevat:                   methode aanroepen die de gegevens uitde data base halen
     * Extra:                   
     */

        String dbConnection;

        private TestDatabase tdb;

        public DataBase(String inputFile)
        {
            dbConnection = String.Format("Data Source={0}", inputFile);
           // tdb = new TestDatabase();
        }

        //public List<Sight> getSights()
        //{
        //    return tdb.getAllSights();
        //}

        public List<Route> getRoutes()
        {
            return tdb.getAllRoutes();
        }

        public Sight getsight(int ID)
        {
            return tdb.getSight(ID);
        }

        public Route getRoute(int ID)
        {
            return tdb.getRoute(ID);
        }

        public List<Sight> getSights()
        {

            //DataTable dt = new DataTable();
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                List<Sight> sights = cnn.Query<Sight>("select * from [view]").ToList();
                //cnn.Open();
                //SQLiteCommand mycommand = new SQLiteCommand(cnn);
                //mycommand.CommandText = sql;
                //SQLiteDataReader reader = mycommand.ExecuteReader();
                //dt.Load(reader);
                //reader.Close();
                cnn.Close();
                return sights;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            //return dt;

        }
    }
}
