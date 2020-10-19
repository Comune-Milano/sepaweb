<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Portale.aspx.vb" Inherits="Portale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex,nofollow" />
    <title>SEPA</title>
    <meta name="keywords" content="sepa" />
    <meta name="description" content="SEPA" />
    <link href="sepa_style.css" rel="stylesheet" type="text/css" />
    <!--  Designed by w w w . t e m p l a t e m o . c o m  -->
    <link rel="stylesheet" type="text/css" href="contentslider.css" />
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <link rel="icon" href="favicon.ico" type="image/x-icon" />
    <script type="text/javascript" src="contentslider.js">

        /***********************************************
        * Featured Content Slider- (c) Dynamic Drive DHTML code library (www.dynamicdrive.com)
        * This notice MUST stay intact for legal use
        * Visit Dynamic Drive at http://www.dynamicdrive.com/ for this script and 100s more
        ***********************************************/
    </script>
    <script type="text/javascript" src="Funzioni.js">

    </script>
    <!--[if lt IE 7]>
<style type="text/css">

  .sepa_thumb_box span { behavior: url(iepngfix.htc); }

</style>
<![endif]-->
</head>
<%= psNews %>
<body>
    <div id="sepa_container">
        <div id="sepa_header_panel">
            <div id="sepa_title_section">
                &nbsp;</div>
            <div id="sepa_top_right_section">
                <ul>
                    <li><a href="#">Home</a></li>
                    <li><a href="javascript:top.location.href='AreaPrivata.aspx';">Area Privata</a></li>
                    <li><a href="javascript:ApriContatti();">Contatti</a></li>
                </ul>
            </div>
        </div>
        <div id="sepa_login_banner_panel">
            <div id="sepa_banner_panel">
                <!--Inner content DIVs should always carry "contentdiv" CSS class-->
                <!--Pagination DIV should always carry "paginate-SLIDERID" CSS class-->
                <div id="paginate-slider2" class="pagination">
                    <a href="#" class="toc">&nbsp;</a> <a href="#" class="toc anotherclass">&nbsp;</a>
                    <a href="#" class="toc">&nbsp;</a> <a href="#" class="toc">&nbsp;</a> <a href="#"
                        class="toc">&nbsp;</a> <a href="#" class="toc">&nbsp;</a> <a href="#" class="toc">&nbsp;</a>
                    <a href="#" class="toc">&nbsp;</a> <a href="#" class="toc">&nbsp;</a> <a href="#"
                        class="toc">&nbsp;</a> <a href="#" class="toc">&nbsp;</a> <a href="#" class="toc">&nbsp;</a>
                </div>
                <div id="slider2" class="sliderwrapper">
                    <div class="contentdiv">
                        <img src="images/portale/portale_1.jpg" alt="image 10" /></div>
                    <div class="contentdiv">
                        <img src="images/portale/portale_2.jpg" alt="image 10" /></div>
                    <div class="contentdiv">
                        <img src="images/portale/portale_3.jpg" alt="image 10" /></div>
                    <div class="contentdiv">
                        <img src="images/portale/portale_4.jpg" alt="image 10" /></div>
                    <div class="contentdiv">
                        <img src="images/portale/portale_5.jpg" alt="image 10" /></div>
                    <div class="contentdiv">
                        <img src="images/portale/portale_6.jpg" alt="image 10" /></div>
                    <div class="contentdiv">
                        <img src="images/portale/portale_7.jpg" alt="image 10" /></div>
                    <div class="contentdiv">
                        <img src="images/portale/portale_8.jpg" alt="image 10" /></div>
                    <div class="contentdiv">
                        <img src="images/portale/portale_9.jpg" alt="image 10" /></div>
                    <div class="contentdiv">
                        <img src="images/portale/portale_10.jpg" alt="image 10" /></div>
                    <div class="contentdiv">
                        <img src="images/portale/portale_11.jpg" alt="image 10" /></div>
                    <div class="contentdiv">
                        <img src="images/portale/portale_12.jpg" alt="image 10" /></div>
                </div>
                <script type="text/javascript">

                    featuredcontentslider.init({
                        id: "slider2",  //id of main slider DIV
                        contentsource: ["inline", ""],  //Valid values: ["inline", ""] or ["ajax", "path_to_file"]
                        toc: "markup",  //Valid values: "#increment", "markup", ["label1", "label2", etc]
                        nextprev: ["Previous", "Next"],  //labels for "prev" and "next" links. Set to "" to hide.
                        revealtype: "click", //Behavior of pagination links to reveal the slides: "click" or "mouseover"
                        enablefade: [true, 0.2],  //[true/false, fadedegree]
                        autorotate: [true, 3000],  //[true/false, pausetime]
                        onChange: function (previndex, curindex) {  //event handler fired whenever script changes slide
                            //previndex holds index of last slide viewed b4 current (1=1st slide, 2nd=2nd etc)
                            //curindex holds index of currently shown slide (1=1st slide, 2nd=2nd etc)
                        }
                    })
            
                </script>
            </div>
            <h1>
                <br />
                Accedi all'AREA PRIVATA</h1>
            <div id="sepa_login_panel" style="width: 233px">
                <form id="form1" runat="server" defaultbutton="btnAccedi">
                <asp:HiddenField ID="indirizzo" runat="server" />
                <table style="width: 99%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label1" runat="server" Text="Utente"></asp:Label>
                        </td>
                        <td style="width: 3px">
                            <asp:TextBox ID="txtUtente" runat="server" TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                        </td>
                        <td style="width: 3px">
                            <asp:TextBox ID="txtPw" runat="server" TextMode="Password" TabIndex="2" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblAvviso" runat="server" Width="227px"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:LinkButton ID="btnAccedi" runat="server" TabIndex="3">Login</asp:LinkButton>&nbsp;
                </form>
                <p>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</p>
            </div>
        </div>
        <!-- end of login and banner panel -->
        <div id="sepa_menu">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/News.aspx?ID=-1" Style="left: 784px;
                position: relative; top: 67px" Target="_top" TabIndex="5" ToolTip="Visualizza tutte le news, con il testo completo.">Tutte le News</asp:HyperLink>
            <div id="News" align="left" style="top: 0px; left: 150px; position: relative">
                <script type="text/javascript">

                    /***********************************************
                    * Fading Scroller- © Dynamic Drive DHTML code library (www.dynamicdrive.com)
                    * This notice MUST stay intact for legal use
                    * Visit Dynamic Drive at http://www.dynamicdrive.com/ for full source code
                    ***********************************************/

                    var delay = 2000; //set delay between message change (in miliseconds)
                    var maxsteps = 30; // number of steps to take to change from start color to endcolor
                    var stepdelay = 40; // time in miliseconds of a single step
                    //**Note: maxsteps*stepdelay will be total time in miliseconds of fading effect
                    var startcolor = new Array(255, 255, 255); // start color (red, green, blue)
                    var endcolor = new Array(0, 0, 0); // end color (red, green, blue)


                    //var aa = document.getElementById("Hidden2").value;
                    begintag = '<div style="font: normal 14px Arial; padding: 5px;">'; //set opening tag, such as font declarations
                    //fcontent[0]="<a href='http://www.sistemiesoluzionisrl.it'><span style='font-size: 14pt; font-family: Arial; color: #982127'>"+aa+"</span><br><span style='font-size: 10pt; font-family: Arial'>Sottotitolo</span></a>";
                    //fcontent[1]="<a href='http://www.sistemiesoluzionisrl.it'><span style='font-size: 14pt; font-family: Arial; color: #982127'>Alessandro Gobbi offre!!</span><br><span style='font-size: 10pt; font-family: Arial'>Alessandro Gobbi offrirà da bere a tutta la cittadinanza</span></a>";
                    //fcontent[2]="<a href='http://www.sistemiesoluzionisrl.it'><span style='font-size: 14pt; font-family: Arial; color: #982127'>Eventuale Terza Notizia</span><br><span style='font-size: 10pt; font-family: Arial'>Sottotitolo</span></a>";

                    closetag = '</div>';

                    var fwidth = '500px'; //set scroller width
                    var fheight = '150px'; //set scroller height

                    var fadelinks = 1;  //should links inside scroller content also fade like text? 0 for no, 1 for yes.

                    ///No need to edit below this line/////////////////

                    var ie4 = document.all && !document.getElementById;
                    var DOM2 = document.getElementById;
                    var faderdelay = 0;
                    var index = 0;

                    /*Rafael Raposo edited function*/
                    //function to change content
                    function changecontent() {
                        if (index >= fcontent.length)
                            index = 0
                        if (DOM2) {
                            document.getElementById("fscroller").style.color = "rgb(" + startcolor[0] + ", " + startcolor[1] + ", " + startcolor[2] + ")"
                            document.getElementById("fscroller").innerHTML = begintag + fcontent[index] + closetag
                            if (fadelinks)
                                linkcolorchange(1);
                            colorfade(1, 15);
                        }
                        else if (ie4)
                            document.all.fscroller.innerHTML = begintag + fcontent[index] + closetag;
                        index++
                    }

                    // colorfade() partially by Marcio Galli for Netscape Communications.  ////////////
                    // Modified by Dynamicdrive.com

                    function linkcolorchange(step) {
                        var obj = document.getElementById("fscroller").getElementsByTagName("A");
                        if (obj.length > 0) {
                            for (i = 0; i < obj.length; i++)
                                obj[i].style.color = getstepcolor(step);
                        }
                    }

                    /*Rafael Raposo edited function*/
                    var fadecounter;
                    function colorfade(step) {
                        if (step <= maxsteps) {
                            document.getElementById("fscroller").style.color = getstepcolor(step);
                            if (fadelinks)
                                linkcolorchange(step);
                            step++;
                            fadecounter = setTimeout("colorfade(" + step + ")", stepdelay);
                        } else {
                            clearTimeout(fadecounter);
                            document.getElementById("fscroller").style.color = "rgb(" + endcolor[0] + ", " + endcolor[1] + ", " + endcolor[2] + ")";
                            setTimeout("changecontent()", delay);

                        }
                    }

                    /*Rafael Raposo's new function*/
                    function getstepcolor(step) {
                        var diff
                        var newcolor = new Array(3);
                        for (var i = 0; i < 3; i++) {
                            diff = (startcolor[i] - endcolor[i]);
                            if (diff > 0) {
                                newcolor[i] = startcolor[i] - (Math.round((diff / maxsteps)) * step);
                            } else {
                                newcolor[i] = startcolor[i] + (Math.round((Math.abs(diff) / maxsteps)) * step);
                            }
                        }
                        return ("rgb(" + newcolor[0] + ", " + newcolor[1] + ", " + newcolor[2] + ")");
                    }

                    if (ie4 || DOM2)
                        document.write('<div id="fscroller" style="border:0px solid black;width:' + fwidth + ';height:' + fheight + '"></div>');

                    if (window.addEventListener)
                        window.addEventListener("load", changecontent, false)
                    else if (window.attachEvent)
                        window.attachEvent("onload", changecontent)
                    else if (document.getElementById)
                        window.onload = changecontent





                </script>
            </div>
        </div>
        <!-- content -->
        <div id="sepa_content">
            <div id="sepa_leftcolumn">
                <div class="sepa_leftcolumn_twocolumn">
                    <h1>
                        Servizi On-line</h1>
                    <div class="service_box">
                        <img src="images/sepa_image_02.jpg" alt="Immagine" />
                        <p>
                            Nella sezione è possibile consultare le graduatorie pubbliche di Bando ERP, Bando
                            Cambi ERP, Bando POR e Bando FSA del Comune di Milano e calcolare gli indici Isee
                            ERP, POR e FSA.</p>
                    </div>
                    <ul>
                        <li><a href="javascript:ApriGrad();"><strong>Consultazione Graduatorie</strong></a></li>
                        <li><a href="Trasparenza.aspx" target="_blank"><strong>Calcolo Isee ERP</strong></a></li>
                        <li><a href="javascript:alert('Non disponibile!');"><strong>Calcolo Isee FSA</strong></a></li>
                        <li><a href="javascript:alert('Non disponibile!');"><strong>Calcolo Isee POR</strong></a></li>
                        <li><a href="cf/codice.htm" target="_blank"><strong>Calcolo Codice Fiscale</strong></a></li>
                    </ul>
                    <p>
                        &nbsp;</p>
                    <p>
                        &nbsp;</p>
                    <p>
                        &nbsp;</p>
                    <p>
                        &nbsp;</p>
                    <p>
                        &nbsp;</p>
                    <p>
                        &nbsp;</p>
                    <p>
                        &nbsp;</p>
                    <p>
                        &nbsp;</p>
                    <p>
                        &nbsp;</p>
                    <p>
                        &nbsp;</p>
                </div>
                <div class="sepa_rightcolumn_twocolumn">
                    <h1>
                        Compilazione On-line</h1>
                    <div class="service_box">
                        <img src="images/sepa_image_03.jpg" alt="Immagine" width="80" height="120" />
                        <p>
                            Sezione pubblica dedicata alla compilazione on-line delle Domande per la locazione
                            di immobili del Comune di Milano ed all'aggiornamento dell'Anagrafe utenza degli
                            assegnatari ERP.</p>
                    </div>
                    <ul>
                        <li><a href="javascript:ApriAuto();"><strong>Domanda ERP</strong></a></li>
                        <li><a href="javascript:alert('Non Disponibile al momento!');"><strong>Domanda Bando
                            Cambi</strong></a></li>
                        <li><a href="javascript:alert('Non Disponibile al momento!');"><strong>Domanda FSA</strong></a></li>
                        <li><a href="javascript:alert('Non Disponibile al momento!');"><strong>Domanda POR</strong></a></li>
                        <li><a href="javascript:alert('Non Disponibile al momento!');"><strong>Domanda Forze
                            dell’Ordine</strong></a></li>
                        <li><a href="javascript:alert('Non Disponibile al momento!');"><strong>Domanda Legge
                            431</strong></a></li>
                        <li><a href="javascript:alert('Non Disponibile al momento!');"><strong>Domanda Immobile
                            al Demanio</strong></a></li>
                        <li><a href="javascript:alert('Non Disponibile al momento!');"><strong>Aggiornamento
                            Anagrafe Utenza</strong></a></li>
                        <li><a href="javascript:alert('Non Disponibile al momento!');"><strong>Domanda di Voltura,
                            Subentro o Ampliamento</strong></a></li>
                        <li><a href="javascript:alert('Non Disponibile al momento!');"><strong>Domanda di Riduzione
                            Canone ERP</strong></a></li>
                        <li><a href="javascript:alert('Non Disponibile al momento!');"><strong>Domanda di Cambio
                            Consensuale</strong></a></li>
                        <li><a href="javascript:ApriFondoSolidarieta();"><strong>Fondo di solidariet&agrave;</strong></a></li>
                    </ul>
                </div>
                <div class="cleaner">
                    &nbsp;
                </div>
                <div class="sepa_leftcolumn_fullrow">
                    <h1>
                        &nbsp;</h1>
                    <p>
                        <a href="http://get.adobe.com/it/reader/" target="_blank">
                            <img src="images/get_adobe_reader.png" alt="Immagine" border="0" /></a></p>
                    <p>
                        Per una corretta visualizzazione dei documenti nel formato PDF è necessario l'Adobe
                        Acrobat Reader
                    </p>
                    <p>
                        Applicativo:<asp:Label ID="Label7" runat="server" Text="Ver.2.56"></asp:Label></p>
                </div>
                <!-- end of left full column -->
            </div>
            <!-- end of left column -->
            <div id="sepa_rightcolumn">
                <!-- end of search panel -->
                <div id="sepa_blog_section">
                    <h1>
                        Bandi, Avvisi, Modulistica</h1>
                    <div class="service_box">
                        <img src="images/sepa_image_04.jpg" alt="Immagine" width="80" height="120" />
                        <p>
                            Nella Sezione è possibile scaricare gli avvisi e gli schemi dei bandi ERP e POR,
                            la relativa modulistica e consultare la normativa di riferimento mediante link ai
                            servizi web della Regione Lombardia.</p>
                    </div>
                    <ul>
                        <li><a href="public/MODULO_ANAGRAFE_UTENZA.pdf" target="_blank"><strong>Modulo Anagrafe
                            Utenza</strong></a></li>
                        <li><a href="public/ALLEGATO_ANAGRAFE_UTENZA.pdf" target="_blank"><strong>Allegato Anagrafe
                            Utenza</strong></a></li>
                        <li><a href="public/SchemaBando2Semestre2011.pdf" target="_blank"><strong>Schema bando
                            E.R.P. 2011</strong></a></li>
                        <li><a href="public/AvvisoBando2Semestre2011.pdf" target="_blank"><strong>Avviso di
                            bando E.R.P. 2011</strong></a></li>
                        <li><a href="public/DomandaBando2Semestre2011.pdf" target="_blank"><strong>Modulo Domanda
                            Bando E.R.P.</strong></a></li>
                        <li><a href="public/Avviso_bando_POR_2009.pdf" target="_blank"><strong>Avviso di bando
                            P.O.R. 2009</strong></a></li>
                        <li><a href="public/Schema_di_bando_2009.pdf" target="_blank"><strong>Schema bando P.O.R.
                            2009</strong></a></li>
                        <li><a href="public/Domanda_BANDO_POR_2009.pdf" target="_blank"><strong>Modulo Domanda
                            bando P.O.R. 2009</strong></a></li>
                        <li><a href="http://www.politicheperlacasa.regione.lombardia.it/" target="_blank"><strong>
                            BANDI ERP Lombardia </strong></a></li>
                        <li><a href="http://consiglionline.lombardia.it/normelombardia/accessibile/main.aspx?view=showdoc&exp_coll=rr002004021000001&rebuildtree=1&selnode=rr002004021000001&iddoc=rr002004021000001&testo="
                            target="_blank"><strong>Regolamento Regionale 1/2004 </strong></a></li>
                    </ul>
                </div>
            </div>
            <!-- end of right column -->
        </div>
        <!-- end of content -->
        <div id="sepa_footer">
            <a href="#">Home</a> | <a href="#">Chi Siamo</a> | <a href="Normativa.htm" target="_blank">
                Policy</a> | <a href="#">Help</a> | <a href="#">FAQs</a> | <a href="javascript:ApriContatti();">
                    Contatti</a><br />
            Copyright © 2009 <a href="#"><strong>SEPA@WEB</strong></a> | Designed by <a href="http://www.sistemiesoluzionisrl.it"
                target="_blank">Sistemi e Soluzioni</a>
        </div>
        <!--  Designed by sistemi e soluzioni srl  -->
    </div>
    <!-- end of container -->
    <asp:TextBox ID="txtID" runat="server" Visible="False"></asp:TextBox>
    <script type="text/javascript">
        var indirizzo = window.location + '';
        var Test = indirizzo.substring(0, 15);
        if (Test == 'http://sepatest') {
            alert('ATTENZIONE...Ti ricordiamo che questo è un ambiente di TEST!');
        }
        if (Test == 'https://sepammt') {
            alert('ATTENZIONE...Ti ricordiamo che questo è un ambiente di TEST!');
        }
        document.getElementById('indirizzo').value = indirizzo;

    </script>
</body>
</html>
