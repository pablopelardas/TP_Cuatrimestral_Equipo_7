USE "tp-cuatrimestral-grupo-7";
GO

--  FEED CONTACTOS
 INSERT INTO "CONTACTOS" (
     "nombre_apellido",
     "tipo",
     "correo",
     "telefono",
     "fuente",
     "direccion",
     "producto_que_provee",
     "desea_recibir_correos",
     "desea_recibir_whatsapp",
     "informacion_personal"
 ) VALUES
 ('Juan Pérez', 'Cliente', 'juan.perez@example.com', '555-1234', 'Referencia', 'Calle Falsa 123', NULL, 1, 0, '<div><h1>Template de info personal</h1><p>Hola soy Juan perez</p></div>'),
 ('María López', 'Cliente', 'maria.lopez@example.com', '555-5678', 'Internet', 'Avenida Siempre Viva 742', NULL, 0, 1, null),
 ('Pedro García', 'Proveedor', 'pedro.garcia@example.com', '555-8765', 'Evento', 'Calle Luna 15', 'Productos de limpieza', 1, 1,null),
 ('Ana Martínez', 'Cliente', 'ana.martinez@example.com', '555-4321', 'Referencia', 'Boulevard del Sol 87', NULL, 0, 0, null),
 ('Carlos Rodríguez', 'Proveedor', 'carlos.rodriguez@example.com', '555-9876', 'Internet', 'Calle Estrella 99', 'Suministros de oficina', 1, 0, null),
 ('Laura González', 'Cliente', 'laura.gonzalez@example.com', '555-6543', 'Evento', 'Avenida del Mar 54', NULL, 0, 1, null),
 ('Jorge Fernández', 'Cliente', 'jorge.fernandez@example.com', '555-3210', 'Referencia', 'Calle Rio 25', NULL, 1, 1, null),
 ('Sofía Ruiz', 'Proveedor', 'sofia.ruiz@example.com', '555-0987', 'Internet', 'Boulevard Central 12', 'Material de construcción', 0, 0, null),
 ('Luis Hernández', 'Cliente', 'luis.hernandez@example.com', '555-7654', 'Evento', 'Avenida Las Palmas 78', NULL, 1, 0, null),
 ('Carmen Jiménez', 'Proveedor', 'carmen.jimenez@example.com', '555-2109', 'Referencia', 'Calle Primavera 34', 'Equipos de cocina', 0, 1, null),
 ('Miguel Díaz', 'Cliente', 'miguel.diaz@example.com', '555-3456', 'Internet', 'Avenida del Parque 22', NULL, 1, 1, null),
 ('Elena Morales', 'Cliente', 'elena.morales@example.com', '555-6547', 'Evento', 'Calle del Bosque 11', NULL, 0, 0, null),
 ('Raúl Ortega', 'Proveedor', 'raul.ortega@example.com', '555-7890', 'Referencia', 'Avenida Principal 45', 'Productos electrónicos', 1, 0, null),
 ('Isabel Santos', 'Cliente', 'isabel.santos@example.com', '555-4567', 'Internet', 'Calle Flor 20', NULL, 0, 1, null),
 ('Andrés Castro', 'Cliente', 'andres.castro@example.com', '555-6789', 'Evento', 'Boulevard Jardín 30', NULL, 1, 1, null),
 ('Patricia Ramírez', 'Proveedor', 'patricia.ramirez@example.com', '555-1235', 'Referencia', 'Avenida del Lago 98', 'Ropa y accesorios', 0, 0,null),
 ('José Reyes', 'Cliente', 'jose.reyes@example.com', '555-9875', 'Internet', 'Calle Norte 55', NULL, 1, 0, null),
 ('Marta Herrera', 'Proveedor', 'marta.herrera@example.com', '555-8764', 'Evento', 'Avenida Sur 67', 'Juguetes', 0, 1, null),
 ('Roberto Castro', 'Cliente', 'roberto.castro@example.com', '555-7653', 'Referencia', 'Boulevard Este 23', NULL, 1, 1, null),
 ('Lucía Domínguez', 'Proveedor', 'lucia.dominguez@example.com', '555-6542', 'Internet', 'Calle Oeste 89', 'Material de oficina', 0, 0, null);

--  select * from Contactos;
GO

-- Inserción en UNIDADES DE MEDIDA
INSERT INTO "UNIDADES_MEDIDA"(nombre, abreviatura)
VALUES
('Kilogramo', 'Kg'),
('Litro', 'L'),
('Unidad', 'U'),
('Metro', 'M'),
('Centímetro', 'Cm'),
('Mililitro', 'Ml'),
('Gramo', 'G'),
('Miligramo', 'Mg'),
('Pieza', 'Pz'),
('Botella', 'Bot'),
('Caja', 'Cj'),
('Bolsa', 'Bol'),
('Paquete', 'Pqt'),
('Docena', 'Doc'),
('Cucharada', 'Cda'),
('Cucharadita', 'Cdt'),
('Taza', 'Tza'),
('Copa', 'Cop'),
('Vaso', 'Vas')
GO


-- Inserción en CATEGORIAS
INSERT INTO "CATEGORIAS"(tipo, nombre)
VALUES
('Receta', 'Torta'),
('Receta', 'Tarta'),
('Receta', 'Cupcakes'),
('Receta', 'Galletas'),
('Receta', 'Panadería'),
('Receta', 'Pastelería'),
('Receta', 'Otros'),
('Suministro', 'Insumos'),
('Suministro', 'Decoración'),
('Suministro', 'Cajas'),
('Suministro', 'Tarjetas y Etiquetas'),
('Suministro', 'Otros')
GO

-- Inserción en INGREDIENTES
INSERT INTO "INGREDIENTES" (nombre, cantidad, id_unidad, costo, proveedor)
VALUES
('Harina', 10, 1, 20.00, 'Proveedor A'),
('Azúcar', 5, 1, 10.00, 'Proveedor B'),
('Mantequilla', 2, 1, 50.00, 'Proveedor C'),
('Huevos', 12, 14, 30.00, 'Proveedor D'),
('Leche', 3, 2, 15.00, 'Proveedor E');
GO

-- Inserción en RECETAS
INSERT INTO "RECETAS" (nombre, descripcion, id_categoria, precio_personalizado)
VALUES
('Torta de Chocolate', 'Deliciosa torta de chocolate', 1, 500.00),
('Tarta de Manzana', 'Tarta con relleno de manzana', 2, 400.00);
GO

-- Inserción en DETALLE_RECETAS
INSERT INTO "DETALLE_RECETAS" (id_receta, id_ingrediente, cantidad)
VALUES
(1, 1, 2),
(1, 2, 1),
(1, 3, 0.5),
(2, 1, 1),
(2, 2, 0.5),
(2, 5, 1);
GO

-- Inserción en SUMINISTROS
INSERT INTO "SUMINISTROS" (id_categoria, nombre, proveedor, cantidad, costo)
VALUES
(8, 'Caja para Torta', 'Proveedor X', 50, 200.00),
(9, 'Decoración de Chocolate', 'Proveedor Y', 30, 100.00);
GO

-- Inserción en PRODUCTOS
INSERT INTO "PRODUCTOS" (nombre, descripcion, porciones, horas_trabajo, tipo_precio, valor_precio, id_categoria)
VALUES
('Torta de Cumpleaños', 'Torta decorada para cumpleaños', 20, 5, 'Precio fijo', 800.00, 1),
('Cupcakes de Vainilla', 'Deliciosos cupcakes de vainilla', 12, 3, 'Precio por porción', 50.00, 3);
GO

-- Inserción en DETALLE_PRODUCTOS
INSERT INTO "DETALLE_PRODUCTOS" (id_producto, id_suministro, id_receta, cantidad)
VALUES
(1, 1, NULL, 1),
(1, NULL, 1, 1),
(2, 2, NULL, 1),
(2, NULL, 2, 1);
GO

-- Inserción en ORDENES_ESTADO

INSERT INTO "ORDENES_ESTADOS" (nombre)
VALUES
('PENDIENTE'),
('ENTREGADO')


INSERT INTO "ORDENES_PAGO_ESTADOS" (nombre)
VALUES
('SIN PAGOS'),
('PARCIALMENTE PAGADO'),
('TOTALMENTE PAGADO')


-- Inserción en TIPOS_EVENTOS
INSERT INTO "TIPOS_EVENTOS" (nombre)
VALUES
('Cumpleaños'),
('Boda'),
('Aniversario'),
('Baby Shower'),
('Graduación'),
('Otro');

-- Inserción en EVENTOS
INSERT INTO "EVENTOS" (fecha, id_cliente, id_tipo_evento)
VALUES
('2024-05-01', 1, 1),
('2024-06-15', 2, 2);



-- Inserción en ORDENES
INSERT INTO "ORDENES" (id_cliente, tipo_entrega, descripcion, descuento_porcentaje, costo_envio, direccion_entrega, hora_entrega, id_evento)
VALUES
(1, 'R', 'Torta de cumpleaños personalizada', 10.00, 250.00, 'Av Sinnombre 123, Sinnombre, Argentia','15:55', 1),
(2, 'D', 'Tarta de boda', NULL, NULL, NULL, '16:00', 2);
GO


-- Inserción en DETALLE_ORDENES
INSERT INTO "DETALLE_ORDENES" (id_orden, id_producto, cantidad, producto_porciones, producto_costo, producto_precio)
VALUES
(1, 1, 1, 20, 600.00, 800.00),
(2, 2, 24, 12, 720.00, 1200.00);
GO

