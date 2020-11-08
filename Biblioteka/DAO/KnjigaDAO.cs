using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Biblioteka.Model;

/* U ovu datoteku smo stavili funkcionalnosti koje su vezane za komunikaciju sa bazom podataka prilikom rada sa knjigama - na primer dodavanje nove knjige u bazu, brisanje postojece knjige iz baze, itd. */

namespace Biblioteka.DAO
{
    public static class KnjigaDAO
    {
        public static List<Knjiga> PreuzmiSveKnjige()
        {
            /* Preuzimamo novu konekciju za bazu od modula za preuzimanje konekcije za bazu (logicno) */
            SqlConnection connection = DAOConnection.TraziNovuKonekciju();

            /* Pripremamo praznu listu */
            List<Knjiga> listaKnjiga = new List<Knjiga>();

            /* Pripremamo SQL upit i komandu */
            string upit = "select Knjige.id, naziv, autor, godina, id_clana , ime, prezime from Knjige left join Clanovi on Knjige.id_clana = Clanovi.id";
            SqlCommand cmd = new SqlCommand(upit, connection);

            /* Izvrsavamo upit nad bazom podataka */
            SqlDataReader rdr = cmd.ExecuteReader();

            /* Iterativno citamo podatke i smestamo ih u listu */
            while (rdr.Read())
            {
                /* Zbog toga sto neke kolone u ovoj tabeli mogu imati vrednost NULL, moramo biti oprezni prilikom preuzimanja podataka */

                /* Prvo deklarisemo promenljive koje cemo koristiti */
                int id;
                string naziv;
                string autor;
                int godina;
                Clan clan;

                /* ID i naziv ne mogu biti null, pa ih preuzimamo direktno bez ikakve provere */
                id = (int)rdr[0];
                naziv = (string)rdr[1];

                /* Kolone "autor" i "godina" mogu biti imati vrednost NULL, pa moramo prvo izvrsiti proveru */
                if (rdr.IsDBNull(2))
                {
                    /* Ukoliko ne postoje podaci o autoru, koristimo predefinisanu vrednost "-" */
                    autor = "-";
                }
                else
                {
                    /* Ukoliko postoje podaci o autoru, mozemo bezbedno da ih preuzmemo */
                    autor = (string)rdr[2];
                }


                if(rdr.IsDBNull(3))
                {
                    /* Ukoliko ne postoje podaci o godini, koristimo predefinisanu vrednost -1 */
                    godina = -1;
                }
                else
                {
                    /* Ukoliko postoje podaci o godini, mozemo bezbedno da ih preuzmemo */
                    godina = (int)rdr[3];
                }

                /* ID Clana takodje moze imati vrednost NULL, tako da moramo izvrsiti proveru */
                if(rdr.IsDBNull(4))
                {
                    /* Ukoliko je vrednost ove kolone NULL, knjiga je slobodna */
                    clan = null;
                }
                else
                {
                    /* Ako id_clana nije null, onda mozemo da pokupimo podatke o clanu (posto ta polja onda sigurno nisu NULL). Obratiti paznju na to da su nam kolone odgovarajuceg clana dostupne jer smo prilikom upita izvrsili JOIN */
                    int id_clana = (int)rdr[4];
                    string ime = (string)rdr[5];
                    string prezime = (string)rdr[6];
                    clan = new Clan(id_clana, ime, prezime);
                }

                /* Formiramo novi objekat koristeci pribavljene podatke i dodajemo ga u listu */
                Knjiga novaKnjiga = new Knjiga(id, naziv, autor, godina, clan);
                listaKnjiga.Add(novaKnjiga);
            }

            /* Zatvaramo otvorene resurse*/
            rdr.Close();
            connection.Close();

            /* Vracamo napunjenu listu */
            return listaKnjiga;
        }

        public static bool DodajNovuKnjigu(Knjiga novaKnjiga)
        {
            /* Preuzimamo novu konekciju za bazu od modula za preuzimanje konekcije za bazu (logicno) */
            SqlConnection connection = DAOConnection.TraziNovuKonekciju();

            /* Pripremamo promenljivu za povratnu vrednost */
            bool retVal;

            /* Pripremamo upit i komandu. Obezbedjujemo se protiv SQL injection-a. */
            string upit = "insert into Knjige (naziv, autor, godina) values (@naziv, @autor, @godina)";
            SqlCommand cmd = new SqlCommand(upit, connection);
            cmd.Parameters.AddWithValue("naziv", novaKnjiga.Naziv);

            /* Neka polja knjige mogu biti nedefinisana. Proveravamo da li je to slucaj, i u zavisnosti od toga u upit ubacujemo NULL, ili odgovarajucu vrednost tog polja. */
            if(novaKnjiga.Autor == null)
            {
                cmd.Parameters.AddWithValue("autor", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("autor", novaKnjiga.Autor);
            }
            if(novaKnjiga.Godina == -1)
            {
                cmd.Parameters.AddWithValue("godina", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("godina", novaKnjiga.Godina);
            }

            /* Saljemo komandu bazi podataka */
            try
            {
                cmd.ExecuteNonQuery();

                /* Ukoliko je komanda uspela bez izuzetka, smatramo operaciju uspesnom */
                retVal = true;
            }
            catch
            {
                /* Ukoliko je doslo do izuzetka, smatramo operaciju neuspesnom */
                retVal = false;
            }

            /* Zatvaramo otvorene resurse */
            connection.Close();

            /* Vracamo uspesnost operacije kao povratnu vrednost metode */
            return retVal;
        }

        public static Knjiga PreuzmiKnjiguPoID(int id)
        {
            /* Preuzimamo novu konekciju za bazu od modula za preuzimanje konekcije za bazu (logicno) */
            SqlConnection connection = DAOConnection.TraziNovuKonekciju();

            /* Pripremamo promenljivu za povratnu vrednost */
            Knjiga retVal;

            /* Pripremamo upit i komandu. Obezbedjujemo se protiv SQL injection-a (mada ovde ne bi trebalo da dodje do toga). */
            string upit = "select Knjige.id, naziv, autor, godina, id_clana , ime, prezime from Knjige left join Clanovi on Knjige.id_clana = Clanovi.id where Knjige.id = @id";
            SqlCommand cmd = new SqlCommand(upit, connection);
            cmd.Parameters.AddWithValue("id", id);

            /* Izvrsavamo upit nad bazom podataka */
            SqlDataReader rdr = cmd.ExecuteReader();
            
            if(rdr.Read())
            {
                /* Ukoliko smo dobili neki rezultat, preuzimamo podatke */

                /* Prvo deklarisemo promenljive koje cemo koristiti */
                string naziv;
                string autor;
                int godina;
                Clan clan;

                /* ID i naziv ne mogu biti null, pa ih preuzimamo direktno bez ikakve provere */
                id = (int)rdr[0];
                naziv = (string)rdr[1];

                /* Kolone "autor" i "godina" mogu biti imati vrednost NULL, pa moramo prvo izvrsiti proveru */
                if (rdr.IsDBNull(2))
                {
                    autor = "-";
                }
                else
                {
                    autor = (string)rdr[2];
                }
                if (rdr.IsDBNull(3))
                {
                    godina = -1;
                }
                else
                {
                    godina = (int)rdr[3];
                }

                /* ID Clana takodje moze imati vrednost NULL, tako da moramo izvrsiti proveru */
                if (rdr.IsDBNull(4))
                {
                    /* Ukoliko je vrednost ove kolone NULL, knjiga je slobodna */
                    clan = null;
                }
                else
                {
                    /* Ako id_clana nije null, onda mozemo da pokupimo podatke o clanu (posto ta polja onda sigurno nisu NULL). Obratiti paznju na to da su nam kolone odgovarajuceg clana dostupne jer smo prilikom upita izvrsili JOIN */
                    int id_clana = (int)rdr[4];
                    string ime = (string)rdr[5];
                    string prezime = (string)rdr[6];
                    clan = new Clan(id_clana, ime, prezime);
                }

                /* Na osnovu prikupljenih podataka formiramo novi objekat i smestamo ga u povratnu vrednost */
                retVal = new Knjiga(id, naziv, autor, godina, clan);
            }
            else
            {
                /* Ukoliko nismo dobili nijedan rezultat iz baze podataka, vraticemo null */
                retVal = null;
            }

            /* Zatvaramo otvorene resurse i zavrsavamo metodu */
            rdr.Close();
            connection.Close();

            return retVal;
        }


        public static bool ObrisiKnjigu(Knjiga knjigaZaBrisanje)
        {
            /* Preuzimamo novu konekciju za bazu od modula za preuzimanje konekcije za bazu (logicno) */
            SqlConnection connection = DAOConnection.TraziNovuKonekciju();

            /* Pripremamo promenljivu za povratnu vrednost metode */
            bool retVal;

            /*Pripremamo upit i komandu.Obezbedjujemo se protiv SQL injection-a. */
            string upit = "delete from Knjige where id = @id";
            SqlCommand cmd = new SqlCommand(upit, connection);
            cmd.Parameters.AddWithValue("id", knjigaZaBrisanje.Id);

            /* Saljemo komandu bazi podataka */
            try
            {
                cmd.ExecuteNonQuery();

                /* Ukoliko je komanda uspela bez izuzetka, smatramo operaciju uspesnom */
                retVal = true;
            }
            catch
            {
                /* Ukoliko je doslo do izuzetka, smatramo operaciju neuspesnom */
                retVal = false;
            }

            /* Zatvaramo otvorene resurse */
            connection.Close();

            /* Vracamo uspesnost operacije kao povratnu vrednost metode */
            return retVal;
        }

        public static bool DodeliKnjiguClanu(Knjiga knjigaZaDodeljivanje, Clan clanZaDodeljivanje)
        {
            /* Preuzimamo novu konekciju za bazu od modula za preuzimanje konekcije za bazu (logicno) */
            SqlConnection connection = DAOConnection.TraziNovuKonekciju();

            /* Proveravamo da li smo dobili ispravne reference */
            if (knjigaZaDodeljivanje == null || clanZaDodeljivanje == null)
            {
                return false;
            }

            /* Pripremamo promenljivu za povratnu vrednost metode */
            bool retVal;

            /* Pripremamo upit i komandu */
            string upit = "update Knjige set id_clana = " + clanZaDodeljivanje.Id + " where id = " + knjigaZaDodeljivanje.Id;
            SqlCommand cmd = new SqlCommand(upit, connection);

            /* Saljemo komandu bazi podataka */
            try
            {
                cmd.ExecuteNonQuery();

                /* Ukoliko je komanda uspela bez izuzetka, smatramo operaciju uspesnom */
                retVal = true;
            }
            catch
            {
                /* Ukoliko je doslo do izuzetka, smatramo operaciju neuspesnom */
                retVal = false;
            }

            /* Zatvaramo otvorene resurse */
            connection.Close();

            return retVal;
        }

        public static bool OslobodiKnjigu(Knjiga knjigaZaOslobadjanje)
        {
            /* Preuzimamo novu konekciju za bazu od modula za preuzimanje konekcije za bazu (logicno) */
            SqlConnection connection = DAOConnection.TraziNovuKonekciju();

            /* Proveravamo da li smo dobili ispravnu referencu */
            if (knjigaZaOslobadjanje == null)
            {
                return false;
            }

            /* Pripremamo promenljivu za povratnu vrednost metode */
            bool retVal;

            /* Pripremamo upit i komandu */
            string upit = "update Knjige set id_clana = NULL where id = " + knjigaZaOslobadjanje.Id;
            SqlCommand cmd = new SqlCommand(upit, connection);

            /* Saljemo komandu bazi podataka */
            try
            {
                cmd.ExecuteNonQuery();

                /* Ukoliko je komanda uspela bez izuzetka, smatramo operaciju uspesnom */
                retVal = true;
            }
            catch
            {
                /* Ukoliko je doslo do izuzetka, smatramo operaciju neuspesnom */
                retVal = false;
            }

            /* Zatvaramo otvorene resurse */
            connection.Close();

            return retVal;
        }
    }
}