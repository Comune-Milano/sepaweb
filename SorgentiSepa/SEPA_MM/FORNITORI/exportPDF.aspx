<%@ Page Language="VB" AutoEventWireup="false" CodeFile="exportPDF.aspx.vb" Inherits="FORNITORI_exportPDF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
          
    .rsVerticalHeaderTable th  
    {  
        color: Red !important;  
    }  
      
    /*css selector for time header */ 
    .rsVerticalHeaderTable th div  
    {  
        color: black !important;  
    } 
    
 
    
</style> 
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>

        <div>
            <telerik:RadScheduler RenderMode="Lightweight" runat="server" ID="RadAgenda" SelectedView="TimelineView"
                DayEndTime="23:59:00" DayStartTime="00:01:00" RowHeight="30px" DataKeyField="ID_MANUTENZIONE"
                DataSubjectField="NUM_ODL" DataStartField="DATA_INIZIO_INTERVENTO" DataEndField="DATA_INIZIO_INTERVENTO"
                Localization-HeaderMultiDay="Work Week" AllowDelete="False" Culture="it-IT" DisplayDeleteConfirmation="False"
                AppointmentStyleMode="Simple" Skin="Web20" ShowViewTabs="False" ShowHoursColumn="False"
                AllowEdit="False" AllowInsert="False" ReadOnly="True" Width="1500px">
                <ExportSettings FileName="OrdiniExport" OpenInNewWindow="True">
                    <Pdf Author="Sepa@Com" Title="Elenco ordini" PageHeight="270mm" PageWidth="210mm" PaperSize="A4" PaperOrientation="Portrait" FontType="Embed" PageLeftMargin="20px" PageHeaderMargin="40px" />
                </ExportSettings>
                <AdvancedForm Modal="true"></AdvancedForm>
                <Localization HeaderMultiDay="Work Week"></Localization>
                <MultiDayView UserSelectable="false"></MultiDayView>
                <DayView UserSelectable="false"></DayView>
                <TimelineView ShowInsertArea="True" ReadOnly="True" SlotDuration="1.00:00:00" />
                <WeekView UserSelectable="false"></WeekView>
                <MonthView UserSelectable="false"></MonthView>
                <TimeSlotContextMenuSettings EnableEmbeddedBaseStylesheet="False" EnableEmbeddedScripts="False"
                    EnableEmbeddedSkins="False"></TimeSlotContextMenuSettings>
                <AgendaView UserSelectable="False" />
                <AppointmentContextMenuSettings EnableEmbeddedBaseStylesheet="False" EnableEmbeddedScripts="False"
                    EnableEmbeddedSkins="False"></AppointmentContextMenuSettings>
            </telerik:RadScheduler>
        </div>
    </form>
   
</body>
</html>
