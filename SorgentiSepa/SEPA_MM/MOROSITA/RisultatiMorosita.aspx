<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiMorosita.aspx.vb" Inherits="MOROSITA_RisultatiMorosita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<script type="text/javascript" src="Funzioni.js">
<!--
var Uscita1;
Uscita1=1;
// -->
</script>

<head id="Head1" runat="server">
    <title>RISULTATI RICERCA MOROSITA</title>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server">
    <div>
        &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
                width: 800px; position: absolute; top: 0px;">
            <tr>
                <td style="width: 800px; height: 51px; left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); position: absolute; top: 0px;">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <br />
                        &nbsp;&nbsp; Risultati Ricerca n.<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
&nbsp;</span></strong><br />
                    <br />
                    <br />
                    <br />
                    <div style="left: 8px; overflow: auto; width: 784px; position: absolute; top: 56px;
                        height: 320px">
        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            PageSize="1" Style="z-index: 101; left: 0px; top: 8px; table-layout: auto; clip: rect(auto auto auto auto); direction: ltr; border-collapse: separate; position: absolute;"
            Width="98%" AllowSorting="True" ForeColor="Black" BorderColor="#000099">
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="ID_MOROSITA" HeaderText="ID_MOROSITA" Visible="False">
                    <HeaderStyle Width="0%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PROGR" HeaderText="NUM.">
                    <HeaderStyle Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PROTOCOLLO_ALER" HeaderText="NUM. PROTOCOLLO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_PROTOCOLLO" HeaderText="DATA PROTOCOLLO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Width="7%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TIPO_INVIO" HeaderText="TIPO INVIO">
                    <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" 
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                        HorizontalAlign="Left" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_DAL" HeaderText="DEBITO DAL">
                    <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" 
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                        HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_AL" HeaderText="DEBITO AL">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Width="30%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FILE_MOROSITA" HeaderText="File Morosità">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                        Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                        Wrap="False" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                            Text="Modifica">Seleziona</asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                            ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                            Text="Annulla"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="FL_ANNULLATA" HeaderText="FL_ANNULLATA" 
                    Visible="False"></asp:BoundColumn>
            </Columns>
            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" Position="TopAndBottom" Visible="False" />
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
        </asp:DataGrid></div>
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
                    <br />
                    <br />
                    &nbsp;<br />
                    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                        Font-Names="Arial" Font-Size="12pt" MaxLength="100" Style="left: 16px; position: absolute;
                        top: 392px" Width="768px" ReadOnly="True" Font-Bold="True">Nessuna Selezione</asp:TextBox>
                    <br />
                    <br />
                    <img alt="Elenco2" src="Immagini/alert_elencoMorosita.gif" style="z-index: 109; left: 16px;
                        position: absolute; top: 424px; background-color: white" />
                    <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                        Style="z-index: 103; left: 512px; position: absolute; top: 480px" ToolTip="Nuova Ricerca" />
                    <br />
                    <br />
                    <br /><asp:ImageButton ID="btnStampa" runat="server" ImageUrl="~/MOROSITA/Immagini/Img_Stampa2.png"
                        Style="z-index: 103; left: 368px; position: absolute; top: 480px" ToolTip="Stampa lista risultato" />
                </td>
            </tr>
        </table>
        &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 107; left: 648px; position: absolute; top: 480px" ToolTip="Home" />
        &nbsp;&nbsp;
        <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
            Style="z-index: 106; left: 224px; position: absolute; top: 480px" ToolTip="Visualizza" />
        &nbsp;
        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
            MaxLength="100" Style="left: 544px; position: absolute; top: 576px" Width="152px"></asp:TextBox>
        &nbsp;
    
    </div>

    <script  language="javascript" type="text/javascript">
             document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    
    </form>

    </body>
</html>

