<#@ template inherits="Microsoft.VisualStudio.TextTemplating.VSHost.ModelingTextTransformation" language="C#" hostspecific="True" #>
<#@ output extension=".html" #>
<#@ AMOAMRMAOPProyectoIPS processor="AMOAMRMAOPProyectoIPSDirectiveProcessor" requires="fileName='Sample.AMOAMRMAOP_DLSProyIPS'" #>
<#@ include file="EF.utility.CS.ttinclude" #>

<#
var fileManager = EntityFrameworkTemplateFileManager.Create(this);
if (this.Tapiz.Elements.Count > 0) {

    List<string> listaAsociaciones = new List<string>();
    //Asociaciones
    foreach (Clase claseOClaseE in this.Tapiz.Elements) {
        if (claseOClaseE.AsociacionOrigen.Count > 0){
            foreach(Asociacion rel in Asociacion.GetLinksToAsociacionOrigen(claseOClaseE)) {
                if (comprobarCardinalidad(rel.CardinalidadOrigen.ToString()) &&  comprobarCardinalidad(rel.CardinalidadDestino.ToString())) {
                    string nombre = rel.Nombre;
                    AtributoID atrib1 = null;
                    AtributoID atrib2 = null;
                    listaAsociaciones.Add(nombre);
                    if (claseOClaseE.AtributoID != null){
                        atrib1 = claseOClaseE.AtributoID;
                    }

                    if (claseOClaseE.AsociacionOrigen.FirstOrDefault().AtributoID != null){
                        atrib2 = claseOClaseE.AtributoID;
                    }
                    fileManager.StartNewFile(nombre + ".html");
                    creacionHomeAsociacion(nombre);
                    fileManager.StartNewFile(nombre + "Listado.html");
                    creacionListadoAsociacion(nombre);

                    if (atrib1 == null && atrib2 == null) {  
                        fileManager.StartNewFile(nombre + "Alta.html");
                        creacionAltaAsociacion(nombre, null, null);
                    } else if (atrib1 == null) {
                        fileManager.StartNewFile(nombre + "Alta.html");
                        creacionAltaAsociacion(nombre, null, atrib2.Nombre);
                    } else if (atrib2 == null) {
                        fileManager.StartNewFile(nombre + "Alta.html");
                        creacionAltaAsociacion(nombre, atrib1.Nombre, null);
                    } else {
                        fileManager.StartNewFile(nombre + "Alta.html");
                        creacionAltaAsociacion(nombre, atrib1.Nombre, atrib2.Nombre);
                    }
                    
                    
                }
            }
        }
    }

    //Menu linkeado
    fileManager.StartNewFile("menu.html");
    WriteLine("<html>\n<head>\n\t<title>Menú</title>");
    WriteLine("\t<style>");
    WriteLine("\t\tbody { font-family: 'Arial', sans-serif; margin: 0; padding: 0; background-color: #f4f4f4; }");
    WriteLine("\t\t.container { width: 80%; margin: 0 auto; padding: 20px; background-color: #fff; box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.1); }");
    WriteLine("\t\th1 { text-align: center; color: #333; }");
    WriteLine("\t\tul { list-style: none; padding: 0; }");
    WriteLine("\t\tli { margin-bottom: 15px; }");
    WriteLine("\t\tli a { text-decoration: none; color: #0066cc; font-size: 20px; transition: color 0.3s ease-in-out; }");
    WriteLine("\t\tli a:hover { color: #004080; }");
    WriteLine("\t</style>");
    WriteLine("</head>");
    WriteLine("<body>");
    WriteLine("\t<div class=\"container\">");
    WriteLine("\t\t<h1>Menú</h1>");
    WriteLine("\t\t<ul>");

    foreach (Clase claseOClaseE in this.Tapiz.Elements) {
        if (!isClasePadreAbstracta(claseOClaseE)) {
            string fileName = claseOClaseE.Nombre;
            if (claseOClaseE is ClaseEnriquecida claseE && !fileName.ToLower().Contains("enriquecida")) {
                fileName += "Enriquecida";
            }
            WriteLine("\t\t\t<li><a href=\"" + fileName + ".html\">" + fileName + "</a></li>");
        }
    }

    if (listaAsociaciones.Count > 0){
        foreach(string nombre in listaAsociaciones){
            WriteLine("\t<li><a href=\"" + nombre + ".html\">" + nombre + "</a></li>");
        }
    }

    WriteLine("\t\t</ul>");
    WriteLine("\t</div>");
    WriteLine("</body>");
    WriteLine("</html>");

    foreach (Clase claseOClaseE in this.Tapiz.Elements){
        
        if (!isClasePadreAbstracta(claseOClaseE)) {
            //Home clase
            string fileName = claseOClaseE.Nombre;
            if (claseOClaseE is ClaseEnriquecida claseE && !fileName.ToLower().Contains("enriquecida")) {
                fileName += "Enriquecida";
            }    
            fileManager.StartNewFile(fileName + ".html"); // Corregir extensión del archivo HTML
            WriteLine("<html>\n<head>\n\t<title>" + fileName + "</title>\n</head>");
            WriteLine("<body>\n<div style='border:1px solid black; margin:20px'>"); // Corregir el estilo del borde
            if (claseOClaseE is ClaseEnriquecida && (claseOClaseE as ClaseEnriquecida).EstiloClase != null) {
                EstiloClase estilo = (claseOClaseE as ClaseEnriquecida).EstiloClase.FirstOrDefault();
                string colorLetra = formatoColor(estilo.ColorLetra.ToString());
                string colorFondo = formatoColor(estilo.ColorFondo.ToString());
                string alineacion = formatoAlineacion(estilo.Alineacion.ToString());
                WriteLine("<div style='background-color:" + colorFondo + ";color:" + colorLetra + ";font-family:" + estilo.TipoLetra.ToString() + ";text-align:" + alineacion + "'>");
            } else {
                WriteLine("<div style='background-color:white;color:black;font-family:Arial;text-align:center;'>");
            }
        
            WriteLine("<h1>" + fileName + "</h1>");
            if (claseOClaseE is ClaseEnriquecida clEn && (claseOClaseE as ClaseEnriquecida).EstiloOperacioned != null) {
                EstiloOperacion estilo = (claseOClaseE as ClaseEnriquecida).EstiloOperacioned.FirstOrDefault();
                string colorLetra = formatoColor(estilo.ColorLetra.ToString());
                string formatoEstilo = "style='color:" + colorLetra + ";font-family:" + estilo.TipoLetra.ToString() + "'";
                WriteLine("<button " + formatoEstilo + ">\n<a href=\"./" + fileName + "Alta.html\"" + ">Alta</a>\n</button>"); 
                WriteLine("<button " + formatoEstilo + ">\n<a href=\"./" + fileName + "Listado.html\"" + ">Listado</a>\n</button>");
                WriteLine("<button " + formatoEstilo + ">\n<a href=\"./menu.html\"" + ">Volver</a>\n</button>"); 
            } else {
                WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + fileName + "Alta.html\"" + ">Alta</a>\n</button>"); 
                WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + fileName + "Listado.html\"" + ">Listado</a>\n</button>");
                WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./menu.html\"" + ">Volver</a>\n</button>");
            }
            WriteLine("</div>\n</body>\n</html>");

            //Alta Clase
            fileManager.StartNewFile(fileName + "Alta.html");
            WriteLine("<html>\n<head>\n\t<title>" + "Alta " + fileName + "</title>\n</head>");
            WriteLine("<body>\n<div style='border:1px solid black; margin:20px'>");

            if (claseOClaseE is ClaseEnriquecida && (claseOClaseE as ClaseEnriquecida).EstiloClase != null) {
                EstiloClase estilo = (claseOClaseE as ClaseEnriquecida).EstiloClase.FirstOrDefault();
                string colorLetra = formatoColor(estilo.ColorLetra.ToString());
                string colorFondo = formatoColor(estilo.ColorFondo.ToString());
                string alineacion = formatoAlineacion(estilo.Alineacion.ToString());
                WriteLine("<form name='Alta" + fileName + "' style='background-color:" + colorFondo + ";color:" + colorLetra + ";font-family:" + estilo.TipoLetra.ToString() + ";text-align:" + alineacion + ";'>");
            } else {
                WriteLine("<form name='Alta" + fileName + "' style='background-color:white;color:black;font-family:Arial;text-align:center;'>");
            }

            WriteLine("<h1>Alta " + fileName + "</h1>");
            if (claseOClaseE is ClaseEnriquecida  && (claseOClaseE as ClaseEnriquecida).EstiloAtributo != null) {
                EstiloAtributo estilo = (claseOClaseE as ClaseEnriquecida).EstiloAtributo.FirstOrDefault();
                string colorLetra = formatoColor(estilo.ColorLetra.ToString());
                string formatoEstilo = "style='color:" + colorLetra + ";font-family:" + estilo.TipoLetra.ToString() + "'";
                string estiloLetra = estilo.TipoLetra.ToString();

                if (claseOClaseE.AtributoID != null){
                    AtributoID atrib = claseOClaseE.AtributoID;
                    formatoAtrib(colorLetra, estiloLetra, atrib.Nombre);
                }
                foreach (Atributo atrib in claseOClaseE.Atributo) {
                    formatoAtrib(colorLetra, estiloLetra, atrib.Nombre);
                }
            } else {
                if (claseOClaseE.AtributoID != null){
                    AtributoID atrib = claseOClaseE.AtributoID;
                    formatoAtrib("#000000", "Arial", atrib.Nombre);
                }
                foreach (Atributo atrib in claseOClaseE.Atributo) {
                    formatoAtrib("#000000", "Arial", atrib.Nombre);
                }
            }
            if (isClaseHija(claseOClaseE)) {
                foreach(Herencia rel in Herencia.GetLinksToClaseHija(claseOClaseE)) {
                    foreach(Clase cP in claseOClaseE.ClaseHija) {
                        if (claseOClaseE is ClaseEnriquecida && (claseOClaseE as ClaseEnriquecida).EstiloAtributo != null) {
                            EstiloAtributo estilo = (claseOClaseE as ClaseEnriquecida).EstiloAtributo.FirstOrDefault();
                            string colorLetra = formatoColor(estilo.ColorLetra.ToString());
                            string formatoEstilo = "style='color:" + colorLetra + ";font-family:" + estilo.TipoLetra.ToString() + "'";
                            string estiloLetra = estilo.TipoLetra.ToString();
                            if (cP.AtributoID != null) {
                                AtributoID atrib = cP.AtributoID;
                                formatoAtrib(colorLetra, estiloLetra, atrib.Nombre);
                            }
                            foreach (Atributo atrib in cP.Atributo) {
                                formatoAtrib(colorLetra, estiloLetra, atrib.Nombre);
                            }
                            WriteLine("<button " + formatoEstilo + "> <a href='" + fileName + ".html'>Guardar</a> </button>");
                            WriteLine("<button " + formatoEstilo + "> <a href='" + fileName + ".html'>Volver</a> </button>");
                        } else {
                            if (cP.AtributoID != null) {
                                AtributoID atrib = cP.AtributoID;
                                formatoAtrib("#000000", "Arial", atrib.Nombre);
                            }
                            foreach (Atributo atrib in cP.Atributo) {
                                formatoAtrib("#000000", "Arial", atrib.Nombre);
                            }
                            WriteLine("<button style='color:#000000;font-family:Arial;'> <a href='" + fileName + ".html'>Guardar</a> </button>");
                            WriteLine("<button style='color:#000000;font-family:Arial;'> <a href='" + fileName + ".html'>Volver</a> </button>");
                        }
                    }
                }
            } else {
                if (claseOClaseE is ClaseEnriquecida && (claseOClaseE as ClaseEnriquecida).EstiloAtributo != null) {
                    EstiloAtributo estilo = (claseOClaseE as ClaseEnriquecida).EstiloAtributo.FirstOrDefault();
                    string colorLetra = formatoColor(estilo.ColorLetra.ToString());
                    string formatoEstilo = "style='color:" + colorLetra + ";font-family:" + estilo.TipoLetra.ToString() + "'";
                    string estiloLetra = estilo.TipoLetra.ToString();
                    WriteLine("<button " + formatoEstilo + "> <a href='" + fileName + ".html'>Guardar</a> </button>");
                    WriteLine("<button " + formatoEstilo + "> <a href='" + fileName + ".html'>Volver</a> </button>");
                } else {
                    WriteLine("<button style='color:#000000;font-family:Arial;'> <a href='" + fileName + ".html'>Guardar</a> </button>");
                    WriteLine("<button style='color:#000000;font-family:Arial;'> <a href='" + fileName + ".html'>Volver</a> </button>");
                }
            }
            WriteLine("</form>\n</div>\n</body>\n</html>");


           //Listado clase
        fileManager.StartNewFile(fileName + "Listado.html");
        WriteLine("<html>\n<head>\n\t<title>" + fileName + "</title>\n</head>");
        WriteLine("<body>\n<div style='border:1px solid black; margin:50px'>"); 
        if (claseOClaseE is ClaseEnriquecida && (claseOClaseE as ClaseEnriquecida).EstiloClase != null) {
            EstiloClase estilo = (claseOClaseE as ClaseEnriquecida).EstiloClase.FirstOrDefault();
            string colorLetra = formatoColor(estilo.ColorLetra.ToString());
            string colorFondo = formatoColor(estilo.ColorFondo.ToString());
            string alineacion = formatoAlineacion(estilo.Alineacion.ToString());
            WriteLine("<div style='background-color:" + colorFondo + ";color:" + colorLetra + ";font-family:" + estilo.TipoLetra.ToString() + ";text-align:" + alineacion + "'>");
        } else {
            WriteLine("<div style='background-color:white;color:black;font-family:Arial;text-align:center;'>");
        }
        
        WriteLine("<h1>" + fileName + "</h1>");
        if (claseOClaseE is ClaseEnriquecida  && (claseOClaseE as ClaseEnriquecida).EstiloOperacioned != null) {
            EstiloOperacion estilo = (claseOClaseE as ClaseEnriquecida).EstiloOperacioned.FirstOrDefault();
            string colorLetra = formatoColor(estilo.ColorLetra.ToString());
            string formatoEstilo = "style='color:" + colorLetra + ";font-family:" + estilo.TipoLetra.ToString() + "'";
            
            WriteLine("<div style='display: flex; align-items: center;'>");
            WriteLine("<p>" + claseOClaseE.Nombre + "1</p>");
            WriteLine("<button " + formatoEstilo + ">\n<a href=\"./" + fileName + "Consultar.html\"" + ">Consultar</a>\n</button>"); 
            WriteLine("<button " + formatoEstilo + ">\n<a href=\"./" + fileName + "Modificar.html\"" + ">Modificar</a>\n</button>"); 
            WriteLine("<button " + formatoEstilo + ">\n<a href=\"./" + fileName + "Eliminar.html\"" + ">Eliminar</a>\n</button>"); 
            WriteLine("<button " + formatoEstilo + ">\n<a href=\"./" + fileName + "GenerarCodigo.html\"" + ">GenerarCodigo</a>\n</button>");
            WriteLine("</div");
            WriteLine("<br>");
            WriteLine("<div style='display: flex; align-items: center;'>");
            WriteLine("<p>" + claseOClaseE.Nombre + "M</p>");
            WriteLine("<button " + formatoEstilo + ">\n<a href=\"./" + fileName + "Consultar.html\"" + ">Consultar</a>\n</button>"); 
            WriteLine("<button " + formatoEstilo + ">\n<a href=\"./" + fileName + "Modificar.html\"" + ">Modificar</a>\n</button>"); 
            WriteLine("<button " + formatoEstilo + ">\n<a href=\"./" + fileName + "Eliminar.html\"" + ">Eliminar</a>\n</button>"); 
            WriteLine("<button " + formatoEstilo + ">\n<a href=\"./" + fileName + "GenerarCodigo.html\"" + ">GenerarCodigo</a>\n</button>");
            WriteLine("<button " + formatoEstilo + "> <a href='" + fileName + ".html'>Volver</a> </button>"); 
            
        } else {
            WriteLine("<div style='display: flex; align-items: center;'>");
            WriteLine("<p>" + claseOClaseE.Nombre + "1</p>");
            WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + fileName + "Consultar.html\"" + ">Consultar</a>\n</button>"); 
            WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + fileName + "Modificar.html\"" + ">Modificar</a>\n</button>");
            WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + fileName + "Eliminar.html\"" + ">Eliminar</a>\n</button>");
            WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + fileName + "GenerarCodigo.html\"" + ">GenerarCodigo</a>\n</button>");
            WriteLine("</div");
            WriteLine("<br>");
            WriteLine("<div style='display: flex; align-items: center;'>");
            WriteLine("<p>" + claseOClaseE.Nombre + "M</p>");
            WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + fileName + "Consultar.html\"" + ">Consultar</a>\n</button>"); 
            WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + fileName + "Modificar.html\"" + ">Modificar</a>\n</button>");
            WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + fileName + "Eliminar.html\"" + ">Eliminar</a>\n</button>");
            WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + fileName + "GenerarCodigo.html\"" + ">GenerarCodigo</a>\n</button>");
            WriteLine("</div");
            WriteLine("<br>");
            WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./menu.html\"" + ">Volver</a>\n</button>");
            
        }
            WriteLine("</div>\n</body>\n</html>");
        }
        
    }

    fileManager.Process();
}
#>


<#+
    private bool isTipoDatoValido (String tipoDato) {
        if (tipoDato.Equals("byte") || tipoDato.Equals("short") || tipoDato.Equals("int") || 
        tipoDato.Equals("long") || tipoDato.Equals("float") || tipoDato.Equals("double") ||
        tipoDato.Equals("char") || tipoDato.Equals("boolean") || tipoDato.Equals("String")) {
            return true;
        } else return false;
    }
#>

<#+
    private bool isRetornoValido (String tipoDato) {
        if (tipoDato.Equals("byte") || tipoDato.Equals("short") || tipoDato.Equals("int") || 
        tipoDato.Equals("long") || tipoDato.Equals("float") || tipoDato.Equals("double") ||
        tipoDato.Equals("char") || tipoDato.Equals("boolean") || tipoDato.Equals("String") || 
        tipoDato.Equals("void")) {
            return true;
        } else return false;
    }
#>

<#+
    private string formatoNombreClaseEnriquecida(string nombre){
        if (!nombre.ToLower().Contains("enriquecida")){
            return nombre + "Enriquecida";
        }
        return nombre;
    }
#>

<#+
    private string formatoColor(string color){
        if (color.CompareTo("Amarillo") == 0) {
            color = "yellow";
        } else if (color.CompareTo("Verde") == 0){
            color = "green";
        } else if (color.CompareTo("Azul") == 0){
            color = "skyblue";
        } else if (color.CompareTo("Morado") == 0){
            color = "lavender";
        } else if (color.CompareTo("Gris") == 0){
            color = "gray";
        } else if (color.CompareTo("Rojo") == 0) {
            color = "red";
        }
        return color;
    }
#>

<#+
    private string formatoAlineacion(string alineacion){
        if (alineacion.CompareTo("Centro") == 0){
            alineacion = "center";
        } else if (alineacion.CompareTo("Derecha") == 0){
            alineacion = "right";
        } else if (alineacion.CompareTo("Izquierda") == 0){
            alineacion = "left";
        } else if (alineacion.CompareTo("Justificar") == 0){
            alineacion = "justify";
        } 
        return alineacion;
    }
#>

<#+
    private void formatoAtrib(string color, string tipoLetra, string nombre) {
        WriteLine("<table><tr><td style='color:" + color + ";font-family:" + tipoLetra + ";'>" + nombre + ": <input type='text' id='" + nombre + "'></td></tr></table>");
    }
#>

<#+
    private bool isClasePadreAbstracta(Clase clase){
        bool isAbstracta = false;
        if (clase.ClasePadre.Count > 0) {
            foreach(Herencia rel in Herencia.GetLinksToClasePadre(clase)) {
                if (rel.TGradoHerencia.ToString().CompareTo("Total") == 0) {
                    isAbstracta = true;
                }
            }
        }
        return isAbstracta;
    }
#>

<#+
    private bool isClaseHija(Clase clase){
        if (clase.ClaseHija.Count > 0) {
            return true;
        }
        return false;
    }
#>

<#+
    private bool comprobarCardinalidad(string card){
        if (card.EndsWith("MUCHOS") || card.EndsWith("N")){
            return true;
        }
        return false;
    }
#>

<#+
    private void creacionHomeAsociacion(string rel) {
        WriteLine("<html>\n<head>\n\t<title>" + rel + "</title>\n</head>");
        WriteLine("<body>\n<div style='border:1px solid black; margin:20px'>");
        WriteLine("<div style='background-color:white;color:black;font-family:Arial;text-align:center;'>");
        WriteLine("<h1>" + rel + "</h1>");
        WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + rel + "Alta.html\"" + ">Alta</a>\n</button>"); 
        WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + rel + "Listado.html\"" + ">Listado</a>\n</button>");
        WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./menu.html\"" + ">Volver</a>\n</button>");
        WriteLine("</div>\n</body>\n</html>");
    }
#>

<#+
    private void creacionAltaAsociacion(string rel, string nombreA1, string nombreA2) {
        WriteLine("<html>\n<head>\n\t<title>" + "Alta " + rel + "</title>\n</head>");
        WriteLine("<body>\n<div style='border:1px solid black; margin:20px'>");
        WriteLine("<form name='Alta" + rel + "' style='background-color:white;color:black;font-family:Arial;text-align:center;'>");
        WriteLine("<h1>Alta " + rel + "</h1>");
        if (nombreA1 != null){
            formatoAtrib("#000000", "Arial", nombreA1);
        }
        if (nombreA2 != null){
            formatoAtrib("#000000", "Arial", nombreA2);
        }
        WriteLine("<button style='color:#000000;font-family:Arial;'> <a href='" + rel + ".html'>Guardar</a> </button>");
        WriteLine("<button style='color:#000000;font-family:Arial;'> <a href='" + rel + ".html'>Volver</a> </button>");
        WriteLine("</form>\n</div>\n</body>\n</html>");
        
    }
#>

<#+
    private void creacionListadoAsociacion(string rel) {
        WriteLine("<html>\n<head>\n\t<title>" + rel + "</title>\n</head>");
        WriteLine("<body>\n<div style='border:1px solid black; margin:20px'>"); // Corregir el estilo del borde
        WriteLine("<div style='background-color:white;color:black;font-family:Arial;text-align:center;'>");
        WriteLine("<h1>" + rel + "</h1>");
        WriteLine("<div style='display: flex; align-items: center;'>");
        WriteLine("<p>" + rel + "1</p>");
        WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + rel + "Consultar.html\"" + ">Consultar</a>\n</button>"); 
        WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + rel + "Modificar.html\"" + ">Modificar</a>\n</button>");
        WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + rel + "Eliminar.html\"" + ">Eliminar</a>\n</button>");
        WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + rel + "GenerarCodigo.html\"" + ">GenerarCodigo</a>\n</button>");
        WriteLine("</div");
        WriteLine("<br>");
        WriteLine("<div style='display: flex; align-items: center;'>");
        WriteLine("<p>" + rel + "M</p>");
        WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + rel + "Consultar.html\"" + ">Consultar</a>\n</button>"); 
        WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + rel + "Modificar.html\"" + ">Modificar</a>\n</button>");
        WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + rel + "Eliminar.html\"" + ">Eliminar</a>\n</button>");
        WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./" + rel + "GenerarCodigo.html\"" + ">GenerarCodigo</a>\n</button>");
        WriteLine("</div");
        WriteLine("<br>");
        WriteLine("<button style='color:#000000;font-family:Arial'>\n<a href=\"./menu.html\"" + ">Volver</a>\n</button>");
        WriteLine("</div>\n</body>\n</html>");

        
    }
#>