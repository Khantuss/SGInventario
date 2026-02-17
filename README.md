SGInventario - Sistema de Gestión de Inventario
* Descripción

SGInventario es una API REST desarrollada en .NET 8 para la gestión de productos.
El proyecto implementa arquitectura por capas, autenticación con JWT, uso de Dapper como micro-ORM y Stored Procedures en SQL Server para el acceso a datos.

Este proyecto fue desarrollado como práctica técnica utilizando el stack:

.NET Core

SQL Server

Dapper

JWT

Swagger

* Tecnologías utilizadas

.NET 8

ASP.NET Core Web API

SQL Server

Dapper

JWT (JSON Web Tokens)

Swagger / OpenAPI

* Arquitectura

El proyecto está organizado siguiendo separación por capas:

SGInventario.Domain

Entidades del sistema (Producto, Usuario)

SGInventario.Application

Interfaces y contratos

SGInventario.Infrastructure

Implementaciones de repositorios con Dapper

SGInventario (API)

Controladores

Configuración JWT

Swagger

Inyección de dependencias

Esta estructura permite mantener una arquitectura limpia y desacoplada.

* Base de Datos
* Crear la base de datos

En SQL Server ejecutar:

CREATE DATABASE SGInventarioDB;
GO


Luego seleccionar la base de datos y ejecutar el archivo:

script_bd.sql


Este archivo incluye:

Creación de tablas:

Usuario

Producto

Creación de procedimientos almacenados:

sp_producto_listar

sp_producto_obtener_por_id

sp_producto_crear

sp_producto_actualizar

sp_producto_eliminar

sp_usuario_obtener_por_username

* Configuración

Abrir el archivo:

appsettings.json


Modificar la cadena de conexión según su entorno:

"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=SGInventarioDB;Trusted_Connection=True;TrustServerCertificate=True;"
}


Configurar también la clave JWT:

"Jwt": {
  "Key": "CLAVE_DE_32_CARACTERES_MINIMO"
}

* Ejecutar el Proyecto

Abrir la solución en Visual Studio.

Establecer el proyecto SGInventario como proyecto de inicio.

Ejecutar (F5 o Ctrl+F5).

Abrir en el navegador:

https://localhost:{puerto}/swagger

* Autenticación
Login

Endpoint:

POST /api/Auth/login


Body de ejemplo:

{
  "username": "admin2",
  "passwordHash": "1234"
}


Respuesta:

{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}

Uso del Token en Swagger

Copiar el token generado.

Hacer clic en el botón Authorize en Swagger.

Escribir:

Bearer {token}


Confirmar.

Ahora los endpoints protegidos podrán ejecutarse.

* Endpoints Principales
Productos

GET /api/Producto

GET /api/Producto/{id}

POST /api/Producto

PUT /api/Producto/{id}

DELETE /api/Producto/{id}

* Seguridad

Autenticación basada en JWT.

Uso de parámetros en Dapper para evitar inyección SQL.

Separación de responsabilidades por capas.

Uso de Stored Procedures para operaciones en base de datos.

* Notas Finales

El proyecto incluye el script SQL necesario para recrear la base de datos.

Implementa arquitectura limpia con separación clara de responsabilidades.

Swagger permite probar fácilmente todos los endpoints.
