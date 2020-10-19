<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OrdineDelGiorno.aspx.vb" Inherits="Condomini_OrdineDelGiorno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ordine del Giorno</title>
    <script language = "javascript" type = "text/javascript" >
        window.name = "modal";
        var Selezionato;
        var OldColor;
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d]/g,
            'onlynumbers': /[^\d\-\,\.]/g

        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');

        }
    </script>
</head>
<body>
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 100%; height: 95%; vertical-align: top;
        line-height: normal; top: 22px; left: 10px; background-color: #FFFFFF;">
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
        <img src='Immagini/load.gif' alt='caricamento in corso' /><br />
        <br />
        caricamento in corso...<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;
    </div>

    <form id="form1" runat="server" target ="modal">
    <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="border: thin solid #808080; text-align: center" bgcolor="#FFFFCC">
                <asp:Label ID="lblAmministratore" runat="server" Font-Names="Arial" 
                    Font-Size="10pt" style="font-weight: 700" Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                <table>
                    <tr>
                        <td style="vertical-align: top; text-align: left">
                            <div style="text-align: center; height: 400px; width: 100%; overflow: auto;">
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:DataGrid ID="DataGridOrdGiorno" runat="server" AutoGenerateColumns="False" BackColor="White"
                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="Horizontal"
                            PageSize="24" Style="z-index: 105; left: 193px; top: 54px" Width="97%"
                            CellPadding="3" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
                                    ViewStateMode="Enabled">
                            <PagerStyle Mode="NumericPages" BackColor="#E7E7FF" ForeColor="#4A3C8C" 
                                HorizontalAlign="Right" />
                            <AlternatingItemStyle BackColor="#F7F7F7" />
                            <ItemStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_CONVOCAZIONE" HeaderText="ID_CONVOCAZIONE" 
                                    Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_ORDINE" HeaderText="COD_ORDINE" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ANNO" HeaderText="ANNO" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SI_NO" HeaderText="SI_NO" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE" Visible="False">
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Estratto punti all'ordine del giorno">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="cmbOrdine" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" 
                                            Width="250px" 
                                            SelectedValue='<%# DataBinder.Eval(Container, "DataItem.COD_ORDINE") %>'>
                                        </asp:DropDownList>
                                        <asp:Label runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Anno di gestione">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAnno" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            MaxLength="4" Width="50px" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.ANNO") %>' 
                                            style="text-align: right"></asp:TextBox>
                                        <asp:Label runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Si/No">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="cmbSiNo" runat="server" Width="70px" 
                                            SelectedValue='<%# DataBinder.Eval(Container, "DataItem.SI_NO") %>'>
                                            <asp:ListItem Value="-1">---</asp:ListItem>
                                            <asp:ListItem Value="1">Si</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Note Aggiuntive">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Width="300px" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>' 
                                            Font-Names="Arial" Font-Size="8pt" Height="50px"></asp:TextBox>
                                        <asp:Label runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="#F7F7F7" />
                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:DataGrid>
                </span></strong>
                            </div>
                        </td>
                        <td style="vertical-align: top">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnAdd" runat="server" 
                                            ImageUrl="~/Condomini/Immagini/40px-Crystal_Clear_action_edit_add.png" OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Condomini/Immagini/minus_icon.png"
                            Style="z-index: 102; left: 392px; top: 387px; height: 18px;" OnClientClick="document.getElementById('splash').style.visibility = 'visible';"
                ToolTip="Elimina Elemento Selezionato"  />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <table style="width: 100%;" align="center">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                            <strong>
                                <asp:ImageButton ID="btnSalva" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Salva.png"
                                    ToolTip="Salva le informazioni inserite" 
                                style="height: 12px" OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />
                            </strong>
                        </td>
                        <td>
                            <asp:Image ID="imgEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png" Style="cursor: pointer;"
                                onclick="self.close();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="idSelected" runat="server" Value="0" />
        <asp:HiddenField ID="gestInizio" runat="server" Value="0" />
    <asp:HiddenField ID="gestFine" runat="server" Value="0" />

    </form>
        <script language="javascript" type="text/javascript">
            document.getElementById('splash').style.visibility = 'hidden';
    </script>

</body>
</html>
