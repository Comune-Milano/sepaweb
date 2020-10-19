<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VerificaSManutentivo.aspx.vb"
    Inherits="CENSIMENTO_VerificaSManutentivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Verifica Stato Manutentivo</title>
    <style type="text/css">
        .style14
        {
            font-family: Arial;
            font-size: 10pt;
        }
        
        a:link
        {
            color: #034af3;
        }
        .style15
        {
            text-align: left;
            font-family: Arial;
            font-size: 9pt;
        }
        .style16
        {
            font-size: 3pt;
        }
    </style>
</head>
<body bgcolor="#efefef">
    <form id="form1" runat="server" defaultfocus="cmbDecentramento">
    <div align="center">
        <table style="border: 1px solid #000000; width: 800px;">
            <tr>
                <td>
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>
                        <asp:Label ID="Label1" runat="server" Text="Verifica Stato Manutentivo Unità"></asp:Label>
                    </strong></span>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label21" runat="server" Font-Names="arial" Font-Size="12pt" Font-Bold="True"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblPianoVendita" runat="server" Font-Names="arial" Font-Size="10pt"
                        Text="Zona Decentramento" Visible="False" Style="font-weight: 700"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblUltimaRilevazione" runat="server" Font-Names="arial" Font-Size="10pt"
            Style="font-weight: 700" Width="300px"></asp:Label>
        <br />
        <br />
        <table style="border: 1px solid #000000; width: 800px;">
            <tr>
                <td style="text-align: left; width: 200px;">
                    <asp:Label ID="Label29" runat="server" Font-Names="arial" Font-Size="10pt" Text="DATI DELL'EDIFICIO"
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 200px;">
                    <asp:Label ID="Label25" runat="server" Font-Names="arial" Font-Size="10pt" Text="Quartiere"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbQuartiere" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        TabIndex="1" Width="350px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 200px;">
                    <asp:Label ID="Label22" runat="server" Font-Names="arial" Font-Size="10pt" Text="Condominio"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbCondominio" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        TabIndex="2" Enabled="False">
                        <asp:ListItem Value="1">SI</asp:ListItem>
                        <asp:ListItem Value="0">NO</asp:ListItem>
                        <asp:ListItem Selected="True" Value="NULL">--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 200px;">
                    <asp:Label ID="Label23" runat="server" Font-Names="arial" Font-Size="10pt" Text="Gestione diretta Riscaldamento"
                        Width="200px"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbDirettaRisc" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        TabIndex="3" Enabled="False">
                        <asp:ListItem Value="1">SI</asp:ListItem>
                        <asp:ListItem Value="0">NO</asp:ListItem>
                        <asp:ListItem Selected="True" Value="NULL">--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 200px;">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="border: 1px solid #000000; width: 800px;">
            <tr>
                <td style="width: 200px; text-align: left;">
                    <asp:Label ID="Label41" runat="server" Font-Names="arial" Font-Size="10pt" Text="DATI DELL'U.I."
                        Font-Bold="True"></asp:Label>
                </td>
                <td text-align="left">
                    &nbsp;
                </td>
                <td text-align="left">
                    &nbsp;
                </td>
                <td style="width: 300px; text-align: left;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 200px; text-align: left;">
                    <asp:Label ID="Label39" runat="server" Font-Names="arial" Font-Size="10pt" Text="U.I. Occupata Abusivamente"
                        Width="200px"></asp:Label>
                </td>
                <td text-align="left">
                    <asp:DropDownList ID="cmbAbusivo" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        TabIndex="4" AutoPostBack="True" CausesValidation="True">
                        <asp:ListItem Value="1">SI</asp:ListItem>
                        <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td text-align="left">
                    &nbsp;
                </td>
                <td style="width: 300px; text-align: left;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label14" runat="server" Font-Names="arial" Font-Size="10pt" Text="Zona Decentramento"></asp:Label>
                </td>
                <td style="font-family: arial; font-size: 8pt; font-style: italic; text-align: left;"
                    valign="middle">
                    <asp:DropDownList ID="cmbDecentramento" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        TabIndex="5">
                        <%--<asp:ListItem Value="01" Selected="True">01</asp:ListItem>
                        <asp:ListItem Value="02">02</asp:ListItem>
                        <asp:ListItem Value="03">03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>99</asp:ListItem>--%>
                    </asp:DropDownList>
                    &nbsp;<%--<asp:ListItem Value="01" Selected="True">01</asp:ListItem>
                        <asp:ListItem Value="02">02</asp:ListItem>
                        <asp:ListItem Value="03">03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>99</asp:ListItem>--%>
                </td>
                <td text-align="left">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label15" runat="server" Font-Names="arial" Font-Size="10pt" Text="Tipologia"></asp:Label>
                </td>
                <td text-align="left">
                    <asp:DropDownList ID="cmbTipo" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        TabIndex="6" Width="200px">
                    </asp:DropDownList>
                </td>
                <td text-align="left">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td text-align="left">
                    <asp:Label ID="Label16" runat="server" Font-Names="arial" Font-Size="10pt" Text="Numero Vani"></asp:Label>
                </td>
                <td text-align="left">
                    <asp:TextBox ID="txtVani" runat="server" Font-Names="ARIAL" Font-Size="10pt" MaxLength="10"
                        Width="43px" TabIndex="7"></asp:TextBox>
                </td>
                <td text-align="left">
                    <asp:Label ID="Label17" runat="server" Font-Names="arial" Font-Size="10pt" Text="Numero Servizi"
                        Width="100px"></asp:Label>
                </td>
                <td width="200" style="text-align: left">
                    <asp:TextBox ID="txtServizi" runat="server" Font-Names="ARIAL" Font-Size="10pt" MaxLength="10"
                        Width="43px" TabIndex="8"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td text-align="left">
                    &nbsp;
                </td>
                <td text-align="left">
                    <asp:Label ID="lblsuggerito" runat="server" Font-Names="arial" Font-Size="8pt"></asp:Label>
                </td>
                <td text-align="left">
                    &nbsp;
                </td>
                <td width="200" style="text-align: left">
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table style="border: 1px solid #000000; width: 800px;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: left; font-family: Arial; border-left-color: #800000; border-left-width: thin;
                    border-right-color: #800000; border-right-width: thin; border-top-color: #800000;
                    border-top-width: thin;" class="style16">
                    &nbsp;
                </td>
                <td style="text-align: left; font-family: Arial; border-left-color: #800000; border-left-width: thin;
                    border-right-color: #800000; border-right-width: thin; border-top-color: #800000;
                    border-top-width: thin;" class="style16">
                    &nbsp;
                </td>
                <td style="text-align: left; font-family: Arial; border-left-color: #800000; border-left-width: thin;
                    border-right-color: #800000; border-right-width: thin; border-top-color: #800000;
                    border-top-width: thin;" class="style16">
                    &nbsp;
                </td>
                <td style="text-align: left; font-family: Arial; border-left-color: #800000; border-left-width: thin;
                    border-right-color: #800000; border-right-width: thin; border-top-color: #800000;
                    border-top-width: thin;" class="style16">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" class="style14">
                    <strong>Informazioni Pre-Sloggio / Sloggio</strong>
                </td>
                <td style="text-align: left;">
                    <asp:Menu ID="TStampe" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Black"
                        Orientation="Horizontal" RenderingMode="List" ToolTip="Elenco Stampe ">
                        <DynamicHoverStyle BackColor="#C0FFC0" BorderWidth="1px" Font-Bold="True" ForeColor="#0000C0" />
                        <DynamicMenuItemStyle BackColor="#E9F1F5" Height="20px" ItemSpacing="2px" BorderStyle="None"
                            ForeColor="#0066FF" Width="150px" />
                        <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                            VerticalPadding="1px" />
                        <Items>
                            <asp:MenuItem ImageUrl="../NuoveImm/Img_Stampa_Moduli.png" Selectable="False" Value="">
                                <asp:MenuItem Text="Modulo pre-sloggio" Value="1"></asp:MenuItem>
                                <asp:MenuItem Text="Ricevuta ritiro chiavi" Value="2"></asp:MenuItem>
                                <asp:MenuItem Text="Rapporto sloggio" Value="3"></asp:MenuItem>
                                <asp:MenuItem Text="Promemoria Utente" Value="4"></asp:MenuItem>
                            </asp:MenuItem>
                        </Items>
                    </asp:Menu>
                </td>
                <td style="text-align: left;">
                    <asp:ImageButton ID="btn_verbale" runat="server" OnClientClick="ApriVerbaleSloggio();return false;"
                        ToolTip="Verbale di Sloggio" ImageUrl="../NuoveImm/Img_Verbale.png" Style="cursor: pointer;" />
                </td>
                <td style="text-align: center;">
                    <asp:CheckBox ID="Chk_SL" runat="server" Font-Names="arial" Font-Size="10pt" TabIndex="34"
                        Text="Verbale Sloggio Completo" onClick="SloggioComplChk();" />
                </td>
            </tr>
            <tr>
                <td style="border-bottom: thin solid #800000; text-align: left; font-size: 3pt; font-family: Arial;
                    border-left-color: #800000; border-left-width: thin; border-right-color: #800000;
                    border-right-width: thin; border-top-color: #800000; border-top-width: thin;">
                    &nbsp;
                </td>
                <td style="border-bottom: thin solid #800000; text-align: left; font-size: 3pt; font-family: Arial;
                    border-left-color: #800000; border-left-width: thin; border-right-color: #800000;
                    border-right-width: thin; border-top-color: #800000; border-top-width: thin;">
                    &nbsp;
                </td>
                <td style="border-bottom: thin solid #800000; text-align: left; font-size: 3pt; font-family: Arial;
                    border-left-color: #800000; border-left-width: thin; border-right-color: #800000;
                    border-right-width: thin; border-top-color: #800000; border-top-width: thin;">
                    &nbsp;
                </td>
                <td style="border-bottom: thin solid #800000; text-align: left; font-size: 3pt; font-family: Arial;
                    border-left-color: #800000; border-left-width: thin; border-right-color: #800000;
                    border-right-width: thin; border-top-color: #800000; border-top-width: thin;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left" width="30%">
                    <asp:Label ID="Label26" runat="server" Font-Names="arial" Font-Size="10pt" Text="Ultimo Rapporto attivo su questa unità"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblUltimoRapporto" runat="server" Font-Names="arial" Font-Size="10pt"
                        Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label27" runat="server" Font-Names="arial" Font-Size="10pt" Text="Data Disdetta"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblDataDisdetta" runat="server" Font-Names="arial" Font-Size="10pt"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left" colspan="4">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="3" width="100%">
                                            <asp:CheckBox ID="Chk_PB" runat="server" Text="Porta Blindata" Font-Names="arial"
                                                Font-Size="10pt" onclick="AbilitaDrop(this.checked)" TabIndex="9" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="5%">
                                            <asp:Label ID="Label64" Font-Names="arial" Font-Size="10pt" runat="server" Text="Mano"></asp:Label>
                                        </td>
                                        <td width="20%">
                                            <asp:DropDownList ID="Ddl_mano" runat="server" Height="20px" Width="62px" Font-Size="10pt"
                                                TabIndex="10" Font-Names="arial">
                                                <asp:ListItem Value="-1">---</asp:ListItem>
                                                <asp:ListItem Value="0">dx</asp:ListItem>
                                                <asp:ListItem Value="1">sx</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label65" Font-Names="arial" Font-Size="10pt" runat="server" Text="Sopraluce"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="Ddl_sopL" runat="server" Height="20px" Width="62px" Font-Size="10pt"
                                                TabIndex="11" Font-Names="arial">
                                                <asp:ListItem Value="-1">---</asp:ListItem>
                                                <asp:ListItem Value="0">si</asp:ListItem>
                                                <asp:ListItem Value="1">no</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="3" width="100%">
                                            <asp:CheckBox ID="CheckBox4" runat="server" Font-Names="arial" Font-Size="10pt" TabIndex="12"
                                                Text="Sostituzione serratura porta blindata" />
                                        </td>
                                        <td width="100%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="5%">
                                            <asp:CheckBox ID="Chk_nLF" runat="server" Font-Names="arial" Font-Size="10pt" TabIndex="13"
                                                Text="N°" onclick="AbilitaTxtLF(this.checked)" />
                                        </td>
                                        <td width="20%">
                                            <asp:TextBox ID="lastraF_txt" runat="server" Font-Names="arial" Font-Size="10pt"
                                                TabIndex="14" Width="25px"></asp:TextBox>
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="Label58" Font-Names="arial" TabIndex="33" Font-Size="10pt" runat="server"
                                                Text=" lastra di protezione finestra" Width="200px"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="Chk_nLPF" runat="server" Font-Names="arial" Font-Size="10pt" TabIndex="15"
                                                Text="N°" onclick="AbilitaTxtLPF(this.checked)" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lastraPF_txt" runat="server" Font-Names="arial" Font-Size="10pt"
                                                TabIndex="16" Width="25px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label59" Font-Names="arial" Font-Size="10pt" runat="server" Text=" lastra di protezione porta finestra"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="3" width="100%">
                                            <asp:CheckBox ID="ChPB4" runat="server" Font-Names="arial" Font-Size="10pt" TabIndex="17"
                                                Text="Sostituzione serratura serranda box" />
                                        </td>
                                        <td width="100%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="5%">
                                            &nbsp;
                                        </td>
                                        <td width="20%">
                                            <asp:CheckBox ID="Chk_nSerr" runat="server" Font-Names="arial" Font-Size="10pt" TabIndex="18"
                                                Text="N°" onclick="AbilitaTxtSerr(this.checked)" />
                                        </td>
                                        <td width="10%">
                                            <asp:TextBox ID="serr_txt" runat="server" Font-Names="arial" Font-Size="10pt" TabIndex="19"
                                                Width="25px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label60" Font-Names="arial" Font-Size="10pt" runat="server" Text=" sostituzione serratura serranda negozio"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label63" runat="server" Font-Names="arial" Font-Size="10pt" Text="Data PRE-SLOGGIO"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="datapreSL_txt" runat="server" Font-Names="arial" Font-Size="10pt"
                        TabIndex="20" Width="82px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="datapreSL_txt"
                        Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False" Font-Names="arial"
                        Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label24" runat="server" Font-Names="arial" Font-Size="10pt" Text="Data di SLOGGIO"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="dataSL_txt" runat="server" Font-Names="arial" Font-Size="10pt" TabIndex="21"
                        Width="82px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                        ControlToValidate="dataSL_txt" Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False"
                        Font-Names="arial" Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                    &nbsp;<br />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label40" runat="server" Font-Names="arial" Font-Size="10pt" 
                        Text="Recuperata da SEC"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:CheckBox ID="chGRTP" runat="server" Font-Names="arial" Font-Size="10pt" TabIndex="22" />
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblNoteGRTP" runat="server" Font-Names="arial" Font-Size="10pt" 
                        Text="Note SEC"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtNoteGRTP" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        MaxLength="200" Width="191px" TabIndex="23" Height="55px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="10pt" Text="Data Soprall. POST-SLOGGIO/inizio lavori"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDataSopralluogo" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        MaxLength="10" Width="82px" TabIndex="24"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtDataSopralluogo"
                        Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False" Font-Names="arial"
                        Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table style="border: 1px solid #000000; width: 800px;">
            <tr>
                <td style="text-align: left; width: 327px;">
                    <asp:Label ID="Label46" runat="server" Font-Names="arial" Font-Size="10pt" Text="STRUTTURA DEPOSITARIA DELLE CHIAVI"
                        Width="277px" Font-Bold="True"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:RadioButton ID="Rd5" runat="server" Font-Names="arial" Font-Size="9pt" GroupName="X"
                        Text="Non Definito" TabIndex="25" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 327px;">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 327px;">
                    <asp:Label ID="Label47" runat="server" Font-Names="arial" Font-Size="10pt" Text="Ufficio Tecnico 1-2-3"
                        Font-Bold="True" Visible="False"></asp:Label>
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 327px;">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    <asp:RadioButton ID="Rd1" runat="server" Font-Names="arial" Font-Size="9pt" GroupName="X"
                        Text="Via Saponaro" TabIndex="26" Visible="False" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 327px;">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    <asp:RadioButton ID="Rd2" runat="server" Font-Names="arial" Font-Size="9pt" GroupName="X"
                        Text="Via Newton" TabIndex="27" Visible="False" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 327px;">
                    <asp:Label ID="Label52" runat="server" Font-Names="arial" Font-Size="10pt" Text="Ufficio Tecnico 4-5-6"
                        Font-Bold="True" Visible="False"></asp:Label>
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 327px;">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    <asp:RadioButton ID="Rd3" runat="server" Font-Names="arial" Font-Size="9pt" GroupName="X"
                        Text="Via Costa" TabIndex="28" Visible="False" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 327px;">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    <asp:RadioButton ID="Rd4" runat="server" Font-Names="arial" Font-Size="9pt" GroupName="X"
                        Text="Via Salemi" TabIndex="29" Visible="False" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 327px;">
                    &nbsp;&nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 327px;">
                    <asp:Label ID="Label53" runat="server" Font-Names="arial" Font-Size="10pt" Text="Struttura competente in caso di lavori"
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbStruttura" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        TabIndex="30" Width="450px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 327px;">
                    <asp:Label ID="Label54" runat="server" Font-Names="arial" Font-Size="10pt" Text="Data Consegna alla Struttura competente"
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDataSTDal" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        MaxLength="10" Width="82px" TabIndex="31"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                        ControlToValidate="txtDataSTDal" Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False"
                        Font-Names="arial" Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 327px;">
                    <asp:Label ID="Label55" runat="server" Font-Names="arial" Font-Size="10pt" Text="Data Ripresa dalla Struttura competente"
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDataSTAl" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        MaxLength="10" Width="82px" TabIndex="32"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                        ControlToValidate="txtDataSTAl" Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False"
                        Font-Names="arial" Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="border: 1px solid #000000; width: 800px;">
            <tr>
                <td text-align="left">
                    <asp:Label ID="Label32" runat="server" Font-Names="arial" Font-Size="10pt" Text="Data Quantificazione Addebiti"
                        Width="200px"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDataQuantificazione" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        MaxLength="10" Width="82px" TabIndex="33"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                        ControlToValidate="txtDataQuantificazione" Display="Dynamic" ErrorMessage="Errata!"
                        Font-Bold="False" Font-Names="arial" Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style15">
                    Importo Danni Verbale Sloggio
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDanniVerbSl" runat="server" Font-Names="ARIAL" Font-Size="9pt"
                        MaxLength="100" Width="82px" TabIndex="34" ReadOnly="True">0,00</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td text-align="left">
                    <asp:Label ID="Label33" runat="server" Font-Names="arial" Font-Size="10pt" Text="Importo Altri Danni"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtImpDanni" runat="server" Font-Names="Arial" Font-Size="9pt" Width="82px"
                        TabIndex="35">0,00</asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtImpDanni"
                        ErrorMessage="Errato" Font-Bold="False" Font-Names="ARIAL" Font-Size="8pt" ValidationExpression="\b\d*,\d{2}\b"
                        ToolTip="Valore numerico con 2 cifre decimali separati dalla virgola!"></asp:RegularExpressionValidator>
                    <asp:Label ID="Label43" runat="server" Font-Names="arial" Font-Size="8pt" Text="Inserire l'importo comprensivo di IVA"
                        Font-Italic="True" Font-Strikeout="False" Width="248px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td text-align="left">
                    <asp:Label ID="Label34" runat="server" Font-Names="arial" Font-Size="10pt" Text="Importo trasporto Masserizie"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtImpTrasporto" runat="server" Font-Names="Arial" Font-Size="9pt"
                        Width="82px" TabIndex="36">0,00</asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                        ControlToValidate="txtImpTrasporto" ErrorMessage="Errato" Font-Bold="False" Font-Names="ARIAL"
                        Font-Size="8pt" ValidationExpression="\b\d*,\d{2}\b" ToolTip="Valore numerico con 2 cifre decimali separati dalla virgola!"></asp:RegularExpressionValidator>
                    <asp:Label ID="Label44" runat="server" Font-Names="arial" Font-Size="8pt" Text="Inserire l'importo comprensivo di IVA"
                        Font-Italic="True" Font-Strikeout="False" Width="204px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td text-align="left">
                    <asp:Label ID="Label36" runat="server" Font-Names="arial" Font-Size="10pt" Text="Note"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtNoteDanni" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        MaxLength="500" Width="553px" Height="61px" TextMode="MultiLine" TabIndex="37"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td text-align="left">
                    <asp:Label ID="Label37" runat="server" Font-Names="arial" Font-Size="10pt" Text="Importi Autorizzati"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:CheckBox ID="chAutorizzati" runat="server" Font-Names="arial" Font-Size="10pt"
                        TabIndex="38" />
                    &nbsp;<br />
                    <asp:Label ID="Label38" runat="server" Font-Names="arial" Font-Size="8pt" Text="Se non autorizzati, non sarà possibile chiudere il rapporto di utenza e calcolare le relative bollette di chiusura contabile"
                        Font-Italic="True" Font-Strikeout="False"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table style="border: 1px solid #000000; width: 800px;">
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="10pt" Text="Stato Alloggio"
                        Style="text-align: left"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbStatoAlloggio" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        Width="140px" TabIndex="39" AutoPostBack="True" CausesValidation="True">
                        <asp:ListItem Value="0">NON AGIBILE</asp:ListItem>
                        <asp:ListItem Value="1" Selected="True">AGIBILE</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Text="Stato Amministrativo"
                        Style="text-align: left"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbRiassegnabile" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        Width="200px" TabIndex="40" AutoPostBack="True" CausesValidation="True">
                        <asp:ListItem Value="0">(ri)Assegnabile con lavori</asp:ListItem>
                        <asp:ListItem Value="1">(ri)Assegnabile senza lavori</asp:ListItem>
                        <asp:ListItem Value="2">Non (ri)Assegnabile</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <asp:CheckBox ID="chFineL" runat="server" Font-Names="arial" Font-Size="10pt" Text="Fine Lavori"
                        TabIndex="41" AutoPostBack="True" CausesValidation="True" onclick="DaResettare()"/>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label30" runat="server" Font-Names="arial" Font-Size="10pt" Text="Data Presunta Disponibilità"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDataDisponibilita" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        MaxLength="10" Width="82px" TabIndex="42"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                        ControlToValidate="txtDataDisponibilita" Display="Dynamic" ErrorMessage="Errata!"
                        Font-Bold="False" Font-Names="arial" Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label42" runat="server" Font-Names="arial" Font-Size="8pt" Text="Indicare la data di entrata se trattasi di O.A."
                        Font-Italic="True" Font-Strikeout="False" Width="221px" Height="16px"></asp:Label>
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table style="border: 1px solid #000000; width: 800px;">
            <tr>
                <td style="text-align: left; width: 278px;" valign="top">
                    <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="10pt" Text="Tipo Porta"></asp:Label>
                </td>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="ChPB" runat="server" Font-Names="arial" Font-Size="10pt" Text="Porta Blindata"
                                    TabIndex="43" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="ChPB1" runat="server" Font-Names="arial" Font-Size="10pt" Text="Porta Rinforzata"
                                    TabIndex="44" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="ChPB2" runat="server" Font-Names="arial" Font-Size="10pt" Text="Porta Normale"
                                    TabIndex="45" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="ChPB3" runat="server" Font-Names="arial" Font-Size="10pt" Text="Porta di altro tipo"
                                    TabIndex="46" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtNoteTipoPorta" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                                    MaxLength="500" Width="433px" Height="74px" TextMode="MultiLine" TabIndex="47"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 278px;" valign="top">
                    <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="10pt" Text="Indicare quali soluzioni per la messa in sicurezza si intendono realizzare"
                        Width="232px"></asp:Label>
                    &nbsp;
                </td>
                <td style="text-align: left" valign="top">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="chGP" runat="server" Font-Names="arial" Font-Size="10pt" Text="Grata Porta"
                                    TabIndex="48" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="ChGF" runat="server" Font-Names="arial" Font-Size="10pt" Text="Grata Finestre"
                                    TabIndex="49" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="ChAllarme" runat="server" Font-Names="arial" Font-Size="10pt" Text="Allarme"
                                    TabIndex="50" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="ChLA" runat="server" Font-Names="arial" Font-Size="10pt" Text="Lastratura Porte"
                                    TabIndex="51" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="ChLA1" runat="server" Font-Names="arial" Font-Size="10pt" Text="Lastratura Finestre"
                                    TabIndex="52" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="ChAL" runat="server" Font-Names="arial" Font-Size="10pt" Text="Altro"
                                    TabIndex="53" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtNoteSicurezza" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                                    MaxLength="500" Width="433px" Height="74px" TextMode="MultiLine" TabIndex="54"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 278px;">
                    <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="10pt" Text="destinata a portatori di Handicap"></asp:Label>
                </td>
                <td style="text-align: left; font-family: ARIAL; font-size: 8pt; font-style: italic;">
                    <asp:DropDownList ID="cmbHandicap" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        TabIndex="55" Style="text-align: left">
                        <asp:ListItem Value="1">SI</asp:ListItem>
                        <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;(obbligatorio solo se trattasi di ALLOGGIO)
                </td>
            </tr>
            <tr>
                <td style="width: 278px;">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 278px;" valign="top">
                    <asp:Label ID="Label45" runat="server" Font-Names="arial" Font-Size="10pt" Text="Motivazioni"
                        Width="316px" Font-Italic="False"></asp:Label>
                    <br />
                    <asp:Label ID="Label13" runat="server" Font-Names="arial" Font-Size="8pt" Text="Obbligatorie se NON  destinata a portatori di Handicap"
                        Width="316px" Font-Italic="True"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtMotivazioni" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        MaxLength="500" Width="433px" Height="74px" TextMode="MultiLine" TabIndex="56"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 278px;">
                    <asp:Label ID="Label18" runat="server" Font-Names="arial" Font-Size="10pt" Text="Data Consegna Chiavi"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDataConsegnaChiavi" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        MaxLength="10" Width="82px" TabIndex="57"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtDataConsegnaChiavi"
                        Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False" Font-Names="arial"
                        Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 278px;">
                    <asp:Label ID="Label19" runat="server" Font-Names="arial" Font-Size="10pt" Text="Consegnate a:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtConsegnatea" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        MaxLength="100" Width="430px" TabIndex="58"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 278px;">
                    <asp:Label ID="Label20" runat="server" Font-Names="arial" Font-Size="10pt" Text="Data Ripresa Chiavi"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDataRipresaChiavi" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        MaxLength="10" Width="82px" TabIndex="59"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtDataRipresaChiavi"
                        Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False" Font-Names="arial"
                        Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 278px;">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="border: 1px solid #000000; width: 800px;">
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt" Text="Elenco Interventi Manutenzione Programmati"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:CheckBoxList ID="chLavori" runat="server" Font-Names="arial" Font-Size="10pt"
                        TabIndex="60">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label9" runat="server" Font-Names="arial" Font-Size="10pt" Text="Data Presunta di Fine Lavori"></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="txtDataFine" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        MaxLength="10" Width="82px" TabIndex="61"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                        ControlToValidate="txtDataFine" Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False"
                        Font-Names="arial" Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblNote" runat="server" Font-Names="arial" Font-Size="10pt" Text="Note"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:TextBox ID="txtNote" runat="server" Font-Names="ARIAL" Font-Size="10pt" MaxLength="500"
                        Width="778px" Height="119px" TextMode="MultiLine" TabIndex="62"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblNote0" runat="server" Font-Names="arial" Font-Size="10pt" Text="Storico delle Verifiche:"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label12" runat="server" Font-Names="arial" Font-Size="10pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    &nbsp;&nbsp;&nbsp;
                    <img onclick="StampaModulo();" alt="Stampa Modulo Bianco" src="../NuoveImm/Img_Stampa_Modulo_Bianco.png"
                        style="cursor: pointer;" id="Stampa0" />&nbsp;&nbsp;
                    <img onclick="StampaReport();" alt="Stampa" src="../NuoveImm/Img_Stampa_Grande.png"
                        style="cursor: pointer;" id="Stampa" />&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="ImgSalva" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                        TabIndex="51" Style="cursor: pointer; height: 20px;" />
                    <img onclick="ConfermaEsci();" alt="Esci" src="../NuoveImm/Img_EsciCorto.png" style="cursor: pointer;" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
        ForeColor="Red" Visible="False"></asp:Label>
    <asp:HiddenField ID="Modificato" runat="server" />
    <asp:HiddenField ID="idunita" runat="server" />
    <asp:HiddenField ID="EVENTO" runat="server" />
    <asp:HiddenField ID="idcontratto" runat="server" />
    <asp:HiddenField ID="CODICE" runat="server" />
    <asp:HiddenField ID="chiamante" runat="server" Value="0" />
    <asp:HiddenField ID="statoc" runat="server" />
    <asp:HiddenField ID="lettura" runat="server" />
    <asp:HiddenField ID="tipounita" runat="server" />
    <asp:HiddenField ID="datasloggio" runat="server" />
    <asp:HiddenField ID="idSloggio" runat="server" Value="0" />
    <asp:HiddenField ID="idStatoSl" runat="server" Value="0" />
    <asp:HiddenField ID="idContChiuso" runat="server" Value="0" />
    <asp:HiddenField ID="msgDataDisd" runat="server" Value="0" />
    <asp:HiddenField ID="RUdataDecorrenza" runat="server" Value="" />
    <asp:HiddenField ID="t" runat="server" Value="0" />
    <asp:HiddenField ID="resetta" runat="server" />
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
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

        function ConfermaEsci() {

            if (document.getElementById('Modificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                if (chiediConferma == true) {
                    self.close();
                    //document.getElementById('Modificato').value = '111';
                    //document.getElementById('USCITA').value='0';
                }
            }
            else {
                self.close();
            }
        }

        function StampaReport() {
            if (document.getElementById('Modificato').value == '1') {
                alert('Salvare le modifiche effettuate prima di stampare il report!');
            }
            else {
                if (document.getElementById('lettura').value != '2') {
                    window.open('RepostStatoManutentivo1.aspx?A=1&TIPO=0&ID=' + document.getElementById('idunita').value, 'Stato', '');
                }
                else {
                    window.open('RepostStatoManutentivo1.aspx?A=0&TIPO=0&ID=' + document.getElementById('idunita').value, 'Stato', '');
                }
            }
        }

        function StampaModulo() {
            window.open('ModuloBianco.aspx?', 'Modulo', '');

        }


        function SloggioComplChk() {
            if (document.getElementById('Chk_SL').checked == true) {


                alert('Attenzione! I dati relativi al Verbale Sloggio non potranno essere più modificabili a seguito del salvataggio!');

            }


        }






        function AbilitaDrop(status) {
            status = !status;
            document.getElementById('Ddl_mano').disabled = status;
            document.getElementById('Ddl_sopL').disabled = status;
        }


        function AbilitaTxtLF(status) {
            status = !status;
            document.getElementById('lastraF_txt').disabled = status;

        }

        function AbilitaTxtLPF(status) {
            status = !status;
            document.getElementById('lastraPF_txt').disabled = status;

        }

        function AbilitaTxtSerr(status) {
            status = !status;
            document.getElementById('serr_txt').disabled = status;

        }

        function ApriVerbaleSloggio() {
            if (document.getElementById('msgDataDisd').value != 1) {
                if (document.getElementById('idSloggio').value != 0) {
                    window.open('VerbaleSloggio.aspx?ID=' + document.getElementById('idunita').value + '&A=' + document.getElementById('chiamante').value + '&IDSTATO=' + document.getElementById('idStatoSl').value + '&IDSLOGGIO=' + document.getElementById('idSloggio').value + '&T=' + document.getElementById('t').value, 'VerbaleSloggio', '');
                }
                else {
                    alert('Salvare i dati della verifica stato manutentivo!');
                }
            }
            else {
                alert('Verbale disponibile solo con data disdetta del contratto!');

            }

        }
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }


        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            o.value = o.value.replace('.', ',');
            //document.getElementById('txtModificato').value = '1';
        }



        function DelPointer(obj) {
            obj.value = obj.value.replace('.', '');
            document.getElementById(obj.id).value = obj.value;

        }

        function DaResettare() {
            if (document.getElementById('chFineL').checked == true) {
                document.getElementById('resetta').value = '1';
            }
            else {
                document.getElementById('resetta').value = '0';
            }
        }

    </script>
    </form>
</body>
</html>
