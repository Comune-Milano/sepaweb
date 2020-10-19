<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Servizi.ascx.vb" Inherits="NEW_CENSIMENTO_Tab_Servizi" %>
<table style="width: 645px; height: 95px">
    <tr>
        <td style="width: 80px; vertical-align: top; height: 81px; text-align: left;">
            <div 
                
                style="left: 0px; vertical-align: top; overflow: auto; width: 703px; top: 0px;
                height: 135px; text-align: left; border-right: #ccccff solid; border-top: #ccccff solid; border-left: #ccccff solid; border-bottom: #ccccff solid;">
                <asp:DataGrid ID="DgvServizi" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="RoyalBlue" 
                    Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" 
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None" 
                    PageSize="1" Style="z-index: 105; left: 8px; top: 32px" Width="98%">
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" 
                        Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                        Font-Underline="False" ForeColor="#0000C0" />
                    <Columns>
                        <asp:BoundColumn DataField="ID_TIPOLOGIA" HeaderText="ID_TIPO" 
                            Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPOSERV" HeaderText="Serv" Visible="False">
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="SERVIZIO">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.TIPOSERV") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="QUANTITA'">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.QUANT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" 
                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                        Font-Underline="False" />
                </asp:DataGrid>
            </div>
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" 
                BorderStyle="None" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                ForeColor="Black" MaxLength="100" ReadOnly="True" Style="left: 13px;
                top: 197px" Width="585px">Nessuna Selezione</asp:TextBox>
        </td>
        <td style="vertical-align: top; width: 38px; text-align: left; height: 81px;">
            <asp:Image ID="imgAddConv" onclick="myOpacServizi.toggle();"
                runat="server"
                ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/40px-Crystal_Clear_action_edit_add.png" 
                ToolTip="Aggiungi un Preventivo" style="width: 18px; cursor:pointer"  />
            <br />
            <asp:ImageButton ID="BtnElimina" runat="server" 
                ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/minus_icon.png" 
                Style="z-index: 103; left: 495px; top: 69px" TabIndex="2" 
                ToolTip="Elimina elemento selezionato dalla lista" />
        </td>
        <td style="vertical-align: top; width: 140px; height: 81px; text-align: left">
            <asp:HiddenField ID="HFtxtDesc" runat="server" />
            <asp:HiddenField ID="HFtxtId" runat="server" />
        </td>
    </tr>
</table>

 <div id="Servizi"    
    
    
    
    
    
    
    
    
    
    
    
    style="border: thin none #3366ff; left: 0px; width: 799px; position: absolute; top: 0px; height: 600px; background-color: #dedede; visibility :hidden; vertical-align: top; text-align: left; z-index: 201; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg'); margin-right: 10px;">
               <asp:Image ID="Image1" runat="server" BackColor="White" ImageUrl="~/ImmDiv/DivMGrande.png"
                    
                   
                   
                   Style="z-index: 100; left: 32px; position: absolute; top: 55px; height: 408px; width: 672px;" />
               <table style="width: 68%; position: absolute; top: 116px; left: 101px; z-index: 800; height: 72px;">
                   <tr>
                       <td class="style2">
                           <asp:Label ID="lblTipo" runat="server" Font-Bold="True" Font-Names="Arial" 
                               Font-Size="8pt" Text="Tipologia"></asp:Label>
                       </td>
                       <td class="style1">
                           <asp:DropDownList ID="cmbTipoServizi" runat="server" Width="338px">
                           </asp:DropDownList>
                       </td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="style2">
                           <asp:Label ID="lblTipo0" runat="server" Font-Bold="True" Font-Names="Arial" 
                               Font-Size="8pt" Text="Quantità"></asp:Label>
                       </td>
                       <td class="style1">
                           <asp:TextBox ID="txtQuant" runat="server" MaxLength="2"></asp:TextBox>
                       </td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="style2">
                           &nbsp;</td>
                       <td class="style1">
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="style2">
                           &nbsp;</td>
                       <td class="style1">
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="style2">
                           &nbsp;</td>
                       <td class="style1">
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="style2">
                           &nbsp;</td>
                       <td  style="text-align: right">
                           <asp:ImageButton ID="BtnSalva" runat="server" 
                               ImageUrl="~/NuoveImm/Img_SalvaVal.png" ToolTip="Salva" />
                       </td>
                       <td style="text-align: right">
            <img id="imgAnnulla" alt="Annulla" onclick="myOpacServizi.toggle();"
                    src="../NuoveImm/Img_AnnullaVal.png" 
                               style="left: 185px; cursor: pointer; top: 23px" /></td>
                   </tr>
               </table>
            </div>
            
<script type="text/javascript">
    myOpacServizi = new fx.Opacity('Servizi', { duration: 200 });
    myOpacServizi.hide();
</script>