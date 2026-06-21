-- ============================================================
-- Script para actualizar ImagenUrl en Productos y Emprendimientos
-- EmprendeMarketDB - PostgreSQL
-- ============================================================

-- PRODUCTOS (por nombre, mapeado desde linksImagenes.txt)
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/736x/10/56/e5/1056e58d4214a00a1547167e2605b45e.jpg' WHERE "Nombre" = 'Saco de Lana';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/736x/54/d2/17/54d217eac737599e3cd63c0677b8c50f.jpg' WHERE "Nombre" = 'Bufanda Infinita';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/736x/de/91/79/de91797e1faf8f96f68b419f852f4b25.jpg' WHERE "Nombre" = 'Torta de Chocolate';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/1200x/06/e2/ee/06e2ee729543168e9d1d694af3e7d7d8.jpg' WHERE "Nombre" = 'Jarrón Rústico';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/1200x/48/c3/14/48c31431fb8b563ba065751c77a62a52.jpg' WHERE "Nombre" = 'Taza Cerámica';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/736x/76/92/91/769291c0e153b016b29cf7a6adcc5387.jpg' WHERE "Nombre" = 'Mermelada de Fresa';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/736x/98/65/33/986533b73ff7a02e900df76c3cf729ba.jpg' WHERE "Nombre" = 'Dulce de Leche';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/736x/26/f4/07/26f4072307de7abad02f609aacde23fe.jpg' WHERE "Nombre" = 'Soporte Laptop';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/736x/dd/ca/81/ddca81a8bc2b0118ecb3ea5027bed101.jpg' WHERE "Nombre" = 'Cargador Solar';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/1200x/11/22/0c/11220c31dc63827421fdc1585cc4ee26.jpg' WHERE "Nombre" = 'Camiseta Algodón';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/736x/34/98/1d/34981d0f2541f5f21481629bf0c15d42.jpg' WHERE "Nombre" = 'Vestido Verano';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/736x/1e/29/fa/1e29fa38faaa0edbac974c8170c98bf1.jpg' WHERE "Nombre" = 'Jabón de Avena';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/1200x/a7/e3/5d/a7e35dba3a5fa845f92ccac1f8d60781.jpg' WHERE "Nombre" = 'Crema de Rosas';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/1200x/ad/11/48/ad1148f5c891ec18d7c20628f64a41b3.jpg' WHERE "Nombre" = 'Estante Pared';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/736x/21/c6/c2/21c6c291aa1465d8c4a0d3ff1b76bdfd.jpg' WHERE "Nombre" = 'Lámpara Tronco';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/1200x/21/32/99/2132994396676ee2be461131fbfc8d91.jpg' WHERE "Nombre" = 'Plato Barro';
UPDATE "Productos" SET "ImagenUrl" = 'https://i.pinimg.com/1200x/3b/da/b8/3bdab85b7ceca5cf8141b3de032078fc.jpg' WHERE "Nombre" = 'Gelatina de 2 capas';

-- Productos restantes del seed (sin link en el txt, usando imágenes similares de Unsplash/Pexels)
UPDATE "Productos" SET "ImagenUrl" = 'https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=400&auto=format' WHERE "Nombre" = 'Corrección Tesis';
UPDATE "Productos" SET "ImagenUrl" = 'https://images.unsplash.com/photo-1618160702438-9b02ab6515c9?w=400&auto=format' WHERE "Nombre" = 'Alfajores Box';
UPDATE "Productos" SET "ImagenUrl" = 'https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=400&auto=format' WHERE "Nombre" = 'Teclado Madera';
UPDATE "Productos" SET "ImagenUrl" = 'https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=400&auto=format' WHERE "Nombre" = 'Bolso Tela';
UPDATE "Productos" SET "ImagenUrl" = 'https://images.unsplash.com/photo-1526045612212-70caf35c14df?w=400&auto=format' WHERE "Nombre" = 'Aceite Coco';
UPDATE "Productos" SET "ImagenUrl" = 'https://images.unsplash.com/photo-1618160702438-9b02ab6515c9?w=400&auto=format' WHERE "Nombre" = 'Espejo Vintage';
UPDATE "Productos" SET "ImagenUrl" = 'https://images.unsplash.com/photo-1455390582262-044cdead277a?w=400&auto=format' WHERE "Nombre" = 'Redacción Blog';

-- ============================================================
-- EMPRENDIMIENTOS (imágenes temáticas de Unsplash)
-- ============================================================

-- Arte & Barro (Id=1) — cerámica artesanal
UPDATE "Emprendimientos" SET "ImagenUrl" = 'https://images.unsplash.com/photo-1565193566173-7a0ee3dbe261?w=600&auto=format' WHERE "Nombre" = 'Arte & Barro';

-- Sabores de Antaño (Id=2) — mermeladas / comida tradicional
UPDATE "Emprendimientos" SET "ImagenUrl" = 'https://images.unsplash.com/photo-1504754524776-8f4f37790ca0?w=600&auto=format' WHERE "Nombre" = 'Sabores de Antaño';

-- Innovatech (Id=3) — tecnología / gadgets
UPDATE "Emprendimientos" SET "ImagenUrl" = 'https://images.unsplash.com/photo-1518770660439-4636190af475?w=600&auto=format' WHERE "Nombre" = 'Innovatech';

-- EcoModa (Id=4) — moda sostenible / tela
UPDATE "Emprendimientos" SET "ImagenUrl" = 'https://images.unsplash.com/photo-1523381210434-271e8be1f52b?w=600&auto=format' WHERE "Nombre" = 'EcoModa';

-- Hierba Buena (Id=5) — productos naturales / jabones
UPDATE "Emprendimientos" SET "ImagenUrl" = 'https://images.unsplash.com/photo-1607006483224-a2da9f78e0f4?w=600&auto=format' WHERE "Nombre" = 'Hierba Buena';

-- Muebles con Alma (Id=6) — madera / decoración rústica
UPDATE "Emprendimientos" SET "ImagenUrl" = 'https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=600&auto=format' WHERE "Nombre" = 'Muebles con Alma';

-- Punto & Coma (Id=7) — escritura / redacción
UPDATE "Emprendimientos" SET "ImagenUrl" = 'https://images.unsplash.com/photo-1455390582262-044cdead277a?w=600&auto=format' WHERE "Nombre" = 'Punto & Coma';
