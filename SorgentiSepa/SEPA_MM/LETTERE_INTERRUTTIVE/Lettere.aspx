<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Lettere.aspx.vb" Inherits="LETTERE_INTERRUTTIVE_Lettere" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-family: Arial;
            font-weight: bold;
            font-size: x-large;
        }
    </style>
</head>
<body bgcolor="#e4e4e4">
    <form id="form1" runat="server">
    <div>
    
        <table width="100%">
            <tr>
                <td bgcolor="#6699FF" class="style1" style="text-align: center" width='100%'>
                    GESTIONE LETTERE INTERRUTTIVE 2009</td>
            </tr>
            <tr align="center">
                <td width='100%'>
                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="10pt" 
                        
                        Text="Stampa lettere interruttive. Saranno create le lettere in formato RTF" 
                        ForeColor="#CC0000"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td width='100%'>
                    <br />
                    <br />
                    <table style="width: 590px;">
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="Tipo di lettera da stampare"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="cmbTipo" runat="server" Font-Names="arial" 
                                    Font-Size="10pt" Width="350px">
                                    <asp:ListItem Value="ABUSIVI_L_45_B1_1.rtf">ABUSIVI LOTTI 45 - BLOCCO 1</asp:ListItem>
                                    <asp:ListItem Value="ABUSIVI_L_123_B1_1.rtf">ABUSIVI LOTTI 123 - BLOCCO 1</asp:ListItem>
                                    <asp:ListItem Value="ABUSIVI_L_123_B2_1.rtf">ABUSIVI LOTTI 123 - BLOCCO 2</asp:ListItem>
                                    <asp:ListItem Value="FERP_L_12345_B1_1.rtf">FUORI ERP LOTTI 12345 - BLOCCO 1</asp:ListItem>
                                    <asp:ListItem Value="FERP_L_12345_B2_1.rtf">FUORI ERP LOTTI 12345 - BLOCCO 2</asp:ListItem>
                                    <asp:ListItem Value="ERP_L_45_CC_B1.rtf">ERP LOTTI 45 CON CONTRIBUTO - BLOCCO 1</asp:ListItem>
                                    <asp:ListItem Value="ERP_L_45_NOCC_B1_1.rtf">ERP LOTTI 45 SENZA CONTRIBUTO - BLOCCO 1</asp:ListItem>
                                    <asp:ListItem Value="ERP_L_123_CC_B1_1.rtf">ERP LOTTI 123 CON CONTRIBUTO - BLOCCO 1</asp:ListItem>
                                    <asp:ListItem Value="ERP_L_123_NOCC_B1_1.rtf">ERP LOTTI 123 SENZA CONTRIBUTO - BLOCCO 1</asp:ListItem>
                                    <asp:ListItem Value="ERP_L_123_NOCC_B2_1.rtf">ERP LOTTI 123 SENZA CONTRIBUTO - BLOCCO 2</asp:ListItem>
                                    <asp:ListItem Value="ERP_L_123_NOCC_B3_1.rtf">ERP LOTTI 123 SENZA CONTRIBUTO - BLOCCO 3</asp:ListItem>
                                    <asp:ListItem Value="ERP_L_123_NOCC_B4_1.rtf">ERP LOTTI 123 SENZA CONTRIBUTO - BLOCCO 4</asp:ListItem>
                                    <asp:ListItem Value="ERP_L_123_NOCC_B5_1.rtf">ERP LOTTI 123 SENZA CONTRIBUTO - BLOCCO 5</asp:ListItem>
                                    
                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="Numero del protocollo comprensivo di Acronino e Sede Territoriale"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                            <asp:TextBox ID="txtPG" runat="server" Font-Bold="True" Font-Names="arial" 
                                                Font-Size="10pt" Width="167px"></asp:TextBox>
                                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="Data di Stampa da applicare"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                            <asp:TextBox ID="txtDataStampa" runat="server" Font-Bold="True" Font-Names="arial" 
                                                Font-Size="10pt" Width="90px"></asp:TextBox>
                                            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                                runat="server" ControlToValidate="txtDataStampa"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
                        Style="height: 14px;" TabIndex="-1" 
                                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                &nbsp;</td>
                            <td style="text-align: left">
                                            <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="arial" 
                                                Font-Size="10pt" ForeColor="Red" Visible="False"></asp:Label>
                                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                &nbsp;</td>
                            <td style="text-align: right">
                                            <asp:Button ID="Button1" runat="server" BackColor="#6699FF" 
                                                BorderStyle="Outset" ForeColor="White" Text="Procedi" />
                                            </td>
                        </tr>
                    </table>
                    <br />
                    <table style="width: 590px;">
                        <tr>
                            <td style="text-align: left">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="10pt" 
                        
                                    Text="Le lettere saranno create in blocchi, per evitare attese in fase di generazione ed errori dovuti a cadute di rete." 
                                    ForeColor="#000099"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </td>
            </tr>
            <tr align="center">
                <td style="text-align: left" width='100%'>
                                <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Text="Elenco lettere create" 
                        style="font-weight: 700; font-style: italic"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td style="text-align: left" width='100%'>
                    <asp:Label ID="lblTabella" runat="server" Font-Names="arial" Font-Size="8pt" 
                        Width="100%"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
<script language="javascript" type="text/javascript">
    document.getElementById('divLoading').style.visibility = 'hidden';
    </script>
</html>
