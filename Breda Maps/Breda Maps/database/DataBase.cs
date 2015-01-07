using System;
using System.Collections.Generic;
using System.Linq;
using Breda_Maps.Model;
using Windows.Devices.Geolocation;
using SQLite;
using Breda_Maps.Controller.Enums;
using System.IO;
using Windows.Storage;


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

        public static string path = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "database"));
        public DataBase(String inputFile)
        {
            //dbConnection = String.Format("Data Source={0}", inputFile);
            string path = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, inputFile));
            dbConnection = String.Format("Data Source={0}", path);



            init();
        }

       

        public List<Sight> getSights()
        {
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);


                List<Sight> sights = cnn.Query<Sight>(
                    @"SELECT * FROM sight"
                    ).ToList();

                cnn.Close();
                return sights;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string getDescription(int id)
        {
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);

                string query = "SELECT description FROM sight WHERE Id = '"+id+"'";
                List<Sight> sights = cnn.Query<Sight>(query);
                string description = sights[0]._description;

                cnn.Close();
                return description;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void init()
        {
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                //cnn.DropTable<Sight>();
                cnn.Query<Sight>(@"CREATE TABLE IF NOT EXISTS
                                sight (Id      INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            description    VARCHAR( 140 ),
                                            latitude    REAL,
                                            longitude    REAL,
                                            category    VARCHAR( 140 ),
                                            site VARCHAR( 140 ),
                                            media    VARCHAR( 140 )
                            );");

                cnn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            fillDataBase();
        }

        private void fillDataBase()
        {
            List<Sight> sights = new List<Sight>();

            sights.Add(new Sight("VVV Breda", new Geopoint(new BasicGeoposition() { Latitude = 51.59380, Longitude = 4.77963 }), EnumCat.CULTURE));
            sights.Add(new Sight("Liefdeszuster", new Geopoint(new BasicGeoposition() { Latitude = 51.59307, Longitude = 4.77969 }), EnumCat.CULTURE));
            sights.Add(new Sight("Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59250, Longitude = 4.77969 }), EnumCat.CULTURE));
            sights.Add(new Sight("Nassau Baronie Monument", new Geopoint(new BasicGeoposition() { Latitude = 51.59250, Longitude = 4.77969 }), EnumCat.CULTURE));
            sights.Add(new Sight("The Light House", new Geopoint(new BasicGeoposition() { Latitude = 51.59256, Longitude = 4.77889 }), EnumCat.CULTURE));
            sights.Add(new Sight("1e bocht Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59265, Longitude = 4.77844 }), EnumCat.ROUTEPOINT));
            sights.Add(new Sight("2e bocht Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59258, Longitude = 4.77806 }), EnumCat.ROUTEPOINT));
            sights.Add(new Sight("Einde park", new Geopoint(new BasicGeoposition() { Latitude = 51.59059, Longitude = 4.77707 }), EnumCat.PARK));

            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                

                string query = "INSERT INTO sight (description,latitude,longitude,category,site,media) VALUES ("
                                         + "'henk'" + "," + 12 + "," + 14 + ",'" + "test" + "'," + 6 + "," + 7 + ");";

               // cnn.Query<Sight>(query);
                //cnn.Insert(new Sight("The Light House", new Geopoint(new BasicGeoposition() { Latitude = 51.59256, Longitude = 4.77889 }), EnumCat.CULTURE));
                foreach (Sight sight in sights)
                    cnn.Insert(sight);
                
                cnn.Commit();
                cnn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
