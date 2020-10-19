<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConvAssestComune.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_ConvAssestComune" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Approvazione Assestamento - COMUNE</title>
    <style type="text/css">
        #form1 {
            width: 793px;
            height: 668px;
        }

        .style1 {
            font-family: Arial;
            font-weight: bold;
            font-size: 13pt;
            color: #801f1c;
            text-align: left;
        }
    </style>
    <script type="text/javascript">
        function SostPuntVirg(e, obj) {
            var keyPressed;
            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {
                event.keyCode = 0;
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            }

        };
        function AutoDecimal2(obj) {

            obj.value = obj.value.replace('.', '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a != 'NaN') {
                    if (a.substring(a.length - 3, 0).length >= 4) {
                        var decimali = a.substring(a.length, a.length - 2);
                        var dascrivere = a.substring(a.length - 3, 0);
                        var risultato = '';
                        while (dascrivere.replace('-', '').length >= 4) {
                            risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                            dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                        }
                        risultato = dascrivere + risultato + ',' + decimali
                        //document.getElementById(obj.id).value = a.replace('.', ',')
                        document.getElementById(obj.id).value = risultato
                    }
                    else {
                        document.getElementById(obj.id).value = a.replace('.', ',')
                    }

                }
                else
                    document.getElementById(obj.id).value = ''
            }
        };
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');

        };
        function ConfAssest() {
            if (document.getElementById("chkCompleto").checked == true) {
                Conferma = window.confirm("ATTENZIONE...\nSelezionando il flag COMPLETO rende l\'assestamento convalidabile.\nProcedere con l\'operazione?");
                if (Conferma == true) {
                    document.getElementById("ConfCompleto").value = '1';
                }
                else
                { document.getElementById("ConfCompleto").value = '0'; }


            }

        }

        function ApriEventi() {
            window.open('Eventi_Assest.aspx?IDASSEST=' + document.getElementById('IdAssestamento').value, 'Eventi', '');

            // alert('Funzione non disponibile!');
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

        function NonConvalida() {
            document.getElementById('Convalida').style.visibility = 'visible';
        }

        function Convalida() {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Confermi di voler approvare questo assestamento?");
            if (chiediConferma == true) {
                document.getElementById('ConfCompleto').value = '1';
            }
            else {
                document.getElementById('ConfCompleto').value = '0';
            }
        }
        function PrintAss() {
            window.open('Stampe.aspx?ID=' + document.getElementById('IdAssestamento').value + '&CHIAMA=ASSCOMU', 'PrintCom', '');
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
<body style="background-attachment: fixed; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat; background-attachment: fixed; width: 789px; height: 507px;">
    <!-- Da mettere subito dopo l'apertura del tag <body> -->
    <form id="form1" runat="server">
        <div id="Convalida" style="position: absolute; z-index: 500; top: 0px; left: 0px; width: 800px; height: 600px; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat; visibility: hidden;">
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
                        onclick="document.getElementById('Convalida').style.visibility = 'hidden';" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 99%; position: absolute; top: 15px; left: 9px;">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="vertical-align: bottom; text-align: left">
                                <asp:ImageButton ID="btnIndietro" runat="server" AlternateText="Indietro" ToolTip="Indietro"
                                    ImageUrl="../../../NuoveImm/Img_Indietro.png" />
                            </td>
                            <td style="vertical-align: bottom; text-align: left">
                                <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="../../../NuoveImm/Img_Stampa.png"
                                    ToolTip="Stampa Assestamento" AlternateText="Stampa Assestamento"
                                    OnClientClick="document.getElementById('splash').style.visibility = 'visible'" />
                            </td>
                            <td style="vertical-align: bottom; text-align: left">
                                <asp:ImageButton ID="imgEsporta" runat="server" ImageUrl="../../../NuoveImm/Img_ExportExcel.png"
                                    ToolTip="Esporta Assestamento" AlternateText="Esporta Assestamento" OnClientClick="document.getElementById('splash').style.visibility = 'visible'" />
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
                                <img id="Img1" alt="Esci" src="../../../NuoveImm/Img_Esci.png" title="Esci" style="cursor: pointer"
                                    onclick="document.location.href='../../pagina_home.aspx';" />
                            </td>
                            <td class="style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="font-size: 12pt; color: #801f1c; font-family: Arial">
                                <strong>&nbsp;
                                    <asp:Label runat="server" ID="labelTitolo" Font-Names="Arial" Font-Size="10pt">Assestamento Es. Finanziario - </asp:Label>
                                    <asp:Label ID="esercizio" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                </strong></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="style3" align="left" style="width: 60%">
                                <asp:Label ID="lblAssestamentoData" runat="server" Font-Bold="True" Font-Italic="False"
                                    Font-Names="arial" Font-Size="10pt" Font-Strikeout="False" ForeColor="Maroon"></asp:Label>
                            </td>
                            <td class="style3" align="right" style="width: 40%">
                                <asp:Label ID="lblAssestamento" runat="server" Font-Bold="True" Font-Italic="False"
                                    Font-Names="arial" Font-Size="10pt" Font-Strikeout="False" ForeColor="Maroon"
                                    BackColor="#F8F32B"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="Div_DataGridView" style="height: 380px; overflow: auto; width: 770px;">
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
                            TabIndex="-1" Width="100%"
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
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="APPROVATO" HeaderText="ASSESTAMENTO APPROVATO DAL GESTORE"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="True" Width="10%" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" Wrap="True" />
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
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="True" />
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
                <td align="right">
                    <br />
                    <br />
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Image ID="NoConvalida" runat="server" ImageUrl="../../../NuoveImm/Img_NonConvalida.png" AlternateText="Rifiuta Assestamento"
                                    Style="cursor: pointer" ToolTip="Rifiuta Assestamento" CausesValidation="False" OnClick="NonConvalida();" />
                                <asp:ImageButton ID="btnConfirm" runat="server" ImageUrl="../../../CICLO_PASSIVO/CicloPassivo/Plan/Immagini/img_Convalida.png"
                                    ToolTip="Convalida Comunale dell'Assestamento" CausesValidation="False" OnClientClick="document.getElementById('splash').style.visibility = 'visible';Convalida();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Text="Label" Visible="False" Width="580px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="TipoAllegato" runat="server" Value="0" />
                    <asp:HiddenField ID="IdAssestamento" runat="server" Value="0" />
                    <asp:HiddenField ID="ConfCompleto" runat="server" Value="0" />
                    <asp:HiddenField ID="soloLettura" runat="server" Value="0" />
                    <asp:HiddenField ID="salvaok" runat="server" Value="0" />
                    <asp:HiddenField ID="titStampa" runat="server" Value="" />
                </td>
            </tr>
        </table>
        <script type="text/javascript" language="javascript">
            document.getElementById('splash').style.visibility = 'hidden';
        //document.getElementById('dvvvPre').style.visibility = 'hidden';
        </script>
    </form>
</body>
</html>
