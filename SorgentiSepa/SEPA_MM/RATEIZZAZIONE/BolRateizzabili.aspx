<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BolRateizzabili.aspx.vb" Inherits="RATEIZZAZIONE_BolRateizzabili" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Elenco delle Bollette Rateizzabili</title>
    <style type="text/css">
        .style1 {
            font-family: ARIAL;
            font-weight: bold;
            font-size: 12pt;
            color: #000099;
            background-color: #B8C7E1;
            text-align: center;
        }
    </style>
    <script type="text/javascript" language="javascript">
        window.name = "modal";

        function Somma(imp, pagato, obj) {
            if (imp != 0) {
                var a = parseFloat(imp) - parseFloat(pagato);
                if (a != 'NaN') {
                    var b = parseFloat(document.getElementById('SumSelected').value.replace('.', '').replace(',', '.'))
                    var risultato;
                    if (obj.checked == true) { risultato = parseFloat(a + b).toFixed(2) }
                    else { risultato = parseFloat(b - a).toFixed(2) }

                    if (risultato.substring(risultato.length - 3, 0).length >= 4) {
                        var decimali = risultato.substring(risultato.length, risultato.length - 2);
                        var dascrivere = risultato.substring(risultato.length - 3, 0);
                        var risultNew = '';
                        while (dascrivere.replace('-', '').length >= 4) {
                            risultNew = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultNew
                            dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                        }
                        risultNew = dascrivere + risultNew + ',' + decimali
                        //document.getElementById(obj.id).value = a.replace('.', ',')
                        //document.getElementById(obj.id).value = risultNew
                    }
                    else {
                        risultNew = risultato.replace('.', ',')
                    }
                    document.getElementById('SumSelected').value = risultNew
                    document.getElementById('txtSomma').value = 'IMPORTO TOTALE DELLA RATEIZZAZIONE PARI A €. ' + document.getElementById('SumSelected').value + ' AL NETTO DELLE QUOTE SINDACALI';

                    if (document.getElementById('importoMaxRateizzabile').value > 0) {
                        var impMaxRat = parseFloat(document.getElementById('importoMaxRateizzabile').value.replace(/\./g, '').replace(',', '.'));
                        var impSelezionato = parseFloat(risultNew.replace(/\./g, '').replace(',', '.'));
                        if (impMaxRat < impSelezionato) {
                            alert('Importo selezionato maggiore della somma totale rateizzabile!');
                            obj.checked = false;
                            var sommaSelezionata = parseFloat(impSelezionato - a).toFixed(2); 

                            if (sommaSelezionata.substring(sommaSelezionata.length - 3, 0).length >= 4) {
                                var decimali = sommaSelezionata.substring(sommaSelezionata.length, sommaSelezionata.length - 2);
                                var dascrivere = sommaSelezionata.substring(sommaSelezionata.length - 3, 0);
                                var risultNew2 = '';
                                while (dascrivere.replace('-', '').length >= 4) {
                                    risultNew2 = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultNew2
                                    dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                                }
                                risultNew2 = dascrivere + risultNew2 + ',' + decimali
                            }
                            else {
                                risultNew2 = sommaSelezionata.replace('.', ',')
                            }

                             document.getElementById('SumSelected').value = risultNew2
                            document.getElementById('txtSomma').value = 'IMPORTO TOTALE DELLA RATEIZZAZIONE PARI A €. ' + risultNew2 + ' AL NETTO DELLE QUOTE SINDACALI';

                        }
                    }
                }
            }

        };
    </script>
</head>
<body>
    <div id="splash" style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee; z-index: 500;">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px; margin-top: -48px; background-image: url('../NuoveImm/sfondo2.png');">
            <table style="width: 100%; height: 100%; font-family: Arial; font-size: 8pt;">
                <tr>
                    <td valign="middle" align="center">
                        <img alt="Caricamento" src="../immagini/load.gif" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center">Caricamento . . .
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <form id="form1" runat="server" target="modal">
        <table width="100%">
            <tr>
                <td class="style1">ELENCO BOLLETTE RATEIZZABILI</td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rdbBoll" runat="server" AutoPostBack="True"
                        Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        RepeatDirection="Horizontal" onClick="document.getElementById('Selezionati').value=0;">
                        <asp:ListItem Value="T" Selected="True">TUTTE</asp:ListItem>
                        <asp:ListItem Value="S">SCADUTE</asp:ListItem>
                    </asp:RadioButtonList>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True"
                        Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red"
                        Text="Label" Visible="False" Width="98%"></asp:Label>

                </td>
            </tr>
            <tr>
                <td align="left">
                    <div style="width: 98%; height: 390px; overflow: auto;">
                        <asp:DataGrid ID="DgvBolRateizzabili" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            BackColor="White" BorderWidth="1px" Font-Bold="False"
                            Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            PageSize="1" Style="table-layout: auto; z-index: 101; left: 16px; clip: rect(auto auto auto auto); direction: ltr; top: 200px; border-collapse: separate"
                            Width="98%"
                            CellPadding="1" CellSpacing="1">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn HeaderText="ID" Visible="False" DataField="ID"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ACRONIMO" HeaderText="TIPO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                        Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RIFERIMENTO_DA" HeaderText="PERIODO DAL">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RIFERIMENTO_A" HeaderText="RIFERIMENTO AL">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IMPORTO_TOTALE" HeaderText="IMPORTO €">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_SCADENZA" HeaderText="SCADENZA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IMPORTO_PAGATO" HeaderText="PAGATO €">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="SELEZIONA">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderTemplate>
                                        <asp:Button ID="btnSelectAll" runat="server" Font-Bold="True"
                                            Font-Names="Arial" Font-Size="8pt" ForeColor="#0000C0"
                                            OnClick="btnSelectAll_Click" Text="SELEZIONA" Width="76px" TabIndex="-1"
                                            OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkSelected" runat="server" TextAlign="Left" />
                                        <asp:Label runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                        Wrap="False" />
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle BackColor="#C5DCF5" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" Wrap="False" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 3pt">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtSomma" runat="server" BackColor="White" BorderColor="White"
                        BorderStyle="None" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        ForeColor="Blue" ReadOnly="True" Style="text-align: left" Width="100%">IMPORTO TOTALE DELLA RATEIZZAZIONE PARI A €. 0.00 AL NETTO DELLE QUOTE SINDACALI</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnIndietro" runat="server" ImageUrl="../NuoveImm/Img_IndietroGrande.png"
                                    Width="80px" />
                            </td>
                            <td style="text-align: right">
                                <asp:ImageButton ID="btnProcedi" runat="server"
                                    ImageUrl="../NuoveImm/Img_Conferma.png"
                                    ToolTip="Conferma la selezione, e procedi con la rateizzazione"
                                    OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />

                                <img id="exit" alt="Esci" src="../NuoveImm/Img_Esci_AMM.png"
                                    title="Esci" style="cursor: pointer"
                                    onclick="self.close();" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:HiddenField ID="SumSelected" runat="server" Value="0" />
                                <asp:HiddenField ID="Selezionati" runat="server" Value="0" />
                                <asp:HiddenField ID="cfPivaNull" runat="server" Value="0" />
                                <asp:HiddenField ID="ratMor" runat="server" Value="0" />
                                <asp:HiddenField ID="dataPres" runat="server" Value="0" />
                                <asp:HiddenField ID="codRU" runat="server" Value="0" />
                                <asp:HiddenField ID="idRateizz" runat="server" Value="0" />
                                <asp:HiddenField ID="importoMaxRateizzabile" runat="server" Value="0" />
                            </td>
                            <td>&nbsp;</td>
                            <td style="text-align: right">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <script type="text/javascript" language="javascript">        
            document.getElementById('splash').style.visibility = 'hidden';
        </script>
    </form>
</body>
</html>
