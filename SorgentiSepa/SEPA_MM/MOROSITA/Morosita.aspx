<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Morosita.aspx.vb" Inherits="MOROSITA_Morosita" %>

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
    <title>MODULO GESTIONE MOROSITA</title>
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
                    event.returnValue = "Attenzione...Uscire dalla scheda morosità premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
                else {
                    return "Attenzione...Uscire dalla scheda morosità premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
            }
        }


        function StampaMorosita() {
            var sicuro = confirm('Attenzione...Confermi di voler Generare i MAV mancanti?');
            if (sicuro == true) {
                document.getElementById('txtAnnullo').value = '1';
            }
            else {
                document.getElementById('txtAnnullo').value = '0';
            }
        }


        function AnnullaMorosita() {
            var sicuro = confirm('Attenzione...Confermi di voler annullare la morosità? L\'operazione è irreversibile.');
            if (sicuro == true) {
                document.getElementById('txtAnnullo').value = '1';
            }
            else {
                document.getElementById('txtAnnullo').value = '0';
            }
        }


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
    <style type="text/css">
        .style1
        {
            width: 10%;
            text-align: right;
        }
        .style2
        {
            text-align: left;
        }
        .style3
        {
            text-align: right;
        }
        .style4
        {
            text-align: center;
        }
    </style>
</head>
<body bgcolor="#f2f5f1" text="#ede0c0">
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
            <td style="height: 1px;" id="TD_Principale">
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"></span>
                <br />
                <table style="width: 100%" id="TABLE1">
                    <tr>
                        <td style="width: 15%" class="style2">
                            <asp:ImageButton ID="btnINDIETRO" runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
                                OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 100;
                                left: 16px; position: static; top: 29px" TabIndex="-1" ToolTip="Indietro" />
                        </td>
                        <td style="width: 5%" class="style4">
                            <asp:ImageButton ID="btnXLS" runat="server" ImageUrl="~/MOROSITA/Immagini/Esporta_XLS.png"
                                OnClientClick="document.getElementById('USCITA').value='1'; " Style="z-index: 100;
                                left: 584px; position: static; top: 32px" TabIndex="-1" 
                                ToolTip="Esporta l'elenco inquilini in formato Excel" />
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td style="width: 20%" class="style2">
                            <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="~/MOROSITA/Immagini/VisualizzaFile.png"
                                OnClientClick="document.getElementById('USCITA').value='1'; " Style="z-index: 100;
                                left: 584px; position: static; top: 32px" TabIndex="-1" ToolTip="Visualizza il file ZIP (MAV, lettere e PosteAler)" />
                        </td>
                        <td class="style1">
                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/MOROSITA/Immagini/Img_AnnullaMorosita.png"
                                OnClientClick="document.getElementById('USCITA').value='1'; AnnullaMorosita();"
                                Style="z-index: 100; left: 584px; position: static; top: 32px" TabIndex="-1"
                                ToolTip="Annulla la morosità" />
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td style="width: 20%" class="style2">
                            <asp:ImageButton ID="btnRigenera" runat="server" ImageUrl="~/MOROSITA/Immagini/GeneraMAV_Mancanti.png"
                                OnClientClick="document.getElementById('USCITA').value='1'; StampaMorosita()"
                                Style="z-index: 100; left: 584px; position: static; top: 32px" TabIndex="-1"
                                ToolTip="Genera i MAV mancanti" />
                        </td>
                        <td style="width: 10%" class="style3">
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td style="width: 5%" class="style4">
                            <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                                OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 125;
                                left: 600px; position: static; top: 29px" TabIndex="-1" ToolTip="Esci" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%" class="style2">
                            <asp:Label ID="lblNumIntestatari" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 106; left: 24px; top: 368px">ELENCO INQUILINI </asp:Label>
                        </td>
                        <td style="width: 5%" class="style4">
                            <asp:Label ID="lblNum1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="#FF3300" Style="z-index: 106; left: 24px; top: 368px"></asp:Label>
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td style="width: 20%" class="style2">
                            <asp:Label ID="lblTotaleMorosita" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 106; left: 24px; top: 368px">TOTALE MOROSITA':</asp:Label>
                        </td>
                        <td class="style1">
                            <asp:Label ID="lblTotMor" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Blue" Style="z-index: 106; left: 24px; top: 368px; text-align: right;"></asp:Label>
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td style="width: 20%" class="style2">
                            <asp:Label ID="lblTotaleMorosita0" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 106; left: 24px; top: 368px">TOTALE BOLLETTATO:</asp:Label>
                        </td>
                        <td style="width: 10%" class="style3">
                            <asp:Label ID="lblTotBol" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Blue" Style="z-index: 106; left: 24px; top: 368px; text-align: right;"></asp:Label>
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td style="width: 5%" class="style4">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <div style="overflow: auto; width: 760px; height: 280px; border-right: black 1px solid;
                                border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;">
                                <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black"
                                    PageSize="50" Style="z-index: 101; left: 9px; border-collapse: separate" BorderColor="#000099">
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Mode="NumericPages" Wrap="False" />
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" BackColor="Gainsboro" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" Width="0%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="CODICE" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="0%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="CODICE">
                                            <ItemTemplate>
                                                &nbsp;<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>&nbsp;
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" Width="5%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="INTESTATARIO">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" Width="20%" HorizontalAlign="Left" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="TIPO">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_TIPOLOGIA_CONTR_LOC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="MOROSITA">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MOROSITA") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="5%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="POSIZIONE">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POSIZIONE_CONTRATTO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Left" Width="5%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="COD.UNITA">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Left" Width="5%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="INDIRIZZO">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Left" Width="20%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="CIV.">
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Right" Width="2%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="COMUNE">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE_UNITA") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" Width="5%" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="TIPO UN.">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_TIPOLOGIA") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="TOT_MOROSITA" HeaderText="Tot. MOROSITA'">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Width="6%" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TOT_BOLLETTATO" HeaderText="Tot. BOLLETTATO">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Width="6%" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TOT_CANONI" HeaderText="Tot. CANONI">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Width="6%" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TOT_ONERI_SERVIZI" HeaderText="Tot. ONERI e SERVIZI">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Width="5%" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn></asp:BoundColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                        ForeColor="#0000C0" Wrap="False" />
                                </asp:DataGrid></div>
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
                                            top: 56px" TabIndex="2" Width="200px">
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
                                        <asp:TextBox ID="txtProtocollo" runat="server" MaxLength="50" Style="z-index: 10;
                                            left: 408px; top: 171px" Width="200px"></asp:TextBox>
                                        &nbsp; &nbsp;
                                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" TabIndex="-1" Width="130px">Data Protocollo Gestore: *</asp:Label>
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
                                        <asp:Label ID="lblNote" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="100px">Note:</asp:Label><br />
                                        <br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="9pt" Height="80px"
                                            MaxLength="500" Style="left: 80px; top: 88px" TabIndex="1" TextMode="MultiLine"
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
        </table>
        <br />
        <br />
        <asp:TextBox ID="USCITA" runat="server" Style="left: 0px; position: absolute; top: 200px;
            z-index: -1;" TabIndex="-1">0</asp:TextBox>
        <asp:TextBox ID="txtindietro" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="z-index: -1; left: 0px; position: absolute;
            top: 200px" TabIndex="-1" Width="48px">0</asp:TextBox>
        <asp:TextBox ID="txtConnessione" runat="server" Style="left: 0px; position: absolute;
            top: 200px; z-index: -1;" TabIndex="-1"></asp:TextBox>
        <asp:TextBox ID="txtModificato" runat="server" Style="left: 0px; position: absolute;
            top: 200px; z-index: -1;" TabIndex="-1"></asp:TextBox>
        <asp:HiddenField ID="txtAnnullo" runat="server" />
        <asp:HiddenField ID="txtVisualizza" runat="server" />
        <br />
    </div>
    </form>
    <script type="text/javascript">
        window.focus();
        self.focus();

        if (document.getElementById('txtVisualizza').value == '1') {
            //NON BLOCCATO
            document.getElementById('btnStampa').style.visibility = 'visible';
        }

        if (document.getElementById('txtVisualizza').value == '1') {
            //BLOCCATO SOLO LETTURA
            document.getElementById('btnStampa').style.visibility = 'hidden';
        }

    </script>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
