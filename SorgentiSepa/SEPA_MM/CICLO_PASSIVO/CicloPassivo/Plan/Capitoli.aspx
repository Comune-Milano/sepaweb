<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Capitoli.aspx.vb" Inherits="Contabilita_CicloPassivo_Capitoli" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Capitoli</title>
</head>
<body>
    <form id="form1" runat="server">
<br/>
        <table style="left: 0px; BACKGROUND-IMAGE: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 798px;
            position: absolute; top: 0px; height: 596px;">
            <tr>
                <td style="width: 706px">
                   <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Piano Finanziario-</strong>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                    -
                    <asp:Label ID="lblStato" runat="server" style="font-weight: 700"></asp:Label>
                    <br />
                    </span><br />
                    <br />
                    <br />
                    
                    <br />
                    <br />
    
                    <br />
                    <br />
                    <br />
                    <br />
    
                    <br />
    
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                        Style="left: 576px; position: absolute; top: 516px; height: 20px;" 
                        TabIndex="4" />
    <div id="ContenitoreVoci" 
                        
                        
                        
                        style="overflow: auto; width: 770px; height: 424px; position:absolute; top: 66px; left: 14px;">
                        <asp:DataGrid style="z-index: 105; top: 0px; left: 0px; width: 98%; " AutoGenerateColumns="False" 
                            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" 
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                            Font-Underline="False" GridLines="None" ID="DataGridVoci" PageSize="50" 
                            runat="server"  >
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn HeaderText="id" Visible="False" 
                                    DataField="ID" ReadOnly="True"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="COD.">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CODICE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="VOCE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ID_CAPITOLO" HeaderText="ID_CAPITOLO" 
                                    ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="CAPITOLO">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="cmbCapitolo" runat="server" Font-Names="arial" 
                                            Font-Size="8pt" 
                                            SelectedValue='<%# DataBinder.Eval(Container, "DataItem.ID_CAPITOLO") %>' 
                                            Width="120px" DataSource="<%# miadt %>" DataTextField="cod" 
                                            DataValueField="id">
                                        </asp:DropDownList>
                                        <asp:Label ID="Label5" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" />
                        </asp:DataGrid>
    </div>
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
                    
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 516px; left: 681px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="ConfermaEsci();"/>
                    <br />
                    <br />
                    <br />
                    
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 499px; left: 15px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                    <br />

   

                </td>
            </tr>
     <asp:HiddenField ID="idPianoF" runat="server" />
<asp:HiddenField ID="modificato" runat="server" />
<asp:HiddenField ID="lettura" runat="server" />
        </table>
   

    </form>
    
        <script type="text/javascript">
            function ConfermaEsci() {
                
                if (document.getElementById('modificato').value == '1') {
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione, sono state fatte delle modifiche. Sei sicuro di voler uscire senza salvare?");
                    if (chiediConferma == true) {
                        document.location.href = '../../pagina_home.aspx';
                    }
                }
                else {

                    var chiediConferma
                    chiediConferma = window.confirm("Sei sicuro di voler uscire?");
                    if (chiediConferma == true) {
                        document.location.href = '../../pagina_home.aspx';
                    }
                }
            }

           


    </script>
</body>
</html>