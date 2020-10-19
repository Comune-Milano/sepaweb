<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChiusuraSegnalazioni.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_ChiusuraSegnalazioni" %>

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
        .style1
        {
            height: 20px;
        }
    </style>
    <link rel="stylesheet" href="../../../AUTOCOMPLETE/cmbstyle/chosen.css" />
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnCerca" style="width: 100%" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <table border="0" cellpadding="2" cellspacing="2" width="100%" class="FontTelerik">
        <tr>
            <td colspan="4" class="TitoloModulo">
                Ricerca Segnalazioni per chiusura massiva
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <telerik:RadButton ID="btnCerca" runat="server" Text="Avvia Ricerca" ToolTip="Avvia Ricerca" />
                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Home" />
                &nbsp; &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" class="style1">
                &nbsp;
            </td>
            <td class="style1">
                &nbsp;
            </td>
            <td class="style1">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="Label0" Text="Sede Territoriale" runat="server" />
            </td>
            <td style="width: 35%">
                <telerik:RadComboBox ID="cmbSedeTerritoriale" Width="100%" AppendDataBoundItems="true"
                    Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                </telerik:RadComboBox>
            </td>
            <td style="width: 10%" rowspan="6">
                <asp:Label ID="Label16" Text="&amp;nbsp;Stato Segnalazione" runat="server" />
            </td>
            <td style="width: 35%" rowspan="6">
                <div style="overflow: auto; height: 130px; border: 1px gray solid; background-color: White">
                    <asp:CheckBoxList ID="CheckBoxListStato" runat="server">
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="Label23" Text="B. Manager" runat="server" />
            </td>
            <td>
                <telerik:RadComboBox ID="cmbBManager" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                    LoadingMessage="Caricamento...">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="Label1" Text="Complesso" runat="server" />
            </td>
            <td>
                <telerik:RadComboBox ID="cmbComplesso" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                    LoadingMessage="Caricamento...">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="Label22" Text="Edificio" runat="server" />
            </td>
            <td>
                <telerik:RadComboBox ID="cmbEdificio" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                    LoadingMessage="Caricamento...">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="Label9" Text="Segnalante" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="txtSegnalante" runat="server" MaxLength="100" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="Label3" Text="Fornitore" runat="server" />
            </td>
            <td>
                <telerik:RadComboBox ID="DropDownListFornitore" Width="100%" AppendDataBoundItems="true"
                    Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="Label17" Text="Categoria Segnalazione" runat="server" />
            </td>
            <td>
                <div style="overflow: auto; height: 170px; border: 1px gray solid; background-color: White">
                    <asp:CheckBoxList ID="CheckBoxListTipoSegnalazione" runat="server" AutoPostBack="True">
                    </asp:CheckBoxList>
                </div>
            </td>
            <td colspan="2" style="vertical-align: top">
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="width: 5%">
                            <asp:Label ID="Label4" Text="Categoria 1" runat="server" />
                        </td>
                        <td style="width: 30%">
                            <telerik:RadComboBox ID="cmbTipoSegnalazioneLivello1" Width="100%" AppendDataBoundItems="true"
                                Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                            <asp:Label ID="Label5" Text="Categoria 2" runat="server" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbTipoSegnalazioneLivello2" Width="100%" AppendDataBoundItems="true"
                                Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                            <asp:Label ID="Label7" Text="Categoria 3" runat="server" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbTipoSegnalazioneLivello3" Width="100%" AppendDataBoundItems="true"
                                Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                            <asp:Label ID="Label10" Text="Categoria 4" runat="server" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbTipoSegnalazioneLivello4" Width="250px" AppendDataBoundItems="true"
                                Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="Label18" Text="Urgenza" runat="server" />
            </td>
            <td>
                <telerik:RadComboBox ID="DropDownListUrgenza" Width="90px" AppendDataBoundItems="true"
                    Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                    <Items>
                        <telerik:RadComboBoxItem Style="background-color: White; color: Black;" Value="---"
                            Text="Tutti" Selected="true" />
                        <telerik:RadComboBoxItem Style="background-color: blue; color: blue;" Value="Blu" />
                        <telerik:RadComboBoxItem Style="background-color: White; color: white;" Value="Bianco" />
                        <telerik:RadComboBoxItem Style="background-color: green; color: green;" Value="Verde" />
                        <telerik:RadComboBoxItem Style="background-color: Yellow; color: yellow;" Value="Giallo" />
                        <telerik:RadComboBoxItem Style="background-color: Red; color: Red;" Value="Rosso" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="Label19" Text="Numero" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="TextBoxNumero" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="Label20" Text="Dal" runat="server" />
            </td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDal" runat="server" ToolTip="gg/mm/aaaa" Width="68px" MaxLength="10"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDal"
                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="TextBoxOreDal" runat="server" ToolTip="ore" Width="25px"></asp:TextBox>
                            <asp:Label ID="Label6" runat="server" Width="10px" TabIndex="-1" Style="text-align: center">:</asp:Label>
                            <asp:TextBox ID="TextBoxMinutiDal" runat="server" ToolTip="minuti" Width="25px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="Label21" Text="Al" runat="server" />
            </td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtAl" runat="server" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtAl"
                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="TextBoxOreAl" runat="server" ToolTip="ore" Width="25px"></asp:TextBox>
                            <asp:Label ID="Label8" runat="server" Width="10px" TabIndex="-1" Style="text-align: center">:</asp:Label>
                            <asp:TextBox ID="TextBoxMinutiAl" runat="server" ToolTip="minuti" Width="25px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="vertical-align: middle">
                            <img src="../../../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>Utilizzare <b>*</b> come carattere jolly nelle ricerche</i></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" Value="-1" ID="idStruttura" />
    <asp:HiddenField runat="server" Value="" ID="strutturaNome" />
    <asp:HiddenField runat="server" Value="-1" ID="idBM" />
    <script src="../../../AUTOCOMPLETE/cmbscript/jquery.min.js" type="text/javascript"></script>
    <script src="../../../AUTOCOMPLETE/cmbscript/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(".chzn-select").chosen({
            disable_search_threshold: 10,
            no_results_text: "Nessun risultato trovato!",
            placeholder_text_single: "- - -",
            width: "95%"
        });
        // document.getElementById('caricamento').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>
