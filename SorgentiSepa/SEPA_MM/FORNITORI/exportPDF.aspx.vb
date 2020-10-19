Imports Telerik.Web.UI
Imports Telerik.Web.UI.Scheduler

Partial Class FORNITORI_exportPDF
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        RicavaOrdini()
    End Sub

    Private Sub RicavaOrdini()
        Try
            Select Case Session.Item("g3")
                Case "7"
                    RadAgenda.RowHeaderWidth = Unit.Pixel(500)
                    RadAgenda.RowHeight = Unit.Pixel(35)
                    RadAgenda.ColumnWidth = Unit.Pixel(330)
                    RadAgenda.Width = Unit.Pixel(3100)
                    RadAgenda.Font.Name = "arial"
                    RadAgenda.Font.Size = 16
                Case "14"
                    RadAgenda.RowHeaderWidth = Unit.Pixel(300)
                    RadAgenda.RowHeight = Unit.Pixel(35)
                    RadAgenda.ColumnWidth = Unit.Pixel(180)
                    RadAgenda.Width = Unit.Pixel(3100)
                    RadAgenda.Font.Name = "arial"
                    RadAgenda.Font.Size = 14
                Case "21"
                    RadAgenda.RowHeaderWidth = Unit.Pixel(300)
                    RadAgenda.RowHeight = Unit.Pixel(35)
                    RadAgenda.ColumnWidth = Unit.Pixel(120)
                    RadAgenda.Width = Unit.Pixel(3000)
                    RadAgenda.Font.Name = "arial"
                    RadAgenda.Font.Size = 12
            End Select

            RadAgenda.DataStartField = "DATA_INIZIO_INTERVENTO"
            RadAgenda.DataSubjectField = "RIFERIMENTO"
            RadAgenda.DataEndField = "DATA_FINE_INTERVENTO"
            RadAgenda.DataKeyField = "ID_MANUTENZIONE"
            RadAgenda.DataDescriptionField = "STATO"


            RadAgenda.DataSource = par.getDataTableGrid(Session.Item("g1"))
            RadAgenda.DataBind()
            RadAgenda.SelectedView = SchedulerViewType.TimelineView
            RadAgenda.GroupingDirection = DirectCast([Enum].Parse(GetType(GroupingDirection), "Vertical"), GroupingDirection)

            RadAgenda.ResourceTypes.Clear()
            Dim restype1 As New ResourceType("RAGIONE_SOCIALE")

            restype1.DataSource = par.getDataTableGrid(Session.Item("g2"))
            restype1.KeyField = "RAGIONE_SOCIALE"
            restype1.TextField = "RAGIONE_SOCIALE"
            restype1.ForeignKeyField = "RAGIONE_SOCIALE"

            RadAgenda.ResourceTypes.Add(restype1)
            RadAgenda.GroupBy = "RAGIONE_SOCIALE"
            RadAgenda.SelectedDate = Session.Item("g4")
            RadAgenda.DataBind()

            RadAgenda.TimelineView.NumberOfSlots = Session.Item("g3")


            RadAgenda.ExportSettings.Pdf.PaperSize = SchedulerPaperSize.A4
            RadAgenda.ExportSettings.Pdf.PaperOrientation = SchedulerPaperOrientation.Portrait
            RadAgenda.ExportSettings.FileName = "CalendarioInterventi_" & Format(Now, "yyyyMMdd")
            RadAgenda.ExportSettings.Pdf.PageTitle = "Calendario Interventi"
            RadAgenda.ExportSettings.Pdf.Author = "Sepa@Web"
            RadAgenda.ExportSettings.Pdf.Creator = "Sepa@Web"
            RadAgenda.ExportSettings.Pdf.Title = "Calendario Interventi"
            RadAgenda.ExportSettings.OpenInNewWindow = True
            RadAgenda.ExportSettings.Pdf.AllowPaging = True
            RadAgenda.ExportSettings.Pdf.DefaultFontFamily = "arial"
            RadAgenda.ExportSettings.Pdf.FontType = Telerik.Web.Apoc.Render.Pdf.FontType.Embed

            RadAgenda.GroupBy = "RAGIONE_SOCIALE"


            RadAgenda.ExportToPdf()


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - Segnalazioni - RicavaOrdini - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadAgenda_AppointmentDataBound(sender As Object, e As Telerik.Web.UI.SchedulerEventArgs) Handles RadAgenda.AppointmentDataBound
        e.Appointment.Font.Name = "arial"
        Select Case Session.Item("g3")
            Case "7"
                e.Appointment.Font.Size = 16
            Case "14"
                e.Appointment.Font.Size = 14
            Case "21"
                e.Appointment.Font.Size = 10
        End Select
    End Sub
End Class
