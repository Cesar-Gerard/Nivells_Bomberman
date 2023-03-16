drop table if exists introduccio;
drop table if exists nivell;
drop table if exists nivell_caselles;

CREATE TABLE Introduccio (
    
	id_introduccio int(3) not null auto_increment,
    intro_nom varchar(100) not null,
    intro_desc varchar(250) not null,
    hores int(2),
    minuts int(2),
    segons int (2) not null,
    intro_imatge varchar(2000) not null,
    estat bool,
    
    constraint primary key(id_introduccio,intro_nom)
    
    
);

CREATE TABLE Nivell (
    
	id_nivell int(3) not null auto_increment,
    nivell_nom varchar(100) not null,
    nivell_desc varchar(250) not null,
    hores int(2),
    minuts int(2),
    segons int (2) not null,
    estat bool,
    
    constraint primary key(id_nivell,nivell_nom)
    
);

create table Nivell_Caselles(
	id int(3) not null,
	cX int(4) not null,
	cY int(4) not null,
	valor int(3) not null
	

);


