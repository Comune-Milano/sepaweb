<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AssFiliali.aspx.vb" Inherits="CENSIMENTO_AssFiliali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assegnazione Sede Territoriale</title>
    <script type="text/javascript">
        var Selezionato;
        var OldColor;
        var SelColo;
        function ConfProcediSalva() {
            var Conferma;
            Conferma = window.confirm("Attenzione...Confermi di voler aggiornare le Sedi Territoriali degli edifici selezionati?");
            if (Conferma == false) {
                document.getElementById('ConfProcedi').value = '0';
                return false;
            }
            else {
                document.getElementById('ConfProcedi').value = '1';
            };
        };
    </script>
</head>
<body style="width: 800px; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
    position: absolute; top: 0px; left: 0px; background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Assegnazioni
                    Sede Territoriale Edifici</strong></span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="font-family: Arial; font-size: 8pt">
                            Elenco Sedi T.: &nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFiliale" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="450px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="font-family: Arial; font-size: 8pt; width: 80%;">
                            Elenco Edifici:
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSeleziona" runat="server" ImageUrl="../NuoveImm/Img_SelezionaTuttiGrande.png" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: auto; width: 97%; height: 400px">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:DataGrid ID="dgvEdifici" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                            Width="97%" ForeColor="#333333" AllowPaging="True" PageSize="100">
                            <ItemStyle BackColor="#EFF3FB" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cboggetto" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.SELEZIONE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Width="95%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                            </Columns>
                            <EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="White" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataGrid>
                    </span></strong>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 65%">
                            &nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="../NuoveImm/Img_Procedi.png"
                                OnClientClick="return ConfProcediSalva();" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                                OnClientClick="parent.main.location.replace('pagina_home.aspx');return false;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="SelezionaTutti" runat="server" Value="0" />
    <asp:HiddenField ID="ConfProcedi" runat="server" Value="0" />
    <script type="text/javascript">
        if (document.getElementById('dvvvPre') != null) {
            document.getElementById('dvvvPre').style.visibility = 'hidden';
        };
    </script>
    </form>
</body>
</html>
