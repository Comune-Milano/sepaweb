<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AggIndirizzi.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_AggIndirizzi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Indirizzo</title>
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../../GestioneAllegati/Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../../GestioneAllegati/Scripts/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../../../GestioneAllegati/Scripts/jquery-ui-1.9.0.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function disabilitaMinore(e) {
            var key;
            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 226)
                return false;
            else
                return true;
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 221px;
        }
        .style2
        {
            width: 61px;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla" />
    <div>
        <asp:HiddenField ID="modificaINDIRIZZO" runat="server" Value="0" />
        <asp:HiddenField ID="indSELEZIONATO" runat="server" Value="-1" />
        <asp:HiddenField ID="noCAP" runat="server" Value="0" />
 
        &nbsp; <strong>
        <%--    <asp:Label runat="server" ID="nomeFORN" Style="color: #660000; font-family: Arial;
                font-size: 11px; vertical-align: text-top;">
            </asp:Label>--%>
        </strong>
      
        <table>
            <tr>
                <td>
                    <telerik:RadButton ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva le modifiche apportate all'indirizzo"
                        Style="top: 0px; left: 0px">
                    </telerik:RadButton>
                </td>
                <td>
                    <telerik:RadButton ID="btnApriInd0" runat="server" Text="Esci" ToolTip="Esci" OnClientClicking="function(sender,args){ CloseAndRefresh('Tab_Indirizzi_btnApriInd');}">
                    </telerik:RadButton> 
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtIndirizzo" runat="server" Width="200px" MaxLength="100" onkeydown="return disabilitaMinore(event)"
                        Font-Names="Arial" Font-Size="8pt" TabIndex="1" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="erroreIndirizzo" runat="server" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Text="Indirizzo obbligatorio"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Provincia"></asp:Label>
                </td>
                <td class="style1">
                    <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True" Width="90px"
                        Font-Names="Arial" Font-Size="8pt" TabIndex="2" Enabled="False">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="erroreProvincia" runat="server" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Text="Seleziona provincia"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Comune"></asp:Label>
                </td>
                <td class="style1">
                    <asp:DropDownList ID="ddlComuni" runat="server" Width="200px" AutoPostBack="True"
                        Font-Names="Arial" Font-Size="8pt" TabIndex="3" Enabled="False">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="erroreComune" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                        Text="Seleziona comune"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cap"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtCap" runat="server" Width="63px" MaxLength="5" onkeydown="return disabilitaMinore(event)"
                        Font-Italic="False" Font-Names="Arial" Font-Size="8pt" TabIndex="4" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="errorecap" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                        Text="CAP non valido"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Telefono 1"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtTelefono1" runat="server" Width="200px" MaxLength="30" onkeydown="return disabilitaMinore(event)"
                        Font-Names="Arial" Font-Size="8pt" TabIndex="5" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Telefono 2"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtTelefono2" runat="server" Width="200px" MaxLength="30" onkeydown="return disabilitaMinore(event)"
                        Font-Names="Arial" Font-Size="8pt" TabIndex="6"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fax"></asp:Label>
                </td>
                <td style="text-align: left" class="style1">
                    <asp:TextBox ID="txtFax" runat="server" Width="200px" MaxLength="30" onkeydown="return disabilitaMinore(event)"
                        Style="text-align: left" Font-Names="Arial" Font-Size="8pt" TabIndex="7"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="E-mail"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="txtMail" runat="server" Width="200px" MaxLength="40" onkeydown="return disabilitaMinore(event)"
                        Style="text-align: left" Font-Names="Arial" Font-Size="8pt" TabIndex="8"></asp:TextBox>
                </td>
                <td style="text-align: right">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;&nbsp;
                </td>
                <td class="style2">
                    &nbsp;&nbsp;
                </td>
                <td style="text-align: right">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td style="text-align: right" class="style1">
                </td>
                <td style="text-align: left">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
