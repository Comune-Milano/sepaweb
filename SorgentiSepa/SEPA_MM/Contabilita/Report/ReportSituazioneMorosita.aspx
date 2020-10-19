<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportSituazioneMorosita.aspx.vb"
    Inherits="Contabilita_Report_ReportSituazioneMorosita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="../prototype.lite.js"></script>
<script type="text/javascript" src="../moo.fx.js"></script>
<script type="text/javascript" src="../moo.fx.pack.js"></script>
<head runat="server">
    <title>Ricerca Situazione Morosità</title>
    <script type="text/javascript">

        function ScrollPosClasseAppartenenza(obj) {
            document.getElementById('yPosClasseAppartenenza').value = obj.scrollTop;
        }
        
        function ScrollPosTipologiaContrattuale(obj) {
            document.getElementById('yPosCompetenza').value = obj.scrollTop;
        }
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
        .style1
        {
            width: 100%;
            height: 19px;
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
                        <asp:Label ID="LabelRicerca" Text="Ricerca Situazione Morosità" runat="server" Font-Bold="True"
                            Font-Names="Arial" Font-Size="14pt" ForeColor="Maroon" />
                    </td>
                </tr>
                <tr>
                    <td style="font-family: Arial; font-size: 3pt;" class="style1">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1" style="font-family: Arial; font-size: 3pt;">
                        <asp:Panel ID="Panel5" runat="server" Style="border: 1px solid #507CD1" Width="95%">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="background-color: #507CD1; text-align: center; width: 100%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
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
                </tr>
                
                <tr>
                    <td style="width: 100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 23%">
                                    <asp:Label ID="Label64" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                        Text="FIle selezione RU"></asp:Label>
                                </td>
                                <td style="width: 20%">                                    
                                    <asp:FileUpload ID="FileUpload2" runat="server" Font-Names="arial" Font-Size="8pt"  />
                                </td>
                                <td style="width: 40%">
                                    &nbsp;
                                    <asp:Button ID="uploadListaRU" runat="server" Text="Carica Lista RU"/>
                                    </td>
                                <td style="width: 80%">
                                    &nbsp;</td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">                            
                            <tr>
                                <td style="width: 22%">
                                    <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                        Text="Riferimento dal"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="TextBoxRiferimentoDal" runat="server" Font-Names="Arial" 
                                        Font-Size="9pt" Width="70px"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                        ControlToValidate="TextBoxRiferimentoDal" ErrorMessage="!" Font-Bold="True" 
                                        Font-Names="Arial" Font-Size="9pt" 
                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 3%">
                                    <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                        Text="al"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="TextBoxRiferimentoAl" runat="server" Font-Names="Arial" 
                                        Font-Size="9pt" Width="70px"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                                        ControlToValidate="TextBoxRiferimentoAl" ErrorMessage="!" Font-Bold="True" 
                                        Font-Names="Arial" Font-Size="9pt" 
                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 18%">
                                    &nbsp;</td>
                                <td style="width: 10%">
                                    &nbsp;</td>
                                <td style="width: 5%">
                                    &nbsp;</td>
                                <td style="width: 5%; text-align: center;">
                                    &nbsp;</td>
                                <td style="width: 10%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 23%">
                                    <%--<asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Conto corrente"></asp:Label>--%>
                                    <asp:Label ID="Label60" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                        Text="Complesso"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <%--<asp:DropDownList ID="DropDownListContoCorrente" runat="server" Font-Names="Arial"
                                        Font-Size="9pt" Width="90px">
                                        <asp:ListItem Value="0">Tutti</asp:ListItem>
                                        <asp:ListItem Value="1">c/c 59</asp:ListItem>
                                        <asp:ListItem Value="2">c/c 60</asp:ListItem>
                                    </asp:DropDownList>--%>
                                    <asp:DropDownList ID="DropDownListComplesso" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Visible="true" Width="90px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%">
                                    <%--<asp:Label ID="LabelTipologiaIncassoExtramav" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Text="Tipologia Incassi Manuali" Visible="False"></asp:Label>--%>
                                    <asp:Label ID="Label62" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                        Text="Ingiunto"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <%--<asp:DropDownList ID="DropDownListTipoIncassoExtramav" runat="server" Font-Names="Arial"
                                        Font-Size="9pt" Width="90px" Visible="False">
                                    </asp:DropDownList>--%>
                                    <asp:CheckBox ID="chIngiunto" runat="server" />
                                </td>
                                 <td style="width: 15%">
                                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                        Text="Ruolo"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:CheckBox ID="chRuolo" runat="server" />
                                </td>
                                <td style="width: 10%">
                                    &nbsp
                                </td>
                                <td>
                                    <%--<asp:TextBox ID="TextBoxNumeroAssegno" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Width="100%"></asp:TextBox>--%>
                                    <img onclick="javascript:myOpacity.toggle();" alt="" src="../../NuoveImm/Img_Indirizzi.png"
                                        style="cursor: pointer;" />
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
                                    <%--<asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Conto corrente"></asp:Label>--%>
                                    <asp:Label ID="Label61" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                        Text="Edificio"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <%--<asp:DropDownList ID="DropDownListContoCorrente" runat="server" Font-Names="Arial"
                                        Font-Size="9pt" Width="90px">
                                        <asp:ListItem Value="0">Tutti</asp:ListItem>
                                        <asp:ListItem Value="1">c/c 59</asp:ListItem>
                                        <asp:ListItem Value="2">c/c 60</asp:ListItem>
                                    </asp:DropDownList>--%>
                                    <asp:DropDownList ID="DropDownListEdificio" runat="server" Font-Names="Arial" 
                                        Font-Size="9pt" Visible="true" Width="90px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%">
                                    <asp:Label ID="Label59" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                        Text="Condomini" />
                                </td>
                                <td style="width: 48%">
                                    <asp:DropDownList ID="DropDownListCondomini" runat="server" Font-Names="Arial" 
                                        Font-Size="9pt" Width="100%">
                                        <asp:ListItem Value="-1">Nessuna condizione</asp:ListItem>
                                        <asp:ListItem Value="0">Non in condominio</asp:ListItem>
                                        <asp:ListItem Value="3">Tutti i Condomini</asp:ListItem>
                                        <asp:ListItem Value="1">Condomini Gestione Diretta</asp:ListItem>
                                        <asp:ListItem Value="2">Condomini Gestione Indiretta</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            
                            
                            <tr>
                                <td style="width: 22%">
                                    &nbsp;</td>
                                <td style="width: 15%">
                                    
                                    <div id="Indirizzi" style="display: none; border: 1px solid #0000FF; position: absolute;
                                        width: 349px; width: 296px; background-color: #C0C0C0; top: 70px; left: 320px;
                                        height: 431px; overflow: auto; z-index: 200;">
                                        <table style="width: 90%;">
                                            <tr>
                                                <td style="text-align: right">
                                                    <img onclick="javascript:myOpacity.toggle();" alt="" src="../NuoveImm/Img_Conferma.png"
                                                        style="cursor: pointer" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBoxList ID="chIndirizzi" runat="server" Font-Names="arial" Font-Size="8pt">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <img onclick="javascript:myOpacity.toggle();" alt="" src="../NuoveImm/Img_Conferma.png"
                                                        style="cursor: pointer" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                                <td style="width: 48%">
                                    &nbsp;
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
                                                                    ForeColor="White" Width="100%" Height="16px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="height: 100px; overflow: auto">
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
                                    <%--<asp:Panel ID="Panel7" runat="server" Style="border: 1px solid #507CD1" Width="100%">
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
                                                        <asp:Panel ID="PanelCompetenza" runat="server" Style="height: 100px; overflow: auto"
                                                            onscroll="ScrollPosCompetenza(this);">
                                                            <asp:CheckBoxList ID="CheckBoxListCompetenza" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                                AutoPostBack="True">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>--%>
                                    <asp:Panel ID="Panel7" runat="server" Style="border: 1px solid #507CD1" Width="100%">
                                        <table id="table5" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="background-color: #507CD1; text-align: center; width: 100%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="width: 10%">
                                                                <asp:CheckBox ID="CheckBoxTipologiaContrattuale" runat="server" />
                                                            </td>
                                                            <td style="width: 90%">
                                                                <asp:Label ID="Label10" runat="server" Text="Tipologia Contrattuale" Font-Names="Arial"
                                                                    Font-Size="9pt" ForeColor="White" Width="100%"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div>
                                                        <asp:Panel ID="PanelTipologiaContrattuale" runat="server" Style="height: 100px; overflow: auto"
                                                            onscroll="ScrollPosTipologiaContrattuale(this);">
                                                            <asp:CheckBoxList ID="CheckBoxListTipologiaContrattuale" runat="server" Font-Names="Arial"
                                                                Font-Size="9pt" AutoPostBack="True">
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
                                                                <asp:CheckBox ID="CheckBoxMacrocategorie" runat="server" AutoPostBack ="true" />
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
                                                        <asp:Panel ID="PanelMacrocategorie" runat="server" Style="height: 100px; overflow: auto"
                                                            onscroll="ScrollPosMacrocategorie(this);">
                                                            <asp:CheckBoxList ID="CheckBoxListMacrocategorie" runat="server" Font-Names="Arial"
                                                                Font-Size="9pt" >
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width: 67%">
                                <asp:Panel ID="Panel1" runat="server" Style="border: 1px solid #507CD1" Width="100%">
                                        <table id="table2" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="background-color: #507CD1; text-align: center; width: 100%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="width: 10%">
                                                                <asp:CheckBox ID="CheckBoxClasseAppartenenza" runat="server" />
                                                            </td>
                                                            <td style="width: 90%">
                                                                <asp:Label ID="Label2" runat="server" Text="Classe Appartenenza" Font-Names="Arial"
                                                                    Font-Size="9pt" ForeColor="White" Width="100%"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div>
                                                        <asp:Panel ID="Panel2" runat="server" Style="height: 100px; overflow: auto"
                                                            onscroll="ScrollPosClasseAppartenenza(this);">
                                                            <asp:CheckBoxList ID="CheckBoxListClasseAppartenenza" runat="server" Font-Names="Arial"
                                                                Font-Size="9pt" >
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
                        <asp:Label ID="Label11" Text="Ricerca Situazione Morosità" runat="server" Font-Bold="True"
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
                                                    <div style="height: 120px; overflow: auto">
                                                        <asp:CheckBoxList ID="CheckBoxListVoci" runat="server" Font-Names="Arial" Font-Size="9pt">
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
                                                                <asp:Label ID="Label19" runat="server" Text="Capitoli" Font-Names="Arial" Font-Size="9pt"
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
                        <%--<asp:Label ID="Label1" Text="Ordinamento " runat="server" Font-Names="Arial" Font-Size="9pt" />
                        <asp:DropDownList runat="server" ID="Ordinamento" Font-Names="Arial" Font-Size="9pt">
                            <asp:ListItem Text="per Bollettazione" Value="1" />
                            <asp:ListItem Text="per Capitolo" Value="2" />
                        </asp:DropDownList>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="height: 4px;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: right">
                        <table cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td style="text-align: right" width="60%">
                                    <asp:ImageButton ID="ImageButtonIndietro" runat="server" ImageUrl="../../NuoveImm/Img_Indietro2.png"
                                        ToolTip="Avvia Ricerca" AlternateText="Avvia Ricerca" />
                                </td>
                                <td style="text-align: right" width="15%">
                                    <%--<asp:ImageButton ID="ImageButtonAvviaRicerca" runat="server" AlternateText="Avvia Ricerca Riepilogo"
                                        ImageUrl="../../NuoveImm/Img_Riepilogo.png" ToolTip="Avvia Ricerca Riepilogo" />--%>
                                </td>
                                <td style="text-align: right" width="15%">
                                    <asp:ImageButton ID="ImageButtonAvviaDettaglio" runat="server" AlternateText="Avvia Ricerca Dettaglio"
                                        ImageUrl="../../NuoveImm/Img_Dettaglio.png" 
                                        ToolTip="Avvia Ricerca Dettaglio" style="height: 20px" />
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
    <asp:HiddenField ID="yPosTipologiaContrattuale" runat="server" />
    <asp:HiddenField ID="yPosClasseAppartenenza" runat="server" />    
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
<script type="text/javascript">
    myOpacity = new fx.Opacity('Indirizzi', { duration: 200 });    
    myOpacity.hide();
    //document.getElementById('Indirizzi').style.visibility='hidden';
</script>
</html>
