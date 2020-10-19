
Partial Class Condomini_Eventi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            Select Case Request.QueryString("CHIAMA")
                Case "CO"
                    ComplexEvent(Request.QueryString("ID"))
                Case "ED"
                    EdifixEvent(Request.QueryString("ID"))
                Case "UI"
                    UIxEvent(Request.QueryString("ID"))
                Case "UC"
                    UCxEvent(Request.QueryString("ID"))
            End Select
        End If
    End Sub
    Private Sub ComplexEvent(ByVal IdComplesso As String)
        Try
            If IdComplesso <> "" And IdComplesso <> 0 Then

                Dim OPERATORE As String = ""
                Dim MiaData As String = ""

                par.OracleConn.Open()
                par.SettaCommand(par)
                'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

                'par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO WHERE RAPPORTI_UTENZA.ID=" & IdContratto
                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader.Read Then
                'Response.Write("CONDOMINI, ID_CONDOMINIO: <strong>" & Codice & "</strong><br />")
                Response.Write("<br />")
                'End If
                'myReader.Close()

                Response.Write("<table width='100%'>")
                Response.Write("<tr>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>DATA</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>COMPLESSO</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>TIPO EVENTO</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>OPERATORE</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>MOTIVAZIONE</strong></span></td>")

                Response.Write("</tr>")

                par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS COMPLESSO, SEPA.OPERATORI.OPERATORE, EVENTI_COMPLESSI.DATA_ORA AS DATA_EVENTO, SISCOM_MI.TAB_EVENTI.DESCRIZIONE AS TIPO_EVENTO, EVENTI_COMPLESSI.MOTIVAZIONE FROM SISCOM_MI.EVENTI_COMPLESSI, SEPA.OPERATORI, SISCOM_MI.TAB_EVENTI, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EVENTI_COMPLESSI.COD_EVENTO = TAB_EVENTI.COD AND EVENTI_COMPLESSI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID AND EVENTI_COMPLESSI.ID_OPERATORE = OPERATORI.ID AND ID_COMPLESSO = " & IdComplesso & " ORDER BY DATA_ORA DESC"

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                Do While myReader.Read()



                    MiaData = Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 1, 4)
                    If IsDate(MiaData) = True Then
                        MiaData = MiaData & " " & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 9, 2) & ":" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 11, 2)
                    Else
                        MiaData = ""
                    End If


                    OPERATORE = par.IfNull(myReader("OPERATORE"), "")


                    Response.Write("<tr>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & MiaData & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("COMPLESSO"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("TIPO_EVENTO"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & OPERATORE & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("MOTIVAZIONE"), "") & "</span></td>")

                    Response.Write("</tr>")

                Loop
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else

            End If

        Catch ex As Exception

            par.OracleConn.Close()
            Response.Write(ex.Message)

        End Try
    End Sub
    Private Sub EdifixEvent(ByVal IdEdificio As String)
        Try
            If IdEdificio <> "" And IdEdificio <> 0 Then

                Dim OPERATORE As String = ""
                Dim MiaData As String = ""

                par.OracleConn.Open()
                par.SettaCommand(par)
                'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

                'par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO WHERE RAPPORTI_UTENZA.ID=" & IdContratto
                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader.Read Then
                'Response.Write("CONDOMINI, ID_CONDOMINIO: <strong>" & Codice & "</strong><br />")
                Response.Write("<br />")
                'End If
                'myReader.Close()

                Response.Write("<table width='100%'>")
                Response.Write("<tr>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>DATA</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>EDIFICIO</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>TIPO EVENTO</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>OPERATORE</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>MOTIVAZIONE</strong></span></td>")

                Response.Write("</tr>")

                par.cmd.CommandText = "SELECT EDIFICI.DENOMINAZIONE AS EDIFICIO, SEPA.OPERATORI.OPERATORE, EVENTI_EDIFICI.DATA_ORA AS DATA_EVENTO, SISCOM_MI.TAB_EVENTI.DESCRIZIONE AS TIPO_EVENTO, EVENTI_EDIFICI.MOTIVAZIONE FROM SISCOM_MI.EVENTI_EDIFICI, SEPA.OPERATORI, SISCOM_MI.TAB_EVENTI, SISCOM_MI.EDIFICI WHERE EVENTI_EDIFICI.COD_EVENTO = TAB_EVENTI.COD AND EVENTI_EDIFICI.ID_EDIFICIO = EDIFICI.ID AND EVENTI_EDIFICI.ID_OPERATORE = OPERATORI.ID AND ID_EDIFICIO = " & IdEdificio & " ORDER BY DATA_ORA DESC"

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                Do While myReader.Read()



                    MiaData = Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 1, 4)
                    If IsDate(MiaData) = True Then
                        MiaData = MiaData & " " & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 9, 2) & ":" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 11, 2)
                    Else
                        MiaData = ""
                    End If


                    OPERATORE = par.IfNull(myReader("OPERATORE"), "")


                    Response.Write("<tr>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & MiaData & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("EDIFICIO"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("TIPO_EVENTO"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & OPERATORE & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("MOTIVAZIONE"), "") & "</span></td>")

                    Response.Write("</tr>")

                Loop
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else

            End If

        Catch ex As Exception

            par.OracleConn.Close()
            Response.Write(ex.Message)

        End Try
    End Sub

    Private Sub UIxEvent(ByVal IdUnitaImmob As String)
        Try
            If IdUnitaImmob <> "" And IdUnitaImmob <> 0 Then

                Dim OPERATORE As String = ""
                Dim MiaData As String = ""

                par.OracleConn.Open()
                par.SettaCommand(par)
                'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

                'par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO WHERE RAPPORTI_UTENZA.ID=" & IdContratto
                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader.Read Then
                'Response.Write("CONDOMINI, ID_CONDOMINIO: <strong>" & Codice & "</strong><br />")
                Response.Write("<br />")
                'End If
                'myReader.Close()

                Response.Write("<table width='100%'>")
                Response.Write("<tr>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>DATA</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>COD.UNITA IMMOB.</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>TIPO EVENTO</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>OPERATORE</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>MOTIVAZIONE</strong></span></td>")

                Response.Write("</tr>")

                par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UI, SEPA.OPERATORI.OPERATORE, EVENTI_UI.DATA_ORA AS DATA_EVENTO, SISCOM_MI.TAB_EVENTI.DESCRIZIONE AS TIPO_EVENTO, EVENTI_UI.MOTIVAZIONE FROM SISCOM_MI.EVENTI_UI, SEPA.OPERATORI, SISCOM_MI.TAB_EVENTI, SISCOM_MI.UNITA_IMMOBILIARI WHERE EVENTI_UI.COD_EVENTO = TAB_EVENTI.COD AND EVENTI_UI.ID_UI = UNITA_IMMOBILIARI.ID AND EVENTI_UI.ID_OPERATORE = OPERATORI.ID AND ID_UI = " & IdUnitaImmob & " ORDER BY DATA_ORA DESC"

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                Do While myReader.Read()



                    MiaData = Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 1, 4)
                    If IsDate(MiaData) = True Then
                        MiaData = MiaData & " " & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 9, 2) & ":" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 11, 2)
                    Else
                        MiaData = ""
                    End If


                    OPERATORE = par.IfNull(myReader("OPERATORE"), "")


                    Response.Write("<tr>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & MiaData & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("COD_UI"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("TIPO_EVENTO"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & OPERATORE & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("MOTIVAZIONE"), "") & "</span></td>")

                    Response.Write("</tr>")

                Loop
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else

            End If

        Catch ex As Exception

            par.OracleConn.Close()
            Response.Write(ex.Message)

        End Try
    End Sub

    Private Sub UCxEvent(ByVal IdComplesso As String)
        Try
            If IdComplesso <> "" And IdComplesso <> 0 Then

                Dim OPERATORE As String = ""
                Dim MiaData As String = ""

                par.OracleConn.Open()
                par.SettaCommand(par)
                'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

                'par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO WHERE RAPPORTI_UTENZA.ID=" & IdContratto
                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader.Read Then
                'Response.Write("CONDOMINI, ID_CONDOMINIO: <strong>" & Codice & "</strong><br />")
                Response.Write("<br />")
                'End If
                'myReader.Close()

                Response.Write("<table width='100%'>")
                Response.Write("<tr>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>DATA</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>COD.UNITA COMUNE</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>TIPO EVENTO</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>OPERATORE</strong></span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>MOTIVAZIONE</strong></span></td>")

                Response.Write("</tr>")

                par.cmd.CommandText = "SELECT UNITA_COMUNI.COD_UNITA_COMUNE AS COD_UC, SEPA.OPERATORI.OPERATORE, EVENTI_UC.DATA_ORA AS DATA_EVENTO, SISCOM_MI.TAB_EVENTI.DESCRIZIONE AS TIPO_EVENTO, EVENTI_UC.MOTIVAZIONE FROM SISCOM_MI.EVENTI_UC, SEPA.OPERATORI, SISCOM_MI.TAB_EVENTI, SISCOM_MI.UNITA_COMUNI WHERE EVENTI_UC.COD_EVENTO = TAB_EVENTI.COD AND EVENTI_UC.ID_UC = UNITA_COMUNI.ID AND EVENTI_UC.ID_OPERATORE = OPERATORI.ID AND ID_UC = " & IdComplesso & " ORDER BY DATA_ORA DESC"

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                Do While myReader.Read()



                    MiaData = Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 1, 4)
                    If IsDate(MiaData) = True Then
                        MiaData = MiaData & " " & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 9, 2) & ":" & Mid(par.IfNull(myReader("DATA_EVENTO"), "          "), 11, 2)
                    Else
                        MiaData = ""
                    End If


                    OPERATORE = par.IfNull(myReader("OPERATORE"), "")


                    Response.Write("<tr>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & MiaData & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("COD_UC"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("TIPO_EVENTO"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & OPERATORE & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("MOTIVAZIONE"), "") & "</span></td>")

                    Response.Write("</tr>")

                Loop
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else

            End If

        Catch ex As Exception

            par.OracleConn.Close()
            Response.Write(ex.Message)

        End Try
    End Sub

End Class
