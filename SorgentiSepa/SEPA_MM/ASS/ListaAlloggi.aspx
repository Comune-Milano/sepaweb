<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListaAlloggi.aspx.vb" Inherits="ASS_ListaAlloggi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Lista Alloggi Disponibili</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="z-index: 123; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td style="width: 5px">
                </td>
            </tr>
            <tr>
                <td style="height: 26px">
                </td>
                <td style="height: 26px; text-align: center">
                    <img id="stampa" style="cursor:pointer;" alt="" src="../NuoveImm/Img_Stampa_Grande.png" onclick="return stampa_onclick()" />&nbsp; &nbsp; &nbsp;&nbsp;
                    <img id="CHIUDI" style="cursor:pointer;" alt="" src="../NuoveImm/Img_Esci_AMM.png" onclick="return CHIUDI_onclick()" /></td>
                <td style="width: 5px; height: 26px">
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
<script type="text/javascript">
window.focus();
self.focus();
function CHIUDI_onclick() {
    window.close();
}

function stampa_onclick() {
    window.print();
}

</script>
</html>
