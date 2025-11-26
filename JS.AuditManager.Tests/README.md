# Proyecto de Pruebas Unitarias - JS.AuditManager

Este proyecto contiene pruebas unitarias para la API REST de auditorías (Audits) en .NET 9.  
Las pruebas aseguran que los métodos principales de la entidad **Audit** funcionen correctamente.

#Cómo ejecutar las pruebas

1. Abre una terminal en la carpeta raíz de tu solución.
2. Ve a la carpeta del proyecto de pruebas (ejemplo: `JS.AuditManager.Tests`).
3. Ejecuta el siguiente comando: dotnet test

4. El sistema compilará el proyecto y correrá todas las pruebas.

## Qué se está probando

- **CreateAuditAsync** → Verifica que se pueda crear una auditoría con datos válidos.  
- **UpdateAuditAsync** → Verifica que solo se pueda actualizar si la auditoría existe y está en estado Pendiente.  
- **ChangeAuditStatusAsync** → Verifica que el estado de una auditoría se actualice correctamente.  
- **AssignResponsibleAsync** → Verifica que solo se pueda asignar un responsable válido.  
- **GetAuditsByResponsibleAsync** → Verifica que se obtengan las auditorías asignadas a un responsable específico.

## Cómo saber si las pruebas están bien

- Si todo funciona, verás un mensaje como: Passed! - 5 passed, 0 failed

- Si alguna prueba falla, verás: Failed! - 4 passed, 1 failed


