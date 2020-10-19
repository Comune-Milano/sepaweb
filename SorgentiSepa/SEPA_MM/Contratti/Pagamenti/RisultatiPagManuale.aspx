<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiPagManuale.aspx.vb"
    Inherits="Contratti_Pagamenti_RisultatiPagManuale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Anagrafica Condomini</title>
    <style type="text/css">
        #form1
        {
            width: 774px;
        }
    </style>
    <script type="text/javascript">
        function Visualizza() {
            {
                if (document.getElementById('IdAnagrafica').value != 0) {
                    var left = (screen.width / 2) - (1024 / 2);
                    var top = (screen.height / 2) - (768 / 2);
                    if (document.getElementById('tipoPagamanto').value == 'R') {
                        window.open('IncassiRuolo.aspx?IDANA=' + document.getElementById('IdAnagrafica').value + '&IDCONT=' + document.getElementById('IdContratto').value + '&INDIETRO=' + document.getElementById('Indietro').value, 'PagaManualeRuolo', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=1024, height=768, top=' + top + ', left=' + left);


                    } else {
                        if (document.getElementById('tipoPagamanto').value == 'I') {
                            window.open('IncassiIngiunzioni.aspx?IDANA=' + document.getElementById('IdAnagrafica').value + '&IDCONT=' + document.getElementById('IdContratto').value + '&INDIETRO=' + document.getElementById('Indietro').value, 'PagaManualeIng', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=1024, height=768, top=' + top + ', left=' + left);

                        }
                        else {
                            window.open('PagaManuale.aspx?IDANA=' + document.getElementById('IdAnagrafica').value + '&IDCONT=' + document.getElementById('IdContratto').value + '&INDIETRO=' + document.getElementById('Indietro').value, 'PagaManuale', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=1024, height=768, top=' + top + ', left=' + left);
                        }
                    }

                }
                else {
                    alert('Selezionare una riga dalla lista dei risultati!');
                }
            }
        }
        var Selezionato;

        function TornaIndietro() {

            switch (document.getElementById('tipoPagamanto').value) {
                case "R":
                    document.location.href = "RicercaPagManuale.aspx?T=R";
                    break;
                case "I":
                    document.location.href = "RicercaPagManuale.aspx?T=I";
                    break;
                default:
                    document.location.href = "RicercaPagManuale.aspx";
                  break;
            }
            
        }
    </script>
</head>
<body style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; background-attachment: fixed">
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Risultati
                    Ricerca</span></strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: auto; width: 98%; z-index: 500; height: 409px;">
                    <asp:DataGrid Style="z-index: 105" AutoGenerateColumns="False" BackColor="White"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                        ID="dgvResult" PageSize="22" runat="server" Width="150%" AllowPaging="True">
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" ReadOnly="True"
                                Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CFIVA" HeaderText="CFIVA" ReadOnly="True" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="CONTRATTO" Visible="False">
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="COD. CONTRATTO">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="STATO CONTR.">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.STATO_CONTR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="INTESTATARIO">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DATA NASCITA">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_NASCITA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CF/P.IVA">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CFIVA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SESSO">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SESSO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TELEFONO">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TELEFONO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="RESIDENZA">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RESIDENZA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COMUNE RES.">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE_RESIDENZA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PR. RES.">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PROVINCIA_RESIDENZA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" />
                    </asp:DataGrid></div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                    Font-Bold="True" Font-Names="Arial" Font-Size="11pt" MaxLength="150" ReadOnly="True"
                    Width="600px">Nessuna Selezione</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Red" Text="Label" Visible="False" Width="580px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <table width="100%">
                    <tr>
                        <td style="text-align: left" width="70%">
                            <img onclick="TornaIndietro();" alt="NuovaRicerca" src="../../NuoveImm/Img_NuovaRicerca.png"
                                style="cursor: pointer;" title="Nuova Ricerca" /><asp:ImageButton ID="btnExport"
                                    runat="server" ImageUrl="~/Condomini/Immagini/Img_Export_Grande.png" ToolTip="Esporta in Excel" />
                        </td>
                        <td>
                            <img onclick="Visualizza();" alt="Visualizza" src="../../NuoveImm/Img_Visualizza.png"
                                style="cursor: pointer;" title="Visualizza" />
                        </td>
                        <td>
                            <img onclick="document.location.href='../pagina_home.aspx';" alt="Home" src="../../NuoveImm/Img_Home.png"
                                style="cursor: pointer;" title="Home" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        document.getElementById('divPre').style.visibility = 'hidden';
    </script>
    <asp:HiddenField ID="IdAnagrafica" runat="server" />
    <asp:HiddenField ID="CFIVA" runat="server" />
    <asp:HiddenField ID="IdContratto" runat="server" />
    <asp:HiddenField ID="Indietro" runat="server" />
    <asp:HiddenField ID="tipoPagamanto" runat="server" />
    </form>
</body>
</html>
