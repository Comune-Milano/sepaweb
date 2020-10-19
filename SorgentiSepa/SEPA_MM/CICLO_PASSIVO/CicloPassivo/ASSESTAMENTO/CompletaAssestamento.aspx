<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CompletaAssestamento.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_CompletaAssestamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1
        {
            width: 783px;
            height: 517px;
        }
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 12pt;
            color: #801f1c;
            text-align: center;
        }
        .style2
        {
            font-size: 12pt;
        }
        .style3
        {
            width: 50%;
        }
        .style4
        {
            width: 60%;
        }
    </style>
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
            'notnumbers': /[^\d\,]/g
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

        function chiediConferma() {
            Conferma = window.confirm("ATTENZIONE...\nCliccando su OK  l\'assestamento sarà convalidabile dal Gestore.\nProcedere con l\'operazione?");
            if (Conferma == true) {
                document.getElementById("ConfCompleto").value = '1';
            }
            else
            { document.getElementById("ConfCompleto").value = '0'; }

        }

        function ApriEventi() {
            window.open('Eventi_Assest.aspx?IDASSEST=' + document.getElementById('IdAssestamento').value, 'Eventi', '');

            //alert('Funzione non disponibile!');
        }

        function PrintAss() {
            window.open('Stampe.aspx?IDassestamento=' + document.getElementById('IdAssestamento').value + '&IDstato= ' + document.getElementById('IDSTATO').value + '&CHIAMA=ASSEST', 'PrintAssest', '');
        }

    </script>
</head>
<body>
    <!-- Da mettere subito dopo l'apertura del tag <body> -->
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server">
    <table style="width: 99%; position: absolute; top: 15px; left: 9px;">
        <tr>
            <td style="width: 100%">
                <table style="width: 100%;">
                    <tr>
                        <td style="vertical-align: bottom; text-align: left">
                            &nbsp;
                        </td>
                        <td style="vertical-align: bottom; text-align: left">
                            <asp:ImageButton ID="indietro" runat="server" ImageUrl="../../../NuoveImm/Img_indietro.png"
                                Style="z-index: 100; left: 584px; position: static; top: 32px" TabIndex="-1"
                                alt="Indietro" ToolTip="Indietro" />
                        </td>
                        <td style="vertical-align: bottom; text-align: left">
                            <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="../../../NuoveImm/Img_Stampa.png"
                                Style="z-index: 100; left: 584px; position: static; top: 32px" TabIndex="-1"
                                ToolTip="Stampa Assestamento" />
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
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="style2">ASSESTAMENTO ESERCIZIO FINANZIARIO
                                <asp:Label ID="esercizio" runat="server" Text=""></asp:Label></span>
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
                                Font-Size="9pt" Font-Strikeout="False" ForeColor="Maroon"></asp:Label>
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
            <td class="style3">
                <div id="Div_DataGridView" style="height: <%=DG1 %>; overflow: auto; width: 100%;">
                    <asp:DataGrid ID="datagrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                        PageSize="30" Style="table-layout: auto; z-index: 101; left: 8px; clip: rect(auto auto auto auto);
                        direction: ltr; top: 40px; border-collapse: separate" TabIndex="-1" Width="750px"
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
                            <asp:BoundColumn DataField="STRUTTURA" HeaderText="STRUTTURA"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                    <asp:DataGrid ID="datagrid2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                        PageSize="30" Style="table-layout: auto; z-index: 101; left: 8px; clip: rect(auto auto auto auto);
                        direction: ltr; top: 40px; border-collapse: separate" TabIndex="-1" Width="750px"
                        BorderColor="#666666" CellPadding="1">
                        <FooterStyle Font-Bold="True" Wrap="False" BackColor="#507CD1" ForeColor="White" />
                        <EditItemStyle Wrap="False" BackColor="#2461BF" />
                        <SelectedItemStyle Font-Bold="True" Wrap="False" BackColor="#D1DDF1" ForeColor="#333333" />
                        <PagerStyle Position="TopAndBottom" Visible="False" Wrap="False" BackColor="#2461BF"
                            ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="White" Wrap="True" />
                        <ItemStyle Wrap="True" BackColor="#EFF3FB" />
                        <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                            Wrap="True" BorderStyle="None" BackColor="#006699" />
                        <Columns>
                            <asp:BoundColumn DataField="CODICE" HeaderText="CODICE" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VOCE" HeaderText="VOCE" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundColumn>
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
            <td style="height: 1px;">
                <div style="visibility: hidden">
                    <asp:DataGrid ID="datagrid3" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        BorderColor="#666666" BorderWidth="1px" CellPadding="1" Font-Bold="False" Font-Italic="False"
                        Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                        Font-Underline="False" ForeColor="#333333" PageSize="30" Style="table-layout: auto;
                        z-index: 101; left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px;
                        border-collapse: separate" TabIndex="-1" Width="100%">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="False" />
                        <EditItemStyle BackColor="#2461BF" Wrap="False" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" Wrap="False" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Position="TopAndBottom"
                            Visible="False" Wrap="False" />
                        <AlternatingItemStyle BackColor="White" Wrap="False" />
                        <ItemStyle BackColor="#EFF3FB" Wrap="False" />
                        <HeaderStyle BackColor="#006699" BorderStyle="None" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="White" Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="CODICE" HeaderStyle-HorizontalAlign="Center" HeaderText="CODICE">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VOCE" HeaderStyle-HorizontalAlign="Center" HeaderText="VOCE">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RICHIESTO" HeaderStyle-HorizontalAlign="Center" HeaderText="ASSESTAMENTO RICHIESTO"
                                ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                            <asp:BoundColumn DataField="APPROVATO" HeaderStyle-HorizontalAlign="Center" HeaderText="ASSESTAMENTO APPROVATO"
                                ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table width="100%">
                    <tr>
                        <td align="right" style="width: 100%">
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="../../../NuoveImm/ImgCompleta.png"
                                alt="Completa Assestamento" ToolTip="Completa Assestamento" 
                                OnClientClick="document.getElementById('splash').style.visibility='visible';chiediConferma();" />
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
                <asp:HiddenField ID="IdAssestamento" runat="server" Value="0" />
                <asp:HiddenField ID="compl" runat="server" Value="10" />
                <asp:HiddenField ID="ConfCompleto" runat="server" Value="0" />
                <asp:HiddenField ID="IDSTATO" runat="server" Value="0" />
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
