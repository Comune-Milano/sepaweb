<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovoAssest.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_NuovoAssest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Prospetto</title>
</head>
<body>
    <form id="form1" runat="server">
        
        <table >
            <tr>
                <td style="width: 706px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Assestamento Esercizio Finanziario in corso- Nuovo<br />
                    </strong></span><br />
                    <div style="position: absolute; top: 86px; left: 12px; height: 224px; width: 746px;">
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                <asp:DataGrid ID="DataGridAssestamenti" runat="server" AutoGenerateColumns="False"
                    BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="24" 
                    Style="z-index: 105; left: 193px; top: 54px" Width="97%" BorderColor="Gray" 
                            BorderWidth="1px">
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle ForeColor="Black" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ESERCIZIO" HeaderText="ES. FINANZIARIO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_INSERIMENTO" 
                            HeaderText="DATA CREAZ.ASSESTAMENTO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="STATO" HeaderText="STATO ASSEST.">
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
                </asp:DataGrid>
                        </span></strong>
                    </div>
                    <br />
                    <asp:Label ID="lblTitolo" runat="server" Text="Riepilogo degli Assestamenti generati nell'Esercizio Finanziario in corso" 
                        style="position:absolute; top: 57px; left: 12px; width: 745px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt"></asp:Label>
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
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 375px; left: 16px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="prosegui" runat="server" Value="0" />
                    <asp:HiddenField ID="idPfMain" runat="server" Value="0" />
                    <asp:HiddenField ID="newAssest" runat="server" Value="0" />
                    <br />
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 516px; left: 669px; cursor:pointer" 
                        ImageUrl="../../../NuoveImm/Img_EsciCorto.png" 
                        onclick="document.location.href='../../pagina_home.aspx';"/>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    <div>
    
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="../../../NuoveImm/Img_Procedi.png"
                        Style="left: 574px; position: absolute; top: 516px; height: 20px;" 
                        TabIndex="31" onclientclick="Conferma()" 
                        ToolTip="Procedi alla creazione di un nuovo Assestamento" />
    
    </div>
    </form>
    <script type="text/javascript">
        function Conferma() {

            var sicuro = window.confirm('Confermi di voler creare un nuovo Assestamento per l\'esercizio finanziario corrente?');
            if (sicuro == true) {
                document.getElementById('prosegui').value = '1';
            }
            else {
                document.getElementById('prosegui').value = '0';
            }
        }
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
