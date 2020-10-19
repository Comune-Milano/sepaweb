<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoDistinte.aspx.vb" Inherits="CAMBI_RisultatoDistinte" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Elenco Distinte</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" align="center" border="1" cellpadding="1" cellspacing="1" style="z-index: 100;
            left: 0px; position: static; top: 0px" width="95%">
            <tr>
                <td align="middle" bgcolor="#ffffff" style="text-align: left" valign="center">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                        ForeColor="Navy">Elenco Distinte trovate</asp:Label></td>
            </tr>
            <tr>
                <td align="middle" bgcolor="#ffffff" style="text-align: center" valign="center">
                    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        Font-Names="Arial" Font-Size="8pt" PageSize="13" Width="593px">
                        <PagerStyle Mode="NumericPages" />
                        <HeaderStyle BackColor="PapayaWhip" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
                        <Columns>
                            <asp:BoundColumn DataField="NUMERO" HeaderText="NUMERO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="OPERATORE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="NUMERO">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NUMERO") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NUMERO") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DATA">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_DIS") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_DIS") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="OPERATORE">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                        Text="Modifica">Seleziona</asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                        Text="Annulla"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid></td>
            </tr>
            <tr>
                <td align="middle" bgcolor="#eceff2" style="text-align: center" valign="center">
                    <asp:Label ID="lbl1" runat="server" BorderColor="Black" BorderStyle="None" Font-Bold="True"
                        Font-Names="Arial" Font-Size="XX-Small" ForeColor="Blue">Selezionare la Distinta da visualizzare e premere il pulsante "VISUALIZZA"</asp:Label></td>
            </tr>
            <tr>
                <td align="left" bgcolor="#ffffff" valign="center">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="12pt">Nessuna selezione</asp:Label></td>
            </tr>
            <tr>
                <td align="middle" bgcolor="#eceff2" style="text-align: center" valign="center">
                    <asp:Button ID="btnVisualizza" runat="server" Height="32px" TabIndex="1" Text="Visualizza"
                        Width="70px" />
                    &nbsp;
                    <asp:Button ID="btnRicerca" runat="server" Height="32px" TabIndex="2" Text="Nuova Ricerca" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnChiudi" runat="server" CausesValidation="False" Height="28px"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Text="Chiudi" /></td>
            </tr>
        </table>
    
    </div>
        <asp:Label ID="LBLID" runat="server" Height="21px" Style="z-index: 100; left: 99px;
            position: absolute; top: 374px" Visible="False" Width="78px">Label</asp:Label>
        <asp:Label ID="LBLPROGR" runat="server" Height="23px" Style="z-index: 102; left: 29px;
            position: absolute; top: 377px" Visible="False" Width="57px">Label</asp:Label>
    </form>
</body>
</html>
