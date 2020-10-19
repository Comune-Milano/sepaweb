<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NotificaISEIncompleta.aspx.vb" Inherits="ANAUT_NotificaISE" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Notifica</title>
</head>

<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td style="font-family: arial; font-size: 12pt; font-weight: bold">
                    Dati riassuntivi</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCodContratto0" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="False">Cod.Contratto:</asp:Label>
                    <asp:Label ID="lblCodContratto" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCodContratto1" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="False">Intestatario:</asp:Label>
                    <asp:Label ID="lblIntestatario" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCodContratto2" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="False">Indirizzo U.I.</asp:Label>
                    <asp:Label ID="lblIndirizzo" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCodContratto6" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="False">Località U.I.:</asp:Label>
                    <asp:Label ID="lblLocalita" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCodContratto3" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="False">Indirizzo Postale:</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIndirizzoPostale" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIndirizzoPostale2" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIndirizzoPostale0" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIndirizzoPostale1" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCodContratto4" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="False">Sede Territoriale:</asp:Label>
                    <asp:Label ID="lblFiliale" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEstremi" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                &nbsp;<asp:Label ID="lblEstremi0" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                &nbsp;<asp:Label ID="lblEstremi1" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEstremi2" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEstremi3" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEstremi4" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEstremi5" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="True"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td valign="middle">
                    <asp:Label ID="lblCodContratto7" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="False">Numero di Protocollo:</asp:Label>
                    <asp:TextBox ID="txtProtocollo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    <img id="img_procedi" alt="" src="../NuoveImm/Img_Procedi.png" 
                        style="cursor: pointer" onclick="Apri();"/>&nbsp;&nbsp;<img alt="" src="../NuoveImm/Img_Esci_AMM.png" onclick="self.close();" style="cursor:pointer"/></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErrore" runat="server" Font-Names="ARIAL" Font-Size="10pt" 
                        Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
    <asp:HiddenField ID="idc" runat="server" />
    <asp:HiddenField ID="idd" runat="server" />
    <asp:HiddenField ID="a" runat="server" />
    <asp:HiddenField ID="b" runat="server" />
    <asp:HiddenField ID="c" runat="server" />
    <asp:HiddenField ID="d" runat="server" />
    <asp:HiddenField ID="xx" runat="server" />
    <asp:HiddenField ID="f" runat="server" />
    <asp:HiddenField ID="g" runat="server" />
    <asp:HiddenField ID="h" runat="server" />
    <asp:HiddenField ID="i" runat="server" />
    <asp:HiddenField ID="l" runat="server" />
    <asp:HiddenField ID="m" runat="server" />
    <asp:HiddenField ID="n" runat="server" />
    <asp:HiddenField ID="o" runat="server" />
    <asp:HiddenField ID="p" runat="server" />
    <asp:HiddenField ID="q" runat="server" />
    <asp:HiddenField ID="y" runat="server" />
    <asp:HiddenField ID="j" runat="server" />
    <asp:HiddenField ID="AU" runat="server" />


    <script type="text/javascript">
        function Apri() {
            if (document.getElementById('y').value == '0' &&  document.getElementById('q').value != '' && document.getElementById('o').value != '' && document.getElementById('p').value != '' && document.getElementById('a').value != '' && document.getElementById('b').value != '' && document.getElementById('c').value != '' && document.getElementById('d').value != '' && document.getElementById('xx').value != '' && document.getElementById('f').value != '' && document.getElementById('g').value != '' && document.getElementById('h').value != '' && document.getElementById('i').value != '' && document.getElementById('l').value != '' && document.getElementById('m').value != '' && document.getElementById('n').value != '') {
                if (document.getElementById('txtProtocollo').value != '') {
                    window.open('Stampe/NotificaISEIncompleta.aspx?AU=' + document.getElementById('AU').value + '&PG=' + document.getElementById('txtProtocollo').value + '&COD=' + document.getElementById('idc').value + '&ID=' + document.getElementById('idd').value + '&1=' + document.getElementById('a').value + '&2=' + document.getElementById('b').value + '&3=' + document.getElementById('c').value + '&4=' + document.getElementById('d').value + '&5=' + document.getElementById('xx').value + '&6=' + document.getElementById('f').value + '&7=' + document.getElementById('g').value + '&8=' + document.getElementById('h').value + '&9=' + document.getElementById('i').value + '&10=' + document.getElementById('l').value + '&11=' + document.getElementById('m').value + '&12=' + document.getElementById('n').value + '&13=' + document.getElementById('o').value + '&14=' + document.getElementById('p').value + '&15=' + document.getElementById('q').value + '&16=' + document.getElementById('j').value, 'NotificaISEIncompleta', '');
                }
                else {
                    alert('Inserire il numero di protocollo!');
                }
            }
            else {
                alert('Tutti i campi devono essere valorizzati!');
            }
        }
    
    </script>
    </form>
</body>
</html>
