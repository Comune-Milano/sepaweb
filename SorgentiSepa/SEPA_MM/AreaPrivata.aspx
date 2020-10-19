<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AreaPrivata.aspx.vb" Inherits="AreaPrivata"
    EnableSessionState="True" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" type="text/css" href="CONTRATTI/impromptu.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex,nofollow" />
    <title>SEPA</title>
    <meta name="keywords" content="sepa" />
    <meta name="description" content="SEPA" />
    <link href="sepa_style_privata.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="contentslider.css" />
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <link rel="icon" href="favicon.ico" type="image/x-icon" />
    <script type="text/javascript" src="contentslider.js"></script>
    <script type="text/javascript" src="Funzioni.js"></script>
    <script language="javascript" type="text/javascript">
        function Exit() {
            if (document.getElementById('LinkLogOut') != null) {
                if (document.getElementById('Scarica').value == '1') {
                    document.getElementById('LinkLogOut').click();
                };
            };
        };
    </script>
    <script language="javascript" type="text/javascript">
        window.onunload = Exit;
    </script>
    <script src="Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
</head>
<%= psNews %>
<body>
    <div id="sepa_container">
        <div id="sepa_header_panel">
            <div id="sepa_title_section">
            </div>
            <div id="sepa_top_middle_section">
                <ul>
                    <li><a href="javascript:document.getElementById('Scarica').value='0';top.location.href='Portale.aspx';">
                        Home</a></li>
                    <li><a href="javascript:document.getElementById('Scarica').value='0';top.location.href='AreaPrivata.aspx';">
                        Area Privata</a></li>
                    <li><a href="javascript:document.getElementById('Scarica').value='0';ApriContatti();">
                        Contatti</a></li>
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
                AREA PRIVATA</h1>
            <div id="sepa_login_panel">
                <form id="form1" runat="server" style="width: 99%">
                <asp:HiddenField ID="C1" runat="server" />
                <asp:HiddenField ID="C2" runat="server" />
                <asp:HiddenField ID="C3" runat="server" />
                <asp:HiddenField ID="Scarica" runat="server" Value="1" />
                <table width="100%">
                    <tr>
                        <td align="left" style="height: 20px">
                            Benvenuto
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblOperatore" runat="server" Width="225px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt"
                                ForeColor="#FF3300"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:document.getElementById('Scarica').value='0';top.location.href='RegistraUtente.aspx';"
                    ToolTip="Registrazione Utenti appartenenti al proprio Ente." Font-Names="ARIAL"
                    Font-Size="8pt">Registra</asp:HyperLink>
                &nbsp;
                <asp:HyperLink ID="hyAmministrazione" runat="server" NavigateUrl="~/AMMSEPA/menu.htm"
                    Target="_top" onclick="document.getElementById('Scarica').value='0';" ToolTip="Gestione del Sistema">Gestione</asp:HyperLink>
                &nbsp;
                <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/impostapw.aspx" Target="_top"
                    onclick="document.getElementById('Scarica').value='0';" ToolTip="Permette di cambiare la propria password di accesso al Sistema"
                    Font-Names="ARIAL" Font-Size="8pt">Password</asp:HyperLink>
                &nbsp;
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/InfoUtente.aspx" Target="_top"
                    onclick="document.getElementById('Scarica').value='0';" ToolTip="Permette di inserire e/o modificare le informazioni operatore">Info Op.</asp:HyperLink>
                &nbsp;
                <asp:LinkButton ID="LinkLogOut" runat="server" Style="left: 194px; top: 135px" ToolTip="Uscita dal Sistema"
					OnClientClick="document.getElementById('Scarica').value = '0';">Logout</asp:LinkButton>
                </form>
            </div>
        </div>
        <!-- end of login and banner panel -->
        <div id="sepa_menu">
            <div id="News" align="left" style="top: 0px; left: 150px; position: relative">
                <script type="text/javascript">


                    var delay = 2000; //set delay between message change (in miliseconds)
                    var maxsteps = 30; // number of steps to take to change from start color to endcolor
                    var stepdelay = 40; // time in miliseconds of a single step
                    //**Note: maxsteps*stepdelay will be total time in miliseconds of fading effect
                    var startcolor = new Array(255, 255, 255); // start color (red, green, blue)
                    var endcolor = new Array(0, 0, 0); // end color (red, green, blue)


                    begintag = '<div style="font: normal 14px Arial; padding: 5px;">'; //set opening tag, such as font declarations

                    closetag = '</div>';

                    var fwidth = '500px'; //set scroller width
                    var fheight = '150px'; //set scroller height

                    var fadelinks = 1;  //should links inside scroller content also fade like text? 0 for no, 1 for yes.

                    ///No need to edit below this line/////////////////

                    var ie4 = document.all && !document.getElementById;
                    var DOM2 = document.getElementById;
                    var faderdelay = 0;
                    var index = 0;

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


                    function linkcolorchange(step) {
                        var obj = document.getElementById("fscroller").getElementsByTagName("A");
                        if (obj.length > 0) {
                            for (i = 0; i < obj.length; i++)
                                obj[i].style.color = getstepcolor(step);
                        }
                    }

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


                    function ChiudiDivMessaggio() {
                        if (document.getElementById('Messaggio')) {
                            document.getElementById('Messaggio').style.visibility = 'hidden';
                            document.getElementById("vis").value = '0';
                        }
                    }

                    function ApriNews(ID) {
                        document.getElementById('Scarica').value = '0';
                        location.href = 'News.aspx?T=1&ID=' + ID;
                    }
                </script>
            </div>
            <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/News.aspx?ID=-2" onclick="document.getElementById('Scarica').value='0';"
                Style="left: 784px; position: relative; top: -80px" Target="_top" ToolTip="Visualizza tutte le news relative all'ente collegato, con il testo completo.">Tutte le News</asp:HyperLink>
        </div>
        <!-- content -->
        <div id="sepa_content">
            <div id="sepa_leftcolumn">
                <div id="Colonna1" style="height: 190px" class="sepa_leftcolumn_twocolumn">
                    <%=SModulo1%>
                    <%=SModulo4%>
                    <%=SModulo7%>
                    <%=SModulo10%>
                    <%=SModulo13%>
                    <%=SModulo16%>
                    <%= SModulo19%>
                    <%= SModulo22%>
                    <%= SModulo25%>
                    <%= SModulo28%>
                </div>
                <div id="Colonna2" style="height: 190px" class="sepa_rightcolumn_twocolumn">
                    <%=SModulo2%>
                    <%=SModulo5%>
                    <%=SModulo8%>
                    <%=SModulo11%>
                    <%=SModulo14%>
                    <%= SModulo17%>
                    <%= SModulo20%>
                    <%= SModulo23%>                
                    <%= SModulo26%>
                </div>
            </div>
            <!-- end of left column -->
            <div id="sepa_rightcolumn">
                <!-- end of search panel -->
                <div id="sepa_blog_section" style="height: 190px; width: 253px">
                    <%=SModulo3%>
                    <%=SModulo6%>
                    <%=SModulo9%>
                    <%=SModulo12%>
                    <%= SModulo15%>
                    <%=SModulo18 %>
                    <%=SModulo21 %>
                    <%= SModulo24%>
                    <%= SModulo27%>
                </div>
            </div>
            <!-- end of right column -->
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <div class="cleaner">
                &nbsp;
            </div>
            <div class="sepa_leftcolumn_fullrow">
                <h1>
                    &nbsp;</h1>
                <h1>
                    Istruzioni per l&#39;utilizzo</h1>
                <p>
                    - <a href="Public/GuidaSepaWeb.pdf" target="_blank">Download Guida Operativa</a></p>
                Il sistema di accesso prevede la gestione delle utenze secondo quanto indicato dal
                Testo Unico sulla Privacy.
                <br />
                La password ha una durata di
                <asp:Label ID="lblInattivita" runat="server" Text="0"></asp:Label>
                giorni e una lunghezza non inferiore a
                <asp:Label ID="lblLunghezza" runat="server" Text="0"></asp:Label>
                caratteri
                <asp:Label ID="lblAlfa" runat="server" Text="0"></asp:Label>
                .
                <br />
                Deve inoltre essere modificata almeno una volta ogni
                <asp:Label ID="lblGiorni" runat="server" Text="0"></asp:Label>
                giorni e non può essere uguale alle ultime quattro precedentemente inserite.
                <br />
                <br />
                Al primo accesso è richiesta la modifica obbligatoria della password. Successivamente
                la modifica della password sarà richiesta dal sistema ogni
                <asp:Label ID="lblGiorni1" runat="server" Text="0"></asp:Label>
                giorni.
                <br />
                <br />
                Il sistema differenzia i caratteri minuscoli da quelli maiuscoli, pertanto si raccomanda
                di fare attenzione al momento della digitazione della password. In caso di smarrimento
                della password di accesso, l&#39;utente dovrà rivolgersi all&#39;amministratore
                del sistema per ottenerne una nuova.
                <br />
                <br />
                <p>
                    Applicativo :
                    <asp:Label ID="Label7" runat="server" Text="Ver.2.56"></asp:Label></p>
            </div>
            <!-- end of left full column -->
        </div>
        <!-- end of content -->
        <div id="sepa_footer">
            <a href="#">Home</a> | <a href="#">Chi Siamo</a> | <a href="Normativa.htm" target="_blank">
                Policy</a> | <a href="#">Help</a> | <a href="#">FAQs</a> | <a href="javascript:document.getElementById('Scarica').value='0';ApriContatti();">
                    Contatti</a><br />
            Copyright © 2009 <a href="#"><strong>SEPA@WEB</strong></a> | Designed by <a href="http://www.sistemiesoluzionisrl.it"
                target="_blank">Sistemi e Soluzioni</a>
        </div>
        <!--  Designed by sistemi e soluzioni srl  -->
    </div>
    <!-- end of container -->
    <script type="text/javascript">
        var ievs = (/MSIE (\d+\.\d+);/.test(navigator.userAgent));
        if (ievs) {

            var iev = new Number(RegExp.$1);

            if (iev <= 6) {

                alert('Attenzione...Stai usando una versione di Internet Explorer non pienamente supportata dal sistema. Ti consigliamo di aggiornare il tuo browser alla versione 7 o successive.');

            }

        }

        function detectPopupBlocker() {
            var test = window.open(null, "", "width=100,height=100");
            try {
                test.close();

            } catch (e) {
                alert("Attenzione, il blocco dei Popup è attivo. Per un corretto funzionamento del sistema è consigliabile abilitare questo sito all\'uso dei Popup.");
            }
        }

        //detectPopupBlocker();
    </script>
    <script type="text/javascript">
        document.getElementById('Colonna1').style.height = document.getElementById('C1').value + 'px';
        document.getElementById('Colonna2').style.height = document.getElementById('C2').value + 'px';
        document.getElementById('sepa_blog_section').style.height = document.getElementById('C3').value + 'px';

       

    </script>
</body>
</html>
