<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiRestDep.aspx.vb" Inherits="Contratti_RisultatiRestDep" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    var Selezionato;

    Uscita = 1;

    function $onkeydown() {

        if (event.keyCode == 13) {
            ApriContratto();
        }
    }
   
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">
        document.onkeydown = $onkeydown;
    </script>
    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
            width: 801px; position: absolute;top:0px;">
            <tr>
                <td style="width: 670px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Elenco Restituzioni Depositi Cauzionali - Trovati
                        <asp:Label ID="Label4" runat="server" Text="DD"></asp:Label>&nbsp;RU </strong>
                    </span>
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
                    <div style="left: 9px; overflow: auto; width: 765px; position: absolute; top: 67px;
                        height: 364px">
                        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                            Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                            Font-Strikeout="False" Font-Underline="False" meta:resourcekey="DataGrid1Resource1"
                            PageSize="200" Style="z-index: 101; left: 483px; top: 68px" Width="753px"
                            BackColor="White">
                            <FooterStyle Wrap="False" />
                            <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                Wrap="False" />
                            <EditItemStyle Wrap="False" />
                            <SelectedItemStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_CONTRATTO" 
                                    HeaderText="COD. CONTRATTO">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
<HeaderStyle Wrap="False" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                        Font-Underline="False"></HeaderStyle>

                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_CERT_PAG_1" HeaderText="DATA CERT.PAGAM.">
<HeaderStyle Wrap="False" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                        Font-Underline="False"></HeaderStyle>

                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NUM_CDP" HeaderText="NUM.CDP">

<ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                        Font-Underline="False"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ANNO_CDP" HeaderText="ANNO CDP">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NUM_MANDATO" HeaderText="NUM.MANDATO">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_MANDATO_1" 
                                    HeaderText="DATA MANDATO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
<ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                        Font-Underline="False"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ANNO_MANDATO" HeaderText="ANNO MANDATO">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CREDITO" HeaderText="IMPORTO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="INTERESSI" HeaderText="INTERESSI" Visible="False"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png"
                        Style="z-index: 102; left: 363px; position: absolute; top: 513px" ToolTip="Visualizza"
                        OnClientClick="document.getElementById('H1').value='1';" />
                    &nbsp;<img onclick="document.location.href='pagina_home.aspx';" alt="" src="../NuoveImm/Img_Home.png"
                        style="position: absolute; top: 511px; left: 627px; cursor: pointer; height: 20px;" />
                    <img onclick="document.location.href='RicercaRestDepositi.aspx';" alt="" src="../NuoveImm/Img_NuovaRicerca.png"
                        style="position: absolute; top: 512px; left: 490px; cursor: pointer;" />
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
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="LBLID" runat="server" />
    <asp:HiddenField ID="Label3" runat="server" />
    <asp:HiddenField ID="H1" runat="server" Value="0" />
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';

        function ApriContratto() {
            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                today = new Date();
                var Titolo = 'Contratto' + today.getMinutes() + today.getSeconds();

                popupWindow = window.open('Contratto.aspx?ID=' + document.getElementById('LBLID').value + '&COD=' + document.getElementById('Label3').value, Titolo, 'height=780,width=1160');
                popupWindow.focus();
            }
            else {
                alert('Nessun Contratto Selezionato!');
            }

        }

    </script>
</body>

