<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiDepCauzionali.aspx.vb"
    Inherits="Contratti_RisultatiDepCauzionali" %>

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
    <title>Risultato Ricerca Depositi</title>
    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }



        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }


    </script>
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
                        Depositi Cauzionali - Trovati
                        <asp:Label ID="Label4" runat="server" Text="DD"></asp:Label>&nbsp;RU </strong>
                    </span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;<asp:Label
                        ID="lblVoceBp" runat="server" Style="font-size: 12pt; color: #000000" 
                        Visible="False"></asp:Label>
                    </strong></span>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="DivRoot" align="left">
                        <div style="overflow: hidden;" id="DivHeaderRow">
                        </div>
                        <div style="width: 1367px; overflow: scroll; height: 355px;" 
                            onscroll="OnScrollDiv(this)" id="DivMainContent">
                            <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                Font-Strikeout="False" Font-Underline="False" meta:resourcekey="DataGrid1Resource1"
                                PageSize="200" Style="z-index: 101; left: 483px; top: 68px" Width="1200px" BackColor="White">
                                <FooterStyle Wrap="False" />
                                <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <HeaderStyle BackColor="#3366FF" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                    Wrap="False" ForeColor="White" Height="25px" />
                                <EditItemStyle Wrap="False" />
                                <SelectedItemStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                                <Columns>
                                    <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <HeaderStyle Wrap="False" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False"></HeaderStyle>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IMP_DEPOSITO_CAUZ" HeaderText="IMPORTO DEP."></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="DATA EMISSIONE"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_PAGAMENTO" HeaderText="DATA PAGAMENTO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="PROVENIENZA" HeaderText="PROVENIENZA"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="LIBRO" HeaderText="LIBRO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="BOLLA" HeaderText="BOLLA"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_RIMBORSO" HeaderText="DATA RIMBORSO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_RESTITUZIONE" HeaderText="DATA RESTITUZIONE"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IMPORTO_RESTITUZIONE" HeaderText="IMPORTO RESTITUITO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="RESTITUIBILE" HeaderText="RESTITUIBILE"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                        <div id="DivFooterRow" style="overflow: hidden">
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
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
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png"
            Style="z-index: 102; left: 363px; position: absolute; top: 513px" ToolTip="Visualizza"
            OnClientClick="document.getElementById('H1').value='1';" />
        &nbsp;
        <img onclick="document.location.href='pagina_home.aspx';" alt="" src="../NuoveImm/Img_Home.png"
            style="position: absolute; top: 511px; left: 627px; cursor: pointer; height: 20px;" />
        <img onclick="document.location.href='RicercaDepCauzionali.aspx';" alt="" src="../NuoveImm/Img_NuovaRicerca.png"
            style="position: absolute; top: 512px; left: 490px; cursor: pointer;" />
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
