-- Crear base de datos
CREATE DATABASE IF NOT EXISTS ferrooriente CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE ferrooriente;

-- Tabla de productos base
CREATE TABLE IF NOT EXISTS productos (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(150) NOT NULL,
  precio DECIMAL(10,2) NOT NULL,
  stock INT NOT NULL,
  imagen_url VARCHAR(300)
);

-- Tabla de ventas
CREATE TABLE IF NOT EXISTS ventas (
  id INT AUTO_INCREMENT PRIMARY KEY,
  fecha DATETIME NOT NULL,
  usuario VARCHAR(120) NOT NULL,
  total DECIMAL(10,2) NOT NULL
);

-- Tabla de detalle de venta
CREATE TABLE IF NOT EXISTS detalle_venta (
  id INT AUTO_INCREMENT PRIMARY KEY,
  id_venta INT NOT NULL,
  id_producto INT NOT NULL,
  cantidad INT NOT NULL,
  precio_unitario DECIMAL(10,2) NOT NULL,
  subtotal DECIMAL(10,2) NOT NULL,
  FOREIGN KEY (id_venta) REFERENCES ventas(id),
  FOREIGN KEY (id_producto) REFERENCES productos(id)
);

-- Ejemplo de inserción de producto
INSERT INTO productos (nombre, precio, stock, imagen_url) VALUES
('Martillo de acero', 150.00, 30, 'imagenes/martillo.jpg'),
('Taladro percutor', 1250.00, 10, 'imagenes/taladro.jpg');
