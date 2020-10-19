<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConsGestione.aspx.vb" Inherits="Condomini_ConsGestione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" src="prototype.lite.js"></script>
    <script type="text/javascript" src="moo.fx.js"></script>
    <script type="text/javascript" src="moo.fx.pack.js"></script>
    <script type="text/javascript" language="javascript">
        window.name = "modal";
        function CompletaData(e, obj) {

            // Check if the key is a number
            var sKeyPressed;

            sKeyPressed = (window.event) ? event.keyCode : e.which;

            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    // don't insert last non-numeric character
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
            else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                }
                else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        // make sure the field doesn't exceed the maximum length
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
            }
        }

        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            document.getElementById('txtModificato').value = '1';

        }
        function delPointer(obj) {
            obj.value = obj.value.replace('.', '');
            document.getElementById(obj.id).value = obj.value;
        }

        //    function Differenza(obj) { 
        //    var risultato

        //    if (obj.value.replace(',', '.') >= 0){
        //    risultato=(obj.value.replace(',', '.') - document.getElementById(obj.id.replace('txtConsuntivo', 'txtPreventivo')).value.replace(',', '.'))
        //    risultato = risultato.toFixed(2)	
        //    document.getElementById(obj.id.replace('txtConsuntivo', 'txtConguaglio')).value = risultato.replace('.', ',')
        //                                            }
        //    }

        function Differenza(obj) {
            var a

            if (obj.value.replace(',', '').replace('.', '') > 0) {
                a = (parseFloat(obj.value.replace('.', '').replace(',', '.')) - parseFloat(document.getElementById(obj.id.replace('txtConsuntivo', 'txtPreventivo')).value.replace('.', '').replace(',', '.')))
                a = a.toFixed(2)
                if (a.substring(a.length - 3, 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - 2);
                    var dascrivere = a.substring(a.length - 3, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = risultato + '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3)
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                    }
                    risultato = dascrivere + risultato + ',' + decimali
                    document.getElementById(obj.id.replace('txtConsuntivo', 'txtConguaglio')).value = risultato
                }
                else {
                    document.getElementById(obj.id.replace('txtConsuntivo', 'txtConguaglio')).value = a.replace('.', ',')

                }
            }
        }

        function DifferenzaMor(obj) {
            if (obj.value.replace(',', '').replace('.', '') > 0) {
                a = (parseFloat(obj.value.replace('.', '').replace(',', '.')) - parseFloat(document.getElementById('txtMorPreventivo').value.replace('.', '')))
                a = a.toFixed(2)
                if (a.substring(a.length - 3, 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - 2);
                    var dascrivere = a.substring(a.length - 3, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = risultato + '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3)
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                    }
                    risultato = dascrivere + risultato + ',' + decimali
                    document.getElementById('txtMorConguaglio').value = risultato
                }
                else {
                    document.getElementById('txtMorConguaglio').value = a.replace('.', ',')

                }

            }

        }
        //    function AutoDecimal2(obj) {
        //        if (obj.value.replace(',', '.') > 0) {
        //            var a = obj.value.replace(',', '.');
        //            a = parseFloat(a).toFixed(2)
        //            document.getElementById(obj.id).value = a.replace('.', ',')
        //        }
        //    }
        function AutoDecimal2(obj) {
            obj.value = obj.value.replace('.', '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a.substring(a.length - 3, 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - 2);
                    var dascrivere = a.substring(a.length - 3, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                    }
                    risultato = dascrivere + risultato + ',' + decimali
                    document.getElementById(obj.id).value = risultato
                }
                else {
                    document.getElementById(obj.id).value = a.replace('.', ',')
                }

            }
        }
        function UpdateSituazPat() {
            if (document.getElementById('Domanda').value != '0') {
                var Conferma
                Conferma = window.confirm("Attenzione...Desidera aggiornare anche la Situazione Patrimoniale?");
                if (Conferma == false) {
                    document.getElementById('AggSitPat').value = '0';

                }
                else {
                    document.getElementById('AggSitPat').value = '1';

                }
            }
        }

        function ConfermaUscita() {
            if (document.getElementById('txtModificato').value == 1) {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche.Uscire senza salvare?");
                if (chiediConferma == true) {
                    document.getElementById('txtesci').value = '1';
                    //                '<%=Session("MODIFYMODAL")%>' = '1';

                }

            }
            else {
                document.getElementById('txtesci').value = '1';
            }

        }
    </script>
    <title>Riepilogo Gestione Condominiale</title>
    <style type="text/css">
        #form1
        {
            width: 891px;
            height: 545px;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url(Immagini/SfondoContratto.png);
    background-repeat: no-repeat">
    <form id="form1" runat="server" target="modal">
    <div>
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
            <asp:Label ID="lblTitolo" runat="server" Style="position: absolute; top: 22px; left: 13px;"
                Text="Gestione Condominio : nomeCond"></asp:Label>
        </span></strong>
    </div>
    <table style="width: 883px; position: absolute; top: 53px; left: 10px;">
        <tr>
            <td style="vertical-align: top; text-align: left; width: 864px;">
                <table width="100%" style="border-right: gainsboro thin solid; border-top: gainsboro thin solid;
                    border-left: gainsboro thin solid; border-bottom: gainsboro thin solid; vertical-align: top;
                    text-align: left;">
                    <tr>
                        <td style="width: 76px">
                            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="9pt" Text="GESTIONE*"></asp:Label>
                        </td>
                        <td style="width: 83px">
                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="9pt" Text="ANNO*"></asp:Label>
                        </td>
                        <td style="width: 77px">
                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="9pt" Text="GESTIONE*"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="9pt" Text="ANNO*"></asp:Label>
                        </td>
                        <td style="width: 237px">
                            <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="9pt" Text="TIPO*"></asp:Label>
                        </td>
                        <td>
                            <%--<asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="9pt" Text="N° RATE*"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 76px; height: 26px">
                            <asp:TextBox ID="txtInizioGest" runat="server" Width="55px" BackColor="White" TabIndex="1"
                                MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="9pt" Text="/"></asp:Label>
                        </td>
                        <td style="height: 26px; width: 83px; text-align: left;">
                            <asp:TextBox ID="txtAnnoInizio" runat="server" Width="60px" BackColor="White" TabIndex="2"
                                MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: center"
                                Text="-" Width="10px"></asp:Label>
                        </td>
                        <td style="height: 26px; width: 77px; text-align: left;">
                            <asp:TextBox ID="TxtFineGest" runat="server" Width="55px" BackColor="White" TabIndex="3"
                                MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                            &nbsp;
                            <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="9pt" Text="/"></asp:Label>
                        </td>
                        <td style="height: 26px; text-align: left;">
                            <asp:TextBox ID="TxtAnnoFine" runat="server" Width="60px" BackColor="White" TabIndex="4"
                                MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="height: 26px; width: 237px;">
                            <asp:DropDownList ID="cmbTipoGest" runat="server" Style="top: 109px; left: 9px; right: 481px;"
                                Font-Names="Arial" Font-Size="9pt" TabIndex="5" Width="120px" BackColor="White"
                                Enabled="False">
                                <asp:ListItem Value="O">ORDINARIO</asp:ListItem>
                                <asp:ListItem Value="S">STRAORDINARIO</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="height: 26px">
                            <%--<asp:DropDownList ID="cmbNumRate" runat="server" Style="top: 109px; left: 9px; right: 481px;"
                                Font-Names="Arial" Font-Size="9pt" TabIndex="6" Width="100px" BackColor="White"
                                AutoPostBack="True" Enabled="False">
                            </asp:DropDownList>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 76px; height: 26px">
                            &nbsp;
                        </td>
                        <td style="height: 26px; width: 83px; text-align: left;">
                            &nbsp;
                        </td>
                        <td style="height: 26px; width: 77px; text-align: left;">
                            &nbsp;
                        </td>
                        <td style="height: 26px; text-align: left;">
                            &nbsp;
                        </td>
                        <td style="height: 26px; width: 237px;">
                            &nbsp;
                        </td>
                        <td style="height: 26px; text-align: right;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div style="overflow: auto; width: 100%; height: 175px;" id="dIVvOCI">
                    <asp:DataGrid ID="DataGridVociSpesa" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="1" Style="z-index: 105;
                        left: 8px; top: 32px" CellPadding="0" HorizontalAlign="Left" Width="99%">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="#0000CC"
                            HorizontalAlign="Center" />
                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="IDVOCE" HeaderText="IDVOCE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="VOCE">
                                <HeaderStyle Width="40%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CONSUNTIVO" HeaderText="CONSUNTIVO" Visible="False">
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="CONSUNTIVO">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtConsuntivo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Style="text-align: right;" Width="70px" Text='<%# DataBinder.Eval(Container, "DataItem.CONSUNTIVO") %>'></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="PREVENTIVO" HeaderText="PREVENTIVO" Visible="False">
                                <HeaderStyle Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PREVENTIVO">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPreventivo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        ReadOnly="True" Style="text-align: right;" Text='<%# DataBinder.Eval(Container, "DataItem.PREVENTIVO") %>'
                                        Width="70px"></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CONGUAGLIO" HeaderText="CONGUAGLIO" Visible="False">
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="CONGUAGLIO">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtConguaglio" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Style="text-align: right;" Width="70px" Text='<%# DataBinder.Eval(Container, "DataItem.CONGUAGLIO") %>'></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="FL_TOTALE" HeaderText="FL_TOTALE" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:DataGrid></div>
                <table style="vertical-align: bottom; width: 98%; text-align: left">
                    <tr>
                        <td style="width: 40%; text-align: center">
                            <asp:ImageButton ID="btnConguaglio" runat="server" ImageUrl="~/Condomini/Immagini/Img_Totale.png"
                                Style="z-index: 102; left: 392px; top: 387px; height: 16px;" ToolTip="Aggiungi"
                                CausesValidation="False" />
                        </td>
                        <td style="width: 20%; text-align: right">
                            &nbsp;
                            <asp:TextBox ID="totConsuntivo" runat="server" BackColor="White" Enabled="False"
                                Font-Names="Arial" Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1"
                                Width="70px"></asp:TextBox>
                        </td>
                        <td style="width: 20%; text-align: right">
                            <asp:TextBox ID="totPreventivo" runat="server" BackColor="White" Enabled="False"
                                Font-Names="Arial" Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1"
                                Width="70px"></asp:TextBox>
                        </td>
                        <td style="width: 20%; text-align: right">
                            <asp:TextBox ID="totConguaglio" runat="server" BackColor="White" Enabled="False"
                                Font-Names="Arial" Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1"
                                Width="70px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div style="overflow: auto; width: 100%; height: 65px;" id="dIVvOCI0">
                    <asp:DataGrid ID="DataGridVociSpMor" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="1" Style="z-index: 105;
                        left: 8px; top: 32px" CellPadding="0" HorizontalAlign="Left" Width="99%">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="#0000CC"
                            HorizontalAlign="Center" />
                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="IDVOCE" HeaderText="IDVOCE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE">
                                <HeaderStyle Width="40%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CONSUNTIVO" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtConsuntivoMor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Style="text-align: right;" Width="70px" Text='<%# DataBinder.Eval(Container, "DataItem.CONSUNTIVO") %>'></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="PREVENTIVO" HeaderText="PREVENTIVO" Visible="False">
                                <HeaderStyle Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPreventivoMor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        ReadOnly="True" Style="text-align: right;" Text='<%# DataBinder.Eval(Container, "DataItem.PREVENTIVO") %>'
                                        Width="70px"></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CONGUAGLIO" HeaderText="CONGUAGLIO" Visible="False">
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtConguaglioMor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Style="text-align: right;" Width="70px" Text='<%# DataBinder.Eval(Container, "DataItem.CONGUAGLIO") %>'></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="FL_TOTALE" HeaderText="FL_TOTALE" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:DataGrid></div>
                <br />
                <table style="vertical-align: bottom; width: 99%; text-align: left">
                    <tr>
                        <td style="width: 40%; text-align: left">
                            <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Red" Height="18px" Style="z-index: 104; left: 9px; top: 222px" Visible="False"
                                Width="207px"></asp:Label>
                        </td>
                        <td style="width: 20%; text-align: right">
                            <asp:ImageButton ID="btnLiberiAbusivi" runat="server" ImageUrl="~/Condomini/Immagini/Img_LibeAbusivi.png"
                                TabIndex="14" ToolTip="Visualizza l'elenco delle unità libere o occupate abusivamente" />
                        </td>
                        <td style="width: 20%; text-align: right">
                            <asp:ImageButton ID="btnSituazPatr" runat="server" ImageUrl="~/Condomini/Immagini/Img_SituazPatr.png"
                                TabIndex="14" ToolTip="Visualizza la situazione patrimoniale" />
                        </td>
                        <td style="width: 20%; text-align: right">
                            <asp:ImageButton ID="btnSalvaCambioAmm" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                TabIndex="31" OnClientClick="UpdateSituazPat();" ToolTip="Salva" />
                        </td>
                        <td style="width: 20%; text-align: right">
                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
                                TabIndex="11" ToolTip="Esci" OnClientClick="ConfermaUscita();" Style="height: 16px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: text-top; text-align: right; width: 864px; height: 22px;">
                <asp:HiddenField ID="Domanda" runat="server" Value="0" />
                <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
                <asp:HiddenField ID="txtesci" runat="server" Value="0" />
                <asp:HiddenField ID="txtSalvato" runat="server" Value="0" />
                <asp:HiddenField ID="AggSitPat" runat="server" Value="0" />
                <asp:HiddenField ID="idPianoF" runat="server" Value="0" />
            </td>
        </tr>
    </table>
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <asp:HiddenField ID="TextBox1" runat="server" />
    </span></strong>
    </form>
</body>
</html>
