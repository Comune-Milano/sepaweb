<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_VariazioniLavori.ascx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_VariazioniLavori" %>


<table style="width: 62%; ">
    <tr>
        <td style="text-align: left; vertical-align: top;">
            <asp:Label ID="lblAPPALTI" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Text="LAVORI (QUINTO D'OBBLIGO) SULL'IMPORTO A CANONE"
                Width="661px"></asp:Label>

        </td>
        <td style="text-align: left; vertical-align: top;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="text-align: left; vertical-align: top;">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top;
                overflow: auto; width: 660px; top: 0px; height: 70px; text-align: left">
                <asp:DataGrid ID="DataGridVariazLavCanone" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="97%">
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_VARIAZIONE" HeaderText="DATA VARIAZIONE" 
                            ReadOnly="True"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PROVVEDIMENTO" HeaderText="PROVVEDIMENTO" 
                            ReadOnly="True"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PERCENTUALE" HeaderText="IMPORTO %">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" ReadOnly="True">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></div>

        </td>
        <td style="text-align: left; vertical-align: top;">
            <table>
                <tr>
                    <td>
            <asp:Image ID="imgAggiungiLavCan" 
               
                runat="server" onclick="CaricaVarLavoriCanone();"
                 ImageUrl="../../../NuoveImm/btn_Aggiungi.jpg" 
                ToolTip="Aggiungi " Style="cursor:pointer; "   />
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:ImageButton ID="btnEliminaLavCan" runat="server" 
                            OnClientClick = "CongElimVariazLavori();" ImageUrl="../../../NuoveImm/btn_Elimina.jpg"
                           
                            TabIndex="15" ToolTip="Elimina voce selezionata" 
                            CausesValidation="False" /></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>

        </td>
    </tr>
    <tr>
        <td style="text-align: left; vertical-align: top;">
            <asp:Label ID="lblAPPALTI0" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Text="LAVORI (QUINTO D'OBBLIGO) SULL'IMPORTO A CONSUMO"
                Width="663px"></asp:Label>

        </td>
        <td style="text-align: left; vertical-align: top;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="text-align: left; vertical-align: top;">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top;
                overflow: auto; width: 660px; top: 0px; height: 70px; text-align: left">
                <asp:DataGrid ID="DataGridVariazLavConsumo" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="97%">
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_VARIAZIONE" HeaderText="DATA VARIAZIONE" 
                            ReadOnly="True"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PROVVEDIMENTO" HeaderText="PROVVEDIMENTO" 
                            ReadOnly="True"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PERCENTUALE" HeaderText="IMPORTO %">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" ReadOnly="True">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></div>

        </td>
        <td style="text-align: left; vertical-align: top;">
            <table>
                <tr>
                    <td>
            <asp:Image ID="imgAggiungiLavCons" 
               
                runat="server" onclick="CaricaVarLavoriConsumo();"
                 ImageUrl="../../../NuoveImm/btn_Aggiungi.jpg" 
                ToolTip="Aggiungi " Style="cursor:pointer; "   />
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:ImageButton ID="btnEliminaLavCons" runat="server" 
                            OnClientClick = "CongElimVariazLavori();" ImageUrl="../../../NuoveImm/btn_Elimina.jpg"
                           
                            TabIndex="15" ToolTip="Elimina voce selezionata" 
                            CausesValidation="False" /></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>

        </td>
    </tr>
    <tr>
        <td style="text-align: left; vertical-align: top;">
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="100"
                ReadOnly="True" Style="left: 13px; top: 197px" Width="100%">Nessuna Selezione</asp:TextBox>

        </td>
        <td style="text-align: left; vertical-align: top;">
            &nbsp;</td>
    </tr>
</table>


            
<asp:HiddenField runat="server" ID="txtAppare" />


            
<asp:HiddenField runat="server" ID="txtTipo" Value="0" />


            
<asp:HiddenField ID="id_selected" runat="server" Value="0" />


            
<asp:HiddenField ID="txtElimina" runat="server" Value="0" />


            
            <div id="VariazioniLavori"    
    
    
    
    
    
    
    
    
    
    
    
    
    style="border: thin none #3366ff; left: -8px; width: 802px; position: absolute; top: -14px; height: 560px; background-color: #dedede; visibility : hidden; vertical-align: top; text-align: left; z-index: 201; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); margin-right: 10px;">
                <table style="width: 85%; position: absolute; top: 79px; left: 45px; z-index:600">
                    <tr>
                        <td colspan = "2">
            <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Text="LAVORI" Width="473px"></asp:Label>

                        </td>

                    </tr>
                    <tr>
                        <td class="style2" >
                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" Text="Data" Width="74px"></asp:Label>
                        </td>
                        <td>
                <asp:TextBox ID="txtData" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    Font-Names="ARIAL" Font-Size="8pt" MaxLength="10" Style="z-index: 107; left: 109px;
                    top: 67px; text-align: left" TabIndex="21" Width="70px" ToolTip="dd/MM/yyyy"></asp:TextBox>
                        &nbsp;<asp:RegularExpressionValidator ID="controllodata0" 
                        runat="server" ControlToValidate="txtData"
                            ErrorMessage="!" Font-Bold="True" ToolTip="Inserire una data valida" 
                                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
                                            Font-Size="10pt" Font-Names="arial" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>

                    </tr>
                    <tr>
                        <td class="style2"  >
                            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" Text="Provvedimento" Width="74px"></asp:Label>
                        </td>
                        <td >
                            <asp:TextBox ID="txtNote" runat="server" BackColor="White" MaxLength="180" 
                                TabIndex="31" TextMode="MultiLine" Width="539px" Height="37px" 
                                Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td colspan = "2">
            <div style="border: medium solid #ccccff; vertical-align: top;
                overflow: auto; text-align: left; height: 271px;" id="DivImpServ">
                <asp:DataGrid ID="DataGridImportiVariaz" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="97%">
                    <Columns>
                        <asp:BoundColumn DataField="ID_SERVIZIO" HeaderText="ID_SERVIZIO" 
                            Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_VOCE" HeaderText="ID_VOCE" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="VOCE">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" 
                            Visible="False"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="IMPORTO">
                            <EditItemTemplate>
                                <asp:TextBox runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtPercVariazione" runat="server" Font-Names="Arial" 
                                    Font-Size="8pt" style="text-align: right;" Width="70px"></asp:TextBox>
                                <asp:Label runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="MAXCANONE" HeaderText="MAXCANONE" 
                            Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MAXCONSUMO" HeaderText="MAXCONSUMO" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SCONTO_CANONE" HeaderText="SCONTO_CANONE" 
                            Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SCONTO_CONSUMO" HeaderText="SCONTO_CONSUMO" 
                            Visible="False"></asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" Wrap="False" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></div>

                        </td>

                        
                    </tr>

                    <tr>
                        <td class="style2" >
                            </td>
                            <td style="text-align: right"> 
                                                        <asp:ImageButton ID="btn_InserisciAppalti" runat="server" ImageUrl="Immagini/Next.png"
                                OnClientClick="document.getElementById('USCITA').value='1'"
                                Style="cursor: pointer" TabIndex="55" ToolTip="Salva" />
                            <asp:ImageButton ID="btn_ChiudiAppalti" runat="server" ImageUrl="../../../NuoveImm/Img_Esci.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Variazioni').style.visibility='hidden';"
                                Style="cursor: pointer" TabIndex="57" ToolTip="Esci senza inserire" />

                            
                            </td>

                    </tr>
                </table>
                <asp:Image ID="Image1" runat="server" BackColor="White" 
                    ImageUrl="~/ImmDiv/DivMGrande.png" 
                    
                    Style="z-index: 100; left: 16px; position: absolute; top: 52px; height: 480px; width: 770px;" />
            </div>
<script type="text/javascript">

    if (document.getElementById('Tab_VariazioniLavori1_txtAppare').value != '1') {
        document.getElementById('VariazioniLavori').style.visibility = 'hidden';
    }
    else {
        document.getElementById('VariazioniLavori').style.visibility = 'hidden';
    }

</script>

            
