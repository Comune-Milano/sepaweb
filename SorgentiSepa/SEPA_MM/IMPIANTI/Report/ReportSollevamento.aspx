<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportSollevamento.aspx.vb" Inherits="ASS_ReportSollevamento" %>

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
                    <img src="../../IMG/logo.gif" style="z-index: 100; left: 0px; position: static; top: 0px" /></td>
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
                <td style="height: 21px; width: 20%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">COMPLESSO:</span></td>
                <td style="height: 21px; width: 30%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblComplesso" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="height: 21px; width: 20%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">EDIFICIO:</span></td>
                <td style="height: 21px; width: 30%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblEdificio" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 20%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">COD. IMPIANTO:</span></td>
                <td style="width: 30%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblCodice" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="width: 20%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">SCALA:</span></td>
                <td style="width: 30%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblScala" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 20%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">DENOMINAZIONE:</span></td>
                <td style="width: 30%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblDenominazione" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="width: 20%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">TIPOLOGIA D'USO:</span></td>
                <td style="width: 30%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblTipologia" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 20%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">DITTA INSTALLATRICE:</span></td>
                <td style="width: 30%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblDitta" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="width: 20%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">DATA INSTALLAZIONE:</span></td>
                <td style="width: 30%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblData" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
        </table>
        <br />
        <table width="100%" style="z-index: 100; left: 0px; position: static; top: 0px">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">DATI TECNICI</span></strong></td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">MARCA:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblMarca" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">MODELLO:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblModello" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr style="font-weight: bold">
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">NUMERO IMPIANTO:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblNumImpianto" runat="server" Font-Bold="False" Font-Names="arial"
                        Font-Size="10pt" Style="z-index: 100; left: 0px; position: static; top: 0px"
                        Width="100%"></asp:Label></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">UBICAZIONE LOCALE MACCHINE:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblUbicazione" runat="server" Font-Bold="False" Font-Names="arial"
                        Font-Size="10pt" Style="z-index: 100; left: 0px; position: static; top: 0px"
                        Width="100%"></asp:Label></td>
            </tr>
            <tr style="font-weight: bold">
                <td style="font-size: 10pt; width: 25%; font-family: 'Times New Roman';">
                    NUMERO MATRICOLA:</td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial;">
                    <asp:Label ID="lblMatricola" runat="server" Font-Bold="False" Font-Names="arial"
                        Font-Size="10pt" Style="z-index: 100; left: 0px; position: static; top: 0px"
                        Width="100%"></asp:Label></td>
                <td class="z" style="font-size: 10pt; width: 25%; font-family: 'Times New Roman';">
                    TIPO AZIONAMENTO:</td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial;">
                    <asp:Label ID="lblAzionamento" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%" Font-Bold="False"></asp:Label></td>
            </tr>
            <tr style="font-weight: bold">
                <td style="font-size: 10pt; width: 25%; font-family: 'Times New Roman'">
                    NUMERO LOTTO:</td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:Label ID="lblLotto" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td class="z" style="font-size: 10pt; width: 25%; font-family: 'Times New Roman'">
                    TIPO MANOVRA:</td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:Label ID="lblManovra" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr style="font-weight: bold">
                <td style="font-size: 10pt; width: 25%; font-family: 'Times New Roman';">
                    DITTA DI GESTIONE:</td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial;">
                    <asp:Label ID="lblDittaGestione" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%" Font-Bold="False"></asp:Label></td>
                <td class="z" style="font-size: 10pt; width: 25%; font-family: 'Times New Roman';">
                    NUM. TELEFONICO DITTA:</td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial;">
                    <asp:Label ID="lblTel" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt" Font-Bold="False"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">TELEALLARME:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:CheckBox ID="chkAllarme_SI" runat="server" Font-Bold="False" Text="SI" TextAlign="Left" />
                    &nbsp;
                    <asp:CheckBox ID="chkAllarme_NO" runat="server" Font-Bold="False" Text="NO"
                        TextAlign="Left" /></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">NUM. TELEFONICO TELEALLARME:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblTelAllarme" runat="server" Font-Bold="False" Font-Names="arial"
                        Font-Size="10pt" Style="z-index: 100; left: 0px; position: static; top: 0px"
                        Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 10pt; width: 25%; font-family: 'Times New Roman'; height: 21px">
                </td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial; height: 21px">
                </td>
                <td class="z" style="font-size: 10pt; width: 25%; font-family: 'Times New Roman';
                    height: 21px">
                </td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial; height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">VELOCITA (m/s):</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblVelocita" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt"></span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">PORTATA (Kg):</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblPortata" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                </td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                </td>
            </tr>
            <tr>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt"><strong>CORSA (m):</strong></span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblCorsa" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt"></span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    </td>
            </tr>
            <tr>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt"><strong>NUM. MAX PERSONE:</strong></span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblMaxPersone" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt"></span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    </td>
            </tr>
            <tr>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt"><strong>NUM. FERMATE:</strong></span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblFermate" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <strong>TIPOLOGIA CANONE:</strong></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblNumero" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
        </table>
        <br />
        <table width="100%" style="z-index: 100; left: 0px; position: static; top: 0px">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">CERTIFICAZIONI</span></strong></td>
            </tr>
        </table>
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="width: 30%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">SCHEMA IMPIANTO:</span></td>
                <td style="width: 30%; font-size: 10pt; font-family: Arial;">
                    <asp:CheckBox ID="chkSchema_SI" runat="server" Font-Bold="False" Text="SI" TextAlign="Left" />
                    &nbsp;
                    <asp:CheckBox ID="chkSchema_NO" runat="server" Font-Bold="False" Text="NO"
                        TextAlign="Left" /></td>
                <td style="width: 20%; font-weight: bold;" class="z">
                    <span style="font-size: 10pt; font-weight: bold;"></span>
                </td>
                <td style="width: 20%;">
                </td>
            </tr>
            <tr style="font-weight: bold; font-size: 12pt;">
                <td style="width: 30%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">CERTIFICAZIONE &nbsp;&nbsp;DM 37/2008:</span></td>
                <td style="width: 30%; font-size: 10pt; font-family: Arial;">
                    <asp:CheckBox ID="chkDM37_SI" runat="server" Font-Bold="False" Text="SI" TextAlign="Left" />
                    &nbsp;
                    <asp:CheckBox ID="chkDM37_NO" runat="server" Font-Bold="False" Text="NO"
                        TextAlign="Left" /></td>
                <td style="width: 20%; font-weight: bold;" class="z">
                    <span style="font-size: 10pt; font-weight: bold;"></span>
                </td>
                <td style="width: 20%;">
                </td>
            </tr>
            <tr style="font-weight: bold">
                <td style="width: 30%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">LIBRETTO:</span></td>
                <td style="width: 30%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:CheckBox ID="chkLibretto_SI" runat="server" Font-Bold="False" Text="SI" TextAlign="Left" />
                    &nbsp;
                    <asp:CheckBox ID="chkLibretto_NO" runat="server" Font-Bold="False" Text="NO"
                        TextAlign="Left" /></td>
                <td style="width: 20%; height: 21px; font-weight: bold;" class="z">
                    <span style="font-size: 10pt; font-weight: normal;"></span>
                </td>
                <td style="width: 20%; height: 11px">
                </td>
            </tr>
            <tr>
                <td style="height: 21px; width: 30%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">CERT. CONFORMITA' CE:</span></td>
                <td style="height: 21px; width: 30%; font-size: 10pt; font-family: Arial;">
                    <asp:CheckBox ID="chkCE_SI" runat="server" Font-Bold="False" Text="SI" TextAlign="Left" />
                    &nbsp;
                    <asp:CheckBox ID="chkCE_NO" runat="server" Font-Bold="False" Text="NO"
                        TextAlign="Left" /></td>
                <td style="height: 21px; width: 20%; font-weight: bold;" class="z">
                    <span style="font-size: 10pt; font-weight: normal;"></span>
                </td>
                <td style="height: 11px; width: 20%;">
                </td>
            </tr>
            <tr>
                <td style="font-size: 10pt; width: 30%; font-family: 'Times New Roman'; height: 21px">
                    <strong>NUM. MATRICOLA:</strong></td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial; height: 21px">
                    <asp:CheckBox ID="chkMatricola_SI" runat="server" Font-Bold="False" Text="SI" TextAlign="Left" />
                    &nbsp;
                    <asp:CheckBox ID="chkMatricola_NO" runat="server" Font-Bold="False" Text="NO"
                        TextAlign="Left" /></td>
                <td class="z" style="font-weight: bold; width: 20%; height: 21px">
                </td>
                <td style="width: 20%; height: 11px">
                </td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; page-break-before: always; position: static;
            top: 0px" width="100%">
            <tr>
                <td style="height: 24px; text-align: center" width="100%">
                    <strong><span style="font-size: 14pt"></span></strong>
                </td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px; font-size: 12pt;" width="100%">
            <tr>
                <td style="text-align: center" width="100%">
                    <span style="font-size: 14pt"><strong>
                        <asp:Label ID="lblTitoloInquilini" runat="server" Font-Names="Times New Roman" Font-Size="14pt"
                            Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%">UTILIZZO ESCLUSIVO DEL MONTASCALE (Elenco Inquilini)</asp:Label></strong></span></td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGridI" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
            left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
            TabIndex="24" Width="100%">
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
            <AlternatingItemStyle BackColor="#FAFFFF" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="INTESTATARIO">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="75%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="SCALA" HeaderText="SCALA">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="5%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PIANO" HeaderText="PIANO">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="15%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="5%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
        </asp:DataGrid><br />
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px; font-size: 12pt;" width="100%">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">VERIFICHE PERIODICHE BIENNALI</span></strong></td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" PageSize="1" Style="table-layout: auto; z-index: 101; left: 8px;
            clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
            TabIndex="24" Width="100%">
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
                <asp:BoundColumn DataField="DITTA" HeaderText="DITTA">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="25%" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="DATA">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="10%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="PRESCRIZIONI">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="30%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ESITO">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO_DETTAGLIO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO_DETTAGLIO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="10%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="ES_PRESCRIZIONE" HeaderText="PRESCRIZIONE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="10%" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="VALIDITA' (mesi)">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_VALIDITA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_VALIDITA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="5%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="SCADENZA">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="10%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ESITO" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="MESI_PREALLARME" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_PREALLARME") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_PREALLARME") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="TIPO" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                            ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                            Text="Annulla"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                            Text="Modifica">Seleziona</asp:LinkButton>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
        </asp:DataGrid><br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="height: 24px; text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">VERIFICHE STRAORDINARIE</span></strong></td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" PageSize="1" Style="table-layout: auto; z-index: 101; left: 8px;
            clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
            TabIndex="39" Width="100%">
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
                <asp:BoundColumn DataField="DITTA" HeaderText="DITTA">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="30%" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="DATA">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="10%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="MOTIVO">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ES_PRESCRIZIONE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ES_PRESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="30%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ESITO">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO_DETTAGLIO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO_DETTAGLIO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="10%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="VALIDITA' (mesi)" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_VALIDITA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_VALIDITA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="10%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="SCADENZA" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="10%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ESITO" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="MESI_PREALLARME" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_PREALLARME") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_PREALLARME") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="TIPO" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                            ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                            Text="Annulla"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                            Text="Modifica">Seleziona</asp:LinkButton>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
        </asp:DataGrid><br />
        <br /><table width="100%" style="z-index: 100; left: 0px; position: static; top: 0px">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">NOTE</span></strong></td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="height: 21px; width: 1000%;">
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
