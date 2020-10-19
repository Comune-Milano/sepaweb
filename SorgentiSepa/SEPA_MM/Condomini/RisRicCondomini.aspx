<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisRicCondomini.aspx.vb"
    Inherits="Condomini_RisRicCondomini" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Risultati Ricerca</title>
    <script type="text/javascript">
        function ApriCondominio() {
            if (document.getElementById('txtid').value != 0) {
                parent.main.location.replace('Condominio.aspx?IdCond=' + document.getElementById('txtid').value + '&CALL=AnCondomini');
            }
            else {
                alert('Selezionare un condominio da visualizzare!');
            }
        }

        var Selezionato;

    </script>
    <style type="text/css">
        #form1
        {
            width: 783px;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td style="font-family: Arial; font-size: 14pt; color: #801f1c;">
                    <strong>Risultati Ricerca Condomini</strong>
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 6pt">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <div id="risultatiRicCondomini" style="width: 99%; height: 430px; overflow: auto;">
                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                            <asp:DataGrid ID="dgvCondomini4" runat="server" AutoGenerateColumns="False" BackColor="White"
                                Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                                PageSize="24" Style="z-index: 105; left: 193px; top: 54px" Width="97%" CellPadding="1"
                                CellSpacing="1">
                                <PagerStyle Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle ForeColor="Black" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CONDOMINIO" HeaderText="CODNOMINIO" Visible="True">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COD_CONDOMINIO" HeaderText="COD. CONDOMINIO" Visible="True">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CITTA" HeaderText="CITTA'"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="AMMINIST" HeaderText="AMMINISTRATORE"></asp:BoundColumn>
                                </Columns>
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                    ForeColor="#0000C0" />
                            </asp:DataGrid>
                        </span></strong>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" MaxLength="100" ReadOnly="True"
                        Width="632px">Nessuna Selezione</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td width="20%">
                                <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="Immagini/Img_Export_Grande.png" />
                            </td>
                            <td width="20%">
                                <img alt="Visualizza Condominio" src="../NuoveImm/Img_NuovaRicerca.png" style="cursor: pointer;"
                                    onclick="document.location.href='RicercaCondominio.aspx';" />
                            </td>
                            <td style="text-align: right" width="40%">
                                <img alt="Visualizza Condominio" src="../NuoveImm/Img_Visualizza.png" style="cursor: pointer;"
                                    onclick="ApriCondominio();" />
                            </td>
                            <td style="text-align: right">
                                <img alt="" src="../NuoveImm/Img_Home.png" onclick="document.location.href='pagina_home.aspx';"
                                    style="cursor: pointer" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:HiddenField ID="txtid" runat="server" Value="0" />
                    </span></strong>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
