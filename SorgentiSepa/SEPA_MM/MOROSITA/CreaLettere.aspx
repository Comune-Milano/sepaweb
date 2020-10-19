<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreaLettere.aspx.vb" Inherits="MOROSITA_CreaLettere" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">



    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) {
            e.preventDefault();
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }


    function $onkeydown() {

        if (event.keyCode == 13) {
            event.keyCode = 0;
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }

    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d\-\,]/g
    }
    
    


            

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>CREA LETTERA MOROSITA</title>
    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>
    <script type="text/javascript" src="tabber.js"></script>
    <link rel="stylesheet" href="example.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">

        //window.onbeforeunload = confirmExit; 



        function ConfermaEsci() {
            //alert('ciao');
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                    //document.getElementById('USCITA').value='0';
                }
            }
        }


        function confirmExit() {
            if (document.getElementById("USCITA").value == '0') {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.returnValue = "Attenzione...Uscire dalla scheda premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
                else {
                    return "Attenzione...Uscire dalla scheda premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
            }
        }


        function SalvaMorosita() {
            var sicuro = confirm('Attenzione...Confermi di voler emettere i MAV?');
            if (sicuro == true) {
                document.getElementById('txtAnnullo').value = '1';
            }
            else {
                document.getElementById('txtAnnullo').value = '0';
            }
        }



        //function StampaMorosita() {
        //    var sicuro = confirm('Attenzione...Confermi di voler Ristampare i MAV?');
        //    if (sicuro == true) {
        //    document.getElementById('txtAnnullo').value='1';
        //    }
        //    else
        //    {
        //    document.getElementById('txtAnnullo').value='0'; 
        //    }
        //}



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

   
   
    </script>
    <script type="text/javascript">
        function Somma(imp, imp2, imp3, obj) {

            var a = parseFloat(imp);
            if (a != 'NaN') {
                var b = parseFloat(document.getElementById('txtImportoSelezionato1').value.replace(/\./gi, '').replace(',', '.'));
                var risultato1;
                var risultato1 = parseFloat(document.getElementById('lblNumIntestatariSelezionati').value);
                var risultato;
                if (obj.checked == true) {
                    risultato = parseFloat(a + b).toFixed(2);
                    risultato1 = parseFloat(risultato1) + 1;
                }
                else {
                    risultato = parseFloat(b - a).toFixed(2);
                    risultato1 = parseFloat(risultato1) - 1;
                }
                if (risultato.substring(risultato.length - 3, 0).length >= 4) {
                    var decimali = risultato.substring(risultato.length, risultato.length - 2);
                    var dascrivere = risultato.substring(risultato.length - 3, 0);
                    var risultNew = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultNew = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultNew;
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                    }
                    risultNew = dascrivere + risultNew + ',' + decimali;
                }
                else {
                    risultNew = risultato.replace('.', ',');
                }
                document.getElementById('txtImportoSelezionato1').value = risultNew;
                document.getElementById('lblNumIntestatariSelezionati').value = risultato1;
            }
            var a = parseFloat(imp2);
            if (a != 'NaN') {
                var b = parseFloat(document.getElementById('txtImportoSelezionato2').value.replace(/\./gi, '').replace(',', '.'));
                var risultato;
                if (obj.checked == true) {
                    risultato = parseFloat(a + b).toFixed(2);
                }
                else {
                    risultato = parseFloat(b - a).toFixed(2);
                }
                if (risultato.substring(risultato.length - 3, 0).length >= 4) {
                    var decimali = risultato.substring(risultato.length, risultato.length - 2);
                    var dascrivere = risultato.substring(risultato.length - 3, 0);
                    var risultNew = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultNew = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultNew;
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                    }
                    risultNew = dascrivere + risultNew + ',' + decimali;
                }
                else {
                    risultNew = risultato.replace('.', ',');
                }
                document.getElementById('txtImportoSelezionato2').value = risultNew;
            }
            var a = parseFloat(imp3);
            if (a != 'NaN') {
                var b = parseFloat(document.getElementById('txtImportoSelezionato3').value.replace(/\./gi, '').replace(',', '.'));
                var risultato;
                if (obj.checked == true) {
                    risultato = parseFloat(a + b).toFixed(2);
                }
                else {
                    risultato = parseFloat(b - a).toFixed(2);
                }
                if (risultato.substring(risultato.length - 3, 0).length >= 4) {
                    var decimali = risultato.substring(risultato.length, risultato.length - 2);
                    var dascrivere = risultato.substring(risultato.length - 3, 0);
                    var risultNew = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultNew = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultNew;
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                    }
                    risultNew = dascrivere + risultNew + ',' + decimali;
                }
                else {
                    risultNew = risultato.replace('.', ',');
                }
                document.getElementById('txtImportoSelezionato3').value = risultNew;
            }
        }
    </script>
</head>
<body>
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <br />
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px; height: 1px;" id="TD_Principale">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"></span>
                    <br />
                    <table style="width: 760px" id="TABLE1">
                        <tr>
                            <td style="width: 20px">
                                <asp:ImageButton ID="btnINDIETRO" runat="server" ImageUrl="../NuoveImm/Img_Indietro.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 100;
                                    left: 16px; position: static; top: 29px" TabIndex="-1" ToolTip="Indietro" />
                            </td>
                            <td style="width: 76px">
                                <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="../NuoveImm/Img_Salva.png"
                                    OnClientClick="document.getElementById('USCITA').value='1'; SalvaMorosita();"
                                    Style="z-index: 100; left: 584px; position: static; top: 32px" TabIndex="-1"
                                    ToolTip="Salva" />
                            </td>
                            <td style="width: 76px">
                                <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="Immagini/VisualizzaFile.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';" Style="z-index: 100;
                                    left: 584px; position: static; top: 32px" TabIndex="-1" ToolTip="Visualizza il file ZIP (MAV, lettere e PosteAler)" />
                            </td>
                            <td style="width: 76px">
                                &nbsp;
                            </td>
                            <td style="width: 76px">
                                <asp:ImageButton ID="imgExcel" runat="server" ImageUrl="Immagini/Img_ElencoCF.png"
                                    OnClientClick="document.getElementById('USCITA').value='1'; " Style="z-index: 100;
                                    left: 584px; position: static; top: 32px" TabIndex="-1" ToolTip="Visualizza un file Excel con l'elenco degli inquilini aventi C.F. errato."
                                    Visible="False" />
                            </td>
                            <td style="width: 76px">
                                &nbsp;
                            </td>
                            <td style="width: 76px">
                                <asp:ImageButton ID="imgInquinili" runat="server" ImageUrl="Immagini/Img_Elenco_gia_morosi.png"
                                    OnClientClick="document.getElementById('USCITA').value='1'; " Style="z-index: 100;
                                    left: 584px; position: static; top: 32px" TabIndex="-1" ToolTip="Visualizza un file Excel con l'elenco degli inquilini che hanno già ricevuto una lettera di morosità"
                                    Visible="False" />
                            </td>
                            <td style="width: 76px">
                            </td>
                            <td style="width: 76px">
                            </td>
                            <td style="width: 76px">
                            </td>
                            <td style="width: 76px">
                                <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="../NuoveImm/Img_Esci.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 125;
                                    left: 600px; position: static; top: 29px" TabIndex="-1" ToolTip="Esci" />
                            </td>
                        </tr>
                    </table>
                    &nbsp;
                    <br />
                    &nbsp;<asp:Label ID="lblElencoInquilini" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="8pt" ForeColor="Black" Style="z-index: 106; left: 24px; top: 368px"
                        Width="200px">RISULTATO RICERCA INTESTATARI : </asp:Label>&nbsp;
                    <asp:Label ID="lblNumIntestatari" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="10pt" ForeColor="Maroon" Style="z-index: 106; left: 24px; top: 368px"
                        Width="136px">0</asp:Label>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 106; left: 24px; top: 368px" Width="200px">INTESTATARI SELEZIONATI:</asp:Label>&nbsp;
                    <asp:TextBox ID="lblNumIntestatariSelezionati" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="10pt" ForeColor="Maroon" Style="z-index: 106; left: 24px; top: 368px;
                        border: 0 none transparent; background-color: transparent;" Width="136px" />
                    <br />
                    <table style="width: 368px">
                        <tr>
                            <td>
                                <div style="overflow: auto; width: 760px; height: 200px;">
                                    <asp:DataGrid runat="server" ID="DataGrid1" AutoGenerateColumns="False" CellPadding="1"
                                        BorderWidth="1px" BorderColor="#507CD1" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                        GridLines="None" CellSpacing="1" Width="1100px" PageSize="300">
                                        <AlternatingItemStyle BackColor="#F9F9F9" ForeColor="#000000" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="CODICE" Visible="False">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="CODICE">
                                                <ItemTemplate>
                                                    &nbsp;<asp:CheckBox ID="CheckBox1" runat="server" />
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>&nbsp;
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DEBITO2" HeaderText="DEBITO TOTALE">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DEBITO_MG" HeaderText="DEBITO M.G.">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DEBITO_MA" HeaderText="DEBITO M.A.">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="COD_TIPOLOGIA_CONTR_LOC" HeaderText="TIPO">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="POSIZIONE_CONTRATTO" HeaderText="POSIZIONE">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD.UNITA">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="COD_TIPOLOGIA" HeaderText="TIPO UN.">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="CIVICO" HeaderText="CIV.">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="COMUNE_UNITA" HeaderText="COMUNE">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ALLEGATI_CONTRATTO" HeaderText="ALLEGATI">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                        </Columns>
                                        <EditItemStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                                            Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                                            HorizontalAlign="Center" />
                                        <ItemStyle BackColor="#F9F9F9" ForeColor="#000000" />
                                        <PagerStyle BackColor="White" ForeColor="#507CD1" HorizontalAlign="Left" Mode="NumericPages"
                                            Position="Top" />
                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                                    </asp:DataGrid></div>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" style="font-family: Arial; font-size: 9pt;
                                    color: Maroon" width="380px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" Text="Debito" runat="server" Font-Bold="True" />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label3" Text="Totale" runat="server" Font-Bold="True" />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label4" Text="M.G." runat="server" Font-Bold="True" />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label5" Text="M.A." runat="server" Font-Bold="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label Text="Totale" runat="server" Font-Bold="True" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtImporto1" runat="server" Style="text-align: right; border: 0 none transparent;
                                                color: Maroon; background-color: transparent;" Width="90px" Font-Bold="True"
                                                Font-Names="Arial" Font-Size="9pt" ForeColor="Maroon" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtImporto2" runat="server" Style="text-align: right; border: 0 none transparent;
                                                color: Maroon; background-color: transparent;" Width="90px" Font-Names="Arial"
                                                Font-Size="9pt" ForeColor="Maroon" Font-Bold="True" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtImporto3" runat="server" Style="text-align: right; border: 0 none transparent;
                                                color: Maroon; background-color: transparent;" Width="90px" Font-Names="Arial"
                                                Font-Size="9pt" ForeColor="Maroon" Font-Bold="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" Text="Totale Selezione" runat="server" Font-Bold="True" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtImportoSelezionato1" runat="server" Style="text-align: right;
                                                color: Maroon; border: 0 none transparent; background-color: transparent;" Width="90px"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="Maroon" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtImportoSelezionato2" runat="server" Style="text-align: right;
                                                color: Maroon; border: 0 none transparent; background-color: transparent;" Width="90px"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="Maroon" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtImportoSelezionato3" runat="server" Style="text-align: right;
                                                color: Maroon; border: 0 none transparent; background-color: transparent;" Width="90px"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="Maroon" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 760px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblStato" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="100px">Tipo Invio:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbTipoInvio" runat="server" BackColor="White" Font-Names="arial"
                                                Font-Size="9pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                                top: 56px" Width="200px">
                                            </asp:DropDownList>
                                            &nbsp; &nbsp; &nbsp;<asp:Label ID="lblODL" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px"
                                                TabIndex="-1" Width="130px">RIFERIMENTO DAL:</asp:Label>
                                            <asp:Label ID="lbl_RIF_DA" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
                                                Width="100px"></asp:Label>
                                            <asp:Label ID="lbl_1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
                                                Width="30px">al</asp:Label>
                                            <asp:Label ID="lbl_RIF_A" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
                                                Width="70px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblProtocollo" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                                TabIndex="-1" Width="100px">Protocollo Gestore: *</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtProFis" runat="server" MaxLength="50" Style="z-index: 10;
                                                left: 408px; top: 171px" Width="90px" TabIndex="1" ReadOnly="True"></asp:TextBox>
                                            &nbsp;<asp:TextBox ID="txtProtocollo" runat="server" MaxLength="50" Style="z-index: 10;
                                                left: 408px; top: 171px" Width="150px" TabIndex="1"></asp:TextBox>
                                            &nbsp; &nbsp;
                                            <asp:Label runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                Style="z-index: 100; left: 24px; top: 32px" TabIndex="-1" Width="130px" ID="lblDataProtocollo">Data Protocollo Gestore: *</asp:Label>
                                            <asp:TextBox ID="txtDataProtocolloAler" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="10" Style="left: 144px; top: 192px" TabIndex="3" ToolTip="gg/mm/aaaa"
                                                Width="70px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                    runat="server" ControlToValidate="txtDataProtocolloAler" Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)"
                                                    Font-Bold="False" Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="192px"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDataScad" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                                TabIndex="-1" Width="100px">Data Scadenza*</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDataScad" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="10" Style="left: 144px; top: 192px" TabIndex="3" ToolTip="gg/mm/aaaa"
                                                Width="70px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                    runat="server" ControlToValidate="txtDataScad" Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)"
                                                    Font-Bold="False" Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="192px"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNote" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="100px">Note:</asp:Label><br />
                                            <br />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="9pt" Height="60px"
                                                MaxLength="500" Style="left: 80px; top: 88px" TabIndex="2" TextMode="MultiLine"
                                                Width="650px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 104; left: 17px; width: 858px; top: 588px; height: 30px"
                        Visible="False" TabIndex="-1"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:TextBox ID="USCITA" runat="server" Style="left: 0px; position: absolute; top: 200px;
            z-index: -1;" TabIndex="-1">0</asp:TextBox>
        <asp:TextBox ID="txtModificato" runat="server" BackColor="White" BorderStyle="None"
            ForeColor="White" Style="left: 0px; position: absolute; top: 200px; z-index: -1;">0</asp:TextBox>
        <asp:TextBox ID="txtindietro" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="z-index: -1; left: 0px; position: absolute;
            top: 200px" TabIndex="-1" Width="48px">0</asp:TextBox>
        <asp:TextBox ID="txtConnessione" runat="server" Style="left: 0px; position: absolute;
            top: 200px; z-index: -1;" TabIndex="-1"></asp:TextBox>
        <asp:TextBox ID="SOLO_LETTURA" runat="server" Style="z-index: -1; left: 0px; position: absolute;
            top: 415px" TabIndex="-1" Width="24px">0</asp:TextBox>
        <asp:HiddenField ID="txtAnnullo" runat="server" />
        <br />
    </div>
    </form>
    <script type="text/javascript">
        window.focus();
        self.focus();



    </script>
</body>
</html>
