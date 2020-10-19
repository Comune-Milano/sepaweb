<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Assestamento.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_Assestamento" %>

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
            text-align: center;
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
            'notnumbers': /[^\d\-\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');

        };

        function ApriEventi() {
            window.open('Eventi_Assest.aspx?IDASSEST=' + document.getElementById('IdAssestamento').value, 'Eventi', '');

            //alert('Funzione non disponibile!');
        }
        function PrintAss() {

            window.open('Stampe.aspx?ID=' + document.getElementById('IdAssestamento').value + '&CHIAMA=ASSEST', 'PrintAssest', '');

        }

    </script>
</head>
<body>
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server">
    <asp:HiddenField ID="IdAssestamento" runat="server" Value="0" />
    <asp:HiddenField ID="compl" runat="server" Value="10" />
    <asp:HiddenField ID="IDSTATO" runat="server" Value="0" />
    <table style="width: 99%; position: absolute; top: 15px; left: 9px;">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 10%; vertical-align: bottom; text-align: right">
                            <asp:ImageButton ID="indietro" runat="server" OnClientClick="document.getElementById('splash').style.visibility = 'visible';"
                                ImageUrl="../../../NuoveImm/Img_Indietro.png" TabIndex="-1" ToolTip="Indietro" />
                            &nbsp;
                        </td>
                        <td style="width: 10%; vertical-align: middle; text-align: right">
                            <asp:ImageButton ID="btnSalva" runat="server" OnClientClick="document.getElementById('splash').style.visibility = 'visible';"
                                ImageUrl="../../../NuoveImm/Img_Salva.png" TabIndex="-1" ToolTip="Salva" />
                        </td>
                        <td style="width: 10%; vertical-align: middle; text-align: right">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../../../NuoveImm/Img_Esci.png" 
                            alt="Esci" ToolTip="Esci"/>
                        </td>
                        <td style="width: 70%;" class="style1">
                            <span class="style2">ASSESTAMENTO ESERCIZIO FINANZIARIO
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
                        <td style="width: 70%">
                            <asp:Label ID="TXTStruttura" runat="server" Font-Bold="True" Font-Italic="False"
                                Font-Names="arial" Font-Size="10pt" Font-Strikeout="False" ForeColor="Maroon"></asp:Label>
                        </td>
                        <td style="width: 30%;">
                            <asp:Label ID="StrutturaCompletata" runat="server" Font-Bold="True" Font-Italic="False"
                                Font-Names="arial" Font-Size="10pt" Font-Strikeout="False" ForeColor="Maroon"
                                BackColor="#F8F32B"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <div id="Div_DataGridView" style="height: 400px; overflow: auto; width: 100%;">
                    <asp:DataGrid ID="DataGrid3" runat="server" AllowSorting="True" AutoGenerateColumns="False"
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
                                <HeaderStyle Width="30%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BUDGET_INIZIALE" HeaderText="BUDGET INIZIALE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="10%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ASSESTAMENTI_PRECEDENTI" HeaderText="ASSESTAMENTI PRECEDENTI">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="10%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VARIAZIONI" HeaderText="VARIAZIONI">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="10%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SPESO" HeaderText="SPESO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RESIDUO" HeaderText="RESIDUO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="ASSESTAMENTO RICHIESTO">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtImpAssest" runat="server" Style="text-align: right" Width="75px"
                                        Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.ASSESTAMENTO") %>'
                                        MaxLength="12"></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="12%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="APPROVATO" Visible="False">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="70px" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                            Wrap="True" BorderStyle="None" BackColor="#507CD1" />
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
            <td style="width: 100%">
                <table width="100%">
                    <tr>
                        <td align="right" style="width: 100%">
                           <asp:ImageButton ID="completa" runat="server" ImageUrl="../../../NuoveImm/ImgCompleta.png" 
                            onclientclick="document.getElementById('splash').style.visibility='visible';"
                            alt="Completa Assestamento" ToolTip="Completa Assestamento" />
                            <asp:ImageButton ID="convalida" runat="server" ImageUrl="../../../NuoveImm/img_ConvAler.png" 
                            alt="Completa Assestamento" ToolTip="Completa Assestamento" onclientclick="ConfermaAler();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
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

