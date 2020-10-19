<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AbbinaDomanda.aspx.vb" Inherits="ASS_AbbinaDomanda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 0;
    var popupWindow;

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Abbinamento Domanda</title>
    <style type="text/css">
        #contenitore
        {
            top: 117px;
            left: 8px;
        }
        #contenitore1
        {
            top: 259px;
            left: 8px;
        }
    </style>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server" defaultfocus="imgPreferenze" defaultbutton="Button1">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
            left: 9px; position: absolute; top: 7px" Text="PG:"></asp:Label>
        <asp:Label ID="lblPG" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 101; left: 80px; position: absolute; top: 8px"
            Text="0000000000" Width="120px"></asp:Label>
        <asp:ImageButton ID="btnEsci" runat="server" Style="z-index: 102; left: 603px; position: absolute;
            top: 511px" Text="Uscita" TabIndex="6" ImageUrl="~/NuoveImm/Img_EsciCorto.png" />
        <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 103;
            left: 201px; position: absolute; top: 8px" Text="Nominativo"></asp:Label>
        <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 104;
            left: 9px; position: absolute; top: 28px" Text="ISBARC/R"></asp:Label>
        <asp:Label ID="lblNominativo" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="10pt" ForeColor="#0000C0" Style="z-index: 105; left: 272px; position: absolute;
            top: 8px; width: 239px;" Text="Label"></asp:Label>
        <asp:Label ID="lblIsbarcr" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 106; left: 80px; position: absolute; top: 28px"
            Text="Label" Width="104px"></asp:Label>
        <asp:Label ID="Label14" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 107;
            left: 379px; position: absolute; top: 28px" Text="Tipo Alloggio" Visible="False"></asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 107;
            left: 201px; position: absolute; top: 28px" Text="ISEE"></asp:Label>
        <asp:Label ID="lblISEE" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 108; left: 273px; position: absolute; top: 28px"
            Text="Label" Width="104px"></asp:Label>
        <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 109;
            left: 10px; position: absolute; top: 48px" Text="N. Comp."></asp:Label>
        <asp:Label ID="LBLmotorio" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
            ForeColor="#C00000" Style="z-index: 110; left: 378px; position: absolute; top: 71px;
            text-align: center; width: 307px;" Text="COMPONENTE PORTATORE DI HANDICAP MOTORIO"></asp:Label>
        <asp:Label ID="lblComp" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 111; left: 80px; position: absolute; top: 48px"
            Text="Label" Width="104px"></asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 112;
            left: 357px; position: absolute; top: 48px" Text="N. Invalidi (66%-100%)" Width="130px"></asp:Label>
        <asp:Label ID="lblInvalidi" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 113; left: 493px; position: absolute; top: 48px"
            Text="Label" Width="33px"></asp:Label>
        <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 114;
            left: 202px; position: absolute; top: 48px" Text="N. Anziani (>=65)" Width="108px"></asp:Label>
        <asp:Label ID="lblTipoAlloggio" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="10pt" ForeColor="#0000C0" Style="z-index: 115; left: 458px; position: absolute;
            top: 28px" Text="Label" Width="41px" Visible="False"></asp:Label>
        <asp:Label ID="lblAnziani" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 115; left: 309px; position: absolute; top: 48px"
            Text="Label" Width="41px"></asp:Label>
        <asp:Button ID="Button1" runat="server" Style="z-index: 117; left: 417px; position: absolute;
            top: 508px; width: 157px;" Text="Abbina e Memorizza" BackColor="Red" Font-Bold="True"
            ForeColor="White" TabIndex="5" />
        <asp:Image ID="btnReport" runat="server" Style="cursor: pointer; z-index: 118; left: 272px;
            position: absolute; top: 512px" Text="Visualizza Report" Visible="False" Width="135px"
            TabIndex="4" ImageUrl="~/NuoveImm/Img_VisualizzaReport.png" />
        &nbsp;&nbsp;
        <asp:Image ID="imgPreferenze" runat="server" Style="cursor: pointer; z-index: 132;
            left: 3px; position: absolute; top: 513px" TabIndex="1" ImageUrl="~/NuoveImm/Img_VisPreferenze.png" />
        &nbsp;
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            ForeColor="#0000C0" Style="z-index: 124; left: 3px; position: absolute; top: 484px;
            text-align: left; right: 481px;" Text="Nessuna Unità Abbinata" 
            Width="648px" BackColor="#FFFFC0"
            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            Style="z-index: 126; left: 12px; position: absolute; top: 119px" 
            Text="UNITA' ABBINATA"></asp:Label>
            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            Style="z-index: 126; left: 12px; position: absolute; top: 157px" 
            Text="CODICE:"></asp:Label>
            <asp:Label ID="lblCodice" runat="server" Font-Bold="True" 
            Font-Names="arial" Font-Size="10pt"
            Style="z-index: 126; left: 92px; position: absolute; top: 157px" 
            Text="---"></asp:Label>
            <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            Style="z-index: 126; left: 12px; position: absolute; top: 188px" 
            Text="INDIRIZZO:"></asp:Label>
            <asp:Label ID="lblIndirizzo" runat="server" Font-Bold="True" 
            Font-Names="arial" Font-Size="10pt"
            Style="z-index: 126; left: 92px; position: absolute; top: 188px" 
            Text="---"></asp:Label>
            <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            Style="z-index: 126; left: 12px; position: absolute; top: 219px" 
            Text="PIANO:"></asp:Label>
            <asp:Label ID="lblPiano" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            Style="z-index: 126; left: 92px; position: absolute; top: 219px" 
            Text="---"></asp:Label>
            <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            Style="z-index: 126; left: 12px; position: absolute; top: 250px" 
            Text="SCALA:"></asp:Label>
            <asp:Label ID="lblScala" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            Style="z-index: 126; left: 92px; position: absolute; top: 250px" 
            Text="---"></asp:Label>

        &nbsp;
        <asp:Panel ID="Panel1" runat="server" BackColor="#C00000" Height="134px" Style="z-index: 129;
            left: 674px; position: absolute; top: 214px" Visible="False" Width="359px">
            <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="True" ForeColor="Yellow"
                Style="z-index: 100; left: 0px; position: absolute; top: 0px">Chiudi</asp:LinkButton>
            <asp:Label ID="Label11" runat="server" ForeColor="White" Height="77px" Style="z-index: 101;
                left: 1px; position: absolute; top: 48px" Visible="False" Width="351px"></asp:Label>
            <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="White" Style="z-index: 103;
                left: 3px; position: absolute; top: 26px" Text="ATTENZIONE:"></asp:Label>
        </asp:Panel>
        <asp:TextBox ID="txtData" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 130;
            left: 565px; position: absolute; top: 484px; width: 86px;" MaxLength="10" TabIndex="2"
            ToolTip="Formato dd/mm/aaaa">dd/mm/yyyy</asp:TextBox>
        <asp:Label ID="Label13" runat="server" Style="z-index: 131; left: 422px; position: absolute;
            top: 485px" Text="Data Scadenza Offerta"></asp:Label>
    </div>
    <p>
        &nbsp;</p>
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

        // popupWindow.focus();
    </script>
    <p>
        <asp:Label ID="lblVisEstremi" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="10pt" ForeColor="#0000C0" Style="cursor: pointer; z-index: 101; left: 248px;
            position: absolute; top: 70px; width: 102px;" Text="Estremi Docum."></asp:Label>
        <asp:Label ID="lblVisDomanda" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="10pt" ForeColor="#0000C0" Style="cursor: pointer; z-index: 101; left: 143px;
            position: absolute; top: 70px; width: 92px;" Text="Vis. Domanda"></asp:Label>
        <asp:Label ID="lblVisDichiarazione" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="10pt" ForeColor="#0000C0" Style="cursor: pointer; z-index: 101; left: 12px;
            position: absolute; top: 70px; width: 117px; right: 1003px;" Text="Vis. Dichiarazione"></asp:Label>
    </p>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="lblDataPg" runat="server" />
    <asp:HiddenField ID="LBLID" runat="server" />
    <asp:HiddenField ID="lblTipologia" runat="server" />
    <asp:ImageButton ID="imgSeleziona" runat="server" 
        ImageUrl="~/NuoveImm/search-icon.png" 
        style="position:absolute; top: 117px; left: 148px;" 
        onclientclick="ApriElenco();" 
        ToolTip="Visualizza l'elenco delle unità disponibili per l'abbinamento"/>
    </form>
    <script type="text/javascript">
        function ApriElenco() {
            window.showModalDialog('RicercaUIDaAbbinare.aspx?TIPO=' + document.getElementById('HiddenField1').value + '&ID=<%=lIdDomanda %>', window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');
        }
    </script>
</body>
</html>
