<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Sondrio.aspx.vb" Inherits="Contratti_Sondrio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    <style type="text/css">
        #Submit1
        {
            height: 26px;
        }
        .style1
        {
            font-family: Arial;
            font-size: small;
        }
    </style>
</head>
<body>
<form id="form1" runat ="server" action="" method="post">
<%=Indirizzo%>
    <div style="text-align: center">
    <table style="width:100%;">
            <tr>
                <td style="text-align: left">
                <img alt="" src="Immagini/LogoMilano.gif" /></td>
                <td align="right">
                <img alt="" src="Immagini/LogoPopSo.gif" /></td>
            </tr>
        </table>
        <span class="style1">Si prega di verificare che le informazioni sotto riportate 
        siano corrette. Premere il pulsante EMETTI MAV ON LINE per effettuare il 
        collegamento alla Banca preposta e generare l&#39;avviso di pagamento.<br />
        IL PAGAMENTO DEVE ESSERE EFFETTUATO TRAMITE CONTANTI O ASSEGNO CIRCOLARE<br />
        </span> 
        <table style="border-style: solid; border-width: 1px; width:80%; text-align: left;">
            <tr>
                <td>
                    Codice Fiscale</td>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Nominativo</td>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Presso</td>
                <td>
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Indirizzo</td>
                <td>
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    CAP</td>
                <td>
                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Città</td>
                <td>
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Provincia</td>
                <td>
                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    E-mail</td>
                <td>
                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Importo</td>
                <td>
                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Scadenza</td>
                <td>
                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Causale</td>
                <td>
                    <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <asp:HiddenField ID="idtransazione" runat="server" Value="4" />
        <asp:HiddenField ID="xml" runat="server" />
        <asp:HiddenField ID="cassa" runat="server" Value="AM"/>
        <asp:HiddenField ID="mac" runat="server"/>       
        <asp:HiddenField ID="url_ritorno" runat="server"/>  
        &nbsp;&nbsp;
        <br />
&nbsp;<br />
        <input id="Submit1" type="submit" value="Emetti MAV on Line" onclick="return Submit1_onclick()" />
        <input id="Button1" name="Annulla ed Esci" type="button" 
            value="Annulla ed Esci" onclick="return Button1_onclick()" /></div>
    </form>
                    <script type="text/javascript">
                    window.focus();
                    self.focus();
                    function Button1_onclick() {
                        self.close();
                    }



                    </script>
    <p style="text-align: center">
        &nbsp;</p>
</body>
</html>
