using System;
using System.Collections.Generic;

/* U ovoj datoteci smo definisali klase koje olaksavaju rad sa korisnickim menijem. */

namespace Biblioteka.Meni
{
    class Meni
    {
        /* Meni u sebi sadrzi proizvoljan broj opcija. Najjednostavnije je da ih cuvamo u listi. */
        public List<Opcija> listaOpcija;

        public Meni()
        {
            /* Kada konstruisemo novi meni, inicijalizujemo praznu listu opcija */
            listaOpcija = new List<Opcija>();
        }


        public void DodajOpciju(FunkcijaOpcije funkcija, string tekst)
        {
            /* Prilikom dodavanja nove metode i njenog opisa, kreiramo novi objekat Opcije i stavljamo ga u listu svih opcija vezanih za ovaj meni. Ovo je odlican primer kompozicije - Kreiramo opcije samo unutar menija, i unistavamo ih zajedno sa menijem. */

            Opcija novaOpcija = new Opcija(funkcija, tekst);
            listaOpcija.Add(novaOpcija);
        }

        private void IspisiMeni()
        {
            /* Prikaz menija korisniku */

            Console.WriteLine("Opcije:");

            /* Ispisujemo redom opine tekstove svih opcija, i obelezavamo ih redom brojevima, pocevsi od 1 */
            int redniBrojOpcije = 1;
            foreach (Opcija o in listaOpcija)
            {
                Console.WriteLine("{0} - {1}", redniBrojOpcije, o.Tekst);
                redniBrojOpcije++;
            }

            /* Uvek dodajemo podrazumevanu opciju 'Nazad' i obelezamo je brojem 0 */
            Console.WriteLine();
            Console.WriteLine("0 - Nazad");
            Console.WriteLine("---------------------------------");
        }

        public int PreuzmiOpcijuOdKorisnika()
        {
            /* Preuzimanje rednog broja opcije od korisnika. Ukoliko je uneta opcija neispravna, metoda vraca -1. */

            /* Prvo ispisujemo korisniku koji redni brojevi opcije su validni */
            Console.Write("Unesite opciju (0-{0}): ", listaOpcija.Count);

            /* Preuzimamo tekstualni unos korisnika */
            string unos = Console.ReadLine();
            Console.WriteLine();

            int odabranaOpcija;

            /* Proveravamo da li se uneti tekst moze konvertovati u celi broj */
            bool daLiJeBroj = int.TryParse(unos, out odabranaOpcija);
            if (daLiJeBroj == false)
            {
                /* Ukoliko ne moze, smatramo da je opcija neispravna, pa vracamo rezultat -1. */
                Console.WriteLine("Uneta opcija je neispravna!");
                return -1;
            }

            /* Ukoliko je broj opcije van dozvoljenog opsega, opet smatramo da je opcija neispravna */
            if (odabranaOpcija < 0 || odabranaOpcija > listaOpcija.Count)
            {
                Console.WriteLine("Uneta opcija je neispravna!");
                return -1;
            }

            /* Ukoliko je broj opcija validan, vracamo ga kao rezultat metode */
            return odabranaOpcija;
        }

        public void IzvrsiOpciju(int brojOpcije)
        {
            /* Izvrsi zadatu opciju, ukoliko je dobijeni broj opcije validan. Ukoliko nije, ne radi nista */
            if (brojOpcije > 0 && brojOpcije <= listaOpcija.Count)
            {
                listaOpcija[brojOpcije - 1].Funkcija();
            }
            Console.WriteLine();
        }

        public void Pokreni()
        {
            int odabranaOpcija;
            do
            {
                IspisiMeni();
                odabranaOpcija = PreuzmiOpcijuOdKorisnika();
                IzvrsiOpciju(odabranaOpcija);
            } while (odabranaOpcija != 0);
        }
    }
}
