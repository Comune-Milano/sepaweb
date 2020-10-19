<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProcessoDecisionale.aspx.vb"
    Inherits="ASS_ProcessoDecisionale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="../Contratti/jquery-1.8.2.js"></script>
<script type="text/javascript" src="../Contratti/jquery-impromptu.4.0.min.js"></script>
<script type="text/javascript" src="../Contratti/jquery.corner.js"></script>
<head runat="server">
    <title>Processo Decisionale</title>
    <link rel="stylesheet" type="text/css" href="../Contratti/impromptu.css" />
    <style type="text/css">
        .stileSezioni {
            font-family: Arial;
            font-size: 12pt;
            font-weight: bold;
            color: black;
        }

        .stileTesto2 {
            font-family: Arial;
            font-size: 10pt;
            background-color: #FFF7D7;
        }

        .stileTesto {
            font-family: Arial;
            font-size: 10pt;
        }

        .giustificato {
            font-family: Arial;
            font-size: 8pt;
            text-align: left;
        }

        .bottone {
            background-color: #EAEAEA;
            font-weight: bold;
            color: Blue;
            height: 32px;
            width: 160px;
            font-family: Arial;
            font-size: 8pt;
        }
    </style>
</head>
<body style="background-image: url(../NuoveImm/XBackGround.gif); background-repeat: repeat-x;">
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="6">
                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Assegnazione
                        Alloggi</span></strong>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblRevoca" runat="server" BackColor="#FF3300" Font-Bold="True" Font-Names="ARIAL"
                            Font-Size="16pt" ForeColor="White" Text="R E V O C A T O" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="stileSezioni" width="150px">
                        <br />
                        DATI DOMANDA
                    </td>
                    <td style="vertical-align: bottom;">
                        <asp:DropDownList ID="cmbDeroga" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="170px" Visible="false">
                            <asp:ListItem Value="0">ASSEGNAZIONE IN DEROGA</asp:ListItem>
                            <asp:ListItem Value="1">ASSEGNAZIONE ORDINARIA</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">&nbsp
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td width="120px" align="left">
                                    <asp:ImageButton ID="btnAggiornaRedditi" runat="server" ImageUrl="../NuoveImm/Img_AggRedditi.png" OnClientClick="CreaDichiarazione();"></asp:ImageButton>

                                </td>
                                <td>
                                    <asp:Menu ID="TStampe" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Black"
                                        Orientation="Horizontal" RenderingMode="Table" ToolTip="Elenco Stampe ">
                                        <DynamicHoverStyle BackColor="#C0FFC0" BorderWidth="1px" Font-Bold="True" ForeColor="#0000C0" />
                                        <DynamicMenuItemStyle BackColor="#E9F1F5" Height="20px" ItemSpacing="2px" BorderStyle="None"
                                            ForeColor="#0066FF" Width="220px" CssClass="giustificato" />
                                        <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                                            VerticalPadding="1px" />
                                        <Items>
                                            <asp:MenuItem ImageUrl="../NuoveImm/Img_Documentazione.png" Selectable="False" Value=""></asp:MenuItem>
                                        </Items>
                                    </asp:Menu>
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; border: 1px solid blue" cellpadding="2" cellspacing="2">
                <tr class="stileTesto2">
                    <td>Nominativo
                    </td>
                    <td style="font-weight: bold" colspan="5">
                        <asp:Label ID="lblNominativo" runat="server" ForeColor="Black" Width="100%" Font-Names="Arial"
                            Font-Size="9pt"></asp:Label>
                    </td>
                </tr>
                <tr id="rigaNuova" runat="server" class="stileTesto2" visible="false">
                    <td>
                        <asp:Label ID="lblProtocolloDom2" runat="server">Protocollo Dom.</asp:Label>
                    </td>
                    <td style="font-weight: bold">
                        <asp:Label ID="lblPGDomNew" runat="server" ForeColor="Black" Width="100%" Font-Names="Arial"
                            Font-Size="9pt"></asp:Label>
                    </td>
                    <td><asp:Label ID="lblProtocolloDich2" runat="server">Protocollo Dich.</asp:Label>
                    </td>
                    <td style="font-weight: bold">
                        <asp:Label ID="lblPGDichNew" runat="server" ForeColor="Black" Width="100%" Font-Names="Arial"
                            Font-Size="9pt"></asp:Label>
                    </td>
                    <td>&nbsp
                    </td>
                    <td style="font-weight: bold">&nbsp
                    </td>

                </tr>
                <tr class="stileTesto2">
                    <td> <asp:Label ID="lblProtocolloDom1" runat="server">Protocollo Dom.</asp:Label>
                    </td>
                    <td style="font-weight: bold">
                        <asp:Label ID="lblPGDom" runat="server" ForeColor="Black" Width="100%" Font-Names="Arial"
                            Font-Size="9pt"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblProtocolloDich1" runat="server">Protocollo Dich.</asp:Label>
                    </td>
                    <td style="font-weight: bold">
                        <asp:Label ID="lblPGDich" runat="server" ForeColor="Black" Width="100%" Font-Names="Arial"
                            Font-Size="9pt"></asp:Label>
                    </td>
                    <td>&nbsp
                    </td>
                    <td style="font-weight: bold">&nbsp
                    </td>

                </tr>

                <tr class="stileTesto2">
                    <td>ISBARC/R
                    </td>
                    <td style="font-weight: bold">
                        <asp:Label ID="lblIsbarc" runat="server" ForeColor="Black" Font-Names="Arial" Font-Size="9pt"
                            Width="100%"></asp:Label>
                    </td>
                    <td>ISEE
                    </td>
                    <td style="font-weight: bold">
                        <asp:Label ID="lblIsee" runat="server" ForeColor="Black" Font-Names="Arial" Font-Size="9pt"
                            Width="100%"></asp:Label>
                    </td>
                    <td>N. Comp.
                    </td>
                    <td style="font-weight: bold">
                        <asp:Label ID="lblNumComp" runat="server" ForeColor="Black" Font-Names="Arial" Font-Size="9pt"
                            Width="100%"></asp:Label>
                    </td>
                </tr>
                <tr class="stileTesto2">
                    <td>Posiz. Grad.
                    </td>
                    <td style="font-weight: bold">
                        <asp:Label ID="lblPosizGr" runat="server" ForeColor="Black" Font-Names="Arial" Font-Size="9pt"
                            Width="100%"></asp:Label>
                    </td>
                    <td>Tipo Alloggio
                    </td>
                    <td style="font-weight: bold">
                        <asp:Label ID="lblTipoAll" runat="server" ForeColor="Black" Font-Names="Arial" Font-Size="9pt"
                            Width="100%"></asp:Label>
                    </td>
                    <td>Estremi Doc.
                    </td>
                    <td style="font-weight: bold">
                        <asp:Label ID="lblNumDoc" runat="server" ForeColor="Black" Font-Names="Arial" Font-Size="9pt"
                            Width="100%"></asp:Label>
                    </td>
                </tr>
                <tr class="stileTesto2">
                    <td>Num. Offerta
                    </td>
                    <td style="font-weight: bold">
                        <asp:Label ID="lblNumOfferta" runat="server" ForeColor="Black" Font-Names="Arial"
                            Font-Size="9pt" Width="100%"></asp:Label>
                    </td>
                    <td>Scad. Offerta
                    </td>
                    <td style="font-weight: bold">
                        <asp:Label ID="lblScadOfferta" runat="server" ForeColor="Black" Font-Names="Arial"
                            Font-Size="9pt" Width="100%"></asp:Label>
                    </td>
                    <td>&nbsp
                    </td>
                    <td>&nbsp
                    </td>
                </tr>
            </table>
            <%--<table>
            <tr class="stileTesto2">
                <td class="stileSezioni" colspan="6">
                    DATI ALLOGGIO ABBINATO
                </td>
            </tr>
            <tr class="stileTesto2">
                <td colspan="6">
                    &nbsp
                </td>
            </tr>
            <tr class="stileTesto2">
                <td>
                    Codice
                </td>
                <td style="font-weight: bold">
                    <asp:Label ID="lblCodUI" runat="server" ForeColor="Black" Font-Names="Arial" Font-Size="9pt"
                        Width="100%"></asp:Label>
                </td>
                <td>
                    Indirizzo
                </td>
                <td style="font-weight: bold" colspan="3">
                    <asp:Label ID="lblIndirizzo" runat="server" ForeColor="Black" Font-Names="Arial"
                        Font-Size="9pt" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr class="stileTesto2">
                <td>
                    Zona
                </td>
                <td style="font-weight: bold">
                    <asp:Label ID="lblZona" runat="server" ForeColor="Black" Font-Names="Arial" Font-Size="9pt"
                        Width="100%"></asp:Label>
                </td>
                <td>
                    Superficie
                </td>
                <td style="font-weight: bold">
                    <asp:Label ID="lblSuperficie" runat="server" ForeColor="Black" Font-Names="Arial"
                        Font-Size="9pt" Width="100%"></asp:Label>
                </td>
                <td>
                    Data Dispon.
                </td>
                <td style="font-weight: bold">
                    <asp:Label ID="lblDataDisp" runat="server" ForeColor="Black" Font-Names="Arial" Font-Size="9pt"
                        Width="100%"></asp:Label>
                </td>
            </tr>
            <tr class="stileTesto2">
                <td>
                    Interno
                </td>
                <td style="font-weight: bold">
                    <asp:Label ID="lblInterno" runat="server" ForeColor="Black" Font-Names="Arial" Font-Size="9pt"
                        Width="100%"></asp:Label>
                </td>
                <td>
                    Piano
                </td>
                <td style="font-weight: bold">
                    <asp:Label ID="lblPiano" runat="server" ForeColor="Black" Font-Names="Arial" Font-Size="9pt"
                        Width="100%"></asp:Label>
                </td>
                <td>
                    Scala
                </td>
                <td style="font-weight: bold">
                    <asp:Label ID="lblScala" runat="server" ForeColor="Black" Font-Names="Arial" Font-Size="9pt"
                        Width="100%"></asp:Label>
                </td>
            </tr>
            <tr class="stileTesto2">
                <td>
                    Proprietà
                </td>
                <td style="font-weight: bold">
                    <asp:Label ID="lblProprieta" runat="server" ForeColor="Black" Font-Names="Arial"
                        Font-Size="9pt" Width="100%"></asp:Label>
                </td>
                <td>
                    Gestore
                </td>
                <td style="font-weight: bold">
                    <asp:Label ID="lblGestore" runat="server" ForeColor="Black" Font-Names="Arial" Font-Size="9pt"
                        Width="100%"></asp:Label>
                </td>
                <td>
                    &nbsp
                </td>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:HyperLink ID="HlinkCanone" runat="server" Style="" Font-Names="Arial" Font-Size="10pt"
                        Font-Italic="True" NavigateUrl="Canone.aspx" Font-Underline="True" Target="_blank">Clicca qui per visualizzare lo schema di calcolo del canone + oneri accessori</asp:HyperLink>
                </td>
            </tr>
        </table>--%>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td class="stileSezioni" colspan="6">ALLOGGI PROPOSTI
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <%--<asp:Menu ID="TStampe" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Black"
                                    Orientation="Horizontal" RenderingMode="Table" ToolTip="Elenco Stampe ">
                                    <DynamicHoverStyle BackColor="#C0FFC0" BorderWidth="1px" Font-Bold="True" ForeColor="#0000C0" />
                                    <DynamicMenuItemStyle BackColor="#E9F1F5" Height="20px" ItemSpacing="2px" BorderStyle="None"
                                        ForeColor="#0066FF" Width="220px" CssClass="giustificato" />
                                    <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                                        VerticalPadding="1px" />
                                    <Items>
                                        <asp:MenuItem ImageUrl="../NuoveImm/Img_Documentazione.png" Selectable="False" Value="">
                                        </asp:MenuItem>

                                    </Items>
                                </asp:Menu>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
            </table>
            <table style="width: 100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td style="width: 100%">
                        <div id="proposte" style="border: 1px solid blue; height: 110px; width: 100%; background-color: #E4E4E4; overflow: scroll;">
                            <asp:DataGrid ID="DataGridProposte" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BackColor="white" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black"
                                HorizontalAlign="Left" Width="100%" PageSize="8" GridLines="Horizontal">
                                <PagerStyle Mode="NumericPages" />
                                <HeaderStyle BackColor="#FFF7D7" ForeColor="#990000" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID_ALLOGGIO" HeaderText="ID ALLOGGIO" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="COD. ALLOGGIO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="COMUNE" HeaderText="COMUNE"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="indirizzoCompleto" HeaderText="INDIRIZZO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="NUM_LOCALI" HeaderText="N. LOCALI"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SUP" HeaderText="SUPERFICIE"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_PROPOSTA" HeaderText="DATA PRENOT."></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA" HeaderText="DATA ANNULLO/RIFIUTO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="MOTIVAZIONE_ALL" HeaderText="MOTIVAZIONE"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="ESITO" HeaderText="ESITO" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DIFFIDA2" HeaderText="FL_DIFFIDA2" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="STATO" HeaderText="STATO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Visualizza"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="VisualizzaCanone"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="ID_UNITA" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="PROPRIETA" Visible="False"></asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="buttonAnnProp" type="button" onclick="ScegliMotiviAnnull();" class="bottone"
                            value="ANNULLA PROPOSTA" />
                        <asp:Button ID="btnAnnullaProp" runat="server" BackColor="#EAEAEA" Font-Bold="True"
                            ForeColor="Blue" TabIndex="1" Text="ANNULLA PROPOSTA" Font-Names="Arial" Font-Size="8pt"
                            BorderWidth="0px" Width="0px" />
                    </td>
                    <td>
                        <asp:Button ID="btnNuovaProp" runat="server" BackColor="#EAEAEA" Font-Bold="True"
                            ForeColor="Blue" Height="32px" TabIndex="1" Text="NUOVA PROPOSTA" Width="160px"
                            Font-Names="Arial" Font-Size="8pt" OnClientClick="ApriElenco();" />
                    </td>
                    <td width="100%">&nbsp
                    </td>
                    <td align="right">
                        <table align="right">
                            <tr>
                                <td>
                                    <asp:Button ID="btnAccetta" runat="server" BackColor="#EAEAEA" Font-Bold="True" ForeColor="Blue"
                                        Height="32px" TabIndex="1" Text="ACCETTA OFFERTA" Width="160px" Font-Names="Arial"
                                        Font-Size="8pt" OnClientClick="ConfermaAccett();" />
                                </td>
                                <td>
                                    <input id="buttonRifiutaOff" type="button" onclick="ScegliMotiviRifiuto();" class="bottone"
                                        value="RIFIUTA OFFERTA" />
                                    <asp:Button ID="btnRifiuta" runat="server" BackColor="#EAEAEA" Font-Bold="True" ForeColor="Blue"
                                        Height="0px" TabIndex="1" Text="RIFIUTA OFFERTA" Width="0px" Font-Names="Arial"
                                        Font-Size="8pt" BorderWidth="0px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td class="stileSezioni" colspan="6">ALLOGGIO ASSEGNATO
                    </td>
                    <td align="right">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
            </table>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <div id="Div1" style="border: 1px solid blue; height: 110px; background-color: #E4E4E4; overflow: scroll; width: 100%;">
                            <asp:DataGrid ID="DataGridAssegnati" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BackColor="white" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black"
                                HorizontalAlign="Left" Width="100%" PageSize="8" GridLines="Horizontal">
                                <PagerStyle Mode="NumericPages" />
                                <HeaderStyle BackColor="#FFF7D7" ForeColor="#990000" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID_ALLOGGIO" HeaderText="ID ALLOGGIO" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID UNITA" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="COD. ALLOGGIO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="COMUNE" HeaderText="COMUNE"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="indirizzoCompleto" HeaderText="INDIRIZZO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="NUM_LOCALI" HeaderText="N. LOCALI"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SUP" HeaderText="SUPERFICIE"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_ACCETTAZIONE" HeaderText="DATA ACCETTAZIONE"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Visualizza"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="VisualizzaCanone"></asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>&nbsp
                    </td>
                    <td>&nbsp
                    </td>
                    <td align="center">
                        <asp:Label ID="lblAvviso" runat="server" BackColor="Yellow" Font-Bold="True" Font-Names="ARIAL"
                            Font-Size="10pt" ForeColor="#004FC6"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="buttonAnnAss" type="button" onclick="ScegliMotiviAnnAssegn();" class="bottone"
                            value="ANNULLA ASSEGNAZIONE" />
                        <asp:Button ID="btnAnnullaAss" runat="server" BackColor="#EAEAEA" Font-Bold="True"
                            ForeColor="Blue" Height="0px" TabIndex="1" Text="ANNULLA ASSEGNAZIONE" Width="0px"
                            Font-Names="Arial" Font-Size="8pt" BorderWidth="0px" />
                    </td>
                    <td>
                        <input id="buttonConfAss" type="button" onclick="ConfermaAssegnazione();" class="bottone"
                            value="CONFERMA ASSEGNAZIONE" />
                        <asp:Button ID="btnConfAss" runat="server" BackColor="#EAEAEA" Font-Bold="True" ForeColor="Blue"
                            Height="0px" TabIndex="1" Text="CONFERMA ASSEGNAZIONE" Width="0px" Font-Names="Arial"
                            Font-Size="8pt" BorderWidth="0px" />
                    </td>
                    <td width="100%">&nbsp
                    </td>
                    <td align="right">
                        <table align="right">
                            <tr>
                                <td>&nbsp
                                </td>
                                <td>
                                    <input id="buttonRevoca" type="button" onclick="ScegliMotiviRevoca();" class="bottone"
                                        value="REVOCA OFFERTA" />
                                    <asp:Button ID="btnRevoca" runat="server" BackColor="#EAEAEA" Font-Bold="True" ForeColor="Blue"
                                        Height="0px" TabIndex="1" Text="REVOCA OFFERTA" Width="0px" Font-Names="Arial"
                                        Font-Size="8pt" BorderWidth="0px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table align="right">
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnHome" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
                            OnClientClick="ConfermaEsci(); return false;" />
                    </td>
                    <td>
                        <asp:ImageButton ID="btnAggiornaPagina" runat="server" Style="visibility: hidden;" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="tipoAll" runat="server" Value="0" />
        <asp:HiddenField ID="tipoDomanda" runat="server" Value="0" />
        <asp:HiddenField ID="idDomanda" runat="server" Value="0" />
        <asp:HiddenField ID="idDichiarazione" runat="server" Value="0" />
        <asp:HiddenField ID="idAlloggio" runat="server" Value="0" />
        <asp:HiddenField ID="numOfferta" runat="server" Value="0" />
        <asp:HiddenField ID="invito" runat="server" Value="0" />
        <asp:HiddenField ID="motivazione" runat="server" Value="-1" />
        <asp:HiddenField ID="confermaAnnullo1" runat="server" Value="0" />
        <asp:HiddenField ID="RifiutoNote" runat="server" Value="0" />
        <asp:HiddenField ID="numProvv" runat="server" Value="0" />
        <asp:HiddenField ID="dataProvv" runat="server" Value="0" />
        <asp:HiddenField ID="proposti" runat="server" Value="0" />
        <asp:HiddenField ID="assegnati" runat="server" Value="0" />
        <asp:HiddenField ID="idAnnullo" runat="server" />
        <asp:HiddenField ID="diffida" runat="server" Value="0" />
        <asp:HiddenField ID="frmModify" runat="server" Value="0" />
        <asp:HiddenField ID="dataRevoca" runat="server" Value="0" />
        <asp:HiddenField ID="revocato" runat="server" Value="0" />
        <asp:HiddenField ID="strRadioButton1" runat="server" Value="0" />
        <asp:HiddenField ID="strRadioButton2" runat="server" Value="0" />
        <asp:HiddenField ID="idUnitaValida" runat="server" Value="0" />
        <asp:HiddenField ID="fl_proprieta" runat="server" Value="0" />
        <asp:HiddenField ID="tipoDomanda2" runat="server" Value="0" />
    </form>
</body>
<script type="text/javascript" language="javascript">

    document.getElementById('dvvvPre').style.visibility = 'hidden';

    function ConfermaEsci() {

        if (document.getElementById('frmModify').value == '1') {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
            if (chiediConferma == true) {
                self.close();
            }
        }
        else {
            self.close();
        }
    }



    document.getElementById('btnAnnullaProp').style.visibility = 'hidden';
    document.getElementById('btnAnnullaProp').style.position = 'absolute';
    document.getElementById('btnAnnullaProp').style.left = '-100px';
    document.getElementById('btnAnnullaProp').style.display = 'none';

    document.getElementById('btnRifiuta').style.visibility = 'hidden';
    document.getElementById('btnRifiuta').style.position = 'absolute';
    document.getElementById('btnRifiuta').style.left = '-100px';
    document.getElementById('btnRifiuta').style.display = 'none';

    document.getElementById('btnConfAss').style.visibility = 'hidden';
    document.getElementById('btnConfAss').style.position = 'absolute';
    document.getElementById('btnConfAss').style.left = '-100px';
    document.getElementById('btnConfAss').style.display = 'none';

    document.getElementById('btnAnnullaAss').style.visibility = 'hidden';
    document.getElementById('btnAnnullaAss').style.position = 'absolute';
    document.getElementById('btnAnnullaAss').style.left = '-100px';
    document.getElementById('btnAnnullaAss').style.display = 'none';

    document.getElementById('btnRevoca').style.visibility = 'hidden';
    document.getElementById('btnRevoca').style.position = 'absolute';
    document.getElementById('btnRevoca').style.left = '-100px';
    document.getElementById('btnRevoca').style.display = 'none';


    if (document.getElementById('assegnati').value == '0') {
        document.getElementById('buttonConfAss').disabled = "disabled";
        document.getElementById('buttonAnnAss').disabled = "disabled";
    } else {
        document.getElementById('buttonConfAss').disabled = "";
        document.getElementById('buttonAnnAss').disabled = "";
        document.getElementById('buttonAnnProp').disabled = "disabled";
        document.getElementById('buttonRifiutaOff').disabled = "disabled";
    }
    if (document.getElementById('proposti').value == '0' && document.getElementById('assegnati').value == '0') {
        document.getElementById('buttonRevoca').disabled = "disabled";
    }
    if (document.getElementById('proposti').value == '0') {
        document.getElementById('buttonAnnProp').disabled = "disabled";
        document.getElementById('buttonRifiutaOff').disabled = "disabled";
        document.getElementById('buttonConfAss').disabled = "disabled";

    }
    if (document.getElementById('proposti').value == '1' && document.getElementById('assegnati').value == '0') {
        document.getElementById('buttonAnnProp').disabled = "";
        document.getElementById('buttonRifiutaOff').disabled = "";

    }
    if (document.getElementById('proposti').value == '0' && document.getElementById('assegnati').value == '1') {
        document.getElementById('buttonConfAss').disabled = "";
        document.getElementById('buttonAnnAss').disabled = "";
        document.getElementById('buttonAnnProp').disabled = "disabled";
        document.getElementById('buttonRifiutaOff').disabled = "disabled";
    }


    if (document.getElementById('proposti').value == '3' && document.getElementById('assegnati').value == '0') {
        document.getElementById('buttonConfAss').disabled = "disabled";
        document.getElementById('buttonAnnAss').disabled = "disabled";
        document.getElementById('buttonAnnProp').disabled = "disabled";
        document.getElementById('buttonRifiutaOff').disabled = "disabled";
        document.getElementById('buttonRevoca').disabled = "disabled";
    }

    if (document.getElementById('revocato').value == '1') {
        document.getElementById('buttonConfAss').disabled = "disabled";
        document.getElementById('buttonAnnAss').disabled = "disabled";
        document.getElementById('buttonAnnProp').disabled = "disabled";
        document.getElementById('buttonRifiutaOff').disabled = "disabled";
        document.getElementById('buttonRevoca').disabled = "disabled";

    }

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


    function ApriElenco() {

        if (document.getElementById('frmModify').value != '1') {
            window.showModalDialog('RicercaUIDaAbbinare.aspx?PROV=1&TIPO=' + document.getElementById('tipoDomanda').value + '&ID=' + document.getElementById('idDomanda').value, window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');
        } else {
            alert('Salvare le modifiche effettuate!');
        }
    }


    function ConfermaAccett() {

        var sicuro = window.confirm('Sei sicuro di voler accettare l\'offerta proposta?');

        if (sicuro == true) {
            document.getElementById('confermaAnnullo1').value = "1";
        }
        else {
            document.getElementById('confermaAnnullo1').value = "0";
        }

    }


    function ScegliMotiviAnnull() {



        var txt = "ANNULLAMENTO PROPOSTA MOTIVATO DA:<br /><br />" + document.getElementById('strRadioButton1').value.replace(/@/g, '<').replace(/#/g, '>');
        //  document.getElementById('strRadioButton1').value.replace(/#/g, '>');

        // var txt = "<input id='Radio1' type='radio' name='rdbMotivi' value='2'/>OCCUPAZIONE ABUSIVA<br /><input id='Radio1' type='radio' name='rdbMotivi' value='3'/>ALTRO<br /><input id='Radio1' type='radio' name='rdbMotivi' value='5'/>ALLOGGIO INAGIBILE O NON IDONEO<br /><input id='Radio1' type='radio' name='rdbMotivi' value='6'/>CANONE NON SOPPORTABILE<br /><input id='Radio1' type='radio' name='rdbMotivi' value='8'/>ERRORE UFFICIO<br /><input id='Radio1' type='radio' name='rdbMotivi' value='9'/>BARRIERE ARCHITETTONICHE<br />";
        //var txt = 'ANNULLAMENTO PROPOSTA MOTIVATO DA:<br /><br /><input type="radio" name="rdbMotivi" value="OCCUPAZIONE ABUSIVA"/>OCCUPAZIONE ABUSIVA<br /><input type="radio" name="rdbMotivi" value="ALTRO"/>ALTRO<br /><input type="radio" name="rdbMotivi" value="ALLOGGIO INAGIBILE O NON IDONEO"/>ALLOGGIO INAGIBILE O NON IDONEO<br /><input type="radio" name="rdbMotivi" value="CANONE NON SOPPORTABILE"/>CANONE NON SOPPORTABILE<br /><input type="radio" name="rdbMotivi" value="ERRORE UFFICIO"/>ERRORE UFFICIO<br /><input type="radio" name="rdbMotivi" value="BARRIERE ARCHITETTONICHE"/>BARRIERE ARCHITETTONICHE';
        jQuery.prompt(txt, {
            submit: mycallScegliMotiviAnnull,
            buttons: { Procedi: '1', Annulla: '2' },
            show: 'slideDown',
            focus: 0
        });
    }

    function mycallScegliMotiviAnnull(e, v, m, f) {
        if (v != undefined)

            if (v != '2') {

                if (f.rdbMotivi != undefined) {

                    document.getElementById('motivazione').value = f.rdbMotivi;

                    var sicuro = window.confirm('Sei sicuro di voler annullare la proposta di abbinamento?');

                    if (sicuro == true) {
                        document.getElementById('confermaAnnullo1').value = "1";
                        document.getElementById('btnAnnullaProp').click();
                        return true;
                    }
                    else {
                        document.getElementById('confermaAnnullo1').value = "0";
                    }
                }
                else {
                    alert('Selezionare la motivazione!');
                    return false;
                }
                return true;
            }
    }



    function ScegliMotiviRifiuto() {

        var txt = "SELEZIONARE IL MOTIVO DI RIFIUTO OPPURE COMPILARE IL CAMPO NOTE:<br /><br />" + document.getElementById('strRadioButton2').value.replace(/@/g, '<').replace(/#/g, '>') + "<br />Note:<br /><textarea id='txtNote' name='txtNote' cols='25' rows='3'></textarea>";
        jQuery.prompt(txt, {
            submit: mycallScegliMotiviRifiuto,
            buttons: { Procedi: '1', Annulla: '2' },
            show: 'slideDown',
            focus: 0
        });

    }

    function mycallScegliMotiviRifiuto(e, v, m, f) {
        if (v != undefined)

            if (v != '2') {

                if (f.rdbMotivi != undefined || f.txtNote != undefined) {

                    document.getElementById('motivazione').value = f.rdbMotivi;
                    if (f.txtNote == undefined) {
                        document.getElementById('RifiutoNote').value = '';
                    }
                    else {
                        document.getElementById('RifiutoNote').value = f.txtNote;
                    }

                    var sicuro = window.confirm('Sei sicuro di voler rifiutare l\'offerta proposta?');

                    if (sicuro == true) {
                        document.getElementById('confermaAnnullo1').value = "1";
                        document.getElementById('btnRifiuta').click();
                        return true;
                    }
                    else {
                        document.getElementById('confermaAnnullo1').value = "0";
                    }
                }
                else if (f.txtNote == undefined) {
                    alert('Selezionare una motivazione o compilare il campo note!');
                    return false;

                }

                return true;
            }
    }


    //REVOCA
    function ScegliMotiviRevoca() {
        var txt = "SELEZIONARE IL MOTIVO DELLA REVOCA OPPURE COMPILARE IL CAMPO NOTE:<br /><br />" + document.getElementById('strRadioButton2').value.replace(/@/g, '<').replace(/#/g, '>') + "<br />Note:<br /><textarea id='txtNote' name='txtNote' cols='25' rows='3'></textarea><br /><br />Data revoca:<br /><input type='text' id='txtDataRevoca' name='txtDataRevoca' onkeypress='CompletaData(event,this);'/>";
        jQuery.prompt(txt, {
            submit: mycallScegliMotiviRevoca,
            buttons: { Procedi: '1', Annulla: '2' },
            show: 'slideDown',
            focus: 0
        });

    }

    function mycallScegliMotiviRevoca(e, v, m, f) {
        if (v != undefined)

            if (v != '2') {

                if (f.rdbMotivi != undefined || f.txtNote != undefined) {

                    document.getElementById('motivazione').value = f.rdbMotivi;
                    if (f.txtNote == undefined) {
                        document.getElementById('RifiutoNote').value = '';
                    }
                    else {
                        document.getElementById('RifiutoNote').value = f.txtNote;
                    }
                    if (f.txtDataRevoca == '') {
                        alert('Inserire la data!');
                        return false;
                    }

                    var sicuro = window.confirm('Sei sicuro di voler revocare l\'offerta proposta?');

                    if (sicuro == true) {

                        document.getElementById('confermaAnnullo1').value = "1";
                        document.getElementById('dataRevoca').value = f.txtDataRevoca;
                        document.getElementById('btnRevoca').click();
                        return true;
                    }
                    else {
                        document.getElementById('confermaAnnullo1').value = "0";
                    }
                }
                else if (f.txtNote == undefined) {
                    alert('Selezionare una motivazione o compilare il campo note!');
                    return false;

                }

                return true;
            }
    }


    function ConfermaAssegnazione() {
        var txt;
        if (document.getElementById('numProvv').value != '0' && document.getElementById('dataProvv').value != '0') {
            var numProvv = document.getElementById('numProvv').value;
            var dataProvv = document.getElementById('dataProvv').value;
            txt = 'MODIFICARE GLI ESTREMI DEL PROVVEDIMENTO:&nbsp&nbsp&nbsp&nbsp<br /><br />N.ro provvedimento:<br /><input type="text" id="txtNumProvv" name="txtNumProvv" value="' + numProvv + '" /><br />Data provvedimento:<br /><input type="text" id="txtDataProvv" name="txtDataProvv" value="' + dataProvv + '" onkeypress="CompletaData(event,this);"/>';
        }
        else {
            txt = 'INSERIRE GLI ESTREMI DEL PROVVEDIMENTO:&nbsp&nbsp&nbsp&nbsp<br /><br />N.ro provvedimento:<br /><input type="text" id="txtNumProvv" name="txtNumProvv" /><br />Data provvedimento:<br /><input type="text" id="txtDataProvv" name="txtDataProvv" onkeypress="CompletaData(event,this);"/>';
        }
        jQuery.prompt(txt, {
            submit: mycallConfermaAssegnazione,
            buttons: { Procedi: '1', Annulla: '2' },
            show: 'slideDown',
            focus: 0
        });

    }

    function mycallConfermaAssegnazione(e, v, m, f) {
        if (v != undefined)

            if (v != '2') {

                if (f.txtNumProvv != '' && f.txtDataProvv != '') {


                    document.getElementById('numProvv').value = f.txtNumProvv;
                    document.getElementById('dataProvv').value = f.txtDataProvv;

                    var sicuro = window.confirm('Sei sicuro di voler accettare l\'assegnazione?');

                    if (sicuro == true) {
                        document.getElementById('confermaAnnullo1').value = "1";
                        document.getElementById('btnConfAss').click();
                    }
                    else {
                        document.getElementById('confermaAnnullo1').value = "0";
                    }
                }
                else {
                    alert('Compilare i campi!');
                    return false;
                }

                return true;
            }
    }


    function ScegliMotiviAnnAssegn() {
        var txt = "ANNULLAMENTO ASSEGNAZIONE MOTIVATO DA:<br /><br />" + document.getElementById('strRadioButton1').value.replace(/@/g, '<').replace(/#/g, '>');
        jQuery.prompt(txt, {
            submit: mycallScegliMotiviAnnAssegn,
            buttons: { Procedi: '1', Annulla: '2' },
            show: 'slideDown',
            focus: 0
        });
    }

    function mycallScegliMotiviAnnAssegn(e, v, m, f) {
        if (v != undefined)

            if (v != '2') {

                if (f.rdbMotivi != undefined) {

                    //document.getElementById('idAnnullo').value = f.rdbMotivi.substring(0, 1);
                    document.getElementById('motivazione').value = f.rdbMotivi;

                    var sicuro = window.confirm('Sei sicuro di voler annullare l\'assegnazione?');

                    if (sicuro == true) {
                        document.getElementById('confermaAnnullo1').value = "1";
                        document.getElementById('btnAnnullaAss').click();
                    }
                    else {
                        document.getElementById('confermaAnnullo1').value = "0";
                    }
                }
                else {
                    alert('Selezionare la motivazione!');
                    return false;
                }
                return true;
            }
    }


    function RistampaOfferta() {
        window.open('ReportAbbinamento.aspx?ABB=' + document.getElementById('numOfferta').value + '&IDALL=' + document.getElementById('idAlloggio').value, '');
    }

    function TestoInvito() {

        window.open('Doc_Offerta_Alloggio/Doc_TestoTelegramma.aspx?IDDOM=' + document.getElementById('idDomanda').value + '&IDOFF=' + document.getElementById('numOfferta').value + '&T=' + document.getElementById('tipoDomanda').value, '');
    }

    function PermessoVisita() {

        window.open('Doc_Offerta_Alloggio/Doc_PermessoVisita.aspx?IDDOM=' + document.getElementById('idDomanda').value + '&IDOFF=' + document.getElementById('numOfferta').value + '&T=' + document.getElementById('tipoDomanda').value, '');
    }

    function AccettAlloggio() {

        window.open('Doc_Offerta_Alloggio/Doc_AutocertAbbinam.aspx?IDDOM=' + document.getElementById('idDomanda').value + '&IDOFF=' + document.getElementById('numOfferta').value + '&T=' + document.getElementById('tipoDomanda').value, '');
    }

    function Diffida1Alloggio() {

        window.open('Doc_Offerta_Alloggio/Doc_OffertaAllDiffida.aspx?IDDOM=' + document.getElementById('idDomanda').value + '&IDOFF=' + document.getElementById('numOfferta').value + '&T=' + document.getElementById('tipoDomanda').value, '');
    }

    function Diffida2Alloggio() {

        window.open('Doc_Offerta_Alloggio/Doc_OffertaAllDiffidaAccet.aspx?IDDOM=' + document.getElementById('idDomanda').value + '&IDOFF=' + document.getElementById('numOfferta').value + '&T=' + document.getElementById('tipoDomanda').value, '');
    }

    function OffertaAlloggio() {

        window.open('Doc_Offerta_Alloggio/Doc_OffertaAlloggio.aspx?IDDOM=' + document.getElementById('idDomanda').value + '&IDOFF=' + document.getElementById('numOfferta').value + '&T=' + document.getElementById('tipoDomanda').value, '');
    }

    function RappSintetico() {

        window.open('Doc_Offerta_Alloggio/Doc_RapportoOffertaAll.aspx?IDDOM=' + document.getElementById('idDomanda').value + '&IDOFF=' + document.getElementById('numOfferta').value + '&T=' + document.getElementById('tipoDomanda').value, '');
    }

    function ElencoStampe() {

        window.open('Doc_Offerta_Alloggio/ElencoStampe.aspx?IDDOM=' + document.getElementById('idDomanda').value, '');
    }

    function CreaDichiarazione() {
        //var chiediConferma
        //chiediConferma = window.confirm("Attenzione...Sei sicuro di voler aggiornare i dati della dichiarazione?");
        //if (chiediConferma == true) {

        window.open('../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=-1&P=' + document.getElementById('fl_proprieta').value + '&CODU=' + document.getElementById('idUnitaValida').value + '&IDD=' + document.getElementById('idDomanda').value + '&T=' + document.getElementById('tipoDomanda').value, 'DichNuovoRU', 'top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');
        //}
        //else {

        //    alert('Operazione annullata!');
        //}
    }

</script>
</html>
