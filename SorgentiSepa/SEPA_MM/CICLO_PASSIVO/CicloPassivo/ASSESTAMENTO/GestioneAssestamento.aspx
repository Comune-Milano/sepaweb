<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestioneAssestamento.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_GestioneAssestamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13) {

                e.preventDefault();
            }
        }

        function $onkeydown() {
            if (event.keyCode == 13) {
                event.keyCode = '9';
            }
        }

        function Convalida() {
            var chiediConferma
            if (document.getElementById('CONThidden').value == '1') {
                chiediConferma = window.confirm("Attenzione...Confermi di voler approvare questo assestamento?\nGli importi APPROVATI vuoti verranno convalidati per l\'importo richiesto!");
            } else {
                chiediConferma = window.confirm("Attenzione...Confermi di voler approvare questo assestamento?");
            }
            if (chiediConferma == true) {
                document.getElementById('ConfAlerCompleto').value = '1';
            }
            else {
                document.getElementById('ConfAlerCompleto').value = '0';
            }
        }

        function ConvalidaComune() {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Confermi di voler approvare questo assestamento?");
            if (chiediConferma == true) {
                document.getElementById('ConfCompleto').value = '1';
            }
            else {
                document.getElementById('ConfCompleto').value = '0';
            }
        }

        function ApriEventi() {
            window.open('Eventi_Assest.aspx?IDASSEST=' + document.getElementById('IdAssestamento').value, 'Eventi', '');
        }

        function PrintAss() {
            //window.open('Stampe.aspx?ID=' + document.getElementById('IdAssestamento').value, 'PrintCom', '');
        }
        function NonConvalida() {
            document.getElementById('Convalida').style.visibility = 'visible';
        }
        function Sicuro() {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Confermi di rigettare l\'approvazione di questo assestamento?");
            if (chiediConferma == true) {
                document.getElementById('salvaok').value = '1';
            }
            else {
                document.getElementById('salvaok').value = '0';
            }
        }

        function AllegaFile() {
            if ((document.getElementById('IdAssestamento').value == '') || (document.getElementById('IdAssestamento').value == '-1')) {
                apriAlert('Impossibile inserire allegati!', 300, 150, 'Attenzione', null, null);
            } else {
                CenterPage('../../../GestioneAllegati/GestioneAllegati.aspx?T=2&O=' + document.getElementById('TipoAllegato').value + '&I=' + document.getElementById('IdAssestamento').value, 'Allegati', 1000, 800);
            };

            //window.open('ElencoAllegati.aspx?T=3&COD=' + document.getElementById('txtIdAppalto').value, 'AllegatiContratto', 'scrollbars=1, width=800px, height=600px, resizable');
            return false;
        };

        function CenterPage(pageURL, title, w, h) {
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        };

    </script>
</head>
<body style="background-attachment: fixed; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat; background-attachment: fixed; width: 794px; height: 100%;">
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form2" runat="server">
        <asp:HiddenField ID="TipoAllegato" runat="server" Value="0" />
        <asp:HiddenField ID="IdAssestamento" runat="server" Value="0" />
        <asp:HiddenField ID="CONThidden" runat="server" Value="0" />
        <asp:HiddenField ID="compl" runat="server" Value="10" />
        <asp:HiddenField ID="ConfCompleto" runat="server" />
        <asp:HiddenField ID="IDSTATO" runat="server" Value="0" />
        <asp:HiddenField ID="naVisibile" runat="server" Value="0" />
        <asp:HiddenField ID="salvaok" runat="server" Value="0" />
        <asp:HiddenField ID="ConfALerCompleto" runat="server" Value="0" />
        <asp:HiddenField ID="stampaass" runat="server" Value="0" />
        <asp:HiddenField ID="soloLettura" runat="server" Value="0" />
        <asp:HiddenField runat="server" ID="URLdiProvenienza" Value="" />
        <div id="Convalida" style="position: absolute; z-index: 500; top: 0px; left: 0px; width: 800px; height: 100%; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat; visibility: hidden;">
            <img id="immDiv" alt="" src="../../../ImmDiv/DivMGrande.png" style="position: absolute; z-index: 401; top: 75px; left: 40px;" />
            <table style="background-color: #FFFFFF; position: absolute; z-index: 501; top: 100px; left: 71px;">
                <tr>
                    <td style="font-family: arial; font-size: 12pt; font-weight: bold; text-align: center;">Indicare i motivi per cui l'Assestamento dovrà essere modificato
                    </td>
                </tr>
                <tr>
                    <td align="center" style="font-family: arial; font-size: 8pt; font-weight: normal; text-align: center;">L'Assestamento dovrà essere nuovamente modificato dal Gestore
                    </td>
                </tr>
                <tr>
                    <td align="center">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="font-family: arial; font-size: 10pt; font-weight: bold">Note
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="TxtNote" runat="server" Height="225px" MaxLength="4000" TextMode="MultiLine"
                            Width="671px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:ImageButton ID="btnSalvaNoteRifiuto" runat="server" ImageUrl="../../../NuoveImm/Img_SalvaGrande.png"
                            Style="height: 20px" TabIndex="4" OnClientClick="document.getElementById('splash').style.visibility = 'visible';Sicuro();" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Image ID="imgAnnulla" runat="server" Style="cursor: pointer" ImageUrl="../../../NuoveImm/Img_EsciCorto.png"
                        onclick="document.getElementById('Convalida').style.visibility = 'hidden'" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 99%; height: 100%; position: absolute; top: 15px; left: 9px;">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="vertical-align: bottom; text-align: left">
                                <asp:ImageButton ID="btnIndietro" runat="server" AlternateText="Indietro" ToolTip="Indietro"
                                    ImageUrl="../../../NuoveImm/Img_Indietro.png" />
                            </td>
                            <td style="vertical-align: bottom; text-align: left">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../../NuoveImm/Img_Stampa.png"
                                    ToolTip="Stampa Assestamento" AlternateText="Stampa Assestamento" OnClientClick="document.getElementById('splash').style.visibility = 'visible'" />
                            </td>
                            <td style="vertical-align: bottom; text-align: right">
                                <img id="btnAllegati" alt="Allegati" src="../../../NuoveImm/Img_Allegati.png" title="Allegati"
                                    style="cursor: pointer" onclick="AllegaFile();" />
                            </td>
                            <td style="vertical-align: bottom; text-align: right">
                                <img id="Eventi" alt="Eventi" src="../../../NuoveImm/Img_Eventi.png" title="Eventi"
                                    style="cursor: pointer" onclick="ApriEventi();" />
                            </td>
                            <td style="vertical-align: bottom; text-align: right">
                                <img id="exit" alt="Esci" src="../../../NuoveImm/Img_Esci.png" title="Esci" style="cursor: pointer"
                                    onclick="document.location.href='../../pagina_home.aspx';" />
                            </td>
                            <td class="style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="font-size: 12pt; color: #801f1c; font-family: Arial">
                                <strong>&nbsp;
                                    <asp:Label runat="server" ID="labelTitolo" Font-Names="Arial" Font-Size="11pt">Assestamento Esercizio Finanziario - </asp:Label>
                                    <asp:Label ID="esercizio" runat="server" Font-Names="Arial" Font-Size="11pt"></asp:Label><br />
                                </strong></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style3" style="text-align: center; font-size: 1pt;">
                    <table width="100%">
                        <tr>
                            <td style="text-align: left;" class="style4">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="arial"
                                    Font-Size="10pt" Font-Strikeout="False" ForeColor="Maroon"></asp:Label>
                            </td>
                            <td style="width: 50%" align="right">
                                <asp:Label ID="AssestamentoCompletato" runat="server" Font-Bold="True" Font-Italic="False"
                                    Font-Names="arial" Font-Size="10pt" Font-Strikeout="False" ForeColor="Maroon"
                                    BackColor="#F8F32B"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="Div2" style="height: 1px; width: 100%; visibility: hidden; overflow: auto;">
                        <asp:DataGrid ID="DgvApprAssestCopia" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            PageSize="30" Style="table-layout: auto; z-index: 101; left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                            TabIndex="-1" Width="760px"
                            BorderColor="#666666" CellPadding="1">
                            <FooterStyle Font-Bold="True" Wrap="False" BackColor="#507CD1" ForeColor="White" />
                            <EditItemStyle Wrap="False" BackColor="#2461BF" />
                            <SelectedItemStyle Font-Bold="True" Wrap="False" BackColor="#D1DDF1" ForeColor="#333333" />
                            <PagerStyle Position="TopAndBottom" Visible="False" Wrap="False" BackColor="#2461BF"
                                ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="White" Wrap="False" />
                            <ItemStyle Wrap="False" BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundColumn DataField="TIPO" HeaderText="TIPO" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                    <HeaderStyle Height="1px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_PIANO_FINANZIARIO" HeaderText="ID_PIANO_FINANZIARIO"
                                    Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CODICE" HeaderText="CODICE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" Width="10%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="VOCE">
                                    <HeaderStyle Width="69%" Wrap="true" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ASSESTAMENTO" HeaderText="ASSESTAMENTO RICHIESTO">
                                    <HeaderStyle Width="10%" Wrap="true" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="True" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="APPROVATO" HeaderText="ASSESTAMENTO APPROVATO DAL GESTORE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="11%" Wrap="True" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                                Wrap="False" BorderStyle="None" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" BackColor="#507CD1" />
                        </asp:DataGrid>
                    </div>
                    <div id="Div1" style="height: <%=visialt%>; visibility: <%=VISI%>; overflow: auto; width: 770px;">
                        <asp:DataGrid ID="DgvApprAssCapitoli" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            PageSize="30" Style="table-layout: auto; z-index: 101; left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                            TabIndex="-1" Width="750px"
                            BorderColor="#666666" CellPadding="1">
                            <FooterStyle Font-Bold="True" Wrap="False" BackColor="#507CD1" ForeColor="White" />
                            <EditItemStyle Wrap="False" BackColor="#2461BF" />
                            <SelectedItemStyle Font-Bold="True" Wrap="False" BackColor="#D1DDF1" ForeColor="#333333" />
                            <PagerStyle Position="TopAndBottom" Visible="False" Wrap="False" BackColor="#2461BF"
                                ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="White" Wrap="False" />
                            <ItemStyle Wrap="False" BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundColumn DataField="COD" HeaderText="CODICE" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderStyle-HorizontalAlign="Center" HeaderText="DESCRIZIONE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ASSESTAMENTO" HeaderText="ASSESTAMENTO" HeaderStyle-HorizontalAlign="Center">
                                    <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                                Wrap="False" BorderStyle="None" BackColor="#507CD1" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid>
                        <br />
                        <asp:DataGrid ID="DgvApprAssest" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            PageSize="30" Style="table-layout: auto; z-index: 101; left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                            TabIndex="-1" Width="750px"
                            BorderColor="#666666" CellPadding="1">
                            <FooterStyle Font-Bold="True" Wrap="False" BackColor="#507CD1" ForeColor="White" />
                            <EditItemStyle Wrap="False" BackColor="#2461BF" />
                            <SelectedItemStyle Font-Bold="True" Wrap="False" BackColor="#D1DDF1" ForeColor="#333333" />
                            <PagerStyle Position="TopAndBottom" Visible="False" Wrap="False" BackColor="#2461BF"
                                ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="White" Wrap="False" />
                            <ItemStyle Wrap="False" BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                    <HeaderStyle Height="1px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_PIANO_FINANZIARIO" HeaderText="ID_PIANO_FINANZIARIO"
                                    Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CODICE" HeaderText="CODICE" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="60px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="VOCE" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="100px" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="APPROVATO" HeaderText="ASSESTAMENTO APPROVATO DAL COMUNE"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="80px" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="True" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_CAPITOLO" HeaderText="CAPITOLO" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CAPITOLO" HeaderText="DESCRIZIONE CAPITOLO" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="140px" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                                Wrap="False" BorderStyle="None" BackColor="#507CD1" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="style3" valign="top">
                    <div id="Div_DataGridView" style="height: 335px; overflow: auto; width: 100%;">
                        <asp:DataGrid ID="datagrid2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            PageSize="30" Style="table-layout: auto; z-index: 101; left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                            TabIndex="-1" Width="750px"
                            BorderColor="#666666" CellPadding="1">
                            <FooterStyle Font-Bold="True" Wrap="False" BackColor="#507CD1" ForeColor="White" />
                            <EditItemStyle Wrap="False" BackColor="#2461BF" />
                            <SelectedItemStyle Font-Bold="True" Wrap="False" BackColor="#D1DDF1" ForeColor="#333333" />
                            <PagerStyle Position="TopAndBottom" Visible="False" Wrap="False" BackColor="#2461BF"
                                ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="White" Wrap="False" />
                            <ItemStyle Wrap="False" BackColor="#EFF3FB" />
                            <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                                Wrap="False" BorderStyle="None" BackColor="#006699" />
                            <Columns>
                                <asp:BoundColumn DataField="CODICE" HeaderText="CODICE" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                <asp:BoundColumn DataField="VOCE" HeaderText="VOCE" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                <asp:BoundColumn DataField="BUDGET" HeaderText="BUDGET INIZIALE" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ASSESTAMENTI_PRECEDENTI" HeaderText="ASSESTAMENTI PRECEDENTI"
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                <asp:BoundColumn DataField="VARIAZIONI" HeaderText="VARIAZIONI" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SPESO" HeaderText="SPESO" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RESIDUO" HeaderText="RESIDUO" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ASSESTAMENTO" HeaderText="ASSESTAMENTO RICHIESTO" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Text="Label" Visible="False" Width="580px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <table width="100%">
                        <tr>
                            <td style="width: 100%; text-align: right;">
                                <asp:ImageButton ID="modifica" runat="server" ImageUrl="../../../CICLO_PASSIVO/CicloPassivo/Plan/Immagini/img_modifica.png"
                                    alt="Modifica Assestamento" ToolTip="Modifica Assestamento" />
                                <asp:ImageButton ID="convalida" runat="server" ImageUrl="../../../NuoveImm/Img_ConvAler.png"
                                    OnClientClick="document.getElementById('splash').style.visibility = 'visible';Convalida();"
                                    alt="Convalida Gestore" ToolTip="Convalida Gestore" />
                                <img id="nonapprova" alt="Rifiuta Assestamento" src="../../../NuoveImm/Img_NonConvalida.png"
                                    title="Rifiuta Assestamento" style="cursor: pointer" onclick="NonConvalida();" />
                                <asp:ImageButton ID="approva" runat="server" ImageUrl="../../../NuoveImm/Img_ConvComune.png"
                                    OnClientClick="document.getElementById('splash').style.visibility = 'visible';ConvalidaComune();"
                                    alt="Convalida Assestamento" ToolTip="Convalida Assestamento" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <script type="text/javascript" language="javascript">
            document.getElementById('splash').style.visibility = 'hidden';
            //document.getElementById('dvvvPre').style.visibility = 'hidden';
            if (document.getElementById('naVisibile').value == '0') {
                document.getElementById('nonapprova').style.visibility = 'hidden';
            }

            if (document.getElementById('stampaass').value == '0') {
                document.getElementById('stampavis').style.visibility = 'hidden';
            }
        </script>
    </form>
</body>
</html>
