<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UtMillesimali.aspx.vb" Inherits="CENSIMENTO_UtMillesimali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Utenza Tabelle Millesimali</title>
</head>
	<body bgcolor="white">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 8px; position: absolute; top: 168px" Width="136px">TABELLA MILLESIMALE</asp:Label>
        <asp:DropDownList ID="cmbTabMillesimale" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 144px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 168px" TabIndex="1" Width="448px">
        </asp:DropDownList>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 8px; position: absolute; top: 200px" Width="136px">TIPOLOGIA COSTO</asp:Label>
        <asp:DropDownList ID="cmbTipolCatasto" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 144px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 200px" TabIndex="2" Width="448px">
        </asp:DropDownList>
        <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;"
            Style="z-index: 100; left: 486px; position: absolute; top: 268px" ToolTip="SALVA" TabIndex="4" />
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 8px; position: absolute; top: 232px" Width="128px">Percentuale Ripartizione</asp:Label>
        <asp:TextBox ID="txtPercRipart" runat="server" MaxLength="7" Style="left: 144px;
            position: absolute; top: 232px" Width="64px" TabIndex="3"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 224px; position: absolute; top: 240px" Width="16px">%</asp:Label>
        <asp:ImageButton ID="ImButEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="left: 557px; position: absolute; top: 268px" ToolTip="ESCI" TabIndex="5" />
        <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
            Height="1px" Style="z-index: 100; left: 104px; position: absolute; top: 8px"
            Width="288px">UTENZA TABELLE MILLESIMALI</asp:Label>
        &nbsp;
        <asp:Label ID="LBLID" runat="server" Font-Size="10pt" ForeColor="Black" Height="16px"
            Style="z-index: 100; left: 120px; position: absolute; top: 141px" Visible="False"
            Width="78px">Label</asp:Label>
        <asp:Label ID="LBLDESCRIZIONE" runat="server" Font-Size="10pt" ForeColor="Black"
            Height="16px" Style="z-index: 102; left: 200px; position: absolute; top: 141px"
            Visible="False" Width="57px">Label</asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 107; left: 1px; position: absolute; top: 140px">Nessuna selezione</asp:Label>
        <asp:ImageButton ID="BtnDeleteConsistenza" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/minus_icon.png"
            Style="left: 448px; position: absolute; top: 48px" />
        &nbsp;
        <div style="overflow: auto; width: 424px; height: 100px">
        <asp:DataGrid ID="DatGridUtenzaMillesim" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" EnableTheming="True" Font-Bold="False" Font-Italic="False"
            Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
            Font-Underline="False" ForeColor="Black" PageSize="4" Style="z-index: 101; left: 8px; top: 32px" Width="381px" Height="77px">
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
                <asp:BoundColumn DataField="ROWNUM" HeaderText="ROWNUM" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID_UTENZA" HeaderText="ID_UTENZA" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="###">
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
                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Percentuale">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PERC_RIPARTIZIONE_COSTI") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PERC_RIPARTIZIONE_COSTI") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid></div>
    
    </div>
    </form>
</body>
</html>
