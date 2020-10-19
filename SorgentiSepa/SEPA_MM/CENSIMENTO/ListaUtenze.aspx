<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListaUtenze.aspx.vb" Inherits="CENSIMENTO_ListaUtenze" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Lista Utenze</title>
</head>
<body bgColor="white">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
            Height="1px" Style="z-index: 100; left: 8px; position: absolute; top: 8px"
            Width="128px">LISTA UTENZE</asp:Label>
        &nbsp;
        <asp:Label ID="LBLID" runat="server" Font-Size="10pt" ForeColor="Black" Height="16px"
            Style="z-index: 100; left: 136px; position: absolute; top: 240px" Visible="False"
            Width="78px">Label</asp:Label>
        <asp:Label ID="LBLDESCRIZIONE" runat="server" Font-Size="10pt" ForeColor="Black"
            Height="16px" Style="z-index: 102; left: 216px; position: absolute; top: 240px"
            Visible="False" Width="57px">Label</asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 107; left: 10px; position: absolute; top: 241px" Visible="False">Nessuna selezione</asp:Label>
        <asp:ImageButton ID="ImButEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="left: 520px; position: absolute; top: 214px" ToolTip="Esci" TabIndex="4" />
        <asp:ImageButton ID="BtnDeleteConsistenza" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/minus_icon.png"
            Style="left: 584px; position: absolute; top: 80px" ToolTip="ELIMINA" TabIndex="2" />
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/40px-Crystal_Clear_action_edit_add.png"
            Style="left: 584px; position: absolute; top: 56px" ToolTip="AGGIUNGI" TabIndex="1" />
        <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_VisualizzaPiccolo.png"
            Style="z-index: 102; left: 438px; position: absolute; top: 214px" ToolTip="Visualizza" TabIndex="3" />
        &nbsp;
        <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
            Font-Bold="True" Font-Names="Arial" Font-Size="10pt" MaxLength="100" ReadOnly="True"
            Style="left: 8px; position: absolute; top: 193px" Width="352px">Nessuna Selezione</asp:TextBox>
        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
            MaxLength="100" Style="left: 279px; position: absolute; top: 237px; background-color: white;" Width="152px" ForeColor="White"></asp:TextBox>
        <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 10px; position: absolute; top: 236px; background-color: white;"
            Width="152px" ForeColor="White"></asp:TextBox>
        <div style="left: 8px; vertical-align: top; overflow: auto; width: 573px; position: absolute;
            top: 43px; height: 149px; text-align: left">
        <asp:DataGrid ID="DatGridUtenzaMillesim" runat="server" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" EnableTheming="True" Font-Bold="False" Font-Italic="False"
            Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
            Font-Underline="False" ForeColor="Black" PageSize="5" Style="z-index: 101;
            left: 8px; top: 32px" Width="551px">
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
            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="CONTRATTO" HeaderText="CONTRATTO" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="###" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                            Font-Names="Wingdings" Text="o"></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                            ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                            Text="Annulla"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="TIPOLOGIA">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_TIPOLOGIA") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_TIPOLOGIA") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="FORNITORE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FORNITORE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FORNITORE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="CONTATORE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CONTATORE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CONTATORE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="CONTRATTO">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CONTRATTO") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CONTRATTO") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid></div>
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 102; left: 141px; position: absolute; top: 11px"
            Text="Label" Visible="False" Width="441px"></asp:Label>
    
    </div>
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
