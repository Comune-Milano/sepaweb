<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConvAssestAler.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_ConvAssestAler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Approvazione Assestamento - Gestore</title>
    <style type="text/css">
        #form1
        {
            width: 770px;
        }
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 13pt;
            color: #801f1c;
            text-align: left;
        }
    </style>
    <script type="text/javascript">

        function ConvalidaAler() {
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

            //alert('Funzione non disponibile!');
        }

        function PrintAss() {
            window.open('Stampe.aspx?ID=' + document.getElementById('IdAssestamento').value + '&CHIAMA=ASSALER', 'Print', '');

        }
    </script>
</head>
<body style="background-attachment: fixed; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; background-attachment: fixed; width: 770px; height: 525px;">
    <!-- Da mettere subito dopo l'apertura del tag <body> -->
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="URLdiProvenienza" Value="" />
    <asp:HiddenField ID="CONThidden" runat="server" Value="0" />
    <table style="width: 99%;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="vertical-align: bottom; text-align: right">
                            <asp:ImageButton ID="btnindietro" runat="server" ToolTip="Indietro" AlternateText="Indietro"
                                ImageUrl="../../../NuoveImm/Img_Indietro.png" />
                        </td>
                        <td style="vertical-align: bottom; text-align: right">
                            <img id="Eventi" alt="Eventi" src="../../../NuoveImm/Img_Eventi.png" title="Eventi"
                                style="cursor: pointer" onclick="ApriEventi()" />
                        </td>
                        <td style="vertical-align: bottom; text-align: right">
                            <img id="exit" alt="Esci" src="../../../NuoveImm/Img_Esci.png" title="Esci" style="cursor: pointer"
                                onclick="document.location.href='../../pagina_home.aspx';" />
                        </td>
                        <td class="style1">
                            <asp:Label runat="server" ID="lbl1" Font-Names="Arial" Font-Size="10pt">ASSESTAMENTO ESERCIZIO FINANZIARIO - </asp:Label>
                            <asp:Label ID="esercizio" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                            <asp:Label runat="server" ID="Label1" Font-Names="Arial" Font-Size="10pt"> - Convalida Gestore</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style3" style="text-align: center; font-size: 1pt;">
                <br />
                <table width="100%">
                    <tr>
                        <td style="width: 50%" text-align="left">
                            <asp:Label ID="lblAssestamentoData" runat="server" Font-Bold="True" Font-Italic="False"
                                Font-Names="arial" Font-Size="10pt" Font-Strikeout="False" ForeColor="Maroon"></asp:Label>
                        </td>
                        <td style="width: 50%" text-align="right">
                            <asp:Label ID="AssestamentoCompletato" runat="server" Font-Bold="True" Font-Italic="False"
                                Font-Names="arial" Font-Size="10pt" Font-Strikeout="False" ForeColor="Maroon"
                                BackColor="#F8F32B"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <div id="Div_DataGridView" style="height: 370px; overflow: auto; width: 100%;">
                    <asp:DataGrid ID="DgvApprAssest" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                        PageSize="30" Style="table-layout: auto; z-index: 101; left: 8px; clip: rect(auto auto auto auto);
                        direction: ltr; top: 40px; border-collapse: separate" TabIndex="-1" Width="760px"
                        BorderColor="#666666" CellPadding="1">
                        <FooterStyle Font-Bold="True" Wrap="False" BackColor="#507CD1" ForeColor="White" />
                        <EditItemStyle Wrap="False" BackColor="#2461BF" />
                        <SelectedItemStyle Font-Bold="True" Wrap="False" BackColor="#D1DDF1" ForeColor="#333333" />
                        <PagerStyle Position="TopAndBottom" Visible="False" Wrap="False" BackColor="#2461BF"
                            ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="White" Wrap="True" />
                        <ItemStyle Wrap="True" BackColor="#EFF3FB" />
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
                            Wrap="True" BorderStyle="None" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" BackColor="#507CD1" />
                    </asp:DataGrid>
                </div>
                <div id="Div1" style="height: 10px; width: 100%; visibility: hidden; overflow: auto;">
                    <asp:DataGrid ID="DgvApprAssestCopia" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                        PageSize="30" Style="table-layout: auto; z-index: 101; left: 8px; clip: rect(auto auto auto auto);
                        direction: ltr; top: 40px; border-collapse: separate" TabIndex="-1" Width="760px"
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
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Red" Text="Label" Visible="False" Width="400px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align:left">
                
                <asp:ImageButton ID="btnConfirm" runat="server" ImageUrl="../../../NuoveImm/img_ConvAler.png"
                    ToolTip="Convalida Gestore dell'Assestamento" CausesValidation="False" OnClientClick="document.getElementById('splash').style.visibility = 'visible';ConvalidaAler();" />
                    <asp:ImageButton ID="btnRicarica" runat="server" OnClientClick="document.getElementById('splash').style.visibility = 'visible';"
                    ImageUrl="../../../NuoveImm/Img_Salva.png" Style="visibility: hidden;" TabIndex="-1"
                    ToolTip="Salva" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="IdAssestamento" runat="server" Value="0" />
    <asp:HiddenField ID="ConfAlerCompleto" runat="server" Value="0" />
    <asp:HiddenField ID="soloLettura" runat="server" Value="0" />
    </form>
    <script type="text/javascript" language="javascript">
        document.getElementById('splash').style.visibility = 'hidden';
        //document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
