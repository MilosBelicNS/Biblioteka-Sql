use DotNetMilos
create table Clanovi (
    id int primary key identity,
    ime nvarchar(50) not null,
    prezime nvarchar(50) not null
    );

create table Knjige (
    id int primary key identity,
    naziv nvarchar(50) not null,
    autor nvarchar(50),
    godina int,
    id_clana int,
    foreign key(id_clana) references Clanovi(id)
);