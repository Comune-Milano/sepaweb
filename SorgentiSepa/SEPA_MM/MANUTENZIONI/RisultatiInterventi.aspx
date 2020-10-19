<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiInterventi.aspx.vb" Inherits="MANUTENZIONI_RisultatiInterventi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>RISULTATI RICERCA</title>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="LBLID" runat="server" Height="21px" Style="left: 455px;
            position: absolute; top: 359px" Visible="False" Width="78px">Label</asp:Label>
        &nbsp;
        <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
            Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
            Style="z-index: 503; left: 8px; position: absolute; top: 354px" Width="632px">Nessuna Selezione</asp:TextBox>
        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
            MaxLength="100" Style="left: 555px; position: absolute; top: 409px" Width="6px" Height="8px"></asp:TextBox>
        <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 555px; position: absolute; top: 385px"
            Width="5px" Height="8px"></asp:TextBox>
        &nbsp;
        <asp:Label ID="LBLPROGR" runat="server" Height="23px" Style="left: 535px;
            position: absolute; top: 359px" Visible="False" Width="57px">Label</asp:Label>
        &nbsp;&nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px; z-index: 5;">
            <tr>
                <td style="width: 670px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Risultati
                        Ricerca n.<asp:Label ID="LnlNumeroRisultati" runat="server" Text="Label"></asp:Label></strong></span><br />
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
                </td>
            </tr>
        </table>
        &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 107; left: 537px; position: absolute; top: 453px" ToolTip="Home" />
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
            ForeColor="Black" Style="z-index: 103; left: 598px; position: absolute; top: 380px"
            Width="1px" Visible="False">0</asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Style="left: 311px; position: absolute; top: 359px">Nessuna selezione</asp:Label>
        <img src="Immagini/alert_elencod2.gif" style="z-index: 109; left: 104px; position: absolute;
            top: 384px" alt ="Elenco2" />
        <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
            Style="z-index: 106; left: 312px; position: absolute; top: 453px" ToolTip="Visualizza" />
        <div style="left: 8px; overflow: auto; width: 660px; position: absolute; top: 48px;
            height: 304px; z-index: 50;">
        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            PageSize="15" Style="z-index: 101; left: 3px; top: 65px"
            Width="1197px">
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="ROWNUM" HeaderText="ROWNUM" Visible="False">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="COD. IMMOBILE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_IMMOBILE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_IMMOBILE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" 
                        Width="100px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DENOMINAZIONE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" 
                        Width="400px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="INDIRIZZO">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" 
                        Width="400px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="CIVICO">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" 
                        Width="85px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DATA INIZIO INTERVENTO">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INIZIO") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" 
                        Width="100px" />
                </asp:TemplateColumn>
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
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="TIPO INTERVENTO">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.TIPO_INTERVENTO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="500px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="TIPO SERVIZIO">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.TIPO_SERVIZIO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="500px" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="DESCRIZIONE_INTERVENTO" HeaderText="DESCRIZIONE INTERVENTO">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" 
                        Width="500px" />
                </asp:BoundColumn>
            </Columns>
            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Mode="NumericPages" Wrap="False" />
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
    
    </div>
    </form>
</body>
</html>

