<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportCannaFumaria.aspx.vb" Inherits="ReportCannaFumaria" %>

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
                    <span style="font-size: 10pt; font-weight: bold;">DENOMINAZIONE:</span></td>
                <td style="width: 30%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblDenominazione" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="width: 20%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">COD. IMPIANTO:</span></td>
                <td style="width: 30%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblCodice" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
        </table>
        <br />
        <table width="100%" style="z-index: 100; left: 0px; position: static; top: 0px">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">DETTAGLI</span></strong></td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">TIPO INSTALLAZIONE:</span></td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblTipologia" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">NUMERO UTENZE:</span></td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblNumUtenze" runat="server" Font-Bold="False" Font-Names="arial"
                        Font-Size="10pt" Style="z-index: 100; left: 0px; position: static; top: 0px"
                        Width="100%"></asp:Label></td>
            </tr>
            <tr style="font-weight: bold">
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">TIPO SERVITU':</span></td>
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblServitu" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;"></span></td>
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: Arial;">
                    </td>
            </tr>
            <tr>
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">CERTIFICATO DI CONFORMITA':</span></td>
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblConformita" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;"></span></td>
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: Arial;">
                    </td>
            </tr>
            <tr>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman'; height: 21px;">
                    <span style="font-size: 10pt; font-weight: bold;">POTENZIALITA' TOTALE:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial; height: 21px;">
                    <asp:Label ID="lblPotenzaTOT" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman'; height: 21px;" class="z">
                    <span style="font-size: 10pt"></span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial; height: 21px;">
                    </td>
            </tr>
            <tr>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">POTENZIALITA' SINGOLA UTENZA:</span></td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblPotenza" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    </td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: Arial;">
                    </td>
            </tr>
        </table>
        <br />
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
