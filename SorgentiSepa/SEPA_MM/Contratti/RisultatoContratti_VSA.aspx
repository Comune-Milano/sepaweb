<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoContratti_VSA.aspx.vb"
    Inherits="Contratti_RisultatoContratti_VSA" %>

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
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">
        document.onkeydown = $onkeydown;
    </script>
    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
            width: 801px; position: absolute; top: 0px;">
            <tr>
                <td style="width: 670px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                        Contratti - Trovate
                        <asp:Label ID="Label4" runat="server" Text="DD"></asp:Label>&nbsp;Domande </strong>
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
                    <div style="left: 9px; overflow: auto; width: 775px; position: absolute; top: 67px;
                        height: 364px">
                        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                            Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                            Font-Strikeout="False" Font-Underline="False" meta:resourcekey="DataGrid1Resource1"
                            PageSize="200" Style="z-index: 101; left: 483px; top: 68px" Width="2700px" AllowPaging="True"
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
                                <asp:BoundColumn DataField="IDCONTRATTO" HeaderText="id" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NORMATIVA" HeaderText="NORMATIVA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PG_DOM" HeaderText="NUM. DOMANDA/PROT.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_DOMANDA_VSA" HeaderText="TIPO DOMANDA">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_SPECIFICO_DOMANDA" HeaderText="TIPO SPECIFICO">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="STATO_DOMANDA" HeaderText="STATO DOMANDA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PG_DIC" HeaderText="NUM. DICH.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="STATO_DICH" HeaderText="STATO DICH.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_EVENTO" HeaderText="DATA EVENTO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_PRESENTAZIONE" HeaderText="DATA PRESENTAZIONE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MOT_PRES" HeaderText="MOT. PRESENTAZ.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_INIZIO_VAL" HeaderText="DATA INIZIO VAL.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_FINE_VAL" HeaderText="DATA FINE VAL.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ANNO_SIT_ECONOMICA" HeaderText="ANNO REDDITI">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FL_AUTORIZZATA" HeaderText="AUTORIZZATA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_AUTORIZZAZIONE" HeaderText="DATA AUTORIZZ.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FL_CONTABILIZZATA" HeaderText="CONTABILIZZATA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="FILIALE_ALER" HeaderText="NOME SEDE T.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO UNITA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO UNITA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_PG" HeaderText="DATA INSERIMENTO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png"
                        Style="z-index: 102; left: 363px; position: absolute; top: 513px" ToolTip="Visualizza"
                        OnClientClick="document.getElementById('H1').value='1';" />
                    <img onclick="ApriContratto();" alt="" src="../NuoveImm/Img_Visualizza.png" style="position: absolute;
                        top: 513px; left: 222px; cursor: pointer;" id="Visualizza" />
                    <img onclick="document.location.href='pagina_home.aspx';" alt="" src="../NuoveImm/Img_Home.png"
                        style="position: absolute; top: 511px; left: 627px; cursor: pointer; height: 20px;" />
                    <img onclick="document.location.href='RicercaContratti_VSA.aspx';" alt="" src="../NuoveImm/Img_NuovaRicerca.png"
                        style="position: absolute; top: 512px; left: 490px; cursor: pointer;" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="TextBox3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                        Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                        border-bottom: white 1px solid; left: -1px; top: 45px;" Width="777px">Nessuna Selezione</asp:TextBox>
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
</html>
