<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Dichiara_VSA.ascx.vb"
    Inherits="Dom_Dichiara_VSA" %>
<div id="dic" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    left: 10px; border-left: lightsteelblue 1px solid; width: 641px; border-bottom: lightsteelblue 1px solid;
    position: absolute; top: 107px; height: 315px; background-color: #ffffff; z-index: 199;">
    <p>
        &nbsp;</p>
    <asp:Label ID="Label16" Style="z-index: 101; left: 9px; position: absolute; top: 32px;
        width: 190px;" Font-Size="8pt" Font-Names="Times New Roman" Height="18px" runat="server"
        ForeColor="#0000C0">Motivo di presentazione della Richiesta</asp:Label>
    <asp:Label ID="lblCodContrattoScambio" Style="z-index: 101; left: 9px; position: absolute;
        top: 32px; width: 190px;" Font-Size="8pt" Font-Names="Times New Roman" Height="18px"
        runat="server" ForeColor="#0000C0" Visible="False">Codice Contratto oggetto dello scambio</asp:Label>
    <asp:Label ID="lblDataDomanda" Style="z-index: 101; left: 9px; position: absolute;
        top: 59px; width: 190px;" Font-Size="8pt" Font-Names="Times New Roman" Height="18px"
        runat="server" ForeColor="#0000C0">Data presentazione Richiesta</asp:Label>
    <asp:Label ID="lblDataEvento" Style="z-index: 101; left: 305px; position: absolute;
        top: 59px; width: 190px;" Font-Size="8pt" Font-Names="Times New Roman" Height="18px"
        runat="server" ForeColor="#0000C0">Data Evento</asp:Label>
    <asp:Label ID="lblInizioVal" Style="z-index: 101; left: 484px; position: absolute;
        top: 58px; width: 67px;" Font-Size="8pt" Font-Names="Times New Roman" Height="18px"
        runat="server" ForeColor="#0000C0">Inizio Validità</asp:Label>
    <asp:Label ID="lblFineVal" Style="z-index: 101; left: 486px; position: absolute;
        top: 87px; width: 64px;" Font-Size="8pt" Font-Names="Times New Roman" Height="18px"
        runat="server" ForeColor="#0000C0">Fine Validità</asp:Label>
    <asp:Label ID="Label13" Style="z-index: 101; left: 9px; position: absolute; top: 10px;
        width: 190px;" Font-Size="8pt" Font-Names="Times New Roman" Height="18px" runat="server"
        ForeColor="#0000C0">Tipologia Richiesta</asp:Label>
        <asp:Label ID="Label17" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 193px" Width="129px">Tipo Documento</asp:Label>
    <asp:Label ID="Label6" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 218px" Width="129px">Doc. Identita N.</asp:Label>
    <asp:Label ID="Label9" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 271px; position: absolute;
        top: 216px" Width="36px">Data</asp:Label>
    <asp:Label ID="Label12" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 384px; position: absolute;
        top: 217px" Width="80px">Rilasciata Da</asp:Label>
    <asp:Label ID="Label7" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 242px" Width="129px">Permesso Soggiorno N.</asp:Label>
    <asp:Label ID="Label10" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 271px; position: absolute;
        top: 243px" Width="34px">Data</asp:Label>
    <asp:Label ID="Label11" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 382px; position: absolute;
        top: 243px" Width="57px">Scadenza</asp:Label>
    <asp:Label ID="Label15" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 512px; position: absolute;
        top: 241px" Width="81px">Rinnovo</asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 9px; position: absolute;
        top: 270px" Width="129px">Carta Soggiorno N.</asp:Label>
    <asp:Label ID="Label14" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        ForeColor="#0000C0" Height="18px" Style="z-index: 101; left: 272px; position: absolute;
        top: 269px" Width="32px">Data</asp:Label>
    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="Black" Height="18px" Style="z-index: 101; left: 9px;
        position: absolute; top: 171px" Width="290px">ESTREMI DOCUMENTO DI RICONOSCIMENTO</asp:Label>
    <asp:Label ID="Label1" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 8px; position: absolute; top: 130px;
        width: 417px;" ForeColor="#0000C0">Il nucleo familiare richiedente ha effettuato gli adempimenti connessi all&#39;anagrafe utenza?</asp:Label>
    &nbsp;&nbsp;
    <asp:Label ID="lblDurataProc" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 8px; position: absolute; top: 107px;
        width: 417px;" ForeColor="#0000C0"></asp:Label>
    <asp:Label ID="lblModPres" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 8px; position: absolute; top: 83px;
        width: 417px;" ForeColor="#0000C0"></asp:Label>
    <asp:DropDownList ID="cmbTipoRichiesta" Style="z-index: 103; left: 200px; position: absolute;
        top: 3px; width: 417px;" ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman"
        CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" TabIndex="1"
        AutoPostBack="True">
    </asp:DropDownList>
    <asp:Image ID="imgAlert" runat="server" ImageUrl="IMG/Alert.gif" Style="z-index: 125;
        left: 10px; position: absolute; top: 294px; height: 17px;" />
    <asp:Label ID="lblDocumRicon" runat="server" Font-Names="arial" Font-Size="8pt" Font-Bold="True"
        ForeColor="#0000C0" Style="z-index: 101; left: 32px; position: absolute; top: 297px;
        width: 580px;">Verificare la correttezza degli estremi del documento di riconoscimento!</asp:Label>
    <asp:DropDownList ID="cmbSiNo" Style="z-index: 103; left: 430px; position: absolute;
        top: 153px; width: 58px;" ForeColor="Red" Font-Size="8pt" Font-Names="Times New Roman"
        CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" TabIndex="4"
        Visible="False">
        <asp:ListItem Value="1">SI</asp:ListItem>
        <asp:ListItem Value="0">NO</asp:ListItem>
    </asp:DropDownList>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;<br />
    <asp:DropDownList ID="cmbPresentaD" Style="z-index: 103; left: 200px; position: absolute;
        top: 26px; width: 417px;" ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman"
        CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" 
        TabIndex="2">
    </asp:DropDownList>
    <br />
    <asp:HyperLink ID="CEM" runat="server" Style="position: absolute; top: 33px; left: 202px;"
        Font-Bold="True" Font-Names="arial" Font-Size="8pt" Visible="False">Clicca qui per visualizzare/inserire le motivazioni della domanda</asp:HyperLink>
    <asp:TextBox ID="txtInizioVal" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 555px; position: absolute; top: 57px" TabIndex="3" Width="72px"></asp:TextBox>
    <br />
    <asp:DropDownList ID="cmbFattaAU" Style="z-index: 103; left: 430px; position: absolute;
        top: 127px; width: 58px;" ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman"
        CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" 
        TabIndex="4">
        <asp:ListItem Value="1">SI</asp:ListItem>
        <asp:ListItem Value="0">NO</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <br />
    <br />
    <br />
    &nbsp;<br />
    <br />
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:TextBox ID="txtCINum" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="25" Style="z-index: 124;
        left: 142px; position: absolute; top: 214px" TabIndex="5" Width="120px"></asp:TextBox>
        <asp:DropDownList ID="cmbTipoDocumento" runat="server" CssClass="CssProv"
        Font-Bold="False" Font-Names="Times New Roman" Font-Size="7pt" ForeColor="#0000C0"
        Height="20px" Style="z-index: 121; left: 142px; position: absolute; top: 192px"
        TabIndex="110" Width="126px">
        <asp:ListItem Selected="True" Value="0">CARTA IDENTITA</asp:ListItem>
        <asp:ListItem Value="1">PASSAPORTO</asp:ListItem>
        <asp:ListItem Value="2">PATENTE DI GUIDA</asp:ListItem>
        <asp:ListItem Value="3">TESSERE MINISTERIALI</asp:ListItem>
        <asp:ListItem Value="-1">--</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="txtCIRilascio" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="50" Style="z-index: 124;
        left: 449px; position: absolute; top: 214px; width: 181px;" TabIndex="7"></asp:TextBox>
    <asp:TextBox ID="txtCIData" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 298px; position: absolute; top: 215px" TabIndex="6" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtDataPrRichiesta" runat="server" Columns="7" Font-Bold="True"
        Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10"
        Style="z-index: 124; left: 199px; position: absolute; top: 57px" TabIndex="3"
        Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtDataEvento" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 398px; position: absolute; top: 57px" TabIndex="3" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtFineVal" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 555px; position: absolute; top: 86px" TabIndex="3" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtCodContrattoScambio" runat="server" Columns="7" Font-Bold="True"
        Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="19"
        Style="z-index: 124; left: 199px; position: absolute; top: 32px; width: 158px;"
        TabIndex="3" Visible="False"></asp:TextBox>
    <%--<asp:CheckBox ID="chkAler" runat="server" Style="z-index: 124; left: 393px; position: absolute;
        top: 32px; width: 199px;" Font-Size="8pt" ForeColor="#0000C0" Text="Trattasi di contratto ALER"
        Visible="False" />--%>
    <img id="imgDatiContratto" style="position: absolute; top: 34px; left: 364px; cursor: pointer"
        alt="Visualizza Dati Contratto" onclick="ApriContratto();" src="../Condomini/Immagini/Search_16x16.png" />
    <img id="imgCercaContratto" alt="Cerca codice contratto" src="NuoveImm/Img_Seleziona.png"
        onclick="apriRicerca();" style="position: absolute; top: 63px; left: 364px; cursor: pointer;
        visibility: hidden;" />
    <asp:TextBox ID="txtPSData" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 298px; position: absolute; top: 241px" TabIndex="9" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtPSScade" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 429px; position: absolute; top: 240px" TabIndex="10" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtPSRinnovo" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 558px; position: absolute; top: 240px" TabIndex="11" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtCSData" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="10" Style="z-index: 124;
        left: 298px; position: absolute; top: 267px" TabIndex="13" Width="72px"></asp:TextBox>
    <asp:TextBox ID="txtPSNum" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="25" Style="z-index: 124;
        left: 142px; position: absolute; top: 240px" TabIndex="8" Width="120px"></asp:TextBox>
    <asp:TextBox ID="txtCSNum" runat="server" Columns="7" Font-Bold="True" Font-Names="TIMES"
        Font-Size="8pt" ForeColor="Blue" Height="18px" MaxLength="25" Style="z-index: 124;
        left: 142px; position: absolute; top: 266px" TabIndex="12" Width="120px"></asp:TextBox>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:Label ID="lblCointest" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
        Height="18px" Style="z-index: 101; left: 9px; position: absolute; top: 153px;
        width: 417px;" ForeColor="Red" Visible="False">E' presente una morosità!! Procedere alla cointestazione di uscente
        e subentrante?
    </asp:Label>
    &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
</div>
<asp:HiddenField ID="cu" runat="server" />
<asp:HiddenField ID="DOMANDA" runat="server" />
<asp:HiddenField ID="DICHIARAZIONE" runat="server" />
<script language="javascript" type="text/javascript">

    // Funzione javascript per l'inserimento in automatico degli slash nella data
    function CompletaData(e, obj) {

        var sKeyPressed;
        sKeyPressed = (window.event) ? event.keyCode : e.which;

        if (sKeyPressed < 48 || sKeyPressed > 57) {
            if (sKeyPressed != 8 && sKeyPressed != 0) {
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


    function apriRicerca() {

        var win = null;
        LeftPosition = (screen.width) ? (screen.width - 620) / 2 : 0;
        TopPosition = (screen.height) ? (screen.height - 500) / 2 : 0;
        LeftPosition = LeftPosition - 20;
        TopPosition = TopPosition - 20;
        window.open('RicercaUI.aspx', 'RicercaUI', 'height=450,top=0,left=0,width=670,scrollbars=no');
    }

</script>
