<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaPerSchedaCens.aspx.vb" Inherits="CENSIMENTO_RicercaPerSchedaCens" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ricerca per Schede Rilievo</title>
        <script type = "text/javascript" >

            function AvviaRicerca() {
                parent.main.location.replace('RisultSchede.aspx?SCHEDA=' + document.getElementById('DrlSchede').value);

            }

        </script>
        <style type="text/css">
        #form1
        {
            width: 787px;
            height: 460px;
        }
        .styleTitle
        {
            font-weight:bold ;
            font-family: Arial;
            font-size: 14pt;
            color:#801f1c;

        }
        .stComboTitleBold
        {
            font-weight:bold ;
            font-family: Arial;
            font-size: 10pt;
        }
        .styleTesto
        {
            font-family: Arial;
            font-size: 10pt;
        }
    </style>

</head>
	<body style="background-attachment: fixed; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat:no-repeat; width: 792px;">
    <form id="form1" runat="server">
                <table style="width: 100%;">
        <tr>
            <td class="styleTitle">
                Ricerca degli Immobili per Scheda Rilievo</td>
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
                <table style="width:100%;">
                    <tr>
                        <td class="stComboTitleBold" >
                            Selezionare una
                            Schede Rilievo</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DrlSchede" runat="server" class="styleTesto" 
                                Width="98%" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: right">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td>
                        <img onclick="document.location.href='pagina_home.aspx';" alt="Home" src="../NuoveImm/Img_Home.png" 
                          style="cursor:pointer;" title="Home"/></td>
                        <td style="text-align: right">
                        <img onclick="AvviaRicerca();" alt="Home" src="../NuoveImm/Img_Procedi.png" 
                          style="cursor:pointer;" title="Avvia la ricerca"/></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        </table>

            
            
    </form>
</body>
</html>
