<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Servizi.ascx.vb" Inherits="GestioneAutonoma_Tab_Servizi" %>



<table id="TABLE1" >
    <tr>
        <td style="vertical-align: top; width: 132px; height: 81px; text-align: left">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top;
                overflow: auto; width: 700px; top: 0px; height: 190px; text-align: left">
                <asp:DataGrid ID="DataGridServizi" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="98%" BorderWidth="1px">
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID_ESERCIZIO" HeaderText="ID_ESERCIZIO" 
                            Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_SERVIZIO" HeaderText="ID_SERVIZIO" 
                            Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_DA_FINANZIARE" 
                            HeaderText="IMPORTO DA FINANZ.">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FREQUENZA_PAGAMENTO" HeaderText="FREQUENZA">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                </asp:DataGrid></div>


            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="100"
                ReadOnly="True" Style="left: 13px; top: 197px" Width="704px">Nessuna Selezione</asp:TextBox>


        </td>
        <td style="vertical-align: top; text-align: left">
            
                        <table>
                            <tr>
                                <td>
            <asp:Image ID="imgAddServ" 
                onclick="MyOpctServ.toggle();document.getElementById('Tab_Servizi1$txtVisServ').value ='1';" 
                runat="server" 
                ImageUrl="~/Condomini/Immagini/40px-Crystal_Clear_action_edit_add.png" 
                                        ToolTip="Aggiungi un servizio"  />
                                </td>
                            </tr>

                            <tr>
                                <td>
                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Condomini/Immagini/minus_icon.png"
                             Style="z-index: 102; left: 392px; top: 387px"  
                ToolTip="Elimina Elemento Selezionato" Height="18px" />
                                </td>
                            </tr>
            </table>

        </td>
    </tr>
    </table>

<div id="DivServizi" 
    
    
    
    
    
    
    
    
    
    
    
    style="border: thin none #3366ff; width: 802px; position: absolute; height: 260px; left: 3px; top: 2px; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg'); visibility: hidden; background-color: #dedede; vertical-align: top; text-align: left; z-index: 201; margin-right: 10px;">
    &nbsp;
    &nbsp; &nbsp;<br />
    &nbsp; &nbsp;<strong><span style="color: #660000; font-family: Arial">Gestione Servizi<br />
    </span></strong><br />
    <table style="width: 95%;">
        <tr>
            <td>
                <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    Text="Seleziona Gruppo"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="cmbservizio" runat="server" BackColor="White"
                    Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid;
                    border-top: black 1px solid; z-index: 10; left: 142px; border-left: black 1px solid;
                    border-bottom: black 1px solid; top: 224px" Width="98%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                Text="Importo Autogestione *"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                Text="Importo da Finanziare *"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label23" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                Text="Ripartizione"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                Width="130px" MaxLength="12"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtImpFinanz" runat="server" Font-Names="Arial" 
                                Font-Size="8pt" Width="130px" MaxLength="12"></asp:TextBox>
                        </td>
                        <td>
                                <asp:DropDownList ID="cmbFrequenza" runat="server" Font-Names="arial" 
                                            Font-Size="8pt" 
                                            SelectedValue='<%# DataBinder.Eval(Container, "DataItem.FREQ_PAGAMENTO") %>' 
                                            TabIndex="11" Width="130px">
                                    <asp:ListItem Value="1">Mensile</asp:ListItem>
                                    <asp:ListItem Value="2">Bimestrale</asp:ListItem>
                                    <asp:ListItem Value="3">Trimestrale</asp:ListItem>
                                    <asp:ListItem Value="4">Quadrimestrale</asp:ListItem>
                                    <asp:ListItem Value="6">Semestrale</asp:ListItem>
                                    <asp:ListItem Value="12">Annuale</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: right">
                        <asp:ImageButton ID="btnAggiorna" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/APPALTI/Immagini/Next.png"
                             Style="z-index: 102; left: 392px; top: 387px" OnClientClick="document.getElementById('Tab_Servizi1$txtVisServ').value ='2';document.getElementById('splash').style.visibility = 'visible'" 
                ToolTip="Aggiungi l'elemento alla lista" />
                                &nbsp;<asp:Image ID="imgEsci" 
                onclick="MyOpctServ.toggle();document.getElementById('Tab_Servizi1$txtVisServ').value ='1';" 
                runat="server" 
                ImageUrl="~/Condomini/Immagini/Img_Esci.png" 
                                        ToolTip="Chiudi"  />
                                </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>







<asp:HiddenField ID="txtVisServ" runat="server" Value="1" />








<asp:HiddenField ID="txtSelected" runat="server" Value="1" />








