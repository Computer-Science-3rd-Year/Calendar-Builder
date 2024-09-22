# Documento Técnico del Proyecto: Generación de Calendarios con Algoritmos Genéticos

## Introducción

Este proyecto utiliza algoritmos genéticos para explorar un amplio espacio de soluciones, con el objetivo de generar un calendario que satisfaga un conjunto específico de restricciones relacionadas con los eventos a distribuir. 

## Estructura del Proyecto

El proyecto está organizado en dos carpetas principales:

1. **`src`**: Contiene el código fuente del backend del proyecto, desarrollado en .NET.
2. **`geneticAproach`**: Es un subproyecto en .NET que implementa un wrapper para el paquete GeneticSharp, facilitando el uso de algoritmos genéticos de manera general.

## Requisitos para la Ejecución

Para ejecutar el proyecto, es necesario cumplir con los siguientes requisitos:

### Base de Datos

- **Instancia de PostgreSQL**: Se recomienda utilizar PostgreSQL con PostGIS (versión 13-3.1-alpine). 
- **Configuración**: Es esencial configurar la conexión a la base de datos en el archivo `appsettings.json`, que se encuentra en la ruta `src/*Api`.

### Alternativa con Docker

Si se prefiere, se puede utilizar Docker para levantar tanto la base de datos como el backend del proyecto. Para ello:

1. Asegúrate de tener Docker instalado.
2. Navega a la carpeta `src`.
3. Ejecuta el siguiente comando:

   ```bash
   docker-compose up
   ```

Este comando iniciará la base de datos y el proyecto, exponiéndolos en los puertos configurados en el archivo `docker-compose.yml`.

## Conclusión

Este proyecto ofrece una solución innovadora para la generación de calendarios utilizando técnicas avanzadas de optimización. Al emplear algoritmos genéticos, se busca maximizar la satisfacción de las restricciones establecidas, facilitando la organización eficiente de eventos.
