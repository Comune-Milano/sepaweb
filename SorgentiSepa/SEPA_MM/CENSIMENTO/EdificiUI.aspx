<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EdificiUI.aspx.vb" Inherits="CENSIMENTO_EdificiUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Elementi Associati</title>
</head>
	<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat :no-repeat ">
    <form id="form1" runat="server">
    <div>
        &nbsp; &nbsp;&nbsp; &nbsp;
        <table style="z-index: 99; left: 0px; 
            width: 796px; position: absolute; top: 0px">
            <tr>
                <td style="width: 670px; text-align: left">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Elenco Unità
                        Immobiliari Trovate&nbsp;<asp:Label ID="LnlNumeroRisultati" runat="server" 
                            Text="Label"></asp:Label>
                    </strong></span><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <div style="left: 7px; overflow: auto; width: 774px; position: absolute; top: 53px;
                        height: 446px">
        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
            PageSize="200" Style="z-index: 105; left: 0px; top: 56px"
            Width="97%">
            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#0000C0" />
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
                <asp:TemplateColumn HeaderText="CODICE UNITA IMMOBILIARE">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="INTERNO">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTERNO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="SCALA">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCALA") %>'></asp:Label>
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
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle Mode="NumericPages" Position="TopAndBottom" />
            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        </asp:DataGrid></div>
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
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" 
                        
                        Style="left: 4px; position: absolute; top: 502px; width: 200px; right: 592px;" Text="Label"
                Visible="False"></asp:Label>
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
        &nbsp;&nbsp;
        <asp:Label ID="LblEdificio" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" Style="z-index: 103; left: 11px; position: absolute; top: 564px"
            Width="608px" Visible="False"></asp:Label>
    
    </div>
        &nbsp;<asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
            Style="z-index: 102; left: 312px; position: absolute; top: 528px" 
        ToolTip="Visualizza" />
        &nbsp; &nbsp;
        <asp:TextBox ID="txtmia" runat="server" BackColor="White" 
        BorderColor="White" BorderStyle="None"
            Font-Names="Arial" Font-Size="10pt" MaxLength="100" Style="left: 3px; position: absolute;
            top: 480px; z-index: 101; width: 731px;" ReadOnly="True"></asp:TextBox>
        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
            MaxLength="100" Style="left: 184px; position: absolute; top: 509px; background-color: #ffffff;" Width="152px" ForeColor="White"></asp:TextBox>
        <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
            BorderStyle="None" MaxLength="100" Style="left: 11px; position: absolute; top: 499px; background-color: #ffffff;"
            Width="152px" ForeColor="White"></asp:TextBox>
        <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/NuoveImm/Img_IndietroGrande.png"
            
        Style="z-index: 102; left: 16px; position: absolute; top: 528px; right: 828px;" 
        ToolTip="Indietro" />
        <asp:ImageButton ID="btnNuovo" runat="server" ImageUrl="~/NuoveImm/Img_Nuovo.png"
            Style="z-index: 102; left: 537px; position: absolute; top: 528px" 
        ToolTip="Nuovo" />
    </form>
</body>
</html>
