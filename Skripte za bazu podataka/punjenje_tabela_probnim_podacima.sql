use DotNetMilos

insert into Clanovi (ime, prezime)
values
    ('Pera', 'Peric'),
    ('Ana', 'Anic'),
    ('Sima', 'Simic'),
    ('Nikola', 'Nikolic'),
    ('Jovana', 'Jovanovic');

insert into Knjige (naziv, autor, godina, id_clana)
values
    ('Znakovi pored puta', 'Ivo Andric', 1976, 1),
    ('Na Drini Cuprija', 'Ivo Andric', 1945, 1),
    ('Majstor i Margarita', 'Mikhail Bulgakov', 1967, 1),
    ('Koreni', 'Dobrica Cosic', 1954, 2),
    ('Zlocin i kazna', 'Fjodor Dostojevski', 1866, 3),
    ('Englesko-srpski recnik', null, 2012, 4),
    ('Starac i more', 'Ernest Hemingvej', 1952, null),
    ('Necista krv', 'Borislav Stankovic', 1910, null);