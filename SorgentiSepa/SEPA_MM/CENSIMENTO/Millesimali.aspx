<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Millesimali.aspx.vb" Inherits="CENSIMENTO_Millesimali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
	<head>
		<title>RisultatoRicercaD</title>
	</head>
	<body bgColor="white">
		<form id="Form1" method="post" runat="server">
			<asp:label id="LBLID" style="Z-INDEX: 100; LEFT: 136px; POSITION: absolute; TOP: 648px" runat="server" Width="78px" Height="21px" Visible="False">Label</asp:label>
            &nbsp; &nbsp;
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                Style="z-index: 101; left: 401px; position: absolute; top: 220px" 
                ToolTip="Esci" TabIndex="3" />
            &nbsp; &nbsp;
            <asp:label id="LBLPROGR" style="Z-INDEX: 104; LEFT: 216px; POSITION: absolute; TOP: 648px" runat="server" Width="57px" Height="23px" Visible="False">Label</asp:label>
            &nbsp;
                        <asp:label id="Label2" runat="server" Font-Names="Arial" Font-Bold="True" style="z-index: 107; left: 16px; position: absolute; top: 648px" Font-Size="10pt">Nessuna selezione</asp:label>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Style="z-index: 107;
                left: 8px; position: absolute; top: 8px">VALORI MILLESIMALI</asp:Label>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/minus_icon.png"
                Style="z-index: 103; left: 474px; position: absolute; top: 72px" 
                ToolTip="ELIMINA" TabIndex="2" Visible="False" />
            &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="10pt" MaxLength="100" Style="left: 5px; position: absolute;
                top: 205px" Width="352px" TabIndex="-1"></asp:TextBox>
            <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
                MaxLength="100" Style="left: 7px; position: absolute; top: 227px; background-color: #ffffff;" Width="152px" ForeColor="White" TabIndex="-1"></asp:TextBox>
            <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" MaxLength="100" Style="left: 153px; position: absolute; top: 221px; background-color: #ffffff;"
                Width="152px" ForeColor="White" TabIndex="-1"></asp:TextBox>
            <asp:ImageButton ID="BtnADD" runat="server" Height="16px" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/40px-Crystal_Clear_action_edit_add.png"
                Style="z-index: 103; left: 474px; position: absolute; top: 48px" ToolTip="AGGIUNGI"
                Width="16px" TabIndex="1" Visible="False" />
            <div style="left: 5px; overflow: auto; width: 460px; position: absolute; top: 33px;
                height: 141px; vertical-align: top; text-align: left;">
            <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False"
                BackColor="White" BorderColor="Black" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                Height="126px" PageSize="15" Style="z-index: 105; left: 7px;
                top: 36px" Width="437px" GridLines="None">
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
                    <asp:BoundColumn DataField="ROWNUM" HeaderText="ROWNUM" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="VALORE_MILLESIMO" HeaderText="VALORE_MILLESIMO" Visible="False">
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="DESCRIZIONE">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="VALORE">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VALORE_MILLESIMO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn Visible="False">
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
            </asp:DataGrid></div>
            <p>
                &nbsp;</p>
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" 
                Style="left: 7px; position: absolute; top: 178px; width: 434px;" Text="Label"
                Visible="False"></asp:Label>
        </form>
	</body>
</html>