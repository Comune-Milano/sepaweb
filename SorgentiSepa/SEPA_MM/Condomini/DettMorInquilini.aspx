<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettMorInquilini.aspx.vb"
    Inherits="Condomini_DettMorInquilini" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <script type="text/javascript" src="prototype.lite.js"></script>
    <script type="text/javascript" src="moo.fx.js"></script>
    <script type="text/javascript" src="moo.fx.pack.js"></script>
    <title>Dettagli Morosità Inquilini</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <script type="text/javascript" language="javascript">
        window.name = "modal";

        function SostPuntVirg(e, obj) {
            var keyPressed;
            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {
                event.keyCode = 0;
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            }

        };

        function CompletaData(e, obj) {
            var sKeyPressed;
            sKeyPressed = (window.event) ? event.keyCode : e.which;
            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
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

        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13) {

                e.preventDefault();
            }
        }
        function $onkeydown() {
            if (event.keyCode == 13) {
                //alert('ATTENZIONE!E\'stato premuto erroneamente il tasto invio! Utilizzare il mouse o il tasto TAB per spostarsi nei campi di testo!');
                //history.go(0);
                document.getElementById('txtModificato').value = '111'
                event.keyCode = 9;
            }
        }

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
    </script>
</head>
<body style="background-attachment: fixed; background-image: url('Immagini/SfondoContratto.png');
    background-repeat: no-repeat; width: 885px;">
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server" target="modal">
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <asp:Label ID="lblTitolo" runat="server" Style="position: absolute; top: 22px; left: 7px;
            width: 100%" Text="Dettagli Morosità Inquilini"></asp:Label>
    </span></strong>
    <br />
    <br />
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="18%">
                <asp:Label ID="Label25" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data Documentazione"
                    Font-Bold="True" Width="90%"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDataDocumentazione" runat="server" Width="90px" BackColor="White"
                    TabIndex="1" MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left"
                    ReadOnly="true"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2" width="95%" style="text-align: left; vertical-align: top">
                <div style="overflow: auto; width: 100%; height: 330px;" id="DivMorositaInquilini">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:DataGrid ID="DataGridDettMorosita" runat="server" AutoGenerateColumns="False"
                            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Style="z-index: 105;
                            left: 193px; top: 54px" Width="99%" TabIndex="10" BorderColor="#000033" BorderWidth="1px"
                            CellPadding="1" CellSpacing="1">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle ForeColor="Black" />
                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundColumn DataField="ID_INTESTATARIO" HeaderText="ID_INTESTATARIO" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_MOROSITA" HeaderText="ID_MOROSITA" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_UI" HeaderText="ID_UI" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="POSIZIONE_BILANCIO" HeaderText="POS.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Width="40px" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="INQUILINO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOMINATIVO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="40%" Font-Bold="True" Font-Italic="False" 
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                        HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                        Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="IMPORTO">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>' Width="80px"></asp:TextBox>
                                        <asp:Label ID="LabelImporto" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NOTE">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>' Width="250px"></asp:TextBox>
                                        <asp:Label ID="LabelNote" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="40%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO" Visible="False">
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </span></strong>
                </div>
            </td>
            <td style="text-align: left; vertical-align: top">
                <br />
                <img alt="Aggiungi" src="Immagini/40px-Crystal_Clear_action_edit_add.png" onclick="OpInquil.toggle();"
                    id="AddInquilini" style="cursor: pointer" /><br />
                <br />
                <asp:ImageButton ID="btnDeleteInquilini" runat="server" ImageUrl="~/Condomini/Immagini/minus_icon.png"
                    Style="z-index: 102; left: 392px; top: 387px" OnClientClick="DeleteConfirm()"
                    ToolTip="Elimina Elemento Selezionato" TabIndex="-1" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left; vertical-align: top" colspan="2">
                <asp:TextBox ID="txtmia" runat="server" BackColor="#F8F8F8" BorderColor="White" BorderStyle="None"
                    Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="100"
                    ReadOnly="True" Style="left: 13px; top: 197px" Width="100%">Nessuna Selezione</asp:TextBox>
            </td>
            <td style="text-align: right; vertical-align: top">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 104; left: 9px; top: 222px" Visible="False" Width="80%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:ImageButton ID="btnSalvaDettMorInquilini" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                    TabIndex="12" ToolTip="Salva" Style="height: 16px;" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
                    TabIndex="11" ToolTip="Esci dalla finestra" Style="height: 16px" />
            </td>
            <td>
            </td>
        </tr>
    </table>
    <div id="DivAddInquilini" style="top: 59px; left: 5px; width: 883px; height: 418px;
        position: absolute; visibility:hidden ">
        <asp:Image ID="Image1" runat="server" BackColor="White" ImageUrl="~/ImmDiv/DivMGrande.png"
            Style="z-index: 100; left: -2px; position: absolute; top: 0px; height: 428px;
            width: 885px; margin-right: 0px;" />
        <table style="width: 91%; position: absolute; z-index: 202; height: 76%; top: 35px;
            left: 36px;">
            <tr>
                <td style="text-align: left; vertical-align: top">
                    <div style="overflow: auto; width: 100%; height: 301px;" id="DivInquilini">
                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                            <asp:DataGrid ID="DataGridElencoInquilini" runat="server" AutoGenerateColumns="False"
                                BackColor="White" Font-Bold="False" Font-Italic="False" 
                            Font-Names="Arial" Font-Overline="False"
                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Style="z-index: 105;
                                left: 193px; top: 54px" Width="97%" TabIndex="10" 
                            BorderColor="#000033" BorderWidth="1px"
                                CellPadding="1" CellSpacing="1" AllowPaging="True" PageSize="50">
                                <PagerStyle Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle ForeColor="Black" />
                                <HeaderStyle BackColor="WhiteSmoke" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                    ForeColor="#0000C0" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID_UI" HeaderText="ID_UI" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="ID_INTESTARIO" HeaderText="ID_INTESTARIO" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="SELEZIONA">
                                        <ItemTemplate>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkSeleziona" runat="server" TabIndex="-1" />
                                            <asp:Label ID="Label2" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="POSIZIONE_BILANCIO" HeaderText="POS. BIL."></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="INQUILINO">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbInquilino" runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="SCALA">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCALA") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="INTERNO">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTERNO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="PIANO">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PIANO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </span></strong>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top">
                    <asp:ImageButton ID="Aggiungi" runat="server" ImageUrl="~/Condomini/Immagini/Aggiungi.png"
                        TabIndex="11" />
                    <img alt="Esci" src="../NuoveImm/Img_Esci.png" onclick="OpInquil.toggle();" id="ImgEsci"
                        style="cursor: hand" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="ReadOnlyxMoro" runat="server" Value="0" />
    <asp:HiddenField ID="txtidIntestatario" runat="server" Value="0" />
    <asp:HiddenField ID="txtConfElimina" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <script type="text/javascript">
        OpInquil = new fx.Opacity('DivAddInquilini', { duration: 200 });
        OpInquil.hide();


        function DeleteConfirm() {
            if (document.getElementById('txtidIntestatario').value != 0) {
                var Conferma
                Conferma = window.confirm("Attenzione...Confermi di voler eliminare il dato selezionato?");
                if (Conferma == false) {
                    document.getElementById('txtConfElimina').value = '0';
                }
                else {
                    document.getElementById('txtConfElimina').value = '1';
                }
            }
        }

        if (document.getElementById('ReadOnlyxMoro').value == 1) {
            document.getElementById('AddInquilini').style.visibility = 'hidden';

        };
    </script>
    </form>
</body>
</html>
