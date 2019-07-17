# AutoCompleteMVC
Crear Autocomplete en Asp.net MVC 5 Usando la Libreria Select2

<div style="text-align: justify;">
<span style="font-family: Arial, Helvetica, sans-serif;">En este articulo veremos la manera de como crear un dropdowlist con <b>autocomplete</b> en mvc 5 usando la libreria&nbsp;<a href="https://select2.org/" target="_blank"><b>select2</b></a>&nbsp;ya que buscando en la web me doy cuenta que existen pocos ejemplos usando esta librería en un proyecto mvc de asp.net.</span></div>
<div style="text-align: justify;">
<span style="font-family: Arial, Helvetica, sans-serif;"><br /></span></div>
<div style="text-align: justify;">
<span style="font-family: Arial, Helvetica, sans-serif;">Para este ejemplo usare una base de datos que solo cuenta con una tabla Departamento(son los departamentos de mi bello país El Salvador)</span></div>
<br />
<div class="separator" style="clear: both; text-align: center;">
<a href="https://drive.google.com/uc?id=1xk-RqRn9ZNGkkvCkUGRK5ONhoZRX-Zuz" imageanchor="1" style="margin-left: 1em; margin-right: 1em;"><img border="0" data-original-height="418" data-original-width="241" height="320" src="https://drive.google.com/uc?id=1xk-RqRn9ZNGkkvCkUGRK5ONhoZRX-Zuz" width="184" /></a></div>
<div style="text-align: justify;">
<span style="font-family: Arial, Helvetica, sans-serif;"><br /></span></div>
<div style="text-align: justify;">
<span style="font-family: Arial, Helvetica, sans-serif;">Creamos un proyecto en VS y lo primero que aremos es agregar la librería select2 a nuestro proyecto(la descargamos con nuget)</span></div>
<br />
<div class="separator" style="clear: both; text-align: center;">
<a href="https://drive.google.com/uc?id=1Tw6Z8JUD8fYYEZTF6KtF_iSk6plKPhkt" imageanchor="1" style="margin-left: 1em; margin-right: 1em;"><img border="0" data-original-height="275" data-original-width="673" height="162" src="https://drive.google.com/uc?id=1Tw6Z8JUD8fYYEZTF6KtF_iSk6plKPhkt" width="400" /></a></div>
<div class="separator" style="clear: both; text-align: center;">
<br /></div>
<div class="separator" style="clear: both; text-align: justify;">
<span style="font-family: Arial, Helvetica, sans-serif;">Creamos el modelo de nuestra base de datos</span></div>
<div class="separator" style="clear: both; text-align: left;">
<br /></div>
<div class="separator" style="clear: both; text-align: center;">
<a href="https://drive.google.com/uc?id=1k11qi_3Fz4f2PWUeBBx74yvSmLdeORHo" imageanchor="1" style="margin-left: 1em; margin-right: 1em;"><img border="0" data-original-height="272" data-original-width="328" height="265" src="https://drive.google.com/uc?id=1k11qi_3Fz4f2PWUeBBx74yvSmLdeORHo" width="320" /></a></div>
<div class="separator" style="clear: both; text-align: left;">
<br /></div>
<div class="separator" style="clear: both; text-align: justify;">
<span style="font-family: Arial, Helvetica, sans-serif;">Creamos un controlador(DepartamentoController) y agregamos el siguiente&nbsp;código</span></div>
<div class="separator" style="clear: both; text-align: left;">
<br /></div>
<div class="separator" style="clear: both; text-align: left;">
<br /></div>
<br />
<pre class="brush: csharp"> 
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetDepartamento(string departamento)
        {
            PaisEntities bd = new PaisEntities();
            //cargo datos sin filtro
            var dataList = bd.Departamento.ToList();
            //si parametro tiene dato
            if(departamento != null)
            {
                //busco dato filtrado
                 dataList = bd.Departamento.Where(x =&gt; x.Departamento1.Contains(departamento)).ToList();
            }

            //datos a usar
            var modificarData = dataList.Select(x =&gt; new
            {
                id= x.Id,
                text=x.Departamento1
            });
            //retorno datos como json
            return Json(modificarData, JsonRequestBehavior.AllowGet);
        }

        //Metodo donde obtengo dato
        [HttpPost]
        public ActionResult Save(string id)
        {
            //aca tu codigo
            return Json(0, JsonRequestBehavior.AllowGet);
        }
</pre>
<br />
<div style="text-align: justify;">
<span style="font-family: Arial, Helvetica, sans-serif;">Como ven tenemos un método <b>GetDepartamento</b> que recibirá un parámetro y que nos retornara un json. Les explico un poco que es lo que hace este método primeramente cargamos la lista con todos los departamentos(los datos que se mostraran por defecto cuando el usuario no escriba nada) luego si el parámetro contiene datos hacemos el filtro para buscar los datos que coincidan con lo que el usuario escriba para eso usamos linq lo hacemos con el método <b>Contains</b>(es similar a hacer un like de sql), para este ejemplo mostraremos los departamento y obtendremos el id del departamento seleccionado, por ultimo tenemos el método <b>Save</b> que sera donde obtendremos el valor del id seleccionado.
</span></div>
<div style="text-align: justify;">
<span style="font-family: Arial, Helvetica, sans-serif;">Ahora creemos nuestra vista <b>Index</b> y agregamos el siguiente código
</span></div>
<pre class="brush: xml"> 
@model PruebaAutocomplete.Models.Departamento
@{
    ViewBag.Title = "Index";

}

@*tema de boostrap para select2*@
<link href="https://cdnjs.cloudflare.com/ajax/libs/select2-bootstrap-theme/0.1.0-beta.10/select2-bootstrap.css" rel="stylesheet"></link>



@using (Html.BeginForm("ConsultarCreditos", "Cliente", FormMethod.Post))
{
    &lt;select class="mySelect2" style="width:200px"&gt;&lt;/select&gt;
    &lt;br /&gt;
    &lt;br /&gt;
    &lt;div class="form-group"&gt;
        @Html.TextBoxFor(m =&gt; m.Id, null, new { disabled = true })

    &lt;/div&gt;

}



&lt;script&gt;
    //obtengo datos por ajax
    $(document).ready(function () {
        $(".mySelect2").select2({
            placeholder: "Seleccione Departamento",
            allowClear: true,
            theme: "bootstrap",
            ajax: {
                //invoco el metodo de mi controlador
                url: '@Url.Action("GetDepartamento","Departamento")',
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    return {
                        departamento: params.term //parametro del metodo de mi controlador
                    };
                },
                processResults: function (data) {
                    return {
                        results: data
                    };
                }
            }
        });
    });

    //capturar dato seleccionado
    $(".mySelect2").on("change", function () {
        var departamentoID = $(this).val(); //capturo id seleccionado
        var textboxData = departamentoID; 
        $.ajax({
            url: '@Url.Action("Save","Departamento")', //url metodo de mi controlador
            data: { id: textboxData },
            dataType: 'json',
            type: 'post',
            success: function () {
                $("#Id").val(textboxData);//asigno valor id a textbox
            }
        });

    });
&lt;/script&gt;
</pre>
<br />
<div style="text-align: justify;">
<span style="font-family: Arial, Helvetica, sans-serif;">Lo que hacemos es crear un dropdowlist donde mostraremos los departamentos y también tenemos un texbox que es donde mostraremos el id del departamento seleccionado.&nbsp;</span></div>
<div style="text-align: justify;">
<span style="font-family: Arial, Helvetica, sans-serif;">Lo importante acá es ver como implementamos jquery con ajax para cargar los departamentos y como obtenemos el valor del id.</span></div>
<div style="text-align: justify;">
<span style="font-family: Arial, Helvetica, sans-serif;">En la primera parte obtenemos los datos invocando por ajax el método&nbsp;<b>GetDepartamento</b> de nuestro controlador en esta parte también&nbsp;usamos algunas propiedades de la librería&nbsp;<b>select2</b> para asignarle el tema y placeholder.
En la segunda parte capturamos el id del dato seleccionado y enviamos por ajax este valor a nuestro método&nbsp;<b>Save</b> de nuestro controlador, también&nbsp;acá&nbsp;asignamos el valor del id seleccionado a nuestro textbox.

<div class="separator" style="clear: both; text-align: center;">
<a href="https://drive.google.com/uc?id=1qOx55aw7ww_BQ__SgDorBJOYwUzbC9vW" imageanchor="1" style="margin-left: 1em; margin-right: 1em;"><img border="0" data-original-height="176" data-original-width="383" height="147" src="https://drive.google.com/uc?id=1qOx55aw7ww_BQ__SgDorBJOYwUzbC9vW" width="320" /></a></div>
