<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiUI.aspx.vb" Inherits="CENSIMENTO_RisultatiUI2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script  language="javascript" type="text/javascript">
                    var selezionato;
                    </script>
<head id="Head1" runat="server">
    <title>Risultati Ricerca</title>
</head>
<body bgcolor="white">
    <form id="form1" runat="server" defaultbutton="btnVisualizza" 
    defaultfocus="btnVisualizza">
    <div>
        &nbsp; &nbsp;&nbsp;&nbsp;
        <table style="left: 0px; BACKGROUND-IMAGE: url(../NuoveImm/SfondoMascheraContratti.jpg); WIDTH: 798px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 670px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                        Unità Imm. Trovate<asp:Label ID="LnlNumeroRisultati" runat="server" 
                            Text="Label"></asp:Label>
                    </strong></span><br />
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
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
            Style="z-index: 106; left: 525px; position: absolute; top: 516px" 
            ToolTip="Nuova Ricerca" />
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 107; left: 667px; position: absolute; top: 515px" ToolTip="Home" />
    
    </div>
        &nbsp;<asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
            Style="z-index: 102; left: 389px; position: absolute; top: 516px" 
        ToolTip="Visualizza" />
        &nbsp;

         <div id="contenitore" 
            
            
           style="position: absolute; width: 770px; height: 317px; overflow: auto; top: 93px; left: 13px;">
            <asp:DataGrid ID="Datagrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                PageSize="50" 
                    Style="z-index: 101; left: 0px; width: 748px; position:absolute; top: 0px; " 
                    TabIndex="1" BorderWidth="0px">
               
                  <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD.UNI.IMMOB" 
                    ReadOnly="True" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ROWNUM" HeaderText="ROWNUM" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="COD. UNITA">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="EDIFICIO">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="FOGLIO">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FOGLIO") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FOGLIO") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="PART.">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NUMERO") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NUMERO") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="SUB">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SUB") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="INT.">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTERNO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="SCALA">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCALA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn Visible="False">
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
                <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#0000C0" Wrap="False" />
                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        </asp:DataGrid>
        </div>
        &nbsp;
        <asp:TextBox ID="txtmia" runat="server" BackColor="White" 
        BorderColor="White" BorderStyle="None"
            Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
            Style="z-index: 2; left: 7px; position: absolute; top: 449px" 
        Width="632px">Nessuna Selezione</asp:TextBox>
        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
            MaxLength="100" Style="left: 13px; position: absolute; top: 456px; background-color: white;" Width="152px" ForeColor="White"></asp:TextBox>
        <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 14px; position: absolute; top: 435px; background-color: white;"
            Width="152px" ForeColor="White"></asp:TextBox>
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" 
        Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 11px; position: absolute; top: 474px" Text="Label"
            Visible="False" Width="624px"></asp:Label>
    </form>
                <script  language="javascript" type="text/javascript">
                    
    document.getElementById('dvvvPre').style.visibility='hidden';
    </script>
</body>
</html>
