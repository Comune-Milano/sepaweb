<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoSegnalazioni.aspx.vb" Inherits="CALL_CENTER_Segnalazione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>Segnalazioni</title>
    <style type="text/css">
        #contenitore
        {
            top: 138px;
            left: 17px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
                <table style="left: 0px; BACKGROUND-IMAGE: url('../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 798px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 706px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Gestione Segnalazioni</strong></span><br />
                    <br />

                    <br />
                    <asp:Label ID="lblSegnalazione" runat="server" 
                        style="position:absolute; top: 56px; left: 169px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="9pt"></asp:Label>
                    <asp:Label ID="Label3" runat="server" 
                        Text="Tipo Segnalazione" 
                        style="position:absolute; top: 106px; left: 14px;" Font-Bold="False" 
                        Font-Names="arial" Font-Size="10pt"></asp:Label>
                    <asp:Label ID="Label2" runat="server" 
                        Text="Elenco segnalazioni" 
                        style="position:absolute; top: 55px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt"></asp:Label>
                    <br />
                    <%--<asp:RadioButton ID="R5" runat="server" 
                        style="position:absolute; top: 105px; left: 407px; width: 77px;" 
                        Font-Names="arial" Font-Size="9pt" GroupName="A" Text="Varie" />
                    <asp:RadioButton ID="R4" runat="server" 
                        style="position:absolute; top: 105px; left: 328px; width: 77px;" 
                        Font-Names="arial" Font-Size="9pt" GroupName="A" Text="Proposte" />--%>
                    <asp:RadioButton ID="R3" runat="server" 
                        style="position:absolute; top: 105px; left: 251px; width: 114px;" 
                        Font-Names="arial" Font-Size="9pt" GroupName="A" Text="Informazioni" />
                    <asp:RadioButton ID="R2" runat="server" 
                        style="position:absolute; top: 105px; left: 187px; width: 77px;" 
                        Font-Names="arial" Font-Size="9pt" GroupName="A" Text="Guasti" />
                    <asp:RadioButton ID="R1" runat="server" 
                        style="position:absolute; top: 105px; left: 131px; width: 48px;" Checked="True" 
                        Font-Names="arial" Font-Size="9pt" GroupName="A" Text="Tutte" />
                    <asp:ImageButton ID="btnAggiorna" runat="server" 
                        ImageUrl="~/CALL_CENTER/Immagini/Button-Refresh-icon.png" 
                        ToolTip="Aggiorna risultati" 
                        style="position:absolute;cursor:pointer; top: 101px; left: 484px;" />
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
                    <asp:Image ID="imgNuova" runat="server" 
                        style="position:absolute; top: 507px; left: 9px; cursor:pointer" 
                        ImageUrl="~/CALL_CENTER/Immagini/Img_NuovaSegnalazione.png" 
                        onclick="NuovaSegnalazione();"/>
                    <asp:Image ID="imgApri" runat="server" 
                        style="position:absolute; top: 507px; left: 531px; cursor:pointer" 
                        ImageUrl="~/CALL_CENTER/Immagini/Img_Visualizza.png" 
                        onclick="Apri();"/>
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 507px; left: 669px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="ConfermaEsci();"/>
                   
                        <asp:TextBox ID="TextBox3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                            Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                            border-bottom: white 1px solid; left: -1px; top: 45px;" Width="777px">Nessuna Selezione</asp:TextBox>
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 485px; left: 10px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                    <br />
                </td>
            </tr>
            <asp:HiddenField ID="tipo" runat="server" />
            <asp:HiddenField ID="identificativo" runat="server" />
            <asp:HiddenField ID="LBLID" runat="server" />
            
        </table>
    <div>
    <div id="contenitore" 
            style="position: absolute; width: 770px; height: 317px; overflow: auto;">
            <asp:DataGrid ID="Datagrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                PageSize="40" 
                    Style="z-index: 101; left: 0px; width: 748px; position:absolute; top: 0px; " 
                    TabIndex="1" BorderWidth="0px">
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DES" HeaderText="a" ReadOnly="True" Visible="False">
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="STATO">
                        <ItemTemplate>
                            <asp:Label runat="server" 
                                Text='<%# DataBinder.Eval(Container, "DataItem.STATO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="TIPOLOGIA">
                        <ItemTemplate>
                            <asp:Label runat="server" 
                                Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="DATA INSERIMENTO">
                        <ItemTemplate>
                            <asp:Label runat="server" 
                                Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INSERIMENTO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="DESCRIZIONE">
                        <ItemTemplate>
                            <asp:Label runat="server" 
                                Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE_RIC") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle BackColor="#F2F5F1" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Navy" Wrap="False" />
                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:DataGrid>
            </div>
    </div>

     
    </form>
    <script type="text/javascript">
        var selezionato;
        function ConfermaEsci() {

         
                var chiediConferma
                chiediConferma = window.confirm("Sei sicuro di voler uscire?");
                if (chiediConferma == true) {
                    document.location.href = 'pagina_home.aspx';
                }

            }

            function Apri() {
                if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                    today = new Date();
                    var Titolo = 'Segnalazione' + today.getMinutes() + today.getSeconds();

                    popupWindow = window.open('Segnalazione.aspx?T=' + document.getElementById('tipo').value + '&IDE=' + document.getElementById('identificativo').value + '&ID=' + document.getElementById('LBLID').value, Titolo, 'height=700,width=900');
                    popupWindow.focus();
                }
                else {
                    alert('Nessuna Segnalazione Selezionata!');
                }

            }

            function NuovaSegnalazione() {
                
                    today = new Date();
                    var Titolo = 'Segnalazione' + today.getMinutes() + today.getSeconds();

                    popupWindow = window.open('Segnalazione.aspx?T=' + document.getElementById('tipo').value + '&IDE=' + document.getElementById('identificativo').value + '&ID=-1', Titolo, 'height=700,width=900');
                    popupWindow.focus();
                }
                 
           
    </script>
</body>
</html>

