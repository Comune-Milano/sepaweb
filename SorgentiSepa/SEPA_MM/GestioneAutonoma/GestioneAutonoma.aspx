<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestioneAutonoma.aspx.vb" Inherits="GestioneAutonoma_GestioneAutonoma" %>

<%@ Register src="Tab_Dettaglio.ascx" tagname="Tab_Dettaglio" tagprefix="uc1" %>

<%@ Register src="Tab_Servizi.ascx" tagname="Tab_Servizi" tagprefix="uc2" %>

<%@ Register src="Note.ascx" tagname="Note" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestione Autonoma</title>

    <script type ="text/javascript" >
        document.write('<style type="text/css">.tabber{display:none;}<\/style>');

        //var tabberOptions = {'onClick':function(){alert("clicky!");}};
        var tabberOptions = {


            /* Optional: code to run when the user clicks a tab. If this
            function returns boolean false then the tab will not be changed
            (the click is canceled). If you do not return a value or return
            something that is not boolean false, */

            'onClick': function (argsObj) {

                var t = argsObj.tabber; /* Tabber object */
                var id = t.id; /* ID of the main tabber DIV */
                var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
                var e = argsObj.event; /* Event object */

                document.getElementById('txttab').value = i + 1;

            },
            'addLinkId': true

        };
          //Inizio scrittura funzioni Javascript per lato client
          
          function CompletaData(e, obj) {
            // Check if the key is a number
            var sKeyPressed;

            sKeyPressed = (window.event) ? event.keyCode : e.which;

            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    // don't insert last non-numeric character
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
            else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                }
                else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        // make sure the field doesn't exceed the maximum length
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
            }
        }

        function $onkeydown() {

            if (event.keyCode == 13) {
                event.keyCode = 0;
                document.getElementById('USCITA').value = '0';
                document.getElementById('txtModificato').value = '111';
            }

            if (event.keyCode == 46) {
                event.keyCode = 0;

            }

        }

        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
        }
        function AutoDecimal2(obj) {
        //controllo per giuseppe Epifani
//            if (obj.value.split('.').length = 2) {
//                obj.value.replace(/\./, ',')

//            }
//            else {
//                obj.value = obj.value.replace(/\./g, '');
//            }
//          
            obj.value = obj.value.replace(/\./g, '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a.substring(a.length - 3, 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - 2);
                    var dascrivere = a.substring(a.length - 3, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length) {

                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                    }
                    risultato = dascrivere + risultato + ',' + decimali
                    //document.getElementById(obj.id).value = a.replace('.', ',')
                    document.getElementById(obj.id).value = risultato
                }
                else {
                    document.getElementById(obj.id).value = a.replace('.', ',')
                }
            }
        } var Uscita;
        Uscita = 0;

        function ConfermaSalva() {
            if (document.getElementById('txtCodice').value == '') {
                var chiediConf
                chiediConf = window.confirm("Attenzione...L\'operazione di salvataggio è DEFINITIVA.\nNon sarà possibile apportare modifiche!Continuare?");
                if (chiediConf == true) {
                    document.getElementById('txtConferma').value = '1';
                }
            }
            else {
                document.getElementById('txtConferma').value = '1';
            }
         }

        function ConfermaEsci() {
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Continuare l\'operazione senza aver salvato?");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                    //document.getElementById('USCITA').value='0';
                }
            }
        }

        function ApriAllegati() {
                window.open('ElencoAllegati.aspx?LT=0&COD=<%=vIdGestAutonoma %>', 'Allegati', '');
        }


    </script>
        <style type="text/css" >
            .CssMaiuscolo { TEXT-TRANSFORM: uppercase}
		</style>
    <script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
    <script type="text/javascript" src="../Contratti/moo.fx.js"></script>
    <script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>

    <script type="text/javascript" src="../Contratti/tabber.js"></script>
    <link rel="stylesheet" href="../Contratti/example.css" type="text/css" media="screen"/>
    <style type="text/css">
        .style2
        {
            width: 426px;
            height: 80%;
        }
        .style4
        {
            height: 25px;
        }
    </style>
    </head>

<body style="background-attachment: fixed; background-image: url('Immagini/SfondoMascheraGestAutonoma.jpg'); background-repeat: no-repeat; width: 785px; height:500px">
    <!-- Da mettere subito dopo l'apertura del tag <body> -->

      <div id="splash"      
          
        
        
        style="border: thin dashed #000066; position :absolute; z-index :500; text-align:center; font-size:10px; width: 85%; height: 76%; vertical-align: top; line-height: normal; top: 52px; left: 58px; background-color:#FFFFFF ;">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <img src='Immagini/load.gif' alt='caricamento in corso'/><br/><br/>
            caricamento in corso...<br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            &nbsp;</div>    
    <% Response.Flush %>
    <form id="form1" runat="server">
    <asp:ImageButton ID="btnIndietro" runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
            
        Style="z-index: 103; left: 10px; cursor: pointer; position: absolute; top: 27px; height: 12px;" 
        ToolTip="Torna ai risultati della ricerca" CausesValidation="False" />
                           <img border="0" 
        alt="Eventi delle modifiche apportate dagli operatori all'autogestione visualizzata" id="ImgEventi" 
            src="../NuoveImm/Img_Eventi.png" 
            style="cursor: pointer; left: 689px; position: absolute; top: 26px; right: 147px;" 
        onclick="window.open('Eventi.aspx?IDAUTOGESTIONE=<%=vIdGestAutonoma %>','Eventi', '');"/><asp:ImageButton ID="btnEsci" runat="server" ImageUrl="~/Condomini/Immagini/Img_Esci.png" 
            Style="z-index: 103; left: 752px; cursor: pointer; position: absolute; top: 26px; height: 12px;"
            ToolTip="Esci" OnClientClick="ConfermaEsci();document.getElementById('splash').style.visibility = 'visible'"/>
            <asp:ImageButton ID="btnNuovoEsercizio" runat="server" ImageUrl="~/GestioneAutonoma/Immagini/NuovoEsercizio.png" 
            Style="z-index: 103; left: 127px; cursor: pointer; position: absolute; top: 27px;"
            ToolTip="Trasporta l'autogestione in un nuovo esercizio" OnClientClick ="document.getElementById('splash').style.visibility = 'visible'"

        Visible="False" />


            <img id="imgAllegaFile" alt="Allega File" border="0" 
                onclick="window.open('../InvioAllegato.aspx?T=5&amp;ID=<%=vIdGestAutonoma %>', 'Allegati', '');" 
                src="../Condomini/Immagini/Img_InviaAllegato.png" 
                
        style="cursor: pointer; left: 226px; position: absolute; top: 26px; " /><img 
        border="0" alt="Allegati" id="imgAllegati" 
            src="../Condomini/Immagini/Img_allegati.png" 
            style="cursor: pointer; left: 322px; position: absolute; top: 26px; right: 458px; " 
            
            
            
            
                
                onclick="ApriAllegati();"/><br />
    <br />
            <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/Condomini/Immagini/Img_Salva.png" 
            Style="z-index: 103; left: 70px; cursor: pointer; position: absolute; top: 27px;"
            ToolTip="Salva" OnClientClick="document.getElementById('splash').style.visibility = 'visible';ConfermaSalva();" />


        <table cellpadding ="1" cellspacing = "1">
        <tr>
            <td style="vertical-align: top; text-align: left; ">
                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    Text="Edificio *"></asp:Label>
            </td>
            <td style="vertical-align: top; text-align: left; ">
                &nbsp;</td>
            <td style="vertical-align: top; text-align: left">
        <asp:Label ID="lblSituazione" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            >MOROSITA' IMMOBILE</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left">
                <div style="border: thin solid #6699ff; z-index: 10; width: 433px; visibility :visible;
                height: 77px; vertical-align: top; text-align: left; overflow: auto;" 
                    id="DivEdifici">
                <asp:Label ID="lblEdifici" runat="server" Font-Names="Arial" Font-Size="9pt" 
                    Width="95%"></asp:Label></div>
            </td>
            <td style="vertical-align: top; text-align: left">
                <asp:Image ID="imgAddEdificio" runat="server" 
                    ImageUrl="~/GestioneAutonoma/Immagini/k-hex-edit-icon.png"
                    onclick="document.getElementById('ScegEdifVis').value!='1';myOpacityEdif.toggle();" 
                    ToolTip="Aggiungi/Rimuovi Edificio alla lista" style = "cursor:hand" />
            </td>
            <td rowspan = "3" style="vertical-align: top; text-align: left">
        <div style="border: thin solid gainsboro; width: 295px; vertical-align: top; text-align: left; " 
                    id="divMorosita">
            <table width="100%" cellpadding="1" cellspacing="1" style="height: 80px">
                <tr>
                    <td style="width: 142px; height: 17px">
                        <span style="font-size: 8pt; font-family: Arial"><em>Bollettato</em></span></td>
                    <td style="width: 53px; height: 17px; vertical-align: top; text-align: right;">
                        <asp:Label ID="lblBollettato" runat="server" Font-Bold="True" Font-Italic="True"
                            Font-Names="Arial" Font-Size="8pt" Style="z-index: 100; left: 478px; top: 251px"
                            Width="57px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 142px">
                        <em><span style="font-size: 8pt; font-family: Arial">Pagato</span></em></td>
                    <td style="width: 53px; vertical-align: top; text-align: right;" 
                        alt="Lista delle modifiche apportate dagli operatori all'autogestione visualizzata">
                        <asp:Label ID="lblPagato" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Arial"
                            Font-Size="8pt" Style="z-index: 100; left: 478px; top: 251px" Width="57px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 142px">
                        <em><span style="font-size: 8pt; font-family: Arial; border-bottom-width: 1px; border-bottom-color: #000033;">Morosità percentuale</span></em></td>
                    <td style="vertical-align: top; width: 53px; text-align: right">
                        <asp:Label ID="lblPercentuale" runat="server" Font-Bold="True" Font-Italic="True"
                            Font-Names="Arial" Font-Size="8pt" Style="z-index: 100; left: 478px; top: 251px"
                            Width="57px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 142px">
                        <em><span style="font-size: 8pt; font-family: Arial; border-bottom-width: 1px; border-bottom-color: #000033;">
                            Percentuale Abusivismo</span></em></td>
                    <td style="vertical-align: top; width: 53px; text-align: right">
                        <asp:Label ID="lblPercAbusiv" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Arial"
                            Font-Size="8pt" Style="z-index: 100; left: 478px; top: 251px" Width="57px"></asp:Label></td>
                </tr>
            </table>

        </div>
            </td>
        </tr>

        </table>
    <table border="0"; cellspacing = "2"; cellpadding = "0";>
        <tr>
            <td>
                <asp:Label ID="lbl1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    Text="Denominazione"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl2" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    Text="Cod."></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl3" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    Text="Data Cost."></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl6" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    Text="C/C"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl5" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    Text="Anno" Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style4">
                <asp:TextBox ID="txtDenominazione" runat="server" Font-Names="Arial" 
                    Font-Size="8pt" Width="240px" Wrap="False" TabIndex="3" 
                    CssClass="CssMaiuscolo" MaxLength="70"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:TextBox ID="txtCodice" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    Wrap="False" ReadOnly="True" TabIndex="4"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:TextBox ID="txtDataCostituzione" runat="server" Font-Names="Arial" 
                    Font-Size="8pt" Wrap="False" TabIndex="5" Width="75px" MaxLength="10"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataCostituzione"
            ErrorMessage="!" Font-Bold="True" 
            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
            ToolTip="Inserire una data valida" Font-Names="Arial" Font-Size="10pt" 
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td class="style4">
                <asp:TextBox ID="txtContoCorrente" runat="server" Font-Names="Arial" 
                    Font-Size="8pt" Width="190px" Wrap="False" TabIndex="6" 
                    CssClass="CssMaiuscolo" MaxLength="20"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:DropDownList ID="cmbAnno" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="8pt" Width="89px" TabIndex="7" 
                    AutoPostBack="True">
                </asp:DropDownList>
        <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="18px" Style="z-index: 104; left: 76px; position: absolute;
            top: 12px; width: 591px;" Visible="False"></asp:Label>
            </td>
        </tr>
        </table>

        <div 
        style="border: thin solid #6699ff; z-index: 100; left: 12px; width: 433px; position: absolute; visibility :hidden;
            top: 96px; height: 329px; vertical-align: top; background-color: #DCDCDC; text-align: left;" 
        id="ScegliEdifici">
            <table style="width: 99%; height: 91%">
                <tr>
                    <td style="vertical-align: top; width: 426px;height: 2%; text-align: left">
        <asp:Label ID="lblSituazione0" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            >Elenco Complessi Immobiliari</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; width: 426px; height: 2%; text-align: left">
                        <asp:DropDownList ID="cmbComplessi" runat="server" 
                            ToolTip="Elenco dei Complessi Immobiliari" Width="98%" Font-Names="Arial" 
                            Font-Size="8pt" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; width: 426px; height: 2%; text-align: left">
        <asp:Label ID="lblSituazione1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            >Elenco Edifici</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; text-align: left" class="style2">
                        <div style="overflow: auto; width: 100%; height: 80%">
            <asp:CheckBoxList ID="ListEdifici" runat="server" Font-Names="Arial" Font-Size="8pt"
                Style="left: 334px; top: 251px" Width="403px">
            </asp:CheckBoxList></div>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; text-align: right; ">
                        <asp:ImageButton ID="imgAggiornaEdifici" runat="server" ImageUrl="~/Condomini/Immagini/Aggiungi.png"
            
            Style="z-index: 103; left: 744px; cursor: pointer; top: 26px" 
            ToolTip="Esci" CausesValidation="False" OnClientClick ="document.getElementById('splash').style.visibility = 'visible'"/></td>
                </tr>
            </table>
        </div>
        <br />

        <div id="MyTabAuto" class="tabber" style="text-align: left; visibility :visible;">
            <div class= "tabbertab <%=tabdefault1%>" title ="Dettaglio">
                <uc1:Tab_Dettaglio ID="Tab_Dettaglio1" runat="server" />
            </div> 
            <div class= "tabbertab <%=tabdefault2%>" title ="Servizi">
            
                <uc2:Tab_Servizi ID="Tab_Servizi1" runat="server" />
            
            </div> 
            <div class= "tabbertab <%=tabdefault3%>" title ="Note">
            
            
                <uc3:Note ID="Note1" runat="server" />
            
            
            </div> 

        </div>
        
        <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
        <asp:HiddenField ID="txtvisibility" runat="server" Value="1" />
        <asp:HiddenField ID="txttab" runat="server" Value="1" />
        <asp:HiddenField ID="SoloLettura" runat="server" />         
        <asp:HiddenField ID="ScegEdifVis" runat="server" Value="1" />
        <asp:HiddenField ID="txtNuovoEs" runat="server" Value="0" />
        <asp:HiddenField ID="txtConferma" runat="server" Value="0" />
        
        <script  language="javascript" type="text/javascript">

            myOpacityEdif = new fx.Opacity('ScegliEdifici', { duration: 200 });
            if (document.getElementById('ScegEdifVis').value != '2') {
                myOpacityEdif.hide(); ;
            }
            MyOpctServ = new fx.Opacity('DivServizi', { duration: 200 });
            if (document.getElementById('Tab_Servizi1$txtVisServ').value != '2') {
                MyOpctServ.hide(); ;
            }

        </script>
    </form>
    <!-- Da mettere subito prima della chiusura del tag </body> -->
    <script type="text/javascript"  language="JavaScript">
        document.getElementById('splash').style.visibility = 'hidden';
        document.getElementById('dvvvPre').style.visibility = 'hidden';
        if (document.getElementById("txtCodice").value > 0 && document.getElementById("txtNuovoEs").value != 1) {
            document.getElementById("imgAddEdificio").style.visibility = 'hidden';
            document.getElementById("Tab_Dettaglio1$txtCognome").readOnly = 'true';
            document.getElementById("Tab_Dettaglio1$txtnome").readOnly = 'true';
            document.getElementById("Tab_Dettaglio1$txtcf").readOnly = 'true';
            document.getElementById("Tab_Dettaglio1$txtrecapito").readOnly = 'true';
            document.getElementById("Tab_Dettaglio1_imgAddConv").style.visibility = 'hidden';
            document.getElementById("Tab_Servizi1_imgAddServ").style.visibility = 'hidden';
            document.getElementById("imgAllegaFile").style.visibility == 'visible';
            document.getElementById("imgAllegati").style.visibility == 'visible';
            document.getElementById("imgEventi").style.visibility == 'visible';
        }
        else {
            if (document.getElementById("txtNuovoEs").value == 1) { document.getElementById("imgAddEdificio").style.visibility = 'hidden'; }
            else { document.getElementById("imgAddEdificio").style.visibility = 'visible'; }
            document.getElementById("Tab_Dettaglio1$txtCognome").readOnly == 'false';
            document.getElementById("Tab_Dettaglio1$txtnome").readOnly == 'false';
            document.getElementById("Tab_Dettaglio1$txtcf").readOnly == 'false';
            document.getElementById("Tab_Dettaglio1$txtrecapito").readOnly == 'false';
            document.getElementById("Tab_Dettaglio1_imgAddConv").style.visibility = 'visible';
            document.getElementById("Tab_Servizi1_imgAddServ").style.visibility == 'visible';
            document.getElementById("imgAllegaFile").style.visibility = 'hidden';
            document.getElementById("imgAllegati").style.visibility = 'hidden';
            document.getElementById("imgEventi").style.visibility = 'hidden';

        }
        
        if (document.getElementById('SoloLettura').value != 0) {
            document.getElementById("imgAllegaFile").style.visibility = 'hidden';
            document.getElementById("imgAllegati").style.visibility = 'hidden';
            document.getElementById("imgEventi").style.visibility = 'hidden';          
        }

    </script>
</body>
</html>
