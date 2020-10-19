<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaContratti.aspx.vb" Inherits="ARCHIVIO_RicercaContratti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;

    function $onkeydown() {

        if (event.keyCode == 13) {
            alert('Usare il tasto <Avvia Ricerca>');
            history.go(0);
            event.keyCode = 0;
        }
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="..\Contratti\prototype.lite.js"></script>
<script type="text/javascript" src="..\Contratti\moo.fx.js"></script>
<script type="text/javascript" src="..\Contratti\moo.fx.pack.js"></script>
<head>
    <title>Ricerca Contratti</title>
</head>
<body bgcolor="#f2f5f1">
    <script type="text/javascript">
        //document.onkeydown = $onkeydown;


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
    <form id="Form1" method="post" runat="server" defaultbutton="btnCerca" defaultfocus="txtCognome">
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; top: 0px">
        <tr>
            <td style="width: 800px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                    Rapporti</strong></span><br />
                <br />
                <br />
                <br />
                <asp:CheckBox ID="ChIntest" runat="server" Style="left: 619px; position: absolute;
                    top: 77px" Font-Names="ARIAL" Font-Size="9pt" TabIndex="30" 
                    Text="Solo Intestatari" Checked="True" />
                <br />
                &nbsp;<br />
                <br />
                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 101; left: 50px; position: absolute; top: 135px" TabIndex="-1">Rag. Sociale</asp:Label>
                <asp:TextBox ID="txtRagione" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    Style="z-index: 107; left: 164px; position: absolute; top: 130px" TabIndex="5"></asp:TextBox>
                &nbsp;<br />
                <img onclick="javascript:myOpacity.toggle();" alt="" src="../NuoveImm/Img_Indirizzi.png"
                    style="position: absolute; top: 206px; left: 329px; cursor: pointer;" />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 50px; position: absolute; top: 267px" TabIndex="-1">Tipologia Rapporto</asp:Label>
                <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 113; left: 471px; position: absolute; top: 263px" TabIndex="-1">Durata</asp:Label>
                <asp:TextBox ID="txtDurata" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 510px; position: absolute; top: 262px"
                    TabIndex="11" Width="25px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                    ControlToValidate="txtDurata" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                    Font-Names="arial" Font-Size="8pt" ValidationExpression="\d+" Style="z-index: 113;
                    left: 543px; position: absolute; top: 265px"></asp:RegularExpressionValidator>
                <asp:Label ID="Label29" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 554px; position: absolute; top: 264px; width: 6px;"
                    TabIndex="-1">+</asp:Label>
                <asp:TextBox ID="txtRinnovo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 572px; position: absolute; top: 262px"
                    TabIndex="12" Width="25px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                    ControlToValidate="txtRinnovo" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                    Font-Names="arial" Font-Size="8pt" ValidationExpression="\d+" Style="z-index: 113;
                    left: 606px; position: absolute; top: 265px"></asp:RegularExpressionValidator>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cmbTipo" runat="server" Height="20px" Style="border: 1px solid black;
                            z-index: 118; left: 163px; position: absolute; top: 263px" TabIndex="10" Width="278px"
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:Label ID="lblSpecifico" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" Style="z-index: 114; left: 50px; position: absolute; top: 294px"
                            TabIndex="-1" Visible="False">Tipo Contr.Specifico</asp:Label>
                        <asp:DropDownList ID="cmbProvenASS" runat="server" Height="20px" Style="border: 1px solid black;
                            z-index: 118; left: 163px; position: absolute; top: 292px" TabIndex="11" Width="278px"
                            Visible="False">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 50px; position: absolute; top: 238px" TabIndex="-1">Stato Rapporto</asp:Label>
                <asp:DropDownList ID="cmbStato" runat="server" Height="20px" Style="border: 1px solid black;
                    z-index: 118; left: 163px; position: absolute; top: 235px" TabIndex="9" Width="278px">
                    <asp:ListItem>BOZZA</asp:ListItem>
                    <asp:ListItem>IN CORSO</asp:ListItem>
                    <asp:ListItem>IN CORSO (S.T.)</asp:ListItem>
                    <asp:ListItem>CHIUSO</asp:ListItem>
                    <asp:ListItem Selected="True">TUTTI</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 50px; position: absolute; top: 325px" TabIndex="-1">Tipologia Unità</asp:Label>
                <asp:DropDownList ID="cmbTipoImm" runat="server" Height="20px" Style="border: 1px solid black;
                    z-index: 118; left: 163px; position: absolute; top: 321px" TabIndex="12" 
                    Width="278px">
                </asp:DropDownList>
                <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 50px; position: absolute; top: 405px" 
                    TabIndex="-1">Scatola</asp:Label>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:CheckBox ID="ChVirtuali" runat="server" Style="left: 467px; position: absolute;
                    top: 234px" Font-Names="ARIAL" Font-Size="9pt" TabIndex="30" Text="Solo R.U. virtuali" />
                    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 50px; position: absolute; top: 350px" Width="100px"
        TabIndex="-1">Codice Eustorgio</asp:Label>
        <asp:TextBox ID="txtEustorgio" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 113;
        left: 164px; position: absolute; top: 348px;" TabIndex="13"></asp:TextBox>
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 50px; position: absolute; top: 378px" Width="100px"
        TabIndex="-1">Faldone</asp:Label>
        <asp:TextBox ID="txtFaldone" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 113;
        left: 164px; position: absolute; top: 374px;" TabIndex="14"></asp:TextBox>
        <asp:TextBox ID="txtScatola" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 113;
        left: 164px; position: absolute; top: 401px;" TabIndex="15"></asp:TextBox>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
    &nbsp;
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
        Style="z-index: 100; left: 660px; position: absolute; top: 504px" TabIndex="26"
        ToolTip="Home" />
    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
        Style="z-index: 101; left: 527px; position: absolute; top: 504px" TabIndex="16"
        ToolTip="Avvia Ricerca" />
    <asp:Label ID="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 102; left: 50px; position: absolute; top: 80px; right: 1065px;"
        TabIndex="-1">Cognome</asp:Label>
    <asp:TextBox ID="txtCognome" TabIndex="1" runat="server" Style="z-index: 103; position: absolute;
        top: 77px; left: 164px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:Label ID="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 104; left: 344px; position: absolute; top: 79px" TabIndex="-1">Nome</asp:Label>
    <asp:TextBox ID="txtNome" TabIndex="2" runat="server" Style="z-index: 105; left: 411px;
        position: absolute; top: 77px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:Label ID="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 101; left: 50px; position: absolute; top: 108px; height: 14px;
        width: 70px;" TabIndex="-1">Codice Fiscale</asp:Label>
    <asp:TextBox ID="txtCF" TabIndex="3" runat="server" Style="z-index: 107; left: 164px;
        position: absolute; top: 103px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 106; left: 344px; position: absolute; top: 106px" TabIndex="-1">Partita Iva</asp:Label>
    <asp:TextBox ID="txtpiva" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 107;
        left: 411px; position: absolute; top: 103px" TabIndex="4"></asp:TextBox>
    &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 50px; position: absolute; top: 186px; height: 14px;"
        Width="100px" TabIndex="-1">Codice C. GIMI</asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 50px; position: absolute; top: 159px" Width="100px"
        TabIndex="-1">Codice Rapporto</asp:Label>
        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 344px; position: absolute; top: 159px" Width="100px"
        TabIndex="-1">Cod. Utente</asp:Label>
    <asp:TextBox ID="txtCodGIMI" runat="server" Style="z-index: 113; left: 164px; position: absolute;
        top: 182px" TabIndex="7" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:TextBox ID="txtCod" runat="server" Style="z-index: 113; left: 164px; position: absolute;
        top: 156px" TabIndex="6" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
        <asp:TextBox ID="txtCodUtente" runat="server" Style="z-index: 113; left: 411px; position: absolute;
        top: 156px" TabIndex="6" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 50px; position: absolute; top: 212px" Width="100px"
        TabIndex="-1">Codice Unità</asp:Label>
    <asp:TextBox ID="txtUnita" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 113;
        left: 164px; position: absolute; top: 207px" TabIndex="8"></asp:TextBox>
    <div id="Indirizzi" style="display: block; border: 1px solid #0000FF; position: absolute;
        width: 349px; width: 296px; background-color: #C0C0C0; top: 70px; left: 449px;
        height: 431px; overflow: auto; z-index: 200;">
        <table style="width: 90%;">
            <tr>
                <td style="text-align: right">
                    <img onclick="javascript:myOpacity.toggle();" alt="" src="../NuoveImm/Img_Conferma.png"
                        style="cursor: pointer" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBoxList ID="chIndirizzi" runat="server" Font-Names="arial" Font-Size="8pt">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <img onclick="javascript:myOpacity.toggle();" alt="" src="../NuoveImm/Img_Conferma.png"
                        style="cursor: pointer" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script type="text/javascript">
    myOpacity = new fx.Opacity('Indirizzi', { duration: 200 });
    myOpacity.hide();
</script>
</html>

