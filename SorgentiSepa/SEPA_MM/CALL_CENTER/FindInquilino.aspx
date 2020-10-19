<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FindInquilino.aspx.vb" Inherits="CALL_CENTER_FindInquilino" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Inquilini</title>
    <script type="text/javascript" language="javascript">
        window.name = "modal";
    </script>
    <style type="text/css">
        .style3
        {
            width: 598px;
        }
    </style>
    <script type="text/javascript">
        var Selezionato;

        function CtrlSelez() {

            if (document.getElementById('idSelected').value == 0) {

                alert('Selezionare un intestatario!');
            }

        }
    </script>
</head>
<body style="background-attachment: fixed; background-image: url('Immagini/XBackGround.gif');
    background-repeat: repeat-x;">
    <form id="form1" runat="server" target="modal">
    <br />
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <asp:Label Text="" runat="server" ID="lblTitolo" />
        </span></strong>
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: auto; height: 230px; width: 794px;">
                    <asp:DataGrid ID="DataGridInte" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CellPadding="1" CellSpacing="1" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        GridLines="None" PageSize="24" Style="z-index: 105; left: 193px; top: 54px" Width="120%">
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle ForeColor="Black" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_INTE" HeaderText="ID_INTE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CODICE_CONTRATTO" HeaderText="COD.CONTRATTO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA NAS."></asp:BoundColumn>
                            <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPO U.I."></asp:BoundColumn>
                            <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" />
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                    Font-Bold="True" Font-Names="Arial" Font-Size="8pt" MaxLength="100" ReadOnly="True"
                    Style="z-index: 500;" Width="685px">Nessuna Selezione</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 99%; text-align: left;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style3" style="text-align: right">
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/CALL_CENTER/Immagini/Aggiungi.png"
                                ToolTip="Conferma la selezione, e procedi" OnClientClick="CtrlSelez();" 
                                style="height: 12px" />
                        </td>
                        <td style="text-align: right">
                            <img id="imgExitHelp" onclick="self.close();" alt="Esci" src="../NuoveImm/Img_Esci.png"
                                style="cursor: pointer" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="idSelected" runat="server" Value="0" />
    <asp:HiddenField ID="idAnagrafica" runat="server" Value="0" />
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
