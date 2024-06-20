<#@ template inherits="Microsoft.VisualStudio.TextTemplating.VSHost.ModelingTextTransformation" language="C#" hostspecific="True" #>
<#@ output extension=".java" #>
<#@ AMOAMRMAOPProyectoIPS processor="AMOAMRMAOPProyectoIPSDirectiveProcessor" requires="fileName='Sample.AMOAMRMAOP_DLSProyIPS'" #>
<#@ include file="EF.utility.CS.ttinclude" #>

Generated material. Generating code in C#.

<#
var fileManager=EntityFrameworkTemplateFileManager.Create(this);

if(this.Tapiz.Elements.Count > 0) {
  foreach (Clase claseOClaseE in this.Tapiz.Elements)
    {

        string fileName;
        if (claseOClaseE is ClaseEnriquecida claseE && !claseE.Nombre.ToLower().Contains("enriquecida"))
        {
            fileName = claseOClaseE.Nombre + "Enriquecida.java";

        }
        else
        {
            fileName = claseOClaseE.Nombre + ".java";
        }

        fileManager.StartNewFile(fileName);

        informeHerencia(claseOClaseE as Clase);
        informeAtributos(claseOClaseE as Clase);
        informeGetterSetter(claseOClaseE as Clase);
        informeOperacion(claseOClaseE as Clase);
    }
    fileManager.Process();
}
#> 

<#+
    private void informeHerencia (Clase clase) {
        string nombreClase = nombreClase = asignarNombreClase(clase.Nombre, clase);
        if (clase.ClasePadre.Count > 0) {
            foreach(Herencia rel in Herencia.GetLinksToClasePadre(clase)) {
                if (rel.TGradoHerencia.ToString().CompareTo("Total") == 0) {
                    Write("public abstract class "+ nombreClase);
                } else {
                    Write("public class "+ nombreClase); 
                }
            }
        } else {
            Write("public class "+ nombreClase);
        }

        if (clase.ClaseHija.Count > 0){
            foreach(Herencia rel in Herencia.GetLinksToClaseHija(clase)) {
                Write(" extends " + clase.ClaseHija.FirstOrDefault().Nombre);
            }
        }
        
        WriteLine(" {");
    }
#>

<#+
    private string asignarNombreClase (string nombre, Clase clase) {
        if (clase is ClaseEnriquecida claseE && !claseE.Nombre.ToLower().Contains("enriquecida")) {
            return clase.Nombre + "Enriquecida";
        } else return clase.Nombre;
    }
#>

<#+ 
private void informeAtributos (Clase clase) {
    if (clase.AtributoID != null ) {
        AtributoID atrib = clase.AtributoID; 
        String tDato = atrib.TipoDato;
        String nombre = atrib.Nombre;
        if (isTipoDatoValido(tDato) && tDato != null && nombre != null) {
            WriteLine("\t[Key]");
            WriteLine("\tprivate " + tDato + " " + nombre + ";");
                
        }   
    }
        
    if (clase.Atributo.Count > 0) {
        foreach(Atributo atributo in clase.Atributo){
            String tDato = atributo.TipoDato;
            String nombre = atributo.Nombre;
            if (isTipoDatoValido(tDato) && tDato != null && nombre != null) {
                WriteLine("\tprivate " + tDato + " " + nombre + ";");
            }
        } 
    }
}
#>

<#+
    private void informeGetterSetter (Clase clase) {
        WriteLine("\tpublic " + clase.Nombre + " (){\n\t}");
        if (clase.AtributoID != null ) {
            AtributoID atrib = clase.AtributoID; 
            String tDato = atrib.TipoDato;
            String nombre = atrib.Nombre;
            if (isTipoDatoValido(tDato) && tDato != null && nombre != null) {
                WriteLine("\tpublic " + tDato + " get" + nombre + " () {");
                WriteLine("\t\treturn this." + nombre + ";\n\t}");
                WriteLine("\tpublic void" + " set" + nombre + " () {");
                WriteLine("\t\tthis." + nombre +  " = " + nombre + ";\n\t}");
            }   
        }

        foreach(Atributo atributo in clase.Atributo){
            String tDato = atributo.TipoDato;
            String nombre = atributo.Nombre;
            if (isTipoDatoValido(tDato) && tDato != null && nombre != null) {
                WriteLine("\tpublic " + tDato + " get" + nombre + " () {");
                WriteLine("\t\treturn this." + nombre + ";\n\t}");
                WriteLine("\tpublic void" + " set" + nombre + " (" + tDato + " " + nombre + ") {");
                WriteLine("\t\tthis." + nombre +  " = " + nombre + ";\n\t}");
            }
        }
    }
#>

<#+
    private void informeOperacion (Clase clase) {
        if (clase.Operacione.Count > 0) {
            foreach(Operaciones op in clase.Operacione){
                String retorno = op.RetornoOperacion.ToString();
                if (isRetornoValido(retorno)) {
                    Write("\tpublic " + retorno + " " + op.Nombre.ToLower());
                    String entradas = "";
                    if (op.Parametro.Count > 0) {
                        
                        int cont = 0;
                        foreach(Parametros par in op.Parametro) {
                            String tDato = par.TipoDato.ToString();
                            if (isTipoDatoValido(tDato) && par.EntradaSalida == 0 && tDato != null && par.Nombre != null) {
                                cont++;
                            }
                        }
                        foreach(Parametros par in op.Parametro){
                            String aux = par.EntradaSalida.ToString();
                            String tDato = par.TipoDato.ToString();
                            String nombrePar = par.Nombre;
                            if (isTipoDatoValido(tDato) && (aux.CompareTo("Entrada") == 0 && tDato != null && nombrePar != null)) {
                                entradas += tDato + " " + nombrePar;
                                if (cont > 1) {
                                    entradas += ", ";
                                    cont--;
                                }
                            }      
                        }
                    }
                    WriteLine(" ( " + entradas + " ) {\n\t}");
                }
            }
        }
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
