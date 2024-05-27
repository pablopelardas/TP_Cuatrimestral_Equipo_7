--USE "tp_cuatrimestral_pasteleria";
--GO

--CREATE TABLE "Ordenes"(
--    "id_orden" int IDENTITY(1,1) PRIMARY KEY NOT NULL,
--    "id_cliente" int not null,
--    "fecha" date not null,
--    "tipo_evento" varchar(30),
--    "tipo_entrega" varchar(30) not null,
--    "total" decimal not null,
--    "descuentos" decimal,
--    "incrementos" decimal,
--    "descripcion" varchar(100)
--)

--INSERT INTO Ordenes (id_cliente, fecha, tipo_evento, tipo_entrega, total, descuentos, incrementos, descripcion)
--VALUES
--(1, '2024-05-27', 'Cumpleaños', 'Domicilio', 150.00, 10.00, 5.00, 'Torta de chocolate y 20 cupcakes'),
--(2, '2024-06-15', 'Boda', 'Recoger', 500.00, 25.00, 15.00, 'Pastel de boda de 3 pisos'),
--(3, '2024-07-04', 'Aniversario', 'Domicilio', 200.00, 0.00, 10.00, 'Tarta de frutas y 30 galletas personalizadas');

--Select * FROM Ordenes