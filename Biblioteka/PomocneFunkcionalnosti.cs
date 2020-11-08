using System;
using Biblioteka.Model;
using Biblioteka.DAO;

/* U ovu datoteku smo smestili razne pomocne funkcionalnosti vezane za rad naseg programa. */

namespace Biblioteka
{
    public static class PomocneFunkcionalnosti
    {
        public static Clan PreuzmiNovogClana()
        {
            /* Pripremamo promenljive */
            Clan noviClan = null;
            string ime, prezime;

            /* Preuzimamo podatke od korisnika */
            Console.Write("Unesite ime novog clana: ");
            ime = Console.ReadLine();
            Console.Write("Unesite prezime novog clana: ");
            prezime = Console.ReadLine();

            if (!string.IsNullOrEmpty(ime) && !string.IsNullOrEmpty(prezime))
            {
                /* Ukoliko su podaci ispravni, pravimo novog clana */
                noviClan = new Clan(ime, prezime);
            }
            else
            {
                /* Ukoliko su podaci neispravni, obavestavamo korisnika o tome i vracamo null */
                Console.WriteLine("Neispravan unos podataka!");
            }

            return noviClan;
        }


        public static Clan PreuzmiPostojecegClana()
        {
            /* Pripremamo promenljive */
            Clan preuzetClan = null;
            int korisnickiUnosInt;

            /* Preuzimamo podatke od korisnka u string formatu */
            Console.Write("Unesite ID zeljenog clana: ");
            string korisnickiUnosString = Console.ReadLine();

            /* Proveravamo ispravnost unetih podataka */
            if(int.TryParse(korisnickiUnosString, out korisnickiUnosInt) == false)
            {
                /* Ukoliko su podaci neispravni, obavestavamo korisnika o tome */
                Console.WriteLine("Neispravan unos podataka!");
            }
            else
            {
                /* Ukoliko su podaci ispravni, pretvaramo ih iz stringa u integer */
                korisnickiUnosInt = int.Parse(korisnickiUnosString);

                /* Preuzimamo odgovarajuceg clana iz baze podataka */
                preuzetClan = ClanDAO.PreuzmiClanaPoID(korisnickiUnosInt);

                if(preuzetClan == null)
                {
                    /* Ukoliko ne postoji clan sa trazenim ID-jem u bazi podataka, obavestavamo korisnika */
                    Console.WriteLine("Ne postoji clan sa ID-jem {0}!", korisnickiUnosInt);
                }
            }

            /* Ukoliko je clan uspesno preuzet, vracamo ga kao povratnu vrednost metode. U suprotnom, vracamo null */
            return preuzetClan;
        }

        public static Knjiga PreuzmiNovuKnjigu()
        {
            /* Pripremamo promenljive */
            Knjiga novaKnjiga = null;
            string naziv, autor, godinaString;
            int godinaInt;

            /* Preuzimamo podatke od korisnika. Prilikom svakog unosa proveravamo ispravnost unetih podataka */
            Console.Write("Unesite naziv nove knjige: ");
            naziv = Console.ReadLine();
            if(string.IsNullOrEmpty(naziv) == true)
            {
                Console.WriteLine("Neispravan unos naziva knjige!");
                return null;
            }

            Console.Write("Unesite autora nove knjige: ");
            autor = Console.ReadLine();
            if (string.IsNullOrEmpty(autor) == true)
            {
                autor = null;
            }

            Console.Write("Unesite godinu izdanja knjige: ");
            godinaString = Console.ReadLine();

            if (string.IsNullOrEmpty(godinaString) == true)
            {
                godinaInt = -1;
            }
            else
            {
                if(int.TryParse(godinaString, out godinaInt) == false)
                {
                    Console.WriteLine("Neispravan unos godine!");
                    return null;
                }
                godinaInt = int.Parse(godinaString);
            }

            /* Ukoliko su uneti podaci ispravni, pravimo novi objekat na osnovu njih i vracamo ga kao povratnu vrednost metode */
            novaKnjiga = new Knjiga(naziv, autor, godinaInt);
            return novaKnjiga;
        }

        public static Knjiga PreuzmiPostojecuKnjigu()
        {
            /* Pripremamo promenljive */
            Knjiga preuzetaKnjiga = null;
            int korisnickiUnosInt;

            /* Preuzimamo podatke od korisnka u string formatu */
            Console.Write("Unesite ID zeljene knjige: ");
            string korisnickiUnosString = Console.ReadLine();

            /* Proveravamo ispravnost unetih podataka */
            if (int.TryParse(korisnickiUnosString, out korisnickiUnosInt) == false)
            {
                /* Ukoliko su podaci neispravni, obavestavamo korisnika o tome */
                Console.WriteLine("Neispravan unos podataka!");
            }
            else
            {
                /* Ukoliko su podaci ispravni, pretvaramo ih iz stringa u integer */
                korisnickiUnosInt = int.Parse(korisnickiUnosString);

                /* Preuzimamo odgovarajucu knjigu iz baze podataka */
                preuzetaKnjiga = KnjigaDAO.PreuzmiKnjiguPoID(korisnickiUnosInt);

                if (preuzetaKnjiga == null)
                {
                    /* Ukoliko ne postoji knjiga sa trazenim ID-jem u bazi podataka, obavestavamo korisnika */
                    Console.WriteLine("Ne postoji knjiga sa ID-jem {0}!", korisnickiUnosInt);
                }
            }

            /* Ukoliko je knjiga uspesno preuzeta, vracamo je kao povratnu vrednost metode. U suprotnom, vracamo null */
            return preuzetaKnjiga;
        }
    }
}
