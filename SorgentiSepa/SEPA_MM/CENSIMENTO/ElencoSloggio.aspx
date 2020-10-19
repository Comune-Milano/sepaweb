<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoSloggio.aspx.vb" Inherits="CENSIMENTO_ElencoSloggio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Verifica Stato Manutentivo</title>
    <link href="../MANUTENZIONI/Styles/Site.css" rel="stylesheet" type="text/css" />
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
            'notnumbers': /[^\d\-\,]/g
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

       
       function AbilitaDrop(status)
            {
               status=!status;
                 document.getElementById('Ddl_mano').disabled = status;
                 document.getElementById('Ddl_sopL').disabled = status;
                 }


        function AbilitaTxtLF(status)
            {
               status=!status;
                 document.getElementById('lastraF_txt').disabled = status;
                
                 }
          
           function AbilitaTxtLPF(status)
            {
               status=!status;
                 document.getElementById('lastraPF_txt').disabled = status;
                
                 }

           function AbilitaTxtSerr(status)
            {
               status=!status;
                 document.getElementById('serr_txt').disabled = status;
                
                 }

              

//               function VisualizzaGestione() {
//          
//                            window.open('GestSloggio.aspx?PROVENIENZA=1&ID=' + document.getElementById('idunita').value + '&IDSTATO=' + document.getElementById('id_stato').value + '&IDSLOGGIO=' + document.getElementById('id_sloggio').value, '');
//             
//            }
//       








    </script>
    <style type="text/css">
        .style1
        {
            color: #0000CC;
            font-size: 8pt;
        }
        
        
        #form1
        {
            width: 1305px;
        }
        .style4
        {
            width: 79%;
        }
        .style5
        {
            width: 12%;
        }
        .style6
        {
            width: 5%;
        }
        .style8
        {
            height: 55px;
        }
        .style9
        {
            height: 19px;
        }
        .style59
        {
            width: 14%;
        }
        </style>
</head>
<body style="width: 99%; position: absolute; background-attachment: fixed; top: -12px;
    background-image: url('IMMCENSIMENTO/Sfondo.png'); background-repeat: repeat-x;"
    bgcolor="#fdfdfd">
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
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
    <form id="form1" runat="server" defaultfocus="cmbDecentramento" style="width: 100%">
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
            <td class="style6">
                &nbsp;
            </td>
            <td class="style59">
                <%--   &nbsp;<asp:ImageButton ID="stampa_btn" runat="server" ImageUrl="~/NuoveImm/Img_Stampa.png"
                   onClientclick="StampaModulo();" Tooltip="Stampa modulo pre-sloggio" ImageAlign="Top"  style="cursor: pointer;" />--%>
            </td>
            <td class="style5">
                &nbsp;</td>
            <td class="style4">
            </td>
            <td class="style5">
                &nbsp;<asp:ImageButton ID="ImButEsci" runat="server" OnClientClick="ConfermaEsci();"
                    ToolTip="Esci" ImageUrl="~/NuoveImm/Img_Esci.png" Style="cursor: pointer;" ImageAlign="Top" />
            </td>
        </tr>
    </table>
    <table style="width: 98%; margin-left: 20px; margin-right: 10px; height: 46px;" cellpadding="0">
        <tr>
            <td class="style9">
                <asp:Label ID="titScheda" runat="server" Style="font-size: 12pt; color: #801f1c;
                    font-family: Arial; margin-left: 10px; text-align: center; margin-top: 0px;"
                    Width="99%" Height="20px"><strong>ELENCO SLOGGI</strong></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanelDatiUI" runat="server">
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
                                <td style="text-align: left" valign="middle" class="style8">
                                    <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label class="stLbDescrizione" ID="lblPianoVendita" runat="server" Font-Bold="True"
                                        Font-Size="10pt" Font-Names="Arial" Text="Zona Decentramento" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
            </table>
        </ContentTemplate>

    </asp:UpdatePanel>
    <br />
    <br />
    <br />


     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table style="width: 99%; margin-left: 13px;">
        <tr>
            <td class="style1" style="font-family: Arial">
                <strong>ELENCO SLOGGI DELL&#39;UNITA&#39; IMMOBILIARE</strong>
            </td>
        </tr>
        <tr>
            <td style="border: 1px solid #0066FF">
                <table style="width: 100%; height: 276px;">
                    <tr>
                        <td width="95%" valign="top">
                           <div style="margin-left: 5px;">
                            <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial;" >
                               <asp:DataGrid ID="dgElenco" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                                    PageSize="10" Style="z-index: 105; left: 193px; top: 54px" Width="100%" 
                                ForeColor="#333333" AllowPaging="True">
                                    <ItemStyle BackColor="#EFF3FB" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                        Position="Top" VerticalAlign="Top" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <EditItemStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="White" />
                                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_STATO" HeaderText="ID_STATO" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="STATO" HeaderText="STATO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DATA_PRESLOGGIO" HeaderText="DATA PRE-SLOGGIO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DATA_SLOGGIO" HeaderText="DATA SLOGGIO"></asp:BoundColumn>
			                        	
				
				                        </Columns>
                                        
                                        <EditItemStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="White" />
                                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:DataGrid>
                                </span></strong>
                            </div>
                        </td>

                        <td width="5%" valign="top">
                        
                         <asp:ImageButton id="btn_modifica" tooltip="Visualizza Dettagli" src="../NuoveImm/view_ico.png" runat="server" style="cursor: pointer" />
                        
                        </td>
                      
                    </tr>
                    

                </table>
            </td>
        </tr>
    </table>
    </ContentTemplate>

      <Triggers>
         
                        <asp:AsyncPostBackTrigger ControlID="btn_modifica" EventName="Click" />
                     

                        </Triggers>



    </asp:UpdatePanel>

    <br />
    <br />
    <br />
    <div>
    </div>
    <br />
    <br />
    <%--<asp:DataGrid ID="dgstati" runat="server" AutoGenerateColumns="False" CellPadding="6"
                                    Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                    GridLines="None" Width="98%" HeadersVisibility="None" ShowHeader="False" Visible="false">
                                    <ItemStyle BackColor="#EFF3FB" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                        Position="Top" VerticalAlign="Top" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <EditItemStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="White" />
                                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_STATO" HeaderText="ID_STATO" Visible="False"></asp:BoundColumn>

                                   
                                    </Columns>
                                </asp:DataGrid>

    --%>
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
    <asp:HiddenField ID="interno" runat="server" />


     <asp:HiddenField ID="id_stato" runat="server" />
    <asp:HiddenField ID="id_sloggio" runat="server" />

    <script language="javascript" type="text/javascript">
        var Selezionato;
        var OldColor;
        var SelColo;

        document.getElementById('caric').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>
