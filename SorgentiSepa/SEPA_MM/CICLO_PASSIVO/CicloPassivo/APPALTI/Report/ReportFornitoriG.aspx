<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportFornitoriG.aspx.vb" Inherits="ReportFornitoriG" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td>
                    <img src="../../../../IMG/logo.gif" alt="logo Comune Milano" style="z-index: 100; left: 0px; position: static; top: 0px" /></td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="font-size: 10pt"><strong>Settore Impianti</strong></span></td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 21px">
                    <span style="font-size: 10pt">Tel. 02/884.64435-36 Fax 02/884.66991</span></td>
                <td style="height: 21px">
                </td>
                <td style="height: 21px">
                </td>
            </tr>
        </table>
    
    </div>
        <br />
        <table width="100%">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">
                        <asp:Label ID="Label2" runat="server" Style="z-index: 100; left: 0px; position: static;
                            top: 0px" Text="Label" Width="595px" BorderStyle="Solid" BorderWidth="1px"></asp:Label></span></strong></td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-weight: bold; font-size: 10pt">RAGIONE SOCIALE:</span></td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial;">
                    <strong><span style="font-family: 'Times New Roman'">
                    <asp:Label ID="lblragione" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></span></strong></td>
                <td style="font-weight: bold; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-weight: bold; font-size: 10pt">PARTITA IVA:</span></td>
                <td style="font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lbliva" runat="server" Font-Names="arial" Font-Size="10pt" Style="font-weight: bold;
                        z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr style="font-weight: bold">
                <td style="font-size: 10pt; font-family: 'Times New Roman'; height: 18px;">
                    <span style="font-weight: bold; font-size: 10pt">CODICE FISCALE:</span></td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial; height: 18px;">
                    <asp:Label ID="lblCF" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="font-size: 10pt; font-family: 'Times New Roman'; height: 18px;">
                    <span style="font-weight: bold; font-size: 10pt">
                    IBAN:</span></td>
                <td style="font-size: 10pt; font-family: Arial; height: 18px;">
                    <asp:Label ID="lbliban" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-weight: bold; font-size: 10pt">
                    </span></td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial; font-weight: bold;">
                    </td>
                <td style="font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-weight: bold; font-size: 10pt">
                    </span></td>
                <td style="font-size: 10pt; font-family: Arial;">
                    </td>
            </tr>
        </table>
        <br />
        <table width="100%" style="z-index: 100; left: 0px; position: static; top: 0px">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">SEDE LEGALE</span></strong></td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;"> INDIRIZZO:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblindirizzo" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;"></span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    </td>
            </tr>
            <tr style="font-weight: bold">
                <td style="font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">CAP:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblcap" runat="server" Font-Names="arial" Font-Size="10pt" Style="font-weight: normal;
                        z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">CIVICO:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblcivico" runat="server" Font-Names="arial" Font-Size="10pt" Style="font-weight: normal;
                        z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">COMUNE:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblcomune" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">PROVINCIA:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblpr" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 10pt; font-family: 'Times New Roman'">
                    <strong>TELEFONO:</strong></td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:Label ID="lblTel" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td class="z" style="font-size: 10pt; font-family: 'Times New Roman'">
                    <strong>FAX:</strong></td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:Label ID="lblfax" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; page-break-before: always; position: static;
            top: 0px" width="100%">
            <tr>
                <td style="height: 24px; text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">SEDE AMMINISTRATIVA</span></strong></td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;"> INDIRIZZO:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblindirizzoa" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="font-size: 10pt; font-family: 'Times New Roman'; font-weight: bold;" class="z">
                    <span style="font-size: 10pt; font-weight: bold;"></span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    </td>
            </tr>
            <tr style="font-weight: bold">
                <td style="font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">CAP:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblcapa" runat="server" Font-Names="arial" Font-Size="10pt" Style="font-weight: normal;
                        z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">CIVICO:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblcivicoa" runat="server" Font-Names="arial" Font-Size="10pt" Style="font-weight: normal;
                        z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">COMUNE:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblcomunea" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">PROVINCIA:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblpra" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 10pt; font-family: 'Times New Roman'">
                    <strong>TELEFONO:</strong></td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:Label ID="lbltela" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td class="z" style="font-size: 10pt; font-family: 'Times New Roman'">
                    <strong>FAX:</strong></td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:Label ID="lblfaxa" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; page-break-before: always; position: static;
            top: 0px" width="100%">
            <tr>
                <td style="height: 24px; text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">RAPPRESENTANTE</span></strong></td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="font-weight: bold; font-size: 10pt; width: 25%; font-family: 'Times New Roman'">
                    COGNOME:</td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:Label ID="lblcognome" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td class="z" style="font-weight: bold; font-size: 10pt; width: 25%; font-family: 'Times New Roman'">
                    NOME:</td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:Label ID="lblnome" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-weight: bold; font-size: 10pt; width: 25%; font-family: 'Times New Roman'">
                    CODICE FISCALE:</td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:Label ID="lblcfr" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td class="z" style="font-weight: bold; font-size: 10pt; width: 25%; font-family: 'Times New Roman'">
                    TIPO:</td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:Label ID="lbltipor" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;"> INDIRIZZO:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblindirizzor" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="font-weight: normal; z-index: 100; left: 0px; position: static; top: 0px"
                        Width="100%"></asp:Label></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman'; font-weight: bold;" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">CAP:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblcapr" runat="server" Font-Names="arial" Font-Size="10pt" Style="font-weight: normal;
                        z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr style="font-weight: bold">
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">CIVICO:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblcivicor" runat="server" Font-Names="arial" Font-Size="10pt" Style="font-weight: normal;
                        z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">COMUNE:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblcomuner" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px; font-weight: normal;" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">PROVINCIA:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblprr" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">TELEFONO:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lbltelr" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-weight: bold; font-size: 10pt; width: 25%; font-family: 'Times New Roman'">
                    NUMERO PROCURA:</td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:Label ID="lblprocura" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td class="z" style="font-weight: bold; font-size: 10pt; width: 25%; font-family: 'Times New Roman'">
                    DATA PROCURA:</td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:Label ID="lbldataprocura" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">ELENCO APPALTI</span></strong></td>
            </tr>
        </table>
            <asp:DataGrid ID="DataGrid3" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="Black" Height="14px" PageSize="1" Style="table-layout: auto; z-index: 101;
                left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                TabIndex="13" Width="932px">
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_FORNITORE" HeaderText="ID_FORNITORE" Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NUM_REPERTORIO" HeaderText="NUM.REPERTORIO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_REPERTORIO" HeaderText="DATA REPERTORIO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="SAL" HeaderText="SAL">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_INIZIO" HeaderText="DATA INIZIO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_FINE" HeaderText="DATA FINE">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DURATA" HeaderText="DURATA MESI">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ASTA_CANONE" HeaderText="BASE ASTA CANONE">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ASTA_CONSUMO" HeaderText="BASE ASTA CONSUMO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ONERI_CANONE" HeaderText="ONERI SICUREZZA CANONE">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ONERI_CONSUMO" HeaderText="ONERI SICUREZZA CONSUMO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="PERC_ONERI_SIC_CAN" HeaderText="PERC.ONERI SICUREZZA CANONE">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="PERC_ONERI_SIC_CON" HeaderText="PERC.ONERI SICUREZZA CONSUMO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="PENALI" HeaderText="PENALI">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="RIFINIZIO" HeaderText="ANNO RIF.INIZIO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="RIFINE" HeaderText="ANNO RIF.FINE">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="COSTO" HeaderText="COSTO GRADO GIORNO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCRIZIONE_LOTTO" HeaderText="DESCRIZIONE LOTTO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_LOTTO" HeaderText="ID_LOTTO" Visible="False">
                    </asp:BoundColumn>
                </Columns>
                <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="#0000C0" Wrap="False" />
            </asp:DataGrid><br />
        <br />
        <table width="100%" style="z-index: 100; left: 0px; position: static; top: 0px">
            <tr>
                <td style="text-align: center; height: 24px;" width="100%">
                    <strong><span style="font-size: 14pt">RIFERIMENTI</span></strong></td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="height: 21px; width: 218%;">
                    <span style="font-size: 10pt">
                    <asp:Label ID="lblNote" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></span></td>
            </tr>
        </table>
        <br />
        <br />
    </form>
</body>
</html>
