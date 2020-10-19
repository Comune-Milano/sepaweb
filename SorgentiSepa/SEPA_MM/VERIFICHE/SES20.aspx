<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SES20.aspx.vb" Inherits="AMMSEPA_Controllo1_Select1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>Select</title>
    <style type="text/css" title="currentStyle">
        @import "media/css/demo_table_jui.css";
        @import "media/css/jquery-ui-1.8.4.custom.css";
        @import "media/css/TableTools_JUI.css";
    </style>
    <script type="text/javascript" language="javascript">
        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 120) {
                document.getElementById('Button2').click();
                //e.preventDefault();
            }
        }

        function $onkeydown() {
            if (event.keyCode == 120) {
                //event.keyCode = '9';
                document.getElementById('Button2').click();

            }
        }
    </script>
    <script type="text/javascript" charset="utf-8" src="media/js/jquery.js"></script>
    <script type="text/javascript" charset="utf-8" src="media/js/jquery.dataTables.js"></script>
    <script type="text/javascript" charset="utf-8" src="media/js/ZeroClipboard.js"></script>
    <script type="text/javascript" charset="utf-8" src="media/js/TableTools.min.js"></script>
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $('#GridView1').dataTable({
                "sScrollY": "350px",
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                "sDom": '<"H"lfr>t<"F"Tip>',
                "oTableTools": {
                    "aButtons": [
							"copy", "xls", {
							    "sExtends": "pdf",
							    "sPdfMessage": ""
							},
							{
							    "sExtends": "collection",
							    "sButtonText": "Save",
							    "aButtons": ["xls", "pdf"]
							}
						]
                }
            });
        });
		</script>
</head>
<body style="font-family: arial, Helvetica, sans-serif; font-size: 13px">
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server" defaultbutton="Button1" defaultfocus="TextBox1">
    <div>
        <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="CONFERMA" />
        &nbsp;<br />
    </div>
    <asp:TextBox ID="TextBox2" runat="server" Height="230px" TextMode="MultiLine" Visible="False"
        Width="100%"></asp:TextBox>
    <br />
    <asp:Button ID="Button2" runat="server" Text="ESEGUI" Visible="False" />
    &nbsp;<asp:Button ID="btnExport" runat="server" Text="EXPORT XLS" Visible="False" />
    &nbsp;<asp:Button ID="btnExport0" runat="server" Text="XLS (numerico)" Visible="False"
        Width="107px" />
    <br />
    <br />
    <asp:Label ID="lblNumResult" runat="server" Visible="False" Font-Bold="True"></asp:Label>
    <br />
    <br />
    <div style="width: 100%; overflow: auto">
        <asp:GridView ID="GridView1" runat="server" CssClass="display" CellPadding="0" CellSpacing="0"
            BorderWidth="1px" Width="100%" Font-Names="Arial" Font-Size="10pt">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
