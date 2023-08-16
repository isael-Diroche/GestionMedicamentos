create database SGI;
use SGI;

create table users 
(
userID int IDENTITY(1,1) PRIMARY KEY,
fname varchar(50),
username varchar(50),
passwd varchar(128)
)

insert into users values
('fulano','hola','b221d9dbb083a7f33428d7c2a3c3198ae925614d70210e28716ccaa7cd4ddb79')

create table medicamentos
(
idMedicamento int IDENTITY(1,1) PRIMARY KEY,
nombreMedicamento varchar(50),
descripcion varchar(100), 
lote varchar(50),
fechaCaducidad date,
stock int check (stock>=0),
distribuidor varchar(50)
);

INSERT INTO medicamentos (nombreMedicamento, descripcion, lote, fechaCaducidad, stock, distribuidor)
VALUES
    ('Paracetamol', 'Analgésico y antifebril', '12345', '2023-12-31', 100, 'Distribuidora A'),
    ('Ibuprofeno', 'Antiinflamatorio no esteroideo', '23456', '2024-05-15', 50, 'Distribuidora B'),
    ('Amoxicilina', 'Antibiótico de amplio espectro', '34567', '2023-11-20', 75, 'Distribuidora C'),
    ('Omeprazol', 'Inhibidor de la bomba de protones', '45678', '2024-02-28', 30, 'Distribuidora A'),
    ('Loratadina', 'Antihistamínico no sedante', '56789', '2023-09-10', 120, 'Distribuidora D'),
    ('Atorvastatina', 'Inhibidor de la HMG-CoA reductasa', '67890', '2024-07-22', 40, 'Distribuidora B'),
    ('Metformina', 'Antidiabético oral', '78901', '2023-08-15', 90, 'Distribuidora E'),
    ('Diazepam', 'Ansiolítico y relajante muscular', '89012', '2024-03-05', 25, 'Distribuidora F'),
    ('Ranitidina', 'Antagonista de los receptores H2', '90123', '2023-10-01', 60, 'Distribuidora C'),
    ('Losartán', 'Antagonista del receptor de angiotensina II', '01234', '2024-06-18', 70, 'Distribuidora G');
