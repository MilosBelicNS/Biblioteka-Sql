namespace Biblioteka.Meni
{
    /* Delegat koji sluzi za metodu koja treba biti izvrsena kada se ova opcija odabere */
    public delegate void FunkcijaOpcije();

    class Opcija
    {
        /* Svaka opcija sadrzi opisni tekst, i metodu koja treba da se izvrsi kada ova opcija bude odabrana */
        public string Tekst { set; get; }
        public FunkcijaOpcije Funkcija { set; get; }

        public Opcija(FunkcijaOpcije funkcija, string tekst)
        {
            Funkcija = funkcija;
            Tekst = tekst;
        }
    }
}
