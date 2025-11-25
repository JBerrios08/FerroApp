# Conexión MySQL (XAMPP) paso a paso

1. **Instalar paquete NuGet MySql.Data**
   - En Visual Studio: `Herramientas > Administrador de paquetes NuGet > Consola del Administrador de paquetes`.
   - Ejecuta: `Install-Package MySql.Data -Version 8.0.33`.
   - Confirmar que se agrega la referencia en `packages.config` y el ensamblado en el proyecto.

2. **Cadena de conexión en Web.config**
   ```xml
   <connectionStrings>
     <add name="MySqlConnection"
          connectionString="Server=localhost;Port=3306;Database=ferrooriente;Uid=tu_usuario;Pwd=tu_contrasena;SslMode=None;CharSet=utf8mb4;"
          providerName="MySql.Data.MySqlClient" />
   </connectionStrings>
   ```
   - Cambia `tu_usuario` y `tu_contrasena` por las credenciales de XAMPP.
   - Si usas otro puerto en MySQL, actualiza `Port`.

3. **Clase de conexión reutilizable**
   - Archivo: `App_Code/ConexionBD.cs`.
   - Uso:
   ```csharp
   using (var conexion = ConexionBD.ObtenerConexion())
   {
       conexion.Open();
       // operaciones
   }
   ```

4. **Ejemplo de SELECT**
   ```csharp
   using (var conexion = ConexionBD.ObtenerConexion())
   using (var cmd = ConexionBD.CrearComando("SELECT * FROM productos", conexion))
   {
       conexion.Open();
       using (var reader = cmd.ExecuteReader())
       {
           while (reader.Read())
           {
               var nombre = reader.GetString("nombre");
               var precio = reader.GetDecimal("precio");
           }
       }
   }
   ```

5. **Ejemplo de INSERT**
   ```csharp
   using (var conexion = ConexionBD.ObtenerConexion())
   using (var cmd = ConexionBD.CrearComando("INSERT INTO productos(nombre, precio, stock) VALUES(@n, @p, @s)", conexion))
   {
       cmd.Parameters.AddWithValue("@n", "Martillo");
       cmd.Parameters.AddWithValue("@p", 150.00m);
       cmd.Parameters.AddWithValue("@s", 30);
       conexion.Open();
       cmd.ExecuteNonQuery();
   }
   ```

6. **Ejemplo de UPDATE**
   ```csharp
   using (var conexion = ConexionBD.ObtenerConexion())
   using (var cmd = ConexionBD.CrearComando("UPDATE productos SET stock = stock - @cantidad WHERE id = @id", conexion))
   {
       cmd.Parameters.AddWithValue("@cantidad", 2);
       cmd.Parameters.AddWithValue("@id", 1);
       conexion.Open();
       cmd.ExecuteNonQuery();
   }
   ```

7. **Probar conexión con XAMPP**
   - Inicia MySQL en el panel de XAMPP.
   - Verifica usuario/contraseña en `phpMyAdmin > Usuarios`.
   - Si hay errores de SSL, agrega `SslMode=None`.
