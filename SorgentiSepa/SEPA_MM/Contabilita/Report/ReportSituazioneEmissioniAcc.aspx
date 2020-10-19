<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportSituazioneEmissioniAcc.aspx.vb"
    Inherits="Contabilita_Report_ReportSituazioneEmissioniAcc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ricerca Situazione Emissioni</title>
    <script type="text/javascript">

        function ScrollPosTipo(obj) {
            document.getElementById('yPosTipo').value = obj.scrollTop;
        }
        function ScrollPosCompetenza(obj) {
            document.getElementById('yPosCompetenza').value = obj.scrollTop;
        }
        function ScrollPosMacrocategorie(obj) {
            document.getElementById('yPosMacrocategorie').value = obj.scrollTop;
        }
        function ScrollPosCategorie(obj) {
            document.getElementById('yPosCategorie').value = obj.scrollTop;
        }


        function CompletaData(e, obj) {
            var sKeyPressed;
            sKeyPressed = (window.event) ? event.keyCode : e.which;
            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    if (window.event) {
                        event.keyCode = 0;
                    } else {
                        e.preventDefault();
                    }
                }
            } else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                } else if (obj.value.length == 5) {
                    obj.value += "/";
                } else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        if (window.event) {
                            event.keyCode = 0;
                        } else {
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
        }
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
    </script>
    <style type="text/css">
        #form1
        {
            width: 782px;
            height: 541px;
        }
    </style>
</head>
<body style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table cellpadding="1" cellspacing="0" width="98%">
                <tr>
                    <td style="width: 100%; font-family: Arial; font-size: 4pt;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <asp:Label ID="Label1" Text="Ricerca Situazione Emissioni per annullamento accertamento" runat="server" Font-Bold="True"
                            Font-Names="Arial" Font-Size="14pt" ForeColor="Maroon" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; font-family: Arial; font-size: 3pt;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Panel ID="Panel1" runat="server" Style="border: 1px solid #507CD1" Width="95%">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="background-color: #507CD1; text-align: center; width: 100%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="width: 10%">
                                                                <asp:CheckBox ID="CheckBoxTipo" runat="server" AutoPostBack="True" />
                                                            </td>
                                                            <td style="width: 90%">
                                                                <asp:Label ID="Label5" runat="server" Text="Tipologia di bollettazione" Font-Names="Arial"
                                                                    Font-Size="9pt" ForeColor="White" Width="100%"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div>
                                                        <asp:Panel ID="PanelTipo" runat="server" onscroll="ScrollPosTipo(this);" Style="overflow: auto;
                                                            height: 72px;">
                                                            <asp:CheckBoxList ID="CheckBoxListTipologiaBollettazione" runat="server" AutoPostBack="True"
                                                                Font-Names="Arial" Font-Size="9pt">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width: 50%">
                                    <asp:Panel ID="Panel2" runat="server" Style="border: 1px solid #507CD1" Width="95%">
                                        <table id="tableBollettazioneStraordinaria" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="background-color: #507CD1; text-align: center; width: 100%">
                                                    <asp:Label ID="Label6" runat="server" Text="Tipologia di bollettazione straordinaria"
                                                        Font-Names="Arial" Font-Size="9pt" ForeColor="White" Width="100%"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="height: 71px; overflow: auto">
                                                        <asp:CheckBoxList ID="CheckBoxListTipologiaStraordinaria" runat="server" Font-Names="Arial"
                                                            Font-Size="9pt">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 22%">
                                    <asp:Label ID="Label2" runat="server" Text="Emissione dal" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="TextBoxEmissioneDal" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Width="70px"></asp:TextBox>
                                </td>
                                <td style="width: 2%">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmissioneDal" runat="server"
                                        ErrorMessage="!" ControlToValidate="TextBoxEmissioneDal" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 3%">
                                    <asp:Label ID="Label3" runat="server" Text="al" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="TextBoxEmissioneAl" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Width="70px"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmissioneAl" runat="server"
                                        ErrorMessage="!" ControlToValidate="TextBoxEmissioneAl" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 18%">
                                    <asp:Label ID="Label4" runat="server" Text="Riferimento dal" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="TextBoxRiferimentoDal" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Width="70px"></asp:TextBox>
                                </td>
                                <td style="width: 2%">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorRiferimentoDal" runat="server"
                                        ErrorMessage="!" ControlToValidate="TextBoxRiferimentoDal" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 5%">
                                    <asp:Label ID="Label55" runat="server" Text="al" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="TextBoxRiferimentoAl" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Width="70px"></asp:TextBox>
                                </td>
                                <td style="width: 3%">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorRiferimentoAl" runat="server"
                                        ErrorMessage="!" ControlToValidate="TextBoxRiferimentoAl" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 22%">
                                    <asp:Label ID="Label7" runat="server" Text="Accertato il" Font-Names="Arial"
                                        Font-Size="9pt"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                        <asp:TextBox ID="TextBoxAccertamento" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Width="70px"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="!"
                                        ControlToValidate="TextBoxAccertamento" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 20%">
                                    <asp:Label ID="Label8" runat="server" Text="Accertato" Font-Names="Arial" 
                                        Font-Size="9pt" Visible="False"></asp:Label>
                                </td>
                                <td style="width: 43%">
                                    <asp:DropDownList ID="DropDownListAccertato" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Width="300px" Enabled="false" Visible="False">
                                        <asp:ListItem Value="2">Tutto</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">Accertato</asp:ListItem>
                                        <asp:ListItem Value="0">Da accertare</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 22%">
                                    <asp:Label ID="Label15" Text="Solo voci da accertare" runat="server" Font-Names="Arial"
                                        Font-Size="9pt" />
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="DropDownListVociDaAccertare" runat="server" Font-Names="Arial"
                                        Font-Size="9pt" Width="70px">
                                        <asp:ListItem Value="2">Tutte</asp:ListItem>
                                        <asp:ListItem Value="1">Sì</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 20%">
                                    <asp:Label ID="Label9" Text="Condomini" runat="server" Font-Names="Arial" Font-Size="9pt" />
                                </td>
                                <td style="width: 43%">
                                    <asp:DropDownList ID="DropDownListCondomini" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Width="300px">
                                        <asp:ListItem Value="-1">Nessun Condominio</asp:ListItem>
                                        <asp:ListItem Value="2">Tutti i Condomini</asp:ListItem>
                                        <asp:ListItem Value="1">Condomini Gestione Diretta</asp:ListItem>
                                        <asp:ListItem Value="0">Condomini Gestione Indiretta</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Panel ID="Panel6" runat="server" Style="border: 1px solid #507CD1" Width="100%">
                                        <table id="table4" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="background-color: #507CD1; text-align: center; width: 100%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="width: 10%">
                                                                <asp:CheckBox ID="CheckBoxTipologieUI" runat="server" AutoPostBack="True" />
                                                            </td>
                                                            <td style="width: 90%">
                                                                <asp:Label ID="Label18" runat="server" Text="Tipologie UI" Font-Names="Arial" Font-Size="9pt"
                                                                    ForeColor="White" Width="100%"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="height: 125px; overflow: auto">
                                                        <asp:CheckBoxList ID="CheckBoxListTipologieUI" runat="server" Font-Names="Arial"
                                                            Font-Size="9pt">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width: 50%">
                                    <asp:Panel ID="Panel7" runat="server" Style="border: 1px solid #507CD1" Width="100%">
                                        <table id="table5" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="background-color: #507CD1; text-align: center; width: 100%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="width: 10%">
                                                                <asp:CheckBox ID="CheckBoxCompetenza" runat="server" AutoPostBack="True" />
                                                            </td>
                                                            <td style="width: 90%">
                                                                <asp:Label ID="Label10" runat="server" Text="Competenza" Font-Names="Arial" Font-Size="9pt"
                                                                    ForeColor="White" Width="100%"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div>
                                                        <asp:Panel ID="PanelCompetenza" runat="server" Style="height: 125px; overflow: auto"
                                                            onscroll="ScrollPosCompetenza(this);">
                                                            <asp:CheckBoxList ID="CheckBoxListCompetenza" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                                AutoPostBack="True">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Panel ID="Panel4" runat="server" Style="border: 1px solid #507CD1" Width="100%">
                                        <table id="table3" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="background-color: #507CD1; text-align: center; width: 100%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="width: 10%">
                                                                <asp:CheckBox ID="CheckBoxMacrocategorie" runat="server" AutoPostBack="True" />
                                                            </td>
                                                            <td style="width: 90%">
                                                                <asp:Label ID="Label17" runat="server" Text="Macrocategorie" Font-Names="Arial" Font-Size="9pt"
                                                                    ForeColor="White" Width="100%"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div>
                                                        <asp:Panel ID="PanelMacrocategorie" runat="server" Style="height: 125px; overflow: auto"
                                                            onscroll="ScrollPosMacrocategorie(this);">
                                                            <asp:CheckBoxList ID="CheckBoxListMacrocategorie" runat="server" Font-Names="Arial"
                                                                Font-Size="9pt" AutoPostBack="True">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width: 67%">
                                    <asp:Panel ID="Panel5" runat="server" Style="border: 1px solid #507CD1" Width="100%">
                                        <table id="table2" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="background-color: #507CD1; text-align: center; width: 100%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="width: 10%">
                                                                <asp:CheckBox ID="CheckBoxCategorie" runat="server" AutoPostBack="True" />
                                                            </td>
                                                            <td style="width: 90%">
                                                                <asp:Label ID="Label16" runat="server" Text="Categorie" Font-Names="Arial" Font-Size="9pt"
                                                                    ForeColor="White" Width="100%"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div>
                                                        <asp:Panel ID="PanelCategorie" runat="server" Style="height: 125px; overflow: auto"
                                                            onscroll="ScrollPosCategorie(this);">
                                                            <asp:CheckBoxList ID="CheckBoxListCategorie" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                                AutoPostBack="True">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: right">
                        <table cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td style="text-align: right" width="90%">
                                    <asp:ImageButton ID="ImageButtonAvanti" runat="server" ImageUrl="~/NuoveImm/Img_Avanti.png"
                                        ToolTip="Avanti" />
                                </td>
                                <td style="text-align: right" width="10%">
                                    <asp:ImageButton ID="ImageButtonEsci" runat="server" ImageUrl="../../NuoveImm/Img_Home.png"
                                        ToolTip="Esci" AlternateText="Esci" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table cellpadding="1" cellspacing="0" width="98%">
                <tr>
                    <td style="width: 100%; font-family: Arial; font-size: 4pt;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <asp:Label ID="Label11" Text="Ricerca Situazione Emissioni" runat="server" Font-Bold="True"
                            Font-Names="Arial" Font-Size="14pt" ForeColor="Maroon" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; font-family: Arial; font-size: 3pt;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 100%">
                                    <asp:Panel ID="Panel3" runat="server" Style="border: 1px solid #507CD1" Width="100%">
                                        <table id="table1" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="background-color: #507CD1; text-align: center; width: 100%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="width: 10%">
                                                                <asp:CheckBox ID="CheckBoxVoci" runat="server" AutoPostBack="True" />
                                                            </td>
                                                            <td style="width: 90%">
                                                                <asp:Label ID="Label14" runat="server" Text="Voci" Font-Names="Arial" Font-Size="9pt"
                                                                    ForeColor="White" Width="100%"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="height: 150px; overflow: auto">
                                                        <asp:CheckBoxList ID="CheckBoxListVoci" runat="server" Font-Names="Arial" 
                                                            Font-Size="9pt" AutoPostBack="True">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 100%">
                                    <asp:Panel ID="Panel9" runat="server" Style="border: 1px solid #507CD1" Width="100%">
                                        <table id="table7" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="background-color: #507CD1; text-align: center; width: 100%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="width: 10%">
                                                                <asp:CheckBox ID="CheckBoxCapitoli" runat="server" AutoPostBack="True" />
                                                            </td>
                                                            <td style="width: 90%">
                                                                <asp:Label ID="Label13" runat="server" Text="Capitoli" Font-Names="Arial" Font-Size="9pt"
                                                                    ForeColor="White" Width="100%"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="height: 100px; overflow: auto">
                                                        <asp:CheckBoxList ID="CheckBoxListCapitoli" runat="server" Font-Names="Arial" Font-Size="9pt">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 100%">
                                    <asp:Panel ID="Panel8" runat="server" Style="border: 1px solid #507CD1" Width="100%">
                                        <table id="table6" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="background-color: #507CD1; text-align: center; width: 100%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="width: 10%">
                                                                <asp:CheckBox ID="CheckBoxEserciziContabili" runat="server" AutoPostBack="True" />
                                                            </td>
                                                            <td style="width: 90%">
                                                                <asp:Label ID="Label12" runat="server" Text="Esercizi contabili" Font-Names="Arial"
                                                                    Font-Size="9pt" ForeColor="White" Width="100%"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="height: 100px; overflow: auto">
                                                        <asp:CheckBoxList ID="CheckBoxListEserciziContabili" runat="server" Font-Names="Arial"
                                                            Font-Size="9pt">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label19" Text="Ordinamento " runat="server" Font-Names="Arial" Font-Size="9pt" />
                        <asp:DropDownList runat="server" ID="Ordinamento" Font-Names="Arial" Font-Size="9pt">
                            <asp:ListItem Text="per Bollettazione" Value="1" />
                            <asp:ListItem Text="per Capitolo" Value="2" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox Text="Dettaglio Anno/Bimestre" runat="server" ID="Dettaglio" Font-Names="Arial"
                            Font-Size="9pt" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 4px;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: right">
                        <table cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td style="text-align: right" width="75%">
                                    <asp:ImageButton ID="ImageButtonIndietro" runat="server" ImageUrl="../../NuoveImm/Img_Indietro2.png"
                                        ToolTip="Avvia Ricerca" AlternateText="Avvia Ricerca" />
                                </td>
                                <td style="text-align: right" width="15%">
                                    <asp:ImageButton ID="ImageButtonAvviaRicerca" runat="server" AlternateText="Avvia Ricerca"
                                        ImageUrl="../../NuoveImm/Img_AvviaRicerca.png" ToolTip="Avvia Ricerca" 
                                        style="height: 20px" />
                                </td>
                                <td style="text-align: right" width="10%">
                                    <asp:ImageButton ID="ImageButtonHome" runat="server" ImageUrl="../../NuoveImm/Img_Home.png"
                                        ToolTip="Esci" AlternateText="Esci" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="yPosTipo" runat="server" />
    <asp:HiddenField ID="yPosCompetenza" runat="server" />
    <asp:HiddenField ID="yPosCategorie" runat="server" />
    <asp:HiddenField ID="yPosMacrocategorie" runat="server" />
    <script type="text/javascript">

        if (document.getElementById('divLoading') != null) {
        }
        document.getElementById('divLoading').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>
