using System.Data.SqlClient;

/* U ovoj datoteci smo definisali funkcije za rad sa konekcijom na bazu. */

namespace Biblioteka.DAO
{
    public static class DAOConnection
    {
        /* Podrazumevani connection string za bazu podataka - podesiti za svoj racunar/server/bazu */
        private static string connectionString = @"Data Source=DESKTOP-GC0HF6E\SQLEXPRESS;Initial Catalog=DotNetMilos;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /* Kada nekom delu aplikacije bude neophodna konekcija za bazu podataka, taj deo aplikacije je moze zatraziti koristeci ovu metodu */
        public static SqlConnection TraziNovuKonekciju()
        {
            /* Pripremamo objekat za konekciju */
            SqlConnection novaKonekcija = new SqlConnection(connectionString);

            /* Pokusavamo da otvorimo konekciju */
            novaKonekcija.Open();

            /* Otvorenu konekciju vracamo onome ko ju je zahtevao */
            return novaKonekcija;
        }
    }
}
