-- CREATE DATABASE "tp_cuatrimestral_pasteleria";
-- GO

-- USE "tp_cuatrimestral_pasteleria";
-- GO


-- CREATE TABLE "Contacto"(
--     "id_contacto" INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
--     "nombre_apellido" VARCHAR(100) NOT NULL,
--     "tipo" VARCHAR(20) NOT NULL,
--     "correo" VARCHAR(100) NOT NULL,
--     "telefono" VARCHAR(20) NOT NULL,
--     "fuente" VARCHAR(100),
--     "direccion" VARCHAR(200) NOT NULL,
--     "producto_que_provee" VARCHAR(100),
--     "desea_recibir_correos" BIT NOT NULL DEFAULT 0,
--     "desea_recibir_whatsapp" BIT NOT NULL DEFAULT 0,

--     CONSTRAINT "CK_Contacto_tipo" CHECK ("tipo" IN ('Cliente', 'Proveedor')),
-- );


-- INSERT INTO "Contacto" (
--     "nombre_apellido",
--     "tipo",
--     "correo",
--     "telefono",
--     "fuente",
--     "direccion",
--     "producto_que_provee",
--     "desea_recibir_correos",
--     "desea_recibir_whatsapp"
-- ) VALUES
-- ('Juan Pérez', 'Cliente', 'juan.perez@example.com', '555-1234', 'Referencia', 'Calle Falsa 123', NULL, 1, 0),
-- ('María López', 'Cliente', 'maria.lopez@example.com', '555-5678', 'Internet', 'Avenida Siempre Viva 742', NULL, 0, 1),
-- ('Pedro García', 'Proveedor', 'pedro.garcia@example.com', '555-8765', 'Evento', 'Calle Luna 15', 'Productos de limpieza', 1, 1),
-- ('Ana Martínez', 'Cliente', 'ana.martinez@example.com', '555-4321', 'Referencia', 'Boulevard del Sol 87', NULL, 0, 0),
-- ('Carlos Rodríguez', 'Proveedor', 'carlos.rodriguez@example.com', '555-9876', 'Internet', 'Calle Estrella 99', 'Suministros de oficina', 1, 0),
-- ('Laura González', 'Cliente', 'laura.gonzalez@example.com', '555-6543', 'Evento', 'Avenida del Mar 54', NULL, 0, 1),
-- ('Jorge Fernández', 'Cliente', 'jorge.fernandez@example.com', '555-3210', 'Referencia', 'Calle Rio 25', NULL, 1, 1),
-- ('Sofía Ruiz', 'Proveedor', 'sofia.ruiz@example.com', '555-0987', 'Internet', 'Boulevard Central 12', 'Material de construcción', 0, 0),
-- ('Luis Hernández', 'Cliente', 'luis.hernandez@example.com', '555-7654', 'Evento', 'Avenida Las Palmas 78', NULL, 1, 0),
-- ('Carmen Jiménez', 'Proveedor', 'carmen.jimenez@example.com', '555-2109', 'Referencia', 'Calle Primavera 34', 'Equipos de cocina', 0, 1),
-- ('Miguel Díaz', 'Cliente', 'miguel.diaz@example.com', '555-3456', 'Internet', 'Avenida del Parque 22', NULL, 1, 1),
-- ('Elena Morales', 'Cliente', 'elena.morales@example.com', '555-6547', 'Evento', 'Calle del Bosque 11', NULL, 0, 0),
-- ('Raúl Ortega', 'Proveedor', 'raul.ortega@example.com', '555-7890', 'Referencia', 'Avenida Principal 45', 'Productos electrónicos', 1, 0),
-- ('Isabel Santos', 'Cliente', 'isabel.santos@example.com', '555-4567', 'Internet', 'Calle Flor 20', NULL, 0, 1),
-- ('Andrés Castro', 'Cliente', 'andres.castro@example.com', '555-6789', 'Evento', 'Boulevard Jardín 30', NULL, 1, 1),
-- ('Patricia Ramírez', 'Proveedor', 'patricia.ramirez@example.com', '555-1235', 'Referencia', 'Avenida del Lago 98', 'Ropa y accesorios', 0, 0),
-- ('José Reyes', 'Cliente', 'jose.reyes@example.com', '555-9875', 'Internet', 'Calle Norte 55', NULL, 1, 0),
-- ('Marta Herrera', 'Proveedor', 'marta.herrera@example.com', '555-8764', 'Evento', 'Avenida Sur 67', 'Juguetes', 0, 1),
-- ('Roberto Castro', 'Cliente', 'roberto.castro@example.com', '555-7653', 'Referencia', 'Boulevard Este 23', NULL, 1, 1),
-- ('Lucía Domínguez', 'Proveedor', 'lucia.dominguez@example.com', '555-6542', 'Internet', 'Calle Oeste 89', 'Material de oficina', 0, 0);

-- select * from Contacto;