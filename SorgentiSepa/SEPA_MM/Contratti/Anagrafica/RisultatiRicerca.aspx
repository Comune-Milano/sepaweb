<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiRicerca.aspx.vb" Inherits="Contratti_Anagrafica_RisultatiRicerca" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var Uscita;
Uscita=0;
function TABLE1_onclick() {

}

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
		<form id="Form1" method="post" runat="server">
            &nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 101; left: 660px; position: absolute; top: 596px" ToolTip="Home" meta:resourcekey="btnAnnullaResource1" />
            <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                Style="z-index: 102; left: 248px; position: absolute; top: 387px" 
                ToolTip="Visualizza" meta:resourcekey="btnVisualizzaResource1" TabIndex="1" />
            <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                Style="z-index: 103; left: 249px; position: absolute; top: 417px" 
                ToolTip="Nuova Ricerca" meta:resourcekey="btnRicercaResource1" 
                TabIndex="2" onclientclick="document.location.href='Ricerca.aspx';" 
                Visible="False" />
            <asp:label id="LBLPROGR" style="Z-INDEX: 104; LEFT: 250px; POSITION: absolute; TOP: 631px" runat="server" Width="57px" Height="23px" Visible="False" meta:resourcekey="LBLPROGRResource1" Text="Label"></asp:label>
            <table style="left: 1px; background-image: url(../../NuoveImm/SfondoMascheraRubrica.jpg);
                width: 501px; position: absolute; top: 2px; height: 460px; background-attachment: fixed; background-repeat: no-repeat;" id="TABLE1" onclick="return TABLE1_onclick()">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="14pt"
                                ForeColor="#660000" Style="z-index: 111; left: 13px; position: absolute; top: 22px"
                                Text="Risultati Ricerca" meta:resourcekey="Label1Resource1"></asp:Label>
                            &nbsp;&nbsp;</strong></span><br />
                        &nbsp;&nbsp;<br />
                        <img onclick="document.location.href='Ricerca.aspx';" alt="" src="../../NuoveImm/Img_NuovaRicerca.png" 
                            style="cursor:pointer;position:absolute; top: 386px; left: 349px; "/><asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="16pt"
                            
                            Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                            border-bottom: white 1px solid; z-index: 5; left: 8px; position: absolute; top: 328px;" 
                            Width="482px" meta:resourcekey="TextBox3Resource1" Text="Nessuna Selezione"></asp:TextBox>
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            PageSize="14" 
                            Style="z-index: 101; left: 8px; position: absolute; top: 60px" Width="486px" 
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
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="INTESTATARIO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" meta:resourcekey="LabelResource1" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COD. FISCALE/P.IVA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" meta:resourcekey="LabelResource2" Text='<%# DataBinder.Eval(Container, "DataItem.FISCALE_PIVA") %>'></asp:Label>
                                    </ItemTemplate>
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
                        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
                            MaxLength="100" Style="left: 528px; position: absolute; top: 576px" Width="152px" meta:resourcekey="txtidResource1"></asp:TextBox>
                        <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
                            BorderStyle="None" MaxLength="100" Style="left: 528px; position: absolute; top: 552px"
                            Width="152px" meta:resourcekey="txtdescResource1"></asp:TextBox>
                        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Style="z-index: 104; left: 9px; position: absolute;
                            top: 355px; height: 26px;" Visible="False" Width="484px"></asp:Label>
                    </td>
                </tr>
            </table>
            &nbsp;&nbsp;</form>
	</body>
</html>
