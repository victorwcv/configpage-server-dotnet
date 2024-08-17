# User Settings Page Server .NET

Proyecto web fullstack version para telefonos  moviles con React-TS + Material UI + .NET + MongoDB, 

## Requisitos

Antes de empezar, asegúrate de tener instalados los siguientes programas:

- [.NET SDK](https://dotnet.microsoft.com/download) (versión 8.0 o superior)
- [MongoDB Compass](https://www.mongodb.com/products/compass) (opcional, para verificar la base de datos)

## Configuración del Entorno Local



1.  **Clonar el Repositorio**

    ```bash
     git clone https://github.com/victorwcv/configpage-server-dotnet.git
     cd configpage-server-dotnet
    ```

2.  **Instalar Dependencias**

    ```bash
      dotnet restore
    ```

3. **Crear una base de datos**

    Crea una base de datos MongoDB en la nube, tu string de conexion debe tener este patron:

    ```bash
      mongodb+srv://<tuUsuario>:<tuContrasena>@cluster0.nclpt.mongodb.net/usuarios?retryWrites=true&w=majority&appName=Cluster0
    ```

4.  **Configurar Variables de Entorno**

    Crea un archivo .env en la raiz del proyecto y agrega lo siguiente:

    ```bash
      MONGO_CONNECTION_STRING= *tu string de conexion*
      ENVIROMENT=development
    ```

5. **Iniciar el servidor**
    ```bash
      dotnet run
    ```

6. **Crear un usuario en la BD**

    Usando alguna herramienta para uso de endpoints como postman realiza una solicitud http POST que contenga en el cuerpo la siguiente estructura JSON:
    Si todo sale bien el Servidor te devolvera el usuario creado incluyendo una id.

    - Endpoint


    ```bash
        http://localhost:5163/api/user
    ```
    - JSON
    ```json
      {
        "Name": "Jane Smith",
        "Username": "janesmith",
        "Password": "SecurePass456",
        "Email": "janesmith@example.com",
        "PhoneNumber": "+0987654321"
      }
    ```

7. **Verificar la Conexión a MongoDB y la creacion del Usuario**

     Puedes verificar la conexión y creacion del usuario usando MongoDB Compass.

8. **Docker**

    Opcionalmente puedes Iniciar el servidor en Docker el Repositorio cuenta con la configuracion necesaria en el Dockerfile.

## Versión Desplegada

Existe una versión desplegada de este proyecto que puedes consultar en el siguiente enlace:

-**Frontend:** [User Settings Page](https://conifgpage.onrender.com/)  
-**Backend:** [API de ConfigPage](https://configpage-server-dotnet.onrender.com/)

