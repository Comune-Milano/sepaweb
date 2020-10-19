<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicrContNonPagate.aspx.vb" Inherits="Condomini_RicrContNonPagate" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Contabilità Rendicontate e non pagate</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: 14pt;
            color: #800000;
        }

        #form1
        {
            width: 782px;
        }

    </style>
    <script language ="javascript" type ="text/javascript" >

        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d]/g,
            'onlynumbers': /[^\d\-\,\.]/g

        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');

        }
        </script>
</head>
<body style="background-attachment: fixed; background-image: url('Immagini/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                <strong>REPORT CONTABILITA&#39; RENDICONTATE E NON PAGATE</strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td  style = "font-family: Arial;font-size: 8pt;text-decoration: underline;" width="50%">
                            <strong style="text-decoration: underline; width: 25%;">ELENCO AMMINISTRATORI</strong>
                        </td>
                        <td style="font-family: Arial;font-size: 8pt;text-decoration: underline;" width="50%">
                            <strong style="width: 25%">ELENCO CONDOMINI</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <div style="height: 350px; width: 100%; overflow: auto;">
                                <asp:CheckBoxList ID="chkAmministratori" runat="server" Width="90%" CellPadding="1"
                                    CellSpacing="1" DataTextField="AMMINISTRATORE" DataValueField="ID" Font-Names="Arial"
                                    Font-Size="8pt" AutoPostBack="True" 
                                    meta:resourcekey="chkAmministratoriResource1">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                        <td style="width: 30%">
                            <div style="height: 350px; width: 100%; overflow: auto;">
                                <asp:CheckBoxList ID="chkCondomini" runat="server" Width="92%" CellPadding="1" CellSpacing="1"
                                    DataTextField="DENOMINAZIONE" DataValueField="ID" Font-Names="Arial" 
                                    Font-Size="8pt" meta:resourcekey="chkCondominiResource1">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Button ID="btnSelAmm" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                Text="Seleziona/Deseleziona Tutti" Width="172px" 
                                meta:resourcekey="btnSelAmmResource1" />
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnSelCondomini" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" Text="Seleziona/Deseleziona Tutti" Width="172px" 
                                meta:resourcekey="btnSelCondominiResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <table>
                                <tr>
                                    <td style="font-family: Arial; font-size: 8pt">
                                        Anno di Gestione da</td>
                                    <td>
                                        <asp:TextBox ID="txtAnnoInizio" runat="server" Font-Italic="False" 
                                            Font-Names="Arial" Font-Size="8pt" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="font-family: Arial; font-size: 8pt">
                                        a</td>
                                    <td>
                                        <asp:TextBox ID="txtAnnoFine" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:CheckBox ID="chkSoloUltima" runat="server" Font-Names="Arial" 
                                Font-Size="8pt" style="text-align: left" 
                                Text="Considera l'ultima rendicontata non pagata" />
                        </td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:HiddenField ID="SelAmminist" runat="server" Value="0" />
                        </td>
                        <td style="text-align: right">
                            <asp:HiddenField ID="SelCondomini" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnExportXls" runat="server" ImageUrl="~/Condomini/Immagini/Img_Export_Grande.png"
                                ToolTip="Esporta in formato XLS" 
                                meta:resourcekey="btnExportXlsResource1" />
                        </td>
                        <td>
                            <img alt="Torna alla pagina HOME" src="Immagini/Img_Home.png" style="float: right;
                                cursor: pointer" onclick="document.location.href='pagina_home.aspx';" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>

</html>
