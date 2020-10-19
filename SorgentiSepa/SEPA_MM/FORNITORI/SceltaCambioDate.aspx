<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SceltaCambioDate.aspx.vb" Inherits="FORNITORI_SceltaCambioDate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Conferma Modifica Date PGI/TDL</title>
    
</head>
<body>
    <form id="Form2" method="post" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" DecoratedControls="All" runat="server"
            Skin="Web20" />
        <script type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }
 
           
            function populateCityName(arg) {
                var cityName = document.getElementById("cityName");
                cityName.value = arg;
            }
 
           
            function returnToParent() {
                //create the argument that will be returned to the parent page
                var oArg = new Object();
 
                //get the city's name            
                oArg.cityName = document.getElementById("cityName").value;
 
                //get the selected date from RadDatePicker
                var datePicker = $find("<%= Datepicker1.ClientID %>");
                oArg.selDate = datePicker.get_selectedDate().toLocaleDateString();
 
                //get a reference to the current RadWindow
                var oWnd = GetRadWindow();
 
 
 
 
                //Close the RadWindow and send the argument to the parent page
                if (oArg.selDate && oArg.cityName) {
                    oWnd.close(oArg);
                }
                else {
                    alert("Please fill both fields");
                }
            }
        </script>
        <div style="width: 268px; height: 193px;">
            <fieldset id="fld1">
                <legend>One way ticket</legend><span style="margin: 6px 0 0 18px;">Choose date:</span>
                <telerik:RadDatePicker ID="Datepicker1" Skin="Sunset" runat="server" Width="140" PopupDirection="BottomLeft">
                    
                </telerik:RadDatePicker>
                <div style="margin: 20px 0 0 0;">
                    <div style="float: left; margin: 6px 0 0 18px;">
                        Choose City:
                    </div>
                    <input type="text" style="width: 100px;" id="cityName" value="Sofia" />
                    <button style="" title="Choose City">
                        ...</button>
                </div>
            </fieldset>
            <div style="margin-top: 4px; text-align: right;">
                <button title="Submit" id="close" onclick="returnToParent(); return false;">
                    Conferma</button>
            </div>
        </div>
    </form>
</body>
</html>
