<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Scale.aspx.vb" Inherits="CENSIMENTO_Scale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
	<head>
		<title>Gestione Scale</title>
	</head>
	<body bgColor="white">
		<form id="Form1" method="post" runat="server">
			<asp:label id="LBLID" style="Z-INDEX: 100; LEFT: 168px; POSITION: absolute; TOP: 544px" runat="server" Width="78px" Height="21px" Visible="False">Label</asp:label>
            &nbsp; &nbsp;
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                Style="z-index: 101; left: 270px; position: absolute; top: 204px" ToolTip="Esci" TabIndex="3" />
            &nbsp; &nbsp;
            <asp:label id="LBLPROGR" style="Z-INDEX: 104; LEFT: 248px; POSITION: absolute; TOP: 544px" runat="server" Width="57px" Height="23px" Visible="False">Label</asp:label>
            &nbsp;
            <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                BackColor="White" BorderColor="Black" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                Height="104px" PageSize="4" Style="z-index: 105; left: 8px; position: absolute;
                top: 40px" Width="296px" GridLines="None">
                <PagerStyle Mode="NumericPages" />
                <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="MediumBlue" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ReadOnly="True"
                        Visible="False"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="DESCRIZIONE">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                Text="Modifica">Seleziona</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                Text="Annulla"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            </asp:DataGrid>
                        <asp:label id="Label2" runat="server" Font-Names="Arial" Font-Bold="True" style="z-index: 107; left: 16px; position: absolute; top: 544px" Visible="False">Nessuna selezione</asp:label>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Style="z-index: 107;
                left: 8px; position: absolute; top: 16px">GESTIONE SCALE</asp:Label>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/minus_icon.png"
                
                Style="z-index: 103; left: 312px; position: absolute; top: 48px; width: 18px;" 
                ToolTip="ELIMINA" TabIndex="4" />
            &nbsp;
            <hr style="left: 80px; width: 288px; position: absolute; top: 184px; height: 1px" />
            <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 8px; position: absolute; top: 176px" Width="64px">INSERIMENTO</asp:Label>
            <asp:TextBox ID="TxtScala" runat="server" Style="left: 72px; position: absolute;
                top: 200px" MaxLength="4" Width="80px" TabIndex="1"></asp:TextBox>
            <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 8px; position: absolute; top: 200px" Width="64px">Nuova Scala</asp:Label>
            <asp:ImageButton ID="BtnSave" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                Style="z-index: 103; left: 200px; position: absolute; top: 204px" ToolTip="Salva" TabIndex="2" />
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="10pt" MaxLength="100" Style="left: 8px; position: absolute;
                top: 152px" Width="352px"></asp:TextBox>
            <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
                MaxLength="100" Style="left: 162px; position: absolute; top: 241px; background-color: white;" Width="152px" ForeColor="White"></asp:TextBox>
            <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" MaxLength="100" Style="left: 4px; position: absolute; top: 242px; background-color: white;"
                Width="152px" ForeColor="White"></asp:TextBox>
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 102; left: 7px; position: absolute; top: 223px"
                Text="Label" Visible="False" Width="295px"></asp:Label>
        </form>
                    <script type="text/javascript">
                self.focus();
            </script>
	</body>
</html>