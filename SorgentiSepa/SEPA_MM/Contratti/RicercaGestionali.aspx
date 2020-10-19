<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaGestionali.aspx.vb"
    Inherits="Contratti_RicercaGestionali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ricerca Partite Gestionali</title>
    <style type="text/css">
        .bordiCombo
        {
            border: 1px solid #000000;
        }
    </style>
</head>
<body style="background-image: url('../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat;
    background-attachment: fixed">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Ricerca Partite
                    Gestionali </span></strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblDoc" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Tipo Documento</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbTipoDoc" runat="server" Width="360px" TabIndex="1" Style="border: 1px solid black;"
                                AutoPostBack="True" Font-Names="arial" Font-Size="9pt" CssClass="bordiCombo">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblDataEmiss" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Data Emissione dal</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataEmissDAL" runat="server" Width="80px" TabIndex="2" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataEmissDAL"
                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                ForeColor="#CC3300"></asp:RegularExpressionValidator>
                            <asp:Label ID="Label17" runat="server" Text="Al" Font-Names="Arial" Font-Size="8pt"
                                Width="50px"></asp:Label>
                            <asp:TextBox ID="txtDataEmissAL" runat="server" Width="80px" TabIndex="3" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataEmissAL"
                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblDataRiferim" runat="server" Font-Size="8pt" Font-Names="Arial"
                                Font-Bold="False" TabIndex="-1">Data Riferimento dal</asp:Label>
                        </td>
                        <td style="vertical-align: top;">
                            <asp:TextBox ID="txtDataRiferDAL" runat="server" Width="80px" TabIndex="4" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataRiferDAL"
                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:Label ID="Label2" runat="server" Text="Al" Font-Names="Arial" Font-Size="8pt"
                                Width="50px"></asp:Label>
                            <asp:TextBox ID="txtDataRiferAL" runat="server" Width="80px" TabIndex="5" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataRiferAL"
                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblTipoUI" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Tipologia Unità</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbTipoUI" runat="server" Width="360px" TabIndex="6" Style="border: 1px solid black;"
                                AutoPostBack="True" Font-Names="arial" Font-Size="9pt" CssClass="bordiCombo">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblTipoContr" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Tipologia Contratto</asp:Label>
                        </td>
                        <td style="height:15px;">
                            <asp:DropDownList ID="cmbTipoContr" runat="server" Width="360px" TabIndex="7" Style="border: 1px solid black;"
                                AutoPostBack="True" Font-Names="arial" Font-Size="9pt" CssClass="bordiCombo">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblSpecifico" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                        TabIndex="-1" Visible="False">Tipo Contr.Specifico</asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cmbProvenASS" runat="server" Width="360px" TabIndex="8" Style="border: 1px solid black;"
                                        AutoPostBack="True" Font-Names="arial" Font-Size="9pt" Visible="False" CssClass="bordiCombo">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Cod. Contratto</asp:Label>
                        </td>
                        <td style="height:45px;vertical-align: middle;">
                            <asp:TextBox ID="txtCodContr" runat="server" Width="354px" TabIndex="9"></asp:TextBox>
                        </td>
                        <td rowspan="2">
                            <img id="imgFumetto" alt="Ricerca approssimata" style="display: none;"
                                src="../ImmMaschere/alert2_ricercad.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Note</asp:Label>
                        </td>
                        <td style="height:45px;vertical-align: middle;">
                            <asp:TextBox ID="txtNote" runat="server" Width="354px" TabIndex="10"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Credito/Debito</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbCredDeb" runat="server" Width="360px" TabIndex="11" Style="border: 1px solid black;"
                                Font-Names="arial" Font-Size="9pt" CssClass="bordiCombo">
                                <asp:ListItem Value="-1">TUTTI</asp:ListItem>
                                <asp:ListItem Value="1">Solo Credito</asp:ListItem>
                                <asp:ListItem Value="0">Solo Debito</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Solo da Elaborare</asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkElaborare" runat="server" Checked="True" TabIndex="12" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Ordina Risultati</asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbOrderBY" runat="server" Font-Names="Arial" Font-Size="8pt"
                                CellPadding="0" CellSpacing="0" TabIndex="13">
                                <asp:ListItem Selected="True" Value="INTEST">Per Intestatario</asp:ListItem>
                                <asp:ListItem Value="INDIR">Per Indirizzo</asp:ListItem>
                                <asp:ListItem Value="CODUI">Per Cod. Unità</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="../NuoveImm/Img_AvviaRicerca.png"
                                ToolTip="Avvia Ricerca" TabIndex="14" OnClientClick="ControllaCampiObb()" />
                        </td>
                        <td>
                            <img onclick="document.location.href='pagina_home.aspx';" alt="Home" src="../NuoveImm/Img_Home.png"
                                style="cursor: pointer;" title="Torna alla pagine Home" />
                        </td>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="DivVisible" runat="server" />
    <asp:HiddenField ID="errore" runat="server" Value="0" />
    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
        ForeColor="Red" Style="z-index: 10; left: 17px; position: absolute; top: 451px;
        height: 13px; width: 442px;" Text="Label" Visible="False"></asp:Label>
    </form>
    <script type="text/javascript">

        function ControllaCampiObb() {
            var tipoDoc;
            var tipoContr;
            var data1Emiss;
            var data2Emiss;
            var data1Rifer;
            var data2Rifer;
            var errore;

            errore = '0';
            document.getElementById('errore').value = '0';
            tipoDoc = document.getElementById('cmbTipoDoc').value
            tipoContr = document.getElementById('cmbTipoContr').value
            data1Emiss = document.getElementById('txtDataEmissDAL').value
            data2Emiss = document.getElementById('txtDataEmissAL').value
            data1Rifer = document.getElementById('txtDataRiferDAL').value
            data2Rifer = document.getElementById('txtDataRiferAL').value

            if (document.getElementById('txtCodContr').value == '' && document.getElementById('txtNote').value == '') {
                if (tipoDoc == '-1') {
                    alert('Inserire la tipologia di documento!');
                    errore = '1';
                }
                else if (data1Emiss == '' && data2Emiss == '' && data1Rifer == '' && data2Rifer == '') {
                    alert('Inserire le date!');
                    errore = '1';
                }
                else if (tipoContr == '-1') {
                    alert('Inserire la tipologia di contratto!');
                    errore = '1';
                }
                else if (data1Emiss != '' && data2Emiss == '') {
                    alert('Inserire le date!');
                    errore = '1';
                }
                else if (data1Emiss == '' && data2Emiss != '') {
                    alert('Inserire le date!');
                    errore = '1';
                }
                else if (data1Rifer == '' && data2Rifer != '') {
                    alert('Inserire le date!');
                    errore = '1';
                }
                else if (data1Rifer != '' && data2Rifer == '') {
                    alert('Inserire le date!');
                    errore = '1';
                }

                document.getElementById('errore').value = errore;
            }
        }


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

        function RendiVisibile() {
            document.getElementById('imgFumetto').style.display = 'block';
        }
        function RendiNonVisibile() {
            document.getElementById('imgFumetto').style.display = 'none';
        }
    </script>
</body>
</html>
