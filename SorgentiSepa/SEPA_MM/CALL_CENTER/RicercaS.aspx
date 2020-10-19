<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaS.aspx.vb" Inherits="CALL_CENTER_RicercaS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ricerca Segnalazioni</title>
    <script type="text/javascript">
        //document.onkeydown = $onkeydown;
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
    </script>
    <style type="text/css">
        body
        {
            font-size: 8pt;
            font-family: Arial;
            color: Black;
        }
    </style>
    <link rel="stylesheet" href="../AUTOCOMPLETE/cmbstyle/chosen.css" />
</head>
<body style="background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); width: 770px;">
    <form id="form1" runat="server" defaultbutton="btnCerca" style="width: 100%">
    <div id="caricamento" style="margin: 0px; background-color: #C0C0C0; width: 100%;
        height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75');
        opacity: 0.75; background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('../NuoveImm/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <img alt="Caricamento" src="../NuoveImm/load.gif" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center">
                        <span style="font-family: Arial; font-size: 8pt">Caricamento</span> . . .
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td colspan="2">
                <asp:Label ID="Label2" Text="Ricerca Segnalazioni" runat="server" Font-Bold="True"
                    Font-Names="Arial" Font-Size="14pt" ForeColor="Maroon" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                <asp:Label ID="Label0" Text="Sede Territoriale" runat="server" Font-Names="Arial"
                    Font-Size="8pt" />
            </td>
            <td style="width: 85%">
                <asp:DropDownList ID="cmbSedeTerritoriale" runat="server" Width="250px" Font-Names="Arial"
                    Font-Size="8pt" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" Text="Complesso" runat="server" Font-Names="Arial" Font-Size="8pt" />
            </td>
            <td>
                <asp:DropDownList ID="cmbComplesso" runat="server" Font-Names="Arial" Font-Size="8pt"
                    CssClass="chzn-select" Width="450px" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label22" Text="Edificio" runat="server" Font-Names="Arial" Font-Size="8pt" />
            </td>
            <td>
                <asp:DropDownList ID="cmbEdificio" runat="server" Font-Names="Arial" Font-Size="8pt"
                    CssClass="chzn-select" Width="450px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" Text="Segnalante" runat="server" Font-Names="Arial" Font-Size="8pt" />
            </td>
            <td>
                <asp:TextBox ID="txtSegnalante" runat="server" MaxLength="100" Font-Names="Arial"
                    Font-Size="8pt" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" Text="Tipo Intervento" runat="server" Font-Names="Arial"
                    Font-Size="8pt" />
            </td>
            <td>
                <asp:DropDownList ID="cmbTipo" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label16" Text="Stato Segnalazione" runat="server" Font-Names="Arial"
                    Font-Size="8pt" />
            </td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <div style="overflow: auto; width: 247px; height: 170px; border: 1px gray solid;
                                background-color: White">
                                <asp:CheckBoxList ID="CheckBoxListStato" runat="server">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                        <td>
                            <div style="overflow: auto; width: 170px; height: 170px; border: 1px gray solid;
                                background-color: White;">
                                <br />
                                <asp:Label Text="Ordina risultati per" runat="server" Font-Bold="True" 
                                    Font-Names="Arial" Font-Size="8pt" style="text-align:center" Width="100%" />
                                <br /><br />
                                <asp:RadioButtonList ID="RadioButtonListOrdine" runat="server" RepeatDirection="Vertical">
                                    <asp:ListItem Value="0" Text="Stato Segnalazione" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Urgenza"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Tipo Segnalazione"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label17" Text="Tipo Segnalazione" runat="server" Font-Names="Arial"
                    Font-Size="8pt" />
            </td>
            <td>
                <asp:DropDownList ID="cmbTipoSegnalazione" runat="server" AutoPostBack="True" Font-Names="Arial"
                    Font-Size="8pt" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label18" Text="Urgenza" runat="server" Font-Names="Arial" Font-Size="8pt" />
            </td>
            <td>
                <asp:DropDownList ID="DropDownListUrgenza" runat="server" BackColor="White" Font-Names="Arial"
                    Font-Size="8pt" TabIndex="7" Width="90px" Enabled="False">
                    <asp:ListItem style="background-color: White; color: Black;" Value="---">Tutti</asp:ListItem>
                    <asp:ListItem style="background-color: White; color: White;" Value="Bianco">&nbsp;</asp:ListItem>
                    <asp:ListItem style="background-color: Green; color: Green;" Value="Verde"></asp:ListItem>
                    <asp:ListItem style="background-color: Yellow; color: Yellow;" Value="Giallo"></asp:ListItem>
                    <asp:ListItem style="background-color: Red; color: Red;" Value="Rosso"></asp:ListItem>
                    <asp:ListItem style="background-color: Blue; color: Blue;" Value="Blu"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label19" Text="Numero" runat="server" Font-Names="Arial" Font-Size="8pt" />
            </td>
            <td>
                <asp:TextBox ID="TextBoxNumero" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label20" Text="Dal" runat="server" Font-Names="Arial" Font-Size="8pt" />
            </td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDal" runat="server" ToolTip="gg/mm/aaaa" Width="68px" MaxLength="10"
                                Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDal"
                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="TextBoxOreDal" runat="server" ToolTip="ore" Width="25px" Font-Names="Arial"
                                Font-Size="8pt"></asp:TextBox>
                            <asp:Label ID="Label6" runat="server" Width="10px" TabIndex="-1" Font-Names="Arial"
                                Style="text-align: center" Font-Size="8pt">:</asp:Label>
                            <asp:TextBox ID="TextBoxMinutiDal" runat="server" ToolTip="minuti" Width="25px" Font-Names="Arial"
                                Font-Size="8pt"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label21" Text="Al" runat="server" Font-Names="Arial" Font-Size="8pt" />
            </td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtAl" runat="server" ToolTip="gg/mm/aaaa" Width="68px" Font-Names="Arial"
                                Font-Size="8pt"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtAl"
                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="TextBoxOreAl" runat="server" ToolTip="ore" Width="25px" Font-Names="Arial"
                                Font-Size="8pt"></asp:TextBox>
                            <asp:Label ID="Label8" runat="server" Width="10px" TabIndex="-1" Font-Names="Arial"
                                Style="text-align: center" Font-Size="8pt">:</asp:Label>
                            <asp:TextBox ID="TextBoxMinutiAl" runat="server" ToolTip="minuti" Width="25px" Font-Names="Arial"
                                Font-Size="8pt"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: right">
                <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                    ToolTip="Avvia Ricerca" />
                <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                    ToolTip="Home" />
            </td>
        </tr>
    </table>
    <script src="../AUTOCOMPLETE/cmbscript/jquery.min.js" type="text/javascript"></script>
    <script src="../AUTOCOMPLETE/cmbscript/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(".chzn-select").chosen({
            disable_search_threshold: 10,
            no_results_text: "Nessun risultato trovato!",
            placeholder_text_single: "- - -",
            width: "95%"
        });
        document.getElementById('caricamento').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>
