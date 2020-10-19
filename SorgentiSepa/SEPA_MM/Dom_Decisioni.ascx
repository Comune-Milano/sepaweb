<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Decisioni.ascx.vb"
    Inherits="Dom_Decisioni" %>
<style type="text/css">
    .style1
    {
        font-family: Arial;
        font-weight: bold;
        font-size: 10pt;
    }
    .style2
    {
        font-family: Arial;
        font-size: 8pt;
        text-decoration: underline;
    }
    
    .style4
    {
        font-family: Arial;
        font-size: 8pt;
        width: 50px;
    }
    #riesame
    {
        width: 626px;
    }
    .bottone
    {
        background-color: #990000;
        font-weight: bold;
        color: white;
        width: 350px;
        font-family: Arial;
        font-size: 10pt;
    }
</style>
<asp:HiddenField ID="esPositivo" runat="server" Value="0" />
<asp:HiddenField ID="esPosiRiesame" runat="server" Value="0" />
<asp:HiddenField ID="esNegativo1" runat="server" Value="0" />
<asp:HiddenField ID="esNegaRiesame" runat="server" Value="0" />
<asp:HiddenField ID="autorizzFinale" runat="server" Value="0" />
<div id="decisioni" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    left: 10px; border-left: lightsteelblue 1px solid; width: 641px; border-bottom: lightsteelblue 1px solid;
    position: absolute; top: 107px; height: 390px; background-color: #ffffff; z-index: 197;">
    <div id="divSospesa" style="left: -1px; width: 641px; position: absolute; top: 0px;
        height: 383px; background-color: #ffffff; z-index: 197;">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblMsgSosp" runat="server" Text="Impossibile applicare delle decisioni per questa domanda!<br/><br/>Il procedimento risulta SOSPESO per documentazione incompleta."
                        Font-Bold="True" Font-Names="arial" Font-Size="10pt" ForeColor="#990000" Width="500px"
                        Style="left: 105px; position: absolute; top: 143px;"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <table style="width: 100%;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <tr>
            <td class="style1" style="border-bottom-style: dotted; border-bottom-width: thin;
                border-bottom-color: #C0C0C0">
                <asp:Label ID="lblTitolo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:CheckBox ID="ChkDecidi" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                Font-Underline="True" Text="SOTTOPONI A DECISIONE" Width="165px" />
                        </td>
                        <td class="style4">
                            DATA
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataDecisione" runat="server" Font-Names="Arial" Font-Size="8pt"
                                ReadOnly="True" Width="80px" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbListDecis0" runat="server" Font-Names="Arial" Font-Size="8pt"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="7">Accolta</asp:ListItem>
                                <asp:ListItem Value="8">NON Accolta</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <div style="overflow: auto;" id="divNoteDecis0">
                                <asp:TextBox ID="txtNoteDec0" runat="server" TextMode="MultiLine" Font-Names="arial"
                                    Font-Size="8pt" Width="150px"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <div id="decisione">
                    <table style="border: thin solid #0066CC; width: 100%;">
                        <tr>
                            <td class="style4">
                                DATA
                            </td>
                            <td style="text-align: left; vertical-align: top">
                                <asp:TextBox ID="txtDataScelta" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="80px" Enabled="False" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td rowspan="2" style="text-align: left; vertical-align: top">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div style="overflow: auto; width: 230px; height: 90px;" id="divCondNonAccolta">
                                            <asp:CheckBoxList ID="ChkMotivi" runat="server" Enabled="False" Font-Names="Arial"
                                                Font-Size="8pt" RepeatLayout="Flow" CellPadding="2" CellSpacing="2" Font-Overline="False"
                                                AutoPostBack="True">
                                            </asp:CheckBoxList>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top">
                                <asp:RadioButtonList ID="rdbListDecisione" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Height="54px" Enabled="False" Width="100px">
                                    <asp:ListItem Value="2">Accolta</asp:ListItem>
                                    <asp:ListItem Value="3">NON Accolta</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align: left; vertical-align: top">
                                <span class="style2">Note<br />
                                </span>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtNoteDecisione" runat="server" Width="250px" Height="46px" TextMode="MultiLine"
                                            ReadOnly="True" Font-Names="arial" Font-Size="8pt"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkRiesame" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" Font-Underline="True" Text="SOTTOPONI A RIESAME" Enabled="False"
                                Width="165px" />
                        </td>
                        <td class="style4">
                            DATA
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataRiesame" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="80px" Enabled="False" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbListRies0" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Enabled="False" RepeatDirection="Horizontal">
                                <asp:ListItem Value="9">Accolta</asp:ListItem>
                                <asp:ListItem Value="10">NON Accolta</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <div style="overflow: auto;" id="divNoteRies0">
                                <asp:TextBox ID="txtNoteRies0" runat="server" TextMode="MultiLine" Font-Names="arial"
                                    Font-Size="8pt" Width="150px"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <div id="riesame">
                    <table style="border: thin solid #0066CC; width: 100%">
                        <tr>
                            <td class="style4">
                                DATA
                            </td>
                            <td style="text-align: left; vertical-align: top">
                                <asp:TextBox ID="txtDatasceltaR" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="80px" Enabled="False" ReadOnly="True"></asp:TextBox>&nbsp&nbsp&nbsp
                                <asp:CheckBox ID="chkOsserv" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Con Osservazioni"
                                    Enabled="False" Width="150px" />
                            </td>
                            <td rowspan="2" style="text-align: left; vertical-align: top">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div style="overflow: auto; width: 260px; height: 110px; z-index: 50px;" id="divCondNonAccolta2">
                                            <asp:CheckBoxList ID="ChkMotiviRies" runat="server" Enabled="False" Font-Names="Arial"
                                                Font-Size="8pt" RepeatLayout="Flow" CellPadding="2" CellSpacing="2" Font-Overline="False"
                                                AutoPostBack="True">
                                            </asp:CheckBoxList>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top">
                                <asp:RadioButtonList ID="rdbListRiesame" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Height="54px" Enabled="False" Width="100px">
                                    <asp:ListItem Value="5">Accolta</asp:ListItem>
                                    <asp:ListItem Value="6">NON Accolta</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align: left; vertical-align: top">
                                <div style="padding-left: 105px;" id="divOsservazioni">
                                    <asp:TextBox ID="txtDataOsserv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="68px" Enabled="False" ToolTip="Inserire la data di present. delle osservazioni"></asp:TextBox>
                                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Data Osserv."></asp:Label>
                                </div>
                                <span class="style2">Note</span>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtNoteRiesame" runat="server" Width="250px" Height="46px" TextMode="MultiLine"
                                            ReadOnly="True" Font-Names="arial" Font-Size="8pt"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <div id="autorizzazione">
                    <table style="width: 100%">
                        <tr>
                            <td style="font-family: Arial; font-size: 11px; width: 100px">
                                DATA
                            </td>
                            <td style="text-align: left; vertical-align: top">
                                <asp:TextBox ID="txtdataAUTORIZ" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="80px" Enabled="False" ToolTip="Inserire la data di autorizzazione"></asp:TextBox>
                            </td>
                            <td rowspan="2" style="text-align: center; vertical-align: top">
                                <input id="buttonAutorAbus" type="button" onclick="document.getElementById('H1').value='0';InserisciCanone();"
                                    class="bottone" value="AUTORIZZA ABUS.SMO AMM.VO ART.15 C.2 RR 1/2004" style="display: none;" />
                                <asp:Button ID="btnRiduzCanone" runat="server" BackColor="#990000" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="10pt" ForeColor="White" Enabled="False" Width="350px" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblCredito" runat="server" Font-Names="Arial" Font-Size="11pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100px">
                            &nbsp
                        </td>
                        <td style="text-align: left; vertical-align: top;" width="99px">
                            &nbsp
                        </td>
                        <td rowspan="2" style="text-align: center; vertical-align: top">
                            &nbsp;<asp:Button ID="btnContabilizza" runat="server" BackColor="#990000" Font-Bold="True"
                                Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="350px" OnClientClick="document.getElementById('H1').value='0';ConfContabilizza();"
                                Text="CONTABILIZZA" Visible="False" />
                            <input id="btnDeposito" type="button" onclick="document.getElementById('H1').value='0';RimborsoDeposito();"
                                class="bottone" value="AUTORIZZA CAMBIO CONTRATTUALE" style="display: none;" />
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    // Funzione javascript per l'inserimento in automatico degli slash nella data
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
