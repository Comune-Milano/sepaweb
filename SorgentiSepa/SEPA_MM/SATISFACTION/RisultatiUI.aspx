<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiUI.aspx.vb" Inherits="VSA_RisultatiUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Risultati Ricerca</title>
</head>
<body bgcolor="white">
    <form id="form1" runat="server" defaultbutton="btnVisualizza" 
    defaultfocus="btnVisualizza">
    <div>
        &nbsp; &nbsp;&nbsp;&nbsp;
        <table style="left: 0px; BACKGROUND-IMAGE: url(../NuoveImm/SfondoMaschere.jpg); WIDTH: 674px;
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
                    <asp:HiddenField ID="CODICE" runat="server" />
                    <asp:HiddenField ID="comune" runat="server" />
                    <asp:HiddenField ID="cap" runat="server" />
                    <asp:HiddenField ID="INDIRIZZO" runat="server" />
                    <asp:HiddenField ID="CIVICO" runat="server" />
                    <asp:HiddenField ID="INTERNO" runat="server" />
                    <asp:HiddenField ID="SCALA" runat="server" />
                    <asp:HiddenField ID="PIANO" runat="server" />
                    <asp:HiddenField ID="ASCENSORE" runat="server" />
                    <asp:HiddenField ID="LOCALI" runat="server" />
                    <asp:HiddenField ID="netta" runat="server" />
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
        <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="../NuoveImm/Img_NuovaRicerca.png"
            Style="z-index: 106; left: 407px; position: absolute; top: 413px" ToolTip="Nuova Ricerca" />
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
            Style="z-index: 107; left: 539px; position: absolute; top: 413px" ToolTip="Home" />
        <asp:Label ID="LblEdificio" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
            ForeColor="Maroon" 
            Style="z-index: 103; left: 648px; position: absolute; top: 21px; width: 8px;" 
            Visible="False"></asp:Label>
    
    </div>
        &nbsp;<asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="../NuoveImm/Img_Seleziona.png"
            Style="z-index: 102; left: 209px; position: absolute; top: 413px" 
        ToolTip="Visualizza" />
        &nbsp;
        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
            PageSize="13" Style="z-index: 105; left: 8px; position: absolute; top: 64px"
            Width="660px">
            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#0000C0" />
            <Columns>
                <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD.UNI.IMMOB" 
                    ReadOnly="True" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="COMUNE" HeaderText="COMUNE" Visible="False" 
                    ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="CAP" HeaderText="CAP" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="SCALA" HeaderText="SCALA" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="PIANO" HeaderText="PIANO" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="NETTA" HeaderText="NETTA" Visible="False">
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="COD. UNITA">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="EDIFICIO">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="FOGLIO">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FOGLIO") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FOGLIO") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="PART.">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NUMERO") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NUMERO") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="SUB">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SUB") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="INT.">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTERNO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="SCALA">
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCALA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle Mode="NumericPages" />
            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        </asp:DataGrid>
        &nbsp;
        <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
            Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
            Style="z-index: 2; left: 7px; position: absolute; top: 305px" Width="632px">Nessuna Selezione</asp:TextBox>
        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
            MaxLength="100" Style="left: 13px; position: absolute; top: 456px; background-color: white;" Width="152px" ForeColor="White"></asp:TextBox>
        <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 14px; position: absolute; top: 435px; background-color: white;"
            Width="152px" ForeColor="White"></asp:TextBox>
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 8px; position: absolute; top: 328px" Text="Label"
            Visible="False" Width="624px"></asp:Label>
    </form>
                <script  language="javascript" type="text/javascript">
                    var Selezionato;
    document.getElementById('dvvvPre').style.visibility='hidden';
    </script>
</body>
</html>