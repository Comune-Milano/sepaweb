<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoImposte.aspx.vb" Inherits="Contratti_ElencoSimulazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Imposte</title>
    <style type="text/css">
        .stileComponenti {
            font-family: Arial;
            font-size: 12pt;
            padding-left: 20px;
            padding-right: 8px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <table style="left: 0px; width: 100%; position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                        Imposte</strong></span><br />
                </td>
            </tr>
            <tr>
                <td>&nbsp
                </td>
            </tr>
            <tr>
                <td class="stileComponenti">
                    <fieldset style="width: 96%">
                        <legend style="font-family: Arial; font-size: 9pt; font-weight: bold; color: Black;">Ann. Successive</legend>
                        <br />
                        <asp:Label ID="lblTbl2" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 28px; position: static; top: 203px"
                            TabIndex="1"></asp:Label>
                        <div style="overflow: auto; height: 320px;">
                            <asp:Label ID="lblAnnSucc" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 28px; position: static; top: 203px"
                                Width="710px" TabIndex="1"></asp:Label>
                        </div>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>&nbsp
                </td>
            </tr>
            <tr>
                <td>&nbsp
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:ImageButton ID="btnAggiorna" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Refresh.png" />
                    <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../NuoveImm/Img_Home.png" />
                </td>
            </tr>
            <tr>
                <td>&nbsp
                </td>
            </tr>
        </table>
        </div>
        <script type="text/javascript" language="javascript">
            function AnnullaInvio(nomefile) {
                var sicuro = window.confirm('Sei sicuro di voler Annullare questo invio? Si ricorda che sarà necessario ricreare il file di invio');
                if (sicuro == true) {
                    window.open('AnnullaInvioImposte.aspx?ID=' + nomefile , 'AnnulloImposte', '');
                }
                else {
                    
                }

            }

        </script>
    </form>
</body>
</html>
