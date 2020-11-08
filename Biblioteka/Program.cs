/****
 * 
 *  Da bi ovaj primer funkcionisao, neophodno je napraviti odgovarajucu
 *  bazu podataka, tabele kao i pocetne podatke koristeći sledece skripte:
 *   
 *   - pravljenje_baze.sql
 *   - pravljenje_tabela.sql
 *   - punjenje_tabela_probnim_podacima.sql
 *   
 *   Navedene skripte se nalaze u istom folderu gde i ovaj solution!
 *
 ****/

namespace Biblioteka
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Pripremamo korisnicki meni */
            Meni.Meni m = new Meni.Meni();
            m.DodajOpciju(UI_Funkcionalnosti.PregledClanova, "Pregled svih clanova");
            m.DodajOpciju(UI_Funkcionalnosti.PregledKnjigaUkratko, "Kratak pregled knjiga");
            m.DodajOpciju(UI_Funkcionalnosti.PregledKnjigaDetaljno, "Detaljan pregled knjiga");
            m.DodajOpciju(UI_Funkcionalnosti.DodavanjeClana, "Dodavanje novog clana");
            m.DodajOpciju(UI_Funkcionalnosti.BrisanjeClana, "Brisanje postojeceg clana");
            m.DodajOpciju(UI_Funkcionalnosti.DodavanjeKnjige, "Dodavanje nove knjige");
            m.DodajOpciju(UI_Funkcionalnosti.BrisanjeKnjige, "Brisanje postojece knjige");
            m.DodajOpciju(UI_Funkcionalnosti.DodeljivanjeKnjigeClanu, "Dodeli postojecu knjigu nekom clanu");
            m.DodajOpciju(UI_Funkcionalnosti.OslobadjanjeKnjigeOdClana, "Oslobodi knjigu koja je vezana za clana");

            /* Pokrecemo korisnicki meni */
            m.Pokreni();          
        }
    }
}
