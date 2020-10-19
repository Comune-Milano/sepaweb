<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VerbaleSloggio.aspx.vb"
    Inherits="CENSIMENTO_VerbaleSloggio" %>

<%@ Register Src="Tab_DatiUI.ascx" TagName="Tab_DatiUI" TagPrefix="uc1" %>
<%@ Register Src="Tab_SanitRubinet.ascx" TagName="Tab_SanitRubinet" TagPrefix="uc2" %>
<%@ Register Src="Tab_Serramenti.ascx" TagName="Tab_Serramenti" TagPrefix="uc3" %>
<%@ Register Src="Tab_PavimRivest.ascx" TagName="Tab_PavimRivest" TagPrefix="uc4" %>
<%@ Register Src="Tab_Note.ascx" TagName="Tab_Note" TagPrefix="uc5" %>
<%@ Register Src="Tab_GeneraleUI.ascx" TagName="Tab_GeneraleUI" TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" language="javascript">
    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            initialize();
        }
    }
</script>
<head id="Head1" runat="server">
    <title>Rapporto Sloggio</title>
    <link href="../MANUTENZIONI/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.19.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="function.js">
    </script>
    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>
    <script type="text/javascript">
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

                       if((obj.value.substring(0,2)) <= 31 )
                       {
                       obj.value += "/";
                       
                       }
                       else {
                          if (window.event) {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            }
                       
                       }


                        
                    }
                    else if (obj.value.length == 5) {
                        if((obj.value.substring(3,5)) <= 12 )
                       {
                        obj.value += "/";
                        }else{
                        
                         if (window.event) {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            }
                        
                        
                        
                        }



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

            function ConfermaEsci() {

                if (document.getElementById('Modificato').value == '1') {
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                    if (chiediConferma == true) {
                        self.close();
                        //document.getElementById('Modificato').value = '111';
                        //document.getElementById('USCITA').value='0';
                    }
                }
                else {
                    self.close();
                }
            }

  

            function StampaModulo() {
                window.open('ModuloPresloggio.aspx?', 'Modulo', '');

            }


            
            function StampaModuloRitiroChiavi() {
                window.open('ModuloRitChiavi.aspx?', 'Modulo', '');

            }



              function StampaModuloRapportoSloggio() {
                window.open('ModuloRappSloggio.aspx?', 'Modulo', '');

            }
            




       function AllegaFile() {
            var ins = <%=Inserimento %> 
            if (ins !='-1'){
                            window.open('InviaAllStatoManut.aspx?IDUNITA=' + +document.getElementById('idunita').value, 'Allegato', 'height=500px,width=900px,resizable=no');
}
else
{
alert('Salvare lo stato manutentivo prima di allegare un file!')}
            
            }

            function ElencoAllegati() {
                                        window.open('ElencoAllegati.aspx?IDUNITA=' + +document.getElementById('idunita').value, 'Allegato', 'height=500px,width=900px,resizable=no');

            
            }




  function VisualizzaSloggio() {
              if (document.getElementById('idunita').value != '-1') 
                {
                    window.open('Sloggio.aspx?A=' + document.getElementById('chiamante').value + '&ID=' + document.getElementById('idunita').value, '');
   
            }

            
            }



           function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13) {
                e.preventDefault();
                //document.getElementById('USCITA').value = '0';
                //document.getElementById('txtModificato').value = '111';
            }
        }


        function $onkeydown() {

            if (event.keyCode == 13) {
                event.keyCode = 0;
                // document.getElementById('USCITA').value = '0';
                //document.getElementById('txtModificato').value = '111';
            }
        }

        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g,
            'notnumbersint': /[^\d]/g

           
        }


        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            o.value = o.value.replace('.', ',');
            //document.getElementById('txtModificato').value = '1';
        }



        function DelPointer(obj) {
            obj.value = obj.value.replace('.', '');
            document.getElementById(obj.id).value = obj.value;

        }


    


        function $onkeydown() {
            if ((event.keyCode == 46) || (event.keyCode == 8) || (event.keyCode == 116)) {
                event.keyCode = 0;
            }
        }



           function AutoDecimal2(obj) {
            obj.value = obj.value.replace(/\./gi, '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a.substring(a.length - 3, 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - 2);
                    var dascrivere = a.substring(a.length - 3, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {

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
        }







        function SostPuntVirg(e, obj) {
            var keyPressed;

            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {

                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }


                obj.value += ',';
                obj.value = obj.value.replace('.', '');

            }

        };









          function CalcolaAddebito(obj, val, addebito) {


            var qt = document.getElementById('' + obj.id + '').value.replace(",",".");

           
            var csu = val;
          


            var adTot;

            adTot = qt * csu;

           adTot=parseFloat(adTot).toFixed(2);

            document.getElementById('' + addebito.id + '').value = adTot

//            if (document.getElementById('' + addebito.id + '').value.indexOf('.') !=-1){



            document.getElementById('' + addebito.id + '').value = document.getElementById('' + addebito.id + '').value.replace(".",",");
       
//            }
        }

       

       function CalcolaTotale() {
       
        document.getElementById('calcolaTot_btn').click()
       
       
       }



       function SettaTestoOncosto(obj) {
       
       
         var txt = document.getElementById('' + obj.id + '').value;

       

         if ((txt=='0') || ( txt=='0,00')) {
         
          txt = '';
         document.getElementById('' + obj.id + '').value= txt;
         
         }
       
      
      

       
       }





       function SettaTestoOutcosto(obj) {
       
       
         var txt = document.getElementById('' + obj.id + '').value;

       

         if ((txt=='') || (txt=='0')) {
         
          txt = '0,00';
          document.getElementById('' + obj.id + '').value= txt;
        
         
         }
       
      
      

       
       }


        function SettaTestoOnquant(obj) {
       
       
         var txt = document.getElementById('' + obj.id + '').value;

       

         if ((txt=='0') || ( txt=='0,00')) {
         
          txt = '';
         document.getElementById('' + obj.id + '').value= txt;
         
         }
       
      
      

       
       }





       function SettaTestoOutquant(obj) {
       
       
         var txt = document.getElementById('' + obj.id + '').value;

       

         if (txt=='') {
         
          txt = '0';
          document.getElementById('' + obj.id + '').value= txt;
        
         
         }
       
      
      

       
       }

                function StampaVerbCompl() {
        
             
               window.open('StampaVerbaleSL.aspx?ID=' + document.getElementById('idunita').value + '&IDSTATO= ' + document.getElementById('id_stato').value + '&IDSLOGGIO= ' + document.getElementById('id_sloggio').value  + '&STVERB= ' + document.getElementById('stato_verb').value, '');
             
             


            }



         






    </script>
    <style type="text/css">
        .style1
        {
            color: #0000CC;
            font-size: 9pt;
        }
        
        
        #form1
        {
            width: 1305px;
        }
      
      
     
      
        .style9
        {
            height: 19px;
            font-family: Arial;
            font-size: 14pt;
            text-align: center;
            color: #990000;
        }
      
      
     
    </style>
</head>
<body style="width: 99%; position: absolute; top: -12px; background-image: url('IMMCENSIMENTO/Sfondo.png');
    background-repeat: repeat-x;">
    <div id="caric" style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%;
        position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75;
        background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Image ID="Image2222" runat="server" ImageUrl="..\NuoveImm\load.gif" />
                        <br />
                        <br />
                        <asp:Label ID="lblcarica222" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                            Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <%Response.Flush()%>

    <form id="form1" runat="server" style="width: 100%" >
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgressEventi" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed;
                top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee;
                z-index: 500">
                <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
                    margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
                    <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="middle" align="center">
                                <asp:Image ID="Image2" runat="server" ImageUrl="..\NuoveImm\load.gif" />
                                <br />
                                <br />
                                <asp:Label ID="lblcarica" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                                    Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <table style="width: 100%; height: 70px;">
        <tr>
            <td width="5%">
                &nbsp;
            </td>
            <td width= "10%">
                &nbsp;<asp:ImageButton ID="salva_btn" runat="server" ToolTip="Salva" ImageUrl="~/NuoveImm/Img_Salva.png"
                    Style="cursor: pointer;" ImageAlign="Top" />
            </td>
            <td width= "10%">
                <asp:ImageButton ID="stampa_btn" runat="server" ImageAlign="Top" ImageUrl="~/NuoveImm/Img_Stampa.png"
                    OnClientClick="StampaVerbCompl();" Style="cursor: pointer;" ToolTip="Stampa verbale completo" />
            </td>
            <td style= "width: 78%; height: 15px;">
            </td>
            <td width= "12%">
                &nbsp;<asp:ImageButton ID="ImButEsci" runat="server" OnClientClick="ConfermaEsci();"
                    ToolTip="Esci" ImageUrl="~/NuoveImm/Img_Esci.png" Style="cursor: pointer;" ImageAlign="Top" />
            </td>
        </tr>
    </table>
    <table style="width: 98%; margin-left: 20px; margin-right: 10px; height: 38px;" cellpadding="0">
        <tr>
            <td class="style9">
                <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="14pt" Text="VERBALE RAPPORTO DI SLOGGIO"></asp:Label>
&nbsp;</td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <table style="width: 99%; height: 63px;">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width: 99%; margin-left: 13px; height: 63px;">
                            <tr>
                                <td class="style1" style="font-family: Arial">
                                    <strong>DATI DELL'UNITA' IMMOBILIARE</strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #0066FF">
                                    <table width="100%" style="height: 70px">
                                        <tr>
                                            <td style="text-align: left" valign="middle" height= "55px">
                                                <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                                <br />
                                                <br />
                                                <asp:Label class="stLbDescrizione" ID="lblPianoVendita" runat="server" Font-Bold="True"
                                                    Font-Size="10pt" Font-Names="Arial" Text="Zona Decentramento" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="tabs">
                                        <ul>
                                            <li><a href="#tabs-1">Dati UI</a></li>
                                            <li><a href="#tabs-2">Sanitari e Rubinetterie</a></li>
                                            <li><a href="#tabs-3">Serramenti</a></li>
                                            <li><a href="#tabs-4">Pavimenti, Rivestimenti e Tavolati</a></li>
                                            <li><a href="#tabs-5">Note del tecnico</a></li>
                                             <li><a href="#tabs-6">Generalità UI</a></li>
                                        </ul>
                                        <div id="tabs-1">
                                            <uc1:Tab_DatiUI ID="Tab_DatiUI1" runat="server" />
                                        </div>
                                        <div id="tabs-2">
                                            <uc2:Tab_SanitRubinet ID="Tab_SanitRubinet1" runat="server"  />
                                        </div>
                                        <div id="tabs-3">
                                            <uc3:Tab_Serramenti ID="Tab_Serramenti1" runat="server" />
                                        </div>
                                        <div id="tabs-4">
                                            <uc4:Tab_PavimRivest ID="Tab_PavimRivest1" runat="server" />
                                        </div>
                                        <div id="tabs-5">
                                            <uc5:Tab_Note ID="Tab_Note1" runat="server" />
                                        </div>
                                         <div id="tabs-6">
                                            <uc6:Tab_GeneraleUI ID="Tab_GeneraleUI1" runat="server" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="salva_btn" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="margin-left: 13px;" width="90%">
                <table style="width: 93%; margin-left: 34px;">
                    <tr>
                        <td style="text-align: left; border: 1px solid #0066FF; margin-left: 40px" valign="middle">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table width="100%" style="height: 117px">
                                        <tr>
                                            <td style="text-align: right; width: 50%;" valign="middle" align="right"  width= "50%">
                                                <asp:Label ID="Label1" runat="server" Text="TOTALE ADDEBITI ESCLUSO IVA:" Width="193px"></asp:Label>
                                                <br />
                                            </td>
                                            <td align="right" width= "35%">
                                                <asp:Label ID="Label4" runat="server" Text="€"></asp:Label>
                                            </td>
                                            <td style="text-align: right" valign="middle" align="right" width= "12%">
                                                <asp:TextBox ID="totAdd_txt" Style="text-align: right" runat="server">0</asp:TextBox>
                                                <br />
                                            </td>
                                            <td style="text-align: left" valign="middle">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right" valign="middle" align="right"  width= "50%">
                                                <asp:Label ID="Label2" runat="server" Text="STIMA DEI COSTI DI RIATTAMENTO:" Width="193px"></asp:Label>
                                                <br />
                                                <td align="right" width= "35%">
                                                    <asp:Label ID="Label5" runat="server" Text="€"></asp:Label>
                                                </td>
                                            </td>
                                            <td style="text-align: right" valign="middle" align="right" width= "12%">
                                                <asp:TextBox ID="stimaCosti_txt" Style="text-align: right" runat="server">0</asp:TextBox>
                                                <br />
                                            </td>
                                            <td style="text-align: left" valign="middle">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right" valign="middle" align="right"  width= "50%">
                                                <asp:Label ID="Label3" runat="server" Text="ADEGUAMENTO NORMATIVO:" Width="193px"></asp:Label>
                                                <br />
                                                <td align="right" width= "35%">
                                                    <asp:Label ID="Label6" runat="server" Text="€"></asp:Label>
                                                </td>
                                            </td>
                                            <td style="text-align: right" valign="middle" align="right" width= "12%">
                                                <asp:TextBox ID="adNormativo_txt" Style="text-align: right" runat="server">0</asp:TextBox>
                                                <br />
                                            </td>
                                            <td style="text-align: left" valign="middle">
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="calcolaTot_btn" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="salva_btn" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <div>
        <asp:Button ID="calcolaTot_btn" runat="server" Text="Button" Style="visibility: hidden;" />
        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
            ForeColor="Red" Visible="False"></asp:Label>
        <asp:HiddenField ID="Modificato" runat="server" />
        <asp:HiddenField ID="idunita" runat="server" />
        <asp:HiddenField ID="EVENTO" runat="server" />
        <asp:HiddenField ID="idcontratto" runat="server" />
        <asp:HiddenField ID="CODICE" runat="server" />
        <asp:HiddenField ID="chiamante" runat="server" Value="0" />
        <asp:HiddenField ID="statoc" runat="server" />
        <asp:HiddenField ID="lettura" runat="server" />
        <asp:HiddenField ID="tipounita" runat="server" />
        <asp:HiddenField ID="datasloggio" runat="server" />
        <asp:HiddenField ID="via_civico" runat="server" />
        <asp:HiddenField ID="scala" runat="server" />
        <asp:HiddenField ID="piano" runat="server" />
        <asp:HiddenField ID="quartiere" runat="server" />
        <asp:HiddenField ID="sup_mq" runat="server" />
        <asp:HiddenField ID="tabSelect" runat="server" Value="-1" />
        <asp:HiddenField ID="id_stato" runat="server" />
        <asp:HiddenField ID="id_sloggio" runat="server" />
        <asp:HiddenField ID="st_1" runat="server" />
        <asp:HiddenField ID="st_2" runat="server" />
        <asp:HiddenField ID="st_3" runat="server" />
        <asp:HiddenField ID="st_4" runat="server" />
        <asp:HiddenField ID="stato_verb" runat="server" value = "0" />
    </div>
    <script type="text/javascript" language="javascript">
        var Selezionato;
        var OldColor;
        var SelColo;

        initialize();

        function initialize() {

            var availableTags = [
			"ActionScript",
			"AppleScript",
			"Asp",
			"BASIC",
			"C",
			"C++",
			"Clojure",
			"COBOL",
			"ColdFusion",
			"Erlang",
			"Fortran",
			"Groovy",
			"Haskell",
			"Java",
			"JavaScript",
			"Lisp",
			"Perl",
			"PHP",
			"Python",
			"Ruby",
			"Scala",
			"Scheme"
		];


            $('#tabs').tabs();


            //            $(document).ready(function () {$("#tabs").bind('tabsselect', function(event, ui) {window.location.href=ui.tab;});});

            $(document).ready(function () {
                $("#tabs").bind('tabsselect', function (event, ui) {
                    window.location.href = ui.tab;
                    document.getElementById("tabSelect").value = $('#tabs').tabs('option', 'selected')
                }
	        );
            }
            );

            if (document.getElementById("tabSelect").value != '-1') {
                document.getElementById("tabSelect").value = $('#tabs').tabs('option', 'selected') + 1
                $('#tabs').tabs('select', document.getElementById("tabSelect").value);
            }



        }





    </script>
    <script language="javascript" type="text/javascript">
        document.getElementById('caric').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>
