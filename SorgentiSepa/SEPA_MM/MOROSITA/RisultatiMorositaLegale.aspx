<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiMorositaLegale.aspx.vb" Inherits="MOROSITA_RisultatiMorositaLegale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<script type="text/javascript" src="Funzioni.js">
<!--
var Uscita1;
Uscita1=1;
// -->
</script>

<head id="Head1" runat="server">
    <title>RISULTATI RICERCA AFFIDAMENTO </title>
</head>
<body  style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
    <form id="form1" runat="server">
    <div>
        &nbsp;&nbsp;
            <table style="left: 0px; top: 0px">
            <tr>
                <td style="width: 396px; height: 58px; left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); position: absolute; top: 0px;">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <br />
                        &nbsp;&nbsp; Risultati Ricerca <br />
                        </span></strong><table style="width: 384px">
                            <tr>
                                <td style="width: 11px; height: 5px">
                                </td>
                                <td style="height: 5px">
                                </td>
                            </tr>
                        <tr>
                            <td style="width: 11px; height: 21px">
                                &nbsp;</td>
                            <td style="height: 21px">
                                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 48px; top: 32px" Width="88px">Legale:</asp:Label><asp:DropDownList
                                        ID="cmbLegale" runat="server" AutoPostBack="True" BackColor="White" Font-Names="arial"
                                        Font-Size="10pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                        z-index: 111; left: 128px; border-left: black 1px solid; border-bottom: black 1px solid;
                                        top: 32px" TabIndex="2" Width="650px">
                                    </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 11px; height: 5px">
                            </td>
                            <td style="height: 5px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 11px">
                            </td>
                            <td>
                    <div style="left: 8px; overflow: auto; width: 750px; top: 56px;
                        height: 300px">
                        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False"
                            BackColor="White" BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="Black" PageSize="300" Style="z-index: 101; left: 9px; border-collapse:separate" Width="1087px" AllowPaging="True" BorderColor="#000099">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" Mode="NumericPages" Position="TopAndBottom" />
                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" Wrap="False" BackColor="Gainsboro" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID_MOROSITA_LETTERE" HeaderText="ID" Visible="False">
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="CODICE" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CODICE">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>&nbsp;
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INTESTATARIO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DEBITO2" HeaderText="DEBITO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_MAV" HeaderText="Tipo MAV">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="TIPO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_TIPOLOGIA_CONTR_LOC") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="POSIZIONE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POSIZIONE_CONTRATTO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COD.UNITA'">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TIPO UN.">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_TIPOLOGIA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INDIRIZZO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CIV.">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COMUNE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE_UNITA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" Wrap="False" />
                        </asp:DataGrid></div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 11px">
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSelTutti" runat="server" ImageUrl="~/MOROSITA/Immagini/Img_SelezionaTutti.png" Style="z-index: 102;
                        left: 16px; top: 856px" ToolTip="Seleziona tutte le righe della pagina" />&nbsp;
                                <asp:ImageButton ID="btnDeselTutti" runat="server" ImageUrl="~/MOROSITA/Immagini/Img_DeSelezionaTutti.png" Style="z-index: 102;
                        left: 160px; top: 392px" ToolTip="Deseleziona tutte le righe della pagina" />
                                &nbsp;
                    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                        Font-Names="Arial" Font-Size="12pt" MaxLength="100" Style="left: 328px;
                        top: 392px" Width="456px" ReadOnly="True" Font-Bold="True" Visible="False">Nessuna Selezione</asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 11px">
                            </td>
                            <td>
                    <img alt="Elenco2" src="Immagini/alert_elencoIntestatariLegale.gif" style="z-index: 109; left: 24px; top: 416px; background-color: white" />
                    <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/MOROSITA/Immagini/Img_Procedi.png" Style="z-index: 102;
                        left: 240px; top: 480px" ToolTip="Visualizza" />
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png"
                        Style="z-index: 102; right: 931px; left: 400px; top: 480px"
                        TabIndex="2" ToolTip="Esporta in Excel" />
                                &nbsp;
                    <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                        Style="z-index: 103; left: 544px; top: 480px" ToolTip="Nuova Ricerca" />
                                &nbsp;
        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
            MaxLength="100" Style="left: 480px; position: absolute; top: 896px" Width="152px"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 107; left: 616px; top: 520px" ToolTip="Home" /></td>
                        </tr>
    <tr>
        <td style="width: 11px">
        </td>
        <td>
            <asp:HiddenField ID="LBLID" runat="server" />
                    <asp:HiddenField ID="Label3" runat="server" />
        </td>
    </tr>
                    </table>
                    &nbsp;</td>
            </tr>
        </table>
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
    
    </div>
    </form>
    
     <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    
</body>
</html>

