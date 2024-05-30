USE "master";
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'tp-cuatrimestral-grupo-7')
BEGIN
  CREATE DATABASE "tp-cuatrimestral-grupo-7";
END;
GO

USE "tp-cuatrimestral-grupo-7";
GO

 CREATE TABLE "CONTACTOS"(
     "id_contacto" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     "nombre_apellido" VARCHAR(100) NOT NULL,
     "tipo" VARCHAR(20) NOT NULL,
     "correo" VARCHAR(100) NOT NULL,
     "telefono" VARCHAR(20) NOT NULL,
     "fuente" VARCHAR(100),
     "direccion" VARCHAR(200) NOT NULL,
     "producto_que_provee" VARCHAR(100),
     "desea_recibir_correos" BIT NOT NULL DEFAULT 0,
     "desea_recibir_whatsapp" BIT NOT NULL DEFAULT 0,
	   "informacion_personal" VARCHAR(500),

     CONSTRAINT "CK_Contacto_tipo" CHECK ("tipo" IN ('Cliente', 'Proveedor')),
 );

 CREATE TABLE "UNIDADES_MEDIDA"(
    "id_unidad" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    "nombre" VARCHAR(50) NOT NULL,
    "abreviatura" VARCHAR(10) NOT NULL
)

CREATE TABLE "CATEGORIAS"(
    "id_categoria" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    "tipo" VARCHAR(50) NOT NULL,
    "nombre" VARCHAR(50) NOT NULL
)

CREATE TABLE "INGREDIENTES"(
    "id_ingrediente" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    "nombre" VARCHAR(50) NOT NULL,
    "cantidad" FLOAT NOT NULL,
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

CREATE TABLE "DETALLE_RECETAS"(
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

CREATE TABLE "PRODUCTOS"(
    "id_producto" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    "nombre" VARCHAR(50) NOT NULL,
    "descripcion" VARCHAR(200) NOT NULL,
    "porciones" INT NOT NULL,
    "horas_trabajo" DECIMAL NOT NULL,
    "tipo_precio" VARCHAR(50) NOT NULL,
    "valor_precio" DECIMAL NOT NULL,
    "id_categoria" INT NOT NULL,

    FOREIGN KEY ("id_categoria") REFERENCES "CATEGORIAS"("id_categoria")
)

CREATE TABLE "IMAGENES_PRODUCTOS"(
    "id_imagen" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    "id_producto" INT NOT NULL,
    "url" VARCHAR(200) NOT NULL,

    FOREIGN KEY ("id_producto") REFERENCES "PRODUCTOS"("id_producto")
)

CREATE TABLE "DETALLE_PRODUCTOS"(
    "id_producto" INT NOT NULL,
    "id_suministro" INT,
    "id_receta" INT,
    "cantidad" FLOAT NOT NULL,

    FOREIGN KEY ("id_producto") REFERENCES "PRODUCTOS"("id_producto"),
    FOREIGN KEY ("id_suministro") REFERENCES "SUMINISTROS"("id_suministro"),
    FOREIGN KEY ("id_receta") REFERENCES "RECETAS"("id_receta"),
    -- solo puede tener un id_suministro o un id_receta
    CONSTRAINT "CK_Detalle_Productos" CHECK (
        ("id_suministro" IS NOT NULL AND "id_receta" IS NULL) OR
        ("id_suministro" IS NULL AND "id_receta" IS NOT NULL)
    )
)

CREATE TABLE "ORDENES_ESTADOS"(
	"id_orden_estado" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	"nombre" VARCHAR(50) NOT NULL
)

CREATE TABLE "ORDENES_PAGO_ESTADOS"(
	"id_orden_pago_estado" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	"nombre" VARCHAR(50) NOT NULL
)

CREATE TABLE "ORDENES"(
    "id_orden" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    "id_cliente" INT NOT NULL,
    "fecha" DATE NOT NULL,
    "tipo_evento" VARCHAR(50),
    "tipo_entrega" VARCHAR(50) NOT NULL,
    "descripcion" VARCHAR(200),
    "descuento_porcentaje" DECIMAL,
    "costo_envio" DECIMAL,
    "direccion_entrega" VARCHAR(200),
    "hora_entrega" TIME NOT NULL,
    "id_orden_estado" INT DEFAULT 1,
    "id_orden_pago_estado" INT DEFAULT 1,

    FOREIGN KEY ("id_cliente") REFERENCES "CONTACTOS"("id_contacto"),
    FOREIGN KEY ("id_orden_estado") REFERENCES "ORDENES_ESTADOS"("id_orden_estado")
)

CREATE TABLE "DETALLE_ORDENES"(
    "id_orden" INT NOT NULL,
    "id_producto" INT NOT NULL,
    "cantidad" INT NOT NULL,
    "producto_porciones" INT NOT NULL,
    "producto_costo" MONEY NOT NULL,
    "producto_precio" MONEY NOT NULL,

    PRIMARY KEY ("id_orden", "id_producto"),
    FOREIGN KEY ("id_orden") REFERENCES "ORDENES"("id_orden"),
    FOREIGN KEY ("id_producto") REFERENCES "PRODUCTOS"("id_producto"),
    CONSTRAINT "CK_Detalle_Ordenes" CHECK ("cantidad" > 0),
    CONSTRAINT "CK_Detalle_Ordenes_2" CHECK ("producto_porciones" > 0),
)

