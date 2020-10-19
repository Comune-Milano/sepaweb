<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Approvazione.aspx.vb" Inherits="Contratti_ContCalore_Approvazione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1
        {
            width: 784px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function ConfermaOperazione() {
            var Conferma;
            Conferma = window.confirm("Attenzione... In alcuni casi l'operazione potrebbe richiedere alcuni minuti. Continuare?");
            if (Conferma == true) {
                document.getElementById('Conferma').value = '1';
            }
            else {
                document.getElementById('Conferma').value = '0';
            }
        }
    </script>
</head>
<body style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; background-attachment: fixed">
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 100%; height: 99%; vertical-align: top;
        line-height: normal; top: 22px; left: 0px; background-color: #FFFFFF;">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <img src='../../CONDOMINI/Immagini/load.gif' alt='caricamento in corso' /><br />
        <br />
        caricamento in corso...<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;
    </div>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"
                    ForeColor="#801F1C" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width: 35%" nowrap="nowrap">
                            <asp:Label ID="lblAnni" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                Text="Label"></asp:Label>
                        </td>
                        <td style="width: 5%">
                            <asp:DropDownList ID="cmbAnniApprovabili" runat="server" Font-Names="Arial" Font-Size="10pt"
                                Font-Bold="True" Style="text-align: center" AutoPostBack="True" Width="70px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 55%">
                            &nbsp;
                        </td>
                        <td style="width: 5%; text-align: right;">
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../../NuoveImm/xlsExport.png"
                                OnClientClick="ConfermaOperazione();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: auto; width: 100%; height: 441px">
                    <asp:DataGrid ID="dgvApprovazione" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                        Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                        Font-Strikeout="False" Font-Underline="False" PageSize="250" Style="z-index: 105;
                        left: 8px; top: 32px" Width="99%" CellPadding="4" ForeColor="#333333" AllowPaging="True"
                        ToolTip="Elenco degli aventi diritto al Contributo Calore">
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="White" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditItemStyle BackColor="#2461BF" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Position="TopAndBottom"
                            Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_CONT_CALORE" HeaderText="ID_CONT_CALORE" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD.CONTRATTO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ANNO" HeaderText="ANNO">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPO_CALCOLO" HeaderText="TIPO_CALCOLO" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_RICONOSCIUTO" HeaderText="IMPORTO">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="CONDOMINIO">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbCondominio" runat="server" Width="300px" Font-Names="Arial"
                                        Font-Size="8pt">
                                    </asp:DropDownList>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SELEZIONA">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    <asp:Button ID="btnSelectAll" runat="server" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="8pt" OnClick="btnSelectAll_Click" OnClientClick="document.getElementById('splash').style.visibility = 'visible';"
                                        Text="SELEZIONA" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSel" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.SELECTED") %>' />
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ID_CONDOMINIO" HeaderText="ID_CONDOMINIO" Visible="False">
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 25%">
                            &nbsp;
                        </td>
                        <td style="text-align: center">
                            <asp:Button ID="btnAvviaCalcolo" runat="server" Text="APPROVA" OnClientClick="document.getElementById('splash').style.visibility = 'visible';ConfCreazione();"
                                BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" />
                        </td>
                        <td style="width: 25%">
                            <center>
                                <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="../../NuoveImm/Img_Home.png"
                                    OnClientClick="parent.main.location.replace('../pagina_home.aspx');return false;" /></center>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="Selezionati" runat="server" Value="0" />
                <asp:HiddenField ID="ConfApprovazione" runat="server" Value="0" />
                <asp:HiddenField ID="idContCalore" runat="server" Value="0" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="Conferma" runat="server" Value="0" />
    </form>
    <script type="text/javascript" language="javascript">
        document.getElementById('splash').style.visibility = 'hidden';
        function ConfCreazione() {
            if (window.confirm('Sei sicuro di voler approvare gli importi selezionati per il contributo calore?')) {
                document.getElementById("ConfApprovazione").value = 1;
            }
            else {
                document.getElementById("ConfApprovazione").value = 0;

            }


        };


    </script>
</body>
</html>
