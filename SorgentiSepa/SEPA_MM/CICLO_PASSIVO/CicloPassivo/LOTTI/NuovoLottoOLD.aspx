<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovoLottoOLD.aspx.vb" Inherits="MANUTENZIONI_NuovoLottoOLD" %>

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

</script>
<script type="text/javascript">
    var Uscita;
    Uscita = 0;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="../../../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../../../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../../../Contratti/moo.fx.pack.js"></script>
<head id="Head1" runat="server">
    <title>Nuovi Lotti</title>
    <style type="text/css">
        .style1
        {
            width: 352px;
            height: 18px;
        }
        .style2
        {
            width: 27px;
            height: 18px;
        }
        .style3
        {
            width: 352px;
            height: 24px;
        }
        .style4
        {
            width: 27px;
            height: 24px;
        }
    </style>
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

        function ApriAccessoAnagrafica() {
            var win = null;
            LeftPosition = (screen.width) ? (screen.width - 620) / 2 : 0;
            TopPosition = (screen.height) ? (screen.height - 500) / 2 : 0;
            LeftPosition = LeftPosition - 20;
            TopPosition = TopPosition - 20;
            window.showModalDialog('Anagrafica/menu.htm', window, 'status:no;dialogTop=' + TopPosition + ';dialogLeft=' + LeftPosition + ';dialogWidth:620px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
        }

        function ControllaPIVA(pi) {
            risultato = '0';
            if (pi == '') {
                document.getElementById('PIVA').value = '0';
                return '';
            }
            if (pi.length != 11) {
                alert("La lunghezza della partita IVA non è\n" +
			"corretta: la partita IVA dovrebbe essere lunga\n" +
			"esattamente 11 caratteri.\n");
                document.getElementById('PIVA').value = '1';
                return "1";
            }
            validi = "0123456789";
            for (i = 0; i < 11; i++) {
                if (validi.indexOf(pi.charAt(i)) == -1) {
                    alert("La partita IVA contiene un carattere non valido `" +
				pi.charAt(i) + "'.\nI caratteri validi sono le cifre.\n");
                    document.getElementById('PIVA').value = '1';
                    return "1";
                }
            }
            s = 0;
            for (i = 0; i <= 9; i += 2)
                s += pi.charCodeAt(i) - '0'.charCodeAt(0);
            for (i = 1; i <= 9; i += 2) {
                c = 2 * (pi.charCodeAt(i) - '0'.charCodeAt(0));
                if (c > 9) c = c - 9;
                s += c;
            }
            if ((10 - s % 10) % 10 != pi.charCodeAt(10) - '0'.charCodeAt(0)) {
                alert("La partita IVA non è valida:\n" +
			"il codice di controllo non corrisponde.\n");
                document.getElementById('PIVA').value = '1';
                return '1';
            }
        }

        function VerPIVA() {

            document.getElementById('PIVA').value = '0';

            if (document.getElementById('txtPIva').value != '') {
                ControllaPIVA(document.getElementById('txtPIva').value);
            }

        }

        function ConfermaEsci() {
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                    //document.getElementById('USCITA').value='0';
                }
            }
        }

        function ConfermaAnnullo() {
            if (document.getElementById('txtIdComponente').value == "") {
                alert('Attenzione...Non hai selezionato alcuna riga!')
            }
            else {
                var sicuro = confirm('Sei sicuro di voler cancellare questo elemento?');
                if (sicuro == true) {
                    document.getElementById('txtannullo').value = '1';
                }
                else {
                    document.getElementById('txtannullo').value = '0';
                }
            }
        }
    
    </script>
    <form id="form1" runat="server" defaultbutton="ImgProcedi" defaultfocus="txtCognome">
    <div>
        <table >
            <tr>
                <td style="height: 556px; width: 796px;">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial">&nbsp;
                        <asp:Label ID="lbltitolo" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="14pt" Text="Inserisci nuovi lotti" Style="left: 12px; position: absolute;
                        top: 20px"></asp:Label>
                    </span>
                    <br />
                    <br />
                    <br />
                    <a href="../../../cf/codice.htm" target="_blank"></a>
                    <br />
                    &nbsp;
                    <asp:DropDownList ID="cmbfiliale" runat="server" AutoPostBack="True" BackColor="White"
                        Font-Names="arial" Font-Size="10pt" Height="20px" Style="border: 1px solid black;
                        z-index: 10; left: 17px; position: absolute; top: 127px" Width="702px" TabIndex="2">
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 16px; position: absolute; top: 154px">Lista servizi*</asp:Label>
                    <br />
                    &nbsp;
                    <asp:DropDownList ID="cmbtipo" runat="server" BackColor="White" Font-Names="arial"
                        Font-Size="10pt" Height="20px" Style="border: 1px solid black; z-index: 10; left: 712px;
                        position: absolute; top: 276px" Width="50px" TabIndex="4" Visible="False">
                        <asp:ListItem Value="M">MANUTENZIONI</asp:ListItem>
                        <asp:ListItem Value="S">SERVIZI A RIMBORSO</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;<br />
                    <div style="border-color: #ccccff; border-style: solid; position: absolute; top: 168px;
                        left: 14px; width: 699px; overflow: auto; right: 246px; height: 110px;">
                        <asp:CheckBoxList ID="lstservizi" runat="server" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" TabIndex="3" AutoPostBack="True">
                        </asp:CheckBoxList>
                        <asp:Label ID="lblnoservizi" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Style="z-index: 100; left: 14px; top: 73px" Visible="False" Width="97px">Nessun Risultato</asp:Label>&nbsp;
                    </div>
                    &nbsp;&nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <div style="border-color: #ccccff; border-style: solid; position: absolute; top: 395px;
                        left: 14px; width: 699px; overflow: auto; right: 246px; height: 111px;">
                        <asp:DataGrid ID="tabcomplessi" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Style="z-index: 101;
                            left: 3px; top: 65px" Width="100%" GridLines="None" AllowSorting="True" Visible="False">
                            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE"></asp:BoundColumn>
                            </Columns>
                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Mode="NumericPages" Wrap="False" Visible="False" />
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </asp:DataGrid>
                    </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtdescrizione" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="50" Style="z-index: 107; left: 18px; position: absolute; top: 313px;
                        right: 291px;" TabIndex="5" TextMode="MultiLine" Font-Names="Arial" Font-Size="8pt"
                        Height="35px" Width="700px"></asp:TextBox>
                    <br />
                    <br />
                    <div id="RicercaEdifici" style="z-index: 200; left: 816px; width: 466px; position: absolute;
                        top: 160px; height: 207px; background-color: transparent; visibility: hidden;">
                        <div style="width: 180px; height: 68px; background-color: silver">
                            <table style="width: 461px; height: 185px; background-color: silver">
                                <tr>
                                    <td style="vertical-align: top; text-align: left; height: 18px;" class="style1">
                                        <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="238px">Inserire complessi per edifici</asp:Label>
                                    </td>
                                    <td style="vertical-align: baseline; text-align: left; height: 18px;" class="style2">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: left" class="style3">
                                        <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
                                            Font-Names="arial" Font-Size="10pt" Style="border: 1px solid black; z-index: 10;
                                            left: 2px; position: absolute; top: 28px; height: 20px; width: 420px;" ToolTip="Seleziona complesso per edificio">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="vertical-align: baseline; text-align: left" class="style4">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 352px; height: 104px; text-align: left">
                                        <asp:Label ID="LblNoResulted" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="Red" Style="z-index: 100; left: 14px; top: 73px" Visible="False"
                                            Width="97px">Nessun Risultato</asp:Label><br />
                                        <div style="left: 5px; overflow: auto; width: 418px; top: 87px; height: 101px">
                                            <asp:CheckBoxList ID="lstedifici" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black" Width="240px">
                                            </asp:CheckBoxList>
                                        </div>
                                    </td>
                                    <td style="vertical-align: baseline; width: 27px; height: 104px; text-align: left">
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <asp:ImageButton ID="BtnConfermaedificio" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/Next_24x24.png"
                                            Style="z-index: 111; left: 268px; top: 190px" ToolTip="Conferma" OnClientClick="myOpacityed.toggle();" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div id="RicercaComplessi" style="z-index: 200; left: 856px; width: 781px; position: absolute;
                        top: 448px; height: 500px; border-top-width: thin; border-left-width: thin; border-left-color: #6699ff;
                        border-bottom-width: thin; border-bottom-color: #6699ff; border-top-color: #6699ff;
                        border-right-width: thin; border-right-color: #6699ff; background-image: url(../../../NuoveImm/SfondoMascheraContratti.jpg);
                        background-position: center; background-color: #dedede; visibility: hidden;">
                        &nbsp;
                        <asp:Image ID="ImgSfondoSchema" runat="server" BackColor="White" Height="497px" ImageUrl="~/ImmDiv/DivMGrande.png"
                            Style="z-index: 101; left: -4px; background-image: url(../../../ImmDiv/DivMGrande.png);
                            position: absolute; top: 0px" Width="787px" />
                        <table style="border-color: #6699ff; border-width: thin; width: 699px; height: 412px;
                            margin-right: 0px; z-index: 105; left: 32px; position: absolute; top: 35px;">
                            <tr>
                                <td style="vertical-align: top; text-align: left; height: 18px;" class="style1">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="238px">COMPLESSI IMMOBILIARI</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left" class="style3">
                                    &nbsp;<asp:Label ID="LblNoResult" runat="server" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Red" Style="z-index: 100; left: 14px; top: 73px" Visible="False"
                                        Width="234px">Nessun complesso disponibile</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 352px; height: 104px; text-align: left">
                                    <div style="border: 2px solid #990000; left: 5px; overflow: auto; width: 674px; top: 87px;
                                        height: 311px">
                                        <asp:CheckBoxList ID="lstcomplessi" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Width="500px">
                                        </asp:CheckBoxList>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right; height: 40px;" align="right">
                                    <br />
                                    <asp:ImageButton ID="BtnConfermaComplesso" runat="server" CausesValidation="False"
                                        ImageUrl="~/CICLO_PASSIVO/CicloPassivo/LOTTI/Immagini/Aggiungi.png" OnClientClick="myOpacitycom.toggle();"
                                        Style="z-index: 103; left: 744px; cursor: pointer; top: 26px" ToolTip="Aggiorna ed esci" />
                                    &nbsp;<asp:ImageButton ID="btnesci" runat="server" CausesValidation="False" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/LOTTI/Immagini/Img_Esci.png"
                                        OnClientClick="myOpacitycom.toggle();" Style="z-index: 103; left: 744px; cursor: pointer;
                                        top: 26px" ToolTip="Aggiorna ed esci" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Image ID="Img2" alt="Aiuto Ricerca per Edifici" onclick="document.getElementById('txtedifici').value!='1';myOpacityed.toggle();"
                        runat="server" ImageUrl="~/Condomini/Immagini/40px-Crystal_Clear_action_edit_add.png"
                        ToolTip="Aggiungi un edificio" Style="position: absolute; top: 544px; left: 7px;
                        visibility: hidden;" Visible="False" />
                    <asp:Image ID="Img1" alt="Aiuto Ricerca per Complessi" onclick="document.getElementById('txtcomplessi').value!='1';myOpacitycom.toggle();"
                        runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/LOTTI/Immagini/40px-Crystal_Clear_action_edit_add.png"
                        ToolTip="Aggiungi un complesso" Style="position: absolute; top: 396px; left: 727px;
                        bottom: 239px; cursor: pointer;" />
                    <asp:ImageButton ID="btnEliminaEdificio" runat="server" ImageUrl="~/Condomini/Immagini/minus_icon.png"
                        Style="z-index: 102; left: 48px; top: 544px; position: absolute; visibility: hidden;"
                        ToolTip="Elimina Elemento Selezionato" Visible="False" />
                    <asp:ImageButton ID="btnEliminaComplesso" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/LOTTI/Immagini/minus_icon.png"
                        Style="z-index: 102; left: 727px; top: 422px; cursor: pointer; position: absolute"
                        ToolTip="Elimina Elemento Selezionato" />
                    <br />
                    &nbsp; &nbsp; &nbsp;
                    <div style="border-color: #ccccff; border-style: solid; position: absolute; top: 8px;
                        left: 243px; width: 538px; overflow: auto; right: 239px; height: 91px; visibility: hidden;">
                        <asp:DataGrid ID="tabedifici" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Style="z-index: 101;
                            left: 3px; top: 65px" Width="100%" GridLines="None" Visible="False">
                            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="EDIFICI" HeaderText="DENOMINAZIONE"></asp:BoundColumn>
                            </Columns>
                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Mode="NumericPages" Wrap="False" />
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </asp:DataGrid>
                    </div>
                    &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;<br />
                    <asp:DropDownList ID="cmbesercizio" runat="server" BackColor="White" Font-Names="arial"
                        Font-Size="10pt" Height="20px" Style="border: 1px solid black; z-index: 10; left: 17px;
                        position: absolute; top: 87px" Width="702px" TabIndex="1" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;
                    <br />
                    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 16px; position: absolute; top: 73px">Esercizio Finanziario*</asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                        Style="left: 577px; position: absolute; top: 522px; right: 222px;" TabIndex="19" />
                    <br />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 101;
                        left: 650px; position: absolute; top: 522px; height: 20px;" ToolTip="Home" TabIndex="20"
                        CausesValidation="False" />
                    <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 17px; position: absolute; top: 364px; font-weight: 700;">COMPOSIZIONI</asp:Label>
                    <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 17px; position: absolute; top: 54px; font-weight: 700;">LOTTI SERVIZI</asp:Label>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 104; left: 7px; position: absolute; top: 537px;
                        height: 16px;" Visible="False" Width="506px"></asp:Label>
                    &nbsp;
                    <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 17px; position: absolute; top: 380px">Lista complessi*</asp:Label>
                    <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 18px; position: absolute; top: 297px">Descrizione*</asp:Label>
                    &nbsp;&nbsp;
                    <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 713px; position: absolute; top: 257px" Visible="False">Tipo lotto</asp:Label>
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 18px; position: absolute; top: 113px">Sede Territoriale*</asp:Label>
                    <asp:TextBox ID="txtseledifici" runat="server" BackColor="#F2F5F1" BorderColor="White"
                        BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                        ReadOnly="True" Style="left: 14px; top: 516px; position: absolute; width: 538px;"
                        TabIndex="-1" Visible="False"></asp:TextBox>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <asp:TextBox ID="USCITA" runat="server" Style="left: 0px; position: absolute; top: 200px;
        z-index: 100;" TabIndex="-1">0</asp:TextBox>
    <asp:TextBox ID="txtModificato" runat="server" BackColor="White" BorderStyle="None"
        ForeColor="White" Style="left: 0px; position: absolute; top: 200px; z-index: 101;"
        TabIndex="3">0</asp:TextBox>
    <asp:TextBox ID="txtcomplessi" runat="server" Style="left: 0px; position: absolute;
        top: 200px; z-index: 102;" TabIndex="-1"></asp:TextBox>
    <asp:TextBox ID="txtedifici" runat="server" Style="left: 0px; position: absolute;
        top: 200px; z-index: 103;" TabIndex="-1" Font-Underline="True"></asp:TextBox>
    <asp:TextBox ID="txtIdlotti" runat="server" Style="left: 0px; position: absolute;
        top: 200px; z-index: 104;" TabIndex="-1" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtIdComponente" runat="server" Style="left: 640px; position: absolute;
        top: 440px; z-index: 99;" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
    <asp:TextBox ID="txtannullo" runat="server" Style="left: 652px; position: absolute;
        top: 535px; z-index: 106;" TabIndex="-1" Width="1px" Height="1px"></asp:TextBox>
    <asp:TextBox ID="txtConnessione" runat="server" Style="left: 0px; position: absolute;
        top: 200px; z-index: 107;" TabIndex="-1"></asp:TextBox>
    <asp:TextBox ID="SOLO_LETTURA" runat="server" Style="z-index: 108; left: 0px; position: absolute;
        top: 415px" TabIndex="-1" Width="24px">0</asp:TextBox>
    <asp:HiddenField ID="tipostruttura" runat="server" />
    <script type="text/javascript">

        myOpacitycom = new fx.Opacity('RicercaComplessi', { duration: 200 });
        //myOpacity.hide();

        if (document.getElementById('txtcomplessi').value != '2') {
            myOpacitycom.hide(); ;
        }
        myOpacityed = new fx.Opacity('RicercaEdifici', { duration: 200 });
        //myOpacity.hide();

        if (document.getElementById('txtedifici').value != '2') {
            myOpacityed.hide(); ;
        }
                                
    </script>
    <script type="text/javascript">
        window.focus();
        self.focus();
    </script>
    <p>
        &nbsp; &nbsp;&nbsp;</p>
    </form>
</body>
</html>
