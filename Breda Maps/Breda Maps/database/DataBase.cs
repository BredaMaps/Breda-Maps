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
                                            info    VARCHAR(2500)
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

            sights.Add(new Sight("VVV Breda", new Geopoint(new BasicGeoposition() { Latitude = 51.59380, Longitude = 4.77963 }), EnumCat.FACILITY, "VVV-Breda.jpg", "", "", "http://www.vvvbreda.nl/en/3", ""));
            sights.Add(new Sight("Liefdeszuster", new Geopoint(new BasicGeoposition() { Latitude = 51.59307, Longitude = 4.77969 }), EnumCat.CULTURE, "Liefdeszuster.jpg", "", "", "", ""));
            sights.Add(new Sight("Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59250, Longitude = 4.77969 }), EnumCat.PARK, "", "", "", "", @"
U duikt meteen al in de vroege historie van
Breda, als u het pad rechts over het bruggetje
neemt en verder door het stadspark, het
Valkenberg, wandelt.

Tot 1812 deed dit park dienst als kasteeltuin voor de
Heren van Breda. Het park dankt zijn naam aan een val-
kenhuis, dat aan de rand ervan stond en van waaruit de
kasteelbewoners en hun gasten de valkenjacht bedreven.
Het park was van orgine een echt bos met hoge bomen.
In de zeventiende eeuw verdween dat
karakter door de aanleg van een tuin
naar Franse stijl.
Eén replica van de 17 beelden staat
verderop in het park, in een speciaal
aangelegd perkje. Dit perkje is een
herinnering aan de Franse stijltuin.
Het originele beeld is te bezichtigen
in het Breda's Museum.
In 1995 is het Valkenberg in een nieuw
jasje gestoken. In het hele park zijn paden
verlegd, struiken en bosschages weggehaald
en nieuwe verlichting aangebracht. Daarnaast
zijn er nieuwe banken in het park geplaatst en
speelvoorzieningen voor de kinderen gemaakt. Bij opgravingen in
het park zijn de fundamenten van een middeleeuwse muurtoren "+
"en van de stenen muur gevonden, die tot 1537 \"klein Breda\" "+
"omringde. Van twee muurtorens is een klein stukje opgebouwd."));
            sights.Add(new Sight("Nassau Baronie Monument", new Geopoint(new BasicGeoposition() { Latitude = 51.59250, Longitude = 4.77969 }), EnumCat.CULTURE, "NassauBaronie.jpg", "", "", "http://nl.wikipedia.org/wiki/Baroniemonument", @"
Als u de brug overgaat, ziet u aan uw linkerhand het 
Nassau-Baroniemonument.

Bij de ingang van het stadspark, het
Valkenberg, staat een monument
dat u iets vertelt over de wordings-
geschiedenis van de stad Breda, maar
vooral over de Heren van de stad uit het
Huis van Nassau en de 500-jarige band
tussen Breda en het Huis van Nassau.
Op 3 juli 1905 werd het Nassau-Baronie-
monument, zoals het officieel heet, met 
veel feestelijk vertoon door Koningin
Wilhelmina onthuld. Het beeld herinnert
aan het feit, dat in 1404 Graaf Engelbert,
de eerste Bredase Nassau en zijn gemalin, Johanna van Polanen,
werden ingehuldigd als Heer en Vrouwe van Breda.
De ontwerper is de welbekende dr. P.J.H. Cuypers, die o.m. het
Rijksmuseum en het Centraal Station in Amsterdam ontwierp.
Op dit monument zijn niet alleen de wapenschilden van twintig
gemeenten in en rond de Baronie aangebracht maar ook de
Leeuw van Nassau die boven alles uittorent met koningskroon,
zwaard en wapenschild. In de drie reliëfs is de 'blijde incomste'
van Graaf Engelbert en zijn gemalin afgebeeld. De poorters
bieden de sleutel van de stad aan."));
            sights.Add(new Sight("The Light House", new Geopoint(new BasicGeoposition() { Latitude = 51.59256, Longitude = 4.77889 }), EnumCat.CULTURE, "Lighthouse.jpg", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("1e bocht Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59265, Longitude = 4.77844 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("2e bocht Valkenberg", new Geopoint(new BasicGeoposition() { Latitude = 51.59258, Longitude = 4.77806 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Einde park", new Geopoint(new BasicGeoposition() { Latitude = 51.59059, Longitude = 4.77707 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Kasteel van Breda", new Geopoint(new BasicGeoposition() { Latitude = 51.59061, Longitude = 4.77624 }), EnumCat.CULTURE, "KasteelBreda1.jpg", "", "", "http//testtest/henk", @"
Aan het einde van het park
loopt u langs het poortgebouw 
van het kasteel; links kijkt u op het kasteelplein.

Dit plein bevindt zich aan de zuiderzijde
van het kasteel. U heeft zo een mooi
uitzicht op de monumentale poort, de
zgn. Stadhouderspoort met het wapen
van stadhouder Willem de Vijfde, dat
overigens pas later is aangebracht.
Achter u ziet u het ruiterstandbeeld
van stadhouder koning Willem III
geplaatst. Voor dit standbeeld werd
onder de burgerij van Breda een
inzameling gehouden die f 47.000,--
opbracht. Voor de oude Nassaustad
Breda is de stadhouderkoning van
grote betekenis geweest. Hij voltooide
na anderhalve eeuw de verbouwing
van het Kasteel.

Hendrik de Derde van Nassau en zijn
(drede) vrouw Mencia de Mendoza hebben veel in Breda ver-
toefd, maar verbleven dan toch bij voorkeur in de vertrekken
boven de overigens al lang geleden gesloopte watermolen op het
terrein van het Kasteel.

Rechts van de poort bevindt zich het zgn. Blokhuis, de ambts-
woning van de gouveneur van de KMA. Willem van Oranje
woonde daar in een aangrenzende ruimte, maar Prins Maurits
prefereerde de watermolen als dagelijks verblijf.
Prins Philips Willem (1554-1618), de oudste en roomskatholieke
zoon van Willem van Oranje, die vele jaren in Spaanse balling-
schap verbleef en in Diest werd begraven, is de eerste Oranje
geweest, die met zijn vrouw ook in het Kasteel ging wonen.

Hij liet het park Valkenberg verfraaien en het Kasteel echt als
een paleis inrichten. Bij zijn dood in 1618 werden in Breda
tweeënveertig dagen achtereen de kerkklokken geluid.
Op de plaats van de vensters, links en rechts van de poort,
bevonden zich in de zestiende en zeventiende eeuw sierlijke
open galerijen. Het muisgrijze gebouw links van de kasteelpoort
werd gebouwd in 1867. Het maakt thans ook deel uit van het
KMA gebouwen complex.
Meer geschiedenis schuilt et achter de witte pui van het gebouw
"));
            sights.Add(new Sight("Stadhouderspoort", new Geopoint(new BasicGeoposition() { Latitude = 51.58992, Longitude = 4.77634 }), EnumCat.CULTURE, "Stadhouderspoort1.jpg", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("Kruising Kasteelplein/Cingelstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.59033, Longitude = 4.77623 }), EnumCat.ROUTEPOINT, "", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("Huis van Brecht", new Geopoint(new BasicGeoposition() { Latitude = 51.59043, Longitude = 4.77518 }), EnumCat.CULTURE, "HuisvanBrecht1.jpg", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("2e bocht Cingelstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.59000, Longitude = 4.77429 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Spanjaardsgat", new Geopoint(new BasicGeoposition() { Latitude = 51.59010, Longitude = 4.77336 }), EnumCat.CULTURE, "Spanjaardsgat2.jpg", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("Vismarkt", new Geopoint(new BasicGeoposition() { Latitude = 51.58982, Longitude = 4.77321 }), EnumCat.FACILITY, "Vismarkt2.jpg", "", "", "http//testtest/henk", @"
Bij de Vishal gaat u de Vismarktstraat in; deze straat komt uit op de Havermarkt.

Van de plaats waar de vishal (1725) nog aangeeft dat hier vroeger een levendige handel in zeevis werd bedreven, wandelen we door de Vismarktstraat naar de Havermarkt. Deze markt is vanaf de jaren zestig tot op de dag van vandaag het uitgaanscentrum van Breda.
De naam Havermarkt ten spijt moet worden vastgesteld, dat de granen niet hier; maar op de Grote Markt werden verhandeld. Wel werden hier door de boeren uit de omtrek groenten, eieren en boter aangevoerd en verkocht. In de zeventiende eeuw werd hier ook een leermarkt gehouden.
De oude benamingen van dit pleintje luidden Groenmarkt, Botermarkt en Korenmarkt hetgeen de vroegere functies iets beter aanduidt.

Rond 1490 werd dit aangelegd, als een verbreding van de Visserstraat. Wie op dit verrukkelijke pleintje in de zomer op een terrasje een pilsje pakt, ontdekt (overigens ook zonder dat pilsje) al heel gauw, dat dit plein het mooiste uitzicht biedt op de grote toren van de Grote Kerk.
Vanaf deze plek kunt u ook de mooie brede onderbouw van die toren bewonderen.
Er is veel te zien op dit pleintje. Links op de hoek van de Havermarkt en Reigerstraat ziet u het huis 'De Arent', gebouwd rond ca. 1490. Het heeft nu een zeventiende eeuwse trapgevel en is in 1966 geheel gerestaureerd en verbouwd tot restaurant.
Tegenover u op de Havermarkt bevinden zich nog twee panden die uw aandacht verdienen. Havermarkt nummer 5 dateert uit de zestiende en zeventiend eeuw. 'De Vogelstruys' op Havermarkt 21 is een opmerkelijk zeventiende eeuws monument met een hoge, niet symmetrische trapgevel. Ook dit huis heeft in de loop de eeuwen allerlei functies en bestemmingen gehad. Het was onder meer refugiehuis voor de zusters van Catharinadal in de zeventiende eeuw.
Voor het beeldje 'De Troubadour', dat aan het eind van de Havermarkt prijkt, zijn vier exemplaren vervaardigd. De andere drie werden onthuld in Diest, Orange en Dillenburg, de zustersteden van Breda in de in 1963 tot stand gekomen Unie van Oranjesteden.
Een stukje verder in de Visserstraat (nr. 31), aan de rechterkant, staat een fraai bakstenen huis uit de 17e eeuw met overblijfselen van een zestiende eeuwse gothische woning, de Drie Moren geheten.
"));
            sights.Add(new Sight("Havermarkt", new Geopoint(new BasicGeoposition() { Latitude = 51.58932, Longitude = 4.77444 }), EnumCat.FACILITY, "Havermarkt2.jpg", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("Driehoek Kerkplein 1", new Geopoint(new BasicGeoposition() { Latitude = 51.58872, Longitude = 4.77501 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Grote of Onze Lieve Vrouwekerk", new Geopoint(new BasicGeoposition() { Latitude = 51.58878, Longitude = 4.77549 }), EnumCat.CHURCH, "LieveVrouwekerk2.jpg", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("Driehoek Kerkplein 3", new Geopoint(new BasicGeoposition() { Latitude = 51.58864, Longitude = 4.77501 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Het poortje", new Geopoint(new BasicGeoposition() { Latitude = 51.58822, Longitude = 4.77525 }), EnumCat.CULTURE, "Poortje.jpg", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("Ridderstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58716, Longitude = 4.77582 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Grote Markt", new Geopoint(new BasicGeoposition() { Latitude = 51.58747, Longitude = 4.77662 }), EnumCat.FACILITY, "", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("Het Wit Lam", new Geopoint(new BasicGeoposition() { Latitude = 51.58771, Longitude = 4.77652 }), EnumCat.CULTURE, "Witlam.jpg", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("Bevrijdingsmonument", new Geopoint(new BasicGeoposition() { Latitude = 51.58797, Longitude = 4.77638 }), EnumCat.CULTURE, "Bevrijdingsmonument1.jpg", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("Stadshuis", new Geopoint(new BasicGeoposition() { Latitude = 51.58885, Longitude = 4.77616 }), EnumCat.FACILITY, "", "", "", "http//testtest/henk", @"
Schuin aan de overkant op de Grote Markt vindt u het stadhuis van Breda 
en tevens de dependance van de VVV.

De hoofdingang van het standhuis bevindt zich boven het bordes, dat bewaakt 
wordt door twee zandstenen leeuwen. Het stadhuis, zoals dat er nu uitziet, 
kwam pas in 1767 tot stand. Het zijn eigenlijk vier huizen, die toen door de 
bouwmeester van Oranje, Philips Willem Schonck, achter één gevel werden 
verborgen. Het oudste deel is de grote hal. Al in de vijftiende eeuw werd 
daarnaast ook een zgn. 'Cleyn raedthuys' in gebruik genomen. In 1898 kwam 
het meest rechtse huis, het 'Liggend Hert' erbij, dat nog steeds een aparte 
gevel. heeft.

Ondanks het feit dat het stadhuis slechts beperkt geopend is voor het 
publiek, willen we u de volgende informatie over de binnen zijde ervan toch 
niet onthouden.
Vrouwe Justitia boven de hoofdingang en het houten beeld achter in de hal 
geven aan dat in het stadhuis vroeger ook recht gesproken werd.
De balie van de vroegere rechtbank, eens staande tegen de achtermuur van de 
hal, wordt nu in het Breda's Museum bewaard.
Links hangt een grote kopie van het beroemde schilderij van Velasques 'Las 
Lanzas', dat de overgave van Breda aan de Spaanse bevelhebber Spinola (1625) 
in beeld brengt.
Het oorspronkelijke schilderij hangt in het Prado te Madrid.
Het stadhuis wordt nog gebruikt voor openbare raadsvergaderingen en voor het 
sluiten van huwelijken. De burgemeester, wethouders en gemeete-ambtenaren 
huizen sinds februari 1992 in het nieuwe Stadskantoor aan de Claudius 
Prinsenlaan in Breda. Door het poortgebouw rechts van het stadhuis lopen we 
het Stadserf op. Midden op dit pleintje herinnert het beeldje De 
Turfschipper van Gerarda Rueb, aan de legendarische overval in 1590 van 
Adriaan van Bergen met zijn Turfschip. (De VVV Breda verkoopt hiervan een 
replica)."));
            sights.Add(new Sight("Kruising Grote Markt / Stadserf", new Geopoint(new BasicGeoposition() { Latitude = 51.58883, Longitude = 4.77617 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Achterkant stadshuis", new Geopoint(new BasicGeoposition() { Latitude = 51.58889, Longitude = 4.77659 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Kruising Grote Markt / Stadserf (terug gaan)", new Geopoint(new BasicGeoposition() { Latitude = 51.58883, Longitude = 4.77617 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Terug naar begin Grote Markt", new Geopoint(new BasicGeoposition() { Latitude = 51.58747, Longitude = 4.77662 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Antonius van Paduakerk", new Geopoint(new BasicGeoposition() { Latitude = 51.58761, Longitude = 4.77712 }), EnumCat.CHURCH, "", "", "", "http//testtest/henk", @"
U loopt via het poortje weer terug naar de Grote Markt en gaat linksaf. 
De tweede straat links is de St. Janstraat. Hier gaat u in.

Op de hoek van de Halstraat / St. Janstraat staan we voor het gebouw met de 
pilaren: het garnizoenscommando. Vroeger werd het gebouwtje 'hoofdwacht' 
genoemd. Voordat die hoofdwacht daar introk, stond op deze plaats de St. 
Janskapel met daar weer achter de gebouwen van de Ridderorde van St. Jan.
De leden van deze orde verzorgden hier zieken en armen.
De straatnaam herinnert aan deze ridders van St. Jan. in de kapel begon de 
grote stadsbrand van 1534.

De St. Janstraat heeft ooit ook de Veterstraat geheten, waarschijnlijk naar 
de huydvetters of leerlooiers, die in de St. Janstraat hun beroep 
uitoefenden.
Aan uw rechterhand in de St. Janstraat staat de Antonius van Paduakerk, een 
opvallend rijk uitgevoerde waterstaatskerk. Deze kerk is in 1836 gebouwd als 
eerste katholieke kerk na de schuilkerkenperiode, die officieel gebouwd 
mocht worden. In 1853 werd het de eerste bisschopskerk (kathedraal) van het 
nieuwe bisdom in Breda. De zetel van de bisschop van Breda keerde na een 
afwezigheid van 32 jaar aan het begin van 2001 terug naar de Antoniuskerk 
die daarmee de status van kathedraal opnieuw verwierf. Aan de voorgevel zijn 
duidelijk de drie soorten antieke zuilen te zien. Van onder naar boven: 
Dorische, Ionische en Corinthische zuilen. Het beeld boven de ingang stelt 
'de Godsdienst' voor. Binnen valt direct de schitterende houten preekstoel 
op. Hierin is in verschillende panelen het leven van H. Antonius uitgebeeld.
Even verder bevinden zich restanten of alleen maar herinneringen aan twee 
zeer voorname hofhuizen, het Huis Ocrum, waarin tot 1994 de Kunstacademie 
St. Joost was gevestigd en het Huis Hersbeeck, thans pastorie. In 1667 
verbleven in beide huizen de Engelse gezanten, die deelnamen aan de 
Vredesonderhandelingen van Breda. De afgevaardigden van de Raad van State 
gebruikten het pand (nr. 16) wanneer zij in de stad op dienstreis waren, 
maar ook koning Lodewijk Napoleon heeft er gelogeerd. De erker van dit pand 
had een controlerende functie: de pastoor kon goed in de gaten houden wie er 
naar de kerk ging.
Het Huis Ocrum (nr. 18) was van 1848 tot 1952 rooms burgerweeshuis. Dat kunt 
u nog zien aan de kinderkopjes die aan de 19e eeuwse voorgevel van het 
academiegebouw zijn aangebracht. De rode kleur van dit huis (na de 
restauratie aangebracht) zal door de loop der jaren een mooie grijs rode 
kleur van dit huis (na de restauratie aangebracht) zal door de loop der 
jaren een mooie grijs rode kleur krijgen.
"));
            sights.Add(new Sight("Kruising St. Jansstraat / Molenstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58828, Longitude = 4.77858 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Bibliotheek", new Geopoint(new BasicGeoposition() { Latitude = 51.58773, Longitude = 4.77948 }), EnumCat.FACILITY, "", "", "", "http//testtest/henk", @"
Aan het eind van de St. Jansstraat gaat u rechtsaf de Molenstraat in.

In de Molenstraat ziet u recht de Bibliotheek, ontworpen door architect 
Herman Hertzberger. Omdat de Molenstraat vrij smal is, heeft de architect 
ruimte gecreëerd door de bibliotheek schuin oplopende wanden te geven. Op de 
plaats van de bibliotheek (op de hoek met de Oude Vest) bevond zich vroeger 
een poort. Boven deze poort bevond zich 's Heeren Gevangenhuys, waar de 
zwaarst gestraften werden ondergebracht. De plaats is nog met keitjes in het 
asfalt aangegeven; de asfaltweg is de plaats van de oude stadsgracht.
"));
            sights.Add(new Sight("Kruising Molenstraat / Kloosterplein", new Geopoint(new BasicGeoposition() { Latitude = 51.58752, Longitude = 4.77994 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Kloosterkazerne", new Geopoint(new BasicGeoposition() { Latitude = 51.58794, Longitude = 4.78105 }), EnumCat.CULTURE, "", "", "", "http//testtest/henk", @"
Op de hoek Molenstraat en de Oude Vest gaat u linksaf.

Op deze hoek ziet u aan de overkant een voornaam monument uit de 
geschiedenis van Breda: de kloosterkazerne. Het is een deel van het vroegere 
zusterklooster St. Catharinadal, dat hier sinds 1295 gevestigd was. Het 
huidige gebouw dateert uit 1504. In 1645 werden de zusters Norbertinessen 
vanuit Breda naar Oosterhout verdreven.
"));
            sights.Add(new Sight("Chasse Theater", new Geopoint(new BasicGeoposition() { Latitude = 51.58794, Longitude = 4.78218 }), EnumCat.CULTURE, "", "", "", "http//testtest/henk", "geen informatie beschikbaar."));
            sights.Add(new Sight("1e bocht Kloosterplein / Begin Vlaszak", new Geopoint(new BasicGeoposition() { Latitude = 51.58794, Longitude = 4.78105 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Binding van Isaäc", new Geopoint(new BasicGeoposition() { Latitude = 51.58862, Longitude = 4.78079 }), EnumCat.CULTURE, "", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("Einde Vlaszak / Begin Boschstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58955, Longitude = 4.78038 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Beyerd", new Geopoint(new BasicGeoposition() { Latitude = 51.58966, Longitude = 4.78076 }), EnumCat.BAR, "", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("Gasthuispoort", new Geopoint(new BasicGeoposition() { Latitude = 51.58939, Longitude = 4.77982 }), EnumCat.CULTURE, "", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("2e bocht Veemarktstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58905, Longitude = 4.77981 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Kruising St. Annastraat / Veemarktstraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58846, Longitude = 4.77830 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Willem Merkxtuin", new Geopoint(new BasicGeoposition() { Latitude = 51.58905, Longitude = 4.77801 }), EnumCat.PARK, "", "", "", "http//testtest/henk", ""));
            sights.Add(new Sight("Binnen Willem Merkxtuin", new Geopoint(new BasicGeoposition() { Latitude = 51.58918, Longitude = 4.77841 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Uitgang Willem Merkxtuin", new Geopoint(new BasicGeoposition() { Latitude = 51.58905, Longitude = 4.77801 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Kruising Catharinastraat / St. Annastraat", new Geopoint(new BasicGeoposition() { Latitude = 51.58960, Longitude = 4.77770 }), EnumCat.ROUTEPOINT, "", "", "", "", ""));
            sights.Add(new Sight("Begijnenhof", new Geopoint(new BasicGeoposition() { Latitude = 51.58965, Longitude = 4.77830 }), EnumCat.PARK, "", "", "", "http//testtest/henk", ""));
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
