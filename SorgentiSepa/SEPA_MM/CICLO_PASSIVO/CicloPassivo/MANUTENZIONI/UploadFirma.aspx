<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UploadFirma.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_UploadFirma" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../CicloPassivo.js"></script>
    <link href="../../CicloPassivo.css" rel="stylesheet" />
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js"></script>
    <title>Upload Firma</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
         <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <div>
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td colspan="2" style="font-size: 14pt;" class="TitoloModulo">Allega Firma
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="font-size: 14pt; color: Maroon; font-weight: bold;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="ImageButton1" runat="server" Text="Allega file" Style="cursor: pointer;" />
                                </td>
                                <td>
                                    <asp:Button ID="ImageButton2" runat="server" Text="Esci" Style="cursor: pointer;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Firma attuale   
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="" runat="server" ID="Immagine" />
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <strong>Per eseguire l'upload della firma sono consentite solamente 
                    immagini con la seguente estensione: jpg,jpeg,png,bmp.
                    <br />
                            Per una corretta visualizzazione della firma nelle stampe inserire un'immagine di dimensioni 300x120.
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="400px" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 150px;"></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right"></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
