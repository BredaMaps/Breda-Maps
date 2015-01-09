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
            sights.Add(new Sight("Liefdeszuster", new Geopoint(new BasicGeoposition() { Latitude = 51.59307, Longitude = 4.77969 }), EnumCat.CULTURE, "Liefdeszuster.jpg", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59250, Longitude = 4.77969 }), EnumCat.PARK, "", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Nassau Baronie Monument", new Geopoint(new BasicGeoposition() { Latitude = 51.59250, Longitude = 4.77969 }), EnumCat.CULTURE, "NassauBaronie.jpg", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("The Light House", new Geopoint(new BasicGeoposition() { Latitude = 51.59256, Longitude = 4.77889 }), EnumCat.CULTURE, "Lighthouse.jpg", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("1e bocht Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59265, Longitude = 4.77844 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("2e bocht Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59258, Longitude = 4.77806 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Einde park", new Geopoint(new BasicGeoposition() { Latitude = 51.59059, Longitude = 4.77707 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Kasteel van Breda", new Geopoint(new BasicGeoposition() { Latitude = 51.59061, Longitude = 4.77624 }), EnumCat.CULTURE, "KasteelBreda1.jpg", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Stadhouderspoort", new Geopoint(new BasicGeoposition() { Latitude = 51.58992, Longitude = 4.77634 }), EnumCat.CULTURE, "Stadhouderspoort1.jpg", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Kruising Kasteelplein/Cingelstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.59033, Longitude = 4.77623 }), EnumCat.ROUTEPOINT, "", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Huis van Brecht", new Geopoint(new BasicGeoposition() { Latitude = 51.59043, Longitude = 4.77518 }), EnumCat.CULTURE, "HuisvanBrecht1.jpg", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("2e bocht Cingelstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.59000, Longitude = 4.77429 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Spanjaardsgat", new Geopoint(new BasicGeoposition() { Latitude = 51.59010, Longitude = 4.77336 }), EnumCat.CULTURE, "Spanjaardsgat2.jpg", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Vismarkt", new Geopoint(new BasicGeoposition() { Latitude = 51.58982, Longitude = 4.77321 }), EnumCat.FACILITY, "Vismarkt2.jpg", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Havermarkt", new Geopoint(new BasicGeoposition() { Latitude = 51.58932, Longitude = 4.77444 }), EnumCat.FACILITY, "Havermarkt2.jpg", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Driehoek Kerkplein 1", new Geopoint(new BasicGeoposition() { Latitude = 51.58872, Longitude = 4.77501 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Grote of Onze Lieve Vrouwekerk", new Geopoint(new BasicGeoposition() { Latitude = 51.58878, Longitude = 4.77549 }), EnumCat.CHURCH, "LieveVrouwekerk2.jpg", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Driehoek Kerkplein 3", new Geopoint(new BasicGeoposition() { Latitude = 51.58864, Longitude = 4.77501 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Het poortje", new Geopoint(new BasicGeoposition() { Latitude = 51.58822, Longitude = 4.77525 }), EnumCat.CULTURE, "Poortje.jpg", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Ridderstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58716, Longitude = 4.77582 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Grote Markt", new Geopoint(new BasicGeoposition() { Latitude = 51.58747, Longitude = 4.77662 }), EnumCat.FACILITY, "", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Het Wit Lam", new Geopoint(new BasicGeoposition() { Latitude = 51.58771, Longitude = 4.77652 }), EnumCat.CULTURE, "Witlam.jpg", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Bevrijdingsmonument", new Geopoint(new BasicGeoposition() { Latitude = 51.58797, Longitude = 4.77638 }), EnumCat.CULTURE, "Bevrijdingsmonument1.jpg", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Stadshuis", new Geopoint(new BasicGeoposition() { Latitude = 51.58885, Longitude = 4.77616 }), EnumCat.FACILITY, "", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Kruising Grote Markt / Stadserf", new Geopoint(new BasicGeoposition() { Latitude = 51.58883, Longitude = 4.77617 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Achterkant stadshuis", new Geopoint(new BasicGeoposition() { Latitude = 51.58889, Longitude = 4.77659 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Kruising Grote Markt / Stadserf (terug gaan)", new Geopoint(new BasicGeoposition() { Latitude = 51.58883, Longitude = 4.77617 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Terug naar begin Grote Markt", new Geopoint(new BasicGeoposition() { Latitude = 51.58747, Longitude = 4.77662 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Antonius van Paduakerk", new Geopoint(new BasicGeoposition() { Latitude = 51.58761, Longitude = 4.77712 }), EnumCat.CHURCH, "", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Kruising St. Jansstraat / Molenstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58828, Longitude = 4.77858 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Bibliotheek", new Geopoint(new BasicGeoposition() { Latitude = 51.58773, Longitude = 4.77948 }), EnumCat.FACILITY, "", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Kruising Molenstraat / Kloosterplein", new Geopoint(new BasicGeoposition() { Latitude = 51.58752, Longitude = 4.77994 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Kloosterkazerne", new Geopoint(new BasicGeoposition() { Latitude = 51.58794, Longitude = 4.78105 }), EnumCat.CULTURE, "", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Chasse Theater", new Geopoint(new BasicGeoposition() { Latitude = 51.58794, Longitude = 4.78218 }), EnumCat.CULTURE, "", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("1e bocht Kloosterplein / Begin Vlaszak", new Geopoint(new BasicGeoposition() { Latitude = 51.58794, Longitude = 4.78105 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Binding van Isaäc", new Geopoint(new BasicGeoposition() { Latitude = 51.58862, Longitude = 4.78079 }), EnumCat.CULTURE, "", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Einde Vlaszak / Begin Boschstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58955, Longitude = 4.78038 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Beyerd", new Geopoint(new BasicGeoposition() { Latitude = 51.58966, Longitude = 4.78076 }), EnumCat.BAR, "", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Gasthuispoort", new Geopoint(new BasicGeoposition() { Latitude = 51.58939, Longitude = 4.77982 }), EnumCat.CULTURE, "", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("2e bocht Veemarktstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58905, Longitude = 4.77981 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Kruising St. Annastraat / Veemarktstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58846, Longitude = 4.77830 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Willem Merkxtuin", new Geopoint(new BasicGeoposition() { Latitude = 51.58905, Longitude = 4.77801 }), EnumCat.PARK, "", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Binnen Willem Merkxtuin", new Geopoint(new BasicGeoposition() { Latitude = 51.58918, Longitude = 4.77841 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Uitgang Willem Merkxtuin", new Geopoint(new BasicGeoposition() { Latitude = 51.58905, Longitude = 4.77801 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Kruising Catharinastraat / St. Annastraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58960, Longitude = 4.77770 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Begijnenhof", new Geopoint(new BasicGeoposition() { Latitude = 51.58965, Longitude = 4.77830 }), EnumCat.PARK, "", "", "", "http//testtest/henk", "info"));
            sights.Add(new Sight("Binnen Begijnenhof", new Geopoint(new BasicGeoposition() { Latitude = 51.58997, Longitude = 4.77810 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Uitgang Begijnenhof", new Geopoint(new BasicGeoposition() { Latitude = 51.58965, Longitude = 4.77830 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Eindpunt stadswandeling", new Geopoint(new BasicGeoposition() { Latitude = 51.58950, Longitude = 4.77649 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));

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
