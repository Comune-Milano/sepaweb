<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DoveCF.aspx.vb" Inherits="Public_DoveCF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Aiuto Ricerca Posizione</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table width="100%">
            <tr>
                <td style="height: 21px">
                    Bisogna indicare il Codice Fiscale dell'intestatario della domanda
                    di bando. Quest'ultimo
                    si trova nella prima pagina della Domanda.<br />
                    Non sono validi i Codici Fiscali degli altri componenti del nucleo famigliare.</td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/IMG/DoveCF.gif" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
