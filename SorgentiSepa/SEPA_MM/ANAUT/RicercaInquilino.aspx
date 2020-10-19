<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaInquilino.aspx.vb"
    Inherits="ANAUT_RicercaInquilino" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;

    function $onkeydown() {

        //if (event.keyCode==13) 
        //{  
        //alert('Usare il tasto <Avvia Ricerca>');
        //history.go(0);
        //event.keyCode=0;
        //}  
    }

</script>
<script type="text/javascript">
    //document.onkeydown = $onkeydown;


    function CompletaData(e, obj) {
        // Check if the key is a number
        var sKeyPressed;

        sKeyPressed = (window.event) ? event.keyCode : e.which;

        if (sKeyPressed != 13) {
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
    }


</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Ricerca</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body bgcolor="#f2f5f1" onload="document.getElementById('btnCerca').focus()">
    <script type="text/javascript">
        //document.onkeydown=$onkeydown;
    </script>
    <form id="Form1" method="post" runat="server" defaultbutton="btnCerca" defaultfocus="txtCognome">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
        position: absolute; top: 0px">
        <tr>
            <td style="width: 670px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                    Inquilino con Appuntamento</strong></span><br />
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
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" BorderWidth="1px"
        MaxLength="50" Style="z-index: 113; left: 173px; position: absolute; top: 103px;
        width: 320px;" TabIndex="1"></asp:TextBox>
    <asp:TextBox ID="txtNome" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
        Style="z-index: 113; left: 173px; position: absolute; top: 139px; width: 320px;"
        TabIndex="2"></asp:TextBox>
    <asp:TextBox ID="txtContratto" runat="server" BorderStyle="Solid" BorderWidth="1px"
        MaxLength="19" Style="z-index: 113; left: 173px; position: absolute; top: 175px;
        width: 320px;" TabIndex="3"></asp:TextBox>
    <asp:TextBox ID="txtDal" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="19"
        Style="z-index: 113; left: 173px; position: absolute; top: 353px; width: 93px;"
        TabIndex="9"></asp:TextBox>
    <asp:TextBox ID="txtAl" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="19"
        Style="z-index: 113; left: 297px; position: absolute; top: 353px; width: 93px;"
        TabIndex="10"></asp:TextBox>
    <asp:TextBox ID="txtConvocazione" runat="server" BorderStyle="Solid" BorderWidth="1px"
        Style="z-index: 113; left: 173px; position: absolute; top: 212px; width: 110px;"
        TabIndex="4"></asp:TextBox>
    &nbsp;
    <asp:CheckBox ID="chk392" runat="server" Style="position: absolute; top: 213px; left: 307px;"
        Visible="true" Font-Bold="True" Font-Names="arial" Font-Size="8pt" TabIndex="4"
        Text="RU di tipo 392/78" />
    <asp:CheckBox ID="Chk431" runat="server" Style="position: absolute; top: 213px; left: 425px;"
        Visible="true" Font-Bold="True" Font-Names="arial" Font-Size="8pt" TabIndex="4"
        Text="RU di tipo 431/98" />
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
        Style="z-index: 100; left: 539px; position: absolute; top: 472px" TabIndex="13"
        ToolTip="Home" />
    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
        Style="z-index: 101; left: 406px; position: absolute; top: 472px; height: 20px;"
        TabIndex="12" ToolTip="Avvia Ricerca" />
    <asp:Label ID="Label10" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 50px; position: absolute; top: 215px;">N. Convocazione</asp:Label>
    <asp:Label ID="Label5" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 50px; position: absolute; top: 355px;">Giorno Dal</asp:Label>
        <asp:Label ID="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
            Style="z-index: 110; left: 50px; position: absolute; top: 392px;">Operatore</asp:Label>
            <asp:TextBox ID="txtOperatore" runat="server" BorderStyle="Solid" BorderWidth="1px"
        MaxLength="50" Style="z-index: 113; left: 173px; position: absolute; top: 390px;
        width: 320px;" TabIndex="11"></asp:TextBox>
    <asp:Label ID="Label7" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 276px; position: absolute; top: 355px;">Al</asp:Label>
    <asp:Label ID="Label3" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 50px; position: absolute; top: 319px">Sede Territoriale</asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 49px; position: absolute; top: 251px;">Stato Conv.</asp:Label>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="Label9" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
                Style="z-index: 110; left: 50px; position: absolute; top: 285px;" Visible="False">Motivo Sosp.</asp:Label>
            <asp:Label ID="lblPresa" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
                Style="z-index: 110; left: 522px; position: absolute; top: 284px;" Visible="False">Solo Prese in carico. AUCM</asp:Label>
            <asp:CheckBox ID="CheckBox1" runat="server" Style="position: absolute; top: 280px;
                left: 500px;" Visible="False" />
            <asp:DropDownList ID="cmbStatoConv" TabIndex="5" runat="server" Height="20px" Width="112px"
                Style="border: 1px solid black; z-index: 111; left: 173px; position: absolute;
                top: 247px" AutoPostBack="True" CausesValidation="True">
            </asp:DropDownList>
            <asp:DropDownList ID="cmbMotivo" TabIndex="6" runat="server" Height="20px" Style="border: 1px solid black;
                z-index: 111; left: 173px; position: absolute; top: 281px; width: 320px;" Visible="False"
                AutoPostBack="True" CausesValidation="True">
            </asp:DropDownList>
            <asp:DropDownList ID="cmbFiliale" TabIndex="7" runat="server" Height="20px" Style="border: 1px solid black;
                z-index: 111; left: 173px; position: absolute; top: 316px" Width="320px" AutoPostBack="True">
            </asp:DropDownList>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 50px; position: absolute; top: 178px">Cod.Contratto</asp:Label>
    <asp:Label ID="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 50px; position: absolute; top: 104px">Cognome</asp:Label>
    <asp:Label ID="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 50px; position: absolute; top: 141px">Nome</asp:Label>
    </form>
</body>
</html>
