<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CaricamentoMassivoConguagli.aspx.vb" Inherits="Contratti_CaricamentoMassivoConguagli" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        .bottone2 {
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

            .bottone2:hover {
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
    </style>
    <style type="text/css">
        .RadUploadProgressArea {
            visibility: visible !important;
            width: auto !important;
            height: auto !important;
            position: absolute;
            top: 150px;
            left: 172px;
        }

        .auto-style1 {
            width: 365px;
        }

        .auto-style2 {
            width: 360px;
        }

        .auto-style3 {
            width: 300px;
        }

        .auto-style4 {
            visibility: visible;
            overflow: auto;
            width: 750px;
            height: 180px;
        }
        .auto-style5 {
            font-size: xx-small;
        }
    </style>
</head>
<body style="background-repeat: no-repeat; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');">
    <form id="form1" runat="server">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <div>
            <telerik:RadProgressManager ID="RadProgressManager1" runat="server" />
            <telerik:RadProgressArea ID="RadProgressArea1" runat="server" Language=""
                BackColor="#CCCCCC" HeaderText="Elaborazione file"
                Skin="Web20" CssClass="ExportProgress">
                <Localization UploadedFiles="Current item" CurrentFileName="Operazione:"></Localization>
            </telerik:RadProgressArea>
        </div>
        <div id="caricamento" style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee; z-index: 1">
            <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px; margin-top: -48px;">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td valign="middle" align="center">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="center"></td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="padding-left: 5px;">
            <div>
                <table>
                    <tr>
                        <td>
                            <br />
                            <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Caricamento massivo conguagli Anagrafe Utenza </strong></span>&nbsp;</td>
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
                                    <table style="width: 700px;">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>Seleziona l'Anagrafe Utenza di riferimento
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="cmbAU" runat="server" Height="20px" Style="border: 1px solid black;"
                                                                TabIndex="10" Width="350px" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Scarica File Excel
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
                                <fieldset class="auto-style1">
                                    <legend>Upload file excel con importi SINGOLA RATA</legend>
                                    <table class="auto-style2">
                                        <tr>
                                            <td>
                                                <table class="auto-style3">
                                                    <tr>
                                                        <td>Carica
                                                        </td>
                                                        <td id="sfoglia">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                        </td>
                                                        <td style="width: 3%" valign="top">
                                                            <asp:Button ID="btnAllega" runat="server" CssClass="minibottone" Text="Upload" OnClientClick="caricamentoincorso();" /><br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" class="auto-style5"><strong>Seleziona l'Anagrafe utenza di riferimento corretta
                                                        </strong>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                        <td>
                            <div>
                                <fieldset class="auto-style1">
                                    <legend>Upload file excel con importi TOTALE RATA</legend>
                                    <table class="auto-style2">
                                        <tr>
                                            <td>
                                                <table class="auto-style3">
                                                    <tr>
                                                        <td>Carica
                                                        </td>
                                                        <td id="sfoglia1">
                                                            <asp:FileUpload ID="FileUpload2" runat="server" />
                                                        </td>
                                                        <td style="width: 3%" valign="top">
                                                            <asp:Button ID="btnAllega1" runat="server" CssClass="minibottone" Text="Upload" OnClientClick="caricamentoincorso();" /><br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" class="auto-style5"><strong>Seleziona l'Anagrafe utenza di riferimento corretta
                                                        </strong>
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
                        <td align="right" colspan="2">
                            <div>
                            </div>
                        </td>
                    </tr>
                </table>
                <div id="cont" class="auto-style4">
                    <table style="width: 700px;">
                        <tr>
                            <td colspan="2">


                                <asp:Label ID="lblEsito" runat="server" Text="Esito" Visible="False"></asp:Label>


                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="txtRisultati" runat="server" Width="737px" Height="100px" TextMode="MultiLine"
                                    Visible="False" MaxLength="5000" ReadOnly="true"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnExport" runat="server" CssClass="bottone2" Text="Export" ToolTip="Export"  Visible="False" OnClientClick="Esporta();return false;" />
                            </td>
                        </tr>
                    </table>
                </div>
                <table style="width: 750px;">
                    <tr>
                        <td align="right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblErrore" runat="server" ForeColor="Red" Font-Names="Arial" Font-Size="8pt"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnHome" runat="server" CssClass="bottone2" Text="Home" ToolTip="Home" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divFooter">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 5px">&nbsp;
                        </td>
                        <td>&nbsp;
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
                <div id="divOscura" style="display: none; text-align: center; width: 100%; height: 100%; position: absolute; top: 0px; left: 0px; background-color: #cccccc;">
                </div>
                <asp:HiddenField ID="tipoSubmit" runat="server" Value="0" />
            </div>
        </div>
    </form>
    <script language="javascript" type="text/javascript">
        if (document.getElementById('caricamento') != null) {
            document.getElementById('caricamento').style.visibility = 'hidden';
        };

        function caricamentoincorso() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
                if (Page_IsValid) {
                    if (document.getElementById('caricamento') != null) {
                        document.getElementById('caricamento').style.visibility = 'visible';
                    };
                }
                else {
                    alert('ATTENZIONE! Ci sono delle incongruenze dati della pagina e/o eventuali TAB!');
                };
            }
            else {
                if (document.getElementById('caricamento') != null) {
                    document.getElementById('caricamento').style.visibility = 'visible';
                };
            };
        };
        function Esporta() {
            window.open('ExportCarMassivo.aspx', 'Export', 'height=400,width=700');
        };
    </script>
</body>
</html>

