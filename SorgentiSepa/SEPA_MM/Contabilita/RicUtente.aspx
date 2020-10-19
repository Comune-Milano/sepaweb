<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicUtente.aspx.vb" Inherits="IMPIANTI_RicercaImpianti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
<script type="text/javascript">
<!--
var Uscita1;
Uscita1=1;
// -->
</script>


<head id="Head1" runat="server">
    <title>RICERCA</title>
    <style type="text/css">
        .style1
        {
            height: 28px;
        }
        .style2
        {
            height: 236px;
        }
        .style3
        {
            font-size: 10pt;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <div>
        L&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
                    width: 800px; position: absolute; top: 0px; height: 483px">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <br />
                        &nbsp;&nbsp; Ricerca Inquilino</span></strong><br />
                    <br />
                    <br />
                    <div style="left: 8px; overflow: auto; width: 784px; position: absolute; top: 64px;
                        height: 370px">
									<asp:label id="lblCognome0" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="False" 
                style="z-index: 104; left: 50px; position: absolute; top: 58px" 
                TabIndex="-1">Nome</asp:label>
									<asp:label id="lblCognome" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="False" 
                style="z-index: 104; left: 50px; position: absolute; top: 34px" 
                TabIndex="-1">Cognome</asp:label>
									<asp:label id="lblCodFisc" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="False" 
                style="z-index: 104; left: 50px; position: absolute; top: 81px" 
                TabIndex="-1">Cod. Fiscale</asp:label>
									<asp:label id="lblCodFisc0" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="False" 
                style="z-index: 104; left: 50px; position: absolute; top: 153px" 
                TabIndex="-1">Cod. Contratto</asp:label>
        <asp:DropDownList ID="cmbInterno" runat="server" BackColor="White" Font-Names="ARIAL"
            Font-Size="10pt" Height="20px" 
                                        Style="border: 1px solid black; z-index: 111; left: 566px; position: absolute; top: 244px" 
                                        TabIndex="13" Width="80px">
        </asp:DropDownList>
    
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 521px; position: absolute; top: 247px">Interno</asp:Label>
        <asp:DropDownList ID="cmbCivico" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
                                        
                                        Style="border: 1px solid black; z-index: 111; left: 430px; position: absolute; top: 244px" TabIndex="12"
            Width="80px">
        </asp:DropDownList>
        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 388px; position: absolute; top: 247px">Civico</asp:Label>
        <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
            
            
                                        Style="border: 1px solid black; z-index: 111; left: 130px; position: absolute; top: 244px; width: 245px; right: 403px;" 
                                        TabIndex="11">
        </asp:DropDownList>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 50px; position: absolute; top: 179px; bottom: 173px;">Codice U.i.</asp:Label>
        <asp:TextBox ID="txtSub" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Style="z-index: 102; left: 334px; position: absolute; top: 204px"
            Width="40px" TabIndex="10"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 294px; position: absolute; top: 206px">Sub</asp:Label>
        <asp:TextBox ID="txtParticella" runat="server" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 239px; position: absolute;
            top: 204px" Width="40px" TabIndex="9"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 184px; position: absolute; top: 206px">Particella</asp:Label>
        <asp:TextBox ID="txtFoglio" runat="server" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 132px; position: absolute;
            top: 204px; bottom: 142px; right: 612px;" Width="40px" TabIndex="8"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 52px; position: absolute; top: 206px">Foglio</asp:Label>
        <asp:TextBox ID="txtCodUi" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 102;
            left: 132px; position: absolute; top: 176px; width: 242px;" Font-Names="arial" Font-Size="10pt" 
                                        TabIndex="7"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 52px; position: absolute; top: 248px">Indirizzo</asp:Label>
        <asp:TextBox ID="txtCodContratto" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 102;
            left: 132px; position: absolute; top: 151px" Font-Names="arial" Font-Size="10pt" Width="241px" 
                                        TabIndex="6"></asp:TextBox>
        <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 102;
            left: 132px; position: absolute; top: 31px" Font-Names="arial" Font-Size="10pt" Width="241px" 
                                        TabIndex="1"></asp:TextBox>
        <asp:TextBox ID="txtNome" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 102;
            left: 132px; position: absolute; top: 54px" Font-Names="arial" Font-Size="10pt" Width="241px" 
                                        TabIndex="2"></asp:TextBox>
                        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 104; left: 50px; position: absolute; top: 106px" TabIndex="-1">Rag. Sociale</asp:Label>
                        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 104; left: 50px; position: absolute; top: 129px" TabIndex="-1">P. IVA</asp:Label>
                        <asp:TextBox ID="txtRagSociale" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 132px; position: absolute;
                            top: 102px" TabIndex="4" Width="241px"></asp:TextBox>
                        <asp:TextBox ID="txtPiva" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
                            Font-Size="10pt" Style="z-index: 102; left: 132px; position: absolute; top: 126px"
                            TabIndex="5" Width="241px"></asp:TextBox>
        <asp:TextBox ID="txtCf" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 102;
            left: 132px; position: absolute; top: 78px" Font-Names="arial" Font-Size="10pt" Width="241px" 
                                        TabIndex="3"></asp:TextBox>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 693px; position: absolute; top: 326px" ToolTip="Home" TabIndex="15" />
                    </div>
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
                    &nbsp;<br />
                    &nbsp;<br />
                    <asp:HiddenField ID="DivVisible" runat="server" />
                    <br />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 11px; position: absolute; top: 366px; height: 13px; width: 442px;"
                        Text="Label" Visible="False"></asp:Label>
                    <br />
                    <div style="position: absolute; top: 54px; left: 12px; width: 739px; height: 410px;z-index: 200; background-color: #DEDEDE;" 
                        id="PrecisaUtente">
                        <table style="width: 100%; height: 90%;">
                            <tr>
                                <td class="style1" width="100%">
                                    <strong><span style="color: #801f1c; font-family: Arial" class="style3">
                                    Esistono più utenti con i parametri definiti,&nbsp; specificare quale è il 
                                    soggetto interessato.</span></strong></td>
                            </tr>
                            <tr>
                                <td class="style2" style="vertical-align: top; height: 317px; text-align: left">
                                    <div style="overflow: auto; width: 100%; height: 86%">
                                    <asp:RadioButtonList ID="RdbListUtente" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" Width="95%">
                                    </asp:RadioButtonList></div>
                                    <table style="width: 639px">
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="vertical-align: top; text-align: right; height: 54px;">
                                                <asp:ImageButton ID="btnConfirm" runat="server" 
                                                    ImageUrl="~/Contabilita/IMMCONTABILITA/Check_24x24.png" ToolTip="Conferma" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 570px; position: absolute; top: 390px; right: 210px;" 
                        ToolTip="Avvia Ricerca" TabIndex="14" />
                    <br />
                    &nbsp;
        
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
    
    <script type="text/javascript">
    myOpacity = new fx.Opacity('PrecisaUtente', { duration: 200 });
//myOpacity.hide();
    if (document.getElementById('DivVisible').value != '2') 
    {
        myOpacity.hide(); ;
    }
    </script>
</body>
</html>
