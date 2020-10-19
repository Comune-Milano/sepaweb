<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaCondominio.aspx.vb"
    Inherits="Condomini_RicercaCondominio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ricerca</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: 16pt;
            color: #660000;
        }
        #form1
        {
            width: 783px;
        }
        .style2
        {
            font-family: Arial;
            font-size: 8pt;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed;
                top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee;
                z-index: 500">
                <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
                    margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
                    <table style="width: 100%; height: 100%">
                        <tr>
                            <td valign="middle" align="center">
                                <asp:Image ID="Image2" runat="server" ImageUrl="Immagini/load.gif" />
                                <br />
                                <br />
                                <asp:Label ID="lblcarica" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                                    Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="style1">
                    <strong>Ricerca Condomini</strong>
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 6pt">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #CCCCCC;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style2" width="10%">
                                        Complesso
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbComplesso" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="90%" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2" width="10%">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2" width="12%">
                                        Edificio
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbEdificio" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="90%" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #CCCCCC;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style2" width="12%">
                                        Indirizzo
                                    </td>
                                    <td width="60%">
                                        <asp:DropDownList ID="cmbIndirizzo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="98%" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style2" width="8%">
                                        Civico
                                    </td>
                                    <td width="20%">
                                        <asp:DropDownList ID="cmbCivico" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="98%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cmbComplesso" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cmbEdificio" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cmbIndirizzo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #CCCCCC;">
                    <table style="width: 100%;">
                        <tr>
                            <td class="style2" width="12%">
                                Amministratore
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbAmministratore" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="98%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td style="text-align: right">
                                <asp:ImageButton ID="btnAvviaRicerca" runat="server" ImageUrl="Immagini/Img_AvviaRicerca.png" />
                            </td>
                            <td style="text-align: right; cursor:pointer;">
                                <img alt="" src="Immagini/Img_Home.png" onclick="document.location.href='pagina_home.aspx';"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
