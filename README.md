# API Seguridad Ciudadana (.NET)

Esta API, desarrollada con .NET, proporciona endpoints para gestionar información relacionada con la seguridad ciudadana. Permite el registro de policías, cuentas de usuarios y lugares seguros. Su objetivo es permitir que los policías validen los lugares y que una aplicación en el frontend pueda consumir los datos para mostrar los lugares en un mapa, clasificándolos según su nivel de peligro.

## Características

- Registro y gestión de policías.
- Registro y gestión de cuentas de usuarios.
- Registro y gestión de lugares seguros.
- Clasificación de lugares según su nivel de peligro.

## Endpoints Disponibles

- `/api/policia`: Endpoints relacionados con la gestión de policías.
- `/api/usuario`: Endpoints relacionados con la gestión de cuentas de usuarios.
- `/api/lugar`: Endpoints relacionados con la gestión de lugares seguros.

## Documentación de API

Para obtener detalles sobre cómo utilizar cada endpoint y los parámetros requeridos, consulte la documentación de la API proporcionada en la ruta `/docs` una vez que la API esté en funcionamiento.

## Instalación y Uso

1. Clona este repositorio: `git clone https://github.com/tu-usuario/ApiSeguridadCiudadana.git`
2. Instala las dependencias: `dotnet restore`
3. Configura las variables de entorno en un archivo `.env` según sea necesario.
4. Inicia el servidor: `dotnet run`
5. La API estará disponible en `http://localhost:5000`.

## Contribuciones

Las contribuciones son bienvenidas. Si desea mejorar esta API, realice un fork del repositorio, realice los cambios y envíe un pull request. Asegúrese de seguir las pautas de contribución del proyecto.

