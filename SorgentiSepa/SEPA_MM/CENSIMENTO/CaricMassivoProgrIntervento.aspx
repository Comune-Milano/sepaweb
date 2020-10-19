<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CaricMassivoProgrIntervento.aspx.vb" Inherits="CENSIMENTO_CaricMassivoProgrIntervento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <title>Caricamento massivo voci</title>
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Standard/Style/css/smoothness/jquery-ui-1.10.4.custom.css" rel="stylesheet"
        type="text/css" />
    <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/gestioneDimensioniPagina.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jsMessage.js" type="text/javascript"></script>
    <style type="text/css">
        .bottone2
        {
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            height: 19px;
            cursor: pointer;
            padding-left: 3px;
        }
        
        .bottone2:hover
        {
            background-color: #FFF5D3;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            height: 19px;
            cursor: pointer;
            padding-left: 3px;
        }
        
        .RadUploadProgressArea
        {
            position: relative;
            top: 150px;
            left: 150px;
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        function Visualizza() {
            document.getElementById('Panel2').style.display = 'block';
        }
    </script>
</head>
<body style="background-repeat: no-repeat; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');">
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <asp:Panel runat="server" ID="Panel2" Style="position: absolute; top: 0px; left: 0px;
        width: 800px; height: 550px; display: none; background-color: #eeeeee">
        <br />
        <telerik:RadProgressManager ID="RadProgressManager1" runat="server" />
        <telerik:RadProgressArea ID="RadProgressArea1" runat="server" Language="" BackColor="#CCCCCC"
            HeaderText="Elaborazione file" Skin="Web20" ProgressIndicators="TotalProgressBar,TotalProgressPercent,TotalProgress,RequestSize,FilesCountBar,FilesCountPercent,FilesCount,SelectedFilesCount,CurrentFileName">
            <Localization UploadedFiles="Current item" CurrentFileName="Operazione:"></Localization>
        </telerik:RadProgressArea>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel1">
        <div style="padding-left: 5px;">
            <div>
                <table>
                    <tr>
                        <td>
                            <br />
                            <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Caricamento
                                massivo programma intervento</strong></span>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <div>
                                <fieldset style="width: 750px;">
                                    <legend>Download file excel</legend>
                                    <table style="width: 500px;">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            Scarica File Excel
                                                        </td>
                                                        <td style="width: 3%" valign="top">
                                                            <asp:Button ID="btnDownload" runat="server" CssClass="minibottone" Text="Download" /><br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <div>
                                <fieldset style="width: 750px;">
                                    <legend>Upload file excel</legend>
                                    <table style="width: 500px;">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            Carica File Excel
                                                        </td>
                                                        <td id="sfoglia">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                        </td>
                                                        <td style="width: 3%" valign="top">
                                                            <asp:Button ID="btnAllega" runat="server" CssClass="minibottone" Text="Upload" OnClientClick="Visualizza();" /><br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
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
                            <asp:TextBox ID="txtRisultati" runat="server" Width="750px" Height="100px" TextMode="MultiLine"
                                Visible="False" MaxLength="5000" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblErrore" runat="server" ForeColor="Red" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <div>
                                <asp:Button ID="btnHome" runat="server" CssClass="bottone2" Text="Home" ToolTip="Avvia ricerca" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divFooter">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 5px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div id="dialog" style="display: none;">
                </div>
                <div id="confirm" style="display: none;">
                </div>
                <div id="loading" style="display: none; text-align: center;">
                </div>
                <div id="divLoading" style="width: 0px; height: 0px; display: none;">
                    <img src="../Standard/Immagini/load.gif" id="imageLoading" alt="" />
                </div>
                <div id="divOscura" style="display: none; text-align: center; width: 100%; height: 100%;
                    position: absolute; top: 0px; left: 0px; background-color: #cccccc;">
                </div>
                <asp:HiddenField ID="tipoSubmit" runat="server" Value="0" />
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>

