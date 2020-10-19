<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MavIntesa.aspx.vb" Inherits="Contratti_MavIntesa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 172px;
            text-align: left;
        }
        .style2
        {
            width: 172px;
            text-align: left;
            font-weight: normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>
                <img alt="" src="Immagini/LogoMilano.gif" /></td>
                <td align="right">
                <img alt="" src="Immagini/LogoIntesa.gif" /></td>
            </tr>
        </table>
    
    </div>
    <table style="width:100%;">
    <tr>
    <td>
        &nbsp;</td>
    </tr>
    <tr>
    <td align="center" 
            style="font-family: arial, Helvetica, sans-serif; font-size: 12pt; font-weight: bold">
        <span class="style2">Si prega di verificare che le informazioni sotto riportate 
        siano corrette. Premere il pulsante EMETTI MAV ON LINE per effettuare il 
        collegamento alla Banca preposta e generare l&#39;avviso di pagamento.<br />
        IL PAGAMENTO DEVE ESSERE EFFETTUATO TRAMITE CONTANTI O ASSEGNO CIRCOLARE<br />
        <br />
        </span> 
        <br />
    </td>
    </tr>
        <tr>
            <td align="center">
                <table style="width:60%;">
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" 
                                Text="Numero Bolletta"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblNumeroMav" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="10pt" 
                                Text="Destinatario"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblCognome" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;</td>
                        <td style="text-align: left">
                            <asp:Label ID="lblNome" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="10pt" 
                                Text="Codice Fiscale"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblCodFiscale" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt" 
                                Text="Codice Debitore"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblCodDebitore" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="10pt" 
                                Text="Importo"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblimporto" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="10pt" 
                                Text="Scadenza"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblScadenza" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="10pt" 
                                Text="Causale"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblCausale" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label9" runat="server" Font-Names="arial" Font-Size="10pt" 
                                Text="Indirizzo"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblindirizzo" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label10" runat="server" Font-Names="arial" Font-Size="10pt" 
                                Text="Località"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblLocalita" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label12" runat="server" Font-Names="arial" Font-Size="10pt" 
                                Text="Provincia"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblProvincia" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label11" runat="server" Font-Names="arial" Font-Size="10pt" 
                                Text="CAP"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblCap" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblErrore" runat="server" ForeColor="#CC0000" 
                    style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Button ID="Button1" runat="server" Text="Emetti MAV Online" 
                    Width="124px" />
&nbsp;
                <asp:Button ID="Button2" runat="server" Text="ANNULLA ed Esci" Width="124px" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:HiddenField ID="idbolletta" runat="server" Value="193536" />
    <asp:HiddenField ID="causalepagamento" runat="server" />
        <asp:HiddenField ID="tipoBolletta" runat="server" Value="0" />

    </form>
</body>
</html>
