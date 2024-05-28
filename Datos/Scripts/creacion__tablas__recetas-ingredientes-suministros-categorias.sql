CREATE TABLE "CATEGORIAS"(
    "id_categoria" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    "tipo" VARCHAR(50) NOT NULL,
    "nombre" VARCHAR(50) NOT NULL
)

CREATE TABLE "UNIDADES_MEDIDA"(
    "id_unidad" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    "nombre" VARCHAR(50) NOT NULL,
    "abreviatura" VARCHAR(10) NOT NULL
)

CREATE TABLE "INGREDIENTES"(
    "id_ingrediente" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    "nombre" VARCHAR(50) NOT NULL,
    "medida" FLOAT NOT NULL,
    "id_unidad" INT NOT NULL,
    "costo" MONEY NOT NULL,
    "proveedor" VARCHAR(50),

    FOREIGN KEY ("id_unidad") REFERENCES "UNIDADES_MEDIDA"("id_unidad"),
)

CREATE TABLE "RECETAS"(
    "id_receta" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    "nombre" VARCHAR(50) NOT NULL,
    "descripcion" VARCHAR(200) NOT NULL,
    "id_categoria" INT NOT NULL,
    "precio_personalizado" MONEY,

)

CREATE TABLE "RECETA_INGREDIENTE"(
    "id_receta" INT NOT NULL,
    "id_ingrediente" INT NOT NULL,
    "cantidad" FLOAT NOT NULL,

    PRIMARY KEY ("id_receta", "id_ingrediente"),
    FOREIGN KEY ("id_receta") REFERENCES "RECETAS"("id_receta"),
    FOREIGN KEY ("id_ingrediente") REFERENCES "INGREDIENTES"("id_ingrediente")
)

CREATE TABLE "SUMINISTROS"(
    "id_suministro" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    "id_categoria" INT NOT NULL,
    "nombre" VARCHAR(50) NOT NULL,
    "proveedor" VARCHAR(50),
    "cantidad" FLOAT NOT NULL,
    "costo" MONEY NOT NULL,

    FOREIGN KEY ("id_categoria") REFERENCES "CATEGORIAS"("id_categoria")
)


INSERT INTO "CATEGORIAS"("tipo", "nombre")
VALUES
('PRODUCTO', 'Torta'),
('PRODUCTO', 'Cookie'),
('PRODUCTO', 'Cake Pop'),
('PRODUCTO', 'Cupcake'),
('PRODUCTO', 'Facturas'),
('PRODUCTO', 'Tarta'),
('PRODUCTO', 'Pan'),
('SUMINISTRO', 'Cajas'),
('SUMINISTRO', 'Limpieza'),
('SUMINISTRO', 'Decoración'),
('SUMINISTRO', 'Equipamiento'),
('SUMINISTRO', 'Empaquetado'),
('SUMINISTRO', 'Insumos'),
('SUMINISTRO', 'Tarjetas y Etiquetas')


INSERT INTO "UNIDADES_MEDIDA"("nombre", "abreviatura")
VALUES
('Kilogramo', 'Kg'),
('Gramo', 'g'),
('Litro', 'L'),
('Mililitro', 'mL'),
('Unidad', 'U'),
('Caja', 'Cj'),
('Docena', 'Dz'),
('Metro', 'm'),
('Centímetro', 'cm'),
('Milímetro', 'mm'),
('Pieza', 'Pz'),
('Taza', 'Tz'),
('Cucharada', 'Cda'),
('Cucharadita', 'Cta')

INSERT INTO "INGREDIENTES"("nombre", "medida", "id_unidad", "costo", "proveedor")
VALUES
('Harina', 1, 1, 10, 'Molinos'),
('Azúcar', 1, 1, 10, 'Molinos'),
('Huevos', 1, 1, 10, 'Granja'),
('Leche', 1, 1, 10, 'Granja'),
('Manteca', 1, 1, 10, 'Molinos'),
('Aceite', 1, 1, 10, 'Molinos'),
('Sal', 1, 1, 10, 'Molinos'),
('Polvo de hornear', 1, 1, 10, 'Molinos'),
('Esencia de vainilla', 1, 1, 10, 'Molinos'),
('Chocolate', 1, 1, 10, 'Molinos'),
('Dulce de leche', 1, 1, 10, 'Molinos'),
('Frutas secas', 1, 1, 10, 'Molinos'),
('Frutas frescas', 1, 1, 10, 'Molinos'),
('Crema', 1, 1, 10, 'Molinos'),
('Queso', 1, 1, 10, 'Molinos'),
('Gelatina', 1, 1, 10, 'Molinos'),
('Galletitas', 1, 1, 10, 'Molinos'),
('Levadura', 1, 1, 10, 'Molinos'),
('Frutas en almíbar', 1, 1, 10, 'Molinos'),
('Frutas confitadas', 1, 1, 10, 'Molinos'),
('Frutas glaseadas', 1, 1, 10, 'Molinos'),
('Frutas deshidratadas', 1, 1, 10, 'Molinos'),
('Frutas enlatadas', 1, 1, 10, 'Molinos'),
('Frutas frescas', 1, 1, 10, 'Molinos'),
('Frutas secas', 1, 1, 10, 'Molinos')

INSERT INTO "RECETAS"("nombre", "descripcion", "id_categoria", "precio_personalizado")
VALUES
('Torta de Chocolate', 'Torta de chocolate con relleno de dulce de leche', 1, 100),
('Torta de Vainilla', 'Torta de vainilla con relleno de frutas', 1, 100),
('Torta de Frutas', 'Torta de frutas con relleno de crema', 1, 100),
('Torta de Queso', 'Torta de queso con relleno de frutas', 1, 100),
('Torta de Crema', 'Torta de crema con relleno de frutas', 1, 100),
('Torta de Dulce de Leche', 'Torta de dulce de leche con relleno de frutas', 1, 100),
('Torta de Frutas Secas', 'Torta de frutas secas con relleno de frutas', 1, 100),
('Torta de Frutas Frescas', 'Torta de frutas frescas con relleno de frutas', 1, 100),
('Torta de Frutas en Almíbar', 'Torta de frutas en almíbar con relleno de frutas', 1, 100),
('Torta de Frutas Confitadas', 'Torta de frutas confitadas con relleno de frutas', 1, 100),
('Torta de Frutas Glaseadas', 'Torta de frutas glaseadas con relleno de frutas', 1, 100),
('Torta de Frutas Deshidratadas', 'Torta de frutas deshidratadas con relleno de frutas', 1, 100),
('Torta de Frutas Enlatadas', 'Torta de frutas enlatadas con relleno de frutas', 1, 100),
('Torta de Frutas Frescas', 'Torta de frutas frescas con relleno de frutas', 1, 100),
('Torta de Frutas Secas', 'Torta de frutas secas con relleno de frutas', 1, 100)

INSERT INTO "RECETA_INGREDIENTE"("id_receta", "id_ingrediente", "cantidad")
VALUES
-- Torta de Chocolate
(1, 1, 200),   -- Harina
(1, 2, 100),   -- Azúcar
(1, 3, 3),     -- Huevos
(1, 4, 200),   -- Leche
(1, 10, 150),  -- Chocolate
(1, 11, 100),  -- Dulce de leche
-- Torta de Vainilla
(2, 1, 200),   -- Harina
(2, 2, 100),   -- Azúcar
(2, 3, 3),     -- Huevos
(2, 4, 200),   -- Leche
(2, 9, 5),     -- Esencia de vainilla
(2, 13, 150),  -- Frutas frescas
-- Torta de Frutas
(3, 1, 200),   -- Harina
(3, 2, 100),   -- Azúcar
(3, 3, 3),     -- Huevos
(3, 4, 200),   -- Leche
(3, 14, 150),  -- Crema
(3, 13, 150),  -- Frutas frescas
-- Torta de Queso
(4, 1, 200),   -- Harina
(4, 2, 100),   -- Azúcar
(4, 3, 3),     -- Huevos
(4, 15, 200),  -- Queso
(4, 13, 150),  -- Frutas frescas
-- Torta de Crema
(5, 1, 200),   -- Harina
(5, 2, 100),   -- Azúcar
(5, 3, 3),     -- Huevos
(5, 14, 200),  -- Crema
(5, 13, 150),  -- Frutas frescas
-- Torta de Dulce de Leche
(6, 1, 200),   -- Harina
(6, 2, 100),   -- Azúcar
(6, 3, 3),     -- Huevos
(6, 11, 200),  -- Dulce de leche
(6, 13, 150),  -- Frutas frescas
-- Torta de Frutas Secas
(7, 1, 200),   -- Harina
(7, 2, 100),   -- Azúcar
(7, 3, 3),     -- Huevos
(7, 12, 200),  -- Frutas secas
(7, 13, 150),  -- Frutas frescas
-- Torta de Frutas Frescas
(8, 1, 200),   -- Harina
(8, 2, 100),   -- Azúcar
(8, 3, 3),     -- Huevos
(8, 13, 300),  -- Frutas frescas
-- Torta de Frutas en Almíbar
(9, 1, 200),   -- Harina
(9, 2, 100),   -- Azúcar
(9, 3, 3),     -- Huevos
(9, 19, 300),  -- Frutas en almíbar
-- Torta de Frutas Confitadas
(10, 1, 200),  -- Harina
(10, 2, 100),  -- Azúcar
(10, 3, 3),    -- Huevos
(10, 20, 300), -- Frutas confitadas
-- Torta de Frutas Glaseadas
(11, 1, 200),  -- Harina
(11, 2, 100),  -- Azúcar
(11, 3, 3),    -- Huevos
(11, 21, 300), -- Frutas glaseadas
-- Torta de Frutas Deshidratadas
(12, 1, 200),  -- Harina
(12, 2, 100),  -- Azúcar
(12, 3, 3),    -- Huevos
(12, 22, 300), -- Frutas deshidratadas
-- Torta de Frutas Enlatadas
(13, 1, 200),  -- Harina
(13, 2, 100),  -- Azúcar
(13, 3, 3),    -- Huevos
(13, 23, 300), -- Frutas enlatadas
-- Torta de Frutas Frescas (Duplicado)
(14, 1, 200),  -- Harina
(14, 2, 100),  -- Azúcar
(14, 3, 3),    -- Huevos
(14, 13, 300), -- Frutas frescas
-- Torta de Frutas Secas (Duplicado)
(15, 1, 200),  -- Harina
(15, 2, 100),  -- Azúcar
(15, 3, 3),    -- Huevos
(15, 12, 300); -- Frutas secas

INSERT INTO "SUMINISTROS"("id_categoria", "nombre", "proveedor", "cantidad", "costo")
VALUES
(8, 'Caja de Cartón', 'Cajas S.A.', 100, 50),
(8, 'Caja de Madera', 'Cajas S.A.', 100, 100),
(8, 'Caja de Plástico', 'Cajas S.A.', 100, 75),
(9, 'Detergente', 'Limpieza S.A.', 100, 20),
(9, 'Desinfectante', 'Limpieza S.A.', 100, 30),
(9, 'Jabón', 'Limpieza S.A.', 100, 10),
(10, 'Cintas', 'Decoración S.A.', 100, 5),
(10, 'Globos', 'Decoración S.A.', 100, 10),
(10, 'Papel', 'Decoración S.A.', 100, 15),
(11, 'Horno', 'Equipamiento S.A.', 10, 500),
(11, 'Batidora', 'Equipamiento S.A.', 10, 300),
(11, 'Licuadora', 'Equipamiento S.A.', 10, 200),
(12, 'Bolsas', 'Empaquetado S.A.', 100, 10),
(12, 'Cajas', 'Empaquetado S.A.', 100, 20),
(12, 'Papel', 'Empaquetado S.A.', 100, 15),
(13, 'Azúcar', 'Insumos S.A.', 100, 10),
(13, 'Harina', 'Insumos S.A.', 100, 10),
(13, 'Huevos', 'Insumos S.A.', 100, 10),
(14, 'Tarjetas', 'Tarjetas y Etiquetas S.A.', 100, 5),
(14, 'Etiquetas', 'Tarjetas y Etiquetas S.A.', 100, 5),
(14, 'Stickers', 'Tarjetas y Etiquetas S.A.', 100, 5)