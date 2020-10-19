<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaRU.aspx.vb" Inherits="VSA_RicercaRU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">
    <title>Ricerca Rapporti Utenza</title>
    <style type="text/css">
        #contenitore
        {
            width: 763px;
            top: 101px;
            left: 3px;
        }
    </style>
</head>
<body bgcolor="white">
    <form id="form1" runat="server" defaultbutton="btnCerca" 
    defaultfocus="btnCerca">
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 16px; position: absolute; top: 65px">Elenco dei rapporti i cui intestatari/componenti sono presenti nella domanda che si sta inserendo.</asp:Label>
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 10px; position: absolute; top: 282px" Text="Label"
            Visible="False" Width="624px"></asp:Label>
    <div>
        &nbsp; &nbsp;&nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 705px; position: absolute; top: 351px" 
            ToolTip="Home" TabIndex="5" onclientclick="self.close();" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_Seleziona.png"
            
            Style="z-index: 111; left: 567px; position: absolute; top: 351px; " 
            ToolTip="Avvia Ricerca" TabIndex="4" />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
        <table style="left: 0px; BACKGROUND-IMAGE: url('../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 800px;
            position: absolute; top: 0px; z-index: 1;">
            <tr>
                <td style="width: 670px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        Rapporti Utenza </strong>
                    <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
                    </span><br />
                    <br />
                    <br />
                    <br />
                    <div id="contenitore" 
                        style="position: absolute; width: 764px; height: 197px; overflow: auto;"> 
        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False"
            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
            PageSize="6" 
                        
                            Style="z-index: 105; left: 0px; position: absolute; top: 0px; width: 869px; ">
            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#0000C0" />
            <Columns>
                <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="CODICE" 
                    ReadOnly="True" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="DECORRENZA" HeaderText="DECORRENZA" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
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
                <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="NOME" HeaderText="NOME" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="COD_FISCALE" HeaderText="CF" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="UNITA" 
                    ReadOnly="True" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="COMUNE" HeaderText="COMUNE" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="CAP" HeaderText="CAP_1" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="PIANO" HeaderText="PIANO" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="NETTA" HeaderText="NETTA" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="NUM_VANI" HeaderText="VANI" ReadOnly="True" 
                    Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="CODICE">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DECORRENZA">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.DECORRENZA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="UNITA">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="INDIRIZZO">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="CIVICO">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="CAP">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.CAP") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="INT.">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.INTERNO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="SCALA">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.SCALA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="COGNOME">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="NOME">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="TIPO">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.COD_TIPOLOGIA_OCCUPANTE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="VANI">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.NUM_VANI") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle Mode="NumericPages" />
            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        </asp:DataGrid>
        </div>
                    <br />
                        &nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
        <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
            Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
            Style="z-index: 2; left: 7px; position: absolute; top: 322px" Width="632px">Nessuna Selezione</asp:TextBox>
                    <br />
                    <asp:HiddenField ID="CODICE" runat="server" />
                    <asp:HiddenField ID="DECORRENZA" runat="server" />
                    <asp:HiddenField ID="COGNOME" runat="server" />
                    <asp:HiddenField ID="NOME" runat="server" />
                    <asp:HiddenField ID="CF" runat="server" />
                    <asp:HiddenField ID="CODICEUNITA" runat="server" />
                    <asp:HiddenField ID="comune" runat="server" />
                    <asp:HiddenField ID="cap" runat="server" />
                    <asp:HiddenField ID="INDIRIZZO" runat="server" />
                    <asp:HiddenField ID="civico" runat="server" />
                    <asp:HiddenField ID="interno" runat="server" />
                    <asp:HiddenField ID="SCALA" runat="server" />
                    <asp:HiddenField ID="PIANO" runat="server" />
                    <asp:HiddenField ID="ASCENSORE" runat="server" />
                    <asp:HiddenField ID="netta" runat="server" />
                    <asp:HiddenField ID="NumVani" runat="server" />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
            
    </form>
</body>
</html>

