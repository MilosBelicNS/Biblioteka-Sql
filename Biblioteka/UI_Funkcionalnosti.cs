using System;
using System.Collections.Generic;
using Biblioteka.DAO;
using Biblioteka.Model;

/* U ovoj datoteci smo definisali funkcije koje izvrsavaju funkcionalnosti ponudjene u glavnom (i jedinom) meniju. */

namespace Biblioteka
{
    public static class UI_Funkcionalnosti
    {
        public static void PregledClanova()
        {
            /* Preuzimamo podatke iz baze podataka */
            List<Clan> listaClanova = ClanDAO.PreuzmiSveClanove();

            /* Ispisujemo zaglavlje tabele */
            Console.WriteLine("Pregled svih clanova biblioteke:");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("{0,-4} | {1,-15} | {2,-15}", "ID", "Ime", "Prezime");
            Console.WriteLine("--------------------------------");

            /* Redom ispisujemo clanove u tabelarnom formatu */
            foreach (Clan c in listaClanova)
            {
                Console.WriteLine(c.TabelarnaStringReprezentacija());
            }
        }

        public static void PregledKnjigaUkratko()
        {
            /* Preuzimamo podatke iz baze podataka */
            List<Knjiga> listaKnjiga = KnjigaDAO.PreuzmiSveKnjige();

            /* Ispisujemo zaglavlje tabele */
            Console.WriteLine("Kratak pregled knjiga:");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("{0,-30} | {1,-10}", "Naziv", "Stanje");
            Console.WriteLine("----------------------------------------------");

            /* Redom ispisujemo knjige u tabelarnom formatu */
            foreach (Knjiga k in listaKnjiga)
            {
                Console.WriteLine(k.StringReprezentacijaUkratko());
            }
        }

        public static void PregledKnjigaDetaljno()
        {
            /* Preuzimamo podatke iz baze podataka */
            List<Knjiga> listaKnjiga = KnjigaDAO.PreuzmiSveKnjige();

            /* Ispisujemo zaglavlje tabele */
            Console.WriteLine("Detaljan pregled svih knjiga biblioteke:");
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine("{0, 3} | {1,-30} | {2,-20} | {3,-6} | {4,-20}", "ID", "Naziv", "Autor", "Godina", "Trenutno kod");
            Console.WriteLine("-----------------------------------------------------------------------------");

            /* Redom ispisujemo knjige u tabelarnom formatu */
            foreach (Knjiga k in listaKnjiga)
            {
                Console.WriteLine(k.StringReprezentacijaDetaljno());
            }
        }

        public static void DodavanjeClana()
        {
            /* Preuzimamo podatke o novom clanu od korisnika */
            Clan noviClan = PomocneFunkcionalnosti.PreuzmiNovogClana();

            /* Ukoliko su podaci ispravni, dodajemo ga u bazu podataka */
            if (noviClan != null)
            {
                /* Pokusavamo dodavanje u bazu podataka */
                bool uspesnoDodavanje = ClanDAO.DodajNovogClana(noviClan);

                /* Ispisujemo da li je dodavanje bilo uspesno */
                if (uspesnoDodavanje == false)
                {
                    Console.WriteLine("Doslo je do greske prilikom dodavanja novog clana!");
                }
                else
                {
                    Console.WriteLine("Clan {0} {1} je uspesno dodat u bazu podataka!", noviClan.Ime, noviClan.Prezime);
                }
            }
        }

        public static void BrisanjeClana()
        {
            /* Preuzimamo podatke o clanu kojeg korisnik zeli da obrise */
            Clan clanZaBrisanje = PomocneFunkcionalnosti.PreuzmiPostojecegClana();

            /* Ukoliko su podaci neispravni, prekidamo izvrsavanje metode */
            if (clanZaBrisanje == null)
            {
                return;
            }

            /* Ukoliko clan poseduje neku knjigu, ne moze se obrisati iz sistema pre nego sto se ne obrise knjiga. U tom slucaju prekidamo izvrsavanje metode. */
            if (ClanDAO.ImaLiClanKnjigu(clanZaBrisanje) == true)
            {
                Console.WriteLine("Nije moguce brisanje clana koji poseduje knjigu! Sta bi onda bilo sa knjigom? :(");
                return;
            }

            /* Pokusavamo brisanje clana iz baze podataka */
            bool uspesnoBrisanje = ClanDAO.ObrisiClana(clanZaBrisanje);

            /* Obavestavamo korisnika o tome da li je brisanje bilo uspesno */
            if (uspesnoBrisanje == false)
            {
                Console.WriteLine("Doslo je do greske prilikom brisanja!");
            }
            else
            {
                Console.WriteLine("Clan {0} {1} je uspesno obrisan iz baze podataka!", clanZaBrisanje.Ime, clanZaBrisanje.Prezime);
            }
        }

        public static void DodavanjeKnjige()
        {
            /* Preuzimamo od korisnika podatke o knjizi koju zeli da unese */
            Knjiga novaKnjiga = PomocneFunkcionalnosti.PreuzmiNovuKnjigu();

            /* Ukoliko su podaci ispravni, ubacujemo novu knjigu u bazu podataka */
            if (novaKnjiga != null)
            {
                /* Pokusavamo dodavanje nove knjige u bazu podataka */
                bool uspesnoDodavanje = KnjigaDAO.DodajNovuKnjigu(novaKnjiga);

                /* Obavestavamo korisnika o tome da li je dodavanje bilo uspesno */
                if (uspesnoDodavanje == false)
                {
                    Console.WriteLine("Doslo je do greske prilikom dodavanja nove knjige!");
                }
                else
                {
                    Console.WriteLine("Knjiga sa nazivom \"{0}\" je uspesno dodata u bazu podataka!", novaKnjiga.Naziv);
                }
            }
        }

        public static void BrisanjeKnjige()
        {
            /* Preuzimamo od korisnika podatke o knjizi koju zeli da obrise */
            Knjiga knjigaZaBrisanje = PomocneFunkcionalnosti.PreuzmiPostojecuKnjigu();

            /* Ukoliko su podaci neispravni, prekidamo izvrsavanje metode */
            if (knjigaZaBrisanje == null)
            {
                return;
            }

            /* Pokusavamo brisanje knjige iz baze podataka */
            bool uspesnoBrisanje = KnjigaDAO.ObrisiKnjigu(knjigaZaBrisanje);

            /* Obavestavamo korisnika o tome da li je brisanje bilo uspesno */
            if (uspesnoBrisanje == false)
            {
                Console.WriteLine("Doslo je do greske prilikom brisanja!");
            }
            else
            {
                Console.WriteLine("Knjiga sa nazivom \"{0}\" je uspesno obrisana iz baze podataka!", knjigaZaBrisanje.Naziv);
            }
        }

        public static void DodeljivanjeKnjigeClanu()
        {
            /* Preuzimamo od korisnika podatke o knjizi koju zeli da dodeli */
            Knjiga knjigaZaDodeljivanje = PomocneFunkcionalnosti.PreuzmiPostojecuKnjigu();

            /* Ukoliko su podaci neispravni, prekidamo izvrsavanje metode */
            if (knjigaZaDodeljivanje == null)
            {
                return;
            }

            /* Preuzimamo podatke o clanu kojem korisnik zeli da dodeli knjigu */
            Clan clanZaDodeljivanje = PomocneFunkcionalnosti.PreuzmiPostojecegClana();

            /* Ukoliko su podaci neispravni, prekidamo izvrsavanje metode */
            if (clanZaDodeljivanje == null)
            {
                return;
            }

            /* Pokusavamo dodeljivanje knjige u bazi podataka */
            bool uspesnoDodeljivanje = KnjigaDAO.DodeliKnjiguClanu(knjigaZaDodeljivanje, clanZaDodeljivanje);

            /* Obavestavamo korisnika o tome da li je dodeljivanje bilo uspesno */
            if (uspesnoDodeljivanje == false)
            {
                Console.WriteLine("Doslo je do greske prilikom dodeljivanja knjige!");
            }
            else
            {
                Console.WriteLine("Knjiga sa nazivom \"{0}\" je uspesno dodeljena clanu {1} {2}!", knjigaZaDodeljivanje.Naziv, clanZaDodeljivanje.Ime, clanZaDodeljivanje.Prezime);
            }
        }

        public static void OslobadjanjeKnjigeOdClana()
        {
            /* Preuzimamo od korisnika podatke o knjizi koju zeli da oslobodi */
            Knjiga knjigaZaOslobadjanje = PomocneFunkcionalnosti.PreuzmiPostojecuKnjigu();

            /* Ukoliko su podaci neispravni, prekidamo izvrsavanje metode */
            if (knjigaZaOslobadjanje == null)
            {
                return;
            }

            /* Pokusavamo dodeljivanje knjige u bazi podataka */
            bool uspesnoOslobadjanje = KnjigaDAO.OslobodiKnjigu(knjigaZaOslobadjanje);

            /* Obavestavamo korisnika o tome da li je dodeljivanje bilo uspesno */
            if (uspesnoOslobadjanje == false)
            {
                Console.WriteLine("Doslo je do greske prilikom oslobadjanja knjige!");
            }
            else
            {
                Console.WriteLine("Knjiga sa nazivom \"{0}\" je sada dostupna!", knjigaZaOslobadjanje.Naziv);
            }
        }
    }
}