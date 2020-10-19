<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiMultiselezione2.aspx.vb" Inherits="MOROSITA_RisultatiMultiselezione2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
            width="100%">
            <tr>
                <td style="height: 45px; color: #FFFFFF; font-size: 18.0pt; font-weight: 700; font-style: normal;
                    text-decoration: none; font-family: Lucida Sans, sans-serif; text-align: center;
                    vertical-align: middle; white-space: normal; border-left: 1.0pt solid windowtext;
                    border-right-style: none; border-right-color: inherit; border-right-width: medium;
                    border-top: 1.0pt solid windowtext; border-bottom-style: none; border-bottom-color: inherit;
                    border-bottom-width: medium; padding-left: 1px; padding-right: 1px; padding-top: 1px;
                    background: #507CD1;">
                    Analisi statistica del credito
                </td>
            </tr>
            <tr style="height: 17.25pt">
                <td style="text-align: right">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 50%; text-align: left">
                                <asp:ImageButton ID="btnIndietro" runat="server" ImageUrl="../NuoveImm/Img_NuovaRicerca.png"
                                    AlternateText="Indietro" ToolTip="Indietro" />
                                <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="../NuoveImm/Img_Stampa_Grande.png"
                                    AlternateText="Stampa" ToolTip="Stampa" />
                            </td>
                            <td style="width: 50%; text-align: right">
                                <asp:ImageButton ID="btnExcel" runat="server" Height="32px" ImageUrl="Immagini/excel.jpg"
                                    Width="32px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Panel ID="PanelTotale" runat="server">
            <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                width="100%">
                <tr style="height: 17.25pt">
                    <td style="height: 17.25pt; width: 100%; color: #262626; font-size: 11.0pt; font-weight: 700;
                        font-style: italic; text-decoration: none; font-family: Book Antiqua , serif;
                        text-align: left; vertical-align: bottom; white-space: normal; border-left: 1.0pt solid windowtext;
                        border-right-style: none; border-right-color: inherit; border-right-width: medium;
                        border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 1px;
                        padding-right: 1px; padding-top: 1px; background: #D9D9D9;">
                        Riepilogo dei filtri di estrazione inseriti per ottenere il risultato (solo filtri
                        valorizzati)<br />
                        Situazione alla data
                        <asp:Label ID="LabelDataAggiornamento" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Label ID="parametriDiRicerca" Text="" runat="server" />
                    </td>
                </tr>
                <tr style="height: 17.25pt">
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panelmorosita3mesi" runat="server">
                <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                    width="100%">
                    <tr>
                        <td colspan="8" style="height: 20.1pt; color: #ffffff; font-size: 15.0pt; font-weight: 700;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: bottom; white-space: nowrap; border-left: 1.0pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #507CD1;">
                            Morosità inferiore a 3 mesi
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" style="height: 20.1pt; color: #ffffff; font-size: 12.0pt; font-weight: 700;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: bottom; white-space: nowrap; border-left: 1.0pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #507CD1;">
                            Assegnatari a debito
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td colspan="4" height="176" rowspan="3" style="height: 132.0pt; color: black; font-size: 11.0pt;
                            font-weight: 400; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top-style: none; border-top-color: inherit; border-top-width: medium;
                            border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: center; width: 50%; height: 70px">
                                        N° assegnatari morosi
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; width: 50%; height: 70px">
                                        <asp:Label ID="NAssegnatari3Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                            border-width: 0px; text-align: center;" Font-Names="Arial" Font-Size="10pt" Height="18px">0</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="4" style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: center;
                            vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top-style: none; border-top-color: inherit; border-top-width: medium;
                            border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        Morosità totale
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        <asp:Label ID="ImportoMorositaTotale3Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td colspan="2" height="110" rowspan="2" style="height: 82.5pt; color: black; font-size: 11.0pt;
                            font-weight: 400; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="height: 40pt; text-align: center;">
                                        Morosità ex gestori
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 40pt; text-align: center;">
                                        <asp:Label ID="ImportoMorositaExGestori3Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2" style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: left;
                            vertical-align: middle; white-space: normal; padding-left: 1px; padding-right: 1px;
                            padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        Morosità Gestore
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        <asp:Label ID="ImportoMorositaALER3Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td height="66" style="height: 49.5pt; color: black; font-size: 11.0pt; font-weight: 400;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-style: none;
                            border-color: inherit; border-width: medium; padding-left: 1px; padding-right: 1px;
                            padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%; height: 25pt; text-align: center">
                                        Canoni
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 24pt; text-align: center;">
                                        <asp:Label ID="ImportoMorositaCanoni3Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt" Height="18px">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: center;
                            vertical-align: middle; white-space: normal; border-style: none; border-color: inherit;
                            border-width: medium; padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%; height: 25pt; text-align: center">
                                        Servizi
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 24pt; text-align: center;">
                                        <asp:Label ID="ImportoMorositaServizi3Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height: 16.5pt; color: black; font-size: 11.0pt;
                            font-weight: 400; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: .5pt solid windowtext; border-bottom: .5pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #EAE7ED;">
                            Dettaglio assegnatari morosi
                        </td>
                        <td style="text-align: center; color: black; font-size: 11.0pt; font-weight: 400;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: .5pt solid windowtext; border-bottom: .5pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #EAE7ED;" colspan="4">
                            Dettaglio morosità
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 12.5%">
                            N° assegnatari a cui è stata inviata M.M.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            N° Assegnatari con richiesta di Contributo Solidarietà in essere
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            N° Assegnatari con Pratiche Legali in essere
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            N° altri assegnatari
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità assegnatari con M.M.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità assegnatari con richiesta di C.S.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità assegnatari con P.L.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità altri assegnatari
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariMM3Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariCS3Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariPL3Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariAltri3Mesi" runat="server" Style="border-style: none;
                                border-color: inherit; border-width: 0px; text-align: center">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaMM3Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0,00</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaCS3Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0,00</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaPL3Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0,00</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaAltri3Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0,00</asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 17.25pt">
                        <td colspan="8">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="panelinquilini3mesi" runat="server">
                <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                    width="100%">
                    <tr>
                        <td style="text-align: center; height: 16.5pt; color: black; font-size: 11.0pt; font-weight: 400;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: .5pt solid windowtext; border-bottom: .5pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #EAE7ED;">
                            Dettaglio inquilini
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="overflow: auto">
                                <asp:DataGrid runat="server" ID="DataGridInquilini3Mesi" AutoGenerateColumns="False"
                                    CellPadding="1" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None"
                                    CellSpacing="2" Width="100%" PageSize="300" Style="border: 1px solid #507CD1;">
                                    <AlternatingItemStyle BackColor="#F9F9F9" ForeColor="#000000" />
                                    <Columns>
                                        <asp:BoundColumn HeaderText="Codice contrattuale" DataField="CODICE_CONTRATTO">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Intestatario" DataField="INTESTATARIO2"></asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Debito" DataField="DEBITO2" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Tipologia rapporto" DataField="COD_TIPOLOGIA_CONTR_LOC">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Posizione contrattuale" DataField="POSIZIONE_CONTRATTO">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Unità immobiliare" DataField="COD_UNITA_IMMOBILIARE">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Tipo UI" DataField="COD_TIPOLOGIA">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Indirizzo completo" DataField="INDIRIZZO"></asp:BoundColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                                        HorizontalAlign="Center" />
                                    <ItemStyle BackColor="#F9F9F9" ForeColor="#000000" />
                                    <PagerStyle BackColor="White" ForeColor="#507CD1" HorizontalAlign="Left" Mode="NumericPages"
                                        Position="Top" />
                                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                                </asp:DataGrid>
                            </div>
                        </td>
                    </tr>
                    <tr style="height: 17.25pt">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="panelmorosita312mesi" runat="server">
                <p style='page-break-after: always'>
                    &nbsp;</p>
                <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                    width="100%">
                    <tr>
                        <td colspan="8" style="height: 20.1pt; color: #ffffff; font-size: 15.0pt; font-weight: 700;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: bottom; white-space: nowrap; border-left: 1.0pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #507CD1;">
                            Morosità da a 3 a 12 mesi
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" style="height: 20.1pt; color: #ffffff; font-size: 12.0pt; font-weight: 700;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: bottom; white-space: nowrap; border-left: 1.0pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #507CD1;">
                            Assegnatari a debito
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td colspan="4" height="176" rowspan="3" style="height: 132.0pt; color: black; font-size: 11.0pt;
                            font-weight: 400; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top-style: none; border-top-color: inherit; border-top-width: medium;
                            border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: center; width: 50%; height: 70px">
                                        N° assegnatari morosi
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; width: 50%; height: 70px">
                                        <asp:Label ID="NAssegnatari312Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                            border-width: 0px; text-align: center;" Font-Names="Arial" Font-Size="10pt" Height="18px">0</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="4" style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: center;
                            vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top-style: none; border-top-color: inherit; border-top-width: medium;
                            border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        Morosità totale
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        <asp:Label ID="ImportoMorositaTotale312Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td colspan="2" height="110" rowspan="2" style="height: 82.5pt; color: black; font-size: 11.0pt;
                            font-weight: 400; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="height: 40pt; text-align: center;">
                                        Morosità ex gestori
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 40pt; text-align: center;">
                                        <asp:Label ID="ImportoMorositaExGestori312Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2" style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: left;
                            vertical-align: middle; white-space: normal; padding-left: 1px; padding-right: 1px;
                            padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        Morosità Gestore
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        <asp:Label ID="ImportoMorositaALER312Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td height="66" style="height: 49.5pt; color: black; font-size: 11.0pt; font-weight: 400;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-style: none;
                            border-color: inherit; border-width: medium; padding-left: 1px; padding-right: 1px;
                            padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%; height: 25pt; text-align: center">
                                        Canoni
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 24pt; text-align: center">
                                        <asp:Label ID="ImportoMorositaCanoni312Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt" Height="18px">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: center;
                            vertical-align: middle; white-space: normal; border-style: none; border-color: inherit;
                            border-width: medium; padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%; height: 25pt; text-align: center">
                                        Servizi
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 24pt; text-align: center">
                                        <asp:Label ID="ImportoMorositaServizi312Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height: 16.5pt; color: black; font-size: 11.0pt;
                            font-weight: 400; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: .5pt solid windowtext; border-bottom: .5pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #EAE7ED;">
                            Dettaglio assegnatari morosi
                        </td>
                        <td style="text-align: center; color: black; font-size: 11.0pt; font-weight: 400;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: .5pt solid windowtext; border-bottom: .5pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #EAE7ED;" colspan="4">
                            Dettaglio morosità
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 12.5%">
                            N° assegnatari a cui è stata inviata M.M.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            N° Assegnatari con richiesta di Contributo Solidarietà in essere
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            N° Assegnatari con Pratiche Legali in essere
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            N° altri assegnatari
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità assegnatari con M.M.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità assegnatari con richiesta di C.S.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità assegnatari con P.L.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità altri assegnatari
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariMM312Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariCS312Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariPL312Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariAltri312Mesi" runat="server" Style="border-style: none;
                                border-color: inherit; border-width: 0px; text-align: center">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaMM312Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0,00</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaCS312Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0,00</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaPL312Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0,00</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaAltri312Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0,00</asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 17.25pt">
                        <td colspan="8">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="panelinquilini312mesi" runat="server">
                <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                    width="100%">
                    <tr>
                        <td style="text-align: center; height: 16.5pt; color: black; font-size: 11.0pt; font-weight: 400;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: .5pt solid windowtext; border-bottom: .5pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #EAE7ED;">
                            Dettaglio inquilini
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="overflow: auto">
                                <asp:DataGrid runat="server" ID="DataGridInquilini312Mesi" AutoGenerateColumns="False"
                                    CellPadding="1" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None"
                                    CellSpacing="1" Width="100%" AllowPaging="False" PageSize="300" Style="border: 1px solid #507CD1;">
                                    <AlternatingItemStyle BackColor="#F9F9F9" ForeColor="#000000" />
                                    <Columns>
                                        <asp:BoundColumn HeaderText="Codice contrattuale" DataField="CODICE_CONTRATTO"></asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Intestatario" DataField="INTESTATARIO2"></asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Debito" DataField="DEBITO2" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Tipologia rapporto" DataField="COD_TIPOLOGIA_CONTR_LOC">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Posizione contrattuale" DataField="POSIZIONE_CONTRATTO">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Unità immobiliare" DataField="COD_UNITA_IMMOBILIARE">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Tipo UI" DataField="COD_TIPOLOGIA"></asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Indirizzo completo" DataField="INDIRIZZO"></asp:BoundColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                                        HorizontalAlign="Center" />
                                    <ItemStyle BackColor="#F9F9F9" ForeColor="#000000" />
                                    <PagerStyle BackColor="White" ForeColor="#507CD1" HorizontalAlign="Left" Mode="NumericPages"
                                        Position="Top" />
                                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                                </asp:DataGrid>
                            </div>
                        </td>
                    </tr>
                    <tr style="height: 17.25pt">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="panelmorosita12mesi">
                <p style='page-break-after: always'>
                    &nbsp;</p>
                <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                    width="100%">
                    <tr>
                        <td colspan="8" style="height: 20.1pt; color: #ffffff; font-size: 15.0pt; font-weight: 700;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: bottom; white-space: nowrap; border-left: 1.0pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #507CD1;">
                            Morosità superiore a 12 mesi
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" style="height: 20.1pt; color: #ffffff; font-size: 12.0pt; font-weight: 700;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: bottom; white-space: nowrap; border-left: 1.0pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #507CD1;">
                            Assegnatari a debito
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td colspan="4" height="176" rowspan="3" style="height: 132.0pt; color: black; font-size: 11.0pt;
                            font-weight: 400; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top-style: none; border-top-color: inherit; border-top-width: medium;
                            border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: center; width: 50%; height: 70px">
                                        N° assegnatari morosi
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; width: 50%; height: 70px">
                                        <asp:Label ID="NAssegnatari12Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                            border-width: 0px; text-align: center;" Font-Names="Arial" Font-Size="10pt" Height="18px">0</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="4" style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: center;
                            vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top-style: none; border-top-color: inherit; border-top-width: medium;
                            border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        Morosità totale
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        <asp:Label ID="ImportoMorositaTotale12Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td colspan="2" height="110" rowspan="2" style="height: 82.5pt; color: black; font-size: 11.0pt;
                            font-weight: 400; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="height: 40pt; text-align: center;">
                                        Morosità ex gestori
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 40pt; text-align: center;">
                                        <asp:Label ID="ImportoMorositaExGestori12Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2" style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: left;
                            vertical-align: middle; white-space: normal; padding-left: 1px; padding-right: 1px;
                            padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        Morosità Gestore
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        <asp:Label ID="ImportoMorositaALER12Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td height="66" style="height: 49.5pt; color: black; font-size: 11.0pt; font-weight: 400;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-style: none;
                            border-color: inherit; border-width: medium; padding-left: 1px; padding-right: 1px;
                            padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%; height: 25pt; text-align: center">
                                        Canoni
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 24pt; text-align: center">
                                        <asp:Label ID="ImportoMorositaCanoni12Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt" Height="18px">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: center;
                            vertical-align: middle; white-space: normal; border-style: none; border-color: inherit;
                            border-width: medium; padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%; height: 25pt; text-align: center">
                                        Servizi
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 24pt; text-align: center">
                                        <asp:Label ID="ImportoMorositaServizi12Mesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height: 16.5pt; color: black; font-size: 11.0pt;
                            font-weight: 400; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: .5pt solid windowtext; border-bottom: .5pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #EAE7ED;">
                            Dettaglio assegnatari morosi
                        </td>
                        <td style="text-align: center; color: black; font-size: 11.0pt; font-weight: 400;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: .5pt solid windowtext; border-bottom: .5pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #EAE7ED;" colspan="4">
                            Dettaglio morosità
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 12.5%">
                            N° assegnatari a cui è stata inviata M.M.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            N° Assegnatari con richiesta di Contributo Solidarietà in essere
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            N° Assegnatari con Pratiche Legali in essere
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            N° altri assegnatari
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità assegnatari con M.M.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità assegnatari con richiesta di C.S.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità assegnatari con P.L.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità altri assegnatari
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariMM12Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariCS12Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariPL12Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariAltri12Mesi" runat="server" Style="border-style: none;
                                border-color: inherit; border-width: 0px; text-align: center">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaMM12Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0,00</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaCS12Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0,00</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaPL12Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0,00</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaAltri12Mesi" runat="server" Style="border-style: none; border-color: inherit;
                                border-width: 0px; text-align: center">0,00</asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 17.25pt">
                        <td colspan="8">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="panelinquilini12mesi">
                <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                    width="100%">
                    <tr>
                        <td style="text-align: center; height: 16.5pt; color: black; font-size: 11.0pt; font-weight: 400;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: .5pt solid windowtext; border-bottom: .5pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #EAE7ED;">
                            Dettaglio inquilini
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="overflow: auto">
                                <asp:DataGrid runat="server" ID="DataGridInquilini12Mesi" AutoGenerateColumns="False"
                                    CellPadding="1" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None"
                                    CellSpacing="1" Width="100%" AllowPaging="False" PageSize="300" Style="border: 1px solid #507CD1;">
                                    <AlternatingItemStyle BackColor="#F9F9F9" ForeColor="#000000" />
                                    <Columns>
                                        <asp:BoundColumn HeaderText="Codice contrattuale" DataField="CODICE_CONTRATTO"></asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Intestatario" DataField="INTESTATARIO2"></asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Debito" DataField="DEBITO2" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Tipologia rapporto" DataField="COD_TIPOLOGIA_CONTR_LOC">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Posizione contrattuale" DataField="POSIZIONE_CONTRATTO">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Unità immobiliare" DataField="COD_UNITA_IMMOBILIARE">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Tipo UI" DataField="COD_TIPOLOGIA"></asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Indirizzo completo" DataField="INDIRIZZO"></asp:BoundColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                                        HorizontalAlign="Center" />
                                    <ItemStyle BackColor="#F9F9F9" ForeColor="#000000" />
                                    <PagerStyle BackColor="White" ForeColor="#507CD1" HorizontalAlign="Left" Mode="NumericPages"
                                        Position="Top" />
                                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                                </asp:DataGrid>
                            </div>
                        </td>
                    </tr>
                    <tr style="height: 17.25pt">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="riepilogoGenerale">
                <p style='page-break-after: always'>
                    &nbsp;</p>
                <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                    width="100%">
                    <tr style="height: 20.25pt">
                        <td height="27" style="height: 20.25pt; color: #ffffff; font-size: 15.0pt; font-weight: 700;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: bottom; white-space: nowrap; border-left: 1.0pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #507CD1;">
                            Riepilogo Generale
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 53.25pt; width: 100%; color: black; font-size: 14.0pt; font-weight: 700;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , serif;
                            text-align: left; vertical-align: top; white-space: normal; border-left: 1.0pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: 1.0pt solid windowtext; border-bottom-style: none; border-bottom-color: inherit;
                            border-bottom-width: medium; padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        N° Assegnatari Totali nel Range di estrazione:&nbsp;<asp:Label ID="AssegnatariTotali"
                                            runat="server" Style="border: 0px;" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        N° Assegnatari a Credito nel Range di estrazione:&nbsp;
                                        <asp:Label ID="AssegnatariCreditori" runat="server" Style="border: 0px" />
                                        per un importo a credito totale di € &nbsp;
                                        <asp:Label runat="server" ID="importoAssegnatariCreditori" Style="border: 0px" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 17.25pt">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="riepilogoSaldoContabile">
                <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                    width="100%">
                    <tr>
                        <td colspan="8" style="height: 20.1pt; color: #ffffff; font-size: 15.0pt; font-weight: 700;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: bottom; white-space: nowrap; border-left: 1.0pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #507CD1;">
                            Riepilogo Morosità
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" style="height: 20.1pt; color: #ffffff; font-size: 12.0pt; font-weight: 700;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: bottom; white-space: nowrap; border-left: 1.0pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #507CD1;">
                            Assegnatari a debito
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td colspan="4" height="176" rowspan="3" style="height: 132.0pt; color: black; font-size: 11.0pt;
                            font-weight: 400; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top-style: none; border-top-color: inherit; border-top-width: medium;
                            border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: center; width: 50%; height: 70px">
                                        N° assegnatari morosi
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; width: 50%; height: 70px">
                                        <asp:Label ID="NAssegnatariAllMesi" runat="server" Style="border-style: none; border-color: inherit;
                                            border-width: 0px; text-align: center;" Font-Names="Arial" Font-Size="10pt" Height="18px">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="4" style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: center;
                            vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top-style: none; border-top-color: inherit; border-top-width: medium;
                            border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        Morosità totale
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        <asp:Label ID="ImportoMorositaTotaleAllMesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td colspan="2" height="110" rowspan="2" style="height: 82.5pt; color: black; font-size: 11.0pt;
                            font-weight: 400; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="height: 40pt; text-align: center;">
                                        Morosità ex gestori
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 40pt; text-align: center;">
                                        <asp:Label ID="ImportoMorositaExGestoriallMesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2" style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: left;
                            vertical-align: middle; white-space: normal; padding-left: 1px; padding-right: 1px;
                            padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        Morosità Gestore
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        <asp:Label ID="ImportoMorositaALERallMesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td height="66" style="height: 49.5pt; color: black; font-size: 11.0pt; font-weight: 400;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-style: none;
                            border-color: inherit; border-width: medium; padding-left: 1px; padding-right: 1px;
                            padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%; height: 25pt; text-align: center">
                                        Canoni
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 24pt; text-align: center">
                                        <asp:Label ID="ImportoMorositaCanoniAllMesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt" Height="18px">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: center;
                            vertical-align: middle; white-space: normal; border-style: none; border-color: inherit;
                            border-width: medium; padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%; height: 25pt; text-align: center">
                                        Servizi
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 24pt; text-align: center">
                                        <asp:Label ID="ImportoMorositaServiziAllMesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height: 16.5pt; color: black; font-size: 11.0pt;
                            font-weight: 400; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: .5pt solid windowtext; border-bottom: .5pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #EAE7ED;">
                            Dettaglio assegnatari morosi
                        </td>
                        <td style="text-align: center; color: black; font-size: 11.0pt; font-weight: 400;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: .5pt solid windowtext; border-bottom: .5pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #EAE7ED;" colspan="4">
                            Dettaglio Morosità
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 12.5%">
                            N° assegnatari a cui è stata inviata M.M.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            N° Assegnatari con richiesta di Contributo Solidarietà in essere
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            N° Assegnatari con Pratiche Legali in essere
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            N° altri assegnatari
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità assegnatari con M.M.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità assegnatari con richiesta di C.S.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità assegnatari con P.L.
                        </td>
                        <td style="text-align: left; width: 12.5%">
                            Morosità altri assegnatari
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariMMallMesi" runat="server" Style="border: 0px; text-align: right">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariCSallMesi" runat="server" Style="border: 0px; text-align: right">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariPLallMesi" runat="server" Style="border: 0px; text-align: right">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="NAssegnatariAltriallMesi" runat="server" Style="border: 0px; text-align: right">0</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaMMallMesi" runat="server" Style="border: 0px; text-align: right">0,00</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaCSallMesi" runat="server" Style="border: 0px; text-align: right">0,00</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaPLallMesi" runat="server" Style="border: 0px; text-align: right">0,00</asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="MorositaAltriAllMesi" runat="server" Style="border: 0px; text-align: right">0,00</asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 17.25pt">
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="riepilogoBollette">
                <p style='page-break-after: always'>
                    &nbsp;</p>
                <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                    width="100%">
                    <tr>
                        <td colspan="4" height="27" style="height: 20.25pt; color: #ffffff; font-size: 15.0pt;
                            font-weight: 700; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: bottom; white-space: nowrap; border-left: 1.0pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 1px;
                            padding-right: 1px; padding-top: 1px; background: #507CD1;">
                            Riepilogo bollette di messa in mora comprese nel range di estrazione
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td height="176" rowspan="3" style="height: 132.0pt; color: black; font-size: 11.0pt;
                            font-weight: 400; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top-style: none; border-top-color: inherit; border-top-width: medium;
                            border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: center; width: 50%; height: 70px">
                                        Visualizza n° mensilità messe in mora tra quelle estratte per morosità
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; width: 50%; height: 70px">
                                        <asp:Label ID="NAssegnatariBolMesi" runat="server" Style="border-style: none; border-color: inherit;
                                            border-width: 0px; text-align: center;" Font-Names="Arial" Font-Size="10pt" Height="18px">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="3" style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: center;
                            vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            border-right-style: none; border-right-color: inherit; border-right-width: medium;
                            border-top-style: none; border-top-color: inherit; border-top-width: medium;
                            border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: medium;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        Importo di messa in mora totale
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        <asp:Label ID="ImportoMorositaTotaleBolMesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td height="110" rowspan="2" style="height: 82.5pt; color: black; font-size: 11.0pt;
                            font-weight: 400; font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-left: .5pt solid windowtext;
                            padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="height: 40pt">
                                        Importo messa in mora su morosità ex gestori
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 40pt">
                                        <asp:Label ID="ImportoMorositaExGestoriBolMesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2" style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: left;
                            vertical-align: middle; white-space: normal; padding-left: 1px; padding-right: 1px;
                            padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        Importo messa in mora su morosità Gestore
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        <asp:Label ID="ImportoMorositaALERBolMesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 16.5pt">
                        <td height="66" style="height: 49.5pt; color: black; font-size: 11.0pt; font-weight: 400;
                            font-style: normal; text-decoration: none; font-family: Book Antiqua , sans-serif;
                            text-align: center; vertical-align: middle; white-space: normal; border-style: none;
                            border-color: inherit; border-width: medium; padding-left: 1px; padding-right: 1px;
                            padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%; height: 25pt; text-align: center">
                                        Canoni
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 24pt; text-align: center">
                                        <asp:Label ID="ImportoMorositaCanoniBolMesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt" Height="18px">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="color: black; font-size: 11.0pt; font-weight: 400; font-style: normal;
                            text-decoration: none; font-family: Book Antiqua , sans-serif; text-align: center;
                            vertical-align: middle; white-space: normal; border-style: none; border-color: inherit;
                            border-width: medium; padding-left: 1px; padding-right: 1px; padding-top: 1px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%; height: 25pt; text-align: center">
                                        Servizi
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 24pt; text-align: center">
                                        <asp:Label ID="ImportoMorositaServiziBolMesi" runat="server" Style="border-style: none;
                                            border-color: inherit; border-width: 0px; text-align: center;" Font-Names="Arial"
                                            Font-Size="10pt">0,00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        if (document.getElementById('divLoading') != null) {
            document.getElementById('divLoading').style.visibility = 'hidden';
        }
    </script>
</body>
</html>
