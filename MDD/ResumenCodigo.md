---
title: codigo C#
markmap:
        colorFreezeLevel: 2
---

## Codigo
- `<#@ template inherits="Microsoft.VisualStudio.TextTemplating.VSHost.ModelingTextTransformation"#>`
- `<#@ output extension= "">`
- `<#       #>` Como metodo

## Herencia
- `if (~~intancia a comprobar~~ is ~~classeModelo~~)`
- Para acceder a las propiedades de la clase hija:
    `claseHija x = (claseHija)x`

## Comprobar cardinalidades
- 0...1
    `if( this.x != null)`
- 0...n
    `if( this.x.count > 0)`

## Recorrer instancias
- `foreach( A a in this.B )`

## Acceso a Domain propertis de una DomainRelationship
![Metamodelo](Fotos/DomainRelation.png)
![Codigo](Fotos/CodeDomainRelation.png)

## Inyectar codigo
- `<#=   #>` Se escribe lo que se diga tal cual en la plantilla

## Salida de codigo en diferentes archivos de salida
- `<#@ template inherits="Microsoft.VisualStudio.textTemplating.VSHost.ModelingTextTransformation" language="C#" hostspecific="True"#>` Al principio 
- Definimos una variable para la creación de los ficheros
 `<# var fileManager=EntityFrameworkTemplateFileManager.Create(this); `
 Crear el archivo:
 `fileManager.StartNewFile(nodo.nombre + "txt");`
 Cerrar proceso de creacion
 `fileManager.Process() #>`

## Cosas varias
- Source Fuente  
- Target Objetivo
- `            ------`
 `Target -----|      |----- Source`

![](Fotos/ST.PNG)
 