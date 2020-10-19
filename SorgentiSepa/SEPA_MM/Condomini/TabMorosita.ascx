<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TabMorosita.ascx.vb" Inherits="Condomini_TabMorosita" %>
<style type="text/css">

    .style1
    {
        font-family: arial;
    }
    .style2
    {
        font-family: arial;
        font-size: 10pt;
    }

    </style>
<table style="width: 90%; ">
    <tr>
        <td style="vertical-align: top; width: 100%; height: 81px; text-align: left">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top;
                overflow: auto; width: 703px; top: 0px; height: 260px; text-align: left">
                <asp:DataGrid ID="DataGridMorosita" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="691px">
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RIF_DA" HeaderText="RIF_DAl" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RIF_A" HeaderText="RIF_AL" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FL_COMPLETO" HeaderText="FL_COMPLETO" 
                            Visible="False">
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="AMMINISTRATORE">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.AMMINISTRATORE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DATA ARRIVO">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.DATA_ARRIVO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="RIFERIMENTO DAL">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.RIF_DA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="RIFERIMENTO AL">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.RIF_A") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="M_MESSAGE" HeaderText="M_MESSAGE" Visible="False">
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></div>
            <asp:HiddenField ID="txtidMorosita" runat="server" Value="0" />
            <asp:HiddenField ID="txtConfElimina" runat="server" Value="0" />
            <asp:HiddenField ID="txtConfMav" runat="server" Value="0" />
            <asp:HiddenField ID="txtConfpayment" runat="server" Value="0" />
    <div 
        style="border: thin solid #FF0000; position: absolute; z-index: 500; top: 17px; left: 9px; height: 525px; width: 785px; background-color: #E8EAEC; visibility: hidden; overflow: auto;" 
        id="DivDescrizione">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <table style="width:100%;">
                        <tr>
                <td class="style2">
                    <strong>Data Scadenza</strong></td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:TextBox ID="txtDScadenza" runat="server" Width="100px" Font-Names="Arial" 
                        Font-Size="8pt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <strong>IBAN del Fornitore
                </strong>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:DropDownList ID="cmbIbanFornitore" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="350px">
                    </asp:DropDownList>
                </td>
            </tr>
                            <tr>
                                <td class="style1" style ="width :100%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style1" style ="width :100%; font-size: 10pt; font-weight: 700;">Descrizione del pagamento che sti sta per emettere</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDescrizione" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        Height="68px" TextMode="MultiLine" Width="98%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align ="right">
    

                                   
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
        runat="server" ControlToValidate="txtDescrizione"
            ErrorMessage="E' possibile inserire al massimo 150 caratteri in questo campo di testo" Font-Bold="True" 
                                                    Font-Names="Arial" Font-Size="10pt"
            
        
           ToolTip="E' possibile inserire al massimo 150 caratteri in questo campo di testo!"
            
        
           ValidationExpression="^[\s\S]{0,150}$" 
           SetFocusOnError="True"></asp:RegularExpressionValidator></td>
                                            <td>
                                                &nbsp;</td>
                                            <td align ="right">
    

    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
    
            <asp:ImageButton ID="btnPagamento" runat="server" ImageUrl="../NuoveImm/Img_Conferma1.png"
                
           
            ToolTip="Emissione del Pagamento" OnClientClick="AskConfirm();" />
    
        </span></strong>
    
                                            </td>
                                            <td align ="right">
    

                                                <img alt="Annulla" src="../NuoveImm/Img_AnnullaVal.png" 
                                                    onclick = "myOpacityDescMor.toggle();" style = "cursor :pointer;"/></td>
                                        </tr>
                                    </table>
                                    
                                </td>
                            </tr>
                        </table>
    </div>
            <asp:HiddenField ID="txtFlCompleta" runat="server" Value="0" />
            <asp:HiddenField ID="PagamEmesso" runat="server" Value="0" />
            <asp:HiddenField ID="VisMessage" runat="server" Value="1" />

        </td>
        <td style="vertical-align: top; width: 144px; height: 81px; text-align: left">
                        <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="Immagini/40px-Crystal_Clear_action_edit_add.png"
                            Style="z-index: 102; left: 392px; top: 387px" OnClientClick="ApriModalMorosita()"
                ToolTip="Aggiungi una Comunicazione" CausesValidation="False" Width="18px" 
                            Height="18px" />
            <br />

                        <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="Immagini/pencil-icon.png"
                            Style="z-index: 102; left: 392px; top: 387px" OnClientClick="ModificaModalMorosita()" 
                ToolTip="Modifica Elemento Selezionato" CausesValidation="False" />
            <br />
            
                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="Immagini/minus_icon.png"
                            Style="z-index: 102; left: 392px; top: 387px" OnClientClick="DeleteConfirmMoro()"
                ToolTip="Elimina Elemento Selezionato" />
                        <br />
                       
                        <asp:ImageButton ID="btnLettereMav" runat="server" ImageUrl="Immagini/message-icon.png"
                            Style="z-index: 102; left: 392px; top: 387px" OnClientClick="MavConfirm()"
                ToolTip="Stampa Lettere e Mav" />
                        <br />
                        
            <asp:Image ID="imgPrintReport"
      runat="server" onclick="ApriRptMorosita();"
                ImageUrl="Immagini/print-icon.png" 
                ToolTip="Stampa Morosita" style="cursor: hand"  />
                        <br />
                        
                        <asp:ImageButton ID="btnPayment" runat="server" ImageUrl="Immagini/Payment.png"
                            Style="z-index: 102; left: 392px; top: 387px; " 
                ToolTip="Emetti Pagamento" onclientclick="PaymentConfirm();return false;" 
                            Height="16px" />
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top; text-align: left">
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="100"
                ReadOnly="True" Style="left: 13px; top: 197px" Width="100%">Nessuna Selezione</asp:TextBox>
        </td>
        
    </tr>
</table>
    <script type="text/javascript">

        function DeleteConfirmMoro() {
            if (document.getElementById('TabMorosita1_txtidMorosita').value != 0) {
                var Conferma
                Conferma = window.confirm("Attenzione...Confermi di voler eliminare il dato selezionato?");
                if (Conferma == false) {
                    document.getElementById('TabMorosita1_txtConfElimina').value = '0';

                }
                else {
                    document.getElementById('TabMorosita1_txtConfElimina').value = '1';
                    ApriDeleteMorosita();
                }
            }
        }
        function MavConfirm() {
            if (document.getElementById('TabMorosita1_txtidMorosita').value != 0) {
                if (document.getElementById('TabMorosita1_VisMessage').value == 1) {
                    var Conferma
                    Conferma = window.confirm("Attenzione...Confermi di voler emettere i MAV?\n\rAssicurarsi di aver controllato la correttezza degli importi inseriti!");
                    if (Conferma == false) {
                        document.getElementById('TabMorosita1_txtConfMav').value = '0';

                    }
                    else {
                        document.getElementById('TabMorosita1_txtConfMav').value = '1';
                    }
                }
                else {
                    alert('Morosità già emessa!\nVerrà caricato il file precedentemente generato!');
                    document.getElementById('TabMorosita1_txtConfMav').value = '1';
                 }
                
            }
            else {
                alert('Selezionare una riga!');
                        }

        }

        function PaymentConfirm() {

            if (document.getElementById('TabMorosita1_txtidMorosita').value != 0) {
                if (document.getElementById('txtModificato').value == '1') {
                    alert('Prima di emettere il pagamento, salvare le modifiche apportate al condominio!')
                    return;
                }


                myOpacityDescMor.toggle();
                document.getElementById('TabMorosita1_txtDescrizione').value = 'GESTIONE MOROSITA\' CONDOMINIALE'



            }
            else {
                alert('Selezionare una riga per continuare!');
            }
        }




        function AskConfirm() {

              var Conferma
              Conferma = window.confirm("Attenzione...Confermi di voler emettere un pagamento?");
              if (Conferma == false) {
                document.getElementById('TabMorosita1_txtConfpayment').value = '0';

              }
              else {
                  document.getElementById('TabMorosita1_txtConfpayment').value = '1';
  
              }

      }
                                
                                
       myOpacityDescMor = new fx.Opacity('DivDescrizione', { duration: 200 });
       myOpacityDescMor.hide();  




    </script>

