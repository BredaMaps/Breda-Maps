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
            dbConnection = String.Format("Data Source={0}", inputFile);
            string path = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, inputFile));
           // dbConnection = String.Format("Data Source={0}", path);

        }

        public DataBase(string inputFile, bool create)
        {
            dbConnection = String.Format("Data Source={0}", inputFile);
            if (create)
            {
                init();
            }
        }

       

        public List<Sight> getSights()
        {
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                List<Sight> sights = cnn.Query<Sight>(
                    @"SELECT * FROM sight"
                    );

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
            List<Sight> sights = getById(id);
                string description = sights[0]._description;

                return description;
        }

        public string[] getImages(int id)
        {
            List<Sight> sights = getById(id);
                string[] image = new string[1];
                if (sights[0]._image == null) { image[0] = ""; }
                else {
                    image = sights[0]._image.Split(';');
                }

                return image;
        }

        public string[] getVideo(int id)
        {
            List<Sight> sights = getById(id);
                string[] video = new string[1];
                if (sights[0]._video == null) { video[0] = ""; }
                else
                {
                    video = sights[0]._image.Split(';');
                }
                return video;
        }

        public string[] getsound(int id)
        {
            List<Sight> sights = getById(id);
                string[] sound = new string[1];
                if (sights[0]._sound == null) { sound[0] = ""; }
                else
                {
                    sound = sights[0]._sound.Split(';');
                }

                return sound;
        }

        public string getInfo(int id)
        {
            List<Sight> sights = getById(id);
                string info = sights[0]._info;

                return info;
        }

        public string getSite(int id)
        {
                List<Sight> sights = getById(id);
                string site = sights[0]._site;

                return site;
        }

        private List<Sight> getById(int id)
        {
            List<Sight> sights = getQueryWhere("id = '" + id + "'");
            return sights;
        }

        public int getIdByDescription(string description)
        {
            Sight sight = getIdByQuery("description = '" + description + "'")[0];
            return sight.Id;
        }

        public int getIdByDescription()
        {
            return getIdByDescription("");
        }

        private List<Sight> getIdByQuery(string query)
        {
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);

                string totalquery = "SELECT id FROM sight WHERE " + query;
                List<Sight> sights = cnn.Query<Sight>(totalquery);

                cnn.Close();
                return sights;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }  //*/

        private List<Sight> getQueryWhere(string query)
        {
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);

                string totalquery = "SELECT * FROM sight WHERE "+query;
                List<Sight> sights = cnn.Query<Sight>(totalquery);

                cnn.Close();
                return sights;
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
                cnn.DropTable<Sight>();
                cnn.Query<Sight>(@"CREATE TABLE IF NOT EXISTS
                                sight (Id      INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            description    VARCHAR( 140 ),
                                            latitude    REAL,
                                            longitude    REAL,
                                            category    VARCHAR( 140 ),
                                            site VARCHAR( 140 ),
                                            image    VARCHAR( 140 ),
                                            video    VARCHAR( 140 ),
                                            sound     VARCHAR( 140 ),
                                            info    VARCHAR(140)
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

            sights.Add(new Sight("VVV Breda", new Geopoint(new BasicGeoposition() { Latitude = 51.59380, Longitude = 4.77963 }), EnumCat.FACILITY,"VVV-Breda.jpg","","","http//testtest/henk","info"));
            sights.Add(new Sight("Liefdeszuster", new Geopoint(new BasicGeoposition() { Latitude = 51.59307, Longitude = 4.77969 }), EnumCat.CULTURE));
            sights.Add(new Sight("Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59250, Longitude = 4.77969 }), EnumCat.PARK));
            sights.Add(new Sight("Nassau Baronie Monument", new Geopoint(new BasicGeoposition() { Latitude = 51.59250, Longitude = 4.77969 }), EnumCat.CHURCH));
            sights.Add(new Sight("The Light House", new Geopoint(new BasicGeoposition() { Latitude = 51.59256, Longitude = 4.77889 }), EnumCat.BAR));
            sights.Add(new Sight("1e bocht Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59265, Longitude = 4.77844 }), EnumCat.ROUTEPOINT));
            sights.Add(new Sight("2e bocht Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59258, Longitude = 4.77806 }), EnumCat.ROUTEPOINT));
            sights.Add(new Sight("Einde park", new Geopoint(new BasicGeoposition() { Latitude = 51.59059, Longitude = 4.77707 }), EnumCat.PARK));

            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                
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
