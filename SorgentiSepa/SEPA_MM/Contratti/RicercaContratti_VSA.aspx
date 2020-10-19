<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaContratti_VSA.aspx.vb"
    Inherits="Contratti_RicercaContratti_VSA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ricerca Gestione Locatari</title>
    <style type="text/css">
        .style1
        {
            left: 450px;
            width: 170px;
            padding: 1px 1px 1px 20px;
        }
        
        .style2
        {
            left: 450px;
            width: 180px;
            padding: 1px 1px 1px 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; background-repeat: no-repeat; top: 0px;">
        <table width="100%">
            <tr>
                <td style="padding-left: 10px;">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Ricerca Gestione
                        Locatari</strong></span>
                </td>
            </tr>
            <tr>
                <td height="10px">
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="1">
        <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label31" runat="server" Text="Quadro normativo" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>

            <td>
                    <asp:DropDownList ID="DropDownListType" runat="server" Width="360px" TabIndex="7" Style="border: 1px solid black;"
                        Font-Names="arial" Font-Size="9pt" AutoPostBack="True">
                        <asp:ListItem Selected="True">TUTTI</asp:ListItem>
                        <asp:ListItem>R.R. 1/2004 e s.m.i.</asp:ListItem>
                        <asp:ListItem>R.R. 4/2017 e s.m.i.</asp:ListItem>
                    </asp:DropDownList>
            </td>
        </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label1" runat="server" Text="Cognome" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td width="350px">
                    <asp:TextBox ID="txtCognome" runat="server" TabIndex="1" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px"></asp:TextBox>
                </td>
                <td style="padding-left: 10px">
                    <asp:Label ID="Label2" runat="server" Text="Nome" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNome" runat="server" TabIndex="2" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label3" runat="server" Text="Cod.Fiscale" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td width="300px">
                    <asp:TextBox ID="txtCodF" runat="server" TabIndex="3" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px"></asp:TextBox>
                </td>
                <td align="left" style="padding-left: 10px">
                    <asp:Label ID="Label4" runat="server" Text="Partita Iva" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIva" runat="server" TabIndex="4" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label5" runat="server" Text="Codice Rapporto" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td width="300px">
                    <asp:TextBox ID="txtCodContr" runat="server" TabIndex="5" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px"></asp:TextBox>
                </td>
                <td style="padding-left: 10px">
                    <asp:Label ID="Label6" runat="server" Text="Codice Unità" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCodUI" runat="server" TabIndex="6" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label7" runat="server" Text="Stato Rapporto" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbStato" runat="server" Width="360px" TabIndex="7" Style="border: 1px solid black;"
                        Font-Names="arial" Font-Size="9pt">
                        <asp:ListItem>BOZZA</asp:ListItem>
                        <asp:ListItem>IN CORSO</asp:ListItem>
                        <asp:ListItem>IN CORSO (S.T.)</asp:ListItem>
                        <asp:ListItem>CHIUSO</asp:ListItem>
                        <asp:ListItem Selected="True">TUTTI</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="padding-left: 10px">
                    <asp:Label ID="Label9" runat="server" Text="Durata" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDurata" runat="server" Width="30px" TabIndex="8" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                        ControlToValidate="txtDurata" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                        Font-Names="arial" Font-Size="8pt" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                    <asp:Label ID="Label10" runat="server" Text="+" Font-Names="Arial" Font-Size="8pt"
                        Height="20px"></asp:Label>
                    <asp:TextBox ID="txtRinnovo" runat="server" Width="30px" TabIndex="9" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                        ControlToValidate="txtRinnovo" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                        Font-Names="arial" Font-Size="8pt" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="left" style="left: 450px; width: 180px; padding: 1px 1px 1px 20px;">
                    <table width="140px" cellpadding="0" cellspacing="0">
                        <tr style="height: 20px; vertical-align: top;">
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Tipologia Rapporto" Font-Names="Arial"
                                    Font-Size="8pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblSpecifico" runat="server" Text="Tipo Contr.Specifico" Font-Names="Arial"
                                            Font-Size="8pt" Visible="False"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="1">
                                <tr style="height: 22px; vertical-align: top;">
                                    <td>
                                        <asp:DropDownList ID="cmbTipo" runat="server" Width="360px" TabIndex="10" Style="border: 1px solid black;"
                                            AutoPostBack="True" Font-Names="arial" Font-Size="9pt">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="cmbProvenASS" runat="server" Width="360px" TabIndex="11" Visible="False"
                                            Style="border: 1px solid black;" Font-Names="arial" Font-Size="9pt">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label11" runat="server" Text="Tipologia Unità" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbTipoUnita" runat="server" Width="360px" TabIndex="12" Style="border: 1px solid black;"
                        Font-Names="arial" Font-Size="9pt">
                        <asp:ListItem Value="AL">ALLOGGI</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label14" runat="server" Text="Nome Sede T." Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbFiliale" runat="server" Width="360px" TabIndex="13" Style="border: 1px solid black;"
                        Font-Names="arial" Font-Size="9pt">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label24" runat="server" Text="Nome Operatore" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbOperatore" runat="server" Width="360px" TabIndex="13" Style="border: 1px solid black;"
                        Font-Names="arial" Font-Size="9pt">
                    </asp:DropDownList>
                </td>
            </tr>
            <%--            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label12" runat="server" Text="Tipologia Domanda" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbTipoDom" runat="server" Width="250px" TabIndex="13" Style="border: 1px solid black;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="lblMotivo" runat="server" Text="Motivo present.domanda" Font-Names="Arial"
                        Font-Size="8pt" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbMotivo" runat="server" Width="250px" TabIndex="13" Visible="False"
                        Style="border: 1px solid black;">
                    </asp:DropDownList>
                </td>
            </tr>
            --%>
            <tr>
                <td align="left" style="left: 450px; width: 180px; padding: 1px 1px 1px 20px;">
                    <table width="140px" cellpadding="0" cellspacing="0">
                        <tr style="height: 20px; vertical-align: top;">
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Tipologia Domanda" Font-Names="Arial"
                                    Font-Size="8pt"></asp:Label>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblMotivo" runat="server" Text="Motivo present.domanda" Font-Names="Arial"
                                            Font-Size="8pt" Visible="False"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="1">
                                <tr style="height: 22px; vertical-align: top;">
                                    <td>
                                        <asp:DropDownList ID="cmbTipoDom" runat="server" Width="360px" TabIndex="14" Style="border: 1px solid black;"
                                            AutoPostBack="True" Font-Names="arial" Font-Size="9pt">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="cmbMotivo" runat="server" Width="360px" TabIndex="15" Visible="False"
                                            Style="border: 1px solid black;" Font-Names="arial" Font-Size="8pt">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label13" runat="server" Text="Stato Decisioni Domanda" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbstatodom" runat="server" Width="360px" TabIndex="16" Style="border: 1px solid black;"
                        Font-Names="arial" Font-Size="9pt">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label30" runat="server" Text="Stato Generale Domanda" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbStatoSospesa" runat="server" Width="360" Style="border: 1px solid black;"
                        Font-Names="arial" Font-Size="9pt">
                        <asp:ListItem Value="0">---</asp:ListItem>
                        <asp:ListItem Value="1">VALIDA</asp:ListItem>
                        <asp:ListItem Value="2">SOSPESA</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" style="left: 150px">
                    <asp:Label ID="Label27" runat="server" Text="Protocollo Domanda" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumPG" runat="server" TabIndex="17" Width="80px" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table cellpadding="1" cellspacing="1" style="padding: 1px 1px 1px 18px; width: 565px;">
            <tr>
                <td class="style2">
                    <asp:Label ID="Label16" runat="server" Text="Data Evento dal" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td width="200px">
                    <asp:TextBox ID="txtDataEventoDAL" runat="server" Width="80px" TabIndex="18" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataEventoDAL"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                    </asp:RegularExpressionValidator>
                </td>
                <td align="right">
                    <asp:Label ID="Label17" runat="server" Text="Al" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataEventoAL" runat="server" Width="80px" TabIndex="19" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataEventoAL"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label19" runat="server" Text="Data Presentazione dal" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataPresDAL" runat="server" Width="80px" TabIndex="20" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataPresDAL"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                    </asp:RegularExpressionValidator>
                </td>
                <td align="right">
                    <asp:Label ID="Label18" runat="server" Text="Al" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataPresAL" runat="server" Width="80px" TabIndex="21" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataPresAL"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label22" runat="server" Text="Data Inizio Val. dal" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataInizioDAL" runat="server" Width="80px" TabIndex="22" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtDataInizioDAL"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                    </asp:RegularExpressionValidator>
                </td>
                <td align="right">
                    <asp:Label ID="Label20" runat="server" Text="Al" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataInizioAL" runat="server" Width="80px" TabIndex="23" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDataInizioAL"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label25" runat="server" Text="Data Fine Val.dal" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataFineDAL" runat="server" Width="80px" TabIndex="24" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtDataFineDAL"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                    </asp:RegularExpressionValidator>
                </td>
                <td align="right">
                    <asp:Label ID="Label21" runat="server" Text="Al" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataFineAL" runat="server" Width="80px" TabIndex="25" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtDataFineAL"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label28" runat="server" Text="Data Autorizzazione dal" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataAutorizzDAL" runat="server" Width="80px" TabIndex="26" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtDataAutorizzDAL"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                    </asp:RegularExpressionValidator>
                </td>
                <td align="right">
                    <asp:Label ID="Label26" runat="server" Text="Al" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataAutorizzAL" runat="server" Width="80px" TabIndex="27" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                        ControlToValidate="txtDataAutorizzAL" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                        Font-Names="arial" Font-Size="8pt" Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label15" runat="server" Text="Data Inserimento dal" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataPGDal" runat="server" Width="80px" TabIndex="28" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                        ControlToValidate="txtDataPGDal" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                        Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                    </asp:RegularExpressionValidator>
                </td>
                <td align="right">
                    <asp:Label ID="Label23" runat="server" Text="Al" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataPGal" runat="server" Width="80px" TabIndex="29" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                        ControlToValidate="txtDataPGal" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                        Font-Names="arial" Font-Size="8pt" Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style2" height="25">
                    <asp:Label ID="Label29" runat="server" Text="Componenti in Carrozzina" Font-Names="Arial"
                        Font-Size="8pt"></asp:Label>
                </td>
                <td height="25" valign="middle">
                    <asp:DropDownList ID="ddl_invalid" runat="server" Width="85px" TabIndex="16" Style="border: 1px solid black;"
                        Font-Names="arial" Font-Size="9pt">
                        <asp:ListItem Value="0">---</asp:ListItem>
                        <asp:ListItem Value="1">SI</asp:ListItem>
                        <asp:ListItem Value="2">NO</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" height="25">
                    &nbsp;
                </td>
                <td height="25">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table width="95%">
            <tr>
                <td align="right" height="50px" valign="top">
                    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                        TabIndex="30" ToolTip="Avvia Ricerca" />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                        TabIndex="31" ToolTip="Home" />
                </td>
            </tr>
        </table>
    </div>
    </form>
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


    </script>
</body>
</html>
