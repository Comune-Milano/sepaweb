
Partial Class CAMBI_Scarico
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If


        If Not IsPostBack Then
            cmbAnnoa.SelectedIndex = -1
            cmbAnnoa.Items.FindByText(Year(Now)).Selected = True

            cmbMesea.SelectedIndex = -1
            cmbMesea.Items.FindByValue(Format(Month(Now), "00")).Selected = True

            cmbGa.SelectedIndex = -1
            cmbGa.Items.FindByText(Format(Day(Now), "00")).Selected = True
            RiempiOperatori()
        End If
    End Sub

    Private Function RiempiOperatori()
        Dim ds As New Data.DataSet()
        Dim dlist As CheckBoxList
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter

        Try



            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Function
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            dlist = CheckOperatori

            da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID, OPERATORE AS ""DESCRIZIONE"" FROM OPERATORI WHERE  ID_CAF=" & Session.Item("ID_CAF") & " ORDER BY ID ASC", par.OracleConn)
            da.Fill(ds)

            dlist.Items.Clear()
            dlist.DataSource = ds
            dlist.DataTextField = "DESCRIZIONE"
            dlist.DataValueField = "ID"
            dlist.DataBind()

            da.Dispose()
            da = Nothing

            dlist.DataSource = Nothing
            dlist = Nothing

            ds.Clear()
            ds.Dispose()
            ds = Nothing
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function

    Protected Sub btnChiudi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudi.Click
        Response.Write("<script>window.close();</script>")

    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizza.Click
        Dim INTERVALLO_DA As String
        Dim INTERVALLO_A As String
        Dim Operatore As String
        Dim OPERATORI As String
        Dim I As Integer

        INTERVALLO_DA = cmbAnnoDa.Text & cmbMDa.Text & cmbGDa.Text
        INTERVALLO_A = cmbAnnoa.Text & cmbMesea.Text & cmbGa.Text

        OPERATORI = ""

        For I = 0 To CheckOperatori.Items.Count - 1
            If CheckOperatori.Items(I).Selected Then
                OPERATORI = OPERATORI & CheckOperatori.Items(I).Value & ","
            End If
        Next
        If OPERATORI = "" Then
            Response.Write("<script>alert('Selezionare almeno un operatore');</script>")
            Exit Sub
        Else
            If OPERATORI <> "" Then OPERATORI = Mid(OPERATORI, 1, Len(OPERATORI) - 1)
        End If


        Operatore = Session.Item("ID_OPERATORE")
        If OPERATORI <> Operatore Then
            If UCase(txtPw.Text) = Format(Now, "yyyyMMdd") & "SIS" Then
                Operatore = OPERATORI
            Else
                Response.Write("<script>alert('Password non valida!');</script>")
                Exit Sub
            End If
        End If

        If Val(INTERVALLO_A) < Val(INTERVALLO_DA) Then
            Response.Write("<script>alert('Intervallo Date non valido!');</script>")
            Exit Sub
        End If
        Response.Write("<script>window.open('ListaScarico.aspx?PDA=" & INTERVALLO_DA & "&PA=" & INTERVALLO_A & "&OP=" & Operatore & "','Domande','');</script>")

    End Sub
End Class
