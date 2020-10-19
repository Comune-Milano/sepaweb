<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettagliAppuntamenti.aspx.vb"
    Inherits="SEGNALAZIONI_Agenda_DettagliAppuntamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dettaglio Appuntamenti</title>
    <script src="js/jsfunzioni.js" type="text/javascript"></script>
    <link href="Style/Site.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function EliminaAppuntamento(id) {
            document.getElementById('daElimina').value = '1';
            var chiediConferma = window.confirm("L\'appuntamento verrà eliminato definitivamente.\nVuoi continuare?");
            if (chiediConferma == true) {
                document.getElementById('confermaGenerica').value = '1';
                document.getElementById('idSelected').value = id;
                document.getElementById('btnElimina').click();
            } else {
                document.getElementById('daElimina').value = '0';
                document.getElementById('confermaGenerica').value = '0';
                document.getElementById('idSelected').value = '-1';
            };
        };
        var Selezionato;
        var OldColor;
        var SelColo;
    </script>
</head>
<body style="background-image: url('Immagini/Sfondo.png'); background-repeat: repeat-x;">
    <div id="caricamento" style="margin: 0px; background-color: #C0C0C0; width: 100%;
        height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75');
        opacity: 0.75; background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <img alt="Caricamento" src="Immagini/load.gif" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center">
                        Caricamento . . .
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <form id="form1" runat="server">
    <div id="titolo" style="height: 30px;">
        <table style="width: 100%">
            <tr>
                <td style="width: 15%;">
                    &nbsp;
                </td>
                <td style="height: 35px; text-align: center;">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:Label ID="lbltitolo" runat="server" Text=""></asp:Label></span></strong>
                </td>
                <td style="width: 15%; text-align: right;">
                    <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" ToolTip="Esci dall'Agenda"
                        OnClientClick="self.close();return false;" />
                </td>
            </tr>
        </table>
    </div>
    <div id="contenuto" style="height: 650px; overflow: auto; position: relative; left: 10px;
        width: 98%">
        <table style="width: 100%">
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label Text="Sede territoriale" runat="server" ID="lblFiliale" Font-Names="Arial"
                        Font-Size="9pt" />
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="cmbFiliale" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Width="350px" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 100%;">
                        <tr>
                            <td style="vertical-align: top">
                                <div style="overflow: auto; height: 330px;">
                                    <asp:Label Text="Sportello 1" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    <asp:DataGrid runat="server" ID="DataGridAppuntamentiSportello1" AutoGenerateColumns="False"
                                        CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                        GridLines="None" Width="98%" CellSpacing="2">
                                        <ItemStyle BackColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                            <%--<asp:BoundColumn DataField="DATA_APPUNTAMENTO" HeaderText="DATA APPUNTAMENTO" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>--%>
                                            <asp:BoundColumn DataField="ORA_APPUNTAMENTO" HeaderText="ORA APPUNTAMENTO" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NOME" HeaderText="SEDE TERRITORIALE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <%--<asp:BoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO/MODIFICA" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>--%>
                                            <asp:BoundColumn DataField="APPUNTAMENTO_CON" HeaderText="UTENTE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="TELEFONO" HeaderText="TELEFONO 1" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="CELLULARE" HeaderText="TELEFONO 2" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL" ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NOTE" HeaderText="NOTE" ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SEGNALAZIONE" HeaderText="SEGNALAZIONE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ELIMINA" HeaderText="ELIMINAZIONE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="FL_ELIMINA" HeaderText="" Visible="false"></asp:BoundColumn>
                                        </Columns>
                                        <EditItemStyle BackColor="White" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:DataGrid>
                                    <asp:Label ID="Label1" Text="Sportello 2" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    <asp:DataGrid runat="server" ID="DataGridAppuntamentiSportello2" AutoGenerateColumns="False"
                                        CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                        GridLines="None" Width="98%" CellSpacing="2">
                                        <ItemStyle BackColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                            <%--<asp:BoundColumn DataField="DATA_APPUNTAMENTO" HeaderText="DATA APPUNTAMENTO" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>--%>
                                            <asp:BoundColumn DataField="ORA_APPUNTAMENTO" HeaderText="ORA APPUNTAMENTO" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NOME" HeaderText="SEDE TERRITORIALE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <%--<asp:BoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO/MODIFICA" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>--%>
                                            <asp:BoundColumn DataField="APPUNTAMENTO_CON" HeaderText="UTENTE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="TELEFONO" HeaderText="TELEFONO 1" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="CELLULARE" HeaderText="TELEFONO 2" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL" ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NOTE" HeaderText="NOTE" ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SEGNALAZIONE" HeaderText="SEGNALAZIONE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ELIMINA" HeaderText="ELIMINAZIONE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="FL_ELIMINA" HeaderText="" Visible="false"></asp:BoundColumn>
                                        </Columns>
                                        <EditItemStyle BackColor="White" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:DataGrid>
                                    <asp:Label ID="Label2" Text="Sportello 3" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    <asp:DataGrid runat="server" ID="DataGridAppuntamentiSportello3" AutoGenerateColumns="False"
                                        CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                        GridLines="None" Width="98%" CellSpacing="2">
                                        <ItemStyle BackColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                            <%--<asp:BoundColumn DataField="DATA_APPUNTAMENTO" HeaderText="DATA APPUNTAMENTO" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>--%>
                                            <asp:BoundColumn DataField="ORA_APPUNTAMENTO" HeaderText="ORA APPUNTAMENTO" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NOME" HeaderText="SEDE TERRITORIALE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <%--<asp:BoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO/MODIFICA" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>--%>
                                            <asp:BoundColumn DataField="APPUNTAMENTO_CON" HeaderText="UTENTE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="TELEFONO" HeaderText="TELEFONO 1" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="CELLULARE" HeaderText="TELEFONO 2" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL" ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NOTE" HeaderText="NOTE" ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SEGNALAZIONE" HeaderText="SEGNALAZIONE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ELIMINA" HeaderText="ELIMINAZIONE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="FL_ELIMINA" HeaderText="" Visible="false"></asp:BoundColumn>
                                        </Columns>
                                        <EditItemStyle BackColor="White" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:DataGrid>
                                    <asp:Label ID="Label3" Text="Sportello 4" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                    <asp:DataGrid runat="server" ID="DataGridAppuntamentiSportello4" AutoGenerateColumns="False"
                                        CellPadding="2" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                        GridLines="None" Width="98%" CellSpacing="2">
                                        <ItemStyle BackColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                            <%--<asp:BoundColumn DataField="DATA_APPUNTAMENTO" HeaderText="DATA APPUNTAMENTO" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>--%>
                                            <asp:BoundColumn DataField="ORA_APPUNTAMENTO" HeaderText="ORA APPUNTAMENTO" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NOME" HeaderText="SEDE TERRITORIALE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <%--<asp:BoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO/MODIFICA" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>--%>
                                            <asp:BoundColumn DataField="APPUNTAMENTO_CON" HeaderText="UTENTE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="TELEFONO" HeaderText="TELEFONO 1" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="CELLULARE" HeaderText="TELEFONO 2" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL" ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NOTE" HeaderText="NOTE" ItemStyle-Width="9%"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SEGNALAZIONE" HeaderText="SEGNALAZIONE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ELIMINA" HeaderText="ELIMINAZIONE" ItemStyle-Width="9%">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="FL_ELIMINA" HeaderText="" Visible="false"></asp:BoundColumn>
                                        </Columns>
                                        <EditItemStyle BackColor="White" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td style="width: 20%">
                    Data appuntamento*
                </td>
                <td style="width: 80%">
                    <asp:TextBox ID="TextBoxDataAppuntamento" runat="server" Width="70px" Font-Names="Arial"
                        Font-Size="8pt"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxDataAppuntamento"
                        ErrorMessage="!" Font-Bold="True" Font-Names="arial" Font-Size="8pt" ForeColor="#CC0000"
                        ToolTip="Modificare la data di appuntamento" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Orario appuntamento*
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="cmbOrario" Font-Names="Arial" Font-Size="8pt">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Sportello*
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="cmbSportello" Font-Names="Arial" Font-Size="8pt">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Cognome*
                </td>
                <td>
                    <asp:TextBox ID="TextBoxCognome" runat="server" MaxLength="100" Width="200px" Font-Names="Arial"
                        Font-Size="8pt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Nome
                </td>
                <td>
                    <asp:TextBox ID="TextBoxNome" runat="server" MaxLength="100" Width="200px" Font-Names="Arial"
                        Font-Size="8pt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Telefono 1*
                </td>
                <td>
                    <asp:TextBox ID="TextBoxTelefono" runat="server" MaxLength="20" Width="150px" Font-Names="Arial"
                        Font-Size="8pt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Telefono 2
                </td>
                <td>
                    <asp:TextBox ID="TextBoxCellulare" runat="server" MaxLength="20" Width="150px" Font-Names="Arial"
                        Font-Size="8pt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    E-mail
                </td>
                <td>
                    <asp:TextBox ID="TextBoxEmail" runat="server" MaxLength="100" Width="200px" Font-Names="Arial"
                        Font-Size="8pt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Note
                </td>
                <td>
                    <asp:TextBox ID="TextBoxNote" runat="server" MaxLength="100" Width="400px" Font-Names="Arial"
                        Font-Size="8pt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Stato*
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="cmbStato" Font-Names="Arial" Font-Size="8pt">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div id="footer" style="height: 100px; text-align: right">
        <asp:Button ID="ButtonSalva" runat="server" Text="Modifica appuntamento" CssClass="bottone" />
        <asp:Button ID="btnAggiornaForm" runat="server" Text="" CssClass="bottone" Style="display: none" />
        <asp:HiddenField runat="server" ID="confermaGenerica" Value="0" />
        <asp:HiddenField runat="server" ID="idSelected" Value="-1" />
        <asp:Button Text="" runat="server" ID="btnElimina" Style="display: none" />
        <asp:HiddenField runat="server" ID="HiddenFieldSportello" Value="-1" />
        <asp:HiddenField runat="server" ID="HiddenFieldOrario" Value="-1" />
        <asp:HiddenField runat="server" ID="daElimina" Value="0" />
    </div>
    </form>
    <script type="text/javascript">
        initialize();
        function initialize() {
            document.getElementById('caricamento').style.visibility = 'hidden';
            window.focus;
        };
    </script>
</body>
</html>
