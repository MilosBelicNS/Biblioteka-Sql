using System.Collections.Generic;
using Biblioteka.Model;
using System.Data.SqlClient;

/* U ovu datoteku smo stavili funkcionalnosti koje su vezane za komunikaciju sa bazom podataka prilikom rada sa clanovima - na primer dodavanje novog clana u bazu, brisanje postojeceg clana iz baze, itd. */

namespace Biblioteka.DAO
{
    public static class ClanDAO
    {
        public static List<Clan> PreuzmiSveClanove()
        {
            /* Preuzimamo novu konekciju za bazu od modula za preuzimanje konekcije za bazu (logicno) */
            SqlConnection connection = DAOConnection.TraziNovuKonekciju();

            /* Pripremamo praznu listu */
            List<Clan> listaClanova = new List<Clan>();

            /* Pripremamo SQL upit i komandu */
            string upit = "select id, ime, prezime from Clanovi";
            SqlCommand cmd = new SqlCommand(upit, connection);

            /* Izvrsavamo upit nad bazom podataka */
            SqlDataReader rdr = cmd.ExecuteReader();

            /* Iterativno citamo podatke i smestamo ih u listu */
            while(rdr.Read())
            {
                int id = (int)rdr["id"];
                string ime = (string)rdr["ime"];
                string prezime = (string)rdr["prezime"];
                Clan noviClan = new Clan(id, ime, prezime);
                listaClanova.Add(noviClan);
            }

            /* Zatvaramo otvorene resurse */
            rdr.Close();
            connection.Close();

            /* Vracamo napunjenu listu */
            return listaClanova;
        }

        public static Clan PreuzmiClanaPoID(int id)
        {
            /* Preuzimamo novu konekciju za bazu od modula za preuzimanje konekcije za bazu (logicno) */
            SqlConnection connection = DAOConnection.TraziNovuKonekciju();

            /* Pripremamo promenljivu za povratnu vrednost */
            Clan retVal;

            /* Pripremamo upit i komandu. Obezbedjujemo se protiv SQL injection-a (mada ovde ne bi trebalo da dodje do toga). */
            string upit = "select id, ime, prezime from Clanovi where id = @id";
            SqlCommand cmd = new SqlCommand(upit, connection);
            cmd.Parameters.AddWithValue("id", id);

            /* Izvrsavamo upit nad bazom podataka */
            SqlDataReader rdr = cmd.ExecuteReader();

            if(rdr.Read())
            {
                /* Ako smo dobili nekog clana kao rezultat upita, vratimo ga kao rezultat metode */
                int id_clana = (int)rdr[0];
                string ime = (string)rdr[1];
                string prezime = (string)rdr[2];

                Clan clan = new Clan(id_clana, ime, prezime);
                retVal = clan;
            }
            else
            {
                /* Ako nismo dobili nijedan rezultat, onda cemo da vratimo null */
                retVal = null;
            }

            /* Zatvaramo otvorene resurse i zavrsvamo metodu */
            rdr.Close();
            connection.Close();

            return retVal;
        }

        public static bool DodajNovogClana(Clan noviClan)
        {
            /* Preuzimamo novu konekciju za bazu od modula za preuzimanje konekcije za bazu (logicno) */
            SqlConnection connection = DAOConnection.TraziNovuKonekciju();

            /* Pripremamo promenljivu za povratnu vrednost metode */
            bool retVal;

            /* Pripremamo upit i komandu. Obezbedjujemo se protiv SQL injection-a. */
            string upit = "insert into Clanovi (ime, prezime) values (@ime, @prezime)";
            SqlCommand cmd = new SqlCommand(upit, connection);
            cmd.Parameters.AddWithValue("ime", noviClan.Ime);
            cmd.Parameters.AddWithValue("prezime", noviClan.Prezime);

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

        public static bool ObrisiClana(Clan clanZaBrisanje)
        {
            /* Preuzimamo novu konekciju za bazu od modula za preuzimanje konekcije za bazu (logicno) */
            SqlConnection connection = DAOConnection.TraziNovuKonekciju();

            /* Pripremamo promenljivu za povratnu vrednost metode */
            bool retVal;

            /*Pripremamo upit i komandu.Obezbedjujemo se protiv SQL injection-a. */
            string upit = "delete from Clanovi where id = @id";
            SqlCommand cmd = new SqlCommand(upit, connection);
            cmd.Parameters.AddWithValue("id", clanZaBrisanje.Id);

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

        public static bool ImaLiClanKnjigu(Clan clan)
        {
            /* Preuzimamo novu konekciju za bazu od modula za preuzimanje konekcije za bazu (logicno) */
            SqlConnection connection = DAOConnection.TraziNovuKonekciju();

            /* Pripremamo promenljivu za povratnu vrednost metode */
            bool retVal;

            /* Pripremamo upit i komandu. Obezbedjujemo se protiv SQL injection-a. */
            string upit = "select id from Clanovi where Clanovi.id in (select id_clana from Knjige) and id = @id";
            SqlCommand cmd = new SqlCommand(upit, connection);
            cmd.Parameters.AddWithValue("id", clan.Id);

            /* Saljemo upit bazi podataka */
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read() == true)
            {
                /* Ukoliko smo dobili bilo kakav rezultat, postoji knjiga koja pripada datom clanu */
                retVal = true;
            }
            else
            {
                /* Ukoliko nismo dobili nikakav rezultat, ne postoji knjiga koja pripada datom clanu */
                retVal = false;
            }

            /* Zatvaramo otvorene resurse */
            connection.Close();

            /* Vracamo povratnu vrednost metode */
            return retVal;
        }
    }
}
