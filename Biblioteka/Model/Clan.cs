namespace Biblioteka.Model
{
    public class Clan
    {
        /* Atributi */
        public int Id { set; get; }
        public string Ime { set; get; }
        public string Prezime { set; get; }

        /* Konstruktor bez ID-ja. Koristi se kada ne znamo ID clana */
        public Clan(string ime, string prezime)
        {
            Ime = ime;
            Prezime = prezime;
            Id = -1;
        }

        /* Konstruktor sa ID-jem */
        public Clan(int id, string ime, string prezime):this(ime, prezime)
        {
            Id = id;
        }

        /* Obican ToString() koji se moze koristiti za logovanje ili debug-ovanje */
        public override string ToString()
        {
            return string.Format("ID:{0},ime:{1},prezime:{2}", Id, Ime, Prezime);
        }

        /* Slicna metoda kao ToString(), ali ispisuje podatke u tabelarnom formatu */
        public string TabelarnaStringReprezentacija()
        {
            return string.Format("{0,-4} | {1,-15} | {2,-15}", Id, Ime, Prezime);
        }
    }
}
