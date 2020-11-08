namespace Biblioteka.Model
{
    public class Knjiga
    {
        /* Atributi */
        public int Id { set; get; }
        public string Naziv { set; get; }
        public string Autor { set; get; }
        public int Godina { set; get; }

        /* Atribut TrentniClan predstavlja vezu jedan na vise (asocijaciju)*/
        public Clan TrenutniClan { set; get; }

        /* Konstruktor sa ID-jem */
        public Knjiga(int id, string naziv, string autor, int godina, Clan trenutniClan = null)
        {
            Id = id;
            Naziv = naziv;
            Autor = autor;
            Godina = godina;
            TrenutniClan = trenutniClan;
        }

        /* Konstruktor bez ID-ja. Koristi se kada ne znamo ID knjige */
        public Knjiga(string naziv, string autor, int godina, Clan trenutniClan = null)
        {
            Naziv = naziv;
            Autor = autor;
            Godina = godina;
            TrenutniClan = trenutniClan;
        }

        /* Obican ToString() koji se moze koristiti za logovanje ili debug-ovanje */
        public override string ToString()
        {
            return string.Format("ID:{0},naziv:{1},autor:{2},godina:{3},trenutni clan:{4}", Id, Naziv, Autor, Godina, TrenutniClan);
        }

        /* Slicna metoda kao ToString(), ali ispisuje podatke u tabelarnom formatu - ukratko */
        public string StringReprezentacijaUkratko()
        {
            string stanje = (TrenutniClan == null) ? "dostupna" : "zauzeta";
            return string.Format("{0,-30} | {1,-10}", Naziv, stanje);
        }

        /* Slicna metoda kao ToString(), ali ispisuje podatke u tabelarnom formatu - detaljno */
        public string StringReprezentacijaDetaljno()
        {
            string trenutnoKod = (TrenutniClan == null) ? "dostupna" : (TrenutniClan.Ime + " " + TrenutniClan.Prezime);
            return string.Format("{0, 3} | {1,-30} | {2,-20} | {3,-6} | {4,-20}", Id, Naziv, Autor, (Godina == -1) ? "-" : Godina.ToString(), trenutnoKod);
        }
    }
}
