<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Dettaglio.ascx.vb" Inherits="GestioneAutonoma_Tab_Dettaglio" %>



<table>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" 
                Font-Size="8pt" Font-Underline="True" Text="RAPPRESENTANTE"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table width="80%">
                <tr>
                    <td >
            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" 
                Font-Size="7pt" Font-Underline="False" Text="COGNOME*"></asp:Label>
                    </td>
                    <td>
            <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" 
                Font-Size="7pt" Font-Underline="False" Text="NOME*"></asp:Label>
                    </td>
                    <td>
            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" 
                Font-Size="7pt" Font-Underline="False" Text="COD.FISCALE"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td >
                <asp:TextBox ID="txtCognome" runat="server" Font-Names="Arial" 
                    Font-Size="8pt" Wrap="False" Width="190px" CssClass="CssMaiuscolo" MaxLength="35"></asp:TextBox>
                    </td>
                    <td>
                <asp:TextBox ID="txtnome" runat="server" Font-Names="Arial" 
                    Font-Size="8pt" Wrap="False" Width="190px" CssClass="CssMaiuscolo" MaxLength="35"></asp:TextBox>
                    </td>
                    <td>
                <asp:TextBox ID="txtcf" runat="server" Font-Names="Arial" 
                    Font-Size="8pt" Wrap="False" Width="150px" CssClass="CssMaiuscolo" MaxLength="16"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td td colspan = "3" >
            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" 
                Font-Size="7pt" Font-Underline="False" Text="INDIRIZZO RECAPITO*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan = "3">
                                    <asp:TextBox ID="txtrecapito" runat="server" Font-Names="Arial" 
                    Font-Size="8pt" Wrap="False" Width="402px" CssClass="CssMaiuscolo" MaxLength="100"></asp:TextBox>

                        </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" 
                Font-Size="8pt" Font-Underline="True" Text="COMPOSIZIONE COMITATO *"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table style="width:100%;">
                    <tr>
                        <td class="style1">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top;
                overflow: auto; width: 700px; top: 0px; height: 120px; text-align: left">
                <asp:DataGrid ID="DataGridComitato" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="692px">
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CF_PIVA" HeaderText="C.F./P.IVA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="INDIRIZZO" HeaderText="RECAPITO"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
                
            </div>
                        </td>
                        <td style="text-align: left; vertical-align: top">
            
                        <table style="width:100%;">
                            <tr>
                                <td>
            <asp:Image ID="imgAddConv" 
                onclick="OpInquil.toggle();" 
                runat="server" 
                ImageUrl="~/Condomini/Immagini/40px-Crystal_Clear_action_edit_add.png" ToolTip="Aggiungi un componente"  />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
            </table>
                        </td>
                    </tr>
                    </table>
            </td>
    </tr>
</table>

            <div id="DivAddInquilini" 
    
    
    style="top: 20px; left: 6px; width: 799px; height: 536px; position: absolute; visibility: hidden; z-index: 500;">
               <asp:Image ID="Image1" runat="server" BackColor="White" 
                    ImageUrl="~/ImmDiv/DivMGrande.png" 
                    
                    
                    Style="z-index: 100; left: 1px; position: absolute; top: 8px; height: 524px; width: 785px; margin-right: 0px;" />
                <table style="width: 89%; position: absolute; z-index: 202; height: 76%; top: 35px; left: 36px;">
                    <tr>
                        <td style="text-align: left; vertical-align: top">
                <div style="overflow: auto; width: 98%; height: 420px;" id="DivInquilini">
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                <asp:DataGrid ID="DataGridElencoInquilini" runat="server" AutoGenerateColumns="False"
                    BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" 
                    Style="z-index: 105; left: 193px; top: 54px" Width="97%" TabIndex="10" 
                        BorderColor="#000033" BorderWidth="1px" CellPadding="1" CellSpacing="1">
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle ForeColor="Black" />
                    <HeaderStyle BackColor="WhiteSmoke" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" 
                            Visible="False"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="SELEZIONA">
                            <EditItemTemplate>
                                <asp:TextBox runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkSelezione" runat="server" />
                                <asp:Label runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CF_PIVA" HeaderText="C.F./P.IVA">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
        </span></strong>
    
                </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top">
                            <asp:ImageButton ID="Aggiungi" runat="server" 
                                ImageUrl="~/Condomini/Immagini/Aggiungi.png" TabIndex="11" />
                    <img alt="Esci" src="../NuoveImm/Img_Esci.png" 
                        onclick="OpInquil.toggle();" id="ImgEsci" style="cursor:hand"/></td>
                    </tr>
                </table>
            </div>
                    
    <script type="text/javascript" language ="javascript">
        OpInquil = new fx.Opacity('DivAddInquilini', { duration: 200 });
        OpInquil.hide();
    </script>
