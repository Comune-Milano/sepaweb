<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiUnitaAssegnate.aspx.vb" Inherits="Contratti_RisultatiUnitaAssegnate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var Uscita;
Uscita=1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Elenco Contratti</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	</head>
	<body bgColor="#ffffff">
		<form id="Form1" method="post" runat="server" defaultbutton="btnVisualizza" 
        defaultfocus="DataGrid1">
            &nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Conferma.png"
                
                Style="z-index: 102; left: 561px; position: absolute; top: 479px; " 
                ToolTip="Conferma" meta:resourcekey="btnVisualizzaResource1" 
                TabIndex="2" />
            <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                Style="z-index: 103; left: 661px; position: absolute; top: 479px" 
                ToolTip="Nuova Ricerca" meta:resourcekey="btnRicercaResource1" TabIndex="3" />
            <table style="left: 1px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
                width: 795px; position: absolute; top: 2px; height: 460px; background-attachment: fixed; background-repeat: no-repeat;">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="14pt"
                                ForeColor="#660000" Style="z-index: 111; left: 13px; position: absolute; top: 22px"
                                Text="Risultati Ricerca" meta:resourcekey="Label1Resource1"></asp:Label>
                            &nbsp;&nbsp;</strong></span><br />
                        &nbsp;&nbsp;<br />
                        <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                            Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                            border-bottom: white 1px solid; z-index: 5; left: 8px; position: absolute; top: 420px;" Width="482px" meta:resourcekey="TextBox3Resource1" Text="Nessuna Selezione"></asp:TextBox>
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            PageSize="14" 
                            Style="z-index: 101; left: 8px; position: absolute; top: 60px" Width="763px" 
                            meta:resourcekey="DataGrid1Resource1" TabIndex="1">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Mode="NumericPages" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="INTESTATARIO" HeaderText="ITESTATARI" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_DOMANDA" HeaderText="ID_DOMANDA" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_DICHIARAZIONE" HeaderText="ID_DICHIARAZIONE" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PROVENIENZA" HeaderText="PROVENIENZA" 
                                    ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="COD.UNITA'">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TIPOLOGIA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INTESTATARIO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" meta:resourcekey="LabelResource1" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COD.FISCALE/P.IVA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CF_PIVA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CF_PIVA") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DATA ACCETTAZIONE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_ASSEGNAZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_ASSEGNAZIONE") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="N.OFFERTA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.N_OFFERTA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INDIRIZZO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text="Seleziona" meta:resourcekey="LinkButton1Resource1"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna" meta:resourcekey="LinkButton3Resource1"></asp:LinkButton><asp:LinkButton
                                            ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text="Annulla" meta:resourcekey="LinkButton2Resource1"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                        &nbsp;
                        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Height="10px" Style="z-index: 104; left: 9px; position: absolute;
                            top: 441px" Visible="False" Width="484px"></asp:Label>
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
                        <asp:HiddenField ID="txtprovenienza" runat="server" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
            <asp:label id="LBLPROGR" style="Z-INDEX: 104; LEFT: 153px; POSITION: absolute; TOP: 493px" 
                            runat="server" Width="57px" Height="23px" Visible="False" 
                            meta:resourcekey="LBLPROGRResource1" Text="Label"></asp:label>
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            &nbsp;&nbsp;
            <asp:HiddenField ID="txtdesc" runat="server" />
            <asp:HiddenField ID="txtidunita" runat="server" />
            <asp:HiddenField ID="txtIdDichiarazione" runat="server" />
            <asp:HiddenField ID="txtiddomanda" runat="server" />
        </form>
	</body>
</html>
