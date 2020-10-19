<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AggiungiDocAlleg.aspx.vb"
    Inherits="VSA_Locatari_AggiungiDocAlleg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Aggiungi Doc.Allegati</title>
</head>
<body>

    <form id="form1" runat="server">
    <script type="text/javascript">

        function ConfermaSimili(testo) {

            //SceltaFunzioneOP2(testo);
            var chiediConferma;
            chiediConferma =  window.confirm(testo);
            if (chiediConferma == true) { __doPostBack('<%=btnfunzSimili.ClientID %>', '');alert("Operazione effettuata!");document.getElementById("txtDescrizione").value = ""; }
        }

    </script>
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="conferma" runat="server"/>
    <div id="InserimentoLegali" style="left: -105px; width: 722px; background-repeat: no-repeat;
        background-image: url('../../ImmDiv/SfondoDiv.png'); z-index: 500; position: absolute;
        top: -91px; height: 457px;">
        <span style="font-family: Arial"></span>
        <br />
        <br />
        <table border="0" cellpadding="1" cellspacing="1" style="left: 117px; width: 435px;
            position: absolute; top: 138px; background-color: #FFFFFF; z-index: 200; height: 208px;">
            <tr>
                <td style="width: 52px; height: 19px; text-align: left">
                    &nbsp;
                </td>
                <td style="width: 274px; height: 19px; text-align: left; vertical-align: top;">
                    <strong><span style="font-family: Arial">Nuovo Documento Allegato</span></strong>
                    <br />
                    <br />
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Arial">
                <td style="width: 80px; height: 19px; text-align: left; vertical-align: top;">
                    <span style="font-size: 10pt; font-family: Arial">Descrizione:</span>
                </td>
                <td style="width: 274px; height: 19px; text-align: left; vertical-align: top; ">
                    <asp:TextBox ID="txtDescrizione" runat="server" Font-Names="Arial" Font-Size="10pt"
                        Width="293px" MaxLength="400" TabIndex="1" Height="53px" TextMode="MultiLine"
                        Font-Bold="True" style="text-transform:uppercase;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
                <td style="text-align: left; vertical-align: top;">
                    <%-- <div style="overflow: auto;">
                        <asp:Label ID="lblElencoDoc" runat="server" Font-Bold="True" Font-Names="Times New Roman"
                            Font-Size="8pt" Visible="False"></asp:Label>
                    </div>--%>
                    <div style="overflow: auto; width: 293px; height: 75px;">
                        <asp:CheckBoxList ID="chkElencoDoc" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" RepeatLayout="Flow" CellPadding="2" CellSpacing="2" Font-Overline="False"
                            Visible="False">
                        </asp:CheckBoxList>
                    </div>
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
                <td style="width: 52px; height: 19px">
                </td>
                <td align="right" style="width: 274px; height: 19px; text-align: right">
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 110%">
                        <tr>
                            <td style="text-align: right">
                                <asp:ImageButton ID="img_InserisciSchema" runat="server" ImageUrl="~/NuoveImm/Img_InserisciVal.png"
                                    TabIndex="2" />&nbsp;<asp:ImageButton ID="img_ChiudiSchema" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                        OnClientClick="javascript:window.close();AggDomanda();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
        <div id="ScriptScelta" title="Finestra di Conferma" style="display: none; font-size: 9pt; font-family: Arial">
    </div>
    <asp:Button ID="btnfunzSimili" runat="server" Text="" Style="display: none;" CauseValidation="false" />
    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnfunzSimili" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    </form>

    <script type="text/javascript">

        window.onbeforeunload = function () {
            AggDomanda();
        }

        function AggDomanda() {
            if (typeof opener != 'undefined') {
                if (opener.name.substring(0, 7) == 'Domande') {
                    opener.document.getElementById('btnVisualizzaDich').click();
                }
                if (opener.name.substring(0, 13) == 'Dichiarazione') {
                    opener.document.getElementById('btnVisualizzaDich').click();
                }
            }
        }

    </script>
</body>

</html>