<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UtenzeMillesimali.aspx.vb" Inherits="CENSIMENTO_UtenzeMillesimali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Utenza Millesimale</title>
</head>
<body bgColor="#ffffff">
<%--<script type="text/javascript"  > 
function fissacolore(obj){
obj.style.backgroundColor='red';
}
</script>--%>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
            Height="1px" Style="z-index: 100; left: 176px; position: absolute; top: 8px"
            Width="256px">UTENZA TABELLE MILLESIMALI</asp:Label>
        <asp:ImageButton ID="BtnDeleteUtMil" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/minus_icon.png"
            Style="left: 592px; position: absolute; top: 256px; width: 18px;" />
        &nbsp;
        <asp:Label ID="LBLID" runat="server" Font-Size="10pt" ForeColor="Black" Height="16px"
            Style="z-index: 100; left: 134px; position: absolute; top: 396px" Visible="False"
            Width="78px" TabIndex="-1">Label</asp:Label>
        <asp:Label ID="LBLDESCRIZIONE" runat="server" Font-Size="10pt" ForeColor="Black"
            Height="16px" Style="z-index: 102; left: 13px; position: absolute; top: 416px"
            Visible="False" Width="57px" TabIndex="-1">Label</asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 107; left: 11px; position: absolute; top: 396px" Visible="False" TabIndex="-1">Nessuna selezione</asp:Label>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 112px; position: absolute; top: 40px" Width="64px">TIPOLOGIA*</asp:Label>
        <asp:DropDownList ID="cmbTipologiaUtenza" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 192px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 40px" TabIndex="5" Width="288px">
        </asp:DropDownList><asp:DropDownList ID="cmbFornitore" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 192px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 70px" TabIndex="5" Width="288px">
        </asp:DropDownList>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 112px; position: absolute; top: 72px" Width="64px">FORNITORE*</asp:Label>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 112px; position: absolute; top: 104px" Width="64px">CONTATORE*</asp:Label>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 112px; position: absolute; top: 136px" Width="64px">CONTRATTO*</asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 112px; position: absolute; top: 168px" Width="64px">DESCRIZIONE*</asp:Label>
        &nbsp;
        <asp:TextBox ID="txtContatore" runat="server" MaxLength="80" Style="left: 192px; position: absolute;
            top: 104px" Width="280px"></asp:TextBox>
        <asp:TextBox ID="txtContratto" runat="server" MaxLength="80" Style="left: 192px; position: absolute;
            top: 136px" Width="280px"></asp:TextBox>
        <asp:TextBox ID="txtDescrizione" runat="server" MaxLength="150" Style="left: 192px; position: absolute;
            top: 168px" TextMode="MultiLine" Width="280px"></asp:TextBox>
        <hr style="left: 190px; width: 413px; position: absolute; top: 215px; height: 1px" />
        <asp:ImageButton ID="btnAddUtMill" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/40px-Crystal_Clear_action_edit_add.png"
            Style="left: 592px; position: absolute; top: 232px; width: 18px;" />
        &nbsp;
        <asp:ImageButton ID="ImButEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="left: 559px; position: absolute; top: 383px" ToolTip="Esci" />
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            Style="left: 489px; position: absolute; top: 383px; height: 12px;" 
            ToolTip="Salva" />
        <asp:TextBox ID="txtmia" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
            MaxLength="100" Style="left: 22px; position: absolute; top: 347px; background-color: white;" Width="296px" Font-Names="Arial" Font-Size="10pt" ForeColor="Black" TabIndex="-1" Font-Bold="True">Nessuna Selezione</asp:TextBox>
        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
            MaxLength="100" Style="left: 216px; position: absolute; top: 397px; background-color: #ffffff;" Width="152px" ForeColor="White" TabIndex="-1"></asp:TextBox>
        <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 311px; position: absolute; top: 384px; background-color: white;"
            Width="152px" ForeColor="White" TabIndex="-1"></asp:TextBox>
        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Arial" 
            Font-Size="10pt" Style="z-index: 100; left: 16px; position: absolute; top: 213px; height: 14px;"
            Width="256px">DATI UTENZA MILLESIMALI</asp:Label>
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 102; left: 20px; position: absolute; top: 367px"
            Text="Label" Visible="False" Width="600px"></asp:Label>
        <asp:TextBox ID="txtdesccost" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" ForeColor="White" MaxLength="100" Style="left: 311px; position: absolute;
            top: 401px; background-color: white" TabIndex="-1" Width="152px"></asp:TextBox>
        <div style="left: 20px; overflow: auto; width: 565px; position: absolute; top: 228px;
            height: 117px">
        <asp:DataGrid ID="DatGridUtenzaMillesim" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" EnableTheming="True" Font-Bold="False" Font-Italic="False"
            Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
            Font-Underline="False" ForeColor="Black" PageSize="4" Style="z-index: 101; left: 24px; top: 240px" Width="538px">
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
                <asp:BoundColumn DataField="ID_TABELLA_MILLESIMALE" HeaderText="ID_TABELLA_MILLESIMALE"
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID_UTENZA" HeaderText="ID_UTENZA" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="COD_TIPOLOGIA_COSTO" HeaderText="COD_TIPOLOGIA_COSTO"
                    Visible="False"></asp:BoundColumn>
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
                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="TABELLA MILL.">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE_TABELLA") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE_TABELLA") %>'></asp:TextBox>
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
